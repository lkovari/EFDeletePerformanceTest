IF OBJECT_ID(N'[__EFMigrationsHistory]') IS NULL
BEGIN
    CREATE TABLE [__EFMigrationsHistory] (
        [MigrationId] nvarchar(150) NOT NULL,
        [ProductVersion] nvarchar(32) NOT NULL,
        CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY ([MigrationId])
    );
END;
GO

BEGIN TRANSACTION;
GO

CREATE TABLE [ZipCode] (
    [Id] int NOT NULL IDENTITY,
    [City] nvarchar(max) NOT NULL,
    [State] nvarchar(max) NOT NULL,
    [Zip] nvarchar(max) NOT NULL,
    [County] nvarchar(max) NULL,
    CONSTRAINT [PK_ZipCode] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [Address] (
    [Id] int NOT NULL IDENTITY,
    [Address1] nvarchar(max) NOT NULL,
    [Address2] nvarchar(max) NULL,
    [ZipCodeId] int NOT NULL,
    CONSTRAINT [PK_Address] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Address_ZipCode_ZipCodeId] FOREIGN KEY ([ZipCodeId]) REFERENCES [ZipCode] ([Id]) ON DELETE CASCADE
);
GO

CREATE INDEX [IX_Address_ZipCodeId] ON [Address] ([ZipCodeId]);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20240101133322_initial', N'8.0.0');
GO

COMMIT;
GO

