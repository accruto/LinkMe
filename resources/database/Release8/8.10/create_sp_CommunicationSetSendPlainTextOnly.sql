IF EXISTS (SELECT * FROM dbo.SYSOBJECTS WHERE id = object_id('EmailSetSendPlainTextOnly') AND OBJECTPROPERTY(id, 'IsProcedure') = 1)
BEGIN 
	DROP PROCEDURE EmailSetSendPlainTextOnly
END
GO

IF EXISTS (SELECT * FROM dbo.SYSOBJECTS WHERE id = object_id('CommunicationSetSendPlainTextOnly') AND OBJECTPROPERTY(id, 'IsProcedure') = 1)
BEGIN 
	DROP PROCEDURE CommunicationSetSendPlainTextOnly
END
GO

CREATE PROCEDURE dbo.CommunicationSetSendPlainTextOnly(@userId UNIQUEIDENTIFIER, @sendPlainTextOnly BIT)
AS
BEGIN
	SET NOCOUNT ON

	-- Check that the user exists.

	IF EXISTS (SELECT * FROM RegisteredUser WHERE id = @userId)
	BEGIN

		-- Make sure an CommunicationSettings entry exists for the user.

		DECLARE @settingsId UNIQUEIDENTIFIER
		SELECT @settingsId = id FROM dbo.CommunicationSettings WHERE userId = @userId

		IF (@settingsId IS NULL)
		BEGIN

			SET @settingsId = NEWID()
			INSERT
				dbo.CommunicationSettings (id, userId, sendPlainTextOnly)
			VALUES
				(@settingsId, @userId, @sendPlainTextOnly)

		END
		ELSE
		BEGIN

			UPDATE
				CommunicationSettings
			SET
				sendPlainTextOnly = @sendPlainTextOnly
			WHERE
				id = @settingsId

		END

	END

END
GO