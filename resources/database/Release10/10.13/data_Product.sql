-- Disable old Contacts

UPDATE
	dbo.Product
SET
	enabled = 0
WHERE
	id IN ('4B209ABC-95D2-4AD8-9C99-A2C9A4D0B915', 'BB4BF45B-C0BE-4398-80B6-BE3555444AE5', 'C882B78C-93B4-42E3-B86A-F0C5CAB91B10', 'AF2C1176-98F8-4138-AE5B-BD39E518206B')

-- Disable old Applicants

UPDATE
	dbo.Product
SET
	enabled = 0
WHERE
	id IN ('94170E5D-EFE1-49FF-984D-1BB450244BEB', '3E3410DF-A95A-4995-BC42-CA2A858D1EE8', '8D96FBC8-8F47-4597-B559-EBB7B7D9C5DE', '05FE52A9-D7A7-4A96-9FCF-EFB4414CB6B0')

-- Disable old bundles

UPDATE
	dbo.Product
SET
	enabled = 0
WHERE
	id IN ('A46812F1-CAD2-47C1-B8C3-5F7EF150DFBF', 'BB5BC37D-052C-4946-BA0A-382AA7FEF1F2', 'C9C3DA74-9DFD-42C9-99D6-6A3C8ABF04E1', '5C6B8B88-8057-44A9-8011-D6E0BF38CBAE')

GO

-- Add new

CREATE PROCEDURE [dbo].[CreateContactProduct]
(
	@id UNIQUEIDENTIFIER,
	@name NVARCHAR(100),
	@quantity INT,
	@price INT
)
AS

IF NOT EXISTS (SELECT * FROM dbo.Product WHERE id = @id)
	INSERT
		dbo.Product (id, name, enabled, userTypes, price, currency)
	VALUES
		(@id, @name, 1, 2, @price, 36)
ELSE
	UPDATE
		dbo.Product
	SET
		name = @name,
		enabled = 1,
		userTypes = 2,
		price = @price,
		currency = 36
	WHERE
		id = @id

DELETE
	dbo.ProductCreditAdjustment
WHERE
	productId = @id

INSERT
	dbo.ProductCreditAdjustment (productId, creditId, quantity, duration)
VALUES
	(@id, '571B8809-F368-49CE-88C6-34C8ECD7D2C7', @quantity, 315360000000000)

GO

CREATE PROCEDURE [dbo].[CreateApplicantProduct]
(
	@id UNIQUEIDENTIFIER,
	@name NVARCHAR(100),
	@quantity INT,
	@price INT
)
AS

IF NOT EXISTS (SELECT * FROM dbo.Product WHERE id = @id)
	INSERT
		dbo.Product (id, name, enabled, userTypes, price, currency)
	VALUES
		(@id, @name, 1, 2, @price, 36)
ELSE
	UPDATE
		dbo.Product
	SET
		name = @name,
		enabled = 1,
		userTypes = 2,
		price = @price,
		currency = 36
	WHERE
		id = @id

DELETE
	dbo.ProductCreditAdjustment
WHERE
	productId = @id

INSERT
	dbo.ProductCreditAdjustment (productId, creditId, quantity, duration)
VALUES
	(@id, 'E63229B6-1F14-4F3A-9707-B09B375DA3A5', @quantity, 315360000000000)

INSERT
	dbo.ProductCreditAdjustment (productId, creditId, quantity, duration)
VALUES
	(@id, 'AE51B67A-9871-4916-B38A-C9BB3B27B83D', NULL, 315360000000000)

GO

EXEC dbo.CreateContactProduct '{DEEB3222-4C5B-4325-8AD8-A91BF787E863}', '5 Contacts', 5, 180
EXEC dbo.CreateContactProduct '{8615F10C-58B8-4d6b-9A43-BB12A24FF45A}', '10 Contacts', 10, 330
EXEC dbo.CreateContactProduct '{DB6170F9-5E96-41a1-A58A-33521C060B6F}', '20 Contacts', 20, 600
EXEC dbo.CreateContactProduct '{54901FDD-4518-4e40-9283-E5908338DBE7}', '40 Contacts', 40, 1120
EXEC dbo.CreateContactProduct '{CCC4C837-C424-41ec-8D90-F376FC936A49}', '60 Contacts', 60, 1560
EXEC dbo.CreateContactProduct '{6951B9EC-3EAE-4b2a-942F-D00430A9F2DD}', '80 Contacts', 80, 1920

EXEC dbo.CreateApplicantProduct '{8D472AC3-4027-4102-875C-91D92E4BC3E9}', '15 Applicants', 15, 180
EXEC dbo.CreateApplicantProduct '{6EE3D2E8-0FCD-4678-BF66-3BD987839418}', '25 Applicants', 25, 250
EXEC dbo.CreateApplicantProduct '{E615D71B-5813-4551-97FD-D63E2042491A}', '50 Applicants', 50, 450
EXEC dbo.CreateApplicantProduct '{8FF5AC20-15B8-4d79-8506-DACB7CD6C251}', '100 Applicants', 100, 750
EXEC dbo.CreateApplicantProduct '{95237919-7B39-41de-836D-022B200BE12E}', '200 Applicants', 200, 1200

DROP PROCEDURE [dbo].[CreateContactProduct]
DROP PROCEDURE [dbo].[CreateApplicantProduct]