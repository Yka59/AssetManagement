CREATE TABLE [dbo].[Location]
(
	[Id] INT NOT NULL PRIMARY KEY, 
    [Code] NVARCHAR(100) NULL, 
    [Name] NVARCHAR(200) NULL, 
    [Address] NVARCHAR(500) NULL
)

CREATE TABLE [dbo].[Asset]
(
	[Id] INT NOT NULL PRIMARY KEY, 
    [Code] NVARCHAR(100) NULL, 
    [Type] NVARCHAR(200) NULL, 
    [Model] NVARCHAR(500) NULL
)

CREATE TABLE [dbo].[AssetLocations]
(
	[Id] INT IDENTITY (1,1) NOT NULL, 
    [LocationId] INT NULL, 
    [AssetId] INT NULL, 
    [LastSeen] DATETIME NULL, 
    PRIMARY KEY CLUSTERED ([Id] ASC), 
	 CONSTRAINT [FK_AssetLocations_Location] FOREIGN KEY ([LocationId]) REFERENCES [AssetLocations]([Id]),
    CONSTRAINT [FK_AssetLocations_Assets] FOREIGN KEY ([AssetId]) REFERENCES [Asset]([Id]) 
);