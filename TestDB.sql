USE [master]
GO
/****** Object:  Database [TestWebUI20190816_db]    Script Date: 02-Sep-19 13:00:01 ******/
CREATE DATABASE [TestWebUI20190816_db]
GO
ALTER DATABASE [TestWebUI20190816_db] SET COMPATIBILITY_LEVEL = 140
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [TestWebUI20190816_db].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [TestWebUI20190816_db] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [TestWebUI20190816_db] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [TestWebUI20190816_db] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [TestWebUI20190816_db] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [TestWebUI20190816_db] SET ARITHABORT OFF 
GO
ALTER DATABASE [TestWebUI20190816_db] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [TestWebUI20190816_db] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [TestWebUI20190816_db] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [TestWebUI20190816_db] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [TestWebUI20190816_db] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [TestWebUI20190816_db] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [TestWebUI20190816_db] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [TestWebUI20190816_db] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [TestWebUI20190816_db] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [TestWebUI20190816_db] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [TestWebUI20190816_db] SET ALLOW_SNAPSHOT_ISOLATION ON 
GO
ALTER DATABASE [TestWebUI20190816_db] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [TestWebUI20190816_db] SET READ_COMMITTED_SNAPSHOT ON 
GO
ALTER DATABASE [TestWebUI20190816_db] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [TestWebUI20190816_db] SET  MULTI_USER 
GO
ALTER DATABASE [TestWebUI20190816_db] SET DB_CHAINING OFF 
GO
ALTER DATABASE [TestWebUI20190816_db] SET ENCRYPTION ON
GO
ALTER DATABASE [TestWebUI20190816_db] SET QUERY_STORE = ON
GO
ALTER DATABASE [TestWebUI20190816_db] SET QUERY_STORE (OPERATION_MODE = READ_WRITE, CLEANUP_POLICY = (STALE_QUERY_THRESHOLD_DAYS = 30), DATA_FLUSH_INTERVAL_SECONDS = 900, INTERVAL_LENGTH_MINUTES = 60, MAX_STORAGE_SIZE_MB = 100, QUERY_CAPTURE_MODE = AUTO, SIZE_BASED_CLEANUP_MODE = AUTO)
GO
USE [TestWebUI20190816_db]
GO
/****** Object:  Table [dbo].[Coupons]    Script Date: 02-Sep-19 13:00:09 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Coupons](
	[CouponID] [int] IDENTITY(1,1) NOT NULL,
	[CouponNumber] [nvarchar](50) NOT NULL,
	[CouponUsed] [bit] NOT NULL,
	[CouponOwner] [int] NULL,
	[TransactionID] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[CouponID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Customers]    Script Date: 02-Sep-19 13:00:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Customers](
	[CustomerID] [int] IDENTITY(1,1) NOT NULL,
	[PhoneNumber] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[CustomerID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Transactions]    Script Date: 02-Sep-19 13:00:13 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Transactions](
	[TransactionID] [int] IDENTITY(1,1) NOT NULL,
	[CustomerID] [int] NOT NULL,
	[CouponNumber] [nvarchar](50) NOT NULL,
	[TransactionNumber] [nvarchar](50) NOT NULL,
	[TransactionValue] [decimal](18, 0) NOT NULL,
	[PrizeID] [int] NULL,
	[TransactionDate] [date] NULL,
	[PrizeRecieved] [bit] NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Prizes]    Script Date: 02-Sep-19 13:00:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Prizes](
	[PrizeID] [int] IDENTITY(1,1) NOT NULL,
	[PrizeName] [nvarchar](50) NOT NULL
) ON [PRIMARY]
GO
/****** Object:  View [dbo].[CouponClaimPrizeView]    Script Date: 02-Sep-19 13:00:15 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[CouponClaimPrizeView]
AS
SELECT        Coupons.CouponID, Coupons.CouponNumber, Customers.PhoneNumber, Prizes.PrizeName
FROM            dbo.Coupons LEFT OUTER JOIN
                         dbo.Transactions ON Coupons.CouponNumber = Transactions.CouponNumber LEFT OUTER JOIN
                         dbo.Prizes ON Prizes.PrizeID = Transactions.PrizeID LEFT OUTER JOIN
                         dbo.Customers ON Customers.CustomerID = Coupons.CouponID
GO
/****** Object:  Table [dbo].[Contacts]    Script Date: 02-Sep-19 13:00:15 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Contacts](
	[ContactID] [int] IDENTITY(1,1) NOT NULL,
	[FirstName] [nvarchar](50) NOT NULL,
	[Lastname] [nvarchar](50) NOT NULL,
	[PostCode] [nvarchar](5) NOT NULL,
	[Email] [nvarchar](50) NULL,
PRIMARY KEY CLUSTERED 
(
	[ContactID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Users]    Script Date: 02-Sep-19 13:00:17 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Users](
	[UserID] [int] IDENTITY(1,1) NOT NULL,
	[UserName] [nvarchar](50) NOT NULL,
	[Password] [nvarchar](250) NOT NULL
) ON [PRIMARY]
GO
/****** Object:  Index [IX_Users]    Script Date: 02-Sep-19 13:00:18 ******/
CREATE UNIQUE NONCLUSTERED INDEX [IX_Users] ON [dbo].[Users]
(
	[UserID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Transactions] ADD  CONSTRAINT [DF_Transactions_PrizeRecieved]  DEFAULT ((0)) FOR [PrizeRecieved]
GO
USE [master]
GO
ALTER DATABASE [TestWebUI20190816_db] SET  READ_WRITE 
GO

SET IDENTITY_INSERT [dbo].[Contacts] ON
INSERT INTO [dbo].[Contacts] ([ContactID], [FirstName], [Lastname], [PostCode], [Email]) VALUES (1, N'a', N'a', N'11111', N'a@example.com')
INSERT INTO [dbo].[Contacts] ([ContactID], [FirstName], [Lastname], [PostCode], [Email]) VALUES (2, N'b', N'a', N'11112', NULL)
INSERT INTO [dbo].[Contacts] ([ContactID], [FirstName], [Lastname], [PostCode], [Email]) VALUES (3, N'c', N'a', N'11113', NULL)
INSERT INTO [dbo].[Contacts] ([ContactID], [FirstName], [Lastname], [PostCode], [Email]) VALUES (4, N'd', N'a', N'11114', NULL)
INSERT INTO [dbo].[Contacts] ([ContactID], [FirstName], [Lastname], [PostCode], [Email]) VALUES (5, N'e', N'a', N'11115', NULL)
INSERT INTO [dbo].[Contacts] ([ContactID], [FirstName], [Lastname], [PostCode], [Email]) VALUES (6, N'f', N'a', N'11116', NULL)
INSERT INTO [dbo].[Contacts] ([ContactID], [FirstName], [Lastname], [PostCode], [Email]) VALUES (7, N'A', N'a', N'66666', NULL)
INSERT INTO [dbo].[Contacts] ([ContactID], [FirstName], [Lastname], [PostCode], [Email]) VALUES (8, N'B', N'a', N'66522', NULL)
INSERT INTO [dbo].[Contacts] ([ContactID], [FirstName], [Lastname], [PostCode], [Email]) VALUES (9, N'C', N'a', N'77888', N'example@example.com')
INSERT INTO [dbo].[Contacts] ([ContactID], [FirstName], [Lastname], [PostCode], [Email]) VALUES (10, N'v', N'v', N'64654', NULL)
INSERT INTO [dbo].[Contacts] ([ContactID], [FirstName], [Lastname], [PostCode], [Email]) VALUES (11, N'A', N'a', N'33333', NULL)
INSERT INTO [dbo].[Contacts] ([ContactID], [FirstName], [Lastname], [PostCode], [Email]) VALUES (12, N'A', N'a', N'11111', NULL)
SET IDENTITY_INSERT [dbo].[Contacts] OFF

SET IDENTITY_INSERT [dbo].[Coupons] ON
INSERT INTO [dbo].[Coupons] ([CouponID], [CouponNumber], [CouponUsed], [CouponOwner], [TransactionID]) VALUES (1, N'0001', 1, 1, 1)
INSERT INTO [dbo].[Coupons] ([CouponID], [CouponNumber], [CouponUsed], [CouponOwner], [TransactionID]) VALUES (2, N'0002', 1, 5, 2)
INSERT INTO [dbo].[Coupons] ([CouponID], [CouponNumber], [CouponUsed], [CouponOwner], [TransactionID]) VALUES (3, N'0003', 0, NULL, NULL)
INSERT INTO [dbo].[Coupons] ([CouponID], [CouponNumber], [CouponUsed], [CouponOwner], [TransactionID]) VALUES (4, N'0004', 0, NULL, NULL)
INSERT INTO [dbo].[Coupons] ([CouponID], [CouponNumber], [CouponUsed], [CouponOwner], [TransactionID]) VALUES (5, N'0005', 0, NULL, NULL)
INSERT INTO [dbo].[Coupons] ([CouponID], [CouponNumber], [CouponUsed], [CouponOwner], [TransactionID]) VALUES (6, N'0006', 0, NULL, NULL)
INSERT INTO [dbo].[Coupons] ([CouponID], [CouponNumber], [CouponUsed], [CouponOwner], [TransactionID]) VALUES (7, N'0007', 0, NULL, NULL)
INSERT INTO [dbo].[Coupons] ([CouponID], [CouponNumber], [CouponUsed], [CouponOwner], [TransactionID]) VALUES (8, N'0008', 0, NULL, NULL)
INSERT INTO [dbo].[Coupons] ([CouponID], [CouponNumber], [CouponUsed], [CouponOwner], [TransactionID]) VALUES (9, N'0009', 0, NULL, NULL)
INSERT INTO [dbo].[Coupons] ([CouponID], [CouponNumber], [CouponUsed], [CouponOwner], [TransactionID]) VALUES (10, N'0010', 1, 1, 3)
INSERT INTO [dbo].[Coupons] ([CouponID], [CouponNumber], [CouponUsed], [CouponOwner], [TransactionID]) VALUES (11, N'0011', 1, 1, 4)
INSERT INTO [dbo].[Coupons] ([CouponID], [CouponNumber], [CouponUsed], [CouponOwner], [TransactionID]) VALUES (12, N'0012', 1, NULL, 0)
INSERT INTO [dbo].[Coupons] ([CouponID], [CouponNumber], [CouponUsed], [CouponOwner], [TransactionID]) VALUES (13, N'0013', 0, NULL, NULL)
INSERT INTO [dbo].[Coupons] ([CouponID], [CouponNumber], [CouponUsed], [CouponOwner], [TransactionID]) VALUES (14, N'0014', 0, NULL, NULL)
INSERT INTO [dbo].[Coupons] ([CouponID], [CouponNumber], [CouponUsed], [CouponOwner], [TransactionID]) VALUES (15, N'0015', 0, NULL, NULL)
INSERT INTO [dbo].[Coupons] ([CouponID], [CouponNumber], [CouponUsed], [CouponOwner], [TransactionID]) VALUES (16, N'0016', 0, NULL, NULL)
INSERT INTO [dbo].[Coupons] ([CouponID], [CouponNumber], [CouponUsed], [CouponOwner], [TransactionID]) VALUES (17, N'0017', 0, NULL, NULL)
INSERT INTO [dbo].[Coupons] ([CouponID], [CouponNumber], [CouponUsed], [CouponOwner], [TransactionID]) VALUES (19, N'0018', 0, NULL, NULL)
INSERT INTO [dbo].[Coupons] ([CouponID], [CouponNumber], [CouponUsed], [CouponOwner], [TransactionID]) VALUES (20, N'0019', 0, NULL, NULL)
INSERT INTO [dbo].[Coupons] ([CouponID], [CouponNumber], [CouponUsed], [CouponOwner], [TransactionID]) VALUES (21, N'0020', 0, NULL, NULL)
SET IDENTITY_INSERT [dbo].[Coupons] OFF

SET IDENTITY_INSERT [dbo].[Users] ON
INSERT INTO [dbo].[Users] ([UserID], [UserName], [Password]) VALUES (1, N'example1', N'password')
SET IDENTITY_INSERT [dbo].[Users] OFF

SET IDENTITY_INSERT [dbo].[Transactions] ON
INSERT INTO [dbo].[Transactions] ([TransactionID], [CustomerID], [CouponNumber], [TransactionNumber], [TransactionValue], [PrizeID], [TransactionDate], [PrizeRecieved]) VALUES (1, 1, N'0001', N'357', CAST(22 AS Decimal(18, 0)), 5, N'2019-01-01', 1)
INSERT INTO [dbo].[Transactions] ([TransactionID], [CustomerID], [CouponNumber], [TransactionNumber], [TransactionValue], [PrizeID], [TransactionDate], [PrizeRecieved]) VALUES (2, 5, N'0002', N'445', CAST(1 AS Decimal(18, 0)), 3, N'2019-08-22', 0)
INSERT INTO [dbo].[Transactions] ([TransactionID], [CustomerID], [CouponNumber], [TransactionNumber], [TransactionValue], [PrizeID], [TransactionDate], [PrizeRecieved]) VALUES (3, 1, N'0010', N'4578', CAST(22 AS Decimal(18, 0)), 2, N'2019-04-07', 0)
INSERT INTO [dbo].[Transactions] ([TransactionID], [CustomerID], [CouponNumber], [TransactionNumber], [TransactionValue], [PrizeID], [TransactionDate], [PrizeRecieved]) VALUES (4, 1, N'0011', N'4579', CAST(333 AS Decimal(18, 0)), 2, N'2019-08-08', 1)
SET IDENTITY_INSERT [dbo].[Transactions] OFF

SET IDENTITY_INSERT [dbo].[Customers] ON
INSERT INTO [dbo].[Customers] ([CustomerID], [PhoneNumber]) VALUES (1, 123456789)
INSERT INTO [dbo].[Customers] ([CustomerID], [PhoneNumber]) VALUES (2, 123456788)
INSERT INTO [dbo].[Customers] ([CustomerID], [PhoneNumber]) VALUES (3, 123456787)
INSERT INTO [dbo].[Customers] ([CustomerID], [PhoneNumber]) VALUES (4, 123456786)
INSERT INTO [dbo].[Customers] ([CustomerID], [PhoneNumber]) VALUES (5, 123456785)
INSERT INTO [dbo].[Customers] ([CustomerID], [PhoneNumber]) VALUES (6, 123456784)
INSERT INTO [dbo].[Customers] ([CustomerID], [PhoneNumber]) VALUES (7, 123456783)
INSERT INTO [dbo].[Customers] ([CustomerID], [PhoneNumber]) VALUES (8, 660660660)
INSERT INTO [dbo].[Customers] ([CustomerID], [PhoneNumber]) VALUES (9, 666777888)
INSERT INTO [dbo].[Customers] ([CustomerID], [PhoneNumber]) VALUES (10, 147852369)
INSERT INTO [dbo].[Customers] ([CustomerID], [PhoneNumber]) VALUES (11, 322555555)
INSERT INTO [dbo].[Customers] ([CustomerID], [PhoneNumber]) VALUES (12, 123456780)
SET IDENTITY_INSERT [dbo].[Customers] OFF

SET IDENTITY_INSERT [dbo].[Prizes] ON
INSERT INTO [dbo].[Prizes] ([PrizeID], [PrizeName]) VALUES (1, N'Placeholder')
INSERT INTO [dbo].[Prizes] ([PrizeID], [PrizeName]) VALUES (2, N'1st Prize')
INSERT INTO [dbo].[Prizes] ([PrizeID], [PrizeName]) VALUES (3, N'2nd Prize')
INSERT INTO [dbo].[Prizes] ([PrizeID], [PrizeName]) VALUES (4, N'3rd Prize')
INSERT INTO [dbo].[Prizes] ([PrizeID], [PrizeName]) VALUES (5, N'Participation Prize')
SET IDENTITY_INSERT [dbo].[Prizes] OFF

