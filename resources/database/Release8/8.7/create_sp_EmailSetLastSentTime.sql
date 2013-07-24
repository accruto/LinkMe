IF EXISTS (SELECT * FROM dbo.SYSOBJECTS WHERE id = object_id('EmailSetLastSentTime') AND OBJECTPROPERTY(id, 'IsProcedure') = 1)
BEGIN 
	DROP PROCEDURE EmailSetLastSentTime
END
GO

CREATE PROCEDURE dbo.EmailSetLastSentTime(@userId UNIQUEIDENTIFIER, @emailName NVARCHAR(100), @lastSentTime DATETIME)
AS
BEGIN
	SET NOCOUNT ON

	-- Check that the user exists.

	IF EXISTS (SELECT * FROM RegisteredUser WHERE id = @userId)
	BEGIN

		-- Make sure an EmailSettings entry exists for the user.

		DECLARE @settingsId UNIQUEIDENTIFIER

		SELECT @settingsId = id FROM dbo.EmailSettings WHERE userId = @userId

		IF (@settingsId IS NULL)
		BEGIN

			SET @settingsId = NEWID()
			INSERT
				dbo.EmailSettings (id, userId, sendPlainTextOnly)
			VALUES
				(@settingsId, @userId, 0)

		END

		-- Insert or update.

		UPDATE
			dbo.EmailDefinitionSettings
		SET
			lastSentTime = @lastSentTime
		FROM
			dbo.EmailDefinitionSettings AS eds
		INNER JOIN
			dbo.EmailSettings AS es ON es.id = eds.settingsId
		INNER JOIN
			dbo.EmailDefinition AS ed ON ed.id = eds.definitionId
		WHERE
			es.id = @settingsId
			AND ed.[name] = @emailName

		IF (@@ROWCOUNT = 0)
			INSERT
				dbo.EmailDefinitionSettings (id, definitionId, settingsId, lastSentTime)
			SELECT
				NEWID(), id, @settingsId, @lastSentTime
			FROM
				EmailDefinition
			WHERE
				[name] = @emailName

	END

END
GO