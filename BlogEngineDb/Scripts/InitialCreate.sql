USE [BlogEngine]

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

IF OBJECT_ID(N'[Posts]') IS NULL
BEGIN
    CREATE TABLE [Posts] (
        [Id] int NOT NULL IDENTITY,
        [Title] nvarchar(450) NOT NULL,
        [Content] nvarchar(max) NOT NULL,
        [Created] datetime2 NOT NULL,
        [PublishDate] datetime2 NULL,
        [Author] nvarchar(450) NOT NULL,
        [Status] int NOT NULL,
        CONSTRAINT [PK_Posts] PRIMARY KEY ([Id])
    );

    CREATE INDEX [IX_Post] ON [Posts] ([Author], [Status]);
END
GO

IF NOT EXISTS (SELECT MigrationId FROM __EFMigrationsHistory where MigrationId = '20230224020957_InitialCreate')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20230224020957_InitialCreate', N'7.0.3');
END
GO

COMMIT;
GO


BEGIN TRANSACTION;
GO

IF OBJECT_ID(N'[Comments]') IS NULL
BEGIN
    CREATE TABLE [Comments] (
        [Id] int NOT NULL IDENTITY,
        [PostId] int NOT NULL,
        [Content] nvarchar(1000) NOT NULL,
        [IsRejection] bit NOT NULL,
        [CreatedDate] datetime2 NOT NULL,
        CONSTRAINT [PK_Comments] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_Comments_Posts_PostId] FOREIGN KEY ([PostId]) REFERENCES [Posts] ([Id]) ON DELETE CASCADE
    );

    CREATE INDEX [IX_Comments_PostId_IsRejection] ON [Comments] ([PostId], [IsRejection]);
END;
GO

IF NOT EXISTS (SELECT MigrationId FROM __EFMigrationsHistory where MigrationId = '20230224225108_AddTable_Comments')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20230224225108_AddTable_Comments', N'7.0.3');
END;
GO

COMMIT;
GO