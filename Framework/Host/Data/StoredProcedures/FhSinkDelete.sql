-------------------------------------------------------------------------------
-- Version: 1.0.0.0
-------------------------------------------------------------------------------


-------------------------------------------------------------------------------
-- Drop

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[FhSinkDelete]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[FhSinkDelete]
GO

-------------------------------------------------------------------------------
-- Create

CREATE PROCEDURE dbo.FhSinkDelete
(
	@channelFullName AS VARCHAR(512),
	@name AS VARCHAR(128)
)
AS
BEGIN

SET NOCOUNT ON

DELETE dbo.FhSink
FROM dbo.FhSink AS S
INNER JOIN dbo.FhChannel AS C ON C.id = S.channelId
WHERE C.fullName = @channelFullName AND S.name = @name

RETURN 0

END
GO