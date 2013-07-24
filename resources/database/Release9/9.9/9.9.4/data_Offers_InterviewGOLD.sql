DECLARE @providerId UNIQUEIDENTIFIER
DECLARE @offeringId UNIQUEIDENTIFIER
DECLARE @categoryId UNIQUEIDENTIFIER
DECLARE @name NVARCHAR(255)

SET @providerId = '3CA5A861-34EA-478f-AB05-25FCF61E9C10'
SET @name = 'InterviewGOLD'

IF EXISTS (SELECT * FROM OfferProvider WHERE id = @providerId)
	UPDATE
		OfferProvider
	SET
		name = @name,
		enabled = 1
	WHERE
		id = @providerId
ELSE
	INSERT
		OfferProvider (id, name, enabled)
	VALUES
		(@providerId, @name, 1)

SET @offeringId = '2A21F6AA-7AC1-4a98-947D-50B79E3F4032'
SET @name = 'Online Interview Coaching'

IF EXISTS (SELECT * FROM Offering WHERE id = @offeringId)
	UPDATE
		Offering
	SET
		providerId = @providerId,
		name = @name,
		enabled = 1
	WHERE
		id = @offeringId
ELSE
	INSERT
		Offering (id, providerId, name, enabled)
	VALUES
		(@offeringId, @providerId, @name, 1)

SET @categoryId = '56C8E14B-43CA-4D97-A678-8C6FE0EEDD7A'

IF NOT EXISTS (SELECT * FROM OfferCategoryOffering WHERE categoryId = @categoryId AND offeringId = @offeringId)
	INSERT
		OfferCategoryOffering (categoryId, offeringId)
	VALUES
		(@categoryId, @offeringId)
		