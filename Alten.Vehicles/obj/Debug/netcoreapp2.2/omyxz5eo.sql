IF OBJECT_ID(N'[__EFMigrationsHistory]') IS NULL
BEGIN
    CREATE TABLE [__EFMigrationsHistory] (
        [MigrationId] nvarchar(150) NOT NULL,
        [ProductVersion] nvarchar(32) NOT NULL,
        CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY ([MigrationId])
    );
END;

GO

CREATE TABLE [VehiclePings] (
    [ID] bigint NOT NULL IDENTITY,
    [VehicleID] int NOT NULL,
    [PingDate] datetime2 NOT NULL,
    CONSTRAINT [PK_VehiclePings] PRIMARY KEY ([ID])
);

GO

CREATE TABLE [Vehicles] (
    [ID] int NOT NULL IDENTITY,
    [VehicleId] nvarchar(max) NULL,
    [RegistrationNo] nvarchar(max) NULL,
    [CustomerID] int NOT NULL,
    [CustomerName] nvarchar(max) NULL,
    [LastPingTime] datetime2 NULL,
    CONSTRAINT [PK_Vehicles] PRIMARY KEY ([ID])
);

GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20191012190520_initial', N'2.2.6-servicing-10079');

GO

