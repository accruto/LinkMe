DECLARE @id TINYINT
DECLARE @displayName VARCHAR(50)

SET @id = 12
SET @displayName = 'Trading Post print ad'

IF NOT EXISTS (SELECT * FROM dbo.ReferralSource WHERE [id] = @id)
BEGIN
	INSERT INTO dbo.ReferralSource([id], displayName)
	VALUES (@id, @displayName)
END
ELSE
BEGIN
	UPDATE dbo.ReferralSource
	SET displayName = @displayName
	WHERE [id] = @id
END
