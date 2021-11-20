
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, and Azure
-- --------------------------------------------------
-- Date Created: 12/14/2012 11:25:52
-- Generated from EDMX file: C:\Users\Jeshi\Desktop\CC\CollectorzCorner\CollectorzCorner\CCEntity.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [CollectorzCorner];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[FK_Book_ItemID]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Books] DROP CONSTRAINT [FK_Book_ItemID];
GO
IF OBJECT_ID(N'[dbo].[FK_Collection_TypeID]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Collections] DROP CONSTRAINT [FK_Collection_TypeID];
GO
IF OBJECT_ID(N'[dbo].[FK_Collection_UserID]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Collections] DROP CONSTRAINT [FK_Collection_UserID];
GO
IF OBJECT_ID(N'[dbo].[FK_CollectionItem_CollectionID]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[CollectionItems] DROP CONSTRAINT [FK_CollectionItem_CollectionID];
GO
IF OBJECT_ID(N'[dbo].[FK_CollectionItem_CollectionItemStatus]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[CollectionItems] DROP CONSTRAINT [FK_CollectionItem_CollectionItemStatus];
GO
IF OBJECT_ID(N'[dbo].[FK_CollectionItem_Item]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[CollectionItems] DROP CONSTRAINT [FK_CollectionItem_Item];
GO
IF OBJECT_ID(N'[dbo].[FK_ComicBook_Item]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[ComicBooks] DROP CONSTRAINT [FK_ComicBook_Item];
GO
IF OBJECT_ID(N'[dbo].[FK_Game_ContentRatingID]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Games] DROP CONSTRAINT [FK_Game_ContentRatingID];
GO
IF OBJECT_ID(N'[dbo].[FK_Movie_ItemID]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Movies] DROP CONSTRAINT [FK_Movie_ItemID];
GO
IF OBJECT_ID(N'[dbo].[FK_Game_ItemID]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Games] DROP CONSTRAINT [FK_Game_ItemID];
GO
IF OBJECT_ID(N'[dbo].[FK_Game_Platform]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Games] DROP CONSTRAINT [FK_Game_Platform];
GO
IF OBJECT_ID(N'[dbo].[FK_Genre_ItemType]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Genres] DROP CONSTRAINT [FK_Genre_ItemType];
GO
IF OBJECT_ID(N'[dbo].[FK_Item_ItemType]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Items] DROP CONSTRAINT [FK_Item_ItemType];
GO
IF OBJECT_ID(N'[dbo].[FK_Item_PictureID]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Items] DROP CONSTRAINT [FK_Item_PictureID];
GO
IF OBJECT_ID(N'[dbo].[FK_Item_Series]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Items] DROP CONSTRAINT [FK_Item_Series];
GO
IF OBJECT_ID(N'[dbo].[FK_ItemGenre_Genre]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[ItemGenre] DROP CONSTRAINT [FK_ItemGenre_Genre];
GO
IF OBJECT_ID(N'[dbo].[FK_ItemGenre_Item]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[ItemGenre] DROP CONSTRAINT [FK_ItemGenre_Item];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[Books]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Books];
GO
IF OBJECT_ID(N'[dbo].[Collections]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Collections];
GO
IF OBJECT_ID(N'[dbo].[CollectionItems]', 'U') IS NOT NULL
    DROP TABLE [dbo].[CollectionItems];
GO
IF OBJECT_ID(N'[dbo].[CollectionItemStatus]', 'U') IS NOT NULL
    DROP TABLE [dbo].[CollectionItemStatus];
GO
IF OBJECT_ID(N'[dbo].[ComicBooks]', 'U') IS NOT NULL
    DROP TABLE [dbo].[ComicBooks];
GO
IF OBJECT_ID(N'[dbo].[ContentRatings]', 'U') IS NOT NULL
    DROP TABLE [dbo].[ContentRatings];
GO
IF OBJECT_ID(N'[dbo].[Games]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Games];
GO
IF OBJECT_ID(N'[dbo].[Genres]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Genres];
GO
IF OBJECT_ID(N'[dbo].[Items]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Items];
GO
IF OBJECT_ID(N'[dbo].[ItemTypes]', 'U') IS NOT NULL
    DROP TABLE [dbo].[ItemTypes];
GO
IF OBJECT_ID(N'[dbo].[Movies]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Movies];
GO
IF OBJECT_ID(N'[dbo].[Pictures]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Pictures];
GO
IF OBJECT_ID(N'[dbo].[Platforms]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Platforms];
GO
IF OBJECT_ID(N'[dbo].[Series]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Series];
GO
IF OBJECT_ID(N'[dbo].[Users]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Users];
GO
IF OBJECT_ID(N'[dbo].[ItemGenre]', 'U') IS NOT NULL
    DROP TABLE [dbo].[ItemGenre];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'Books'
CREATE TABLE [dbo].[Books] (
    [ItemID] int  NOT NULL,
    [Author] varchar(max)  NOT NULL,
    [Illustrator] varchar(max)  NULL,
    [Editor] varchar(max)  NULL,
    [Plot] varchar(max)  NULL,
    [NoOfPages] int  NULL,
    [ReadLevel] varchar(50)  NULL,
    [ISBN] varchar(50)  NULL,
    [IsEBook] bit  NOT NULL
);
GO

-- Creating table 'Collections'
CREATE TABLE [dbo].[Collections] (
    [ID] int IDENTITY(1,1) NOT NULL,
    [Name] varchar(50)  NOT NULL,
    [TypeID] nchar(3)  NOT NULL,
    [UserID] int  NOT NULL,
    [Description] varchar(max)  NULL,
    [IsPublic] bit  NOT NULL,
    [IsComplete] bit  NOT NULL
);
GO

-- Creating table 'CollectionItems'
CREATE TABLE [dbo].[CollectionItems] (
    [ItemID] int  NOT NULL,
    [CollectionID] int  NOT NULL,
    [StatusID] varchar(3)  NOT NULL,
    [Rating] tinyint  NULL,
    [Review] varchar(max)  NULL,
    [StatusNote] varchar(150)  NULL
);
GO

-- Creating table 'CollectionItemStatus'
CREATE TABLE [dbo].[CollectionItemStatus] (
    [ID] varchar(3)  NOT NULL,
    [Description] varchar(50)  NOT NULL
);
GO

-- Creating table 'ComicBooks'
CREATE TABLE [dbo].[ComicBooks] (
    [ItemID] int  NOT NULL,
    [Writer] varchar(max)  NOT NULL,
    [Artist] varchar(max)  NULL,
    [Colorist] varchar(max)  NULL,
    [CoverArtist] varchar(max)  NULL,
    [Editor] varchar(max)  NULL
);
GO

-- Creating table 'ContentRatings'
CREATE TABLE [dbo].[ContentRatings] (
    [ID] varchar(3)  NOT NULL,
    [Description] varchar(100)  NOT NULL,
    [Type] nchar(3)  NOT NULL
);
GO

-- Creating table 'Games'
CREATE TABLE [dbo].[Games] (
    [ItemID] int  NOT NULL,
    [Developer] varchar(max)  NULL,
    [ContentRatingID] varchar(3)  NULL,
    [PlatformID] int  NOT NULL
);
GO

-- Creating table 'Genres'
CREATE TABLE [dbo].[Genres] (
    [ID] int IDENTITY(1,1) NOT NULL,
    [Description] varchar(100)  NOT NULL,
    [TypeID] nchar(3)  NULL
);
GO

-- Creating table 'Items'
CREATE TABLE [dbo].[Items] (
    [ID] int IDENTITY(1,1) NOT NULL,
    [Title] varchar(max)  NOT NULL,
    [Description] varchar(max)  NULL,
    [PictureID] int  NULL,
    [ReleaseDate] datetime  NULL,
    [SeriesID] int  NULL,
    [TypeID] nchar(3)  NOT NULL,
    [Publisher] varchar(max)  NULL,
    [UserID] int  NOT NULL
);
GO

-- Creating table 'ItemTypes'
CREATE TABLE [dbo].[ItemTypes] (
    [ID] nchar(3)  NOT NULL,
    [Name] varchar(50)  NOT NULL
);
GO

-- Creating table 'Movies'
CREATE TABLE [dbo].[Movies] (
    [ItemID] int  NOT NULL,
    [Director] varchar(max)  NOT NULL,
    [Writer] varchar(max)  NULL,
    [Producer] varchar(max)  NULL,
    [Format] varchar(50)  NULL,
    [ContentRatingID] varchar(3)  NULL
);
GO

-- Creating table 'Pictures'
CREATE TABLE [dbo].[Pictures] (
    [ID] int IDENTITY(1,1) NOT NULL,
    [Href] varchar(50)  NULL,
    [Alt] varchar(50)  NULL,
    [pictureBytes] varbinary(max)  NOT NULL
);
GO

-- Creating table 'Platforms'
CREATE TABLE [dbo].[Platforms] (
    [ID] int IDENTITY(1,1) NOT NULL,
    [Description] varchar(200)  NOT NULL
);
GO

-- Creating table 'Series'
CREATE TABLE [dbo].[Series] (
    [ID] int IDENTITY(1,1) NOT NULL,
    [Name] varchar(max)  NOT NULL
);
GO

-- Creating table 'Users'
CREATE TABLE [dbo].[Users] (
    [ID] int IDENTITY(1,1) NOT NULL,
    [Username] varchar(20)  NOT NULL,
    [Password] varchar(max)  NOT NULL,
    [Email] varchar(100)  NOT NULL,
    [BirthDate] datetime  NULL,
    [SecurityQuestion] varchar(max)  NULL,
    [SecurityAnswer] varchar(150)  NULL,
    [CreationDate] datetime  NOT NULL,
    [LastLoginDate] datetime  NOT NULL,
    [IsApproved] bit  NOT NULL,
    [IsLockedOut] bit  NOT NULL
);
GO

-- Creating table 'ItemGenre'
CREATE TABLE [dbo].[ItemGenre] (
    [Genres_ID] int  NOT NULL,
    [Items_ID] int  NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [ItemID] in table 'Books'
ALTER TABLE [dbo].[Books]
ADD CONSTRAINT [PK_Books]
    PRIMARY KEY CLUSTERED ([ItemID] ASC);
GO

-- Creating primary key on [ID] in table 'Collections'
ALTER TABLE [dbo].[Collections]
ADD CONSTRAINT [PK_Collections]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [ItemID], [CollectionID] in table 'CollectionItems'
ALTER TABLE [dbo].[CollectionItems]
ADD CONSTRAINT [PK_CollectionItems]
    PRIMARY KEY CLUSTERED ([ItemID], [CollectionID] ASC);
GO

-- Creating primary key on [ID] in table 'CollectionItemStatus'
ALTER TABLE [dbo].[CollectionItemStatus]
ADD CONSTRAINT [PK_CollectionItemStatus]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [ItemID] in table 'ComicBooks'
ALTER TABLE [dbo].[ComicBooks]
ADD CONSTRAINT [PK_ComicBooks]
    PRIMARY KEY CLUSTERED ([ItemID] ASC);
GO

-- Creating primary key on [ID] in table 'ContentRatings'
ALTER TABLE [dbo].[ContentRatings]
ADD CONSTRAINT [PK_ContentRatings]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [ItemID] in table 'Games'
ALTER TABLE [dbo].[Games]
ADD CONSTRAINT [PK_Games]
    PRIMARY KEY CLUSTERED ([ItemID] ASC);
GO

-- Creating primary key on [ID] in table 'Genres'
ALTER TABLE [dbo].[Genres]
ADD CONSTRAINT [PK_Genres]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [ID] in table 'Items'
ALTER TABLE [dbo].[Items]
ADD CONSTRAINT [PK_Items]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [ID] in table 'ItemTypes'
ALTER TABLE [dbo].[ItemTypes]
ADD CONSTRAINT [PK_ItemTypes]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [ItemID] in table 'Movies'
ALTER TABLE [dbo].[Movies]
ADD CONSTRAINT [PK_Movies]
    PRIMARY KEY CLUSTERED ([ItemID] ASC);
GO

-- Creating primary key on [ID] in table 'Pictures'
ALTER TABLE [dbo].[Pictures]
ADD CONSTRAINT [PK_Pictures]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [ID] in table 'Platforms'
ALTER TABLE [dbo].[Platforms]
ADD CONSTRAINT [PK_Platforms]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [ID] in table 'Series'
ALTER TABLE [dbo].[Series]
ADD CONSTRAINT [PK_Series]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [ID] in table 'Users'
ALTER TABLE [dbo].[Users]
ADD CONSTRAINT [PK_Users]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [Genres_ID], [Items_ID] in table 'ItemGenre'
ALTER TABLE [dbo].[ItemGenre]
ADD CONSTRAINT [PK_ItemGenre]
    PRIMARY KEY NONCLUSTERED ([Genres_ID], [Items_ID] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [ItemID] in table 'Books'
ALTER TABLE [dbo].[Books]
ADD CONSTRAINT [FK_Book_ItemID]
    FOREIGN KEY ([ItemID])
    REFERENCES [dbo].[Items]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating foreign key on [TypeID] in table 'Collections'
ALTER TABLE [dbo].[Collections]
ADD CONSTRAINT [FK_Collection_TypeID]
    FOREIGN KEY ([TypeID])
    REFERENCES [dbo].[ItemTypes]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_Collection_TypeID'
CREATE INDEX [IX_FK_Collection_TypeID]
ON [dbo].[Collections]
    ([TypeID]);
GO

-- Creating foreign key on [UserID] in table 'Collections'
ALTER TABLE [dbo].[Collections]
ADD CONSTRAINT [FK_Collection_UserID]
    FOREIGN KEY ([UserID])
    REFERENCES [dbo].[Users]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_Collection_UserID'
CREATE INDEX [IX_FK_Collection_UserID]
ON [dbo].[Collections]
    ([UserID]);
GO

-- Creating foreign key on [CollectionID] in table 'CollectionItems'
ALTER TABLE [dbo].[CollectionItems]
ADD CONSTRAINT [FK_CollectionItem_CollectionID]
    FOREIGN KEY ([CollectionID])
    REFERENCES [dbo].[Collections]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_CollectionItem_CollectionID'
CREATE INDEX [IX_FK_CollectionItem_CollectionID]
ON [dbo].[CollectionItems]
    ([CollectionID]);
GO

-- Creating foreign key on [StatusID] in table 'CollectionItems'
ALTER TABLE [dbo].[CollectionItems]
ADD CONSTRAINT [FK_CollectionItem_CollectionItemStatus]
    FOREIGN KEY ([StatusID])
    REFERENCES [dbo].[CollectionItemStatus]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_CollectionItem_CollectionItemStatus'
CREATE INDEX [IX_FK_CollectionItem_CollectionItemStatus]
ON [dbo].[CollectionItems]
    ([StatusID]);
GO

-- Creating foreign key on [ItemID] in table 'CollectionItems'
ALTER TABLE [dbo].[CollectionItems]
ADD CONSTRAINT [FK_CollectionItem_Item]
    FOREIGN KEY ([ItemID])
    REFERENCES [dbo].[Items]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating foreign key on [ItemID] in table 'ComicBooks'
ALTER TABLE [dbo].[ComicBooks]
ADD CONSTRAINT [FK_ComicBook_Item]
    FOREIGN KEY ([ItemID])
    REFERENCES [dbo].[Items]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating foreign key on [ContentRatingID] in table 'Games'
ALTER TABLE [dbo].[Games]
ADD CONSTRAINT [FK_Game_ContentRatingID]
    FOREIGN KEY ([ContentRatingID])
    REFERENCES [dbo].[ContentRatings]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_Game_ContentRatingID'
CREATE INDEX [IX_FK_Game_ContentRatingID]
ON [dbo].[Games]
    ([ContentRatingID]);
GO

-- Creating foreign key on [ContentRatingID] in table 'Movies'
ALTER TABLE [dbo].[Movies]
ADD CONSTRAINT [FK_Movie_ItemID]
    FOREIGN KEY ([ContentRatingID])
    REFERENCES [dbo].[ContentRatings]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_Movie_ItemID'
CREATE INDEX [IX_FK_Movie_ItemID]
ON [dbo].[Movies]
    ([ContentRatingID]);
GO

-- Creating foreign key on [ItemID] in table 'Games'
ALTER TABLE [dbo].[Games]
ADD CONSTRAINT [FK_Game_ItemID]
    FOREIGN KEY ([ItemID])
    REFERENCES [dbo].[Items]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating foreign key on [PlatformID] in table 'Games'
ALTER TABLE [dbo].[Games]
ADD CONSTRAINT [FK_Game_Platform]
    FOREIGN KEY ([PlatformID])
    REFERENCES [dbo].[Platforms]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_Game_Platform'
CREATE INDEX [IX_FK_Game_Platform]
ON [dbo].[Games]
    ([PlatformID]);
GO

-- Creating foreign key on [TypeID] in table 'Genres'
ALTER TABLE [dbo].[Genres]
ADD CONSTRAINT [FK_Genre_ItemType]
    FOREIGN KEY ([TypeID])
    REFERENCES [dbo].[ItemTypes]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_Genre_ItemType'
CREATE INDEX [IX_FK_Genre_ItemType]
ON [dbo].[Genres]
    ([TypeID]);
GO

-- Creating foreign key on [TypeID] in table 'Items'
ALTER TABLE [dbo].[Items]
ADD CONSTRAINT [FK_Item_ItemType]
    FOREIGN KEY ([TypeID])
    REFERENCES [dbo].[ItemTypes]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_Item_ItemType'
CREATE INDEX [IX_FK_Item_ItemType]
ON [dbo].[Items]
    ([TypeID]);
GO

-- Creating foreign key on [PictureID] in table 'Items'
ALTER TABLE [dbo].[Items]
ADD CONSTRAINT [FK_Item_PictureID]
    FOREIGN KEY ([PictureID])
    REFERENCES [dbo].[Pictures]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_Item_PictureID'
CREATE INDEX [IX_FK_Item_PictureID]
ON [dbo].[Items]
    ([PictureID]);
GO

-- Creating foreign key on [SeriesID] in table 'Items'
ALTER TABLE [dbo].[Items]
ADD CONSTRAINT [FK_Item_Series]
    FOREIGN KEY ([SeriesID])
    REFERENCES [dbo].[Series]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_Item_Series'
CREATE INDEX [IX_FK_Item_Series]
ON [dbo].[Items]
    ([SeriesID]);
GO

-- Creating foreign key on [Genres_ID] in table 'ItemGenre'
ALTER TABLE [dbo].[ItemGenre]
ADD CONSTRAINT [FK_ItemGenre_Genre]
    FOREIGN KEY ([Genres_ID])
    REFERENCES [dbo].[Genres]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating foreign key on [Items_ID] in table 'ItemGenre'
ALTER TABLE [dbo].[ItemGenre]
ADD CONSTRAINT [FK_ItemGenre_Item]
    FOREIGN KEY ([Items_ID])
    REFERENCES [dbo].[Items]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_ItemGenre_Item'
CREATE INDEX [IX_FK_ItemGenre_Item]
ON [dbo].[ItemGenre]
    ([Items_ID]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------