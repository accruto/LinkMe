DECLARE @providerId UNIQUEIDENTIFIER
SET @providerId = '8882EBEF-0E85-4E7E-BCE3-451BFF9BEB56'

DECLARE @parentCategoryId UNIQUEIDENTIFIER
SET @parentCategoryId = '016A322C-77F2-4B6D-B600-5E8629202E2B'

-- Update the name

UPDATE
	OfferCategory
SET
	name = 'Short Courses (Paid)'
WHERE
	id = @parentCategoryId

-- Disable the old categories

UPDATE
	OfferCategory
SET
	enabled = 0
WHERE
	parentId = @parentCategoryId

-- Create the categories

DECLARE @name NVARCHAR(255)
SET @name = 'Personal Development: Goal Setting, Problem Solving and Peak Performance'

DECLARE @categoryId UNIQUEIDENTIFIER
SET @categoryId = '{EF0D721A-777B-4ec9-960A-D821553999C0}'

DECLARE @offeringId UNIQUEIDENTIFIER
SET @offeringId = '{200BD430-FC6F-4d71-96EA-535D54CA8BA1}'

IF EXISTS (SELECT * FROM OfferCategory WHERE id = @categoryId)
	UPDATE
		OfferCategory
	SET
		parentId = @parentCategoryId,
		name = @name,
		enabled = 1,
		deleted = 0
	WHERE
		id = @categoryId
ELSE
	INSERT
		OfferCategory (id, parentId, name, enabled, deleted)
	VALUES
		(@categoryId, @parentCategoryId, @name, 1, 0)

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

IF NOT EXISTS (SELECT * FROM OfferCategoryOffering WHERE categoryId = @categoryId AND offeringId = @offeringId)
	INSERT
		OfferCategoryOffering (categoryId, offeringId)
	VALUES
		(@categoryId, @offeringId)

--    - Leadership Development for Managers

SET @name = 'Leadership Development for Managers'
SET @categoryId = '{36862CAA-F207-42a6-BF86-8B036DB3ACE2}'
SET @offeringId = '{D24BA041-BAC2-4443-9A97-86EFB0D47573}'

IF EXISTS (SELECT * FROM OfferCategory WHERE id = @categoryId)
	UPDATE
		OfferCategory
	SET
		parentId = @parentCategoryId,
		name = @name,
		enabled = 1,
		deleted = 0
	WHERE
		id = @categoryId
ELSE
	INSERT
		OfferCategory (id, parentId, name, enabled, deleted)
	VALUES
		(@categoryId, @parentCategoryId, @name, 1, 0)

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

IF NOT EXISTS (SELECT * FROM OfferCategoryOffering WHERE categoryId = @categoryId AND offeringId = @offeringId)
	INSERT
		OfferCategoryOffering (categoryId, offeringId)
	VALUES
		(@categoryId, @offeringId)

--    - Advanced Selling Skills for Sales Professionals

SET @name = 'Advanced Selling Skills for Sales Professionals'
SET @categoryId = '{347EDB26-0723-4416-BDD3-E3B2A9BAAB2E}'
SET @offeringId = '{FD329E62-CDB9-4415-B14A-564EE7A3B37B}'

IF EXISTS (SELECT * FROM OfferCategory WHERE id = @categoryId)
	UPDATE
		OfferCategory
	SET
		parentId = @parentCategoryId,
		name = @name,
		enabled = 1,
		deleted = 0
	WHERE
		id = @categoryId
ELSE
	INSERT
		OfferCategory (id, parentId, name, enabled, deleted)
	VALUES
		(@categoryId, @parentCategoryId, @name, 1, 0)

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

IF NOT EXISTS (SELECT * FROM OfferCategoryOffering WHERE categoryId = @categoryId AND offeringId = @offeringId)
	INSERT
		OfferCategoryOffering (categoryId, offeringId)
	VALUES
		(@categoryId, @offeringId)

--    - Superior Sales Management for Sales Managers

SET @name = 'Superior Sales Management for Sales Managers'
SET @categoryId = '{3B7913F5-99C4-4066-B98A-65D34EFD8B11}'
SET @offeringId = '{E1006FE2-5BC8-44d3-84D7-6EB4ADA335AE}'

IF EXISTS (SELECT * FROM OfferCategory WHERE id = @categoryId)
	UPDATE
		OfferCategory
	SET
		parentId = @parentCategoryId,
		name = @name,
		enabled = 1,
		deleted = 0
	WHERE
		id = @categoryId
ELSE
	INSERT
		OfferCategory (id, parentId, name, enabled, deleted)
	VALUES
		(@categoryId, @parentCategoryId, @name, 1, 0)

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

IF NOT EXISTS (SELECT * FROM OfferCategoryOffering WHERE categoryId = @categoryId AND offeringId = @offeringId)
	INSERT
		OfferCategoryOffering (categoryId, offeringId)
	VALUES
		(@categoryId, @offeringId)




