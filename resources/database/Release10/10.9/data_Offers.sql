-- ACMA

DECLARE @value DECIMAL
SET @value = 50000

EXEC dbo.CreateOfferingCriteria '7D5A2BE4-BA14-40A7-B5CC-F83C97FB1ABC', '22C57322-0B46-4AE0-BEC2-6673C9756A77', 'SalaryLowerBound', @value, 'Country', 1, 'ExcludeNoSalary', 1, 'UseUnrestrictedSalary', 1, NULL, NULL
EXEC dbo.CreateOfferingCriteria '9F432786-F580-4DF1-9FC1-30C3738B7358', '85B3728D-60D3-4FE0-9059-6219D3A38EC5', 'SalaryLowerBound', @value, 'Country', 1, 'ExcludeNoSalary', 1, 'UseUnrestrictedSalary', 1, NULL, NULL
EXEC dbo.CreateOfferingCriteria '3B23B738-8A04-4AA6-8C84-CD2E2B92413C', '55A990D3-2C6B-4FBF-B3E4-7696E1B55AB6', 'SalaryLowerBound', @value, 'Country', 1, 'ExcludeNoSalary', 1, 'UseUnrestrictedSalary', 1, NULL, NULL
EXEC dbo.CreateOfferingCriteria '1CCD028E-AE0C-40B8-A2CF-DE9ADE452CBE', 'D98A8B4B-2E40-4E4A-A7A9-81C9F6947DA4', 'SalaryLowerBound', @value, 'Country', 1, 'ExcludeNoSalary', 1, 'UseUnrestrictedSalary', 1, NULL, NULL
EXEC dbo.CreateOfferingCriteria '5B047C55-C9CC-42DE-9E18-AA3F567A26B2', 'FFAD2715-CB01-4BA9-B3AB-F08F995CDFF6', 'SalaryLowerBound', @value, 'Country', 1, 'ExcludeNoSalary', 1, 'UseUnrestrictedSalary', 1, NULL, NULL
EXEC dbo.CreateOfferingCriteria 'B1AF0853-2D15-4FD1-A363-914C9EF63CD2', '28643C53-8110-4C63-8753-E05831053EAB', 'SalaryLowerBound', @value, 'Country', 1, 'ExcludeNoSalary', 1, 'UseUnrestrictedSalary', 1, NULL, NULL

-- Milestone Learning 

SET @value = 70000
EXEC dbo.CreateOfferingCriteria '{3FCA9961-7508-49ed-ABFC-AF63ADEDBE99}', '027A9457-0482-4079-8DE6-1596713B8E29', 'JobTitle', 'Manager', 'Keywords', 'Manager', 'SalaryLowerBound', @value, 'ExcludeNoSalary', 1, 'UseUnrestrictedSalary', 1
EXEC dbo.CreateOfferingCriteria '{03223229-1CB9-4db7-8207-57A05F865E76}', 'E25688EF-72C0-4139-8CE4-97A9CFAAEE43', 'JobTitle', 'Manager', 'Keywords', 'Manager', 'SalaryLowerBound', @value, 'ExcludeNoSalary', 1, 'UseUnrestrictedSalary', 1
EXEC dbo.CreateOfferingCriteria '{04650F3E-57C1-4622-933F-8859F52B30F5}', '27E143E3-2212-4D83-8A62-11DCC23D7E1A', 'JobTitle', 'Manager', 'Keywords', 'Manager', 'SalaryLowerBound', @value, 'ExcludeNoSalary', 1, 'UseUnrestrictedSalary', 1
EXEC dbo.CreateOfferingCriteria '{9215CFE9-8E35-4962-B3CB-81B190CF364A}', '9F91635A-E42D-472C-BBBF-85590E0A431E', 'JobTitle', 'Manager', 'Keywords', 'Manager', 'SalaryLowerBound', @value, 'ExcludeNoSalary', 1, 'UseUnrestrictedSalary', 1

-- Swinburne University of Technology - Postgraduate

DECLARE @categoryId UNIQUEIDENTIFIER
SET @categoryId = '{F584EECB-8CBA-4826-8C51-C9A59AF7F8CA}'
EXEC dbo.CreateOfferCategory @categoryId, 'D328CBF4-3429-4D49-A792-442F85FC7763', 'Grad Cert - HR Management'

IF NOT EXISTS (SELECT * FROM dbo.OfferCategoryOffering WHERE categoryId = @categoryId AND offeringId = '00490FF9-41CA-41F9-8D34-867AB8CD2C55')
	INSERT
		dbo.OfferCategoryOffering (categoryId, offeringId)
	VALUES
		(@categoryId, '00490FF9-41CA-41F9-8D34-867AB8CD2C55')

SET @categoryId = '{88807855-2ACE-4cdb-A14A-EC0D499F82E8}'
EXEC dbo.CreateOfferCategory @categoryId, 'D328CBF4-3429-4D49-A792-442F85FC7763', 'Short Courses - Human Resources'

IF NOT EXISTS (SELECT * FROM dbo.OfferCategoryOffering WHERE categoryId = @categoryId AND offeringId = 'D45FC235-55FC-408C-A46E-2EEA4373E206')
	INSERT
		dbo.OfferCategoryOffering (categoryId, offeringId)
	VALUES
		(@categoryId, 'D45FC235-55FC-408C-A46E-2EEA4373E206')

-- ABA

UPDATE
	dbo.OfferCategory
SET
	name = 'Advanced Diploma of Graphic Design'
WHERE
	name = 'Advanced Diploma of Grpahic Design'

UPDATE
	dbo.Offering
SET
	name = 'Advanced Diploma of Graphic Design'
WHERE
	name = 'Advanced Diploma of Grpahic Design'

-- ABA

EXEC dbo.CreateOffering '{5781AB73-C84C-40c4-962C-CAA6C1A11FB5}', 'Diploma of Human Resources Management', '8168214E-DCDA-4660-9D8B-BD9C9E2F7F0A', 'E3D5FBEF-264E-451C-A8CE-1F187F930C31', '97151BFF-CE44-4D57-8DFD-031E4FBFE7D1'
EXEC dbo.CreateOfferingCriteria '{F05C4530-8AC1-4a51-995F-2E8DE5039085}', '{5781AB73-C84C-40c4-962C-CAA6C1A11FB5}', 'Country', 1, 'Location', 'NSW', NULL, NULL, NULL, NULL, NULL, NULL

-- RCSA

EXEC dbo.CreateOfferingCriteria '{0E615408-3F04-49b3-9DCB-181A01EACBC5}', 'F9C45237-7B80-4DCB-8F0F-5C154BCF40DE', 'Country', 1, 'Keywords', 'Recruitment', NULL, NULL, NULL, NULL, NULL, NULL
EXEC dbo.CreateOfferingCriteria '{4656FE16-3030-4fc5-8FCB-44EF955903F6}', '8435DDA3-920C-4C5F-8CEF-68EA145CE60F', 'Country', 1, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL
EXEC dbo.CreateOfferingCriteria '{2DD841FE-8634-406b-A474-5D1894869AE3}', '26F7259E-4E41-432C-A403-8BF925AD53C9', 'Country', 1, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL
EXEC dbo.CreateOfferingCriteria '{12053109-2160-42a9-88D9-814CA3D393E3}', '90933795-7732-411B-AB99-F513011B3415', 'Country', 1, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL
EXEC dbo.CreateOfferingCriteria '{803ED7CE-7CF5-4989-881D-8361AEB70D67}', '3FA94E5D-201F-4346-93D4-0EF21038EC2C', 'Country', 1, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL
EXEC dbo.CreateOfferingCriteria '{B159059A-4F7E-46c5-A072-84E42874BC26}', 'BB34D7D2-A27F-4757-918B-05803906F098', 'Country', 1, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL
EXEC dbo.CreateOfferingCriteria '{3FCD9651-07BC-4b2a-91CC-FD5573B4AB41}', '57F8D265-63B2-4AD3-BBCC-C45985F829F9', 'Country', 1, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL
EXEC dbo.CreateOfferingCriteria '{E9F228F0-5440-4006-8CB7-A1D2F331F3A1}', '3B9BCEC6-06F8-4C45-8173-E9926C737822', 'Country', 1, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL
EXEC dbo.CreateOfferingCriteria '{A76667B8-8918-4662-AB38-19C25405BE1E}', '9280C231-9D41-40B1-BB02-E776E86D255E', 'Country', 1, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL

EXEC dbo.CreateOfferingCriteria '{71A33253-A7AA-4907-89B2-5E62D6787759}', 'F9C45237-7B80-4DCB-8F0F-5C154BCF40DE', 'Country', 8, 'Keywords', 'Recruitment', NULL, NULL, NULL, NULL, NULL, NULL
EXEC dbo.CreateOfferingCriteria '{1532E2D9-C28F-41cd-95EC-CA45CF968B2F}', '8435DDA3-920C-4C5F-8CEF-68EA145CE60F', 'Country', 8, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL
EXEC dbo.CreateOfferingCriteria '{761FA707-F3EA-48fe-A0F9-2CE91C4FD320}', '26F7259E-4E41-432C-A403-8BF925AD53C9', 'Country', 8, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL
EXEC dbo.CreateOfferingCriteria '{914B5EE3-78B6-40ea-8832-713BCA84A751}', '90933795-7732-411B-AB99-F513011B3415', 'Country', 8, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL
EXEC dbo.CreateOfferingCriteria '{93682CC9-8F96-4831-8D4E-028953021050}', '3FA94E5D-201F-4346-93D4-0EF21038EC2C', 'Country', 8, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL
EXEC dbo.CreateOfferingCriteria '{71CBB2A0-E090-4af1-9E6D-3E11C4171919}', 'BB34D7D2-A27F-4757-918B-05803906F098', 'Country', 8, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL
EXEC dbo.CreateOfferingCriteria '{D1F1292B-43A5-4eba-87C6-335AE8812AEB}', '57F8D265-63B2-4AD3-BBCC-C45985F829F9', 'Country', 8, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL
EXEC dbo.CreateOfferingCriteria '{65490332-0247-413f-983A-D73C1DE9E2A1}', '3B9BCEC6-06F8-4C45-8173-E9926C737822', 'Country', 8, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL
EXEC dbo.CreateOfferingCriteria '{F754A377-7127-4a51-9F93-E57E16810A64}', '9280C231-9D41-40B1-BB02-E776E86D255E', 'Country', 8, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL

UPDATE
	dbo.OfferCategory
SET
	name = 'Recruitment industry association for individual Recruitment Professionals'
WHERE
	name = 'Are you a Recruitment Professional seeking  recognised  industry accreditation?'

UPDATE
	dbo.Offering
SET
	name = 'Recruitment industry association for individual Recruitment Professionals'
WHERE
	name = 'Are you a Recruitment Professional seeking  recognised  industry accreditation?'

UPDATE
	dbo.OfferCategory
SET
	name = 'Recruitment industry association for organisations'
WHERE
	name = 'Are you a  representative of a company seeking industry accreditation?'

UPDATE
	dbo.Offering
SET
	name = 'Recruitment industry association for organisations'
WHERE
	name = 'Are you a  representative of a company seeking industry accreditation?'

-- Computer Skills

UPDATE
	dbo.OfferCategory
SET
	name = 'Computer Skills Courses from $39.95'
WHERE
	name = 'Computer Skills Courses from $39.95 (online)'

-- The CEO Institute

UPDATE
	dbo.OfferCategory
SET
	enabled = 0
WHERE
	id IN ('24610A2D-78B1-449C-A170-301D0FB64BF4', 'A089FC99-BDC0-4073-82BA-BF30D5BC1044', '159DAC62-BB59-42C6-892B-34F63D457183')

UPDATE
	dbo.OfferProvider
SET
	enabled = 0
WHERE
	name = 'The CEO Institute'

-- AHRI Affiliate Member

UPDATE
	dbo.Offering
SET
	enabled = 0
WHERE
	id = 'EF46C836-3BA3-4D0B-9C7F-3B32600022DA'

UPDATE
	dbo.OfferCategory
SET
	name = 'HR industry association for individual HR practitioners'
WHERE
	name = 'Are you a current HR Practitioner looking to join a recognised professional HR Association?'

UPDATE
	dbo.Offering
SET
	name = 'HR industry association for individual HR practitioners'
WHERE
	name = 'Are you a current HR Practitioner looking to join a recognised professional HR Association?'

UPDATE
	dbo.OfferCategory
SET
	name = 'HR industry association for organisations'
WHERE
	name = 'Does you organisation have 2 or more employees looking to join a recognised professional HR Association?'

UPDATE
	dbo.Offering
SET
	name = 'HR industry association for organisations'
WHERE
	name = 'Does you organisation have 2 or more employees looking to join a recognised professional HR Association?'

UPDATE
	dbo.OfferCategory
SET
	name = 'HR industry association for students studying HR'
WHERE
	name = 'Are you a current student studying a HR related course looking to join a recognised professional HR Association?'

UPDATE
	dbo.Offering
SET
	name = 'HR industry association for students studying HR'
WHERE
	name = 'Are you a current student studying a HR related course looking to join a recognised professional HR Association?'

