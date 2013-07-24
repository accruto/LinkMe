IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[CreateOfferProvider]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[CreateOfferProvider]
GO

CREATE PROCEDURE [dbo].[CreateOfferProvider](@providerId UNIQUEIDENTIFIER, @providerName NVARCHAR(500))
AS
BEGIN

	SET NOCOUNT ON

	IF NOT EXISTS (SELECT * FROM OfferProvider WHERE id = @providerId)
		INSERT
			OfferProvider (id, name, enabled)
		VALUES
			(@providerId, @providerName, 1)
	ELSE
		UPDATE
			OfferProvider
		SET
			name = @providerName,
			enabled = 1
		WHERE
			id = @providerId

END