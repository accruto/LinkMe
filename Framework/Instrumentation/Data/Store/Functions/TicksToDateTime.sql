-------------------------------------------------------------------------------
-- Version: 1.0.0.0
-------------------------------------------------------------------------------


-------------------------------------------------------------------------------
-- Drop

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TicksToDateTime]') AND type in (N'FN', N'IF', N'TF', N'FS', N'FT'))
DROP FUNCTION [dbo].[TicksToDateTime]
GO


-------------------------------------------------------------------------------
-- Create

CREATE FUNCTION [dbo].[TicksToDateTime] (@ticks BIGINT)
RETURNS DATETIME

AS
-- Converts the number of ticks since since 12:00 A.M., January 1, 0001 (compatible with .NET DateTime.Ticks) into a datetime.
BEGIN

DECLARE @days BIGINT
DECLARE @daysBefore1753 BIGINT
DECLARE @timeTicks BIGINT
DECLARE @seconds BIGINT

SET @days = @ticks / CONVERT(BIGINT,864000000000)
SET @daysBefore1753 = CONVERT(BIGINT,639905)
SET @timeTicks = @ticks % CONVERT(BIGINT,864000000000)
SET @seconds = @timeTicks / CONVERT(BIGINT,10000000)

RETURN DATEADD(s, @seconds, DATEADD(d, @days - @daysBefore1753, CONVERT(DATETIME,'1/1/1753')))
END 

GO
