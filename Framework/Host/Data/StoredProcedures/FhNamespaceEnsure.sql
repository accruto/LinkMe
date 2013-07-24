-------------------------------------------------------------------------------
-- Version: 1.0.0.0
-------------------------------------------------------------------------------


-------------------------------------------------------------------------------
-- Drop

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[FhNamespaceEnsure]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[FhNamespaceEnsure]
GO

-------------------------------------------------------------------------------
-- Create

-- Ensure that a namespace with the specified name exists and return its GUID.

CREATE PROCEDURE dbo.FhNamespaceEnsure
(
	@fullName AS VARCHAR(512),
	@id AS UNIQUEIDENTIFIER OUTPUT
)
AS
BEGIN

SET NOCOUNT ON

SELECT @id = id
FROM dbo.FhNamespace
WHERE fullName = @fullName

IF @id IS NULL
BEGIN
	-- Namespace doesn't exist - create it.

	DECLARE @parentFullName VARCHAR(512)
	DECLARE @parentId AS UNIQUEIDENTIFIER

	SET @parentFullName = dbo.FhGetParentNameFromFullName(@fullName)

	-- Get the parent namespace GUID, creating the parent namespace if it doesn't already exist.

	IF (@parentFullName = '')
		SET @parentId = NULL -- This is a root namespace.
	ELSE
		EXEC dbo.FhNamespaceEnsure @parentFullName, @parentId OUTPUT

	-- Insert this namespace.

	SET @id = NEWID()

	INSERT INTO dbo.FhNamespace (id, fullName, parentId)
	VALUES (@id, @fullName, @parentId)
END

RETURN 0

END
GO
