IF EXISTS (SELECT * FROM dbo.SYSOBJECTS WHERE id = object_id('RemoveAllOrganisationalUnitVerticals') AND OBJECTPROPERTY(id, 'IsProcedure') = 1)
BEGIN 
	DROP PROCEDURE RemoveAllOrganisationalUnitVerticals
END
GO

IF EXISTS (SELECT * FROM dbo.SYSOBJECTS WHERE id = object_id('RemoveAllOrganisationalUnitCommunities') AND OBJECTPROPERTY(id, 'IsProcedure') = 1)
BEGIN 
	DROP PROCEDURE RemoveAllOrganisationalUnitCommunities
END
GO

CREATE PROCEDURE dbo.RemoveAllOrganisationalUnitCommunities(@organisationalUnitId UNIQUEIDENTIFIER)
AS
BEGIN
	SET NOCOUNT ON

	-- Remove all associations.

	DELETE
		CommunityAssociation
	WHERE
		organisationalUnitId = @organisationalUnitId

	-- Remove it.

	DELETE
		CommunityOrganisationalUnit
	WHERE
		id = @organisationalUnitId

END
GO