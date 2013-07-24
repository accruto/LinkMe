-------------------------------------------------------------------------------
-- Version: 1.0.0.0
-------------------------------------------------------------------------------


-------------------------------------------------------------------------------
-- Drop

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[FhNamespacesGet]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[FhNamespacesGet]
GO

-------------------------------------------------------------------------------
-- Create

CREATE PROCEDURE dbo.FhNamespacesGet
(
	@parentFullName AS VARCHAR(512)
)
AS
BEGIN

SET NOCOUNT ON

IF @parentFullName IS NULL OR @parentFullName = ''
BEGIN
	SELECT fullName
	FROM FhNamespace
	WHERE parentId IS NULL
END
ELSE
BEGIN
	SELECT Child.fullName
	FROM FhNamespace AS Child
	INNER JOIN FhNamespace AS Parent
	ON Child.parentId = Parent.id
	WHERE Parent.fullName = @parentFullName
END
			
RETURN 0

END
GO
