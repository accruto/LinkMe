-------------------------------------------------------------------------------
-- Version: 1.0.0.0
-------------------------------------------------------------------------------


-------------------------------------------------------------------------------
-- Drop

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[FiSourcesGet]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[FiSourcesGet]
GO

-------------------------------------------------------------------------------
-- Create

CREATE PROCEDURE dbo.FiSourcesGet
(
	@namespaceFullName AS VARCHAR(512)
)
AS
BEGIN

SET NOCOUNT ON

SELECT S.fullyQualifiedReference, S.enabledEvents, S.sourceType
FROM dbo.FiSource AS S
INNER JOIN dbo.FiNamespace AS N
ON S.namespaceId = N.id
WHERE N.fullName = @namespaceFullName

RETURN 0

END
GO
