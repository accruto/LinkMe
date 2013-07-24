-- Alias used by JobG8

IF NOT EXISTS (SELECT * FROM dbo.IndustryAlias WHERE displayName = 'I.T. & Communications')
	INSERT dbo.IndustryAlias
		(id, industryId, displayName)
	VALUES
		('377e21d5-fca3-4c77-b289-b4d3e8e9770b', '728d4d98-c0ca-43bd-b413-a8db6028733e', 'I.T. & Communications')