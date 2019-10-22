
Param(
    [Parameter(Mandatory = $true)][string] $subscriptionId,
    [Parameter(Mandatory = $true)][string] $O365Tenant,
    [Parameter(Mandatory = $true)][string] $templatefile,
    [Parameter(Mandatory = $true)][string] $parametersfile,
   [Parameter(Mandatory = $true)][string] $adminUserName,
    [Parameter(Mandatory = $true)][string] $adminPassword
)

Import-Module AzureRm -ErrorAction SilentlyContinue
Get-Module AzureRM -list | Select-Object Name, Version, Path
Set-StrictMode -Version 3

function RegisterRP {
    Param(
        [string]$ResourceProviderNamespace
    )

    Write-Host "Registering resource provider '$ResourceProviderNamespace'";
    Register-AzureRmResourceProvider -ProviderNamespace $ResourceProviderNamespace 
}

function Deploy-AzureResources {
    Param(
        [Parameter(Mandatory = $true)][string] $ResourceGroupLocation,
        [Parameter(Mandatory = $true)][string] $ResourceGroupName,
        [Parameter(Mandatory = $true)][switch] $UploadArtifacts,
        [Parameter(Mandatory = $true)][string] $StorageAccountName,
        [Parameter(Mandatory = $true)][string] $StorageContainerName,
        [Parameter(Mandatory = $true)][string] $TemplateFile,
        [Parameter(Mandatory = $true)][string] $TemplateParametersFile,
        [Parameter(Mandatory = $true)][string] $artifactStagingDirectory,
        [Parameter(Mandatory = $true)][string] $dscfolder
    )

    try {
        [Microsoft.Azure.Common.Authentication.AzureSession]::ClientFactory.AddUserAgent("VSAzureTools-$UI$($host.name)".replace(" ", "_"), "2.9")
    }
    catch {
    }

    $OptionalParameters = New-Object -TypeName Hashtable
    $TemplateFile = [System.IO.Path]::GetFullPath([System.IO.Path]::Combine($PSScriptRoot, $TemplateFile))
    $TemplateParametersFile = [System.IO.Path]::GetFullPath([System.IO.Path]::Combine($PSScriptRoot, $TemplateParametersFile))

    if ($UploadArtifacts) {
        # Convert relative paths to absolute paths if needed
        $artifactStagingDirectory = [System.IO.Path]::GetFullPath([System.IO.Path]::Combine($PSScriptRoot, $artifactStagingDirectory))
        $dscfolder = [System.IO.Path]::GetFullPath([System.IO.Path]::Combine($PSScriptRoot, $dscfolder))

		#For Test
		Write-Host "Uploading Artifacts..."
		Write-Host $artifactStagingDirectory
		Write-Host $dscfolder

        Set-Variable ArtifactsLocationName '_artifactsLocation' -Option ReadOnly -Force
        Set-Variable ArtifactsLocationSasTokenName '_artifactsLocationSasToken' -Option ReadOnly -Force

        $OptionalParameters.Add($ArtifactsLocationName, $null)
        $OptionalParameters.Add($ArtifactsLocationSasTokenName, $null)

        $JsonContent = Get-Content $TemplateParametersFile -Raw | ConvertFrom-Json
        $JsonParameters = $JsonContent | Get-Member -Type NoteProperty | Where-Object {$_.Name -eq "parameters"}

        if ($JsonParameters -eq $null) {
            $JsonParameters = $JsonContent
        }
        else {
            $JsonParameters = $JsonContent.parameters
        }

        $JsonParameters | Get-Member -Type NoteProperty | ForEach-Object {
            $ParameterValue = $JsonParameters | Select-Object -ExpandProperty $_.Name
			
            Write-Host "Parameter Name: $($_.Name) -> $($ParameterValue.value)"
			
            if ($_.Name -eq $ArtifactsLocationName -or $_.Name -eq $ArtifactsLocationSasTokenName) {
                $OptionalParameters[$_.Name] = $ParameterValue.value
            }
        }

        # Create DSC configuration archive
        # if (Test-Path $dscfolder)
        # {
        # 	 Add-Type -Assembly System.IO.Compression.FileSystem
        #	 $ArchiveFile = Join-Path $artifactStagingDirectory "dsc.zip"
        #	 Remove-Item -Path $ArchiveFile -ErrorAction SilentlyContinue
        #	 [System.IO.Compression.ZipFile]::CreateFromDirectory($dscfolder, $ArchiveFile)
        # }

        $StorageAccountContext = (Get-AzureRmStorageAccount | Where-Object {$_.StorageAccountName -eq $StorageAccountName}).Context

        $ArtifactsLocation = $OptionalParameters[$ArtifactsLocationName]
        if ($ArtifactsLocation -eq $null) {
            $ArtifactsLocation = $StorageAccountContext.BlobEndPoint + $StorageContainerName
            $OptionalParameters[$ArtifactsLocationName] = $ArtifactsLocation
        }

        Write-Host "Parameter Name: $($ArtifactsLocationName) -> $($ArtifactsLocation)"
		
        # Copy files from the local storage staging location to the storage account container
        New-AzureStorageContainer -Name $StorageContainerName -Context $StorageAccountContext -Permission Container -ErrorAction SilentlyContinue *>&1

        $ArtifactFilePaths = Get-ChildItem $artifactStagingDirectory -Recurse -File | ForEach-Object -Process {$_.FullName}
        foreach ($SourcePath in $ArtifactFilePaths) {
            $BlobName = $SourcePath.Substring($artifactStagingDirectory.length + 1)
            Set-AzureStorageBlobContent -File $SourcePath -Blob $BlobName -Container $StorageContainerName -Context $StorageAccountContext -Force

			#For Test
		    Write-Host $BlobName
        }

        # Generate the value for artifacts location SAS token if it is not provided in the parameter file
        $ArtifactsLocationSasToken = $OptionalParameters[$ArtifactsLocationSasTokenName]
        if ($ArtifactsLocationSasToken -eq $null) {
            # Create a SAS token for the storage container - this gives temporary read-only access to the container
            $ArtifactsLocationSasToken = New-AzureStorageContainerSASToken -Container $StorageContainerName -Context $StorageAccountContext -Permission r -ExpiryTime (Get-Date).AddHours(4)
            $ArtifactsLocationSasToken = ConvertTo-SecureString $ArtifactsLocationSasToken -AsPlainText -Force
            $OptionalParameters[$ArtifactsLocationSasTokenName] = $ArtifactsLocationSasToken
        }
    }




    New-AzureRmResourceGroupDeployment -Name ((Get-ChildItem $TemplateFile).BaseName + '-' + ((Get-Date).ToUniversalTime()).ToString('MMdd-HHmm')) `
        -ResourceGroupName $ResourceGroupName `
        -TemplateFile $TemplateFile `
        -TemplateParameterFile $TemplateParametersFile `
        @OptionalParameters `
        -Force -Verbose
}

function Get-ParameterValue {
    Param(
        [Parameter(Mandatory = $true)][string] $ParameterName,
        [Parameter(Mandatory = $true)][string] $TemplateParametersFile
    )

    $JsonContent = Get-Content $TemplateParametersFile -Raw | ConvertFrom-Json
    $JsonParameters = $JsonContent | Get-Member -Type NoteProperty | Where-Object {$_.Name -eq "parameters"}

    if ($JsonParameters -eq $null) {
        $JsonParameters = $JsonContent
    }
    else {
        $JsonParameters = $JsonContent.parameters
    }

    $JsonParameters | Get-Member -Type NoteProperty | ForEach-Object {
        $ParameterValue = $JsonParameters | Select-Object -ExpandProperty $_.Name
        if ($_.Name -eq $ParameterName) {
            return "$($ParameterValue.value)"
        }
    }
    return $null
}

function Create-ResourceGroup {
    Param(
        [Parameter(Mandatory = $true)][string] $ResourceGroupName,
        [Parameter(Mandatory = $true)][string] $ResourceGroupLocation
    )

    if (!(Get-AzureRmResourceGroup -Name $ResourceGroupName -ErrorAction SilentlyContinue)) {
        Write-Host "Creating [$ResourceGroupName] in [$ResourceGroupLocation]..."
        New-AzureRmResourceGroup -Name $ResourceGroupName -Location $ResourceGroupLocation
    }
    else {
        Write-Host "Using existing [$ResourceGroupName]..."
    }
}

function Create-StorageAccount {
    Param(
        [Parameter(Mandatory = $true)][string]$ResourceGroupName,
        [Parameter(Mandatory = $true)][string]$ResourceGroupLocation,
        [Parameter(Mandatory = $true)][string]$storagedeploy
    )

    if (!(Get-AzureRmStorageAccount | where {$_.StorageAccountName -eq "$storagedeploy"})) {
        Write-Host "Creating [$storagedeploy] in [$ResourceGroupLocation]..."
        New-AzureRmStorageAccount -ResourceGroupName "$ResourceGroupName" -Name "$storagedeploy" -SkuName "Standard_LRS" -Location "$($ResourceGroupLocation)"
    }
    else {
        Write-Host "Using existing [$storagedeploy]..."
    }
}

function Add-ResourceProviders {
    $resourceProviders = @("microsoft.insights", "microsoft.web");
    if ($resourceProviders.length) {
        Write-Host "Registering resource providers"
        foreach ($resourceProvider in $resourceProviders) {
            RegisterRP($resourceProvider);
        }
    }
}

# ********************************************************************

[string]$sitename = Get-ParameterValue -ParameterName "name_webapp" -TemplateParametersFile $parametersfile
[string]$ResourceGroupName = Get-ParameterValue -ParameterName "name_resourcegroup" -TemplateParametersFile $parametersfile
[string]$ResourceGroupLocation = Get-ParameterValue -ParameterName "config_location" -TemplateParametersFile $parametersfile
[string]$storagedeploy = Get-ParameterValue -ParameterName "name_storagedeploy" -TemplateParametersFile $parametersfile
[string]$insightsName = Get-ParameterValue -ParameterName "name_appinsights" -TemplateParametersFile $parametersfile

$sitename = $sitename.TrimEnd(' ')
$ResourceGroupName = $ResourceGroupName.TrimEnd(' ')
$storagedeploy = $storagedeploy.TrimEnd(' ')
$ResourceGroupLocation = $ResourceGroupLocation.TrimEnd(' ')
$insightsName = $insightsName.TrimEnd(' ')
$artifactStagingDirectory = '.\staging'
$dscfolder = '.\DSC'

Write-Host "Logging in...";
Login-AzureRmAccount -SubscriptionId $subscriptionId

Add-ResourceProviders
Create-ResourceGroup -ResourceGroupName $ResourceGroupName -ResourceGroupLocation $ResourceGroupLocation

Create-StorageAccount -ResourceGroupName $ResourceGroupName -ResourceGroupLocation $ResourceGroupLocation -storagedeploy $storagedeploy


Write-Host "Starting deployment...";
Write-Host "ArtifactStagingDirectory: $($artifactStagingDirectory)"
Write-Host "ResourceGroupLocation: $($ResourceGroupLocation)"
Write-Host "ResourceGroupName: $($ResourceGroupName)"
Write-Host "StorageAccountName: $($storagedeploy)"
Write-Host "StorageContainerName: $($storagedeploy)"
Write-Host "TemplateFile: $($templatefile)"
Write-Host "TemplateParametersFile: $($parametersfile)"
Write-Host "dscfolder: $($dscfolder)"

Deploy-AzureResources -ResourceGroupName "$($ResourceGroupName)" -ResourceGroupLocation "$($ResourceGroupLocation)" -UploadArtifacts -StorageAccountName "$($storagedeploy)" -StorageContainerName "$($storagedeploy)stage".ToLowerInvariant() -TemplateFile $templatefile -TemplateParametersFile $parametersfile -artifactStagingDirectory "$($artifactStagingDirectory)" -dscfolder "$($dscfolder)"   

 # Find-AzureRmResource is deprecated for latest Azure RM version we should use "Get-AzureRmResource" instead
Write-Host -NoNewLine "Getting instrumentation id..."
#$resource = (Find-AzureRmResource -ResourceNameContains "$insightsName") | where { $_.ResourceType -eq "microsoft.insights/components"}
$resource = (Get-AzureRmResource -ResourceGroupName $ResourceGroupName -Name "$insightsName") | where { $_.ResourceType -eq "microsoft.insights/components"}
$resourceDetails = Get-AzureRmResource -ResourceId $resource.ResourceId
$instrumentationKey = $resourceDetails.Properties.InstrumentationKey

$resource | where { $_.ResourceType -eq "microsoft.insights/components"}

Write-Host "Updating app settings...";
 $webApp = Get-AzureRMWebApp -ResourceGroupName $ResourceGroupName -Name $sitename
 $appSettingList = $webApp.SiteConfig.AppSettings
 $hash = @{}
 ForEach ($app in $appSettingList) { $hash[$app.Name] = $app.Value }
 $hash["InstrumentationKey"] = "$instrumentationKey"


 Set-AzureRmWebApp -ResourceGroupName $ResourceGroupName -Name $sitename -AppSettings $hash 

Write-Host "Done"