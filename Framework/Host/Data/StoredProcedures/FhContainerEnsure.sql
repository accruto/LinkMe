-------------------------------------------------------------------------------
-- Version: 1.0.0.0
-------------------------------------------------------------------------------


-------------------------------------------------------------------------------
-- Drop

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[FhContainerEnsure]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[FhContainerEnsure]
GO

-------------------------------------------------------------------------------
-- Create

-- Ensure that a namespace with the specified name exists and return its GUID.

CREATE PROCEDURE dbo.FhContainerEnsure
(
	@fullName AS VARCHAR(512),
	@id AS UNIQUEIDENTIFIER OUTPUT
)
AS
BEGIN

SET NOCOUNT ON

SELECT @id = id
FROM dbo.FhContainer
WHERE fullName = @fullName

IF @id IS NULL
BEGIN
	-- Container doesn't exist - create it.

	DECLARE @parentFullName VARCHAR(512)
	DECLARE @namespaceId AS UNIQUEIDENTIFIER

	SET @parentFullName = dbo.FhGetParentNameFromFullName(@fullName)

	-- Get the parent namespace GUID, creating the parent namespace if it doesn't already exist.

	EXEC dbo.FhNamespaceEnsure @parentFullName, @namespaceId OUTPUT

	-- Insert this namespace.

	SET @id = NEWID()

	INSERT INTO dbo.FhContainer (id, fullName, namespaceId)
	VALUES (@id, @fullName, @namespaceId)
END

RETURN 0

END
GO
