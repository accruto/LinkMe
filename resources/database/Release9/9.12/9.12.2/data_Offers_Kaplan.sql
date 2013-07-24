-- Categories
-------------

DECLARE @grandParentId UNIQUEIDENTIFIER
DECLARE @parentId UNIQUEIDENTIFIER
DECLARE @categoryId UNIQUEIDENTIFIER
DECLARE @categoryName NVARCHAR(500)
DECLARE @offeringId UNIQUEIDENTIFIER

-- Alternative Careers

SET @grandParentId = '385188B4-D205-46b5-AA18-6170B9623407'
SET @categoryName = 'Alternative Careers'

IF NOT EXISTS (SELECT * FROM OfferCategory WHERE id = @grandParentId)
	INSERT
		OfferCategory (id, parentId, name, enabled, deleted)
	VALUES
		(@grandParentId, NULL, @categoryName, 1, 0)
ELSE
	UPDATE
		OfferCategory
	SET
		parentId = NULL,
		name = @categoryName,
		enabled = 1,
		deleted = 0
	WHERE
		id = @grandParentId

-- Financial Services courses

SET @parentId = '40BAB544-26AD-469d-A319-64DB914F8F76'
SET @categoryName = 'Financial Services Courses'

IF NOT EXISTS (SELECT * FROM OfferCategory WHERE id = @parentId)
	INSERT
		OfferCategory (id, parentId, name, enabled, deleted)
	VALUES
		(@parentId, @grandParentId, @categoryName, 1, 0)
ELSE
	UPDATE
		OfferCategory
	SET
		parentId = @grandParentId,
		name = @categoryName,
		enabled = 1,
		deleted = 0
	WHERE
		id = @parentId

SET @categoryId = 'F71EB9C8-DA47-4ab7-A06D-3F4C9730B651'
SET @categoryName = 'Diploma of Financial Services (Financial Planning)'

IF NOT EXISTS (SELECT * FROM OfferCategory WHERE id = @categoryId)
	INSERT
		OfferCategory (id, parentId, name, enabled, deleted)
	VALUES
		(@categoryId, @parentId, @categoryName, 1, 0)
ELSE
	UPDATE
		OfferCategory
	SET
		parentId = @parentId,
		name = @categoryName,
		enabled = 1,
		deleted = 0
	WHERE
		id = @categoryId

SET @offeringId = 'AC839BC2-2BBD-4710-9A52-933021020971'
IF NOT EXISTS (SELECT * FROM OfferCategoryOffering WHERE categoryId = @categoryId AND offeringId = @offeringId)
	INSERT
		OfferCategoryOffering (categoryId, offeringId)
	VALUES
		(@categoryId, @offeringId)


SET @categoryId = '7249FD4A-DB53-44af-8C68-E39CCC2F2450'
SET @categoryName = 'RG 146 Compliance'

IF NOT EXISTS (SELECT * FROM OfferCategory WHERE id = @categoryId)
	INSERT
		OfferCategory (id, parentId, name, enabled, deleted)
	VALUES
		(@categoryId, @parentId, @categoryName, 1, 0)
ELSE
	UPDATE
		OfferCategory
	SET
		parentId = @parentId,
		name = @categoryName,
		enabled = 1,
		deleted = 0
	WHERE
		id = @categoryId

SET @offeringId = '99FF1CB4-59C3-4cb1-B8D2-A188C2B41693'
IF NOT EXISTS (SELECT * FROM OfferCategoryOffering WHERE categoryId = @categoryId AND offeringId = @offeringId)
	INSERT
		OfferCategoryOffering (categoryId, offeringId)
	VALUES
		(@categoryId, @offeringId)


SET @categoryId = 'FB834401-D50F-475b-A791-73689363168E'
SET @categoryName = 'Mortgage/Finance Broking'

IF NOT EXISTS (SELECT * FROM OfferCategory WHERE id = @categoryId)
	INSERT
		OfferCategory (id, parentId, name, enabled, deleted)
	VALUES
		(@categoryId, @parentId, @categoryName, 1, 0)
ELSE
	UPDATE
		OfferCategory
	SET
		parentId = @parentId,
		name = @categoryName,
		enabled = 1,
		deleted = 0
	WHERE
		id = @categoryId

SET @offeringId = 'FD007234-A6E5-4c27-BF2C-55252EC35DC2'
IF NOT EXISTS (SELECT * FROM OfferCategoryOffering WHERE categoryId = @categoryId AND offeringId = @offeringId)
	INSERT
		OfferCategoryOffering (categoryId, offeringId)
	VALUES
		(@categoryId, @offeringId)


-- Real Estate

SET @parentId = '5FBA0C70-1AD6-49fa-BBEB-DFBAE574D91A'
SET @categoryName = 'Real Estate'

IF NOT EXISTS (SELECT * FROM OfferCategory WHERE id = @parentId)
	INSERT
		OfferCategory (id, parentId, name, enabled, deleted)
	VALUES
		(@parentId, @grandParentId, @categoryName, 1, 0)
ELSE
	UPDATE
		OfferCategory
	SET
		parentId = @grandParentId,
		name = @categoryName,
		enabled = 1,
		deleted = 0
	WHERE
		id = @parentId

SET @categoryId = 'A9DB0AFF-7EB1-48a5-841B-DE1A978C277F'
SET @categoryName = 'Registration Program (entry level no experience required)'

IF NOT EXISTS (SELECT * FROM OfferCategory WHERE id = @categoryId)
	INSERT
		OfferCategory (id, parentId, name, enabled, deleted)
	VALUES
		(@categoryId, @parentId, @categoryName, 1, 0)
ELSE
	UPDATE
		OfferCategory
	SET
		parentId = @parentId,
		name = @categoryName,
		enabled = 1,
		deleted = 0
	WHERE
		id = @categoryId

SET @offeringId = '3EEF0711-7C60-4c5c-AB42-2320261AA605'
IF NOT EXISTS (SELECT * FROM OfferCategoryOffering WHERE categoryId = @categoryId AND offeringId = @offeringId)
	INSERT
		OfferCategoryOffering (categoryId, offeringId)
	VALUES
		(@categoryId, @offeringId)

