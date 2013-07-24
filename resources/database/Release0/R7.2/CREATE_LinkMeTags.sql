IF EXISTS (SELECT name FROM master.dbo.sysdatabases WHERE name = N'LinkMeTags')
	DROP DATABASE [LinkMeTags]
GO

CREATE DATABASE [LinkMeTags]  ON (NAME = N'LinkMeTags_Data', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL\data\LinkMeTags_Data.MDF' , SIZE = 1, FILEGROWTH = 10%) LOG ON (NAME = N'LinkMeTags_Log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL\data\LinkMeTags_Log.LDF' , SIZE = 1, FILEGROWTH = 10%)
 COLLATE SQL_Latin1_General_CP1_CI_AS
GO

exec sp_dboption N'LinkMeTags', N'autoclose', N'true'
GO

exec sp_dboption N'LinkMeTags', N'bulkcopy', N'false'
GO

exec sp_dboption N'LinkMeTags', N'trunc. log', N'true'
GO

exec sp_dboption N'LinkMeTags', N'torn page detection', N'true'
GO

exec sp_dboption N'LinkMeTags', N'read only', N'false'
GO

exec sp_dboption N'LinkMeTags', N'dbo use', N'false'
GO

exec sp_dboption N'LinkMeTags', N'single', N'false'
GO

exec sp_dboption N'LinkMeTags', N'autoshrink', N'true'
GO

exec sp_dboption N'LinkMeTags', N'ANSI null default', N'false'
GO

exec sp_dboption N'LinkMeTags', N'recursive triggers', N'false'
GO

exec sp_dboption N'LinkMeTags', N'ANSI nulls', N'false'
GO

exec sp_dboption N'LinkMeTags', N'concat null yields null', N'false'
GO

exec sp_dboption N'LinkMeTags', N'cursor close on commit', N'false'
GO

exec sp_dboption N'LinkMeTags', N'default to local cursor', N'false'
GO

exec sp_dboption N'LinkMeTags', N'quoted identifier', N'false'
GO

exec sp_dboption N'LinkMeTags', N'ANSI warnings', N'false'
GO

exec sp_dboption N'LinkMeTags', N'auto create statistics', N'true'
GO

exec sp_dboption N'LinkMeTags', N'auto update statistics', N'true'
GO
