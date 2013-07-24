-------------------------------------------------------------------------------
-- Version: 1.0.0.0
-------------------------------------------------------------------------------


-------------------------------------------------------------------------------
-- Drop

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[FhChannelsGet]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[FhChannelsGet]
GO

-------------------------------------------------------------------------------
-- Create

CREATE PROCEDURE dbo.FhChannelsGet
(
	@containerFullName AS VARCHAR(512)
)
AS
BEGIN

SET NOCOUNT ON

SELECT Ch.fullName, Ch.description, Ch.threadPoolName
FROM dbo.FhChannel AS Ch
INNER JOIN dbo.FhContainer AS C
ON Ch.containerId = C.id
WHERE C.fullName = @containerFullName

RETURN 0

END
GO
