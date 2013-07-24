-- Add the 3004 delivery area to the postal suburb data

INSERT INTO PostalSuburb
SELECT 
	MAX(ps.id)+1 AS id, 'St Kilda Rd Central' AS displayName, 
	(SELECT id FROM PostalCode WHERE postcode = 3004) AS postcodeId, 
	(SELECT id FROM CountrySubdivision WHERE shortDisplayName LIKE 'VIC') AS countrySubdivisionId
FROM 
	PostalSuburb AS ps