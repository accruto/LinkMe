IF EXISTS (SELECT * FROM dbo.SYSOBJECTS WHERE id = object_id('EmailSetCategorySuppress') AND OBJECTPROPERTY(id, 'IsProcedure') = 1)
BEGIN 
	DROP PROCEDURE EmailSetCategorySuppress
END
GO

CREATE PROCEDURE dbo.EmailSetCategorySuppress
(
	@userId UNIQUEIDENTIFIER,
	@categoryId UNIQUEIDENTIFIER,
	@suppress BIT
)
AS
BEGIN
	SET NOCOUNT ON

	-- Check that the user exists.

	IF EXISTS (SELECT * FROM RegisteredUser WHERE id = @userId)
	BEGIN

		-- Check that the category exists.

		IF EXISTS (SELECT * FROM EmailCategory WHERE id = @categoryId)
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

			-- Make sure an EmailSettingsCategory entry exists for the user.

			IF NOT EXISTS (SELECT * FROM EmailCategorySettings WHERE settingsId = @settingsId AND categoryId = @categoryId)

				INSERT
					dbo.EmailCategorySettings (id, categoryId, settingsId, suppress)
				VALUES
					(NEWID(), @categoryId, @settingsId, @suppress)

			ELSE

				UPDATE
					EmailCategorySettings
				SET
					suppress = @suppress
				WHERE
					settingsId = @settingsId
					AND categoryId = @categoryId

		END

	END

END
GO