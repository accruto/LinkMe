DECLARE @id UNIQUEIDENTIFIER
SET @id = '1ef359aa-a3af-42bf-a3a6-3d4a4d8691c7'

IF EXISTS (SELECT * FROM dbo.SYSOBJECTS WHERE id = object_id('Community') AND  OBJECTPROPERTY(id, 'IsUserTable') = 1)
BEGIN

	-- Insert community details.

	DECLARE @name NVARCHAR(100)
	DECLARE @emailDomain NVARCHAR(100)
	DECLARE @url NVARCHAR(100)
	DECLARE @host NVARCHAR(100)
	DECLARE @flags TINYINT
	
	SET @name = 'Business Spectator'
	SET @emailDomain = null
	SET @url = 'businessspectator'
	SET @host = 'jobs.businessspectator.com.au'
	SET @flags = 3

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