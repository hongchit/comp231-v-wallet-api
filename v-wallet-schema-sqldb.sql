USE [v-wallet-db]
GO
ALTER TABLE [dbo].[tbl_UserProfile] DROP CONSTRAINT [FK_tbl_UserProfile_tbl_UserAccount]
GO
ALTER TABLE [dbo].[tbl_UserProfile] DROP CONSTRAINT [DF_tbl_UserProfile_Id]
GO
ALTER TABLE [dbo].[tbl_UserAccount] DROP CONSTRAINT [DF_tbl_UserAccount_Id]
GO
/****** Object:  Table [dbo].[tbl_UserProfile]    Script Date: 2024-07-18 1:14:49 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tbl_UserProfile]') AND type in (N'U'))
DROP TABLE [dbo].[tbl_UserProfile]
GO
/****** Object:  Table [dbo].[tbl_UserAccount]    Script Date: 2024-07-18 1:14:49 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tbl_UserAccount]') AND type in (N'U'))
DROP TABLE [dbo].[tbl_UserAccount]
GO
USE [master]
GO
/****** Object:  Database [v-wallet-db]    Script Date: 2024-07-18 1:14:49 PM ******/
DROP DATABASE [v-wallet-db]
GO
/****** Object:  Database [v-wallet-db]    Script Date: 2024-07-18 1:14:49 PM ******/
CREATE DATABASE [v-wallet-db]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'v-wallet-db', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.MSSQLSERVER\MSSQL\DATA\v-wallet-db.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'v-wallet-db_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.MSSQLSERVER\MSSQL\DATA\v-wallet-db_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
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
ALTER DATABASE [v-wallet-db] SET RECOVERY FULL 
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
EXEC sys.sp_db_vardecimal_storage_format N'v-wallet-db', N'ON'
GO
ALTER DATABASE [v-wallet-db] SET QUERY_STORE = ON
GO
ALTER DATABASE [v-wallet-db] SET QUERY_STORE (OPERATION_MODE = READ_WRITE, CLEANUP_POLICY = (STALE_QUERY_THRESHOLD_DAYS = 30), DATA_FLUSH_INTERVAL_SECONDS = 900, INTERVAL_LENGTH_MINUTES = 60, MAX_STORAGE_SIZE_MB = 1000, QUERY_CAPTURE_MODE = AUTO, SIZE_BASED_CLEANUP_MODE = AUTO, MAX_PLANS_PER_QUERY = 200)
GO
USE [v-wallet-db]
GO
/****** Object:  Table [dbo].[tbl_UserAccount]    Script Date: 2024-07-18 1:14:50 PM ******/
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
/****** Object:  Table [dbo].[tbl_UserProfile]    Script Date: 2024-07-18 1:14:50 PM ******/
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
ALTER TABLE [dbo].[tbl_UserAccount] ADD  CONSTRAINT [DF_tbl_UserAccount_Id]  DEFAULT (newid()) FOR [Id]
GO
ALTER TABLE [dbo].[tbl_UserProfile] ADD  CONSTRAINT [DF_tbl_UserProfile_Id]  DEFAULT (newid()) FOR [Id]
GO
ALTER TABLE [dbo].[tbl_UserProfile]  WITH CHECK ADD  CONSTRAINT [FK_tbl_UserProfile_tbl_UserAccount] FOREIGN KEY([UserAccountId])
REFERENCES [dbo].[tbl_UserAccount] ([Id])
GO
ALTER TABLE [dbo].[tbl_UserProfile] CHECK CONSTRAINT [FK_tbl_UserProfile_tbl_UserAccount]
GO
USE [master]
GO
ALTER DATABASE [v-wallet-db] SET  READ_WRITE 
GO
