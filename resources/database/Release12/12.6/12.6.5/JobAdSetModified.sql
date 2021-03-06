IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[JobAdSetModified]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[JobAdSetModified]
GO

CREATE PROCEDURE [dbo].[JobAdSetModified]
(
	@jobAdId UNIQUEIDENTIFIER
)
AS
BEGIN
	SET NOCOUNT ON

	-- Update or insert

	BEGIN TRANSACTION

	IF EXISTS (SELECT 1 FROM dbo.JobAdIndexing WITH (UPDLOCK) WHERE jobAdId = @jobAdId)
	BEGIN

		UPDATE
			dbo.JobAdIndexing
		SET
			modifiedTime = GETDATE()
		WHERE
			jobAdId = @jobAdId

	END
	ELSE
	BEGIN

		INSERT
			dbo.JobAdIndexing (jobAdId, modifiedTime)
		VALUES
			(@jobAdId, GETDATE())

	END

	COMMIT TRANSACTION

END