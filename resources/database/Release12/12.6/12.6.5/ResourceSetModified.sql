IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ResourceSetModified]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[ResourceSetModified]
GO

CREATE PROCEDURE [dbo].[ResourceSetModified]
(
	@resourceId UNIQUEIDENTIFIER
)
AS
BEGIN
	SET NOCOUNT ON

	-- Update or insert

	BEGIN TRANSACTION

	IF EXISTS (SELECT 1 FROM dbo.ResourceIndexing WITH (UPDLOCK) WHERE resourceId = @resourceId)
	BEGIN

		UPDATE
			dbo.ResourceIndexing
		SET
			modifiedTime = GETDATE()
		WHERE
			resourceId = @resourceId

	END
	ELSE
	BEGIN

		INSERT
			dbo.ResourceIndexing (resourceId, modifiedTime)
		VALUES
			(@resourceId, GETDATE())

	END

	COMMIT TRANSACTION

END