-------------------------------------------------------------------------------
-- Version: 1.0.0.0
-------------------------------------------------------------------------------


-------------------------------------------------------------------------------
-- Drop

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[FhSourceDelete]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[FhSourceDelete]
GO

-------------------------------------------------------------------------------
-- Create

CREATE PROCEDURE dbo.FhSourceDelete
(
	@fullName AS VARCHAR(512)
)
AS
BEGIN

SET NOCOUNT ON

DELETE FROM dbo.FhSource
WHERE fullName = @fullName

RETURN 0

END
GO