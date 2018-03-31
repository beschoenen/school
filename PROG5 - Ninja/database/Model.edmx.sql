
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 10/14/2016 22:25:40
-- Generated from EDMX file: D:\Development\School\prog5-ninja\database\Model.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [prog5-ninja];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[FK_ninja_wearing_equipment_equipment]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[ninja_wearing_equipment] DROP CONSTRAINT [FK_ninja_wearing_equipment_equipment];
GO
IF OBJECT_ID(N'[dbo].[FK_ninja_wearing_equipment_ninja]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[ninja_wearing_equipment] DROP CONSTRAINT [FK_ninja_wearing_equipment_ninja];
GO
IF OBJECT_ID(N'[dbo].[FK_categoryequipment]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[equipments] DROP CONSTRAINT [FK_categoryequipment];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[categories]', 'U') IS NOT NULL
    DROP TABLE [dbo].[categories];
GO
IF OBJECT_ID(N'[dbo].[equipments]', 'U') IS NOT NULL
    DROP TABLE [dbo].[equipments];
GO
IF OBJECT_ID(N'[dbo].[ninjas]', 'U') IS NOT NULL
    DROP TABLE [dbo].[ninjas];
GO
IF OBJECT_ID(N'[dbo].[ninja_wearing_equipment]', 'U') IS NOT NULL
    DROP TABLE [dbo].[ninja_wearing_equipment];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'categories'
CREATE TABLE [dbo].[categories] (
    [name] varchar(255)  NOT NULL
);
GO

-- Creating table 'equipments'
CREATE TABLE [dbo].[equipments] (
    [id] int IDENTITY(1,1) NOT NULL,
    [name] varchar(255)  NOT NULL,
    [image] varchar(max)  NOT NULL,
    [value] int  NOT NULL,
    [strength] int  NOT NULL,
    [intelligence] int  NOT NULL,
    [agility] int  NOT NULL,
    [category_name] varchar(255)  NOT NULL
);
GO

-- Creating table 'ninjas'
CREATE TABLE [dbo].[ninjas] (
    [id] int IDENTITY(1,1) NOT NULL,
    [name] varchar(255)  NOT NULL
);
GO

-- Creating table 'ninja_wearing_equipment'
CREATE TABLE [dbo].[ninja_wearing_equipment] (
    [equipment_id] int  NOT NULL,
    [ninjas_id] int  NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [name] in table 'categories'
ALTER TABLE [dbo].[categories]
ADD CONSTRAINT [PK_categories]
    PRIMARY KEY CLUSTERED ([name] ASC);
GO

-- Creating primary key on [id] in table 'equipments'
ALTER TABLE [dbo].[equipments]
ADD CONSTRAINT [PK_equipments]
    PRIMARY KEY CLUSTERED ([id] ASC);
GO

-- Creating primary key on [id] in table 'ninjas'
ALTER TABLE [dbo].[ninjas]
ADD CONSTRAINT [PK_ninjas]
    PRIMARY KEY CLUSTERED ([id] ASC);
GO

-- Creating primary key on [equipment_id], [ninjas_id] in table 'ninja_wearing_equipment'
ALTER TABLE [dbo].[ninja_wearing_equipment]
ADD CONSTRAINT [PK_ninja_wearing_equipment]
    PRIMARY KEY CLUSTERED ([equipment_id], [ninjas_id] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [equipment_id] in table 'ninja_wearing_equipment'
ALTER TABLE [dbo].[ninja_wearing_equipment]
ADD CONSTRAINT [FK_ninja_wearing_equipment_equipment]
    FOREIGN KEY ([equipment_id])
    REFERENCES [dbo].[equipments]
        ([id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating foreign key on [ninjas_id] in table 'ninja_wearing_equipment'
ALTER TABLE [dbo].[ninja_wearing_equipment]
ADD CONSTRAINT [FK_ninja_wearing_equipment_ninja]
    FOREIGN KEY ([ninjas_id])
    REFERENCES [dbo].[ninjas]
        ([id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_ninja_wearing_equipment_ninja'
CREATE INDEX [IX_FK_ninja_wearing_equipment_ninja]
ON [dbo].[ninja_wearing_equipment]
    ([ninjas_id]);
GO

-- Creating foreign key on [category_name] in table 'equipments'
ALTER TABLE [dbo].[equipments]
ADD CONSTRAINT [FK_categoryequipment]
    FOREIGN KEY ([category_name])
    REFERENCES [dbo].[categories]
        ([name])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_categoryequipment'
CREATE INDEX [IX_FK_categoryequipment]
ON [dbo].[equipments]
    ([category_name]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------