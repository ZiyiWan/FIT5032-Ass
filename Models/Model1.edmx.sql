
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 10/29/2020 18:27:07
-- Generated from EDMX file: C:\Users\57638\source\repos\FIT5032-Ass\Models\Model1.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [aspnet-FIT5032-Ass-20201029055420];
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

-- Creating table 'StudentSet'
CREATE TABLE [dbo].[StudentSet] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [FirstName] nvarchar(max)  NOT NULL,
    [LastName] nvarchar(max)  NOT NULL,
    [PhoneNumber] nvarchar(max)  NOT NULL,
    [Address] nvarchar(max)  NOT NULL,
    [Lat] nvarchar(max)  NOT NULL,
    [Lng] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'HomeVisitSet'
CREATE TABLE [dbo].[HomeVisitSet] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [TeacherEmail] nvarchar(max)  NOT NULL,
    [Date] datetime  NOT NULL,
    [Subject] nvarchar(max)  NOT NULL,
    [StudentId] int  NOT NULL,
    [TeacherId] int  NOT NULL
);
GO

-- Creating table 'TeacherSet'
CREATE TABLE [dbo].[TeacherSet] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [FirstName] nvarchar(max)  NOT NULL,
    [LastName] nvarchar(max)  NOT NULL,
    [PhoneNumber] nvarchar(max)  NOT NULL,
    [Email] nvarchar(max)  NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [Id] in table 'StudentSet'
ALTER TABLE [dbo].[StudentSet]
ADD CONSTRAINT [PK_StudentSet]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'HomeVisitSet'
ALTER TABLE [dbo].[HomeVisitSet]
ADD CONSTRAINT [PK_HomeVisitSet]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'TeacherSet'
ALTER TABLE [dbo].[TeacherSet]
ADD CONSTRAINT [PK_TeacherSet]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [StudentId] in table 'HomeVisitSet'
ALTER TABLE [dbo].[HomeVisitSet]
ADD CONSTRAINT [FK_StudentHomeVisit]
    FOREIGN KEY ([StudentId])
    REFERENCES [dbo].[StudentSet]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_StudentHomeVisit'
CREATE INDEX [IX_FK_StudentHomeVisit]
ON [dbo].[HomeVisitSet]
    ([StudentId]);
GO

-- Creating foreign key on [TeacherId] in table 'HomeVisitSet'
ALTER TABLE [dbo].[HomeVisitSet]
ADD CONSTRAINT [FK_TeacherHomeVisit]
    FOREIGN KEY ([TeacherId])
    REFERENCES [dbo].[TeacherSet]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_TeacherHomeVisit'
CREATE INDEX [IX_FK_TeacherHomeVisit]
ON [dbo].[HomeVisitSet]
    ([TeacherId]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------