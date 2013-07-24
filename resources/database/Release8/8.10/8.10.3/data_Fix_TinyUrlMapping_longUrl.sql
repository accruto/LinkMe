UPDATE
	dbo.TinyUrlMapping
SET
	longUrl = '~/' + RIGHT(longUrl, LEN(longUrl) - 1)
WHERE
	longUrl LIKE '~%' AND longUrl NOT LIKE '~/%'
