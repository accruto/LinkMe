-------------------------------------------------------------------------------
-- Version: 1.0.0.0
-------------------------------------------------------------------------------


-------------------------------------------------------------------------------
-- Drop

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[FiNamespaceDelete]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[FiNamespaceDelete]
GO

-------------------------------------------------------------------------------
-- Create

CREATE PROCEDURE dbo.FiNamespaceDelete
(
	@fullName AS VARCHAR(512)
)
AS
BEGIN

SET NOCOUNT ON

DECLARE @error AS INT
DECLARE @namespacePattern AS VARCHAR(514)
SET @namespacePattern = @fullName + '.%'

BEGIN TRANSACTION

-- Delete sources.

DELETE FROM dbo.FiSource
WHERE fullyQualifiedReference LIKE @namespacePattern

SET @error = @@ERROR
IF @error != 0
BEGIN
	ROLLBACK TRANSACTION
	RETURN @error
END

-- Delete child namespaces.

DELETE FROM dbo.FiNamespace
WHERE fullName LIKE @namespacePattern

SET @error = @@ERROR
IF @error != 0
BEGIN
	ROLLBACK TRANSACTION
	RETURN @error
END

-- Delete this namespace.

DELETE FROM dbo.FiNamespace
WHERE fullName = @fullName

SET @error = @@ERROR
IF @error != 0
BEGIN
	ROLLBACK TRANSACTION
	RETURN @error
END

COMMIT TRANSACTION
RETURN 0

END
GO