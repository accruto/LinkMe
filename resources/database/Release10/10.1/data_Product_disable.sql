-- For the moment disable all applicant packs.

UPDATE
	dbo.Product
SET
	enabled = 0
WHERE
	name LIKE '%Applicant Pack%' OR name LIKE '%Applicants + %'
