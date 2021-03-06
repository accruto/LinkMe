IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[MemberSetModified]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[MemberSetModified]
GO

CREATE PROCEDURE [dbo].[MemberSetModified]
(
	@memberId UNIQUEIDENTIFIER
)
AS
BEGIN
	SET NOCOUNT ON

	-- Update or insert

	BEGIN TRANSACTION

	IF EXISTS (SELECT 1 FROM dbo.MemberIndexing WITH (UPDLOCK) WHERE memberId = @memberId)
	BEGIN

		UPDATE
			dbo.MemberIndexing
		SET
			modifiedTime = GETUTCDATE()
		WHERE
			memberId = @memberId

	END
	ELSE
	BEGIN

		INSERT
			dbo.MemberIndexing (memberId, modifiedTime)
		VALUES
			(@memberId, GETUTCDATE())

	END

	COMMIT TRANSACTION

END