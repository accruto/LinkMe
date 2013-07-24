-------------------------------------------------------------------------------
-- Version: 1.0.0.0
-------------------------------------------------------------------------------


-------------------------------------------------------------------------------
-- Drop

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[AggregateDaysNonLeapYear]') AND type in (N'FN', N'IF', N'TF', N'FS', N'FT'))
DROP FUNCTION [dbo].[AggregateDaysNonLeapYear]
GO


-------------------------------------------------------------------------------
-- Create

CREATE FUNCTION [dbo].[AggregateDaysNonLeapYear] (@month int)
RETURNS int

AS
-- Returns the total number of days upto and including the provided month from the start of the year,
-- for non-leap years
BEGIN 
RETURN
	CASE @month
		WHEN 0 THEN 0
		WHEN 1 THEN 31
		WHEN 2 THEN 59
		WHEN 3 THEN 90
		WHEN 4 THEN 120
		WHEN 5 THEN 151
		WHEN 6 THEN 181
		WHEN 7 THEN 212
		WHEN 8 THEN 243
		WHEN 9 THEN 273
		WHEN 10 THEN 304
		WHEN 11 THEN 334
		WHEN 12 THEN 365
		ELSE 0
	END
END

GO

