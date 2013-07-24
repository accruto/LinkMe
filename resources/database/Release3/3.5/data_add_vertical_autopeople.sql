
DECLARE @id UNIQUEIDENTIFIER
SET @id = 'A59F349A-A896-4abb-A56F-AFD3B4B2C26B'

IF EXISTS (SELECT * FROM dbo.SYSOBJECTS WHERE id = object_id('Vertical') AND  OBJECTPROPERTY(id, 'IsUserTable') = 1)
BEGIN

	-- Insert vertical details.

	DECLARE @displayName NVARCHAR(100)
	DECLARE @emailDomain NVARCHAR(100)
	DECLARE @url NVARCHAR(100)
	DECLARE @host NVARCHAR(100)
	
	SET @displayName = 'Autopeople'
	SET @emailDomain = null
	SET @url = 'autopeople'
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

	SET @brandControl = NULL
	SET @brandFooterControl = NULL
	SET @candidateImageUrl = NULL
	
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

IF EXISTS (SELECT * FROM dbo.SYSOBJECTS WHERE id = object_id('VerticalNav') AND  OBJECTPROPERTY(id, 'IsUserTable') = 1)
BEGIN

	-- Insert vertical navs.

	DECLARE @nav NVARCHAR(100)
	DECLARE @newNav NVARCHAR(100)

	SET @nav = 'employers'
	SET @newNav = 'employers-autopeople'
	
	IF NOT EXISTS (SELECT * FROM VerticalNav WHERE verticalId = @id AND nav = @nav)
		INSERT
			VerticalNav (verticalId, nav, newNav)
		VALUES
			(@id, @nav, @newNav)
	ELSE
		UPDATE
			VerticalNav
		SET
			newNav = @newNav
		WHERE
			verticalId = @id
			AND nav = @nav

	SET @nav = 'employers-header'
	SET @newNav = 'employers-autopeople-header'
	
	IF NOT EXISTS (SELECT * FROM VerticalNav WHERE verticalId = @id AND nav = @nav)
		INSERT
			VerticalNav (verticalId, nav, newNav)
		VALUES
			(@id, @nav, @newNav)
	ELSE
		UPDATE
			VerticalNav
		SET
			newNav = @newNav
		WHERE
			verticalId = @id
			AND nav = @nav

	SET @nav = 'footer'
	SET @newNav = 'autopeople-footer'
	
	IF NOT EXISTS (SELECT * FROM VerticalNav WHERE verticalId = @id AND nav = @nav)
		INSERT
			VerticalNav (verticalId, nav, newNav)
		VALUES
			(@id, @nav, @newNav)
	ELSE
		UPDATE
			VerticalNav
		SET
			newNav = @newNav
		WHERE
			verticalId = @id
			AND nav = @nav

END

