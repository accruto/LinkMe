IF  EXISTS (SELECT * FROM sys.check_constraints WHERE object_id = OBJECT_ID(N'[dbo].[CK_OrganisationalUnit_displayName]') AND parent_object_id = OBJECT_ID(N'[dbo].[OrganisationalUnit]'))
ALTER TABLE [dbo].[OrganisationalUnit] DROP CONSTRAINT [CK_OrganisationalUnit_displayName]

IF  EXISTS (SELECT * FROM sys.check_constraints WHERE object_id = OBJECT_ID(N'[dbo].[CK_Organisation_displayName]') AND parent_object_id = OBJECT_ID(N'[dbo].[Organisation]'))
ALTER TABLE [dbo].[Organisation] DROP CONSTRAINT [CK_Organisation_displayName]

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[HaveDuplicateOrgUnitNames]') AND type in (N'FN', N'IF', N'TF', N'FS', N'FT'))
DROP FUNCTION [dbo].[HaveDuplicateOrgUnitNames]
GO

CREATE FUNCTION dbo.HaveDuplicateOrgUnitNames()
RETURNS bit
AS
BEGIN
	-- The OrganisationalUnit name must be unique within its parent
	-- OrganisationalUnit or, if it has no parent then unique among
	-- top-level OrganisationalUnits. This function returns 1 if a
	-- duplicate names exist, otherwise 0.

	DECLARE @retval BIT

	IF EXISTS
	(
		SELECT 'Duplicates names!'
		FROM dbo.OrganisationalUnit ou
		INNER JOIN dbo.Organisation o
		ON ou.[id] = o.[id]
		GROUP BY ou.parentId, o.displayName
		HAVING COUNT(*) > 1
	)
		SET @retval = 1;
	ELSE
		SET @retval = 0;

	RETURN @retval;
END
GO

ALTER TABLE dbo.OrganisationalUnit
WITH NOCHECK
ADD CONSTRAINT CK_OrganisationalUnit_displayName
CHECK (dbo.HaveDuplicateOrgUnitNames() = 0)
GO

ALTER TABLE dbo.OrganisationalUnit
CHECK CONSTRAINT CK_OrganisationalUnit_displayName
GO

ALTER TABLE dbo.Organisation
WITH NOCHECK
ADD CONSTRAINT CK_Organisation_displayName
CHECK (dbo.HaveDuplicateOrgUnitNames() = 0)
GO

ALTER TABLE dbo.Organisation
CHECK CONSTRAINT CK_Organisation_displayName
GO
