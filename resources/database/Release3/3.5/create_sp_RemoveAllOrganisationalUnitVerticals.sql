IF EXISTS (SELECT * FROM dbo.SYSOBJECTS WHERE id = object_id('RemoveAllOrganisationalUnitVerticals') AND OBJECTPROPERTY(id, 'IsProcedure') = 1)
BEGIN 
	DROP PROCEDURE RemoveAllOrganisationalUnitVerticals
END
GO

CREATE PROCEDURE dbo.RemoveAllOrganisationalUnitVerticals(@organisationalUnitId UNIQUEIDENTIFIER)
AS
BEGIN
	SET NOCOUNT ON

	-- Remove all associations.

	DELETE
		VerticalAssociation
	WHERE
		organisationalUnitId = @organisationalUnitId

	-- Remove it.

	DELETE
		VerticalOrganisationalUnit
	WHERE
		id = @organisationalUnitId

END
GO