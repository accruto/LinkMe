-- Update "KEMPSEY MSC, NSW" to point to the same Locality as "KEMPSEY, NSW"

UPDATE PostalCode
SET localityId = 440
WHERE postcode = '2442'

-- Update the Locality that used to be "KEMPSEY MSC, NSW" to be "ROYAL BRISBANE HOSPITAL, QLD" (4029)

UPDATE GeographicalArea
SET displayName = '4029'
WHERE [id] = 442

UPDATE Locality
SET centroidLatitude = -27.448, centroidLongitude = 153.027
WHERE [id] = 442

DELETE LocalityCountrySubdivision
WHERE localityId = 442 AND countrySubdivisionId = 13

GO
