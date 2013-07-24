-- Disable the old.

UPDATE
	dbo.Product
SET
	enabled = 0
WHERE
	name IN ('15 Applicants', '25 Applicants', '50 Applicants')

-- 20 Applicants

DECLARE @id UNIQUEIDENTIFIER
SET @id = '18A662F3-64CC-4a80-9CED-5E9F94100DD4'

IF EXISTS (SELECT * FROM dbo.Product WHERE id = @id)
	UPDATE
		dbo.Product
	SET
		name = '20 Applicants',
		enabled = 1,
		userTypes = 2,
		price = 200.00,
		currency = 36
	WHERE
		id = @id
ELSE
	INSERT
		dbo.Product (id, name, enabled, userTypes, price, currency)
	VALUES
		(@id, '20 Applicants', 1, 2, 200.00, 36)

DELETE
	dbo.ProductCreditAdjustment
WHERE
	productId = @id

INSERT
	dbo.ProductCreditAdjustment (productId, creditId, quantity, duration)
VALUES
	(@id, 'E63229B6-1F14-4F3A-9707-B09B375DA3A5', 20, 315360000000000)

INSERT
	dbo.ProductCreditAdjustment (productId, creditId, quantity, duration)
VALUES
	(@id, 'AE51B67A-9871-4916-B38A-C9BB3B27B83D', NULL, 315360000000000)

-- 40 Applicants

SET @id = 'AFF77C84-7B6F-4f1e-B777-27D5665DAB54'

IF EXISTS (SELECT * FROM dbo.Product WHERE id = @id)
	UPDATE
		dbo.Product
	SET
		name = '40 Applicants',
		enabled = 1,
		userTypes = 2,
		price = 360.00,
		currency = 36
	WHERE
		id = @id
ELSE
	INSERT
		dbo.Product (id, name, enabled, userTypes, price, currency)
	VALUES
		(@id, '40 Applicants', 1, 2, 360.00, 36)

DELETE
	dbo.ProductCreditAdjustment
WHERE
	productId = @id

INSERT
	dbo.ProductCreditAdjustment (productId, creditId, quantity, duration)
VALUES
	(@id, 'E63229B6-1F14-4F3A-9707-B09B375DA3A5', 40, 315360000000000)

INSERT
	dbo.ProductCreditAdjustment (productId, creditId, quantity, duration)
VALUES
	(@id, 'AE51B67A-9871-4916-B38A-C9BB3B27B83D', NULL, 315360000000000)

-- 60 Applicants

SET @id = 'BA45FA9C-0445-4bc2-B038-4FA56BB5D8D9'

IF EXISTS (SELECT * FROM dbo.Product WHERE id = @id)
	UPDATE
		dbo.Product
	SET
		name = '60 Applicants',
		enabled = 1,
		userTypes = 2,
		price = 510.00,
		currency = 36
	WHERE
		id = @id
ELSE
	INSERT
		dbo.Product (id, name, enabled, userTypes, price, currency)
	VALUES
		(@id, '60 Applicants', 1, 2, 510.00, 36)

DELETE
	dbo.ProductCreditAdjustment
WHERE
	productId = @id

INSERT
	dbo.ProductCreditAdjustment (productId, creditId, quantity, duration)
VALUES
	(@id, 'E63229B6-1F14-4F3A-9707-B09B375DA3A5', 60, 315360000000000)

INSERT
	dbo.ProductCreditAdjustment (productId, creditId, quantity, duration)
VALUES
	(@id, 'AE51B67A-9871-4916-B38A-C9BB3B27B83D', NULL, 315360000000000)

