-------------------------------------------------------------------------------
-- Version: 1.0.0.0
-------------------------------------------------------------------------------


-------------------------------------------------------------------------------
-- Drop

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[FhContainerUpdate]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[FhContainerUpdate]
GO

-------------------------------------------------------------------------------
-- Create

CREATE PROCEDURE dbo.FhContainerUpdate
(
	@fullName AS VARCHAR(512),
	@description AS NVARCHAR(512)
)
AS
BEGIN

SET NOCOUNT ON

-- Try to find the component.

DECLARE @id AS UNIQUEIDENTIFIER

SELECT @id = id
FROM dbo.FhContainer
WHERE fullName = @fullName

IF @id IS NULL
BEGIN
	-- Container doesn't exist - insert it. First ensure the namespace exists.

	DECLARE @namespaceFullName AS VARCHAR(512)
	SET @namespaceFullName = dbo.FhGetParentNameFromFullName(@fullName)

	DECLARE @namespaceId AS UNIQUEIDENTIFIER
	EXEC dbo.FhNamespaceEnsure @namespaceFullName, @namespaceId OUTPUT

	INSERT INTO dbo.FhContainer (id, fullName, namespaceId, description)
	VALUES (NEWID(), @fullName, @namespaceId, @description)
END
ELSE
BEGIN
	-- Container exists - update it.

	UPDATE dbo.FhContainer
	SET
		description = @description
	WHERE id = @id
END

RETURN 0

END
GO