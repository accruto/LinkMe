if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[ToPascalCase]') and xtype in (N'FN', N'IF', N'TF'))
drop function [dbo].[ToPascalCase]
GO

CREATE FUNCTION dbo.ToPascalCase(@input AS NVARCHAR(4000))
RETURNS NVARCHAR(4000) AS  
BEGIN
	DECLARE @length INT

	SET @length = DATALENGTH(@input) / 2
	IF (@length = 0)
		RETURN ''

	-- Make the first letter uppercase and everything else lowercase.

	DECLARE @pascal NVARCHAR(4000)
	SET @pascal = UPPER(LEFT(@input, 1)) + LOWER(SUBSTRING(@input, 2, @length - 1))

	-- Make any letter that follows a non-letter uppercase.

	DECLARE @index INT
	SET @index = PATINDEX('%[^a-zA-Z]%', @pascal)

	WHILE (@index <> 0 AND @index < @length)
	BEGIN
		SET @pascal = LEFT(@pascal, @index) + UPPER(SUBSTRING(@pascal, @index + 1, 1))
			+ SUBSTRING(@pascal, @index + 2, @length - @index + 2)

		DECLARE @tempIndex INT
		SET @tempIndex = PATINDEX('%[^a-zA-Z]%', RIGHT(@pascal, @length - @index))
		IF @tempIndex = 0
			SET @index = 0
		ELSE
			SET @index = @tempIndex + @index
	END

	RETURN @pascal
END
GO
