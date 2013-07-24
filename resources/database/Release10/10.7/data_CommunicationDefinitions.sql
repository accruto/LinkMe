UPDATE
	dbo.CommunicationDefinition
SET
	name = 'FriendInvitationConfirmationEmail'
WHERE
	name = 'InvitationConfirmationEmail'
GO

DECLARE @categoryId UNIQUEIDENTIFIER
DECLARE @id UNIQUEIDENTIFIER
DECLARE @name NVARCHAR(50)

SET @categoryId = '9E45F20E-9F34-4993-A8B7-1FD7E3809A0E'
SET @id = 'C8DC8967-CC51-4270-9FC1-35C8EAF4DA62'
SET @name = 'RepresentativeInvitationConfirmationEmail'

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

SET @categoryId = '9E45F20E-9F34-4993-A8B7-1FD7E3809A0E'
SET @id = '147216C8-9534-42dd-8648-8EA4BF8A6D2E'
SET @name = 'RepresentativeInvitationEmail'

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

SET @categoryId = '0794C0DA-1F07-4541-9391-7BABF15F6FAB'
SET @id = 'BB3345DB-BFFF-4ab9-9662-D33DC5E5A866'
SET @name = 'RepresentativeContactCandidateEmail'

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
