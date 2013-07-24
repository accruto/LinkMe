
CREATE PROCEDURE dbo.CreateDefinition(@id UNIQUEIDENTIFIER, @name NVARCHAR(100), @category NVARCHAR(100))
AS
BEGIN

	DECLARE @categoryId UNIQUEIDENTIFIER
	SELECT @categoryId = id FROM dbo.CommunicationCategory WHERE name = @category

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

END
GO

EXEC dbo.CreateDefinition '3321C021-EC0A-4d05-B997-18834A784DE1', 'ContactCandidateEmail', 'EmployerToMemberNotification'
EXEC dbo.CreateDefinition 'E8C10CAE-AC24-4aa4-BBB2-40981A67A546', 'RejectCandidateEmail', 'EmployerToMemberNotification'

EXEC dbo.CreateDefinition '87A6AF0B-15E4-4e9e-86AF-6F627768D90A', 'GroupEventInvitationEmail', 'MemberGroupNotification'
EXEC dbo.CreateDefinition '6DCEFFA0-1E0E-402b-A3C6-BF5B9FD58DEA', 'GroupFriendInvitationEmail', 'MemberGroupNotification'

EXEC dbo.CreateDefinition '933AC5BD-1DCE-4570-8C35-94278E5ED6C9', 'MemberMessageSentEmail', 'MemberToMemberNotification'
EXEC dbo.CreateDefinition 'B4A4B1D5-381F-4473-9786-0C45A11FEEEA', 'InvitationConfirmationEmail', 'MemberToMemberNotification'
EXEC dbo.CreateDefinition '4043B147-FA62-400d-84A7-524A2AF810C4', 'FriendInvitationEmail', 'MemberToMemberNotification'
EXEC dbo.CreateDefinition 'D6C19AD3-1594-416b-8685-6E3DA5E2FD1D', 'WhiteboardNotificationEmail', 'MemberToMemberNotification'

EXEC dbo.CreateDefinition '8C2F5C61-9DA0-412e-A8B6-033B7940C36D', 'JobSearchAlertEmail', 'MemberAlert'

EXEC dbo.CreateDefinition 'F085710D-79DE-4f03-81BE-58915C480669', 'InviteMoreNetworkersEmail', 'MemberReminder'

EXEC dbo.CreateDefinition 'EBCF2B25-87BC-42f4-94EA-836A5739A3CC', 'StatusUpdateEmail', 'MemberUpdate'

EXEC dbo.CreateDefinition '8352CE8A-64F3-4716-BC9A-B2DFB1D30950', 'EmployerExpiryEmail', 'EmployerNotification'
EXEC dbo.CreateDefinition '89328497-0779-4a12-919E-105F6706422B', 'EmployerUsageEmail', 'EmployerNotification'

DROP PROCEDURE dbo.CreateDefinition
GO
