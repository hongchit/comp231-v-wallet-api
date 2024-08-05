USE [v-wallet-db]
GO
ALTER TABLE [dbo].[tbl_UserProfile] DROP CONSTRAINT [FK_tbl_UserProfile_tbl_UserAccount]
GO
ALTER TABLE [dbo].[tbl_FinancialTransaction] DROP CONSTRAINT [FK_tbl_FinancialTransaction_tbl_FinancialAccount]
GO
ALTER TABLE [dbo].[tbl_FinancialTransaction] DROP CONSTRAINT [FK_tbl_FinancialTransaction_tbl_Category]
GO
ALTER TABLE [dbo].[tbl_FinancialAccount] DROP CONSTRAINT [FK_tbl_FinancialAccount_tbl_UserProfile]
GO
ALTER TABLE [dbo].[tbl_FinancialAccount] DROP CONSTRAINT [FK_tbl_FinancialAccount_tbl_FinancialAccountType]
GO
ALTER TABLE [dbo].[tbl_FinancialAccount] DROP CONSTRAINT [FK_tbl_FinancialAccount_tbl_Currency]
GO
ALTER TABLE [dbo].[tbl_UserProfile] DROP CONSTRAINT [DF_tbl_UserProfile_Id]
GO
ALTER TABLE [dbo].[tbl_UserAccount] DROP CONSTRAINT [DF_tbl_UserAccount_Id]
GO
ALTER TABLE [dbo].[tbl_FinancialTransaction] DROP CONSTRAINT [DF_tbl_FinancialTransaction_Id]
GO
ALTER TABLE [dbo].[tbl_FinancialAccountType] DROP CONSTRAINT [DF_tbl_FinancialAccountType_Id]
GO
ALTER TABLE [dbo].[tbl_FinancialAccount] DROP CONSTRAINT [DF_tbl_FinancialAccount_Id]
GO
ALTER TABLE [dbo].[tbl_Currency] DROP CONSTRAINT [DF_tbl_Currency_Id]
GO
ALTER TABLE [dbo].[tbl_Category] DROP CONSTRAINT [DF_tbl_Category_Id]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tbl_UserProfile]') AND type in (N'U'))
DROP TABLE [dbo].[tbl_UserProfile]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tbl_UserAccount]') AND type in (N'U'))
DROP TABLE [dbo].[tbl_UserAccount]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tbl_FinancialTransaction]') AND type in (N'U'))
DROP TABLE [dbo].[tbl_FinancialTransaction]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tbl_FinancialAccountType]') AND type in (N'U'))
DROP TABLE [dbo].[tbl_FinancialAccountType]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tbl_FinancialAccount]') AND type in (N'U'))
DROP TABLE [dbo].[tbl_FinancialAccount]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tbl_Currency]') AND type in (N'U'))
DROP TABLE [dbo].[tbl_Currency]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tbl_Category]') AND type in (N'U'))
DROP TABLE [dbo].[tbl_Category]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tbl_Category](
	[Id] [uniqueidentifier] ROWGUIDCOL  NOT NULL,
	[Name] [nvarchar](50) NULL,
	[Description] [nvarchar](100) NULL,
 CONSTRAINT [PK_tbl_Category] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tbl_Currency](
	[Id] [uniqueidentifier] ROWGUIDCOL  NOT NULL,
	[Symbol] [nvarchar](20) NULL,
	[Country] [nvarchar](50) NULL,
	[IsActive] [bit] NULL,
 CONSTRAINT [PK_tbl_Currency] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tbl_FinancialAccount](
	[Id] [uniqueidentifier] ROWGUIDCOL  NOT NULL,
	[AccountName] [nvarchar](50) NULL,
	[AccountNumber] [nvarchar](50) NULL,
	[InitialValue] [decimal](18, 0) NULL,
	[CurrentValue] [decimal](18, 0) NULL,
	[AccountTypeId] [uniqueidentifier] NULL,
	[CurrencyId] [uniqueidentifier] NULL,
	[UserProfileId] [uniqueidentifier] NULL,
 CONSTRAINT [PK_tbl_FinancialAccount] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tbl_FinancialAccountType](
	[Id] [uniqueidentifier] ROWGUIDCOL  NOT NULL,
	[Name] [nvarchar](20) NULL,
	[Description] [nvarchar](100) NULL,
 CONSTRAINT [PK_tbl_FinancialAccountType] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tbl_FinancialTransaction](
	[Id] [uniqueidentifier] ROWGUIDCOL  NOT NULL,
	[Amount] [decimal](18, 0) NULL,
	[Description] [nvarchar](50) NULL,
	[TransactionType] [nvarchar](20) NULL,
	[AccountId] [uniqueidentifier] NULL,
	[CategoryId] [uniqueidentifier] NULL,
	[TransactionDate] [datetime2](7) NULL,
 CONSTRAINT [PK_tbl_FinancialTransaction] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
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
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
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
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[tbl_Category] ADD  CONSTRAINT [DF_tbl_Category_Id]  DEFAULT (newid()) FOR [Id]
GO
ALTER TABLE [dbo].[tbl_Currency] ADD  CONSTRAINT [DF_tbl_Currency_Id]  DEFAULT (newid()) FOR [Id]
GO
ALTER TABLE [dbo].[tbl_FinancialAccount] ADD  CONSTRAINT [DF_tbl_FinancialAccount_Id]  DEFAULT (newid()) FOR [Id]
GO
ALTER TABLE [dbo].[tbl_FinancialAccountType] ADD  CONSTRAINT [DF_tbl_FinancialAccountType_Id]  DEFAULT (newid()) FOR [Id]
GO
ALTER TABLE [dbo].[tbl_FinancialTransaction] ADD  CONSTRAINT [DF_tbl_FinancialTransaction_Id]  DEFAULT (newid()) FOR [Id]
GO
ALTER TABLE [dbo].[tbl_UserAccount] ADD  CONSTRAINT [DF_tbl_UserAccount_Id]  DEFAULT (newid()) FOR [Id]
GO
ALTER TABLE [dbo].[tbl_UserProfile] ADD  CONSTRAINT [DF_tbl_UserProfile_Id]  DEFAULT (newid()) FOR [Id]
GO
ALTER TABLE [dbo].[tbl_FinancialAccount]  WITH CHECK ADD  CONSTRAINT [FK_tbl_FinancialAccount_tbl_Currency] FOREIGN KEY([CurrencyId])
REFERENCES [dbo].[tbl_Currency] ([Id])
GO
ALTER TABLE [dbo].[tbl_FinancialAccount] CHECK CONSTRAINT [FK_tbl_FinancialAccount_tbl_Currency]
GO
ALTER TABLE [dbo].[tbl_FinancialAccount]  WITH CHECK ADD  CONSTRAINT [FK_tbl_FinancialAccount_tbl_FinancialAccountType] FOREIGN KEY([AccountTypeId])
REFERENCES [dbo].[tbl_FinancialAccountType] ([Id])
GO
ALTER TABLE [dbo].[tbl_FinancialAccount] CHECK CONSTRAINT [FK_tbl_FinancialAccount_tbl_FinancialAccountType]
GO
ALTER TABLE [dbo].[tbl_FinancialAccount]  WITH CHECK ADD  CONSTRAINT [FK_tbl_FinancialAccount_tbl_UserProfile] FOREIGN KEY([UserProfileId])
REFERENCES [dbo].[tbl_UserProfile] ([Id])
GO
ALTER TABLE [dbo].[tbl_FinancialAccount] CHECK CONSTRAINT [FK_tbl_FinancialAccount_tbl_UserProfile]
GO
ALTER TABLE [dbo].[tbl_FinancialTransaction]  WITH CHECK ADD  CONSTRAINT [FK_tbl_FinancialTransaction_tbl_Category] FOREIGN KEY([CategoryId])
REFERENCES [dbo].[tbl_Category] ([Id])
GO
ALTER TABLE [dbo].[tbl_FinancialTransaction] CHECK CONSTRAINT [FK_tbl_FinancialTransaction_tbl_Category]
GO
ALTER TABLE [dbo].[tbl_FinancialTransaction]  WITH CHECK ADD  CONSTRAINT [FK_tbl_FinancialTransaction_tbl_FinancialAccount] FOREIGN KEY([AccountId])
REFERENCES [dbo].[tbl_FinancialAccount] ([Id])
GO
ALTER TABLE [dbo].[tbl_FinancialTransaction] CHECK CONSTRAINT [FK_tbl_FinancialTransaction_tbl_FinancialAccount]
GO
ALTER TABLE [dbo].[tbl_UserProfile]  WITH CHECK ADD  CONSTRAINT [FK_tbl_UserProfile_tbl_UserAccount] FOREIGN KEY([UserAccountId])
REFERENCES [dbo].[tbl_UserAccount] ([Id])
GO
ALTER TABLE [dbo].[tbl_UserProfile] CHECK CONSTRAINT [FK_tbl_UserProfile_tbl_UserAccount]
GO
