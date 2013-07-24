
DECLARE @id UNIQUEIDENTIFIER
SET @id = 'D6D6819C-5B03-4ac5-B5EE-597709E1D45B'

IF EXISTS (SELECT * FROM dbo.SYSOBJECTS WHERE id = object_id('Vertical') AND  OBJECTPROPERTY(id, 'IsUserTable') = 1)
BEGIN

	-- Insert vertical details.

	DECLARE @displayName NVARCHAR(100)
	DECLARE @emailDomain NVARCHAR(100)
	DECLARE @url NVARCHAR(100)
	DECLARE @host NVARCHAR(100)
	
	SET @displayName = 'RCSA Australia & New Zealand'
	SET @emailDomain = null
	SET @url = 'rcsa'
	SET @host = null

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

IF EXISTS (SELECT * FROM dbo.SYSOBJECTS WHERE id = object_id('VerticalContent') AND  OBJECTPROPERTY(id, 'IsUserTable') = 1)
BEGIN

	-- Insert vertical brand details.

	DECLARE @brandControl NVARCHAR(100)
	DECLARE @brandFooterControl NVARCHAR(100)
	DECLARE @candidateImageUrl NVARCHAR(100)

	SET @brandControl = '~/verticals/rcsa/controls/Header.ascx'
	SET @brandFooterControl = NULL
	SET @candidateImageUrl = '~/verticals/rcsa/images/logo.jpg'
	
	IF NOT EXISTS (SELECT * FROM VerticalContent WHERE verticalId = @id)
		INSERT
			VerticalContent (verticalId, brandControl, brandFooterControl, candidateImageUrl)
		VALUES
			(@id, @brandControl, @brandFooterControl, @candidateImageUrl)
	ELSE
		UPDATE
			VerticalContent
		SET
			brandControl = @brandControl,
			brandFooterControl = @brandFooterControl,
			candidateImageUrl = @candidateImageUrl
		WHERE
			verticalId = @id

END

