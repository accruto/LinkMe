-------------------------------------------------------------------------------
-- Version: 1.0.0.0
-------------------------------------------------------------------------------


-------------------------------------------------------------------------------
-- Drop

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[FhSinksGet]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[FhSinksGet]
GO

-------------------------------------------------------------------------------
-- Create

CREATE PROCEDURE dbo.FhSinksGet
(
	@channelFullName AS VARCHAR(512)
)
AS
BEGIN

SET NOCOUNT ON

SELECT S.name, S.description, S.sinkType, S.configurationData
FROM dbo.FhSink AS S
INNER JOIN dbo.FhChannel AS C ON S.channelId = C.id
WHERE C.fullName = @channelFullName
ORDER BY S.[index]

RETURN 0

END
GO
