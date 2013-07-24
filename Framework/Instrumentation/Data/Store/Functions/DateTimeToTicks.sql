-------------------------------------------------------------------------------
-- Version: 1.0.0.0
-------------------------------------------------------------------------------


-------------------------------------------------------------------------------
-- Drop

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[DateTimeToTicks]') AND type in (N'FN', N'IF', N'TF', N'FS', N'FT'))
DROP FUNCTION [dbo].[DateTimeToTicks]
GO


-------------------------------------------------------------------------------
-- Create

CREATE FUNCTION [dbo].[DateTimeToTicks] (@dt datetime)
RETURNS bigint

AS
-- Converts a date into the number of ticks since 12:00 A.M., January 1, 0001 (compatible with .NET DateTime.Ticks).
BEGIN 
RETURN 
	dbo.DateToTicks(DATEPART(yyyy, @dt), DATEPART(mm, @dt), DATEPART(dd, @dt)) +
	dbo.TimeToTicks(DATEPART(hh, @dt), DATEPART(mi, @dt), DATEPART(ss, @dt)) +
	(CONVERT(bigint, DATEPART(ms, @dt)) * CONVERT(bigint,10000));
END

GO

