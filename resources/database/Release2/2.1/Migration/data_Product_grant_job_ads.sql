-- WARNING: Do not run this script more than once. 20 job ad credits will be granted to employers each time
-- it is run.

DECLARE @intUnlimitedQuantity INT
DECLARE @expiryOneYear DATETIME
DECLARE @intQuantityForOneMonth INT
DECLARE @expiryOneMonth DATETIME
DECLARE @jobAdDefId UNIQUEIDENTIFIER
DECLARE @contactCreditDefId UNIQUEIDENTIFIER

SET @intUnlimitedQuantity = 2000000000
SET @expiryOneYear = DATEADD(Year, 1, GETDATE())
SET @intQuantityForOneMonth = 20
SET @expiryOneMonth = DATEADD(Month, 1, GETDATE())

-- Get the product definition IDs for JobAd and ContactCredit.

SELECT @jobAdDefId = id
FROM linkme_owner.ProductDefinition
WHERE name = 'JobAd'

IF @jobAdDefId IS NULL
	RAISERROR('Failed to find a product definition for "JobAd".', 18, 1)

SELECT @contactCreditDefId = id
FROM linkme_owner.ProductDefinition
WHERE name = 'ContactCredit'

IF @contactCreditDefId IS NULL
	RAISERROR('Failed to find a product definition for "ContactCredit".', 18, 1)

-- Grant UNLIMITED job ads for ONE YEAR to those who have unlimited contact credits and don't already have
-- unlimited job ads.

INSERT INTO linkme_owner.Product(id, expiryDate, quantity, memberId, productDefinitionId)
SELECT NEWID(), @expiryOneYear, @intUnlimitedQuantity, e.id, @jobAdDefId
FROM linkme_owner.employer_profile e
WHERE NOT EXISTS
(
	SELECT *
	FROM linkme_owner.Product p
	WHERE p.memberId = e.id AND p.productDefinitionId = @jobAdDefId AND p.quantity = @intUnlimitedQuantity
)
AND EXISTS
(
	SELECT *
	FROM linkme_owner.Product p2
	WHERE p2.memberId = e.id AND p2.productDefinitionId = @contactCreditDefId AND p2.quantity = @intUnlimitedQuantity
)

-- Grant 20 job ads for ONE MONTH to those who don't have unlimited contact credits and don't already have
-- unlimited job ads.

INSERT INTO linkme_owner.Product(id, expiryDate, quantity, memberId, productDefinitionId)
SELECT NEWID(), @expiryOneMonth, @intQuantityForOneMonth, e.id, @jobAdDefId
FROM linkme_owner.employer_profile e
WHERE NOT EXISTS
(
	SELECT *
	FROM linkme_owner.Product p
	WHERE p.memberId = e.id AND p.productDefinitionId = @jobAdDefId AND p.quantity = @intUnlimitedQuantity
)
AND NOT EXISTS
(
	SELECT *
	FROM linkme_owner.Product p2
	WHERE p2.memberId = e.id AND p2.productDefinitionId = @contactCreditDefId AND p2.quantity = @intUnlimitedQuantity
)
