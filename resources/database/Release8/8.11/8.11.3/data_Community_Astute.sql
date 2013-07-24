DECLARE @id UNIQUEIDENTIFIER
SET @id = '{D28181E5-9D57-485f-B1A4-E59769FFB779}'

IF EXISTS (SELECT * FROM dbo.SYSOBJECTS WHERE id = object_id('Community') AND  OBJECTPROPERTY(id, 'IsUserTable') = 1)
BEGIN

	-- Insert community details.

	DECLARE @name NVARCHAR(100)
	DECLARE @emailDomain NVARCHAR(100)
	DECLARE @url NVARCHAR(100)
	DECLARE @host NVARCHAR(100)
	DECLARE @flags TINYINT
	
	SET @name = 'Astute Career Network'
	SET @emailDomain = null
	SET @url = 'astutecareers'
	SET @host = 'astutecareers.linkme.com.au'
	SET @flags = 1

	IF NOT EXISTS (SELECT * FROM dbo.Community WHERE id = @id)
		INSERT
			dbo.Community ( id, name, emailDomain, url, host, flags )
		VALUES
			( @id, @name, @emailDomain, @url, @host, @flags )
	ELSE
		UPDATE
			dbo.Community
		SET
			name = @name, emailDomain = @emailDomain, url = @url, host = @host, flags = @flags
		WHERE
			id = @id

END