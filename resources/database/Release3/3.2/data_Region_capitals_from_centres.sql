CREATE PROCEDURE dbo.RegionFromCentreAndRadius(@postcode VARCHAR(8), @radiusKm INT, @regionId INT)
AS
BEGIN
	DECLARE @localityId INT

	SELECT @localityId = localityId
	FROM PostalCode
	WHERE postcode = @postcode

	DELETE dbo.LocalityRegion
	WHERE regionId = @regionId
	
	INSERT INTO dbo.LocalityRegion(regionId, localityId)
	SELECT @regionId, localityId
	FROM dbo.GetLocalitiesWithinRadius(@localityId, @radiusKm)
	ORDER BY localityId
END
GO

EXEC dbo.RegionFromCentreAndRadius '5000', 50, 2812
EXEC dbo.RegionFromCentreAndRadius '4000', 50, 2811
EXEC dbo.RegionFromCentreAndRadius '2600', 30, 2808
EXEC dbo.RegionFromCentreAndRadius '0800', 30, 2810
EXEC dbo.RegionFromCentreAndRadius '7000', 40, 2813
EXEC dbo.RegionFromCentreAndRadius '3000', 50, 2814
EXEC dbo.RegionFromCentreAndRadius '2000', 50, 2809
EXEC dbo.RegionFromCentreAndRadius '6000', 50, 2815

DROP PROCEDURE dbo.RegionFromCentreAndRadius
GO
