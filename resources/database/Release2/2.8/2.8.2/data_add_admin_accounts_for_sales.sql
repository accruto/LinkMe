DECLARE @passwordHash VARCHAR(50)
SET @passwordHash = '4C+YKJIYzi7OVxqXoB3TfA==' -- changemenow

EXEC linkme_owner.CreateAdminUser '333a9dff-ea13-4255-acbb-d0dcdb1a1ea7', 'Elie Admin', @passwordHash,
	'Elie', 'Frangie', 'efrangi@linkme.com.au'
EXEC linkme_owner.CreateAdminUser '22c41eb3-ecba-4a02-91a3-5ad9626200c6', 'Paul Admin', @passwordHash,
	'Paul', 'Lord', 'plord@linkme.com.au'
EXEC linkme_owner.CreateAdminUser '3bc36ded-da91-4b37-87ee-fd9866fff9f3', 'Marcus Admin', @passwordHash,
	'Marcus', 'Webb', 'mwebb@linkme.com.au'
EXEC linkme_owner.CreateAdminUser 'cdf56718-c17a-4237-9d1e-7f5592ff3ffc', 'Jamie Admin', @passwordHash,
	'Jamie', 'Flavell', 'jflavell@linkme.com.au'
EXEC linkme_owner.CreateAdminUser '30a2fb45-cd98-4aed-a67d-ceb37095c25f', 'Lilian Admin', @passwordHash,
	'Lilian', 'Elkhouri', 'lelkhouri@linkme.com.au'

GO
