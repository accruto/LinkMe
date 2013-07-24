CREATE DATABASE [Log4Net]  ON (NAME = N'Log4Net_Data', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL\data\Log4Net_Data.MDF' , SIZE = 28, FILEGROWTH = 10%) LOG ON (NAME = N'Log4Net_Log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL\data\Log4Net_Log.LDF' , SIZE = 1, FILEGROWTH = 10%)
 COLLATE SQL_Latin1_General_CP1_CI_AS
GO

exec sp_dboption N'Log4Net', N'autoclose', N'true'
GO

exec sp_dboption N'Log4Net', N'bulkcopy', N'false'
GO

exec sp_dboption N'Log4Net', N'trunc. log', N'true'
GO

exec sp_dboption N'Log4Net', N'torn page detection', N'true'
GO

exec sp_dboption N'Log4Net', N'read only', N'false'
GO

exec sp_dboption N'Log4Net', N'dbo use', N'false'
GO

exec sp_dboption N'Log4Net', N'single', N'false'
GO

exec sp_dboption N'Log4Net', N'autoshrink', N'true'
GO

exec sp_dboption N'Log4Net', N'ANSI null default', N'false'
GO

exec sp_dboption N'Log4Net', N'recursive triggers', N'false'
GO

exec sp_dboption N'Log4Net', N'ANSI nulls', N'false'
GO

exec sp_dboption N'Log4Net', N'concat null yields null', N'false'
GO

exec sp_dboption N'Log4Net', N'cursor close on commit', N'false'
GO

exec sp_dboption N'Log4Net', N'default to local cursor', N'false'
GO

exec sp_dboption N'Log4Net', N'quoted identifier', N'false'
GO

exec sp_dboption N'Log4Net', N'ANSI warnings', N'false'
GO

exec sp_dboption N'Log4Net', N'auto create statistics', N'true'
GO

exec sp_dboption N'Log4Net', N'auto update statistics', N'true'
GO
