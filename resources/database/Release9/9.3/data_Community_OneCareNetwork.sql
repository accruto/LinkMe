DECLARE @id UNIQUEIDENTIFIER
SET @id = 'C08B537F-6171-4811-905D-CFFB05DB6E9D'

DECLARE @name NVARCHAR(100)
DECLARE @emailDomain NVARCHAR(100)
DECLARE @url NVARCHAR(100)
DECLARE @host NVARCHAR(100)
DECLARE @flags TINYINT
DECLARE @countryId INT

SET @name = '1CN People'
SET @emailDomain = null
SET @url = 'onecarenetwork'
SET @host = 'onecarenetwork.linkme.com.au'
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

