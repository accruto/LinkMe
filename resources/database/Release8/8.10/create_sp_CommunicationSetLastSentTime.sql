IF EXISTS (SELECT * FROM dbo.SYSOBJECTS WHERE id = object_id('EmailSetLastSentTime') AND OBJECTPROPERTY(id, 'IsProcedure') = 1)
BEGIN 
	DROP PROCEDURE EmailSetLastSentTime
END
GO

IF EXISTS (SELECT * FROM dbo.SYSOBJECTS WHERE id = object_id('CommunicationSetLastSentTime') AND OBJECTPROPERTY(id, 'IsProcedure') = 1)
BEGIN 
	DROP PROCEDURE CommunicationSetLastSentTime
END
GO

CREATE PROCEDURE dbo.CommunicationSetLastSentTime(@userId UNIQUEIDENTIFIER, @definitionId UNIQUEIDENTIFIER, @lastSentTime DATETIME)
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
				(@settingsId, @userId, 0)

		END

		-- Insert or update.

		IF EXISTS (SELECT * FROM dbo.CommunicationDefinitionSettings WHERE definitionId = @definitionId AND settingsId = @settingsId)
			UPDATE
				dbo.CommunicationDefinitionSettings
			SET
				lastSentTime = @lastSentTime
			WHERE
				definitionId = @definitionId AND settingsId = @settingsId
		ELSE
			INSERT
				dbo.CommunicationDefinitionSettings (id, definitionId, settingsId, lastSentTime)
			SELECT
				NEWID(), @definitionId, @settingsId, @lastSentTime

	END

END
GO