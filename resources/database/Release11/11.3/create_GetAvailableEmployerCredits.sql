IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[GetAvailableEmployerCredits]') AND type in (N'FN', N'IF', N'TF', N'FS', N'FT'))
DROP FUNCTION [dbo].[GetAvailableEmployerCredits]
GO

CREATE FUNCTION [dbo].[GetAvailableEmployerCredits](@ownerId AS UNIQUEIDENTIFIER, @creditId AS UNIQUEIDENTIFIER)
RETURNS INT
AS
BEGIN

	-- Check parent.

	DECLARE @parentOwnerId UNIQUEIDENTIFIER
	DECLARE @parentCredits INT

	IF EXISTS (SELECT * FROM dbo.Employer WHERE id = @ownerId)
		SELECT
			@parentOwnerId = organisationId
		FROM
			dbo.Employer
		WHERE
			id = @ownerId
	ELSE IF EXISTS (SELECT * FROM dbo.OrganisationalUnit WHERE id = @ownerId)
		SELECT
			@parentOwnerId = parentId
		FROM
			dbo.OrganisationalUnit
		WHERE
			id = @ownerId

	IF (NOT @parentOwnerId IS NULL)
		SET @parentCredits = dbo.GetAvailableEmployerCredits(@parentOwnerId, @creditId)
	ELSE
		SET @parentCredits = 0

	-- If a parent has unlimited credits then this owner has unlimited credits.

	IF (@parentCredits IS NULL)
		RETURN NULL;

	-- Look for any unlimited allocations.

	IF EXISTS
	(
		SELECT
			*
		FROM
			dbo.CreditAllocation
		WHERE
			ownerId = @ownerId
			AND creditId = @creditId
			AND (expiryDate IS NULL OR expiryDate > GETDATE())
			AND deallocatedTime IS NULL
			AND quantity IS NULL
	)
		RETURN NULL

	-- Add up all finite allocations.

	DECLARE @credits INT
	SELECT
		@credits = SUM(quantity)
	FROM
		dbo.CreditAllocation
	WHERE
		ownerId = @ownerId
		AND creditId = @creditId
		AND (expiryDate IS NULL OR expiryDate > GETDATE())
		AND deallocatedTime IS NULL

	IF @credits IS NULL
		RETURN @parentCredits

	RETURN @credits + @parentCredits
END
