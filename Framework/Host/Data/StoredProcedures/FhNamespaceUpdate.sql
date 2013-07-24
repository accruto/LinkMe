-------------------------------------------------------------------------------
-- Version: 1.0.0.0
-------------------------------------------------------------------------------


-------------------------------------------------------------------------------
-- Drop

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[FhNamespaceUpdate]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[FhNamespaceUpdate]
GO

-------------------------------------------------------------------------------
-- Create

CREATE PROCEDURE dbo.FhNamespaceUpdate
(
	@fullName AS VARCHAR(512)
)
AS
BEGIN

SET NOCOUNT ON

-- A namespaces has no details, so just ensure it exists.

DECLARE @id AS UNIQUEIDENTIFIER
EXEC dbo.FhNamespaceEnsure @fullName, @id OUTPUT

RETURN 0

END
GO
