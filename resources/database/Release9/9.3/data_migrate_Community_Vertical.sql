DELETE dbo.Vertical
GO

INSERT
	dbo.Vertical (id, name, url, host, countryId)
SELECT
	id, name, url, host, NULL
FROM
	dbo.Community
GO

-- Set New Zealand which is a vertical but not a community.

UPDATE
	dbo.Vertical
SET
	countryId = 8
WHERE
	name = 'New Zealand'
GO

DELETE
	dbo.Community
WHERE
	name = 'New Zealand'
GO
	