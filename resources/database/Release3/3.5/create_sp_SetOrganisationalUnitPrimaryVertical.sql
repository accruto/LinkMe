IF EXISTS (SELECT * FROM dbo.SYSOBJECTS WHERE id = object_id('SetOrganisationalUnitPrimaryVertical') AND OBJECTPROPERTY(id, 'IsProcedure') = 1)
BEGIN 
	DROP PROCEDURE SetOrganisationalUnitPrimaryVertical
END
GO

CREATE PROCEDURE dbo.SetOrganisationalUnitPrimaryVertical(@organisationalUnitId UNIQUEIDENTIFIER, @verticalId UNIQUEIDENTIFIER, @approved INT)
AS
BEGIN
	SET NOCOUNT ON

	-- VerticalOrganisationalUnit first, making sure it is restricted.

	DECLARE @flags TINYINT
	SET @flags = 1

	IF EXISTS (SELECT * FROM VerticalOrganisationalUnit WHERE id = @organisationalUnitId)
		UPDATE
			VerticalOrganisationalUnit
		SET
			flags = @flags
		WHERE
			id = @organisationalUnitId
	ELSE
		INSERT
			VerticalOrganisationalUnit (id, flags)
		VALUES
			(@organisationalUnitId, @flags)

	-- Remove any other associations.

	DELETE
		VerticalAssociation
	WHERE
		organisationalUnitId = @organisationalUnitId

	-- Make sure this association is set.

	IF (@approved <> 0)
		SET @flags = 3
	ELSE
		SET @flags = 2

	INSERT
		VerticalAssociation (organisationalUnitId, verticalId, flags)
	VALUES
		(@organisationalUnitId, @verticalId, @flags)

END
GO