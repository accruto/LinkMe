DELETE dbo.OfferingCriteria
DELETE dbo.OfferingCriteriaSet
GO

INSERT
	dbo.OfferingCriteriaSet (id, offeringId, type)
SELECT
	lr.id, o.id, 'AdvancedMemberSearchCriteria'
FROM
	dbo.LocationReference AS lr
INNER JOIN
	dbo.OfferingLocation AS ol ON ol.locationreferenceid = lr.id
INNER JOIN
	dbo.Offering AS o ON o.id = ol.offeringid

INSERT
	dbo.OfferingCriteria (id, name, value)
SELECT
	lr.id, 'Country', 1
FROM
	dbo.LocationReference AS lr
INNER JOIN
	dbo.OfferingLocation AS ol ON ol.locationreferenceid = lr.id
INNER JOIN
	dbo.Offering AS o ON o.id = ol.offeringid

INSERT
	dbo.OfferingCriteria (id, name, value)
SELECT
	lr.id, 'Location', lr.unstructuredLocation
FROM
	dbo.LocationReference AS lr
INNER JOIN
	dbo.OfferingLocation AS ol ON ol.locationreferenceid = lr.id
INNER JOIN
	dbo.Offering AS o ON o.id = ol.offeringid
WHERE
	NOT lr.unstructuredLocation IS NULL

-- Fix up some categories

UPDATE
	OfferCategoryOffering
SET
	categoryId = '2FEB099E-C4DE-4C78-9737-C8398F6A3F0A'
WHERE
	categoryId = 'C07B309E-5B3F-4081-B751-023D5429A93B'
	AND offeringId = 'D48C8679-F47F-4986-BCC0-708894A0370E'

UPDATE
	OfferCategory
SET
	enabled = 0
WHERE
	id = 'C07B309E-5B3F-4081-B751-023D5429A93B'