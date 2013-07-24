
IF EXISTS (SELECT * FROM dbo.SYSOBJECTS WHERE id = object_id('Vertical') AND  OBJECTPROPERTY(id, 'IsUserTable') = 1)
BEGIN

	DECLARE @id UNIQUEIDENTIFIER
	DECLARE @displayName NVARCHAR(100)
	DECLARE @emailDomain NVARCHAR(100)
	DECLARE @url NVARCHAR(100)
	DECLARE @host NVARCHAR(100)
	
	SET @id = 'DC9284A4-5CE4-4A13-A2C4-D1E6A6A49A1C'
	SET @displayName = 'ITWire'
	SET @emailDomain = null
	SET @url = 'itwire'
	SET @host = 'myprofile.itwire.com'

	IF NOT EXISTS (SELECT * FROM dbo.Vertical WHERE id = @id)
		INSERT
			dbo.Vertical ( id, displayName, emailDomain, url, host )
		VALUES
			( @id, @displayName, @emailDomain, @url, @host )
	ELSE
		UPDATE
			dbo.Vertical
		SET
			displayName = @displayName, emailDomain = @emailDomain, url = @url, host = @host
		WHERE
			id = @id

END
