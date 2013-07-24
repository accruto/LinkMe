IF EXISTS (SELECT name FROM master.dbo.sysdatabases WHERE name = N'Log4Net')
	DROP DATABASE [Log4Net]
GO
