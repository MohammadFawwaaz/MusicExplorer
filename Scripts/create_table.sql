USE [MusicExplorer]
GO

CREATE TABLE [Artist]
(
	[Id] [bigint] IDENTITY(1,1) NOT NULL PRIMARY KEY,
	[Name] [nvarchar](255) NOT NULL,
	[UniqueIdentifier] [uniqueidentifier] NOT NULL,
	[Country] [nvarchar](5) NOT NULL,
	[Aliases] [nvarchar](max) NULL
)
GO
