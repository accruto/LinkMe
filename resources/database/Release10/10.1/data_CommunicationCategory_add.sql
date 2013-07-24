DECLARE @id UNIQUEIDENTIFIER
SET @id = '{A81CDBAF-AEBD-4E97-BDC9-94CD391E53B8}'

DECLARE @name NVARCHAR(100)
SET @name = 'SuggestedJobs'

IF EXISTS (SELECT * FROM dbo.CommunicationCategory WHERE id = @id)
	UPDATE
		dbo.CommunicationCategory
	SET
		[name] = @name,
		[type] = 0,
		defaultFrequency = 4,
		deleted = 0,
		roles = 4
	WHERE
		id = @id
ELSE
	INSERT
		dbo.CommunicationCategory (id, [name], [type], defaultFrequency, deleted, roles)
	VALUES
		(@id, @name, 0, 4, 0, 4)