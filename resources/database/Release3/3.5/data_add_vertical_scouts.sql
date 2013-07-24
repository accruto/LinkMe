
DECLARE @id UNIQUEIDENTIFIER
SET @id = '31C53EA8-BF66-42a1-A024-25A21069EDD3'

IF EXISTS (SELECT * FROM dbo.SYSOBJECTS WHERE id = object_id('Vertical') AND  OBJECTPROPERTY(id, 'IsUserTable') = 1)
BEGIN

	-- Insert vertical details.

	DECLARE @displayName NVARCHAR(100)
	DECLARE @emailDomain NVARCHAR(100)
	DECLARE @url NVARCHAR(100)
	DECLARE @host NVARCHAR(100)
	
	SET @displayName = 'Scouts Australia'
	SET @emailDomain = null
	SET @url = 'scouts'
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

	SET @brandControl = '~/verticals/scouts/controls/Header.ascx'
	SET @brandFooterControl = NULL
	SET @candidateImageUrl = '~/verticals/scouts/images/candidate-logo.jpg'
	
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

