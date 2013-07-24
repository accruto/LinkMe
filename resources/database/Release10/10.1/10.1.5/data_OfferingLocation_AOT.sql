GO

DECLARE @offeringId UNIQUEIDENTIFIER
DECLARE @locationReferenceId UNIQUEIDENTIFIER

DECLARE csr CURSOR FOR
SELECT
	o.id
FROM
	Offering AS o
INNER JOIN
	OfferProvider AS p ON p.id = o.providerId
WHERE
	p.name = 'Accredited Online Training'

OPEN csr

FETCH NEXT FROM csr
INTO @offeringId

WHILE @@FETCH_STATUS = 0
BEGIN

	FETCH NEXT FROM csr
	INTO @offeringId

	SET @locationReferenceId = NEWID()
	EXEC CreateOfferingLocation @offeringId, @locationReferenceId, NULL, 1

END

CLOSE csr
DEALLOCATE csr

GO


