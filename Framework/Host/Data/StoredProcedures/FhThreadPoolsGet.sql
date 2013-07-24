-------------------------------------------------------------------------------
-- Version: 1.0.0.0
-------------------------------------------------------------------------------


-------------------------------------------------------------------------------
-- Drop

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[FhThreadPoolsGet]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[FhThreadPoolsGet]
GO

-------------------------------------------------------------------------------
-- Create

CREATE PROCEDURE dbo.FhThreadPoolsGet
(
	@containerFullName AS VARCHAR(512)
)
AS
BEGIN

SET NOCOUNT ON

SELECT T.fullName, T.description, T.threadPoolType, T.configurationData
FROM dbo.FhThreadPool AS T
INNER JOIN dbo.FhContainer AS C
ON T.containerId = C.id
WHERE C.fullName = @containerFullName

RETURN 0

END
GO
