CREATE FUNCTION [dbo].[MonthToDays365] (@month INT)
RETURNS INT

AS
-- Converts the given month (0-12) to the corresponding number of days into the year (by end of month)
-- this function is for non-leap years
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

CREATE FUNCTION [dbo].[MonthToDays366] (@month INT)
RETURNS INT

AS
-- converts the given month (0-12) to the corresponding number of days into the year (by end of month)
-- this function is for leap years
BEGIN 
RETURN
	CASE @month
		WHEN 0 THEN 0
		WHEN 1 THEN 31
		WHEN 2 THEN 60
		WHEN 3 THEN 91
		WHEN 4 THEN 121
		WHEN 5 THEN 152
		WHEN 6 THEN 182
		WHEN 7 THEN 213
		WHEN 8 THEN 244
		WHEN 9 THEN 274
		WHEN 10 THEN 305
		WHEN 11 THEN 335
		WHEN 12 THEN 366
		ELSE 0
	END
END

GO

CREATE FUNCTION [dbo].[MonthToDays] (@year INT, @month INT)
RETURNS INT

AS
-- converts the given month (0-12) to the corresponding number of days into the year (by end of month)
-- this function is for non-leap years
BEGIN 
RETURN 
	-- determine whether the given year is a leap year
	CASE 
		WHEN (@year % 4 = 0) and ((@year % 100  != 0) or ((@year % 100 = 0) and (@year % 400 = 0))) THEN dbo.MonthToDays366(@month)
		ELSE dbo.MonthToDays365(@month)
	END
END

GO

CREATE FUNCTION [dbo].[TimeToTicks] (@hour INT, @minute INT, @second INT)
RETURNS BIGINT

AS 
-- converts the given hour/minute/second to the corresponding ticks
BEGIN 
RETURN (((@hour * 3600) + CONVERT(bigint, @minute) * 60) + CONVERT(bigint, @second)) * 10000000
END

GO

CREATE FUNCTION [dbo].[DateToTicks] (@year INT, @month INT, @day INT)
RETURNS BIGINT

AS
-- converts the given year/month/day to the corresponding ticks
BEGIN 
RETURN CONVERT(BIGINT, (((((((@year - 1) * 365) + ((@year - 1) / 4)) - ((@year - 1) / 100)) + ((@year - 1) / 400)) + dbo.MonthToDays(@year, @month - 1)) + @day) - 1) * 864000000000;
END

GO

CREATE FUNCTION [dbo].[DateTimeToTicks] (@d DATETIME)
RETURNS BIGINT

AS
-- converts the given datetime to .NET-compatible ticks
-- see http://msdn.microsoft.com/library/default.asp?url=/library/en-us/cpref/html/frlrfsystemdatetimeclasstickstopic.asp
BEGIN 
RETURN 
	dbo.DateToTicks(DATEPART(yyyy, @d), DATEPART(mm, @d), DATEPART(dd, @d)) +
	dbo.TimeToTicks(DATEPART(hh, @d), DATEPART(mi, @d), DATEPART(ss, @d)) +
	(CONVERT(bigint, DATEPART(ms, @d)) * CONVERT(bigint,10000));
END

GO






