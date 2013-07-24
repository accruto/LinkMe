IF EXISTS (SELECT * FROM dbo.SYSOBJECTS WHERE id = object_id('RegisterEmailLinkClick') AND OBJECTPROPERTY(id, 'IsProcedure') = 1)
BEGIN 
	DROP PROCEDURE RegisterEmailLinkClick
END
GO

CREATE PROCEDURE dbo.RegisterEmailLinkClick(@emailId VARCHAR(50), @verticalId VARCHAR(50), @variationId VARCHAR(50), @file VARCHAR(50))
AS
BEGIN
	SET NOCOUNT ON

	BEGIN TRANSACTION

	declare @id uniqueidentifier
	set @id = (SELECT id FROM dbo.EmailStats WHERE emailId = @emailId AND verticalId = @verticalId AND variationId = @variationId)
 
	IF @id is not null
		BEGIN
			IF EXISTS (
					SELECT * FROM dbo.EmailLinkStats WHERE emailStatsId = @id AND [file] = @file
				)
				BEGIN
					UPDATE dbo.EmailLinkStats
					SET hits = hits + 1
					WHERE emailStatsId = @id 
					AND [file] = @file
				END
			ELSE
				BEGIN
					INSERT INTO dbo.EmailLinkStats (emailStatsId, [file], hits)
					VALUES (@id, @file, 1)
				END

			IF (@@ERROR <> 0)
				BEGIN
					ROLLBACK TRANSACTION
					RETURN
				END
		END
	ELSE
		BEGIN
			declare @message varchar(200)
			set @message = 'The email with emailId "' + @emailId + '", verticalId "' + @verticalId +
				'" and variationId "' + @variationId + '" does not exist.'
			RAISERROR(@message, 14, 1)

			ROLLBACK TRANSACTION
			RETURN
		END
	COMMIT TRANSACTION
END
GO