UPDATE
	dbo.FrequentlyAskedQuestion
SET
	title = REPLACE(title, '''', '&#39;')
WHERE
	title LIKE '%''%'
