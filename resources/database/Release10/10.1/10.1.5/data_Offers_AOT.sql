UPDATE
	Offering
SET
	enabled = 0
WHERE
	id IN ('F722A254-6F99-49E3-AB1E-899496742FEC', 'DB2E42D5-6316-4229-8C13-0DCF29F4506E', 'CBEB6579-1D03-4F31-BBD9-A78D73E19A26')

UPDATE
	OfferCategory
SET
	enabled = 0
WHERE
	id IN ('356C28A8-B4C9-4E3C-BF6F-2E1EF586E1D3', '49F5195C-5472-4301-93D6-339B0A8F71F1', '75C962F5-33BE-4D7F-B83D-81600C945055')

-- Move all courses under new categories.

DECLARE @categoryId UNIQUEIDENTIFIER
DECLARE @parentId UNIQUEIDENTIFIER

SET @categoryId = '{945BDF92-49CC-49ed-AA66-2E8336471E76}'
SET @parentId = NULL
EXEC dbo.CreateOfferCategory @categoryId, @parentId, 'Online Courses'

SET @parentId = @categoryId
SET @categoryId = '{815FF87A-7C7C-49d4-819F-D05764D50174}'
EXEC dbo.CreateOfferCategory @categoryId, @parentId, 'Business Courses'

UPDATE
	OfferCategory
SET
	parentId = @categoryId
FROM
	OfferCategory AS c
INNER JOIN
	OfferCategoryOffering AS co ON co.categoryId = c.id
INNER JOIN
	Offering AS o ON co.offeringId = o.id
INNER JOIN
	OfferProvider AS p ON o.providerId = p.id
WHERE
	p.name = 'Accredited Online Training'
	AND c.parentId = 'D328CBF4-3429-4D49-A792-442F85FC7763'


SET @categoryId = '{C6D1CEAD-0FFF-4cf4-BE9F-93D5E00733EE}'
EXEC dbo.CreateOfferCategory @categoryId, @parentId, 'Certificate Courses'

UPDATE
	OfferCategory
SET
	parentId = @categoryId
FROM
	OfferCategory AS c
INNER JOIN
	OfferCategoryOffering AS co ON co.categoryId = c.id
INNER JOIN
	Offering AS o ON co.offeringId = o.id
INNER JOIN
	OfferProvider AS p ON o.providerId = p.id
WHERE
	p.name = 'Accredited Online Training'
	AND c.parentId = 'DE7D1128-EFD0-4622-A25D-B88E9812DD6C'


SET @categoryId = '{5A0589C0-A422-4d64-A3BD-DF62943DEE3D}'
EXEC dbo.CreateOfferCategory @categoryId, @parentId, 'Financial Services Courses'

UPDATE
	OfferCategory
SET
	parentId = @categoryId
FROM
	OfferCategory AS c
INNER JOIN
	OfferCategoryOffering AS co ON co.categoryId = c.id
INNER JOIN
	Offering AS o ON co.offeringId = o.id
INNER JOIN
	OfferProvider AS p ON o.providerId = p.id
WHERE
	p.name = 'Accredited Online Training'
	AND c.parentId = '56099C39-4C57-4464-A2D6-A9FE5F1561CE'


SET @categoryId = '{B1653CCA-4256-499d-8583-B186512DBA38}'
EXEC dbo.CreateOfferCategory @categoryId, @parentId, 'HR & Recruitment Courses'

UPDATE
	OfferCategory
SET
	parentId = @categoryId
FROM
	OfferCategory AS c
INNER JOIN
	OfferCategoryOffering AS co ON co.categoryId = c.id
INNER JOIN
	Offering AS o ON co.offeringId = o.id
INNER JOIN
	OfferProvider AS p ON o.providerId = p.id
WHERE
	p.name = 'Accredited Online Training'
	AND c.parentId = '97151BFF-CE44-4D57-8DFD-031E4FBFE7D1'

-- Disable the categories that no longer have any sub-categories

UPDATE
	OfferCategory
SET
	enabled = 0
WHERE
	id IN ('DE7D1128-EFD0-4622-A25D-B88E9812DD6C', '97151BFF-CE44-4D57-8DFD-031E4FBFE7D1')