DECLARE @providerId UNIQUEIDENTIFIER
SET @providerId = 'C4D236A8-9FC2-4c82-8143-C6370AEDE324'

DECLARE @syndicateName NVARCHAR(500)
SET @syndicateName = 'The CEO Syndicate Program: An exclusive network for Chief Executives'

DECLARE @transitionalName NVARCHAR(500)
SET @transitionalName = 'Transitional Leadership Program for Senior Executives & Aspiring CEOs'

-- Provider

IF NOT EXISTS (SELECT * FROM OfferProvider WHERE id = @providerId)
	INSERT
		OfferProvider (id, name, enabled)
	VALUES
		(@providerId, 'The CEO Institute', 1)
ELSE
	UPDATE
		OfferProvider
	SET
		name = 'The CEO Institute',
		enabled = 1
	WHERE
		id = @providerId

-- Offerings

DECLARE @syndicateId UNIQUEIDENTIFIER
DECLARE @transitionalId UNIQUEIDENTIFIER

SET @syndicateId = '27F06465-3C37-4e2b-A8C2-C4103744926F'
SET @transitionalId = '3EFD7903-6C8C-4803-8BF0-EE9DCB75B6E2'

IF NOT EXISTS (SELECT * FROM Offering WHERE id = @syndicateId)
	INSERT
		Offering (id, providerId, name, enabled)
	VALUES
		(@syndicateId, @providerId, @syndicateName, 1)
ELSE
	UPDATE
		Offering
	SET
		providerId = @providerId,
		name = @syndicateName,
		enabled = 1
	WHERE
		id = @syndicateId

IF NOT EXISTS (SELECT * FROM Offering WHERE id = @transitionalId)
	INSERT
		Offering (id, providerId, name, enabled)
	VALUES
		(@transitionalId, @providerId, @transitionalName, 1)
ELSE
	UPDATE
		Offering
	SET
		providerId = @providerId,
		name = @transitionalName,
		enabled = 1
	WHERE
		id = @transitionalId

-- Categories

DECLARE @grandParentId UNIQUEIDENTIFIER
DECLARE @parentId UNIQUEIDENTIFIER
DECLARE @parentName NVARCHAR(500)
DECLARE @syndicateCategoryId UNIQUEIDENTIFIER
DECLARE @transitionalCategoryId UNIQUEIDENTIFIER

SET @grandParentId = 'E69254E0-F5E6-47C2-899C-8041D77EEF8B'
SET @parentId = 'C117ECB3-F91F-4bb7-8BE5-CC90DF32F0FE'
SET @parentName = 'Short Courses in Leadership'
SET @syndicateCategoryId = '11AA89E1-A1BB-4e53-ABEC-72D95171D7D3'
SET @transitionalCategoryId = '057985F6-7254-4723-AD0A-F080C0F13FF6'

IF NOT EXISTS (SELECT * FROM OfferCategory WHERE id = @parentId)
	INSERT
		OfferCategory (id, parentId, name, enabled, deleted)
	VALUES
		(@parentId, @grandParentId, @parentName, 1, 0)
ELSE
	UPDATE
		OfferCategory
	SET
		parentId = @grandParentId,
		name = @parentName,
		enabled = 1,
		deleted = 0
	WHERE
		id = @parentId

IF NOT EXISTS (SELECT * FROM OfferCategory WHERE id = @syndicateCategoryId)
	INSERT
		OfferCategory (id, parentId, name, enabled, deleted)
	VALUES
		(@syndicateCategoryId, @parentId, @syndicateName, 1, 0)
ELSE
	UPDATE
		OfferCategory
	SET
		parentId = @parentId,
		name = @syndicateName,
		enabled = 1,
		deleted = 0
	WHERE
		id = @syndicateCategoryId

IF NOT EXISTS (SELECT * FROM OfferCategory WHERE id = @transitionalCategoryId)
	INSERT
		OfferCategory (id, parentId, name, enabled, deleted)
	VALUES
		(@transitionalCategoryId, @parentId, @transitionalName, 1, 0)
ELSE
	UPDATE
		OfferCategory
	SET
		parentId = @parentId,
		name = @transitionalName,
		enabled = 1,
		deleted = 0
	WHERE
		id = @transitionalCategoryId

IF NOT EXISTS (SELECT * FROM OfferCategoryOffering WHERE categoryId = @syndicateCategoryId AND offeringId = @syndicateId)
	INSERT
		OfferCategoryOffering (categoryId, offeringId)
	VALUES
		(@syndicateCategoryId, @syndicateId)

IF NOT EXISTS (SELECT * FROM OfferCategoryOffering WHERE categoryId = @transitionalCategoryId AND offeringId = @transitionalId)
	INSERT
		OfferCategoryOffering (categoryId, offeringId)
	VALUES
		(@transitionalCategoryId, @transitionalId)

SET @grandParentId = '23662854-51CF-4824-AB3D-8BC72D46E5CB'
SET @parentId = '159DAC62-BB59-42c6-892B-34F63D457183'
SET @parentName = 'Professional'
SET @syndicateCategoryId = '24610A2D-78B1-449c-A170-301D0FB64BF4'
SET @transitionalCategoryId = 'A089FC99-BDC0-4073-82BA-BF30D5BC1044'

IF NOT EXISTS (SELECT * FROM OfferCategory WHERE id = @parentId)
	INSERT
		OfferCategory (id, parentId, name, enabled, deleted)
	VALUES
		(@parentId, @grandParentId, @parentName, 1, 0)
ELSE
	UPDATE
		OfferCategory
	SET
		parentId = @grandParentId,
		name = @parentName,
		enabled = 1,
		deleted = 0
	WHERE
		id = @parentId

IF NOT EXISTS (SELECT * FROM OfferCategory WHERE id = @syndicateCategoryId)
	INSERT
		OfferCategory (id, parentId, name, enabled, deleted)
	VALUES
		(@syndicateCategoryId, @parentId, @syndicateName, 1, 0)
ELSE
	UPDATE
		OfferCategory
	SET
		parentId = @parentId,
		name = @syndicateName,
		enabled = 1,
		deleted = 0
	WHERE
		id = @syndicateCategoryId

IF NOT EXISTS (SELECT * FROM OfferCategory WHERE id = @transitionalCategoryId)
	INSERT
		OfferCategory (id, parentId, name, enabled, deleted)
	VALUES
		(@transitionalCategoryId, @parentId, @transitionalName, 1, 0)
ELSE
	UPDATE
		OfferCategory
	SET
		parentId = @parentId,
		name = @transitionalName,
		enabled = 1,
		deleted = 0
	WHERE
		id = @transitionalCategoryId

IF NOT EXISTS (SELECT * FROM OfferCategoryOffering WHERE categoryId = @syndicateCategoryId AND offeringId = @syndicateId)
	INSERT
		OfferCategoryOffering (categoryId, offeringId)
	VALUES
		(@syndicateCategoryId, @syndicateId)

IF NOT EXISTS (SELECT * FROM OfferCategoryOffering WHERE categoryId = @transitionalCategoryId AND offeringId = @transitionalId)
	INSERT
		OfferCategoryOffering (categoryId, offeringId)
	VALUES
		(@transitionalCategoryId, @transitionalId)

-- Categories

SET @grandParentId = 'A0D6B83C-B185-46FA-8BD5-E76FAC01D906'
SET @parentId = 'C461079F-AAD6-406f-8AEF-A56B32F5B588'
SET @parentName = 'Leadership Networks'
SET @syndicateCategoryId = 'CA6A71F4-89F5-4d69-AF77-A353B48F694B'
SET @transitionalCategoryId = '80EB8B89-009A-4def-A3BC-1574238147A2'

IF NOT EXISTS (SELECT * FROM OfferCategory WHERE id = @parentId)
	INSERT
		OfferCategory (id, parentId, name, enabled, deleted)
	VALUES
		(@parentId, @grandParentId, @parentName, 1, 0)
ELSE
	UPDATE
		OfferCategory
	SET
		parentId = @grandParentId,
		name = @parentName,
		enabled = 1,
		deleted = 0
	WHERE
		id = @parentId

IF NOT EXISTS (SELECT * FROM OfferCategory WHERE id = @syndicateCategoryId)
	INSERT
		OfferCategory (id, parentId, name, enabled, deleted)
	VALUES
		(@syndicateCategoryId, @parentId, @syndicateName, 1, 0)
ELSE
	UPDATE
		OfferCategory
	SET
		parentId = @parentId,
		name = @syndicateName,
		enabled = 1,
		deleted = 0
	WHERE
		id = @syndicateCategoryId

IF NOT EXISTS (SELECT * FROM OfferCategory WHERE id = @transitionalCategoryId)
	INSERT
		OfferCategory (id, parentId, name, enabled, deleted)
	VALUES
		(@transitionalCategoryId, @parentId, @transitionalName, 1, 0)
ELSE
	UPDATE
		OfferCategory
	SET
		parentId = @parentId,
		name = @transitionalName,
		enabled = 1,
		deleted = 0
	WHERE
		id = @transitionalCategoryId

IF NOT EXISTS (SELECT * FROM OfferCategoryOffering WHERE categoryId = @syndicateCategoryId AND offeringId = @syndicateId)
	INSERT
		OfferCategoryOffering (categoryId, offeringId)
	VALUES
		(@syndicateCategoryId, @syndicateId)

IF NOT EXISTS (SELECT * FROM OfferCategoryOffering WHERE categoryId = @transitionalCategoryId AND offeringId = @transitionalId)
	INSERT
		OfferCategoryOffering (categoryId, offeringId)
	VALUES
		(@transitionalCategoryId, @transitionalId)

