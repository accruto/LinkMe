-------------------------------------------------------------------------------
-- Version: 1.0.0.0
-------------------------------------------------------------------------------


-------------------------------------------------------------------------------
-- Drop

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[FhGetParentNameFromFullName]') and xtype in (N'FN', N'IF', N'TF'))
drop function [dbo].[FhGetParentNameFromFullName]
GO


-------------------------------------------------------------------------------
-- Create

CREATE FUNCTION dbo.FhGetParentNameFromFullName
(
	@fullName AS VARCHAR(512)
)
RETURNS VARCHAR(512)
AS
BEGIN 

DECLARE @pos AS INT
SET @pos = CHARINDEX('.', REVERSE(@fullName))

IF @pos = 0
	RETURN ''

RETURN LEFT(@fullName, LEN(@fullName) - @pos)

END
GO
