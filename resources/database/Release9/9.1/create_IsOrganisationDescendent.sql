IF  EXISTS (SELECT * FROM sys.check_constraints WHERE object_id = OBJECT_ID(N'[dbo].CK_OrganisationalUnit_circularHierarchy') AND parent_object_id = OBJECT_ID(N'[dbo].[OrganisationalUnit]'))
ALTER TABLE [dbo].[OrganisationalUnit] DROP CONSTRAINT CK_OrganisationalUnit_circularHierarchy
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].IsOrganisationDescendent') AND type in (N'FN', N'IF', N'TF', N'FS', N'FT'))
DROP FUNCTION [dbo].IsOrganisationDescendent
GO

CREATE FUNCTION dbo.IsOrganisationDescendent(@parentId AS UNIQUEIDENTIFIER,	@childId AS UNIQUEIDENTIFIER)
RETURNS BIT
AS
BEGIN
	IF (@parentId IS NULL OR @childId IS NULL)
		RETURN NULL

	-- Semantically whether a company is its own "descendent" is much of a muchness,
	-- but this simplifies the CHECK constraint.
	IF (@parentId = @childId)
		RETURN 1

	DECLARE @nextUpId UNIQUEIDENTIFIER

	SELECT @nextUpId = parentId
	FROM dbo.OrganisationalUnit
	WHERE [id] = @childId

	IF (@nextUpId IS NULL)
		RETURN 0
	IF (@nextUpId = @parentId)
		RETURN 1

	RETURN dbo.IsOrganisationDescendent(@parentId, @nextUpId)
END
GO

ALTER TABLE dbo.OrganisationalUnit
WITH NOCHECK
ADD CONSTRAINT CK_OrganisationalUnit_circularHierarchy
CHECK (dbo.IsOrganisationDescendent([id], parentId) = 0)
GO

ALTER TABLE dbo.OrganisationalUnit
CHECK CONSTRAINT CK_OrganisationalUnit_circularHierarchy
GO
