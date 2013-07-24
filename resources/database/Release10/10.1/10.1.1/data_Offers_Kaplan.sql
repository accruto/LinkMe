DECLARE @providerId UNIQUEIDENTIFIER
DECLARE @parentCategoryId UNIQUEIDENTIFIER

-- GCA

SET @providerId = '{836A1C87-6F0C-41ee-9C72-EBD83F94E7E0}'
EXEC dbo.CreateOfferProvider @providerId, 'Kaplan GCA'

SET @parentCategoryId = 'FAF62B20-0302-4E86-BDB5-8EE27443FF71'
EXEC dbo.CreateOffering '{8F25EF13-D27A-4213-B832-1372DF92F8F0}', 'Graduate Certificate in Accounting (VIC, NSW & SA)', @providerId, '{EC29EDFE-05CC-4811-BA9A-0176B8B8F92B}', @parentCategoryId

-- MAS

SET @providerId = '{F4CB5255-989C-48e2-B5A0-20593944D341}'
EXEC dbo.CreateOfferProvider @providerId, 'Kaplan MAS'

SET @parentCategoryId = 'FAF62B20-0302-4E86-BDB5-8EE27443FF71'
EXEC dbo.CreateOffering '{293F4C53-71A8-4b30-9D5D-171D514ECC20}', 'Master of Accounting Studies (VIC, NSW & SA)', @providerId, '{652CF4BA-5EF6-4dd3-BF64-C47A08B38BC1}', @parentCategoryId

-- MPA

SET @providerId = '{13734872-93F6-449a-8A9B-1BE0E484DF83}'
EXEC dbo.CreateOfferProvider @providerId, 'Kaplan MPA'

SET @parentCategoryId = 'FAF62B20-0302-4E86-BDB5-8EE27443FF71'
EXEC dbo.CreateOffering '{B542A8D1-A672-4109-977A-9936CDA40D04}', 'Master of Professional Accounting (VIC, NSW & SA)', @providerId, '{AE9F1171-736A-424d-8FCD-2E9750691081}', @parentCategoryId

-- Bbus

SET @providerId = '{2DF4B430-CCAB-4ace-8702-42528994A377}'
EXEC dbo.CreateOfferProvider @providerId, 'Kaplan Bbus'

SET @parentCategoryId = 'D328CBF4-3429-4D49-A792-442F85FC7763'
EXEC dbo.CreateOffering '{FDAF091E-EB30-4a87-A857-319E8EE8233B}', 'Bachelors (VIC & SA)', @providerId, '{2FEB099E-C4DE-4c78-9737-C8398F6A3F0A}', @parentCategoryId

-- DipCom

SET @providerId = '{90C2199D-277E-41b0-84E9-D5172EEE18AD}'
EXEC dbo.CreateOfferProvider @providerId, 'Kaplan DipCom'

SET @parentCategoryId = 'D328CBF4-3429-4D49-A792-442F85FC7763'
EXEC dbo.CreateOffering '{4D617CC8-0AFF-497e-B868-97A89C0C8FDB}', 'Diploma of Commerce (VIC, SA & NSW)', @providerId, '{C0AFD924-72B6-46df-A53C-D74929423708}', @parentCategoryId

-- Alternative Careers

SET @parentCategoryId = '385188B4-D205-46B5-AA18-6170B9623407'

EXEC dbo.CreateOfferCategory '{3922A0A0-3F90-4c1b-9E6A-F20362BDCAA0}', @parentCategoryId, 'Help me become a Financial Planner'
EXEC dbo.CreateOfferCategory '{BF875C5B-B498-4962-BF26-D073C2FFB61A}', @parentCategoryId, 'Help me become a Real Estate Agent'

DECLARE @categoryId UNIQUEIDENTIFIER
DECLARE @offeringId UNIQUEIDENTIFIER

SET @categoryId = 'F71EB9C8-DA47-4ab7-A06D-3F4C9730B651'
SET @offeringId = 'AC839BC2-2BBD-4710-9A52-933021020971'

UPDATE
	dbo.OfferCategoryOffering
SET
	categoryId = '{3922A0A0-3F90-4c1b-9E6A-F20362BDCAA0}'
WHERE
	categoryId = @categoryId AND offeringId = @offeringId

SET @categoryId = '7249FD4A-DB53-44af-8C68-E39CCC2F2450'
SET @offeringId = '99FF1CB4-59C3-4cb1-B8D2-A188C2B41693'

UPDATE
	dbo.OfferCategoryOffering
SET
	categoryId = '{3922A0A0-3F90-4c1b-9E6A-F20362BDCAA0}'
WHERE
	categoryId = @categoryId AND offeringId = @offeringId

SET @categoryId = 'FB834401-D50F-475b-A791-73689363168E'
SET @offeringId = 'FD007234-A6E5-4c27-BF2C-55252EC35DC2'

UPDATE
	dbo.OfferCategoryOffering
SET
	categoryId = '{3922A0A0-3F90-4c1b-9E6A-F20362BDCAA0}'
WHERE
	categoryId = @categoryId AND offeringId = @offeringId

SET @categoryId = 'A9DB0AFF-7EB1-48a5-841B-DE1A978C277F'
SET @offeringId = '3EEF0711-7C60-4c5c-AB42-2320261AA605'

UPDATE
	dbo.OfferCategoryOffering
SET
	categoryId = '{BF875C5B-B498-4962-BF26-D073C2FFB61A}'
WHERE
	categoryId = @categoryId AND offeringId = @offeringId

UPDATE dbo.OfferCategory SET enabled = 0 WHERE id = '40BAB544-26AD-469d-A319-64DB914F8F76'
UPDATE dbo.OfferCategory SET enabled = 0 WHERE id = 'F71EB9C8-DA47-4ab7-A06D-3F4C9730B651'
UPDATE dbo.OfferCategory SET enabled = 0 WHERE id = '7249FD4A-DB53-44af-8C68-E39CCC2F2450'
UPDATE dbo.OfferCategory SET enabled = 0 WHERE id = 'FB834401-D50F-475b-A791-73689363168E'
UPDATE dbo.OfferCategory SET enabled = 0 WHERE id = '5FBA0C70-1AD6-49fa-BBEB-DFBAE574D91A'
UPDATE dbo.OfferCategory SET enabled = 0 WHERE id = 'A9DB0AFF-7EB1-48a5-841B-DE1A978C277F'
