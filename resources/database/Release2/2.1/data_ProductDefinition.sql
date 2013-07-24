DECLARE @unlimitedQuantity INT
DECLARE @contactCreditId AS UNIQUEIDENTIFIER
DECLARE @jobAdId AS UNIQUEIDENTIFIER
DECLARE @tradingPostAdId AS UNIQUEIDENTIFIER
DECLARE @smallPackageId AS UNIQUEIDENTIFIER
DECLARE @mediumPackageId AS UNIQUEIDENTIFIER
DECLARE @largePackageId AS UNIQUEIDENTIFIER

SET @unlimitedQuantity = 2000000000
SET @contactCreditId = 	'{571B8809-F368-49CE-88C6-34C8ECD7D2C7}'
SET @jobAdId = 		'{AE51B67A-9871-4916-B38A-C9BB3B27B83D}'
SET @tradingPostAdId = 	'{43ABBCF4-4CE8-4A3E-8E92-313808C721D6}'
SET @smallPackageId = 	'{EC578433-2EF7-4D28-9B57-542BE25A528C}'
SET @mediumPackageId = 	'{220EADAE-DBD8-4507-A9E7-484D3D4572C2}'
SET @largePackageId = 	'{6D5E76DC-DAE1-4776-ADD6-91EE43478875}'

-- Base products.

INSERT INTO linkme_owner.ProductDefinition(id, type, duration, [name], displayName, shortDescription, isActive, priceExGst)
VALUES (@contactCreditId, 'ContactCredit', 26784000000000, 'ContactCredit', 'Contact Credit',
	'A credit that allows access to one candidate''s contact details.', 1, 0)

INSERT INTO linkme_owner.ProductDefinition(id, type, duration, [name], displayName, shortDescription, isActive, priceExGst)
VALUES (@jobAdId, 'JobAd', 26784000000000, 'JobAd', 'Job Ad',
	'A credit that allows posting one job ad online.', 1, 0)

INSERT INTO linkme_owner.ProductDefinition(id, type, duration, [name], displayName, shortDescription, isActive, priceExGst)
VALUES (@tradingPostAdId, 'TradingPostAd', 26784000000000, 'TradingPostAd', 'Trading Post Ad',
	'A credit that allows posting one job ad in the Trading Post.', 1, 25)

-- Packages.

INSERT INTO linkme_owner.ProductDefinition(id, type, duration, [name], displayName, shortDescription, isActive, priceExGst)
VALUES (@smallPackageId, 'Package', 26784000000000, '5Contacts1JobAd', 'Small Business Package',
	'5 contact credits and 1 job ad.', 1, 99)

INSERT INTO linkme_owner.ProductPackageAssociation(id, quantity, packageDefinitionId, includedProductDefinitionId)
VALUES(NEWID(), 5, @smallPackageId, @contactCreditId)

INSERT INTO linkme_owner.ProductPackageAssociation(id, quantity, packageDefinitionId, includedProductDefinitionId)
VALUES(NEWID(), 1, @smallPackageId, @jobAdId)

INSERT INTO linkme_owner.ProductDefinition(id, type, duration, [name], displayName, shortDescription, isActive, priceExGst)
VALUES (@mediumPackageId, 'Package', 316224000000000, '50Contacts10JobAds', 'Medium Enterprise Package',
	'50 contact credits and 10 job ads.', 1, 499)

INSERT INTO linkme_owner.ProductPackageAssociation(id, quantity, packageDefinitionId, includedProductDefinitionId)
VALUES(NEWID(), 50, @mediumPackageId, @contactCreditId)

INSERT INTO linkme_owner.ProductPackageAssociation(id, quantity, packageDefinitionId, includedProductDefinitionId)
VALUES(NEWID(), 10, @mediumPackageId, @jobAdId)

INSERT INTO linkme_owner.ProductDefinition(id, type, duration, [name], displayName, shortDescription, isActive, priceExGst)
VALUES (@largePackageId, 'Package', 316224000000000, 'UnlimitedContacts50JobAds', 'HR Professional Package',
	'Unlimited contact credits and 50 job ads.', 1, 3000)

INSERT INTO linkme_owner.ProductPackageAssociation(id, quantity, packageDefinitionId, includedProductDefinitionId)
VALUES(NEWID(), @unlimitedQuantity, @largePackageId, @contactCreditId)

INSERT INTO linkme_owner.ProductPackageAssociation(id, quantity, packageDefinitionId, includedProductDefinitionId)
VALUES(NEWID(), 50, @largePackageId, @jobAdId)

GO
