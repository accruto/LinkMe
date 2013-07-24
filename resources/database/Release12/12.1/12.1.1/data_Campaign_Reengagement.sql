DECLARE @id UNIQUEIDENTIFIER
SET @id = '{B3DAC669-5BEF-4C76-93F5-BD06F6C1AFB2}'

DECLARE @definitionId UNIQUEIDENTIFIER
SET @definitionId = '{9172DCF7-A935-4567-9281-CE9270FD981A}'

DECLARE @categoryId UNIQUEIDENTIFIER
SET @categoryId = 'C353FE96-A654-42FF-81D8-2051AA146DFC'

INSERT
	dbo.CommunicationDefinition (id, name, categoryId)
VALUES
	(@definitionId, 'ReengagementEmail',@categoryId )

IF EXISTS (SELECT * FROM dbo.Campaign WHERE id = @id)
	UPDATE
		dbo.Campaign
	SET
		name = 'Member Reengagement',
		createdTime = GETDATE(),
		createdBy = '2E7D03B6-37E2-4D12-89F3-FFB36B939509',
		status = 0,
		category = 1,
		query = NULL,
		communicationCategoryId = @categoryId,
		communicationDefinitionId = @definitionId
	WHERE
		id = @id
ELSE
	INSERT
		dbo.Campaign (id, name, createdTime, createdBy, status, category, query, communicationCategoryId, communicationDefinitionId)
	VALUES
		(@id, 'Member Reengagement', GETDATE(), '2E7D03B6-37E2-4D12-89F3-FFB36B939509', 0, 1, NULL, @categoryId, @definitionId)

