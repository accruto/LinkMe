-------------------------------------------------------------------------------
-- Version: 1.0.0.0
-------------------------------------------------------------------------------


-------------------------------------------------------------------------------
-- Drop

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[AggregateDays]') AND type in (N'FN', N'IF', N'TF', N'FS', N'FT'))
DROP FUNCTION [dbo].[AggregateDays]
GO


-------------------------------------------------------------------------------
-- Create

CREATE FUNCTION [dbo].[AggregateDays] (@year int, @month int)
RETURNS int

AS
-- Returns the total number of days upto and including the provided month from the start of the year.
BEGIN 
RETURN

	-- Check whether the year is a leap year.

	CASE 
		WHEN (@year % 4 = 0) and ((@year % 100  != 0) or ((@year % 100 = 0) and (@year % 400 = 0))) THEN dbo.AggregateDaysLeapYear(@month)
		ELSE dbo.AggregateDaysNonLeapYear(@month)
	END
END

GO
