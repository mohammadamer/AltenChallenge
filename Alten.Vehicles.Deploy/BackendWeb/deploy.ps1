
# Subscription - Tryg

$subscriptionId = "Your SubscriptionID"
$tenant = "Free Trial"

# App Settings Variables
#$samplevariable = "samplevalue"

$adminUserName = "SomeUserName"
$adminPassword = "SomePassword"

$templatefile = "backendweb.template.json"
$parametersfile = "backendweb.parameters.json"

.\deploytemplate.ps1 -subscriptionId $subscriptionId -O365Tenant $tenant -templatefile $templatefile -parametersfile $parametersfile  -adminUserName $adminUserName -adminPassword $adminPassword
