UPDATE
	dbo.Community
SET
	flags = 11
WHERE
	flags = 3

UPDATE
	dbo.Community
SET
	flags = 11
WHERE
	flags = 9

UPDATE
	dbo.Community
SET
	name = 'Monash University Business and Economics'
WHERE
	name = 'Monash University Graduate School of Business'

SELECT
	*
FROM
	dbo.Community