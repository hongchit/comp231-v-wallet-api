USE [v-wallet-db]
GO
ALTER TABLE [dbo].[tbl_UserProfile] DROP CONSTRAINT [FK_tbl_UserProfile_tbl_UserAccount]
GO
ALTER TABLE [dbo].[tbl_UserProfile] DROP CONSTRAINT [DF_tbl_UserProfile_Id]
GO
ALTER TABLE [dbo].[tbl_UserAccount] DROP CONSTRAINT [DF_tbl_UserAccount_Id]
GO
/****** Object:  Table [dbo].[tbl_UserProfile]    Script Date: 2024-07-18 1:06:56 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tbl_UserProfile]') AND type in (N'U'))
DROP TABLE [dbo].[tbl_UserProfile]
GO
/****** Object:  Table [dbo].[tbl_UserAccount]    Script Date: 2024-07-18 1:06:56 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tbl_UserAccount]') AND type in (N'U'))
DROP TABLE [dbo].[tbl_UserAccount]
GO
/****** Object:  Table [dbo].[tbl_UserAccount]    Script Date: 2024-07-18 1:06:56 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tbl_UserAccount](
	[Id] [uniqueidentifier] ROWGUIDCOL  NOT NULL,
	[Email] [nvarchar](50) NULL,
	[Password] [nvarchar](50) NULL,
	[AccountType] [nvarchar](50) NULL,
 CONSTRAINT [PK_tbl_UserAccount] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tbl_UserProfile]    Script Date: 2024-07-18 1:06:56 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tbl_UserProfile](
	[Id] [uniqueidentifier] ROWGUIDCOL  NOT NULL,
	[Firstname] [nvarchar](50) NULL,
	[Lastname] [nvarchar](50) NULL,
	[Birthdate] [date] NULL,
	[UserAccountId] [uniqueidentifier] NOT NULL,
 CONSTRAINT [PK_tbl_UserProfile] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
INSERT [dbo].[tbl_UserAccount] ([Id], [Email], [Password], [AccountType]) VALUES (N'11324cfd-16b3-476d-9df9-37665b2d5bc8', N'jcborlagdan@outlook.com', N'Password1', N'User')
GO
INSERT [dbo].[tbl_UserProfile] ([Id], [Firstname], [Lastname], [Birthdate], [UserAccountId]) VALUES (N'150cb5dd-7f3a-4bbb-838d-fb6940298448', N'Jc', N'Borlagdan', NULL, N'11324cfd-16b3-476d-9df9-37665b2d5bc8')
GO
ALTER TABLE [dbo].[tbl_UserAccount] ADD  CONSTRAINT [DF_tbl_UserAccount_Id]  DEFAULT (newid()) FOR [Id]
GO
ALTER TABLE [dbo].[tbl_UserProfile] ADD  CONSTRAINT [DF_tbl_UserProfile_Id]  DEFAULT (newid()) FOR [Id]
GO
ALTER TABLE [dbo].[tbl_UserProfile]  WITH CHECK ADD  CONSTRAINT [FK_tbl_UserProfile_tbl_UserAccount] FOREIGN KEY([UserAccountId])
REFERENCES [dbo].[tbl_UserAccount] ([Id])
GO
ALTER TABLE [dbo].[tbl_UserProfile] CHECK CONSTRAINT [FK_tbl_UserProfile_tbl_UserAccount]
GO
