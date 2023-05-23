USE [MusicExplorer]
GO

SET IDENTITY_INSERT [dbo].[Artist] ON 
GO

INSERT [dbo].[Artist] ([Id], [Name], [UniqueIdentifier], [Country], [Aliases]) VALUES (1, N'Metallica ', N'65f4f0c5-ef9e-490c-aee3-909e7ae6b2ab', N'US', N'Metalica,메탈리카')
INSERT [dbo].[Artist] ([Id], [Name], [UniqueIdentifier], [Country], [Aliases]) VALUES (2, N'Lady Gaga ', N'650e7db6-b795-4eb5-a702-5ea2fc46c848', N'US', N'Lady Ga Ga,Stefani Joanne Angelina Germanotta')
INSERT [dbo].[Artist] ([Id], [Name], [UniqueIdentifier], [Country], [Aliases]) VALUES (3, N'Mumford & Sons ', N'c44e9c22-ef82-4a77-9bcd-af6c958446d6', N'GB', NULL)
INSERT [dbo].[Artist] ([Id], [Name], [UniqueIdentifier], [Country], [Aliases]) VALUES (4, N'Mott the Hoople ', N'435f1441-0f43-479d-92db-a506449a686b', N'GB', N'Mott The Hoppie,Mott The Hopple')
INSERT [dbo].[Artist] ([Id], [Name], [UniqueIdentifier], [Country], [Aliases]) VALUES (5, N'Megadeth', N'a9044915-8be3-4c7e-b11f-9e2d2ea0a91e', N'US', N'Megadeath ')
INSERT [dbo].[Artist] ([Id], [Name], [UniqueIdentifier], [Country], [Aliases]) VALUES (6, N'John Coltrane ', N'b625448e-bf4a-41c3-a421-72ad46cdb831', N'US', N'John Coltraine,John William Coltrane')
INSERT [dbo].[Artist] ([Id], [Name], [UniqueIdentifier], [Country], [Aliases]) VALUES (7, N'Mogwai ', N'd700b3f5-45af-4d02-95ed-57d301bda93e', N'GB', N'Mogwa ')
INSERT [dbo].[Artist] ([Id], [Name], [UniqueIdentifier], [Country], [Aliases]) VALUES (8, N'John Mayer ', N'144ef525-85e9-40c3-8335-02c32d0861f3', N'US', NULL)
INSERT [dbo].[Artist] ([Id], [Name], [UniqueIdentifier], [Country], [Aliases]) VALUES (9, N'Johnny Cash', N'18fa2fd5-3ef2-4496-ba9f-6dae655b2a4f', N'US', N'Johhny Cash,Jonny Cash')
INSERT [dbo].[Artist] ([Id], [Name], [UniqueIdentifier], [Country], [Aliases]) VALUES (10, N'Jack Johnson ', N'6456a893-c1e9-4e3d-86f7-0008b0a3ac8a', N'US', N'Jack Hody Johnson ')
INSERT [dbo].[Artist] ([Id], [Name], [UniqueIdentifier], [Country], [Aliases]) VALUES (11, N'John Frusciante ', N'f1571db1-c672-4a54-a2cf-aaa329f26f0b', N'US', N'John Anthony Frusciante ')
INSERT [dbo].[Artist] ([Id], [Name], [UniqueIdentifier], [Country], [Aliases]) VALUES (12, N'Elton John ', N'b83bc61f-8451-4a5d-8b8e-7e9ed295e822', N'GB', N'E. John, Elthon John,Elton Jphn,John Elton, Reginald Kenneth Dwight')
INSERT [dbo].[Artist] ([Id], [Name], [UniqueIdentifier], [Country], [Aliases]) VALUES (13, N'Rancid ', N'24f8d8a5-269b-475c-a1cb-792990b0b2ee', N'US', N'ランシド ')
INSERT [dbo].[Artist] ([Id], [Name], [UniqueIdentifier], [Country], [Aliases]) VALUES (14, N'Transplants ', N'29f3e1bf-aec1-4d0a-9ef3-0cb95e8a3699', N'US', N'The Transplants ')
INSERT [dbo].[Artist] ([Id], [Name], [UniqueIdentifier], [Country], [Aliases]) VALUES (15, N'Operation Ivy ', N'931e1d1f-6b2f-4ff8-9f70-aa537210cd46', N'US', N'Op Ivy ')

SET IDENTITY_INSERT [dbo].[Artist] OFF
GO
