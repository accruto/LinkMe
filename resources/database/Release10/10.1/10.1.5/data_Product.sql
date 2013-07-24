UPDATE
	dbo.Product
SET
	enabled = 1
WHERE
	name IN ('10 Applicant Pack', '20 Applicant Pack', '100 Applicant Pack', '10 Applicants + 3 Contacts', '20 Applicants + 10 Contacts', '100 Applicants + 40 Contacts')

UPDATE
	dbo.Product
SET
	price = 270
WHERE
	name = '10 Applicants + 3 Contacts'

UPDATE
	dbo.Product
SET
	price = 585
WHERE
	name = '20 Applicants + 10 Contacts'

UPDATE
	dbo.Product
SET
	price = 1980
WHERE
	name = '100 Applicants + 40 Contacts'

UPDATE
	dbo.Product
SET
	price = 3825
WHERE
	name = '300 Applicants + 100 Contacts'