DECLARE @categoryId UNIQUEIDENTIFIER
SET @categoryId = '{41EEE2A6-D2D7-402a-A23C-4AFDAA1F7E2C}'

DECLARE @definitionId UNIQUEIDENTIFIER
SET @definitionId = '{12239D7C-C604-4463-98FC-4F27F97D3E9F}'

IF NOT EXISTS (SELECT * FROM dbo.CommunicationCategory WHERE id = @categoryId)
	INSERT
		dbo.CommunicationCategory (id, name, type, defaultFrequency, deleted, roles)
	VALUES
		(@categoryId, 'EmployerUpdate', 1, 1, 0, 10)
ELSE
	UPDATE
		dbo.CommunicationCategory
	SET
		name = 'EmployerUpdate',
		type = 1,
		defaultFrequency = 1,
		deleted = 0,
		roles = 10
	WHERE
		id = @categoryId


IF NOT EXISTS (SELECT * FROM dbo.CommunicationDefinition WHERE id = @definitionId)
	INSERT
		dbo.CommunicationDefinition (id, name, categoryId)
	VALUES
		(@definitionId, 'EmployerNewsletterEmail', @categoryId)
ELSE
	UPDATE
		dbo.CommunicationDefinition
	SET
		name = 'EmployerNewsletterEmail',
		categoryId = @categoryId
	WHERE
		id = @definitionId