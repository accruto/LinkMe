-------------------------------------------------------------------------------
-- Version: 1.0.0.0
-------------------------------------------------------------------------------


-------------------------------------------------------------------------------
-- Drop

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TimeToTicks]') AND type in (N'FN', N'IF', N'TF', N'FS', N'FT'))
DROP FUNCTION [dbo].[TimeToTicks]
GO


-------------------------------------------------------------------------------
-- Create

CREATE FUNCTION [dbo].[TimeToTicks] (@hour int, @minute int, @second int)  
RETURNS bigint 

AS 
-- Converts a time of day into the number of ticks since the start of the day.
BEGIN 
RETURN (((@hour * 3600) + CONVERT(bigint, @minute) * 60) + CONVERT(bigint, @second)) * 10000000
END

GO

