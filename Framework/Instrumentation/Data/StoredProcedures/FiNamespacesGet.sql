-------------------------------------------------------------------------------
-- Version: 1.0.0.0
-------------------------------------------------------------------------------


-------------------------------------------------------------------------------
-- Drop

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[FiNamespacesGet]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[FiNamespacesGet]
GO

-------------------------------------------------------------------------------
-- Create

CREATE PROCEDURE dbo.FiNamespacesGet
(
	@parentFullName AS VARCHAR(512)
)
AS
BEGIN

SET NOCOUNT ON

IF @parentFullName IS NULL OR @parentFullName = ''
BEGIN
	SELECT fullName, enabledEvents, mixedEvents
	FROM dbo.FiNamespace
	WHERE parentId IS NULL
END
ELSE
BEGIN
	SELECT Child.fullName, Child.enabledEvents, Child.mixedEvents
	FROM dbo.FiNamespace AS Child
	INNER JOIN dbo.FiNamespace AS Parent
	ON Child.parentId = Parent.id
	WHERE Parent.fullName = @parentFullName
END
			
RETURN 0

END
GO
