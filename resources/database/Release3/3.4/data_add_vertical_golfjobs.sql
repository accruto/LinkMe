
GO

IF EXISTS (SELECT * FROM dbo.SYSOBJECTS WHERE id = object_id('Vertical') AND  OBJECTPROPERTY(id, 'IsUserTable') = 1)
BEGIN

	DECLARE @id UNIQUEIDENTIFIER
	DECLARE @displayName NVARCHAR(100)
	DECLARE @emailDomain NVARCHAR(100)
	DECLARE @url NVARCHAR(100)
	DECLARE @host NVARCHAR(100)
	
	SET @id = '4E09AB28-27D5-41D6-9AA3-4A87B84413F6'
	SET @displayName = 'golfjobs.com.au'
	SET @emailDomain = null
	SET @url = 'golfjobs'
	SET @host = 'www.golfjobs.com.au'

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
