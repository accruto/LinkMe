if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[GetPostcodesWithinRadius]') and xtype in (N'FN', N'IF', N'TF'))
drop function [dbo].[GetPostcodesWithinRadius]
GO

-- Returns all the postcodes within a specified radius (in km) of the specified postcode. 
CREATE FUNCTION dbo.GetPostcodesWithinRadius(@centrePostcode AS VARCHAR(4), @radiusKm AS FLOAT)
RETURNS @PostcodesWithinRange TABLE (postcode VARCHAR(4))
AS
BEGIN
	DECLARE @latitude FLOAT
	DECLARE @longitude FLOAT

	SELECT @latitude = lat, @longitude = lon
	FROM linkme_owner.locality
	WHERE postcode = @centrePostcode

	IF NOT (@latitude IS NULL OR @longitude IS NULL)
	BEGIN
		-- Optimise by filtering using an approximate latitude and longitude range, as described at
		-- http://www.4guysfromrolla.com/webtech/040100-1.shtml
	
		DECLARE @latRange FLOAT
		DECLARE @lonRange FLOAT
		SET @latRange = @radiusKm / ((6076 / 5280) * 60 * 1.609344)
		SET @lonRange = @radiusKm / ABS(COS(@longitude * PI() / 180) * 6076 / 5280 * 60 * 1.609344)
	
		INSERT INTO @PostcodesWithinRange
		SELECT DISTINCT postcode
		FROM linkme_owner.locality
		WHERE lat >= @latitude - @latRange AND lat <= @latitude + @latRange
			AND lon >= @longitude - @lonRange AND lon <= @longitude + @lonRange
			AND dbo.GetDistanceBetweenCoordinatesInKm(lat, lon, @latitude, @longitude) <= @radiusKm
	END

	RETURN
END
GO

