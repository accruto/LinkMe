if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[GetLatestDate]') and xtype in (N'FN', N'IF', N'TF'))
drop function [dbo].[GetLatestDate]
GO

CREATE FUNCTION dbo.GetLatestDate(@date1 AS DATETIME, @date2 AS DATETIME, @date3 AS DATETIME)
RETURNS DATETIME AS  
BEGIN
	-- Returns the latest of the specified times, some of which may be NULL.

	DECLARE @latest AS DATETIME
	SET @latest = @date1
	
	IF (@latest IS NULL OR @date2 > @latest)
		SET @latest = @date2

	IF (@latest IS NULL OR @date3 > @latest)
		SET @latest = @date3

	RETURN @latest
END
