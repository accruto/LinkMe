IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[GetOrganisationFullName]') AND type in (N'FN', N'IF', N'TF', N'FS', N'FT'))
DROP FUNCTION [dbo].[GetOrganisationFullName]
GO

CREATE FUNCTION [dbo].[GetOrganisationFullName](@id AS UNIQUEIDENTIFIER,
	@separator AS NCHAR)
RETURNS CompanyName
AS
BEGIN
	DECLARE @displayName CompanyName
	DECLARE @parentId UNIQUEIDENTIFIER

	IF (@id IS NULL)
		RETURN NULL

	SELECT @displayName = displayName, @parentId = parentId
	FROM dbo.Organisation o
	LEFT OUTER JOIN dbo.OrganisationalUnit ou
	ON o.[id] = ou.[id]
	WHERE o.[id] = @id

	IF (@displayName IS NULL)
		RETURN NULL

	IF (@parentId IS NULL)
		RETURN @displayName

	IF (@separator IS NULL)
		SET @separator = NCHAR(8594)

	-- The separator character is also defined as OrganisationUnit._fullNameDbSeparator
	RETURN dbo.GetOrganisationFullName(@parentId, @separator) + @separator + @displayName
END
GO

GRANT EXECUTE ON [dbo].[GetOrganisationFullName] TO public
GO
