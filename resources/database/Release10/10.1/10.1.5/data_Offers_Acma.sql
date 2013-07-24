DECLARE @providerId UNIQUEIDENTIFIER
SET @providerId = '{1D66D04A-FDEA-4973-ACEB-EDDD81865202}'

-- Provider

EXEC dbo.CreateOfferProvider @providerId, 'Associated Career Management Australia'

-- Alternative Careers

DECLARE @parentCategoryId UNIQUEIDENTIFIER
SET @parentCategoryId = '385188B4-D205-46B5-AA18-6170B9623407'

EXEC dbo.CreateOffering '{C0134225-1185-4851-AF53-AFF69172A3D3}', 'Help me find my next career move', @providerId, '{57D1E157-CDDD-4768-9392-F4FAFAE2B60A}', @parentCategoryId
EXEC dbo.CreateOffering '{1318D29E-D7AE-4d58-A2C0-A733816EE4E2}', 'Help me find my ideal job', @providerId, '{7CC215C3-D592-4702-9AFF-F94A6B4F028A}', @parentCategoryId
EXEC dbo.CreateOffering '{66B1E6F8-892A-45c2-99F0-1B5DB8B1294C}', 'Help me develop my career', @providerId, '{02A079DC-6EFA-4874-BC20-44997B2CF666}', @parentCategoryId
EXEC dbo.CreateOffering '{7D4CB719-83F5-40a2-8420-DAF8500D7171}', 'Help me manage my career', @providerId, '{0F728EA4-5F67-4f44-B9D6-75B5873E0E4E}', @parentCategoryId
EXEC dbo.CreateOffering '{FAD9BDAD-799B-4731-A3FC-842F5A4599C9}', 'Help me change my career', @providerId, '{C8AC5212-9D4B-4e12-AA17-962A861032AF}', @parentCategoryId
EXEC dbo.CreateOffering '{7DC9D0DD-BC9A-4c36-8ADA-3D34028DCC67}', 'Help me with Key Selection Criteria', @providerId, '{AA484C16-6A1D-4fc1-AC97-C12242D6EE09}', @parentCategoryId

-- Career Advice & Assistance

-- Disable the old 'Job Interview Techniques & Tips' category

UPDATE
	dbo.OfferCategory
SET
	enabled = 0
WHERE
	id = '04B363CD-09ED-4690-9DCD-AF9786953212'

SET @parentCategoryId = '56C8E14B-43CA-4D97-A678-8C6FE0EEDD7A' -- Interview Techniques

EXEC dbo.CreateOffering '{FFAD2715-CB01-4ba9-B3AB-F08F995CDFF6}', 'Interview Skills & Coaching', @providerId, '{54F56082-CBE1-4171-A29A-B5674075B51A}', @parentCategoryId

SET @parentCategoryId = '4DFF023A-07DA-4129-AAC6-7C7F63616AA1' -- Job Search Techniques

EXEC dbo.CreateOffering '{85B3728D-60D3-4fe0-9059-6219D3A38EC5}', 'Career Transition Management', @providerId, '{973FFE82-C078-4aa5-B2C7-29764EBBC4BA}', @parentCategoryId
EXEC dbo.CreateOffering '{22C57322-0B46-4ae0-BEC2-6673C9756A77}', 'Executive Career Transition Management', @providerId, '{02E76970-A567-4456-952F-0C0627BE0D6D}', @parentCategoryId
EXEC dbo.CreateOffering '{55A990D3-2C6B-4fbf-B3E4-7696E1B55AB6}', 'Graduate Job Search Assistance', @providerId, '{2DE9FCCC-1123-4426-860A-9220FF9B2FF1}', @parentCategoryId
EXEC dbo.CreateOffering '{D98A8B4B-2E40-4e4a-A7A9-81C9F6947DA4}', 'Mature Age Job Search Assistance', @providerId, '{7B984D53-13A7-41ed-A06A-B0D47FE881FF}', @parentCategoryId
EXEC dbo.CreateOffering '{28643C53-8110-4c63-8753-E05831053EAB}', 'Migrant Job Search Assistance', @providerId, '{B7C03BFD-C753-4b68-9DA3-E6E3EF472653}', @parentCategoryId


