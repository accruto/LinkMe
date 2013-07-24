
UPDATE
	dbo.Industry
SET
	displayName = 'Primary Industry & Agriculture',
	keywordExpression = '"Primary Industry" OR Agriculture',
	shortDisplayName = 'Primary Industry & Agriculture'
WHERE
	id = '63FE83FD-3000-46D4-A6A2-EB849B9FCB79'


IF NOT EXISTS (SELECT * FROM dbo.IndustryAlias WHERE displayName = 'Primary Industry')
INSERT
	dbo.IndustryAlias (id, industryId, displayName)
VALUES
	('30030C9A-7CFF-4911-97A8-DB6BEE29FE06', '63FE83FD-3000-46D4-A6A2-EB849B9FCB79', 'Primary Industry')


