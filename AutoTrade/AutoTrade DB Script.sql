USE [master]
GO

/****** Object:  Database [AutoTrade2]    Script Date: 06/04/2013 08:02:58 ******/
IF  EXISTS (SELECT name FROM sys.databases WHERE name = N'AutoTrade2')
DROP DATABASE [AutoTrade2]
GO

USE [master]
GO

/****** Object:  Database [AutoTrade2]    Script Date: 06/04/2013 08:02:58 ******/
CREATE DATABASE [AutoTrade2] ON  PRIMARY 
( NAME = N'AutoTrade2', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL11.MSSQLSERVER\MSSQL\DATA\AutoTrade2.mdf' , SIZE = 102400KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB ) LOG ON 
( NAME = N'AutoTrade2_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL11.MSSQLSERVER\MSSQL\DATA\AutoTrade2_log.ldf' , SIZE = 8384KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)
GO

IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [AutoTrade2].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO

ALTER DATABASE [AutoTrade2] SET ANSI_NULL_DEFAULT OFF 
GO

ALTER DATABASE [AutoTrade2] SET ANSI_NULLS OFF 
GO

ALTER DATABASE [AutoTrade2] SET ANSI_PADDING OFF 
GO

ALTER DATABASE [AutoTrade2] SET ANSI_WARNINGS OFF 
GO

ALTER DATABASE [AutoTrade2] SET ARITHABORT OFF 
GO

ALTER DATABASE [AutoTrade2] SET AUTO_CLOSE OFF 
GO

ALTER DATABASE [AutoTrade2] SET AUTO_CREATE_STATISTICS ON 
GO

ALTER DATABASE [AutoTrade2] SET AUTO_SHRINK OFF 
GO

ALTER DATABASE [AutoTrade2] SET AUTO_UPDATE_STATISTICS ON 
GO

ALTER DATABASE [AutoTrade2] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO

ALTER DATABASE [AutoTrade2] SET CURSOR_DEFAULT  GLOBAL 
GO

ALTER DATABASE [AutoTrade2] SET CONCAT_NULL_YIELDS_NULL OFF 
GO

ALTER DATABASE [AutoTrade2] SET NUMERIC_ROUNDABORT OFF 
GO

ALTER DATABASE [AutoTrade2] SET QUOTED_IDENTIFIER OFF 
GO

ALTER DATABASE [AutoTrade2] SET RECURSIVE_TRIGGERS OFF 
GO

ALTER DATABASE [AutoTrade2] SET  DISABLE_BROKER 
GO

ALTER DATABASE [AutoTrade2] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO

ALTER DATABASE [AutoTrade2] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO

ALTER DATABASE [AutoTrade2] SET TRUSTWORTHY OFF 
GO

ALTER DATABASE [AutoTrade2] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO

ALTER DATABASE [AutoTrade2] SET PARAMETERIZATION SIMPLE 
GO

ALTER DATABASE [AutoTrade2] SET READ_COMMITTED_SNAPSHOT OFF 
GO

ALTER DATABASE [AutoTrade2] SET HONOR_BROKER_PRIORITY OFF 
GO

ALTER DATABASE [AutoTrade2] SET  READ_WRITE 
GO

ALTER DATABASE [AutoTrade2] SET RECOVERY FULL 
GO

ALTER DATABASE [AutoTrade2] SET  MULTI_USER 
GO

ALTER DATABASE [AutoTrade2] SET PAGE_VERIFY CHECKSUM  
GO

ALTER DATABASE [AutoTrade2] SET DB_CHAINING OFF 
GO




USE [AutoTrade2]
GO

IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_tLogEntry_dtCreated]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[tLogEntry] DROP CONSTRAINT [DF_tLogEntry_dtCreated]
END

GO

USE [AutoTrade2]
GO

/****** Object:  Table [dbo].[tLogEntry]    Script Date: 06/04/2013 01:52:25 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tLogEntry]') AND type in (N'U'))
DROP TABLE [dbo].[tLogEntry]
GO

USE [AutoTrade2]
GO

/****** Object:  Table [dbo].[tLogEntry]    Script Date: 06/04/2013 01:52:25 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[tLogEntry](
	[idLogEntry] [int] IDENTITY(1,1) NOT NULL,
	[dtCreated] [datetime] NOT NULL,
	[sDescription] [text] NULL,
 CONSTRAINT [PK_tLogEntry] PRIMARY KEY CLUSTERED 
(
	[idLogEntry] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO

ALTER TABLE [dbo].[tLogEntry] ADD  CONSTRAINT [DF_tLogEntry_dtCreated]  DEFAULT (getdate()) FOR [dtCreated]
GO



USE [AutoTrade2]
GO

/****** Object:  Table [dbo].[tOrderType]    Script Date: 06/04/2013 01:52:14 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tOrderType]') AND type in (N'U'))
DROP TABLE [dbo].[tOrderType]
GO

USE [AutoTrade2]
GO

/****** Object:  Table [dbo].[tOrderType]    Script Date: 06/04/2013 01:52:14 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[tOrderType](
	[idOrderType] [int] IDENTITY(1,1) NOT NULL,
	[sName] [varchar](20) NOT NULL,
 CONSTRAINT [PK_tOrderType] PRIMARY KEY CLUSTERED 
(
	[idOrderType] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO


USE [AutoTrade2]
GO

/****** Object:  Table [dbo].[tBitcoinPrice]    Script Date: 06/04/2013 01:52:02 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tBitcoinPrice]') AND type in (N'U'))
DROP TABLE [dbo].[tBitcoinPrice]
GO

USE [AutoTrade2]
GO

/****** Object:  Table [dbo].[tBitcoinPrice]    Script Date: 06/04/2013 01:52:02 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[tBitcoinPrice](
	[idBitcoinPrice] [int] IDENTITY(1,1) NOT NULL,
	[decBid] [decimal](24, 10) NOT NULL,
	[decAsk] [decimal](24, 10) NOT NULL,
	[decPrice] [decimal](24, 10) NOT NULL,
	[dtPrice] [datetime] NOT NULL,
 CONSTRAINT [PK_tBitcoinPrice] PRIMARY KEY CLUSTERED 
(
	[idBitcoinPrice] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO


USE [AutoTrade2]
GO

IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_tBitcoinOrder_tOrderType]') AND parent_object_id = OBJECT_ID(N'[dbo].[tBitcoinOrder]'))
ALTER TABLE [dbo].[tBitcoinOrder] DROP CONSTRAINT [FK_tBitcoinOrder_tOrderType]
GO

USE [AutoTrade2]
GO

/****** Object:  Table [dbo].[tBitcoinOrder]    Script Date: 06/04/2013 01:51:43 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tBitcoinOrder]') AND type in (N'U'))
DROP TABLE [dbo].[tBitcoinOrder]
GO

USE [AutoTrade2]
GO

/****** Object:  Table [dbo].[tBitcoinOrder]    Script Date: 06/04/2013 01:51:44 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[tBitcoinOrder](
	[idBitcoinOrder] [int] IDENTITY(1,1) NOT NULL,
	[decPrice] [decimal](24, 10) NOT NULL,
	[dtOrder] [datetime] NOT NULL,
	[idOrderType] [int] NOT NULL,
	[decVolume] [decimal](24, 10) NOT NULL,
 CONSTRAINT [PK_tBitcoinOrder] PRIMARY KEY CLUSTERED 
(
	[idBitcoinOrder] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[tBitcoinOrder]  WITH CHECK ADD  CONSTRAINT [FK_tBitcoinOrder_tOrderType] FOREIGN KEY([idOrderType])
REFERENCES [dbo].[tOrderType] ([idOrderType])
GO

ALTER TABLE [dbo].[tBitcoinOrder] CHECK CONSTRAINT [FK_tBitcoinOrder_tOrderType]
GO


USE [AutoTrade2]
GO

/****** Object:  Table [dbo].[tBitcoinDepth]    Script Date: 06/04/2013 01:51:29 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tBitcoinDepth]') AND type in (N'U'))
DROP TABLE [dbo].[tBitcoinDepth]
GO

USE [AutoTrade2]
GO

/****** Object:  Table [dbo].[tBitcoinDepth]    Script Date: 06/04/2013 01:51:29 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[tBitcoinDepth](
	[idBitcoinDepth] [int] IDENTITY(1,1) NOT NULL,
	[decFortyCoinVWAPBuy] [decimal](24, 10) NULL,
	[decFortyCoinVWAPSell] [decimal](24, 10) NULL,
	[dtDepth] [datetime] NOT NULL,
 CONSTRAINT [PK_tBitcoinDepth] PRIMARY KEY CLUSTERED 
(
	[idBitcoinDepth] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO



USE [AutoTrade2]
GO

/****** Object:  StoredProcedure [dbo].[C_LOG_ENTRY_INS]    Script Date: 06/04/2013 01:48:41 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[C_LOG_ENTRY_INS]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[C_LOG_ENTRY_INS]
GO

USE [AutoTrade2]
GO

/****** Object:  StoredProcedure [dbo].[C_LOG_ENTRY_INS]    Script Date: 06/04/2013 01:48:41 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

-- ================================================
-- C_LOG_ENTRY_INS
-- Inserts record into tLogEntry
-- BH20130524-Created
-- ================================================
CREATE PROCEDURE [dbo].[C_LOG_ENTRY_INS](
				@sDescription text)
AS
BEGIN

	INSERT INTO [dbo].[tLogEntry] (sDescription)
		 VALUES (@sDescription)

END


GO


USE [AutoTrade2]
GO

/****** Object:  StoredProcedure [dbo].[C_BITCOIN_DEPTH_INS]    Script Date: 06/04/2013 01:48:57 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[C_BITCOIN_DEPTH_INS]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[C_BITCOIN_DEPTH_INS]
GO

USE [AutoTrade2]
GO

/****** Object:  StoredProcedure [dbo].[C_BITCOIN_DEPTH_INS]    Script Date: 06/04/2013 01:48:57 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

-- ================================================
-- C_BITCOIN_DEPTH_INS
-- Inserts record into tBitcoinDepth
-- BH20130603-Created
-- ================================================
CREATE PROCEDURE [dbo].[C_BITCOIN_DEPTH_INS](
		@decFortyCoinVWAPBuy decimal(24,10),
		@decFortyCoinVWAPSell decimal(24,10),
		@dtDepthEST datetime
		)
AS
BEGIN

	--declare @dtDepthEST datetime
	--set @dtDepthEST = dateadd(hour, -4, @dtDepthUTC)
	
	INSERT INTO [dbo].[tBitcoinDepth]
	(
	decFortyCoinVWAPBuy,
	decFortyCoinVWAPSell,
	dtDepth
	)
	VALUES
	(
	@decFortyCoinVWAPBuy,
	@decFortyCoinVWAPSell,
	@dtDepthEST
	)

END



GO


USE [AutoTrade2]
GO

/****** Object:  StoredProcedure [dbo].[C_BITCOIN_ORDER_INS]    Script Date: 06/04/2013 01:49:04 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[C_BITCOIN_ORDER_INS]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[C_BITCOIN_ORDER_INS]
GO

USE [AutoTrade2]
GO

/****** Object:  StoredProcedure [dbo].[C_BITCOIN_ORDER_INS]    Script Date: 06/04/2013 01:49:04 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

-- ================================================
-- C_BITCOIN_ORDER_INS
-- Inserts record into tBitcoinOrder
-- BH20130510-Created
-- ================================================
CREATE PROCEDURE [dbo].[C_BITCOIN_ORDER_INS](
				@decPrice decimal(24,10),
				@dtOrderUTC datetime,
				@idOrderType int,
				@decVolume decimal(24,10))
AS
BEGIN

	declare @dtOrderEST datetime
	set @dtOrderEST = dateadd(hour, -4, @dtOrderUTC)
	INSERT INTO [dbo].[tBitcoinOrder] (decPrice, dtOrder, idOrderType, decVolume)
		 VALUES (@decPrice, @dtOrderEST, @idOrderType, @decVolume)

END

GO



USE [AutoTrade2]
GO

/****** Object:  StoredProcedure [dbo].[C_BITCOIN_PRICE_INS]    Script Date: 06/04/2013 01:49:11 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[C_BITCOIN_PRICE_INS]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[C_BITCOIN_PRICE_INS]
GO

USE [AutoTrade2]
GO

/****** Object:  StoredProcedure [dbo].[C_BITCOIN_PRICE_INS]    Script Date: 06/04/2013 01:49:11 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

-- ================================================
-- C_BITCOIN_PRICE_INS
-- Inserts record into tBitcoinPrice
-- BH20130507-Created
-- ================================================
CREATE PROCEDURE [dbo].[C_BITCOIN_PRICE_INS](
				@decBid decimal(24,10),
				@decAsk decimal(24,10),
				@decPrice decimal(24,10),
				@dtPriceUTC datetime)
AS
BEGIN

	declare @dtPriceEST datetime
	set @dtPriceEST = dateadd(hour, -4, @dtPriceUTC)

	INSERT INTO [dbo].[tBitcoinPrice] ([decBid],[decAsk],[decPrice],[dtPrice])
		 VALUES (@decBid, @decAsk, @decPrice, @dtPriceEST)

END

GO


USE [AutoTrade2]


set identity_insert tOrderType on

insert tOrderType(idOrderType, sName)
values (1,'Bid')

insert tOrderType(idOrderType, sName)
values (2,'Ask')

set identity_insert tOrderType off
go

--select * from tOrderType

