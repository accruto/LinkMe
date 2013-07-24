
DECLARE @verticalId UNIQUEIDENTIFIER
DECLARE @brandControl NVARCHAR(100)
DECLARE @brandFooterControl NVARCHAR(100)
DECLARE @candidateImageUrl NVARCHAR(100)

-- Monash

SELECT @verticalId = id FROM Vertical WHERE displayName = 'Monash University Graduate School of Business'
SET @brandControl = '~/verticals/monash/gsb/controls/Brand.ascx'
SET @brandFooterControl = NULL
SET @candidateImageUrl = '~/verticals/monash/gsb/images/gsb-logo-2line-very-small.jpg'

IF NOT EXISTS (SELECT * FROM VerticalContent WHERE verticalId = @verticalId)
	INSERT
		VerticalContent (verticalId, brandControl, brandFooterControl, candidateImageUrl)
	VALUES
		(@verticalId, @brandControl, @brandFooterControl, @candidateImageUrl)
ELSE
	UPDATE
		VerticalContent
	SET
		brandControl = @brandControl,
		brandFooterControl = @brandFooterControl,
		candidateImageUrl = @candidateImageUrl
	WHERE
		verticalId = @verticalId

-- ITWire

SELECT @verticalId = id FROM Vertical WHERE displayName = 'ITWire'
SET @brandControl = '~/verticals/itwire/controls/Header.ascx'
SET @brandFooterControl = '~/verticals/itwire/controls/Footer.ascx'
SET @candidateImageUrl = '~/verticals/itwire/images/candidate-logo.jpg'

IF NOT EXISTS (SELECT * FROM VerticalContent WHERE verticalId = @verticalId)
	INSERT
		VerticalContent (verticalId, brandControl, brandFooterControl, candidateImageUrl)
	VALUES
		(@verticalId, @brandControl, @brandFooterControl, @candidateImageUrl)
ELSE
	UPDATE
		VerticalContent
	SET
		brandControl = @brandControl,
		brandFooterControl = @brandFooterControl,
		candidateImageUrl = @candidateImageUrl
	WHERE
		verticalId = @verticalId

-- GolfJobs

SELECT @verticalId = id FROM Vertical WHERE displayName = 'golfjobs.com.au'
SET @brandControl = '~/verticals/pga/controls/Header.ascx'
SET @brandFooterControl = NULL
SET @candidateImageUrl = '~/verticals/pga/images/candidate-logo.jpg'

IF NOT EXISTS (SELECT * FROM VerticalContent WHERE verticalId = @verticalId)
	INSERT
		VerticalContent (verticalId, brandControl, brandFooterControl, candidateImageUrl)
	VALUES
		(@verticalId, @brandControl, @brandFooterControl, @candidateImageUrl)
ELSE
	UPDATE
		VerticalContent
	SET
		brandControl = @brandControl,
		brandFooterControl = @brandFooterControl,
		candidateImageUrl = @candidateImageUrl
	WHERE
		verticalId = @verticalId

