DECLARE @id UNIQUEIDENTIFIER
SET @id = 'A3A94328-BD8E-4127-AF62-66D2813EC2A9'

DECLARE @name NVARCHAR(100)
DECLARE @emailDomain NVARCHAR(100)
DECLARE @url NVARCHAR(100)
DECLARE @host NVARCHAR(100)
DECLARE @flags TINYINT
DECLARE @countryId INT

SET @name = 'The Nursing Centre Career Network'
SET @emailDomain = null
SET @url = 'thenursingcentre'
SET @host = 'thenursingcentre.com.au'
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

