if NOT EXISTS(SELECT * FROM dbo.systypes WHERE name = 'DiscussionFlags')
	EXEC dbo.sp_addtype N'DiscussionFlags', N'tinyint',N'not null'
