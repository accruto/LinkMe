DECLARE @id UNIQUEIDENTIFIER
SET @id = 'BA5C152C-D415-488a-8CDC-D8AA3C0F3641'

DECLARE @name VARCHAR(27)
SET @name = 'DiscussionNotificationEmail'

DECLARE @categoryId UNIQUEIDENTIFIER
SELECT @categoryId = id FROM dbo.CommunicationCategory WHERE name = 'MemberGroupNotification'

IF EXISTS (SELECT * FROM dbo.CommunicationDefinition WHERE id = @id)
	UPDATE
		dbo.CommunicationDefinition
	SET
		name = @name,
		categoryId = @categoryId
	WHERE
		id = @id
ELSE
	INSERT
		dbo.CommunicationDefinition (id, name, categoryId)
	VALUES
		(@id, @name, @categoryId)