
-- update hr pro package
UPDATE 	linkme_owner.ProductDefinition
SET 	priceExGst=3500
WHERE 	displayName='HR Professional Package' AND name='UnlimitedContacts50JobAds'
GO

-- update casual user package
UPDATE 	linkme_owner.ProductDefinition
SET 	priceExGst=150
WHERE 	displayName='Casual User Package' AND name='5Contacts1JobAd'
GO

-- disable old small biz package
UPDATE 	linkme_owner.ProductDefinition
SET	isActive=0 
WHERE 	displayName='Small Business Package' AND name='50Contacts10JobAds'
GO

-- add new business package
DECLARE @contactCreditId 	AS UNIQUEIDENTIFIER
DECLARE @jobAdId 		AS UNIQUEIDENTIFIER
DECLARE @newBusinessPackage	AS UNIQUEIDENTIFIER

SET @contactCreditId 	= (SELECT id FROM linkme_owner.ProductDefinition where NAME='ContactCredit' AND type='ContactCredit')
SET @jobAdId 		= (SELECT id FROM linkme_owner.ProductDefinition where NAME='JobAd' AND type='JobAd')
SET @newBusinessPackage = '{3860775D-9E27-46fd-AEEE-92263DBD0D13}'

INSERT INTO linkme_owner.ProductDefinition
	(id, type, duration, [name], displayName, isActive, priceExGst, shortDescription)
VALUES 	(@newBusinessPackage, 'Package', 316224000000000, '150Contacts25JobAds', 'Business Package', 1, 1500, 
	 '150 contact credits and 25 job ads.')

INSERT INTO linkme_owner.ProductPackageAssociation(id, quantity, packageDefinitionId, includedProductDefinitionId)
VALUES	(NEWID(), 150, @newBusinessPackage, @contactCreditId)

INSERT INTO linkme_owner.ProductPackageAssociation(id, quantity, packageDefinitionId, includedProductDefinitionId)
VALUES	(NEWID(), 25, @newBusinessPackage, @jobAdId)
GO
