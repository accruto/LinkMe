DECLARE @id UNIQUEIDENTIFIER
SET @id = '83B9911D-0AE8-4550-B64A-F0D2A97B2380'

IF EXISTS (SELECT * FROM dbo.Campaign WHERE id = @id)
	UPDATE
		dbo.Campaign
	SET
		name = 'iOS Launch',
		createdTime = GETDATE(),
		createdBy = '2E7D03B6-37E2-4D12-89F3-FFB36B939509',
		status = 0,
		category = 1,
		query = NULL,
		communicationCategoryId = 'C353FE96-A654-42FF-81D8-2051AA146DFC',
		communicationDefinitionId = '840ADA47-7FA7-4DA5-A472-4430C662DB8B'
	WHERE
		id = @id
ELSE
	INSERT
		dbo.Campaign (id, name, createdTime, createdBy, status, category, query, communicationCategoryId, communicationDefinitionId)
	VALUES
		(@id, 'iOS Launch', GETDATE(), '2E7D03B6-37E2-4D12-89F3-FFB36B939509', 0, 1, NULL, 'C353FE96-A654-42FF-81D8-2051AA146DFC', '840ADA47-7FA7-4DA5-A472-4430C662DB8B')

