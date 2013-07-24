IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Numbers]') AND type in (N'U'))
DROP TABLE [dbo].[Numbers]
GO

CREATE TABLE Numbers
(
	number INT NOT NULL,
    CONSTRAINT PK_Numbers PRIMARY KEY CLUSTERED (Number ASC)
)

DECLARE @x INT
SET @x = 0

WHILE @x < 8000
BEGIN
	SET @x = @x + 1
    INSERT INTO Numbers VALUES (@x)
END
GO

--SELECT * FROM Numbers
--SELECT * FROM dbo.Split(',','1,12,123,1234,54321,6,A,*,|||,,,,B')

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SplitGuids]') AND type in (N'FN', N'IF', N'TF', N'FS', N'FT'))
DROP FUNCTION [dbo].[SplitGuids]
GO

CREATE FUNCTION [dbo].[SplitGuids]
(
	@separator CHAR(1),
	@s VARCHAR(8000)
)
RETURNS @guids TABLE (guid UNIQUEIDENTIFIER)

AS
BEGIN

INSERT
	@guids (guid)
SELECT
	guid
FROM
(
	SELECT
		LTRIM(RTRIM(SUBSTRING(s, number + 1, CHARINDEX(@separator, s, number + 1) - number - 1))) AS guid
	FROM
	(
		SELECT @separator + @s + @separator AS s
	) AS T1
	INNER JOIN
		Numbers AS n ON n.Number < LEN(T1.s)
	WHERE
		SUBSTRING(s, number, 1) = @separator
) AS T2
WHERE
	guid IS NOT NULL AND guid != ''

RETURN

END

