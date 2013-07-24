DECLARE @id UNIQUEIDENTIFIER
SET @id = '{C353FE96-A654-42ff-81D8-2051AA146DFC}'

DECLARE @name NVARCHAR(100)
SET @name = 'Campaign'

IF EXISTS (SELECT * FROM dbo.CommunicationCategory WHERE id = @id)
	UPDATE
		dbo.CommunicationCategory
	SET
		name = @name,
		type = 1,
		defaultFrequency = 1,
		deleted = 0,
		roles = 14
	WHERE
		id = @id
ELSE
	INSERT
		dbo.CommunicationCategory (id, name, type, defaultFrequency, deleted, roles)
	VALUES
		(@id, @name, 1, 1, 0, 14)