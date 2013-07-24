if exists (select * from dbo.systypes where name = N'RelocationPreference')
exec sp_droptype N'RelocationPreference'
GO

EXEC sp_addtype N'RelocationPreference', N'tinyint', N'not null'
GO
