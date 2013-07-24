DECLARE @id UNIQUEIDENTIFIER
SET @id = '5DC10D39-0ACA-434d-8525-55D5A7F68F23'

DECLARE @name NVARCHAR(100)
DECLARE @emailDomain NVARCHAR(100)
DECLARE @url NVARCHAR(100)
DECLARE @host NVARCHAR(100)
DECLARE @flags TINYINT
DECLARE @countryId INT

SET @name = 'Society of Automotive Engineers'
SET @emailDomain = null
SET @url = 'sae'
SET @host = 'careerdrive.com.au'
SET @flags = 1
SET @countryId = NULL

-- Vertical

IF NOT EXISTS (SELECT * FROM dbo.Vertical WHERE id = @id)
	INSERT
		dbo.Vertical ( id, name, url, host, countryId )
	VALUES
		( @id, @name, @url, @host, @countryId )
ELSE
	UPDATE
		dbo.Vertical
	SET
		name = @name, url = @url, host = @host, countryId = @countryId
	WHERE
		id = @id

-- Community

IF NOT EXISTS (SELECT * FROM dbo.Community WHERE id = @id)
	INSERT
		dbo.Community ( id, name, emailDomain, flags )
	VALUES
		( @id, @name, @emailDomain, @flags )
ELSE
	UPDATE
		dbo.Community
	SET
		name = @name, emailDomain = @emailDomain, flags = @flags
	WHERE
		id = @id

