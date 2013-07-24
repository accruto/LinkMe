
IF NOT EXISTS(SELECT * FROM dbo.systypes WHERE name = 'CommunityFlags')
	EXEC dbo.sp_addtype N'CommunityFlags', N'tinyint', N'not null'
GO

IF NOT EXISTS (SELECT * FROM syscolumns WHERE id = OBJECT_ID('Community') AND NAME = 'flags')
BEGIN

	ALTER TABLE dbo.Community
	ADD flags CommunityFlags NULL

END
	
GO
