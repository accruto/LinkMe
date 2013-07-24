-------------------------------------------------------------------------------
-- Version: 1.0.0.0
-------------------------------------------------------------------------------


-------------------------------------------------------------------------------
-- Drop

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[FhSourceTypeDelete]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[FhSourceTypeDelete]
GO

-------------------------------------------------------------------------------
-- Create

CREATE PROCEDURE dbo.FhSourceTypeDelete
(
	@name AS VARCHAR(128)
)
AS
BEGIN

SET NOCOUNT ON

DELETE FROM dbo.FhSourceType
WHERE name = @name

RETURN 0

END
GO