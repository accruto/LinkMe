-- Copy credits and unlimited accounts from the old way of storing them (employer_profile table) to the new
-- Product table.

DECLARE @contactCreditId UNIQUEIDENTIFIER
DECLARE @expiryDateNever DATETIME
DECLARE @quantityUnlimited INT

SET @expiryDateNever = '9999-12-31 00:00:00.000'
SET @quantityUnlimited = 2000000000

BEGIN TRANSACTION

SELECT @contactCreditId = id
FROM linkme_owner.ProductDefinition
WHERE [name] = 'ContactCredit'

IF @contactCreditId IS NULL
BEGIN
	RAISERROR('There is no product definition for the Contact Credit product in the database.', 18, 1)
	RETURN
END

-- Add unlimited, never-expiring contact credits for those with an unlimited account.

INSERT INTO linkme_owner.Product(id, expiryDate, quantity, memberId, productDefinitionId)
SELECT NEWID(), @expiryDateNever, @quantityUnlimited, ep.id, @contactCreditId
FROM linkme_owner.employer_profile ep
WHERE ep.invoiceAccountStatus = 'Approved'

-- Add other contact credits (also as never-expiring).

INSERT INTO linkme_owner.Product(id, expiryDate, quantity, memberId, productDefinitionId)
SELECT NEWID(), @expiryDateNever, ep.credits, ep.id, @contactCreditId
FROM linkme_owner.employer_profile ep
WHERE ep.credits > 0

COMMIT TRANSACTION
