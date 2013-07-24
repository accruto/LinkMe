DECLARE @id UNIQUEIDENTIFIER
SET @id = '{C84F3FAC-FFD2-49D4-AF35-E3410C5847BE}'

DECLARE @name NVARCHAR(100)
SET @name = 'PartnerNotification'

IF EXISTS (SELECT * FROM dbo.CommunicationCategory WHERE id = @id)
	UPDATE
		dbo.CommunicationCategory
	SET
		[name] = @name,
		[type] = 1,
		defaultFrequency = 1,
		deleted = 0,
		roles = 4
	WHERE
		id = @id
ELSE
	INSERT
		dbo.CommunicationCategory (id, [name], [type], defaultFrequency, deleted, roles)
	VALUES
		(@id, @name, 1, 1, 0, 4)

