-------------------------------------------------------------------------------
-- Version: 1.0.0.0
-------------------------------------------------------------------------------


-------------------------------------------------------------------------------
-- Drop

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[FiEventTypeDelete]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[FiEventTypeDelete]
GO

-------------------------------------------------------------------------------
-- Create

CREATE PROCEDURE dbo.FiEventTypeDelete
(
	@name AS VARCHAR(512)
)
AS
BEGIN

SET NOCOUNT ON

DELETE FROM dbo.FiEventType
WHERE name = @name

RETURN 0

END
GO