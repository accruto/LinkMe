
DECLARE @id UNIQUEIDENTIFIER
SET @id = '168174BC-5F70-4617-83BB-7B04725CAE49'

IF EXISTS (SELECT * FROM dbo.SYSOBJECTS WHERE id = object_id('Community') AND  OBJECTPROPERTY(id, 'IsUserTable') = 1)
BEGIN

	-- Insert community details.

	DECLARE @name NVARCHAR(100)
	DECLARE @emailDomain NVARCHAR(100)
	DECLARE @url NVARCHAR(100)
	DECLARE @host NVARCHAR(100)
	DECLARE @flags TINYINT
	
	SET @name = 'Australian Institute of Marine and Power Engineers'
	SET @emailDomain = null
	SET @url = 'aimpe'
	SET @host = null
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

