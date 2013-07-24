IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].GetOrganisationRootId') AND type in (N'FN', N'IF', N'TF', N'FS', N'FT'))
DROP FUNCTION [dbo].GetOrganisationRootId
GO

CREATE FUNCTION dbo.GetOrganisationRootId(@id AS UNIQUEIDENTIFIER)
RETURNS UNIQUEIDENTIFIER
AS
BEGIN
	DECLARE @parentId UNIQUEIDENTIFIER

	IF (@id IS NULL)
		RETURN NULL

	SELECT @parentId = parentId
	FROM dbo.OrganisationalUnit
	WHERE [id] = @id

	IF (@parentId IS NULL)
		RETURN @id

	RETURN dbo.GetOrganisationRootId(@parentId)
END
GO
