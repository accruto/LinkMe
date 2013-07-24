if NOT EXISTS(SELECT * FROM dbo.systypes WHERE name = 'GroupCategory')
	EXEC dbo.sp_addtype N'GroupCategory', N'tinyint',N'not null'

if NOT EXISTS(SELECT * FROM dbo.systypes WHERE name = 'GroupAccess')
        EXEC dbo.sp_addtype N'GroupAccess', N'tinyint',N'not null'

if NOT EXISTS(SELECT * FROM dbo.systypes WHERE name = 'GroupFlags')
	EXEC dbo.sp_addtype N'GroupFlags', N'tinyint',N'not null'

if NOT EXISTS(SELECT * FROM dbo.systypes WHERE name = 'GroupJoinRestriction')
	EXEC dbo.sp_addtype N'GroupJoinRestriction', N'tinyint',N'not null'

if NOT EXISTS(SELECT * FROM dbo.systypes WHERE name = 'GroupMembershipFlags')
        EXEC dbo.sp_addtype N'GroupMembershipFlags', N'tinyint',N'not null'
