-------------------------------------------------------------------------------
-- Version: 1.0.0.0
-------------------------------------------------------------------------------


-------------------------------------------------------------------------------
-- Drop

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[FcGetParentNameFromFullName]') and xtype in (N'FN', N'IF', N'TF'))
drop function [dbo].[FcGetParentNameFromFullName]
GO


-------------------------------------------------------------------------------
-- Create

CREATE FUNCTION dbo.FcGetParentNameFromFullName
(
	@fullname AS VARCHAR(512)
)
RETURNS VARCHAR(512)
AS
BEGIN 

DECLARE @pos AS INT
SET @pos = CHARINDEX('.', REVERSE(@fullname))

IF @pos = 0
	RETURN ''

RETURN LEFT(@fullname, LEN(@fullname) - @pos)

END
GO
