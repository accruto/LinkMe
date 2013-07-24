IF EXISTS (SELECT * FROM dbo.SYSOBJECTS WHERE id = object_id('EmailSetCategorySuppress') AND OBJECTPROPERTY(id, 'IsProcedure') = 1)
BEGIN 
	DROP PROCEDURE EmailSetCategorySuppress
END
GO

IF EXISTS (SELECT * FROM dbo.SYSOBJECTS WHERE id = object_id('CommunicationSetFrequency') AND OBJECTPROPERTY(id, 'IsProcedure') = 1)
BEGIN 
	DROP PROCEDURE CommunicationSetFrequency
END
GO

CREATE PROCEDURE dbo.CommunicationSetFrequency
(
	@userId UNIQUEIDENTIFIER,
	@categoryId UNIQUEIDENTIFIER,
	@frequency CommunicationFrequency
)
AS
BEGIN
	SET NOCOUNT ON

	-- Check that the user exists.

	IF EXISTS (SELECT * FROM RegisteredUser WHERE id = @userId)
	BEGIN

		-- Check that the category exists.

		IF EXISTS (SELECT * FROM CommunicationCategory WHERE id = @categoryId)
		BEGIN

			-- Make sure a CommunicationSettings entry exists for the user.

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

			-- Make sure a CommunicationSettingsCategory entry exists for the user.

			IF NOT EXISTS (SELECT * FROM CommunicationCategorySettings WHERE settingsId = @settingsId AND categoryId = @categoryId)

				INSERT
					dbo.CommunicationCategorySettings (id, categoryId, settingsId, frequency)
				VALUES
					(NEWID(), @categoryId, @settingsId, @frequency)

			ELSE

				UPDATE
					CommunicationCategorySettings
				SET
					frequency = @frequency
				WHERE
					settingsId = @settingsId
					AND categoryId = @categoryId

		END

	END

END
GO