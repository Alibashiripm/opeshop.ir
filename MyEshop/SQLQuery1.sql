
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 02/11/2022 17:27:50
-- Generated from EDMX file: E:\Programer\MyEshop\DataLayer\EshopModel.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [MyEshop_DB];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[FK_OrderDetails_Orders]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[OrderDetails] DROP CONSTRAINT [FK_OrderDetails_Orders];
GO
IF OBJECT_ID(N'[dbo].[FK_OrderDetails_Products]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[OrderDetails] DROP CONSTRAINT [FK_OrderDetails_Products];
GO
IF OBJECT_ID(N'[dbo].[FK_Orders_Users]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Orders] DROP CONSTRAINT [FK_Orders_Users];
GO
IF OBJECT_ID(N'[dbo].[FK_Prodct_Selected_Groups_Product_Groups]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Prodct_Selected_Groups] DROP CONSTRAINT [FK_Prodct_Selected_Groups_Product_Groups];
GO
IF OBJECT_ID(N'[dbo].[FK_Prodct_Selected_Groups_Products]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Prodct_Selected_Groups] DROP CONSTRAINT [FK_Prodct_Selected_Groups_Products];
GO
IF OBJECT_ID(N'[dbo].[FK_Product_Comments_Product_Comments]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Product_Comments] DROP CONSTRAINT [FK_Product_Comments_Product_Comments];
GO
IF OBJECT_ID(N'[dbo].[FK_Product_Comments_Products]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Product_Comments] DROP CONSTRAINT [FK_Product_Comments_Products];
GO
IF OBJECT_ID(N'[dbo].[FK_Product_Features_Features]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Product_Features] DROP CONSTRAINT [FK_Product_Features_Features];
GO
IF OBJECT_ID(N'[dbo].[FK_Product_Features_Products]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Product_Features] DROP CONSTRAINT [FK_Product_Features_Products];
GO
IF OBJECT_ID(N'[dbo].[FK_Product_Galleries_Products]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Product_Galleries] DROP CONSTRAINT [FK_Product_Galleries_Products];
GO
IF OBJECT_ID(N'[dbo].[FK_Product_Groups_Product_Groups]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Product_Groups] DROP CONSTRAINT [FK_Product_Groups_Product_Groups];
GO
IF OBJECT_ID(N'[dbo].[FK_Product_Tags_Products]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Product_Tags] DROP CONSTRAINT [FK_Product_Tags_Products];
GO
IF OBJECT_ID(N'[dbo].[FK_Users_Roles]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Users] DROP CONSTRAINT [FK_Users_Roles];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[Features]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Features];
GO
IF OBJECT_ID(N'[dbo].[OrderDetails]', 'U') IS NOT NULL
    DROP TABLE [dbo].[OrderDetails];
GO
IF OBJECT_ID(N'[dbo].[Orders]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Orders];
GO
IF OBJECT_ID(N'[dbo].[Prodct_Selected_Groups]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Prodct_Selected_Groups];
GO
IF OBJECT_ID(N'[dbo].[Product_Comments]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Product_Comments];
GO
IF OBJECT_ID(N'[dbo].[Product_Features]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Product_Features];
GO
IF OBJECT_ID(N'[dbo].[Product_Galleries]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Product_Galleries];
GO
IF OBJECT_ID(N'[dbo].[Product_Groups]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Product_Groups];
GO
IF OBJECT_ID(N'[dbo].[Product_Tags]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Product_Tags];
GO
IF OBJECT_ID(N'[dbo].[Products]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Products];
GO
IF OBJECT_ID(N'[dbo].[Roles]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Roles];
GO
IF OBJECT_ID(N'[dbo].[SiteVisit]', 'U') IS NOT NULL
    DROP TABLE [dbo].[SiteVisit];
GO
IF OBJECT_ID(N'[dbo].[Slider]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Slider];
GO
IF OBJECT_ID(N'[dbo].[Users]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Users];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'Roles'
CREATE TABLE [dbo].[Roles] (
    [RoleID] int  NOT NULL,
    [RoleTitle] nvarchar(250)  NOT NULL,
    [RoleName] varchar(150)  NOT NULL
);
GO

-- Creating table 'Users'
CREATE TABLE [dbo].[Users] (
    [UserID] int IDENTITY(1,1) NOT NULL,
    [RoleID] int  NOT NULL,
    [UserName] nvarchar(250)  NOT NULL,
    [Email] nvarchar(250)  NOT NULL,
    [Password] varchar(200)  NOT NULL,
    [ActiveCode] varchar(50)  NOT NULL,
    [IsActive] bit  NOT NULL,
    [RegisterDate] datetime  NOT NULL
);
GO

-- Creating table 'Product_Groups'
CREATE TABLE [dbo].[Product_Groups] (
    [GroupID] int IDENTITY(1,1) NOT NULL,
    [GroupTitle] nvarchar(400)  NOT NULL,
    [ParentID] int  NULL
);
GO

-- Creating table 'Prodct_Selected_Groups'
CREATE TABLE [dbo].[Prodct_Selected_Groups] (
    [PG_ID] int IDENTITY(1,1) NOT NULL,
    [ProductID] int  NOT NULL,
    [GroupID] int  NOT NULL
);
GO

-- Creating table 'Product_Galleries'
CREATE TABLE [dbo].[Product_Galleries] (
    [GalleryID] int IDENTITY(1,1) NOT NULL,
    [ProductID] int  NOT NULL,
    [ImageName] varchar(50)  NOT NULL,
    [Title] nvarchar(250)  NOT NULL
);
GO

-- Creating table 'Product_Tags'
CREATE TABLE [dbo].[Product_Tags] (
    [TagID] int IDENTITY(1,1) NOT NULL,
    [ProductID] int  NOT NULL,
    [Tag] nvarchar(250)  NOT NULL
);
GO

-- Creating table 'Products'
CREATE TABLE [dbo].[Products] (
    [ProductID] int IDENTITY(1,1) NOT NULL,
    [Title] nvarchar(350)  NOT NULL,
    [ShortDescription] nvarchar(500)  NOT NULL,
    [Text] nvarchar(max)  NOT NULL,
    [Price] int  NOT NULL,
    [ImageName] varchar(50)  NOT NULL,
    [CreateDate] datetime  NOT NULL
);
GO

-- Creating table 'Features'
CREATE TABLE [dbo].[Features] (
    [FeatureID] int IDENTITY(1,1) NOT NULL,
    [FeatureTitle] nvarchar(150)  NOT NULL
);
GO

-- Creating table 'Product_Features'
CREATE TABLE [dbo].[Product_Features] (
    [PF_ID] int IDENTITY(1,1) NOT NULL,
    [ProductID] int  NOT NULL,
    [FeatureID] int  NOT NULL,
    [Value] nchar(10)  NULL
);
GO

-- Creating table 'Product_Comments'
CREATE TABLE [dbo].[Product_Comments] (
    [CommentID] int IDENTITY(1,1) NOT NULL,
    [ProductID] int  NOT NULL,
    [Name] nvarchar(150)  NOT NULL,
    [Email] nvarchar(350)  NOT NULL,
    [WebSite] nvarchar(350)  NULL,
    [Comment] nvarchar(800)  NOT NULL,
    [CreateDate] datetime  NOT NULL,
    [ParentID] int  NULL
);
GO

-- Creating table 'OrderDetails'
CREATE TABLE [dbo].[OrderDetails] (
    [DetailID] int IDENTITY(1,1) NOT NULL,
    [OrderID] int  NOT NULL,
    [ProductID] int  NOT NULL,
    [Price] int  NOT NULL,
    [Count] int  NOT NULL
);
GO

-- Creating table 'Orders'
CREATE TABLE [dbo].[Orders] (
    [OrderID] int IDENTITY(1,1) NOT NULL,
    [UserID] int  NOT NULL,
    [Date] datetime  NOT NULL,
    [IsFinaly] bit  NOT NULL
);
GO

-- Creating table 'Slider'
CREATE TABLE [dbo].[Slider] (
    [SlideID] int  NOT NULL,
    [Title] nvarchar(350)  NOT NULL,
    [ImageName] varchar(50)  NOT NULL,
    [StartDate] datetime  NOT NULL,
    [EndDate] datetime  NOT NULL,
    [IsActive] bit  NOT NULL,
    [Url] nvarchar(450)  NOT NULL
);
GO

-- Creating table 'SiteVisit'
CREATE TABLE [dbo].[SiteVisit] (
    [VisitID] int IDENTITY(1,1) NOT NULL,
    [IP] nvarchar(150)  NOT NULL,
    [Date] datetime  NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [RoleID] in table 'Roles'
ALTER TABLE [dbo].[Roles]
ADD CONSTRAINT [PK_Roles]
    PRIMARY KEY CLUSTERED ([RoleID] ASC);
GO

-- Creating primary key on [UserID] in table 'Users'
ALTER TABLE [dbo].[Users]
ADD CONSTRAINT [PK_Users]
    PRIMARY KEY CLUSTERED ([UserID] ASC);
GO

-- Creating primary key on [GroupID] in table 'Product_Groups'
ALTER TABLE [dbo].[Product_Groups]
ADD CONSTRAINT [PK_Product_Groups]
    PRIMARY KEY CLUSTERED ([GroupID] ASC);
GO

-- Creating primary key on [PG_ID] in table 'Prodct_Selected_Groups'
ALTER TABLE [dbo].[Prodct_Selected_Groups]
ADD CONSTRAINT [PK_Prodct_Selected_Groups]
    PRIMARY KEY CLUSTERED ([PG_ID] ASC);
GO

-- Creating primary key on [GalleryID] in table 'Product_Galleries'
ALTER TABLE [dbo].[Product_Galleries]
ADD CONSTRAINT [PK_Product_Galleries]
    PRIMARY KEY CLUSTERED ([GalleryID] ASC);
GO

-- Creating primary key on [TagID] in table 'Product_Tags'
ALTER TABLE [dbo].[Product_Tags]
ADD CONSTRAINT [PK_Product_Tags]
    PRIMARY KEY CLUSTERED ([TagID] ASC);
GO

-- Creating primary key on [ProductID] in table 'Products'
ALTER TABLE [dbo].[Products]
ADD CONSTRAINT [PK_Products]
    PRIMARY KEY CLUSTERED ([ProductID] ASC);
GO

-- Creating primary key on [FeatureID] in table 'Features'
ALTER TABLE [dbo].[Features]
ADD CONSTRAINT [PK_Features]
    PRIMARY KEY CLUSTERED ([FeatureID] ASC);
GO

-- Creating primary key on [PF_ID] in table 'Product_Features'
ALTER TABLE [dbo].[Product_Features]
ADD CONSTRAINT [PK_Product_Features]
    PRIMARY KEY CLUSTERED ([PF_ID] ASC);
GO

-- Creating primary key on [CommentID] in table 'Product_Comments'
ALTER TABLE [dbo].[Product_Comments]
ADD CONSTRAINT [PK_Product_Comments]
    PRIMARY KEY CLUSTERED ([CommentID] ASC);
GO

-- Creating primary key on [DetailID] in table 'OrderDetails'
ALTER TABLE [dbo].[OrderDetails]
ADD CONSTRAINT [PK_OrderDetails]
    PRIMARY KEY CLUSTERED ([DetailID] ASC);
GO

-- Creating primary key on [OrderID] in table 'Orders'
ALTER TABLE [dbo].[Orders]
ADD CONSTRAINT [PK_Orders]
    PRIMARY KEY CLUSTERED ([OrderID] ASC);
GO

-- Creating primary key on [SlideID] in table 'Slider'
ALTER TABLE [dbo].[Slider]
ADD CONSTRAINT [PK_Slider]
    PRIMARY KEY CLUSTERED ([SlideID] ASC);
GO

-- Creating primary key on [VisitID] in table 'SiteVisit'
ALTER TABLE [dbo].[SiteVisit]
ADD CONSTRAINT [PK_SiteVisit]
    PRIMARY KEY CLUSTERED ([VisitID] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [RoleID] in table 'Users'
ALTER TABLE [dbo].[Users]
ADD CONSTRAINT [FK_Users_Roles]
    FOREIGN KEY ([RoleID])
    REFERENCES [dbo].[Roles]
        ([RoleID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_Users_Roles'
CREATE INDEX [IX_FK_Users_Roles]
ON [dbo].[Users]
    ([RoleID]);
GO

-- Creating foreign key on [ParentID] in table 'Product_Groups'
ALTER TABLE [dbo].[Product_Groups]
ADD CONSTRAINT [FK_Product_Groups_Product_Groups]
    FOREIGN KEY ([ParentID])
    REFERENCES [dbo].[Product_Groups]
        ([GroupID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_Product_Groups_Product_Groups'
CREATE INDEX [IX_FK_Product_Groups_Product_Groups]
ON [dbo].[Product_Groups]
    ([ParentID]);
GO

-- Creating foreign key on [GroupID] in table 'Prodct_Selected_Groups'
ALTER TABLE [dbo].[Prodct_Selected_Groups]
ADD CONSTRAINT [FK_Prodct_Selected_Groups_Product_Groups]
    FOREIGN KEY ([GroupID])
    REFERENCES [dbo].[Product_Groups]
        ([GroupID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_Prodct_Selected_Groups_Product_Groups'
CREATE INDEX [IX_FK_Prodct_Selected_Groups_Product_Groups]
ON [dbo].[Prodct_Selected_Groups]
    ([GroupID]);
GO

-- Creating foreign key on [ProductID] in table 'Prodct_Selected_Groups'
ALTER TABLE [dbo].[Prodct_Selected_Groups]
ADD CONSTRAINT [FK_Prodct_Selected_Groups_Products]
    FOREIGN KEY ([ProductID])
    REFERENCES [dbo].[Products]
        ([ProductID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_Prodct_Selected_Groups_Products'
CREATE INDEX [IX_FK_Prodct_Selected_Groups_Products]
ON [dbo].[Prodct_Selected_Groups]
    ([ProductID]);
GO

-- Creating foreign key on [ProductID] in table 'Product_Galleries'
ALTER TABLE [dbo].[Product_Galleries]
ADD CONSTRAINT [FK_Product_Galleries_Products]
    FOREIGN KEY ([ProductID])
    REFERENCES [dbo].[Products]
        ([ProductID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_Product_Galleries_Products'
CREATE INDEX [IX_FK_Product_Galleries_Products]
ON [dbo].[Product_Galleries]
    ([ProductID]);
GO

-- Creating foreign key on [ProductID] in table 'Product_Tags'
ALTER TABLE [dbo].[Product_Tags]
ADD CONSTRAINT [FK_Product_Tags_Products]
    FOREIGN KEY ([ProductID])
    REFERENCES [dbo].[Products]
        ([ProductID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_Product_Tags_Products'
CREATE INDEX [IX_FK_Product_Tags_Products]
ON [dbo].[Product_Tags]
    ([ProductID]);
GO

-- Creating foreign key on [FeatureID] in table 'Product_Features'
ALTER TABLE [dbo].[Product_Features]
ADD CONSTRAINT [FK_Product_Features_Features]
    FOREIGN KEY ([FeatureID])
    REFERENCES [dbo].[Features]
        ([FeatureID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_Product_Features_Features'
CREATE INDEX [IX_FK_Product_Features_Features]
ON [dbo].[Product_Features]
    ([FeatureID]);
GO

-- Creating foreign key on [ProductID] in table 'Product_Features'
ALTER TABLE [dbo].[Product_Features]
ADD CONSTRAINT [FK_Product_Features_Products]
    FOREIGN KEY ([ProductID])
    REFERENCES [dbo].[Products]
        ([ProductID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_Product_Features_Products'
CREATE INDEX [IX_FK_Product_Features_Products]
ON [dbo].[Product_Features]
    ([ProductID]);
GO

-- Creating foreign key on [ParentID] in table 'Product_Comments'
ALTER TABLE [dbo].[Product_Comments]
ADD CONSTRAINT [FK_Product_Comments_Product_Comments]
    FOREIGN KEY ([ParentID])
    REFERENCES [dbo].[Product_Comments]
        ([CommentID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_Product_Comments_Product_Comments'
CREATE INDEX [IX_FK_Product_Comments_Product_Comments]
ON [dbo].[Product_Comments]
    ([ParentID]);
GO

-- Creating foreign key on [ProductID] in table 'Product_Comments'
ALTER TABLE [dbo].[Product_Comments]
ADD CONSTRAINT [FK_Product_Comments_Products]
    FOREIGN KEY ([ProductID])
    REFERENCES [dbo].[Products]
        ([ProductID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_Product_Comments_Products'
CREATE INDEX [IX_FK_Product_Comments_Products]
ON [dbo].[Product_Comments]
    ([ProductID]);
GO

-- Creating foreign key on [OrderID] in table 'OrderDetails'
ALTER TABLE [dbo].[OrderDetails]
ADD CONSTRAINT [FK_OrderDetails_Orders]
    FOREIGN KEY ([OrderID])
    REFERENCES [dbo].[Orders]
        ([OrderID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_OrderDetails_Orders'
CREATE INDEX [IX_FK_OrderDetails_Orders]
ON [dbo].[OrderDetails]
    ([OrderID]);
GO

-- Creating foreign key on [ProductID] in table 'OrderDetails'
ALTER TABLE [dbo].[OrderDetails]
ADD CONSTRAINT [FK_OrderDetails_Products]
    FOREIGN KEY ([ProductID])
    REFERENCES [dbo].[Products]
        ([ProductID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_OrderDetails_Products'
CREATE INDEX [IX_FK_OrderDetails_Products]
ON [dbo].[OrderDetails]
    ([ProductID]);
GO

-- Creating foreign key on [UserID] in table 'Orders'
ALTER TABLE [dbo].[Orders]
ADD CONSTRAINT [FK_Orders_Users]
    FOREIGN KEY ([UserID])
    REFERENCES [dbo].[Users]
        ([UserID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_Orders_Users'
CREATE INDEX [IX_FK_Orders_Users]
ON [dbo].[Orders]
    ([UserID]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------