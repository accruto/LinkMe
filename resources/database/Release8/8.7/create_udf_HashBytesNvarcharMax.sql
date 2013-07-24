IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'dbo.HashBytesNvarcharMax') AND type in (N'FN', N'IF', N'TF', N'FS', N'FT'))
    DROP FUNCTION dbo.HashBytesNvarcharMax
GO

CREATE FUNCTION dbo.HashBytesNvarcharMax(@hash VARCHAR(MAX), @data NVARCHAR(MAX))
RETURNS VARBINARY(MAX)
AS
BEGIN
    IF @data IS NULL RETURN NULL

    DECLARE @buf VARBINARY(MAX), @len INT, @i INT
    SELECT @buf = HashBytes(@hash, SUBSTRING(@data, 1, 4000)),
           @len = LEN(@data),
           @i = 4001

    WHILE @i <= @len
    BEGIN
        SET @buf = @buf + HashBytes(@hash, SUBSTRING(@data, @i, 4000))
        SET @i = @i + 4000
    END

    IF @len <= 4000 RETURN @buf

    RETURN HashBytes(@hash, @buf)
END

/* Test

SELECT dbo.HashBytesNvarcharMax('SHA1', ''), HashBytes('SHA1', SUBSTRING('', 1, 4000))

*/
