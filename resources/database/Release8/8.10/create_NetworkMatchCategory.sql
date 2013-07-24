if exists (select * from dbo.systypes where name = N'NetworkMatchCategory')
exec sp_droptype N'NetworkMatchCategory'
GO

EXEC sp_addtype N'NetworkMatchCategory', N'tinyint', N'not null'
GO
