INSERT INTO dbo.GeographicalArea([id], displayName)
VALUES (12, 'ACT')

INSERT INTO dbo.GeographicalArea([id], displayName)
VALUES (13, 'NSW')

INSERT INTO dbo.GeographicalArea([id], displayName)
VALUES (14, 'NT')

INSERT INTO dbo.GeographicalArea([id], displayName)
VALUES (15, 'QLD')

INSERT INTO dbo.GeographicalArea([id], displayName)
VALUES (16, 'SA')

INSERT INTO dbo.GeographicalArea([id], displayName)
VALUES (17, 'TAS')

INSERT INTO dbo.GeographicalArea([id], displayName)
VALUES (18, 'VIC')

INSERT INTO dbo.GeographicalArea([id], displayName)
VALUES (19, 'WA')

GO

DECLARE @countryId TINYINT

SELECT @countryId = [id]
FROM dbo.Country
WHERE displayName = 'Australia'

INSERT INTO dbo.CountrySubdivision([id], circleRadiusKm, circleCentreId, countryId)
SELECT [id], 0, NULL, @countryId
FROM dbo.GeographicalArea
WHERE [id] >= 12 AND [id] <= 19

GO
