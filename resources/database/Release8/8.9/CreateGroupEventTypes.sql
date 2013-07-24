if NOT EXISTS(SELECT * FROM dbo.systypes WHERE name = 'GroupEventFlags')
	EXEC dbo.sp_addtype N'GroupEventFlags', N'tinyint',N'not null'

if NOT EXISTS(SELECT * FROM dbo.systypes WHERE name = 'GroupEventAttendanceStatus')
	EXEC dbo.sp_addtype N'GroupEventAttendanceStatus', N'tinyint',N'not null'

if NOT EXISTS(SELECT * FROM dbo.systypes WHERE name = 'GroupEventAttendance')
	EXEC dbo.sp_addtype N'GroupEventAttendance', N'tinyint',N'not null'
