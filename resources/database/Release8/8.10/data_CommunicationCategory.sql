
CREATE PROCEDURE dbo.CreateCategory(@id UNIQUEIDENTIFIER, @name NVARCHAR(100), @type TINYINT, @defaultFrequency TINYINT, @deleted BIT, @roles INT)
AS
BEGIN

	IF EXISTS (SELECT * FROM dbo.CommunicationCategory WHERE id = @id)
		UPDATE
			dbo.CommunicationCategory
		SET
			name = @name,
			type = @type,
			defaultFrequency = @defaultFrequency,
			deleted = @deleted,
			roles = @roles
		WHERE
			id = @id
	ELSE
		INSERT
			dbo.CommunicationCategory (id, name, type, defaultFrequency, deleted, roles)
		VALUES
			(@id, @name, @type, @defaultFrequency, @deleted, @roles)

END
GO

-- Employers

EXEC dbo.CreateCategory '2C237669-B320-494b-8142-15B0A5FACBFA', 'EmployerNotification', 1, 1, 0, 10

-- Members

EXEC dbo.CreateCategory '9F57430E-64EC-4A81-9F5B-24518BB88789', 'MemberReminder', 0, 0, 0, 4
EXEC dbo.CreateCategory '9E45F20E-9F34-4993-A8B7-1FD7E3809A0E', 'MemberToMemberNotification', 1, 1, 0, 4
EXEC dbo.CreateCategory '6C8CCADC-1AB5-4022-BB1A-2B4823669D89', 'MemberGroupNotification', 1, 1, 0, 4
EXEC dbo.CreateCategory '0794C0DA-1F07-4541-9391-7BABF15F6FAB', 'EmployerToMemberNotification', 1, 1, 0, 4
EXEC dbo.CreateCategory 'C56C9ECC-5515-432C-BBBB-440ACEDF4E2D', 'MemberAlert', 0, 2, 0, 4
EXEC dbo.CreateCategory '6E92DE26-E536-459D-9641-EE4F34473DAD', 'MemberUpdate', 0, 0, 0, 4

-- Both

EXEC dbo.CreateCategory '292A3436-A928-4B95-B6D3-0EA4113F215F', 'Newsletter', 0, 0, 1, 14

DROP PROCEDURE dbo.CreateCategory
GO
