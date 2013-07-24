ALTER TABLE dbo.JobAd
ADD isFeatured BIT NULL
GO

DECLARE @temp AS TABLE (id UNIQUEIDENTIFIER)
DECLARE @count INT
DECLARE @total INT
SET @count = 1

WHILE @count > 0
BEGIN
	INSERT
		@temp
	SELECT
		TOP 1000 id
	FROM
		dbo.JobAd
	WHERE
		isFeatured IS NULL

	UPDATE
		dbo.JobAd
	SET
		isFeatured = 0
	WHERE
		id IN (SELECT id FROM @temp)

	DELETE @temp

	SELECT @total = COUNT(*) FROM JobAd WHERE isFeatured IS NULL
	PRINT @total

	SET @count = @count - 1
END
GO

SELECT COUNT(*) FROM JobAd WHERE isFeatured IS NULL

ALTER TABLE dbo.JobAd
ALTER COLUMN isFeatured BIT NOT NULL
GO

