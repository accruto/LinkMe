-------------------------------------------------------------------------------
-- Version: 1.0.0.0
-------------------------------------------------------------------------------


-------------------------------------------------------------------------------
-- Drop

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[FiGetParentNameFromFullyQualifiedReference]') and xtype in (N'FN', N'IF', N'TF'))
drop function [dbo].[FiGetParentNameFromFullyQualifiedReference]
GO


-------------------------------------------------------------------------------
-- Create

CREATE FUNCTION dbo.FiGetParentNameFromFullyQualifiedReference
(
	@fullyQualifiedReference AS VARCHAR(512)
)
RETURNS VARCHAR(512)
AS
BEGIN 

DECLARE @reversed AS VARCHAR(512)
SET @reversed = REVERSE(@fullyQualifiedReference)

DECLARE @pos AS INT
SET @pos = CHARINDEX('.', @reversed, CHARINDEX(',', @reversed))

IF @pos = 0
	RETURN ''

RETURN LEFT(@fullyQualifiedReference, LEN(@fullyQualifiedReference) - @pos)

END
GO
