INSERT
	dbo.CommunicationDefinition (id, name, categoryId)
VALUES
	('{F4E474D3-5E64-4935-A8D4-EC5A01DE2624}', 'EdmEmail', 'C353FE96-A654-42FF-81D8-2051AA146DFC')

DECLARE @id UNIQUEIDENTIFIER
SET @id = '{60D5D73C-CB5B-4214-AE54-6F6723945D54}'

IF EXISTS (SELECT * FROM dbo.Campaign WHERE id = @id)
	UPDATE
		dbo.Campaign
	SET
		name = 'EDM',
		createdTime = GETDATE(),
		createdBy = '2E7D03B6-37E2-4D12-89F3-FFB36B939509',
		status = 0,
		category = 1,
		query = NULL,
		communicationCategoryId = 'C353FE96-A654-42FF-81D8-2051AA146DFC',
		communicationDefinitionId = 'F4E474D3-5E64-4935-A8D4-EC5A01DE2624'
	WHERE
		id = @id
ELSE
	INSERT
		dbo.Campaign (id, name, createdTime, createdBy, status, category, query, communicationCategoryId, communicationDefinitionId)
	VALUES
		(@id, 'EDM', GETDATE(), '2E7D03B6-37E2-4D12-89F3-FFB36B939509', 0, 1, NULL, 'C353FE96-A654-42FF-81D8-2051AA146DFC', 'F4E474D3-5E64-4935-A8D4-EC5A01DE2624')

