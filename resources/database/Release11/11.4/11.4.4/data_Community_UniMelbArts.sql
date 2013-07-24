UPDATE
	dbo.Community
SET
	name = 'University of Melbourne - Graduate School of Humanities and Social Sciences'
WHERE
	name = 'University of Melbourne Arts'


DECLARE @verticalId UNIQUEIDENTIFIER
SET @verticalId = '57B22B9F-7509-419B-B4E0-205CDBC7A78B'

DECLARE @itemId UNIQUEIDENTIFIER
DECLARE @parentItemId UNIQUEIDENTIFIER
DECLARE @detailId UNIQUEIDENTIFIER
DECLARE @parentDetailId UNIQUEIDENTIFIER

SELECT
	@parentItemId = i.id
FROM
	dbo.ContentItem AS i
WHERE
	(i.name = 'Logged in home page left section' OR i.name = 'Logged in home page top left section' OR i.name = 'Logged in home page right section')
	AND verticalid = @verticalId

SELECT
	@itemId = i.id
FROM
	dbo.ContentItem AS i
INNER JOIN
	dbo.ContentItem AS p ON p.id = i.parentId
WHERE
	(p.name = 'Logged in home page left section' OR p.name = 'Logged in home page top left section' OR p.name = 'Logged in home page right section')
	AND p.verticalid = @verticalId

SELECT
	 @detailId = d.id
FROM
	dbo.ContentItem AS i
INNER JOIN
	ContentDetail AS d ON d.itemid = i.id
WHERE
	i.id = @itemId

SELECT
	@parentDetailId = d.id
FROM
	dbo.ContentItem AS i
INNER JOIN
	ContentDetail AS d ON d.itemid = i.id
WHERE
	i.id = @parentItemId

-- Update

UPDATE
	dbo.ContentItem
SET
	name = 'Logged in home page right section'
WHERE
	id = @parentItemId

UPDATE
	dbo.ContentItem
SET
	name = 'SectionContent'
WHERE
	id = @itemId

UPDATE
	dbo.ContentItem
SET
	enabled = 1
WHERE
	id = @parentItemId

UPDATE
	dbo.ContentDetail
SET
	stringValue = 'Resources'
WHERE
	id = @parentDetailId

UPDATE
	dbo.ContentDetail
SET
	stringValue = '<br/><p><a href="https://security.arts.unimelb.edu.au/graduate/careersportal/additional-resources.php">GSHSS Additional Resources</a></p>'
WHERE
	id = @detailId
