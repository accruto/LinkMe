-- ACT

UPDATE dbo.CountrySubdivision
SET circleRadiusKm = 71, circleCentreId = p.localityId
FROM dbo.CountrySubdivision cs, dbo.PostalCode p
WHERE cs.[id] = 12 AND p.postcode = '2906'

-- NSW

UPDATE dbo.CountrySubdivision
SET circleRadiusKm = 813, circleCentreId = p.localityId
FROM dbo.CountrySubdivision cs, dbo.PostalCode p
WHERE cs.[id] = 13 AND p.postcode = '2874'

-- NT

UPDATE dbo.CountrySubdivision
SET circleRadiusKm = 1055, circleCentreId = p.localityId
FROM dbo.CountrySubdivision cs, dbo.PostalCode p
WHERE cs.[id] = 14 AND p.postcode = '0861'

-- QLD

UPDATE dbo.CountrySubdivision
SET circleRadiusKm = 1382, circleCentreId = p.localityId
FROM dbo.CountrySubdivision cs, dbo.PostalCode p
WHERE cs.[id] = 15 AND p.postcode = '4820'

-- SA

UPDATE dbo.CountrySubdivision
SET circleRadiusKm = 949, circleCentreId = p.localityId
FROM dbo.CountrySubdivision cs, dbo.PostalCode p
WHERE cs.[id] = 16 AND p.postcode = '5661'

-- TAS

UPDATE dbo.CountrySubdivision
SET circleRadiusKm = 318, circleCentreId = p.localityId
FROM dbo.CountrySubdivision cs, dbo.PostalCode p
WHERE cs.[id] = 17 AND p.postcode = '7304'

-- VIC

UPDATE dbo.CountrySubdivision
SET circleRadiusKm = 503, circleCentreId = p.localityId
FROM dbo.CountrySubdivision cs, dbo.PostalCode p
WHERE cs.[id] = 18 AND p.postcode = '3630'

-- WA

UPDATE dbo.CountrySubdivision
SET circleRadiusKm = 1506, circleCentreId = p.localityId
FROM dbo.CountrySubdivision cs, dbo.PostalCode p
WHERE cs.[id] = 19 AND p.postcode = '6753'

GO
