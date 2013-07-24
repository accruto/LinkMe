-- Add a new Locality for "CHRISTMAS ISLAND" (6798)

INSERT INTO GeographicalArea([id], displayName)
VALUES (2817, '6798')

INSERT INTO Locality([id], centroidLatitude, centroidLongitude)
VALUES (2817, -10.5, 105.65)

INSERT INTO LocalityCountrySubdivision(localityId, countrySubdivisionId)
VALUES (2817, 19)

UPDATE PostalCode
SET localityId = 2817
WHERE postcode = '6798'

-- Remove it from locality 1485

DELETE LocalityCountrySubdivision
WHERE localityId = 1485 AND countrySubdivisionId = 19

GO
