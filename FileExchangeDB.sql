USE Master;
GO
IF EXISTS(select name from master.dbo.sysdatabases where name = 'FileSystem') drop database FileSystem
GO
Create database FileSystem
GO
USE [FileSystem]
/****** Object:  Table [dbo].[Files]    Script Date: 2014-03-03 00:18:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Files](
	[FileID] [int] IDENTITY(1,1) NOT NULL,
	[FileNames] [nvarchar](40) NOT NULL,
	[FileSize] [decimal](10, 0) NOT NULL,
	[DateAttach] [datetime] NOT NULL,
	[Permission] [int] NOT NULL,
	[RepositoryID] [int] NOT NULL,
	[FolderID] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[FileID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[FileUpload]    Script Date: 2014-03-03 00:18:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[FileUpload](
	[FileID] [int] IDENTITY(1,1) NOT NULL,
	[FileNames] [nvarchar](40) NOT NULL,
	[FileSize] [decimal](10, 0) NOT NULL,
	[DateAttach] [datetime] NOT NULL,
	[Permission] [int] NOT NULL,
	[RepositoryID] [int] NOT NULL,
	[FolderID] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[FileID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Folder]    Script Date: 2014-03-03 00:18:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Folder](
	[FolderID] [int] IDENTITY(1,1) NOT NULL,
	[FolderName] [nvarchar](20) NOT NULL,
	[DateAttach] [datetime] NULL,
	[Permission] [int] NOT NULL,
	[RepositoryID] [int] NOT NULL,
	[ParrentID] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[FolderID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Partnership]    Script Date: 2014-03-03 00:18:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Partnership](
	[PartnershipID] [int] IDENTITY(1,1) NOT NULL,
	[RelatingFromRepositoryID] [int] NOT NULL,
	[RelatingToRepositoryID] [int] NOT NULL,
	[IsAccept] [bit] NOT NULL
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Person]    Script Date: 2014-03-03 00:18:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Person](
	[PersonID] [int] IDENTITY(1,1) NOT NULL,
	[PersonName] [nvarchar](20) NOT NULL,
	[Password] [varchar](32) NOT NULL,
	[Email] [varchar](50) NOT NULL,
	[Permission] [int] NOT NULL,
	[DateAttach] [datetime] NULL,
	[RepositoryID] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[PersonID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
UNIQUE NONCLUSTERED 
(
	[Email] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
UNIQUE NONCLUSTERED 
(
	[PersonName] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Repository]    Script Date: 2014-03-03 00:18:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Repository](
	[RepositoryID] [int] IDENTITY(1,1) NOT NULL,
	[RepositoryName] [nvarchar](50) NOT NULL,
	[DateAttach] [datetime] NULL,
	[IsActive] [bit] NULL,
PRIMARY KEY CLUSTERED 
(
	[RepositoryID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
UNIQUE NONCLUSTERED 
(
	[RepositoryName] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[SharedFile]    Script Date: 2014-03-03 00:18:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SharedFile](
	[SharedFileID] [int] IDENTITY(1,1) NOT NULL,
	[FileID] [int] NOT NULL,
	[RepositoryID] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[SharedFileID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[SharedFolder]    Script Date: 2014-03-03 00:18:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SharedFolder](
	[SharedFolderID] [int] IDENTITY(1,1) NOT NULL,
	[FolderID] [int] NOT NULL,
	[RepositoryID] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[SharedFolderID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
ALTER TABLE [dbo].[Person] ADD  DEFAULT (getdate()) FOR [DateAttach]
GO
ALTER TABLE [dbo].[Repository] ADD  DEFAULT ((0)) FOR [IsActive]
GO
ALTER TABLE [dbo].[Files]  WITH CHECK ADD FOREIGN KEY([FolderID])
REFERENCES [dbo].[Folder] ([FolderID])
GO
ALTER TABLE [dbo].[Files]  WITH CHECK ADD FOREIGN KEY([RepositoryID])
REFERENCES [dbo].[Repository] ([RepositoryID])
GO
ALTER TABLE [dbo].[Folder]  WITH CHECK ADD FOREIGN KEY([ParrentID])
REFERENCES [dbo].[Folder] ([FolderID])
GO
ALTER TABLE [dbo].[Folder]  WITH CHECK ADD FOREIGN KEY([RepositoryID])
REFERENCES [dbo].[Repository] ([RepositoryID])
GO
ALTER TABLE [dbo].[Partnership]  WITH CHECK ADD FOREIGN KEY([RelatingFromRepositoryID])
REFERENCES [dbo].[Repository] ([RepositoryID])
GO
ALTER TABLE [dbo].[Partnership]  WITH CHECK ADD FOREIGN KEY([RelatingToRepositoryID])
REFERENCES [dbo].[Repository] ([RepositoryID])
GO
ALTER TABLE [dbo].[Person]  WITH CHECK ADD FOREIGN KEY([RepositoryID])
REFERENCES [dbo].[Repository] ([RepositoryID])
GO
ALTER TABLE [dbo].[SharedFile]  WITH CHECK ADD FOREIGN KEY([FileID])
REFERENCES [dbo].[Files] ([FileID])
GO
ALTER TABLE [dbo].[SharedFile]  WITH CHECK ADD FOREIGN KEY([RepositoryID])
REFERENCES [dbo].[Repository] ([RepositoryID])
GO
ALTER TABLE [dbo].[SharedFolder]  WITH CHECK ADD FOREIGN KEY([FolderID])
REFERENCES [dbo].[Folder] ([FolderID])
GO
ALTER TABLE [dbo].[SharedFolder]  WITH CHECK ADD FOREIGN KEY([RepositoryID])
REFERENCES [dbo].[Repository] ([RepositoryID])
GO
INSERT INTO [dbo].[Person] VALUES ('Admin', 'c21f969b5f03d33d43e04f8f136e7682', 'admin@hotmail.com', 0, default, null)
GO
