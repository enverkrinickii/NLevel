
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 12/09/2017 21:08:24
-- Generated from EDMX file: c:\users\enver\Source\Repos\NLevel\NLevel\ModelManager.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [MyStore];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------


-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------


-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'Managers'
CREATE TABLE [dbo].[Managers] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Surname] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'Clients'
CREATE TABLE [dbo].[Clients] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Surname] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'Products'
CREATE TABLE [dbo].[Products] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [ProductName] nvarchar(max)  NOT NULL,
    [ProductCost] float  NOT NULL
);
GO

-- Creating table 'PurchasesInfo'
CREATE TABLE [dbo].[PurchasesInfo] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [SaleDate] nvarchar(max)  NOT NULL,
    [ManagerId] int  NOT NULL,
    [ClientId] int  NOT NULL,
    [ProductId] int  NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [Id] in table 'Managers'
ALTER TABLE [dbo].[Managers]
ADD CONSTRAINT [PK_Managers]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Clients'
ALTER TABLE [dbo].[Clients]
ADD CONSTRAINT [PK_Clients]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Products'
ALTER TABLE [dbo].[Products]
ADD CONSTRAINT [PK_Products]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'PurchasesInfo'
ALTER TABLE [dbo].[PurchasesInfo]
ADD CONSTRAINT [PK_PurchasesInfo]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [ManagerId] in table 'PurchasesInfo'
ALTER TABLE [dbo].[PurchasesInfo]
ADD CONSTRAINT [FK_ManagerPurchaseInfo]
    FOREIGN KEY ([ManagerId])
    REFERENCES [dbo].[Managers]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_ManagerPurchaseInfo'
CREATE INDEX [IX_FK_ManagerPurchaseInfo]
ON [dbo].[PurchasesInfo]
    ([ManagerId]);
GO

-- Creating foreign key on [ClientId] in table 'PurchasesInfo'
ALTER TABLE [dbo].[PurchasesInfo]
ADD CONSTRAINT [FK_ClientPurchaseInfo]
    FOREIGN KEY ([ClientId])
    REFERENCES [dbo].[Clients]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_ClientPurchaseInfo'
CREATE INDEX [IX_FK_ClientPurchaseInfo]
ON [dbo].[PurchasesInfo]
    ([ClientId]);
GO

-- Creating foreign key on [ProductId] in table 'PurchasesInfo'
ALTER TABLE [dbo].[PurchasesInfo]
ADD CONSTRAINT [FK_ProductPurchaseInfo]
    FOREIGN KEY ([ProductId])
    REFERENCES [dbo].[Products]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_ProductPurchaseInfo'
CREATE INDEX [IX_FK_ProductPurchaseInfo]
ON [dbo].[PurchasesInfo]
    ([ProductId]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------