IF EXISTS (SELECT * FROM dbo.SYSOBJECTS WHERE id = object_id('EmailSetSendPlainTextOnly') AND OBJECTPROPERTY(id, 'IsProcedure') = 1)
BEGIN 
	DROP PROCEDURE EmailSetSendPlainTextOnly
END
GO

CREATE PROCEDURE dbo.EmailSetSendPlainTextOnly(@userId UNIQUEIDENTIFIER, @sendPlainTextOnly BIT)
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
				(@settingsId, @userId, @sendPlainTextOnly)

		END
		ELSE
		BEGIN

			UPDATE
				EmailSettings
			SET
				sendPlainTextOnly = @sendPlainTextOnly
			WHERE
				id = @settingsId

		END

	END

END
GO