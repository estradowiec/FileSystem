USE [FileSystem]
GO
/****** Object:  Table [dbo].[FileUpload]    Script Date: 03/03/2014 13:49:26 ******/
SET IDENTITY_INSERT [dbo].[FileUpload] ON
INSERT [dbo].[FileUpload] ([FileID], [FileNames], [FileSize], [DateAttach], [Permission], [RepositoryID], [FolderID]) VALUES (1, N'NewFile.jpg', CAST(1022012 AS Decimal(10, 0)), CAST(0x0000A2E300D487D0 AS DateTime), 1, 14, 1)
INSERT [dbo].[FileUpload] ([FileID], [FileNames], [FileSize], [DateAttach], [Permission], [RepositoryID], [FolderID]) VALUES (2, N'NewFile.jpg', CAST(1022012 AS Decimal(10, 0)), CAST(0x0000A2E300D487D0 AS DateTime), 1, 16, 2)
INSERT [dbo].[FileUpload] ([FileID], [FileNames], [FileSize], [DateAttach], [Permission], [RepositoryID], [FolderID]) VALUES (3, N'NewFile.jpg', CAST(1022012 AS Decimal(10, 0)), CAST(0x0000A2E300D487D0 AS DateTime), 1, 18, 3)
INSERT [dbo].[FileUpload] ([FileID], [FileNames], [FileSize], [DateAttach], [Permission], [RepositoryID], [FolderID]) VALUES (4, N'Mozart - Hector.mp3', CAST(4305174 AS Decimal(10, 0)), CAST(0x0000A2E300DE6660 AS DateTime), 3, 23, 19)
INSERT [dbo].[FileUpload] ([FileID], [FileNames], [FileSize], [DateAttach], [Permission], [RepositoryID], [FolderID]) VALUES (5, N'Mozart - Piano Sonata.mp3', CAST(4016587 AS Decimal(10, 0)), CAST(0x0000A2E300DE678C AS DateTime), 3, 23, 19)
INSERT [dbo].[FileUpload] ([FileID], [FileNames], [FileSize], [DateAttach], [Permission], [RepositoryID], [FolderID]) VALUES (6, N'tracklist.txt', CAST(10075 AS Decimal(10, 0)), CAST(0x0000A2E300DED104 AS DateTime), 3, 23, 11)
INSERT [dbo].[FileUpload] ([FileID], [FileNames], [FileSize], [DateAttach], [Permission], [RepositoryID], [FolderID]) VALUES (7, N'description.txt', CAST(10075 AS Decimal(10, 0)), CAST(0x0000A2E300DF0F20 AS DateTime), 3, 23, NULL)
INSERT [dbo].[FileUpload] ([FileID], [FileNames], [FileSize], [DateAttach], [Permission], [RepositoryID], [FolderID]) VALUES (8, N'Global.asax', CAST(116 AS Decimal(10, 0)), CAST(0x0000A2E300E02860 AS DateTime), 2, 26, 20)
INSERT [dbo].[FileUpload] ([FileID], [FileNames], [FileSize], [DateAttach], [Permission], [RepositoryID], [FolderID]) VALUES (9, N'Global.asax.cs', CAST(1065 AS Decimal(10, 0)), CAST(0x0000A2E300E02860 AS DateTime), 2, 26, 20)
INSERT [dbo].[FileUpload] ([FileID], [FileNames], [FileSize], [DateAttach], [Permission], [RepositoryID], [FolderID]) VALUES (10, N'packages.config', CAST(501 AS Decimal(10, 0)), CAST(0x0000A2E300E02860 AS DateTime), 2, 26, 20)
INSERT [dbo].[FileUpload] ([FileID], [FileNames], [FileSize], [DateAttach], [Permission], [RepositoryID], [FolderID]) VALUES (11, N'Web.config', CAST(540 AS Decimal(10, 0)), CAST(0x0000A2E300E02860 AS DateTime), 2, 26, 20)
INSERT [dbo].[FileUpload] ([FileID], [FileNames], [FileSize], [DateAttach], [Permission], [RepositoryID], [FolderID]) VALUES (12, N'flash-integration.js', CAST(3708 AS Decimal(10, 0)), CAST(0x0000A2E300E040FC AS DateTime), 3, 26, 21)
INSERT [dbo].[FileUpload] ([FileID], [FileNames], [FileSize], [DateAttach], [Permission], [RepositoryID], [FolderID]) VALUES (13, N'jquery.swfobject.1-1-1.js', CAST(5231 AS Decimal(10, 0)), CAST(0x0000A2E300E040FC AS DateTime), 3, 26, 21)
INSERT [dbo].[FileUpload] ([FileID], [FileNames], [FileSize], [DateAttach], [Permission], [RepositoryID], [FolderID]) VALUES (14, N'jquery-1.10.2.js', CAST(282988 AS Decimal(10, 0)), CAST(0x0000A2E300E040FC AS DateTime), 3, 26, 21)
INSERT [dbo].[FileUpload] ([FileID], [FileNames], [FileSize], [DateAttach], [Permission], [RepositoryID], [FolderID]) VALUES (15, N'swfobject.js', CAST(26337 AS Decimal(10, 0)), CAST(0x0000A2E300E040FC AS DateTime), 3, 26, 21)
INSERT [dbo].[FileUpload] ([FileID], [FileNames], [FileSize], [DateAttach], [Permission], [RepositoryID], [FolderID]) VALUES (16, N'Web.config', CAST(2873 AS Decimal(10, 0)), CAST(0x0000A2E300E07DEC AS DateTime), 3, 26, 22)
INSERT [dbo].[FileUpload] ([FileID], [FileNames], [FileSize], [DateAttach], [Permission], [RepositoryID], [FolderID]) VALUES (17, N'_Layout.cshtml', CAST(525 AS Decimal(10, 0)), CAST(0x0000A2E300E08D28 AS DateTime), 3, 26, 25)
INSERT [dbo].[FileUpload] ([FileID], [FileNames], [FileSize], [DateAttach], [Permission], [RepositoryID], [FolderID]) VALUES (18, N'RunGame.cshtml', CAST(280 AS Decimal(10, 0)), CAST(0x0000A2E300E08D28 AS DateTime), 3, 26, 25)
INSERT [dbo].[FileUpload] ([FileID], [FileNames], [FileSize], [DateAttach], [Permission], [RepositoryID], [FolderID]) VALUES (19, N'GamesController.cs', CAST(2203 AS Decimal(10, 0)), CAST(0x0000A2E300E0A81C AS DateTime), 3, 26, 23)
INSERT [dbo].[FileUpload] ([FileID], [FileNames], [FileSize], [DateAttach], [Permission], [RepositoryID], [FolderID]) VALUES (20, N'AssemblyInfo.cs', CAST(1465 AS Decimal(10, 0)), CAST(0x0000A2E300E0BE60 AS DateTime), 3, 26, 24)
SET IDENTITY_INSERT [dbo].[FileUpload] OFF
/****** Object:  Table [dbo].[Repository]    Script Date: 03/03/2014 13:49:26 ******/
SET IDENTITY_INSERT [dbo].[Repository] ON
INSERT [dbo].[Repository] ([RepositoryID], [RepositoryName], [DateAttach], [IsActive]) VALUES (23, N'Microsoft', CAST(0x0000A2E300D531BC AS DateTime), 0)
INSERT [dbo].[Repository] ([RepositoryID], [RepositoryName], [DateAttach], [IsActive]) VALUES (24, N'ATSI S.A.', CAST(0x0000A2E300D86E40 AS DateTime), 0)
INSERT [dbo].[Repository] ([RepositoryID], [RepositoryName], [DateAttach], [IsActive]) VALUES (25, N'Apple', CAST(0x0000A2E300D8A7AC AS DateTime), 0)
INSERT [dbo].[Repository] ([RepositoryID], [RepositoryName], [DateAttach], [IsActive]) VALUES (26, N'Samsung', CAST(0x0000A2E300D90FF8 AS DateTime), 0)
INSERT [dbo].[Repository] ([RepositoryID], [RepositoryName], [DateAttach], [IsActive]) VALUES (27, N'Panasonic', CAST(0x0000A2E300D94E14 AS DateTime), 0)
INSERT [dbo].[Repository] ([RepositoryID], [RepositoryName], [DateAttach], [IsActive]) VALUES (28, N'LG Electronics', CAST(0x0000A2E300D99914 AS DateTime), 0)
INSERT [dbo].[Repository] ([RepositoryID], [RepositoryName], [DateAttach], [IsActive]) VALUES (29, N'Intel', CAST(0x0000A2E300DB3A44 AS DateTime), 0)
INSERT [dbo].[Repository] ([RepositoryID], [RepositoryName], [DateAttach], [IsActive]) VALUES (30, N'ATI Technologies', CAST(0x0000A2E300DBB7A8 AS DateTime), 0)
SET IDENTITY_INSERT [dbo].[Repository] OFF
/****** Object:  Table [dbo].[Person]    Script Date: 03/03/2014 13:49:26 ******/
SET IDENTITY_INSERT [dbo].[Person] ON
INSERT [dbo].[Person] ([PersonID], [PersonName], [Password], [Email], [Permission], [DateAttach], [RepositoryID]) VALUES (1, N'Admin', N'c21f969b5f03d33d43e04f8f136e7682', N'admin@hotmail.com', 0, CAST(0x0000A2E300D2E328 AS DateTime), NULL)
INSERT [dbo].[Person] ([PersonID], [PersonName], [Password], [Email], [Permission], [DateAttach], [RepositoryID]) VALUES (4, N'Bill Gates', N'c807c5b902f5a94488df0d3ea7c4ba40', N'microsoft@hotmail.com', 1, CAST(0x0000A2E300D531BC AS DateTime), 23)
INSERT [dbo].[Person] ([PersonID], [PersonName], [Password], [Email], [Permission], [DateAttach], [RepositoryID]) VALUES (5, N'Tom Clancy', N'c807c5b902f5a94488df0d3ea7c4ba40', N'clancy@gmail.com', 3, CAST(0x0000A2E300D58298 AS DateTime), 23)
INSERT [dbo].[Person] ([PersonID], [PersonName], [Password], [Email], [Permission], [DateAttach], [RepositoryID]) VALUES (6, N'Bob Builder', N'c807c5b902f5a94488df0d3ea7c4ba40', N'builder@gmail.com', 2, CAST(0x0000A2E300D5CFF0 AS DateTime), 23)
INSERT [dbo].[Person] ([PersonID], [PersonName], [Password], [Email], [Permission], [DateAttach], [RepositoryID]) VALUES (7, N'Rob Halford', N'c807c5b902f5a94488df0d3ea7c4ba40', N'halford@gmail.com', 3, CAST(0x0000A2E300D612BC AS DateTime), 23)
INSERT [dbo].[Person] ([PersonID], [PersonName], [Password], [Email], [Permission], [DateAttach], [RepositoryID]) VALUES (8, N'Albert Einstein', N'c807c5b902f5a94488df0d3ea7c4ba40', N'einstein@gmail.com', 3, CAST(0x0000A2E300D64C28 AS DateTime), 23)
INSERT [dbo].[Person] ([PersonID], [PersonName], [Password], [Email], [Permission], [DateAttach], [RepositoryID]) VALUES (9, N'Gregory Smith', N'c807c5b902f5a94488df0d3ea7c4ba40', N'smith@gmail.com', 4, CAST(0x0000A2E300D66E24 AS DateTime), 23)
INSERT [dbo].[Person] ([PersonID], [PersonName], [Password], [Email], [Permission], [DateAttach], [RepositoryID]) VALUES (10, N'Isaac Newton', N'c807c5b902f5a94488df0d3ea7c4ba40', N'newton@gmail.com', 3, CAST(0x0000A2E300D6B348 AS DateTime), 23)
INSERT [dbo].[Person] ([PersonID], [PersonName], [Password], [Email], [Permission], [DateAttach], [RepositoryID]) VALUES (11, N'Thomas Edison', N'c807c5b902f5a94488df0d3ea7c4ba40', N'edison@gmail.com', 2, CAST(0x0000A2E300D6F164 AS DateTime), 23)
INSERT [dbo].[Person] ([PersonID], [PersonName], [Password], [Email], [Permission], [DateAttach], [RepositoryID]) VALUES (12, N'Stephen King', N'c807c5b902f5a94488df0d3ea7c4ba40', N'king@gmail.com', 3, CAST(0x0000A2E300D7193C AS DateTime), 23)
INSERT [dbo].[Person] ([PersonID], [PersonName], [Password], [Email], [Permission], [DateAttach], [RepositoryID]) VALUES (13, N'Barack Obama', N'c807c5b902f5a94488df0d3ea7c4ba40', N'obama@gmail.com', 3, CAST(0x0000A2E300D74CCC AS DateTime), 23)
INSERT [dbo].[Person] ([PersonID], [PersonName], [Password], [Email], [Permission], [DateAttach], [RepositoryID]) VALUES (14, N'David Beckham', N'c807c5b902f5a94488df0d3ea7c4ba40', N'beckham@gmail.com', 2, CAST(0x0000A2E300D796A0 AS DateTime), 23)
INSERT [dbo].[Person] ([PersonID], [PersonName], [Password], [Email], [Permission], [DateAttach], [RepositoryID]) VALUES (15, N'Cristiano Ronaldo', N'c807c5b902f5a94488df0d3ea7c4ba40', N'ronaldo@gmail.com', 3, CAST(0x0000A2E300D7BAF4 AS DateTime), 23)
INSERT [dbo].[Person] ([PersonID], [PersonName], [Password], [Email], [Permission], [DateAttach], [RepositoryID]) VALUES (16, N'Piotr Nowak', N'c807c5b902f5a94488df0d3ea7c4ba40', N'atsi@gmail.com', 1, CAST(0x0000A2E300D86E40 AS DateTime), 24)
INSERT [dbo].[Person] ([PersonID], [PersonName], [Password], [Email], [Permission], [DateAttach], [RepositoryID]) VALUES (17, N'Tim Cook', N'c807c5b902f5a94488df0d3ea7c4ba40', N'apple@gmail.com', 1, CAST(0x0000A2E300D8A7AC AS DateTime), 25)
INSERT [dbo].[Person] ([PersonID], [PersonName], [Password], [Email], [Permission], [DateAttach], [RepositoryID]) VALUES (18, N'Oh-Hyun Kwon', N'c807c5b902f5a94488df0d3ea7c4ba40', N'samsung@gmail.com', 1, CAST(0x0000A2E300D90FF8 AS DateTime), 26)
INSERT [dbo].[Person] ([PersonID], [PersonName], [Password], [Email], [Permission], [DateAttach], [RepositoryID]) VALUES (19, N'Kazuhiro Tsuga', N'c807c5b902f5a94488df0d3ea7c4ba40', N'panasonic@gmail.com', 1, CAST(0x0000A2E300D94E14 AS DateTime), 27)
INSERT [dbo].[Person] ([PersonID], [PersonName], [Password], [Email], [Permission], [DateAttach], [RepositoryID]) VALUES (20, N'Bon-Joon Koo', N'c807c5b902f5a94488df0d3ea7c4ba40', N'lg@gmail.com', 1, CAST(0x0000A2E300D99914 AS DateTime), 28)
INSERT [dbo].[Person] ([PersonID], [PersonName], [Password], [Email], [Permission], [DateAttach], [RepositoryID]) VALUES (21, N'Brian Krzanich', N'c807c5b902f5a94488df0d3ea7c4ba40', N'intel@gmail.com', 1, CAST(0x0000A2E300DB3A44 AS DateTime), 29)
INSERT [dbo].[Person] ([PersonID], [PersonName], [Password], [Email], [Permission], [DateAttach], [RepositoryID]) VALUES (22, N'Adrian Hartog', N'c807c5b902f5a94488df0d3ea7c4ba40', N'amd@gmail.com', 1, CAST(0x0000A2E300DBB7A8 AS DateTime), 30)
SET IDENTITY_INSERT [dbo].[Person] OFF
/****** Object:  Table [dbo].[Partnership]    Script Date: 03/03/2014 13:49:26 ******/
SET IDENTITY_INSERT [dbo].[Partnership] ON
INSERT [dbo].[Partnership] ([PartnershipID], [RelatingFromRepositoryID], [RelatingToRepositoryID], [IsAccept]) VALUES (9, 23, 24, 1)
INSERT [dbo].[Partnership] ([PartnershipID], [RelatingFromRepositoryID], [RelatingToRepositoryID], [IsAccept]) VALUES (10, 24, 23, 0)
INSERT [dbo].[Partnership] ([PartnershipID], [RelatingFromRepositoryID], [RelatingToRepositoryID], [IsAccept]) VALUES (11, 23, 25, 1)
INSERT [dbo].[Partnership] ([PartnershipID], [RelatingFromRepositoryID], [RelatingToRepositoryID], [IsAccept]) VALUES (12, 25, 23, 1)
INSERT [dbo].[Partnership] ([PartnershipID], [RelatingFromRepositoryID], [RelatingToRepositoryID], [IsAccept]) VALUES (13, 26, 23, 1)
INSERT [dbo].[Partnership] ([PartnershipID], [RelatingFromRepositoryID], [RelatingToRepositoryID], [IsAccept]) VALUES (14, 23, 26, 1)
INSERT [dbo].[Partnership] ([PartnershipID], [RelatingFromRepositoryID], [RelatingToRepositoryID], [IsAccept]) VALUES (15, 28, 23, 1)
INSERT [dbo].[Partnership] ([PartnershipID], [RelatingFromRepositoryID], [RelatingToRepositoryID], [IsAccept]) VALUES (16, 23, 28, 1)
INSERT [dbo].[Partnership] ([PartnershipID], [RelatingFromRepositoryID], [RelatingToRepositoryID], [IsAccept]) VALUES (17, 29, 23, 1)
INSERT [dbo].[Partnership] ([PartnershipID], [RelatingFromRepositoryID], [RelatingToRepositoryID], [IsAccept]) VALUES (18, 23, 29, 1)
INSERT [dbo].[Partnership] ([PartnershipID], [RelatingFromRepositoryID], [RelatingToRepositoryID], [IsAccept]) VALUES (19, 30, 23, 1)
INSERT [dbo].[Partnership] ([PartnershipID], [RelatingFromRepositoryID], [RelatingToRepositoryID], [IsAccept]) VALUES (20, 23, 30, 0)
SET IDENTITY_INSERT [dbo].[Partnership] OFF
/****** Object:  Table [dbo].[Folder]    Script Date: 03/03/2014 13:49:26 ******/
SET IDENTITY_INSERT [dbo].[Folder] ON
INSERT [dbo].[Folder] ([FolderID], [FolderName], [DateAttach], [Permission], [RepositoryID], [ParrentID]) VALUES (10, N'Documents', CAST(0x0000A2E300DCA7E4 AS DateTime), 3, 23, NULL)
INSERT [dbo].[Folder] ([FolderID], [FolderName], [DateAttach], [Permission], [RepositoryID], [ParrentID]) VALUES (11, N'Music', CAST(0x0000A2E300DCB39C AS DateTime), 3, 23, NULL)
INSERT [dbo].[Folder] ([FolderID], [FolderName], [DateAttach], [Permission], [RepositoryID], [ParrentID]) VALUES (12, N'Pictures', CAST(0x0000A2E300DCC080 AS DateTime), 3, 23, NULL)
INSERT [dbo].[Folder] ([FolderID], [FolderName], [DateAttach], [Permission], [RepositoryID], [ParrentID]) VALUES (13, N'Subversion', CAST(0x0000A2E300DCCFBC AS DateTime), 2, 23, NULL)
INSERT [dbo].[Folder] ([FolderID], [FolderName], [DateAttach], [Permission], [RepositoryID], [ParrentID]) VALUES (14, N'Videos', CAST(0x0000A2E300DCD91C AS DateTime), 3, 23, NULL)
INSERT [dbo].[Folder] ([FolderID], [FolderName], [DateAttach], [Permission], [RepositoryID], [ParrentID]) VALUES (15, N'GitHub', CAST(0x0000A2E300DCED08 AS DateTime), 3, 23, 10)
INSERT [dbo].[Folder] ([FolderID], [FolderName], [DateAttach], [Permission], [RepositoryID], [ParrentID]) VALUES (16, N'IISExpress', CAST(0x0000A2E300DCFB18 AS DateTime), 3, 23, 10)
INSERT [dbo].[Folder] ([FolderID], [FolderName], [DateAttach], [Permission], [RepositoryID], [ParrentID]) VALUES (17, N'Visual Studio 2012', CAST(0x0000A2E300DD13B4 AS DateTime), 3, 23, 10)
INSERT [dbo].[Folder] ([FolderID], [FolderName], [DateAttach], [Permission], [RepositoryID], [ParrentID]) VALUES (18, N'Trance', CAST(0x0000A2E300DD73CC AS DateTime), 3, 23, 11)
INSERT [dbo].[Folder] ([FolderID], [FolderName], [DateAttach], [Permission], [RepositoryID], [ParrentID]) VALUES (19, N'Symphony', CAST(0x0000A2E300DD8EC0 AS DateTime), 3, 23, 11)
INSERT [dbo].[Folder] ([FolderID], [FolderName], [DateAttach], [Permission], [RepositoryID], [ParrentID]) VALUES (20, N'ProjectToMicrosoft', CAST(0x0000A2E300DFC71C AS DateTime), 3, 26, NULL)
INSERT [dbo].[Folder] ([FolderID], [FolderName], [DateAttach], [Permission], [RepositoryID], [ParrentID]) VALUES (21, N'Scripts', CAST(0x0000A2E300DFDE8C AS DateTime), 3, 26, 20)
INSERT [dbo].[Folder] ([FolderID], [FolderName], [DateAttach], [Permission], [RepositoryID], [ParrentID]) VALUES (22, N'Views', CAST(0x0000A2E300DFEA44 AS DateTime), 3, 26, 20)
INSERT [dbo].[Folder] ([FolderID], [FolderName], [DateAttach], [Permission], [RepositoryID], [ParrentID]) VALUES (23, N'Controllers', CAST(0x0000A2E300DFFBD8 AS DateTime), 3, 26, 20)
INSERT [dbo].[Folder] ([FolderID], [FolderName], [DateAttach], [Permission], [RepositoryID], [ParrentID]) VALUES (24, N'Properties', CAST(0x0000A2E300E00B14 AS DateTime), 3, 26, 20)
INSERT [dbo].[Folder] ([FolderID], [FolderName], [DateAttach], [Permission], [RepositoryID], [ParrentID]) VALUES (25, N'Games', CAST(0x0000A2E300E062F8 AS DateTime), 3, 26, 22)
SET IDENTITY_INSERT [dbo].[Folder] OFF
/****** Object:  Table [dbo].[Files]    Script Date: 03/03/2014 13:49:26 ******/
SET IDENTITY_INSERT [dbo].[Files] ON
INSERT [dbo].[Files] ([FileID], [FileNames], [FileSize], [DateAttach], [Permission], [RepositoryID], [FolderID]) VALUES (12, N'Mozart - Hector.mp3', CAST(4305174 AS Decimal(10, 0)), CAST(0x0000A2E300DE678C AS DateTime), 3, 23, 19)
INSERT [dbo].[Files] ([FileID], [FileNames], [FileSize], [DateAttach], [Permission], [RepositoryID], [FolderID]) VALUES (13, N'Mozart - Piano Sonata.mp3', CAST(4016587 AS Decimal(10, 0)), CAST(0x0000A2E300DE678C AS DateTime), 3, 23, 19)
INSERT [dbo].[Files] ([FileID], [FileNames], [FileSize], [DateAttach], [Permission], [RepositoryID], [FolderID]) VALUES (14, N'tracklist.txt', CAST(10075 AS Decimal(10, 0)), CAST(0x0000A2E300DED104 AS DateTime), 3, 23, 11)
INSERT [dbo].[Files] ([FileID], [FileNames], [FileSize], [DateAttach], [Permission], [RepositoryID], [FolderID]) VALUES (15, N'description.txt', CAST(10075 AS Decimal(10, 0)), CAST(0x0000A2E300DF0F20 AS DateTime), 3, 23, NULL)
INSERT [dbo].[Files] ([FileID], [FileNames], [FileSize], [DateAttach], [Permission], [RepositoryID], [FolderID]) VALUES (16, N'Global.asax', CAST(116 AS Decimal(10, 0)), CAST(0x0000A2E300E02860 AS DateTime), 2, 26, 20)
INSERT [dbo].[Files] ([FileID], [FileNames], [FileSize], [DateAttach], [Permission], [RepositoryID], [FolderID]) VALUES (17, N'Global.asax.cs', CAST(1065 AS Decimal(10, 0)), CAST(0x0000A2E300E02860 AS DateTime), 2, 26, 20)
INSERT [dbo].[Files] ([FileID], [FileNames], [FileSize], [DateAttach], [Permission], [RepositoryID], [FolderID]) VALUES (18, N'packages.config', CAST(501 AS Decimal(10, 0)), CAST(0x0000A2E300E02860 AS DateTime), 2, 26, 20)
INSERT [dbo].[Files] ([FileID], [FileNames], [FileSize], [DateAttach], [Permission], [RepositoryID], [FolderID]) VALUES (19, N'Web.config', CAST(540 AS Decimal(10, 0)), CAST(0x0000A2E300E02860 AS DateTime), 2, 26, 20)
INSERT [dbo].[Files] ([FileID], [FileNames], [FileSize], [DateAttach], [Permission], [RepositoryID], [FolderID]) VALUES (20, N'flash-integration.js', CAST(3708 AS Decimal(10, 0)), CAST(0x0000A2E300E040FC AS DateTime), 3, 26, 21)
INSERT [dbo].[Files] ([FileID], [FileNames], [FileSize], [DateAttach], [Permission], [RepositoryID], [FolderID]) VALUES (21, N'jquery.swfobject.1-1-1.js', CAST(5231 AS Decimal(10, 0)), CAST(0x0000A2E300E040FC AS DateTime), 3, 26, 21)
INSERT [dbo].[Files] ([FileID], [FileNames], [FileSize], [DateAttach], [Permission], [RepositoryID], [FolderID]) VALUES (22, N'jquery-1.10.2.js', CAST(282988 AS Decimal(10, 0)), CAST(0x0000A2E300E040FC AS DateTime), 3, 26, 21)
INSERT [dbo].[Files] ([FileID], [FileNames], [FileSize], [DateAttach], [Permission], [RepositoryID], [FolderID]) VALUES (23, N'swfobject.js', CAST(26337 AS Decimal(10, 0)), CAST(0x0000A2E300E040FC AS DateTime), 3, 26, 21)
INSERT [dbo].[Files] ([FileID], [FileNames], [FileSize], [DateAttach], [Permission], [RepositoryID], [FolderID]) VALUES (24, N'Web.config', CAST(2873 AS Decimal(10, 0)), CAST(0x0000A2E300E07DEC AS DateTime), 3, 26, 22)
INSERT [dbo].[Files] ([FileID], [FileNames], [FileSize], [DateAttach], [Permission], [RepositoryID], [FolderID]) VALUES (25, N'_Layout.cshtml', CAST(525 AS Decimal(10, 0)), CAST(0x0000A2E300E08D28 AS DateTime), 3, 26, 25)
INSERT [dbo].[Files] ([FileID], [FileNames], [FileSize], [DateAttach], [Permission], [RepositoryID], [FolderID]) VALUES (26, N'RunGame.cshtml', CAST(280 AS Decimal(10, 0)), CAST(0x0000A2E300E08D28 AS DateTime), 3, 26, 25)
INSERT [dbo].[Files] ([FileID], [FileNames], [FileSize], [DateAttach], [Permission], [RepositoryID], [FolderID]) VALUES (27, N'GamesController.cs', CAST(2203 AS Decimal(10, 0)), CAST(0x0000A2E300E0A948 AS DateTime), 3, 26, 23)
INSERT [dbo].[Files] ([FileID], [FileNames], [FileSize], [DateAttach], [Permission], [RepositoryID], [FolderID]) VALUES (28, N'AssemblyInfo.cs', CAST(1465 AS Decimal(10, 0)), CAST(0x0000A2E300E0BE60 AS DateTime), 3, 26, 24)
SET IDENTITY_INSERT [dbo].[Files] OFF
/****** Object:  Table [dbo].[SharedFolder]    Script Date: 03/03/2014 13:49:26 ******/
SET IDENTITY_INSERT [dbo].[SharedFolder] ON
INSERT [dbo].[SharedFolder] ([SharedFolderID], [FolderID], [RepositoryID]) VALUES (1, 18, 26)
INSERT [dbo].[SharedFolder] ([SharedFolderID], [FolderID], [RepositoryID]) VALUES (2, 19, 26)
INSERT [dbo].[SharedFolder] ([SharedFolderID], [FolderID], [RepositoryID]) VALUES (3, 11, 26)
INSERT [dbo].[SharedFolder] ([SharedFolderID], [FolderID], [RepositoryID]) VALUES (4, 21, 23)
INSERT [dbo].[SharedFolder] ([SharedFolderID], [FolderID], [RepositoryID]) VALUES (5, 25, 23)
INSERT [dbo].[SharedFolder] ([SharedFolderID], [FolderID], [RepositoryID]) VALUES (6, 22, 23)
INSERT [dbo].[SharedFolder] ([SharedFolderID], [FolderID], [RepositoryID]) VALUES (7, 23, 23)
INSERT [dbo].[SharedFolder] ([SharedFolderID], [FolderID], [RepositoryID]) VALUES (8, 24, 23)
INSERT [dbo].[SharedFolder] ([SharedFolderID], [FolderID], [RepositoryID]) VALUES (9, 20, 23)
SET IDENTITY_INSERT [dbo].[SharedFolder] OFF
/****** Object:  Table [dbo].[SharedFile]    Script Date: 03/03/2014 13:49:26 ******/
SET IDENTITY_INSERT [dbo].[SharedFile] ON
INSERT [dbo].[SharedFile] ([SharedFileID], [FileID], [RepositoryID]) VALUES (2, 14, 26)
INSERT [dbo].[SharedFile] ([SharedFileID], [FileID], [RepositoryID]) VALUES (3, 12, 26)
INSERT [dbo].[SharedFile] ([SharedFileID], [FileID], [RepositoryID]) VALUES (4, 13, 26)
INSERT [dbo].[SharedFile] ([SharedFileID], [FileID], [RepositoryID]) VALUES (5, 16, 23)
INSERT [dbo].[SharedFile] ([SharedFileID], [FileID], [RepositoryID]) VALUES (6, 17, 23)
INSERT [dbo].[SharedFile] ([SharedFileID], [FileID], [RepositoryID]) VALUES (7, 18, 23)
INSERT [dbo].[SharedFile] ([SharedFileID], [FileID], [RepositoryID]) VALUES (8, 19, 23)
INSERT [dbo].[SharedFile] ([SharedFileID], [FileID], [RepositoryID]) VALUES (9, 20, 23)
INSERT [dbo].[SharedFile] ([SharedFileID], [FileID], [RepositoryID]) VALUES (10, 21, 23)
INSERT [dbo].[SharedFile] ([SharedFileID], [FileID], [RepositoryID]) VALUES (11, 22, 23)
INSERT [dbo].[SharedFile] ([SharedFileID], [FileID], [RepositoryID]) VALUES (12, 23, 23)
INSERT [dbo].[SharedFile] ([SharedFileID], [FileID], [RepositoryID]) VALUES (13, 24, 23)
INSERT [dbo].[SharedFile] ([SharedFileID], [FileID], [RepositoryID]) VALUES (14, 25, 23)
INSERT [dbo].[SharedFile] ([SharedFileID], [FileID], [RepositoryID]) VALUES (15, 26, 23)
INSERT [dbo].[SharedFile] ([SharedFileID], [FileID], [RepositoryID]) VALUES (16, 27, 23)
INSERT [dbo].[SharedFile] ([SharedFileID], [FileID], [RepositoryID]) VALUES (17, 28, 23)
SET IDENTITY_INSERT [dbo].[SharedFile] OFF
