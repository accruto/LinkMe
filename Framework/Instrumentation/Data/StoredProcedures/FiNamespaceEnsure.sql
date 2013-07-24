-------------------------------------------------------------------------------
-- Version: 1.0.0.0
-------------------------------------------------------------------------------


-------------------------------------------------------------------------------
-- Drop

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[FiNamespaceEnsure]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[FiNamespaceEnsure]
GO

-------------------------------------------------------------------------------
-- Create

-- Ensure that a namespace with the specified name exists and return its GUID.

CREATE PROCEDURE dbo.FiNamespaceEnsure
(
	@fullName AS VARCHAR(512),
	@id AS UNIQUEIDENTIFIER OUTPUT
)
AS
BEGIN

SET NOCOUNT ON

IF (@fullName = '')
BEGIN
	SET @id = NULL
	RETURN 0
END

SELECT @id = id
FROM dbo.FiNamespace
WHERE fullName = @fullName

IF @id IS NULL
BEGIN
	-- Namespace doesn't exist - create it.

	DECLARE @parentFullName VARCHAR(512)
	DECLARE @parentId AS UNIQUEIDENTIFIER

	SET @parentFullName = dbo.FiGetParentNameFromFullName(@fullName)

	-- Get the parent namespace GUID, creating the parent namespace if it doesn't already exist.

	IF (@parentFullName = '')
		SET @parentId = NULL -- This is a root namespace.
	ELSE
		EXEC dbo.FiNamespaceEnsure @parentFullName, @parentId OUTPUT

	-- Insert this namespace.

	SET @id = NEWID()

	INSERT INTO dbo.FiNamespace (id, fullName, parentId)
	VALUES (@id, @fullName, @parentId)
END

RETURN 0

END
GO
