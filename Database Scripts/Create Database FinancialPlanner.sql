USE [master]
GO

/****** Object:  Database [FinancialPlanner]    Script Date: 1/6/2016 9:02:02 AM ******/
CREATE DATABASE [FinancialPlanner]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'FinancialPlanner', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL11.APPLICATION\MSSQL\DATA\FinancialPlanner.mdf' , SIZE = 4096KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB )
 LOG ON 
( NAME = N'FinancialPlanner_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL11.APPLICATION\MSSQL\DATA\FinancialPlanner_log.ldf' , SIZE = 1024KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)
GO

ALTER DATABASE [FinancialPlanner] SET COMPATIBILITY_LEVEL = 110
GO

IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [FinancialPlanner].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO

ALTER DATABASE [FinancialPlanner] SET ANSI_NULL_DEFAULT OFF 
GO

ALTER DATABASE [FinancialPlanner] SET ANSI_NULLS OFF 
GO

ALTER DATABASE [FinancialPlanner] SET ANSI_PADDING OFF 
GO

ALTER DATABASE [FinancialPlanner] SET ANSI_WARNINGS OFF 
GO

ALTER DATABASE [FinancialPlanner] SET ARITHABORT OFF 
GO

ALTER DATABASE [FinancialPlanner] SET AUTO_CLOSE OFF 
GO

ALTER DATABASE [FinancialPlanner] SET AUTO_SHRINK OFF 
GO

ALTER DATABASE [FinancialPlanner] SET AUTO_UPDATE_STATISTICS ON 
GO

ALTER DATABASE [FinancialPlanner] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO

ALTER DATABASE [FinancialPlanner] SET CURSOR_DEFAULT  GLOBAL 
GO

ALTER DATABASE [FinancialPlanner] SET CONCAT_NULL_YIELDS_NULL OFF 
GO

ALTER DATABASE [FinancialPlanner] SET NUMERIC_ROUNDABORT OFF 
GO

ALTER DATABASE [FinancialPlanner] SET QUOTED_IDENTIFIER OFF 
GO

ALTER DATABASE [FinancialPlanner] SET RECURSIVE_TRIGGERS OFF 
GO

ALTER DATABASE [FinancialPlanner] SET  DISABLE_BROKER 
GO

ALTER DATABASE [FinancialPlanner] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO

ALTER DATABASE [FinancialPlanner] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO

ALTER DATABASE [FinancialPlanner] SET TRUSTWORTHY OFF 
GO

ALTER DATABASE [FinancialPlanner] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO

ALTER DATABASE [FinancialPlanner] SET PARAMETERIZATION SIMPLE 
GO

ALTER DATABASE [FinancialPlanner] SET READ_COMMITTED_SNAPSHOT OFF 
GO

ALTER DATABASE [FinancialPlanner] SET HONOR_BROKER_PRIORITY OFF 
GO

ALTER DATABASE [FinancialPlanner] SET RECOVERY FULL 
GO

ALTER DATABASE [FinancialPlanner] SET  MULTI_USER 
GO

ALTER DATABASE [FinancialPlanner] SET PAGE_VERIFY CHECKSUM  
GO

ALTER DATABASE [FinancialPlanner] SET DB_CHAINING OFF 
GO

ALTER DATABASE [FinancialPlanner] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO

ALTER DATABASE [FinancialPlanner] SET TARGET_RECOVERY_TIME = 0 SECONDS 
GO

ALTER DATABASE [FinancialPlanner] SET  READ_WRITE 
GO


