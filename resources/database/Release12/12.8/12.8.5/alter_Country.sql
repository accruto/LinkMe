ALTER TABLE dbo.Country
ADD isoCode NVARCHAR(2) NULL
GO

UPDATE
	dbo.Country
SET
	isoCode = 'AU'
WHERE
	id = 1

UPDATE
	dbo.Country
SET
	isoCode = 'CA'
WHERE
	id = 2

UPDATE
	dbo.Country
SET
	isoCode = 'CN'
WHERE
	id = 3

UPDATE
	dbo.Country
SET
	isoCode = 'IN'
WHERE
	id = 4

UPDATE
	dbo.Country
SET
	isoCode = 'ID'
WHERE
	id = 5

UPDATE
	dbo.Country
SET
	isoCode = 'JP'
WHERE
	id = 6

UPDATE
	dbo.Country
SET
	isoCode = 'MY'
WHERE
	id = 7

UPDATE
	dbo.Country
SET
	isoCode = 'NZ'
WHERE
	id = 8

UPDATE
	dbo.Country
SET
	isoCode = 'GB'
WHERE
	id = 9

UPDATE
	dbo.Country
SET
	isoCode = 'US'
WHERE
	id = 10

UPDATE
	dbo.Country
SET
	isoCode = 'SG'
WHERE
	id = 11

