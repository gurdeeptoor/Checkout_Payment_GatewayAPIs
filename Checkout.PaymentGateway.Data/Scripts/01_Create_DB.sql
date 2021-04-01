USE [master]
GO
/****** Object:  Database [CheckoutDB]    Script Date: 01/04/2021 09:14:12 ******/
CREATE DATABASE [CheckoutDB]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'CheckoutDB', FILENAME = N'C:\Users\User\CheckoutDB.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'CheckoutDB_log', FILENAME = N'C:\Users\User\CheckoutDB_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
GO
ALTER DATABASE [CheckoutDB] SET COMPATIBILITY_LEVEL = 130
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [CheckoutDB].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [CheckoutDB] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [CheckoutDB] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [CheckoutDB] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [CheckoutDB] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [CheckoutDB] SET ARITHABORT OFF 
GO
ALTER DATABASE [CheckoutDB] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [CheckoutDB] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [CheckoutDB] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [CheckoutDB] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [CheckoutDB] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [CheckoutDB] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [CheckoutDB] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [CheckoutDB] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [CheckoutDB] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [CheckoutDB] SET  DISABLE_BROKER 
GO
ALTER DATABASE [CheckoutDB] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [CheckoutDB] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [CheckoutDB] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [CheckoutDB] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [CheckoutDB] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [CheckoutDB] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [CheckoutDB] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [CheckoutDB] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [CheckoutDB] SET  MULTI_USER 
GO
ALTER DATABASE [CheckoutDB] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [CheckoutDB] SET DB_CHAINING OFF 
GO
ALTER DATABASE [CheckoutDB] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [CheckoutDB] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [CheckoutDB] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [CheckoutDB] SET QUERY_STORE = OFF
GO
USE [CheckoutDB]
GO
ALTER DATABASE SCOPED CONFIGURATION SET LEGACY_CARDINALITY_ESTIMATION = OFF;
GO
ALTER DATABASE SCOPED CONFIGURATION SET MAXDOP = 0;
GO
ALTER DATABASE SCOPED CONFIGURATION SET PARAMETER_SNIFFING = ON;
GO
ALTER DATABASE SCOPED CONFIGURATION SET QUERY_OPTIMIZER_HOTFIXES = OFF;
GO
USE [CheckoutDB]
GO
/****** Object:  Table [dbo].[Bank]    Script Date: 01/04/2021 09:14:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Bank](
	[BankID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](200) NOT NULL,
	[IsEnabled] [bit] NOT NULL,
 CONSTRAINT [PK_Bank] PRIMARY KEY CLUSTERED 
(
	[BankID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[CardDetail]    Script Date: 01/04/2021 09:14:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CardDetail](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[CardNum] [nvarchar](19) COLLATE Latin1_General_BIN2 ENCRYPTED WITH (COLUMN_ENCRYPTION_KEY = [CEK_Auto1], ENCRYPTION_TYPE = Randomized, ALGORITHM = 'AEAD_AES_256_CBC_HMAC_SHA_256') NOT NULL,
	[ExpMonth] [int] NOT NULL,
	[ExpYear] [int] NOT NULL,
	[HolderName] [nvarchar](250) COLLATE Latin1_General_BIN2 ENCRYPTED WITH (COLUMN_ENCRYPTION_KEY = [CEK_Auto1], ENCRYPTION_TYPE = Randomized, ALGORITHM = 'AEAD_AES_256_CBC_HMAC_SHA_256') NOT NULL,
	[CVV] [nvarchar](3) COLLATE Latin1_General_BIN2 ENCRYPTED WITH (COLUMN_ENCRYPTION_KEY = [CEK_Auto1], ENCRYPTION_TYPE = Randomized, ALGORITHM = 'AEAD_AES_256_CBC_HMAC_SHA_256') NOT NULL,
	[IsEnabled] [bit] NOT NULL,
 CONSTRAINT [PK_CardDetail] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Currency]    Script Date: 01/04/2021 09:14:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Currency](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[Code] [nvarchar](5) NULL,
	[Description] [nvarchar](250) NULL,
	[IsEnabled] [bit] NOT NULL,
	[Symbol] [nchar](1) NULL,
 CONSTRAINT [PK_Currency] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Merchant]    Script Date: 01/04/2021 09:14:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Merchant](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[MerchantRef] [uniqueidentifier] NOT NULL,
	[Name] [nvarchar](250) NOT NULL,
	[IsEnabled] [bit] NOT NULL,
 CONSTRAINT [PK_Merchant] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[MerchantAPIKey]    Script Date: 01/04/2021 09:14:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MerchantAPIKey](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[MerchantID] [int] NOT NULL,
	[APIKey] [nvarchar](200) NOT NULL,
	[IsEnabled] [bit] NOT NULL,
 CONSTRAINT [PK_MerchantAPIKey] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Transaction]    Script Date: 01/04/2021 09:14:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Transaction](
	[TransactionID] [uniqueidentifier] NOT NULL,
	[MerchantID] [int] NOT NULL,
	[MerchantRef] [nvarchar](20) NULL,
	[CardDetailID] [int] NOT NULL,
	[BankID] [int] NOT NULL,
	[CurrencyID] [int] NOT NULL,
	[Amount] [decimal](10, 2) NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
	[TransactionStatusID] [int] NOT NULL,
	[AuthCode] [nvarchar](20) NULL,
	[BankRef] [uniqueidentifier] NULL,
	[SourceType] [nvarchar](50) NULL,
 CONSTRAINT [PK_Transaction] PRIMARY KEY CLUSTERED 
(
	[TransactionID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TransactionHistory]    Script Date: 01/04/2021 09:14:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TransactionHistory](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[TransactionID] [uniqueidentifier] NOT NULL,
	[TransactionStatusID] [int] NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
 CONSTRAINT [PK_TransactionHistory] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TransactionStatus]    Script Date: 01/04/2021 09:14:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TransactionStatus](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[StatusName] [nvarchar](50) NOT NULL,
	[Description] [nvarchar](250) NULL,
 CONSTRAINT [PK_TransactionStatus] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[MerchantAPIKey]  WITH CHECK ADD  CONSTRAINT [FK_MerchantAPIKey_Merchant] FOREIGN KEY([MerchantID])
REFERENCES [dbo].[Merchant] ([ID])
GO
ALTER TABLE [dbo].[MerchantAPIKey] CHECK CONSTRAINT [FK_MerchantAPIKey_Merchant]
GO
ALTER TABLE [dbo].[Transaction]  WITH CHECK ADD  CONSTRAINT [FK_Transaction_Bank] FOREIGN KEY([BankID])
REFERENCES [dbo].[Bank] ([BankID])
GO
ALTER TABLE [dbo].[Transaction] CHECK CONSTRAINT [FK_Transaction_Bank]
GO
ALTER TABLE [dbo].[Transaction]  WITH CHECK ADD  CONSTRAINT [FK_Transaction_CardDetail] FOREIGN KEY([CardDetailID])
REFERENCES [dbo].[CardDetail] ([ID])
GO
ALTER TABLE [dbo].[Transaction] CHECK CONSTRAINT [FK_Transaction_CardDetail]
GO
ALTER TABLE [dbo].[Transaction]  WITH CHECK ADD  CONSTRAINT [FK_Transaction_Currency] FOREIGN KEY([CurrencyID])
REFERENCES [dbo].[Currency] ([ID])
GO
ALTER TABLE [dbo].[Transaction] CHECK CONSTRAINT [FK_Transaction_Currency]
GO
ALTER TABLE [dbo].[Transaction]  WITH CHECK ADD  CONSTRAINT [FK_Transaction_Merchant] FOREIGN KEY([MerchantID])
REFERENCES [dbo].[Merchant] ([ID])
GO
ALTER TABLE [dbo].[Transaction] CHECK CONSTRAINT [FK_Transaction_Merchant]
GO
ALTER TABLE [dbo].[Transaction]  WITH CHECK ADD  CONSTRAINT [FK_Transaction_TransactionStatus] FOREIGN KEY([TransactionStatusID])
REFERENCES [dbo].[TransactionStatus] ([ID])
GO
ALTER TABLE [dbo].[Transaction] CHECK CONSTRAINT [FK_Transaction_TransactionStatus]
GO
ALTER TABLE [dbo].[TransactionHistory]  WITH CHECK ADD  CONSTRAINT [FK_TransactionHistory_Transaction] FOREIGN KEY([TransactionID])
REFERENCES [dbo].[Transaction] ([TransactionID])
GO
ALTER TABLE [dbo].[TransactionHistory] CHECK CONSTRAINT [FK_TransactionHistory_Transaction]
GO
ALTER TABLE [dbo].[TransactionHistory]  WITH CHECK ADD  CONSTRAINT [FK_TransactionHistory_TransactionStatus] FOREIGN KEY([TransactionStatusID])
REFERENCES [dbo].[TransactionStatus] ([ID])
GO
ALTER TABLE [dbo].[TransactionHistory] CHECK CONSTRAINT [FK_TransactionHistory_TransactionStatus]
GO
USE [master]
GO
ALTER DATABASE [CheckoutDB] SET  READ_WRITE 
GO