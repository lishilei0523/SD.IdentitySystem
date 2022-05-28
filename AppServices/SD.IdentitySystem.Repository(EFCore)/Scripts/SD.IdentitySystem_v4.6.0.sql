USE [master]
GO

CREATE DATABASE [SD.IdentitySystem]
GO

USE [SD.IdentitySystem]
GO

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

CREATE TABLE [InfoSystem] (
    [Id] uniqueidentifier NOT NULL,
    [AdminLoginId] nvarchar(max) NULL,
    [ApplicationType] int NOT NULL,
    [Host] nvarchar(max) NULL,
    [Port] int NULL,
    [Index] nvarchar(max) NULL,
    [AddedTime] datetime2 NOT NULL,
    [Number] nvarchar(16) NOT NULL,
    [Name] nvarchar(64) NOT NULL,
    [Keywords] nvarchar(256) NOT NULL,
    [SavedTime] datetime2 NOT NULL,
    [CreatorAccount] nvarchar(max) NULL,
    [CreatorName] nvarchar(max) NULL,
    [OperatorAccount] nvarchar(max) NULL,
    [OperatorName] nvarchar(max) NULL,
    CONSTRAINT [PK_InfoSystem] PRIMARY KEY NONCLUSTERED ([Id]),
    CONSTRAINT [AK_InfoSystem_Number] UNIQUE ([Number])
);
GO

CREATE TABLE [LoginRecord] (
    [Id] uniqueidentifier NOT NULL,
    [PublicKey] uniqueidentifier NOT NULL,
    [LoginId] nvarchar(max) NULL,
    [RealName] nvarchar(max) NULL,
    [IP] nvarchar(max) NULL,
    [ClientId] nvarchar(max) NULL,
    [PartitionIndex] int NOT NULL,
    [AddedTime] datetime2 NOT NULL,
    [Keywords] nvarchar(256) NOT NULL,
    [SavedTime] datetime2 NOT NULL,
    [CreatorAccount] nvarchar(max) NULL,
    [CreatorName] nvarchar(max) NULL,
    CONSTRAINT [PK_LoginRecord] PRIMARY KEY NONCLUSTERED ([Id])
);
GO

CREATE TABLE [User] (
    [Id] uniqueidentifier NOT NULL,
    [Password] nvarchar(32) NOT NULL,
    [PrivateKey] nvarchar(64) NOT NULL,
    [Enabled] bit NOT NULL,
    [AddedTime] datetime2 NOT NULL,
    [Number] nvarchar(20) NOT NULL,
    [Name] nvarchar(max) NULL,
    [Keywords] nvarchar(256) NOT NULL,
    [SavedTime] datetime2 NOT NULL,
    [CreatorAccount] nvarchar(max) NULL,
    [CreatorName] nvarchar(max) NULL,
    [OperatorAccount] nvarchar(max) NULL,
    [OperatorName] nvarchar(max) NULL,
    CONSTRAINT [PK_User] PRIMARY KEY NONCLUSTERED ([Id]),
    CONSTRAINT [AK_User_Number] UNIQUE ([Number]),
    CONSTRAINT [AK_User_PrivateKey] UNIQUE ([PrivateKey])
);
GO

CREATE TABLE [Authority] (
    [Id] uniqueidentifier NOT NULL,
    [InfoSystemNo] nvarchar(16) NOT NULL,
    [ApplicationType] int NOT NULL,
    [AuthorityPath] nvarchar(256) NOT NULL,
    [Description] nvarchar(max) NULL,
    [AddedTime] datetime2 NOT NULL,
    [Name] nvarchar(64) NOT NULL,
    [Keywords] nvarchar(256) NOT NULL,
    [SavedTime] datetime2 NOT NULL,
    [Deleted] bit NOT NULL,
    [DeletedTime] datetime2 NULL,
    [CreatorAccount] nvarchar(max) NULL,
    [CreatorName] nvarchar(max) NULL,
    [OperatorAccount] nvarchar(max) NULL,
    [OperatorName] nvarchar(max) NULL,
    CONSTRAINT [PK_Authority] PRIMARY KEY NONCLUSTERED ([Id]),
    CONSTRAINT [FK_Authority_InfoSystem_InfoSystemNo] FOREIGN KEY ([InfoSystemNo]) REFERENCES [InfoSystem] ([Number]) ON DELETE NO ACTION
);
GO

CREATE TABLE [Menu] (
    [Id] uniqueidentifier NOT NULL,
    [InfoSystemNo] nvarchar(16) NOT NULL,
    [ApplicationType] int NOT NULL,
    [Url] nvarchar(max) NULL,
    [Path] nvarchar(max) NULL,
    [Icon] nvarchar(max) NULL,
    [Sort] int NOT NULL,
    [IsRoot] bit NOT NULL,
    [ParentNode_Id] uniqueidentifier NULL,
    [AddedTime] datetime2 NOT NULL,
    [Name] nvarchar(32) NOT NULL,
    [Keywords] nvarchar(256) NOT NULL,
    [SavedTime] datetime2 NOT NULL,
    [CreatorAccount] nvarchar(max) NULL,
    [CreatorName] nvarchar(max) NULL,
    [OperatorAccount] nvarchar(max) NULL,
    [OperatorName] nvarchar(max) NULL,
    CONSTRAINT [PK_Menu] PRIMARY KEY NONCLUSTERED ([Id]),
    CONSTRAINT [FK_Menu_InfoSystem_InfoSystemNo] FOREIGN KEY ([InfoSystemNo]) REFERENCES [InfoSystem] ([Number]) ON DELETE NO ACTION,
    CONSTRAINT [FK_Menu_Menu_ParentNode_Id] FOREIGN KEY ([ParentNode_Id]) REFERENCES [Menu] ([Id]) ON DELETE NO ACTION
);
GO

CREATE TABLE [Role] (
    [Id] uniqueidentifier NOT NULL,
    [InfoSystemNo] nvarchar(16) NOT NULL,
    [Description] nvarchar(max) NULL,
    [AddedTime] datetime2 NOT NULL,
    [Number] nvarchar(max) NULL,
    [Name] nvarchar(32) NOT NULL,
    [Keywords] nvarchar(256) NOT NULL,
    [SavedTime] datetime2 NOT NULL,
    [CreatorAccount] nvarchar(max) NULL,
    [CreatorName] nvarchar(max) NULL,
    [OperatorAccount] nvarchar(max) NULL,
    [OperatorName] nvarchar(max) NULL,
    CONSTRAINT [PK_Role] PRIMARY KEY NONCLUSTERED ([Id]),
    CONSTRAINT [FK_Role_InfoSystem_InfoSystemNo] FOREIGN KEY ([InfoSystemNo]) REFERENCES [InfoSystem] ([Number]) ON DELETE NO ACTION
);
GO

CREATE TABLE [Menu_Authority] (
    [Authority_Id] uniqueidentifier NOT NULL,
    [Menu_Id] uniqueidentifier NOT NULL,
    CONSTRAINT [PK_Menu_Authority] PRIMARY KEY ([Authority_Id], [Menu_Id]),
    CONSTRAINT [FK_Menu_Authority_Authority_Authority_Id] FOREIGN KEY ([Authority_Id]) REFERENCES [Authority] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_Menu_Authority_Menu_Menu_Id] FOREIGN KEY ([Menu_Id]) REFERENCES [Menu] ([Id]) ON DELETE CASCADE
);
GO

CREATE TABLE [Role_Authority] (
    [Authority_Id] uniqueidentifier NOT NULL,
    [Role_Id] uniqueidentifier NOT NULL,
    CONSTRAINT [PK_Role_Authority] PRIMARY KEY ([Authority_Id], [Role_Id]),
    CONSTRAINT [FK_Role_Authority_Authority_Authority_Id] FOREIGN KEY ([Authority_Id]) REFERENCES [Authority] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_Role_Authority_Role_Role_Id] FOREIGN KEY ([Role_Id]) REFERENCES [Role] ([Id]) ON DELETE CASCADE
);
GO

CREATE TABLE [User_Role] (
    [Role_Id] uniqueidentifier NOT NULL,
    [User_Id] uniqueidentifier NOT NULL,
    CONSTRAINT [PK_User_Role] PRIMARY KEY ([Role_Id], [User_Id]),
    CONSTRAINT [FK_User_Role_Role_Role_Id] FOREIGN KEY ([Role_Id]) REFERENCES [Role] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_User_Role_User_User_Id] FOREIGN KEY ([User_Id]) REFERENCES [User] ([Id]) ON DELETE CASCADE
);
GO

CREATE CLUSTERED INDEX [IX_Authority_AddedTime] ON [Authority] ([AddedTime]);
GO

CREATE INDEX [IX_Authority_InfoSystemNo] ON [Authority] ([InfoSystemNo]);
GO

CREATE CLUSTERED INDEX [IX_InfoSystem_AddedTime] ON [InfoSystem] ([AddedTime]);
GO

CREATE CLUSTERED INDEX [IX_LoginRecord_AddedTime] ON [LoginRecord] ([AddedTime]);
GO

CREATE INDEX [IX_Menu_InfoSystemNo] ON [Menu] ([InfoSystemNo]);
GO

CREATE INDEX [IX_Menu_ParentNode_Id] ON [Menu] ([ParentNode_Id]);
GO

CREATE INDEX [IX_Menu_Authority_Menu_Id] ON [Menu_Authority] ([Menu_Id]);
GO

CREATE CLUSTERED INDEX [IX_Role_AddedTime] ON [Role] ([AddedTime]);
GO

CREATE INDEX [IX_Role_InfoSystemNo] ON [Role] ([InfoSystemNo]);
GO

CREATE INDEX [IX_Role_Authority_Role_Id] ON [Role_Authority] ([Role_Id]);
GO

CREATE CLUSTERED INDEX [IX_User_AddedTime] ON [User] ([AddedTime]);
GO

CREATE INDEX [IX_User_Role_User_Id] ON [User_Role] ([User_Id]);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20220528074620_v4.6.0', N'5.0.10');
GO

COMMIT;
GO
