-------------------------------------------------------------------------------
-- Version: 1.0.0.0
-------------------------------------------------------------------------------


-------------------------------------------------------------------------------
-- Drop

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[DateToTicks]') AND type in (N'FN', N'IF', N'TF', N'FS', N'FT'))
DROP FUNCTION [dbo].[DateToTicks]
GO


-------------------------------------------------------------------------------
-- Create

CREATE FUNCTION [dbo].[DateToTicks] (@year int, @month int, @day int)
RETURNS bigint

AS
-- Converts a date into the number of ticks since January 1, 0001
BEGIN 
RETURN CONVERT(bigint, (((((((@year - 1) * 365) + ((@year - 1) / 4)) - ((@year - 1) / 100)) + ((@year - 1) / 400)) + dbo.AggregateDays(@year, @month - 1)) + @day) - 1) * 864000000000;
END

GO

