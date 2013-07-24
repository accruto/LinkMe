IF EXISTS (SELECT * FROM dbo.SYSOBJECTS WHERE id = object_id('SetOrganisationalUnitPrimaryVertical') AND OBJECTPROPERTY(id, 'IsProcedure') = 1)
BEGIN 
	DROP PROCEDURE SetOrganisationalUnitPrimaryVertical
END
GO

IF EXISTS (SELECT * FROM dbo.SYSOBJECTS WHERE id = object_id('SetOrganisationalUnitPrimaryCommunity') AND OBJECTPROPERTY(id, 'IsProcedure') = 1)
BEGIN 
	DROP PROCEDURE SetOrganisationalUnitPrimaryCommunity
END
GO

CREATE PROCEDURE dbo.SetOrganisationalUnitPrimaryCommunity(@organisationalUnitId UNIQUEIDENTIFIER, @communityId UNIQUEIDENTIFIER, @approved INT)
AS
BEGIN
	SET NOCOUNT ON

	-- CommunityOrganisationalUnit first, making sure it is restricted.

	DECLARE @flags TINYINT
	SET @flags = 1

	IF EXISTS (SELECT * FROM CommunityOrganisationalUnit WHERE id = @organisationalUnitId)
		UPDATE
			CommunityOrganisationalUnit
		SET
			flags = @flags
		WHERE
			id = @organisationalUnitId
	ELSE
		INSERT
			CommunityOrganisationalUnit (id, flags)
		VALUES
			(@organisationalUnitId, @flags)

	-- Remove any other associations.

	DELETE
		CommunityAssociation
	WHERE
		organisationalUnitId = @organisationalUnitId

	-- Make sure this association is set.

	IF (@approved <> 0)
		SET @flags = 3
	ELSE
		SET @flags = 2

	INSERT
		CommunityAssociation (organisationalUnitId, communityId, flags)
	VALUES
		(@organisationalUnitId, @communityId, @flags)

END
GO