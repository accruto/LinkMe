IF EXISTS (SELECT * FROM dbo.SYSOBJECTS WHERE id = object_id('RegisterEmailSend') AND OBJECTPROPERTY(id, 'IsProcedure') = 1)
BEGIN 
	DROP PROCEDURE RegisterEmailSend
END
GO

CREATE PROCEDURE dbo.RegisterEmailSend(@emailId VARCHAR(50), @verticalId VARCHAR(50), @variationId VARCHAR(50))
AS
BEGIN
	SET NOCOUNT ON

	BEGIN TRANSACTION
-- Does the row exist?
	IF EXISTS (SELECT * FROM dbo.EmailStats WHERE emailId = @emailId AND verticalId = @verticalId AND variationId = @variationId)
		-- If so, update it by adding 1 send
		BEGIN
			UPDATE dbo.EmailStats
			SET sends = sends + 1
			WHERE emailId = @emailId 
			AND verticalId = @verticalId 
			AND variationId = @variationId 
		END
	ELSE
		-- If not, add a row with the appropriate details
		BEGIN
			INSERT INTO dbo.EmailStats (id, emailId, verticalId, variationId, sends)
			VALUES (NEWID(), @emailId, @verticalId, @variationId, 1)
		END

	IF (@@ERROR <> 0)
		BEGIN
			ROLLBACK TRANSACTION
			RETURN
		END
	COMMIT TRANSACTION
END
GO