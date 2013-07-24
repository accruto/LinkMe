-------------------------------------------------------------------------------
-- Version: 1.0.0.0
-------------------------------------------------------------------------------


-------------------------------------------------------------------------------
-- Drop

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[FhContainersGet]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[FhContainersGet]
GO

-------------------------------------------------------------------------------
-- Create

CREATE PROCEDURE dbo.FhContainersGet
(
	@namespaceFullName AS VARCHAR(512)
)
AS
BEGIN

SET NOCOUNT ON

SELECT C.fullName, C.description
FROM dbo.FhContainer AS C
INNER JOIN dbo.FhNamespace AS N
ON C.namespaceId = N.id
WHERE N.fullName = @namespaceFullName

RETURN 0

END
GO
