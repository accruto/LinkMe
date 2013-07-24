-------------------------------------------------------------------------------
-- Version: 1.0.0.0
-------------------------------------------------------------------------------


-------------------------------------------------------------------------------
-- Drop

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[FiSourceDelete]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[FiSourceDelete]
GO

-------------------------------------------------------------------------------
-- Create

CREATE PROCEDURE dbo.FiSourceDelete
(
	@fullyQualifiedReference AS VARCHAR(512)
)
AS
BEGIN

SET NOCOUNT ON

-- Delete source.

DELETE FROM dbo.FiSource
WHERE fullyQualifiedReference = @fullyQualifiedReference

RETURN 0

END
GO