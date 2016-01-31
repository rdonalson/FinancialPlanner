USE [master]
GO

/* For security reasons the login is created disabled and with a random password. */
/****** Object:  Login [fpadmin]    Script Date: 1/6/2016 9:08:15 AM ******/
CREATE LOGIN [fpadmin] WITH PASSWORD=N'U9ygmavCCk5fZX2uZfIMzFzr2Qz5/zQ0yPskCysaoU8=', DEFAULT_DATABASE=[master], DEFAULT_LANGUAGE=[us_english], CHECK_EXPIRATION=OFF, CHECK_POLICY=OFF
GO

ALTER LOGIN [fpadmin] DISABLE
GO


USE [FinancialPlanner]
GO
/****** Object:  User [fpadmin]    Script Date: 1/6/2016 9:07:31 AM ******/
CREATE USER [fpadmin] FOR LOGIN [fpadmin] WITH DEFAULT_SCHEMA=[dbo]
GO
ALTER ROLE [db_owner] ADD MEMBER [fpadmin]
GO
