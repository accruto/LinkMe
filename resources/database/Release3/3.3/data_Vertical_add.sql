
IF EXISTS (SELECT * FROM dbo.SYSOBJECTS WHERE id = object_id('Vertical') AND  OBJECTPROPERTY(id, 'IsUserTable') = 1)
BEGIN

	DECLARE @id UNIQUEIDENTIFIER
	DECLARE @displayName NVARCHAR(100)
	DECLARE @emailDomain NVARCHAR(100)
	DECLARE @url NVARCHAR(100)
	
	SET @id = 'E1066ECE-958D-4496-9D0C-251EF28DC463'
	SET @displayName = 'Monash University Graduate School of Business'
	SET @emailDomain = 'student.monash.edu'
	SET @url = 'monash/gsb'

	IF NOT EXISTS (SELECT * FROM dbo.Vertical WHERE id = @id)
		INSERT
			dbo.Vertical ( id, displayName, emailDomain, url )
		VALUES
			( @id, @displayName, @emailDomain, @url )
	ELSE
		UPDATE
			dbo.Vertical
		SET
			displayName = @displayName, emailDomain = @emailDomain, url = @url
		WHERE
			id = @id

END
