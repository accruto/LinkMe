if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[GetLocalitiesWithinRadius]') and xtype in (N'FN', N'IF', N'TF'))
drop function [dbo].[GetLocalitiesWithinRadius]
GO

-- Returns the IDs of Localities within a specified radius (in km) of the
-- specified Locality.

CREATE FUNCTION dbo.GetLocalitiesWithinRadius(@localityId AS INT, @radiusKm AS FLOAT)
RETURNS @LocalitiesWithinRadius TABLE (localityId INT)
AS
BEGIN
	DECLARE @latitude GeographicCoordinate
	DECLARE @longitude GeographicCoordinate

	SELECT @latitude = centroidLatitude, @longitude = centroidLongitude
	FROM dbo.Locality
	WHERE [id] = @localityId

	IF NOT (@latitude IS NULL OR @longitude IS NULL)
	BEGIN
		-- Optimise by filtering using an approximate latitude and longitude range, as described at
		-- http://www.4guysfromrolla.com/webtech/040100-1.shtml
	
		DECLARE @latRange GeographicCoordinate
		DECLARE @lonRange GeographicCoordinate
		SET @latRange = @radiusKm / ((6076 / 5280) * 60 * 1.609344)
		SET @lonRange = @radiusKm / ABS(COS(@longitude * PI() / 180) * 6076 / 5280 * 60 * 1.609344)

		INSERT INTO @LocalitiesWithinRadius
		SELECT [id]
		FROM dbo.Locality
		WHERE centroidLatitude >= @latitude - @latRange AND centroidLatitude <= @latitude + @latRange
			AND centroidLongitude >= @longitude - @lonRange AND centroidLongitude <= @longitude + @lonRange
			AND dbo.GetDistanceBetweenCoordinatesInKm(centroidLatitude, centroidLongitude, @latitude, @longitude) <= @radiusKm
	END

	RETURN
END
GO
