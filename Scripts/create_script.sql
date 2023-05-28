CREATE DATABASE [MusicExplorer]
GO

USE [MusicExplorer]
GO

CREATE TABLE [dbo].[Artist](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](255) NOT NULL,
	[UniqueIdentifier] [uniqueidentifier] NOT NULL,
	[Country] [nvarchar](5) NOT NULL,
	[Aliases] [nvarchar](max) NULL,
 CONSTRAINT [PK_Artist] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
