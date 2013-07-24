
CREATE PROCEDURE dbo.UpdatePostalCode(@displayName NVARCHAR(100), @latitude REAL, @longitude REAL)
AS
BEGIN

	DECLARE @localityId INT

	SELECT
		@localityId = L.id
	FROM
		dbo.Locality AS L
	INNER JOIN
		dbo.PostalCode AS PC ON PC.localityId = L.id
	INNER JOIN
		dbo.NamedLocation AS NL ON NL.id = PC.id
	WHERE
		NL.displayName = @displayName
		
	UPDATE
		dbo.Locality
	SET
		centroidLatitude = @latitude,
		centroidLongitude = @longitude
	WHERE
		id = @localityId
		
END
GO

CREATE PROCEDURE dbo.RemovePostalCodeFromRegion(@postalCode NVARCHAR(100), @region NVARCHAR(100))
AS
BEGIN

	DECLARE @localityId INT
	DECLARE @regionId INT

	SELECT
		@localityId = L.id
	FROM
		dbo.Locality AS L
	INNER JOIN
		dbo.PostalCode AS PC ON PC.localityId = L.id
	INNER JOIN
		dbo.NamedLocation AS NL ON NL.id = PC.id
	WHERE
		NL.displayName = @postalCode
		
	SELECT
		@regionId = R.id
	FROM
		dbo.Region AS R
	INNER JOIN
		dbo.NamedLocation AS NL ON NL.id = R.id
	WHERE
		NL.displayName = @region
		
	DELETE
		dbo.LocalityRegion
	WHERE
		localityId = @localityId AND regionId = @regionId
		
END
GO

-- Fix up some lat/longs

EXEC dbo.UpdatePostalCode '0840', -12.756590, 130.357518
EXEC dbo.UpdatePostalCode '0847', -13.822876, 131.834790
EXEC dbo.UpdatePostalCode '0861', -19.649844, 134.189667
EXEC dbo.UpdatePostalCode '0885', -13.852865, 136.422487
EXEC dbo.UpdatePostalCode '1220', -33.862600, 151.206747
EXEC dbo.UpdatePostalCode '1235', -33.874312, 151.205873
EXEC dbo.UpdatePostalCode '1240', -33.880777, 151.202796
EXEC dbo.UpdatePostalCode '1811', -33.835928, 151.047591
EXEC dbo.UpdatePostalCode '2015', -33.897075, 151.191130
EXEC dbo.UpdatePostalCode '2033', -33.909003, 151.223419
EXEC dbo.UpdatePostalCode '2042', -30.721541, 151.731619
EXEC dbo.UpdatePostalCode '2088', -33.829077, 151.244090
EXEC dbo.UpdatePostalCode '2097', -33.740969, 151.303133
EXEC dbo.UpdatePostalCode '2115', -33.814144, 151.054495
EXEC dbo.UpdatePostalCode '2144', -33.849322, 151.033421
EXEC dbo.UpdatePostalCode '2154', -33.732307, 151.005616
EXEC dbo.UpdatePostalCode '2167', -33.971305, 150.894517
EXEC dbo.UpdatePostalCode '2229', -34.040139, 151.122844
EXEC dbo.UpdatePostalCode '2267', -33.069169, 151.588552
EXEC dbo.UpdatePostalCode '2302', -32.927424, 151.767267
EXEC dbo.UpdatePostalCode '2308', -32.896446, 151.703191
EXEC dbo.UpdatePostalCode '2309', -33.539357, 151.241762
EXEC dbo.UpdatePostalCode '2314', -32.796297, 151.837996
EXEC dbo.UpdatePostalCode '2329', -31.506430, 150.642146
EXEC dbo.UpdatePostalCode '2335', -28.888546, 153.409839
EXEC dbo.UpdatePostalCode '2337', -32.050961, 150.867561
EXEC dbo.UpdatePostalCode '2347', -30.140940, 150.447100
EXEC dbo.UpdatePostalCode '2422', -31.922744, 151.780768
EXEC dbo.UpdatePostalCode '2428', -33.451762, 151.360909
EXEC dbo.UpdatePostalCode '2452', -30.352620, 153.090285
EXEC dbo.UpdatePostalCode '2463', -33.735174, 150.479149
EXEC dbo.UpdatePostalCode '2530', -34.494001, 150.793006
EXEC dbo.UpdatePostalCode '2577', -34.781805, 150.561629
EXEC dbo.UpdatePostalCode '2621', -35.256083, 149.440417
EXEC dbo.UpdatePostalCode '2642', -35.971080, 148.052595
EXEC dbo.UpdatePostalCode '2648', -34.138711, 142.162958
EXEC dbo.UpdatePostalCode '2663', -34.870998, 147.584679
EXEC dbo.UpdatePostalCode '2678', -35.061232, 147.358126
EXEC dbo.UpdatePostalCode '2710', -35.927134, 144.847987
EXEC dbo.UpdatePostalCode '2730', -30.280280, 151.778311
EXEC dbo.UpdatePostalCode '2734', -34.951605, 143.484273
EXEC dbo.UpdatePostalCode '2777', -33.665114, 150.650802
EXEC dbo.UpdatePostalCode '2824', -31.699379, 147.837310
EXEC dbo.UpdatePostalCode '2831', -32.163806, 146.299530
EXEC dbo.UpdatePostalCode '2835', -31.498218, 145.840727
EXEC dbo.UpdatePostalCode '2836', -31.558559, 143.377976
EXEC dbo.UpdatePostalCode '2877', -33.089075, 147.152078
EXEC dbo.UpdatePostalCode '2880', -29.434259, 142.010079
EXEC dbo.UpdatePostalCode '2899', -29.039123, 167.939564
EXEC dbo.UpdatePostalCode '3037', -37.691437, 144.740020
EXEC dbo.UpdatePostalCode '3167', -37.909509, 145.085245
EXEC dbo.UpdatePostalCode '3338', -37.704323, 144.574818
EXEC dbo.UpdatePostalCode '3364', -37.383521, 143.757838
EXEC dbo.UpdatePostalCode '3400', -36.711714, 142.204899
EXEC dbo.UpdatePostalCode '3501', -34.196245, 142.150996
EXEC dbo.UpdatePostalCode '3550', -38.080294, 145.210794
EXEC dbo.UpdatePostalCode '3585', -35.336175, 143.560181
EXEC dbo.UpdatePostalCode '3677', -36.353715, 146.324937
EXEC dbo.UpdatePostalCode '3724', -37.052322, 146.087887
EXEC dbo.UpdatePostalCode '3751', -37.535190, 145.029035
EXEC dbo.UpdatePostalCode '4010', -21.509030, 142.693170
EXEC dbo.UpdatePostalCode '4019', -27.259580, 153.082110
EXEC dbo.UpdatePostalCode '4051', -27.418369, 152.986155
EXEC dbo.UpdatePostalCode '4061', -27.444986, 152.952771
EXEC dbo.UpdatePostalCode '4072', -23.321302, 150.521136
EXEC dbo.UpdatePostalCode '4352', -27.351633, 152.032446
EXEC dbo.UpdatePostalCode '4354', -19.320127, 146.751080
EXEC dbo.UpdatePostalCode '4407', -27.545088, 151.292100
EXEC dbo.UpdatePostalCode '4426', -26.642228, 149.623168
EXEC dbo.UpdatePostalCode '4428', -26.587040, 149.186312
EXEC dbo.UpdatePostalCode '4461', -26.585255, 148.386748
EXEC dbo.UpdatePostalCode '4471', -27.368756, 145.960479
EXEC dbo.UpdatePostalCode '4478', -25.479054, 146.011909
EXEC dbo.UpdatePostalCode '4480', -26.668654, 143.267376
EXEC dbo.UpdatePostalCode '4490', -27.968275, 144.636217
EXEC dbo.UpdatePostalCode '4557', -26.677686, 153.117168
EXEC dbo.UpdatePostalCode '4625', -25.629139, 151.504038
EXEC dbo.UpdatePostalCode '4751', -27.773632, 151.899437
EXEC dbo.UpdatePostalCode '4816', -20.099354, 146.886937
EXEC dbo.UpdatePostalCode '5140', -34.953617, 138.696352
EXEC dbo.UpdatePostalCode '5223', -35.656837, 137.638139
EXEC dbo.UpdatePostalCode '5260', -35.253802, 139.454280
EXEC dbo.UpdatePostalCode '5291', -37.829388, 140.780222
EXEC dbo.UpdatePostalCode '5577', -34.990060, 137.398818
EXEC dbo.UpdatePostalCode '5690', -32.126082, 133.675174
EXEC dbo.UpdatePostalCode '5710', -32.491582, 137.762118
EXEC dbo.UpdatePostalCode '5731', -30.287589, 138.349417
EXEC dbo.UpdatePostalCode '6338', -34.392394, 119.380815
EXEC dbo.UpdatePostalCode '6443', -31.732319, 121.705795
EXEC dbo.UpdatePostalCode '6642', -23.937780, 119.845280
EXEC dbo.UpdatePostalCode '6722', -20.407445, 118.599737
EXEC dbo.UpdatePostalCode '6728', -17.304811, 123.632488
EXEC dbo.UpdatePostalCode '6740', -14.878970, 125.916455
EXEC dbo.UpdatePostalCode '6753', -23.357288, 119.737169
EXEC dbo.UpdatePostalCode '6762', -21.710095, 122.225694
EXEC dbo.UpdatePostalCode '6920', -31.860972, 115.752859
EXEC dbo.UpdatePostalCode '6953', -32.019400, 115.833223
EXEC dbo.UpdatePostalCode '6987', -32.019462, 115.939199
EXEC dbo.UpdatePostalCode '7151', -54.605892, 158.869867
EXEC dbo.UpdatePostalCode '7249', -41.457458, 147.164853
GO

-- These postal codes should point to the same localities.

DECLARE @localityId INT
SELECT
	@localityId = L.id
FROM
	dbo.Locality AS L
INNER JOIN
	dbo.PostalCode AS PC ON PC.localityId = L.id
INNER JOIN
	dbo.NamedLocation AS NL ON NL.id = PC.id
WHERE
	NL.displayName = '0861'

UPDATE
	PostalCode
SET
	localityId = @localityId
FROM
	PostalCode AS PC
INNER JOIN
	NamedLocation AS NL ON NL.id = PC.id
WHERE
	NL.displayName = '0860' OR NL.displayName = '0862'
GO

-- The changes mean that these postocdes should no longer be considered part of the region.

EXEC dbo.RemovePostalCodeFromRegion '2267', 'Sydney'
GO

DROP PROCEDURE dbo.UpdatePostalCode
DROP PROCEDURE dbo.RemovePostalCodeFromRegion
GO

