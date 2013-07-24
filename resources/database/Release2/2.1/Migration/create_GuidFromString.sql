if exists (select * from dbo.sysobjects where id = object_id(N'dbo.GuidFromString') and xtype in (N'FN', N'IF', N'TF'))
drop function dbo.GuidFromString
GO

CREATE FUNCTION dbo.GuidFromString(@guidAsString AS VARCHAR(50))
RETURNS UNIQUEIDENTIFIER AS  
BEGIN 
	DECLARE @temp AS VARCHAR(50)
	
	IF (CHARINDEX('-', @guidAsString) = 0)
		SET @guidAsString = SUBSTRING(@guidAsString, 1, 8) + '-' + SUBSTRING(@guidAsString, 9, 4) + '-'
			+ SUBSTRING(@guidAsString, 13, 4) + '-' + SUBSTRING(@guidAsString, 17, 4) + '-' + SUBSTRING(@guidAsString, 21, 12)

	RETURN CONVERT(uniqueidentifier, @guidAsString)
END
GO