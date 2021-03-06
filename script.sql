USE [master]
GO
/****** Object:  Database [Depo]    Script Date: 27.02.2022 13:21:26 ******/
CREATE DATABASE [Depo]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'Depo', FILENAME = N'c:\Program Files\Microsoft SQL Server\MSSQL11.SQLEXPRESS\MSSQL\DATA\Depo.mdf' , SIZE = 5120KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB )
 LOG ON 
( NAME = N'Depo_log', FILENAME = N'c:\Program Files\Microsoft SQL Server\MSSQL11.SQLEXPRESS\MSSQL\DATA\Depo_log.ldf' , SIZE = 1024KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)
GO
ALTER DATABASE [Depo] SET COMPATIBILITY_LEVEL = 110
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [Depo].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [Depo] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [Depo] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [Depo] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [Depo] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [Depo] SET ARITHABORT OFF 
GO
ALTER DATABASE [Depo] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [Depo] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [Depo] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [Depo] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [Depo] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [Depo] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [Depo] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [Depo] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [Depo] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [Depo] SET  DISABLE_BROKER 
GO
ALTER DATABASE [Depo] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [Depo] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [Depo] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [Depo] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [Depo] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [Depo] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [Depo] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [Depo] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [Depo] SET  MULTI_USER 
GO
ALTER DATABASE [Depo] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [Depo] SET DB_CHAINING OFF 
GO
ALTER DATABASE [Depo] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [Depo] SET TARGET_RECOVERY_TIME = 0 SECONDS 
GO
USE [Depo]
GO
/****** Object:  Table [dbo].[Dolum]    Script Date: 27.02.2022 13:21:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Dolum](
	[tblMax] [int] IDENTITY(1,1) NOT NULL,
	[KategoriAd] [nvarchar](50) NULL,
	[KategoriDepoMax] [int] NULL,
 CONSTRAINT [PK_Dolum] PRIMARY KEY CLUSTERED 
(
	[tblMax] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Kategori]    Script Date: 27.02.2022 13:21:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Kategori](
	[KategoriId] [int] IDENTITY(1,1) NOT NULL,
	[Kategoriler] [nvarchar](80) NULL,
 CONSTRAINT [PK_Kategori] PRIMARY KEY CLUSTERED 
(
	[KategoriId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Kullanici]    Script Date: 27.02.2022 13:21:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Kullanici](
	[K_id] [int] IDENTITY(1,1) NOT NULL,
	[K_name] [nvarchar](80) NULL,
	[K_pasword] [nvarchar](80) NULL,
 CONSTRAINT [PK_Kullanici] PRIMARY KEY CLUSTERED 
(
	[K_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Marka]    Script Date: 27.02.2022 13:21:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Marka](
	[MarkaId] [int] NULL,
	[Kategoriler] [nvarchar](80) NULL,
	[Marka] [nvarchar](80) NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Musteri]    Script Date: 27.02.2022 13:21:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Musteri](
	[m_id] [int] NOT NULL,
	[adsoyad] [nvarchar](80) NULL,
	[telefon] [nvarchar](80) NULL,
	[adres] [nvarchar](80) NULL,
	[email] [nvarchar](80) NULL,
 CONSTRAINT [PK_Musteri] PRIMARY KEY CLUSTERED 
(
	[m_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Satis]    Script Date: 27.02.2022 13:21:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Satis](
	[Satis_id] [int] IDENTITY(1,1) NOT NULL,
	[M_id] [nvarchar](50) NULL,
	[AdSoyad] [nvarchar](50) NULL,
	[Telefon] [nvarchar](50) NULL,
	[BarkodNo] [nvarchar](50) NULL,
	[UrunAdi] [nvarchar](50) NULL,
	[Miktari] [int] NULL,
	[SatisFiyat] [decimal](18, 2) NULL,
	[ToplamFiyat] [decimal](18, 2) NULL,
	[Tarih] [nvarchar](50) NULL,
 CONSTRAINT [PK_Satis] PRIMARY KEY CLUSTERED 
(
	[Satis_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Sepet]    Script Date: 27.02.2022 13:21:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Sepet](
	[Table_id] [int] IDENTITY(1,1) NOT NULL,
	[M_id] [nvarchar](80) NULL,
	[AdSoyad] [nvarchar](80) NULL,
	[Telefon] [nvarchar](80) NULL,
	[Barkodno] [nvarchar](80) NULL,
	[UrunAdi] [nvarchar](80) NULL,
	[Miktari] [int] NULL,
	[SatisFiyat] [decimal](18, 2) NULL,
	[ToplamFiyat] [decimal](18, 2) NULL,
	[Tarih] [nvarchar](80) NULL,
 CONSTRAINT [PK_Sepet] PRIMARY KEY CLUSTERED 
(
	[Table_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Urun]    Script Date: 27.02.2022 13:21:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Urun](
	[Product_Id] [int] IDENTITY(1,1) NOT NULL,
	[Barkodno] [nvarchar](80) NULL,
	[Kategori] [nvarchar](80) NULL,
	[Marka] [nvarchar](80) NULL,
	[Urun_Adi] [nvarchar](80) NULL,
	[Miktari] [int] NULL,
	[Alis_Fiyati] [decimal](18, 2) NULL,
	[Satis_Fiyati] [decimal](18, 2) NULL,
	[Tarih] [nvarchar](50) NULL,
 CONSTRAINT [PK_Urun] PRIMARY KEY CLUSTERED 
(
	[Product_Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
USE [master]
GO
ALTER DATABASE [Depo] SET  READ_WRITE 
GO
