CREATE PROCEDURE UpdateOffering
(
	@oldName NVARCHAR(200),
	@newname NVARCHAR(200)
)
AS

BEGIN
	UPDATE
		dbo.Offering
	SET
		name = @newName
	WHERE
		name = @oldName

	UPDATE
		dbo.OfferCategory
	SET
		name = @newName
	WHERE
		name = @oldName
END

GO

DECLARE @offeringId UNIQUEIDENTIFIER
DECLARE @locationReferenceId UNIQUEIDENTIFIER

DECLARE csr CURSOR FOR
SELECT
	o.id
FROM
	OfferCategory AS c
INNER JOIN
	OfferCategoryOffering AS co ON co.categoryId = c.id
INNER JOIN
	Offering AS o ON o.id = co.offeringId
WHERE
	c.name LIKE '% (NSW)'

OPEN csr

FETCH NEXT FROM csr
INTO @offeringId

WHILE @@FETCH_STATUS = 0
BEGIN

	FETCH NEXT FROM csr
	INTO @offeringId

	SET @locationReferenceId = NEWID()
	EXEC CreateOfferingLocation @offeringId, @locationReferenceId, 'NSW', 13

END

CLOSE csr
DEALLOCATE csr

GO

UPDATE
	dbo.OfferCategory
SET
	name = LEFT(name, LEN(name) - 6)
WHERE
	name LIKE '% (NSW)'

UPDATE
	OfferCategoryOffering
SET
	categoryId = '2FEB099E-C4DE-4C78-9737-C8398F6A3F0A'
WHERE
	categoryId = 'C07B309E-5B3F-4081-B751-023D5429A93B'

UPDATE
	OfferCategory
SET
	enabled = 0
WHERE
	id = 'C07B309E-5B3F-4081-B751-023D5429A93B'

DROP PROCEDURE UpdateOffering
