set ANSI_NULLS ON
set QUOTED_IDENTIFIER ON
go

ALTER PROCEDURE [dbo].[RegisterEmailSend](@emailId VARCHAR(50), @verticalId VARCHAR(50), @variationId VARCHAR(50))
AS
BEGIN
	SET NOCOUNT ON

	BEGIN TRANSACTION

	DECLARE @id UNIQUEIDENTIFIER
	
	SET @id = (SELECT id FROM dbo.EmailStats WHERE emailId = @emailId AND verticalId = @verticalId AND variationId = @variationId)
	-- Does the row exist?
	IF @id IS NULL
		-- If not, create it
		BEGIN
			SET @id = NEWID()
			INSERT INTO dbo.EmailStats (id, emailId, verticalId, variationId)
			VALUES (@id, @emailId, @verticalId, @variationId)
		END

	-- Add a row to EmailSendStats with the appropriate details
	INSERT INTO dbo.EmailSendStats (id, emailStatsId, time)
	VALUES (NEWID(), @id, GETDATE())

	IF (@@ERROR <> 0)
		BEGIN
			ROLLBACK TRANSACTION
			RETURN
		END
	COMMIT TRANSACTION
END
