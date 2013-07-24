-------------------------------------------------------------------------------
-- Version: 1.0.0.0
-------------------------------------------------------------------------------


-------------------------------------------------------------------------------
-- Drop

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[FhSourcesGet]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[FhSourcesGet]
GO

-------------------------------------------------------------------------------
-- Create

CREATE PROCEDURE dbo.FhSourcesGet
(
	@containerFullName AS VARCHAR(512)
)
AS
BEGIN

SET NOCOUNT ON

SELECT S.fullName, S.description, S.sourceType, S.channelName, S.configurationData
FROM dbo.FhSource AS S
INNER JOIN dbo.FhContainer AS C
ON S.containerId = C.id
WHERE C.fullName = @containerFullName

RETURN 0

END
GO
