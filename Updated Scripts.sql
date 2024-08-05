USE [master]
GO
/****** Object:  Database [v-wallet-db]    Script Date: 05/08/2024 8:10:44 am ******/
CREATE DATABASE [v-wallet-db]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'v-wallet-db', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.SQLEXPRESS\MSSQL\DATA\v-wallet-db.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'v-wallet-db_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.SQLEXPRESS\MSSQL\DATA\v-wallet-db_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT, LEDGER = OFF
GO
ALTER DATABASE [v-wallet-db] SET COMPATIBILITY_LEVEL = 160
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [v-wallet-db].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [v-wallet-db] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [v-wallet-db] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [v-wallet-db] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [v-wallet-db] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [v-wallet-db] SET ARITHABORT OFF 
GO
ALTER DATABASE [v-wallet-db] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [v-wallet-db] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [v-wallet-db] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [v-wallet-db] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [v-wallet-db] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [v-wallet-db] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [v-wallet-db] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [v-wallet-db] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [v-wallet-db] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [v-wallet-db] SET  DISABLE_BROKER 
GO
ALTER DATABASE [v-wallet-db] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [v-wallet-db] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [v-wallet-db] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [v-wallet-db] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [v-wallet-db] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [v-wallet-db] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [v-wallet-db] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [v-wallet-db] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [v-wallet-db] SET  MULTI_USER 
GO
ALTER DATABASE [v-wallet-db] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [v-wallet-db] SET DB_CHAINING OFF 
GO
ALTER DATABASE [v-wallet-db] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [v-wallet-db] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [v-wallet-db] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [v-wallet-db] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
ALTER DATABASE [v-wallet-db] SET QUERY_STORE = ON
GO
ALTER DATABASE [v-wallet-db] SET QUERY_STORE (OPERATION_MODE = READ_WRITE, CLEANUP_POLICY = (STALE_QUERY_THRESHOLD_DAYS = 30), DATA_FLUSH_INTERVAL_SECONDS = 900, INTERVAL_LENGTH_MINUTES = 60, MAX_STORAGE_SIZE_MB = 1000, QUERY_CAPTURE_MODE = AUTO, SIZE_BASED_CLEANUP_MODE = AUTO, MAX_PLANS_PER_QUERY = 200, WAIT_STATS_CAPTURE_MODE = ON)
GO
USE [v-wallet-db]
GO
/****** Object:  Table [dbo].[tbl_Category]    Script Date: 05/08/2024 8:10:44 am ******/
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
/****** Object:  Table [dbo].[tbl_Currency]    Script Date: 05/08/2024 8:10:44 am ******/
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
/****** Object:  Table [dbo].[tbl_FinancialAccount]    Script Date: 05/08/2024 8:10:44 am ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tbl_FinancialAccount](
	[Id] [uniqueidentifier] ROWGUIDCOL  NOT NULL,
	[AccountName] [nvarchar](50) NULL,
	[AccountNumber] [nvarchar](50) NULL,
	[InitialValue] [decimal](18, 0) NULL,
	[AccountType] [nvarchar](50) NULL,
	[Currency] [nvarchar](50) NULL,
	[UserProfileId] [uniqueidentifier] NULL,
	[currentValue] [decimal](18, 2) NULL,
 CONSTRAINT [PK_tbl_FinancialAccount] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tbl_FinancialAccountType]    Script Date: 05/08/2024 8:10:44 am ******/
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
/****** Object:  Table [dbo].[tbl_FinancialTransaction]    Script Date: 05/08/2024 8:10:44 am ******/
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
/****** Object:  Table [dbo].[tbl_UserAccount]    Script Date: 05/08/2024 8:10:44 am ******/
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
/****** Object:  Table [dbo].[tbl_UserProfile]    Script Date: 05/08/2024 8:10:44 am ******/
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
INSERT [dbo].[tbl_Category] ([Id], [Name], [Description]) VALUES (N'6513dcff-b3d5-453e-9427-4cf2cf57cbd9', N'Shopping', NULL)
GO
INSERT [dbo].[tbl_Category] ([Id], [Name], [Description]) VALUES (N'02fdf1d2-6878-4199-ae4c-77288ffddb6d', N'Food', NULL)
GO
INSERT [dbo].[tbl_Category] ([Id], [Name], [Description]) VALUES (N'b32d115b-23a4-4276-bc3d-7c105bd05f97', N'Housing', NULL)
GO
INSERT [dbo].[tbl_Category] ([Id], [Name], [Description]) VALUES (N'70916aa6-5f82-4b15-9dbd-99c56e55d342', N'Income', NULL)
GO
INSERT [dbo].[tbl_Category] ([Id], [Name], [Description]) VALUES (N'ef0bf1e3-01cc-457a-9bcc-ba8c1f6df307', N'Transportation', NULL)
GO
INSERT [dbo].[tbl_FinancialAccount] ([Id], [AccountName], [AccountNumber], [InitialValue], [AccountType], [Currency], [UserProfileId], [currentValue]) VALUES (N'001cb153-f3d6-4303-8c9b-540041eff69d', N'James Bond', N'229916692', CAST(500 AS Decimal(18, 0)), N'Saving', N'PKR', N'3fa85f64-5717-4562-b3fc-2c963f66afa6', CAST(500.00 AS Decimal(18, 2)))
GO
INSERT [dbo].[tbl_FinancialAccountType] ([Id], [Name], [Description]) VALUES (N'62115192-d968-49cb-ba3c-78ce4d2b142d', N'Loan', NULL)
GO
INSERT [dbo].[tbl_FinancialAccountType] ([Id], [Name], [Description]) VALUES (N'3ea0097b-5c67-4322-b6ae-7d4dd9e436bd', N'Savings', NULL)
GO
INSERT [dbo].[tbl_FinancialAccountType] ([Id], [Name], [Description]) VALUES (N'03edd126-968f-4fb0-bb05-abbfa566e76d', N'General', NULL)
GO
INSERT [dbo].[tbl_FinancialAccountType] ([Id], [Name], [Description]) VALUES (N'ea3b2b04-54b4-4ddd-9ab7-e2d0d6f346ca', N'Chequing', NULL)
GO
INSERT [dbo].[tbl_FinancialAccountType] ([Id], [Name], [Description]) VALUES (N'4fb16ee4-a08b-4d4d-b113-f14c03711cae', N'Cash', NULL)
GO
INSERT [dbo].[tbl_UserAccount] ([Id], [Email], [Password], [AccountType]) VALUES (N'c7f91643-089a-4a5e-15c1-08dcb42f0fa0', N'string', N'12345678', N'User')
GO
INSERT [dbo].[tbl_UserAccount] ([Id], [Email], [Password], [AccountType]) VALUES (N'11324cfd-16b3-476d-9df9-37665b2d5bc8', N'jcborlagdan@outlook.com', N'Password1', N'User')
GO
INSERT [dbo].[tbl_UserAccount] ([Id], [Email], [Password], [AccountType]) VALUES (N'4ecf8d30-bc7b-4477-85c3-4e492d88740c', N'v-wallet@admin.com', N'5x1&3MRi\t?&', N'Admin')
GO
INSERT [dbo].[tbl_UserProfile] ([Id], [Firstname], [Lastname], [Birthdate], [UserAccountId]) VALUES (N'773ee32a-eeed-4f9b-7811-08dcb42f0fc8', N'string', N'string', CAST(N'2024-08-04' AS Date), N'c7f91643-089a-4a5e-15c1-08dcb42f0fa0')
GO
INSERT [dbo].[tbl_UserProfile] ([Id], [Firstname], [Lastname], [Birthdate], [UserAccountId]) VALUES (N'833673d4-9c7f-4dfb-b062-87f4532b925c', N'System', N'Admin', NULL, N'4ecf8d30-bc7b-4477-85c3-4e492d88740c')
GO
INSERT [dbo].[tbl_UserProfile] ([Id], [Firstname], [Lastname], [Birthdate], [UserAccountId]) VALUES (N'150cb5dd-7f3a-4bbb-838d-fb6940298448', N'Jc', N'Borlagdan', NULL, N'11324cfd-16b3-476d-9df9-37665b2d5bc8')
GO
USE [master]
GO
ALTER DATABASE [v-wallet-db] SET  READ_WRITE 
GO
