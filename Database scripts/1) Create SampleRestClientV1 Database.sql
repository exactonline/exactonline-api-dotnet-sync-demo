USE [master]
GO
/****** Object:  Database [SampleRestClientV1]    Script Date: 16-Apr-21 01:46:52 PM ******/
CREATE DATABASE [SampleRestClientV1]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'SampleRestClientV1', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL14.MSSQLSERVER\MSSQL\DATA\SampleRestClientV1.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'SampleRestClientV1_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL14.MSSQLSERVER\MSSQL\DATA\SampleRestClientV1_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
GO
ALTER DATABASE [SampleRestClientV1] SET COMPATIBILITY_LEVEL = 140
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [SampleRestClientV1].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [SampleRestClientV1] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [SampleRestClientV1] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [SampleRestClientV1] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [SampleRestClientV1] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [SampleRestClientV1] SET ARITHABORT OFF 
GO
ALTER DATABASE [SampleRestClientV1] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [SampleRestClientV1] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [SampleRestClientV1] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [SampleRestClientV1] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [SampleRestClientV1] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [SampleRestClientV1] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [SampleRestClientV1] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [SampleRestClientV1] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [SampleRestClientV1] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [SampleRestClientV1] SET  DISABLE_BROKER 
GO
ALTER DATABASE [SampleRestClientV1] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [SampleRestClientV1] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [SampleRestClientV1] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [SampleRestClientV1] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [SampleRestClientV1] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [SampleRestClientV1] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [SampleRestClientV1] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [SampleRestClientV1] SET RECOVERY FULL 
GO
ALTER DATABASE [SampleRestClientV1] SET  MULTI_USER 
GO
ALTER DATABASE [SampleRestClientV1] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [SampleRestClientV1] SET DB_CHAINING OFF 
GO
ALTER DATABASE [SampleRestClientV1] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [SampleRestClientV1] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [SampleRestClientV1] SET DELAYED_DURABILITY = DISABLED 
GO
EXEC sys.sp_db_vardecimal_storage_format N'SampleRestClientV1', N'ON'
GO
ALTER DATABASE [SampleRestClientV1] SET QUERY_STORE = OFF
GO
USE [SampleRestClientV1]
GO
/****** Object:  Table [dbo].[CrmAccount]    Script Date: 16-Apr-21 01:46:52 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CrmAccount](
	[ID] [uniqueidentifier] NOT NULL,
	[Code] [varchar](200) NULL,
	[Name] [varchar](500) NOT NULL,
	[Website] [varchar](1000) NULL,
	[StartDate] [datetime] NULL,
	[EndDate] [datetime] NULL,
	[Timestamp] [bigint] NOT NULL,
 CONSTRAINT [PK_CrmAccount] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
USE [master]
GO
ALTER DATABASE [SampleRestClientV1] SET  READ_WRITE 
GO
