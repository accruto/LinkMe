ALTER TABLE
	dbo.Product
ADD
	description NVARCHAR(200) NULL
GO

CREATE INDEX IX_Product_name ON dbo.Product
(
	name
)
GO

UPDATE
	dbo.Product
SET
	name = 'Applicants200',
	description = 'Job applicant credits'
WHERE
	id = '95237919-7B39-41DE-836D-022B200BE12E'

UPDATE
	dbo.Product
SET
	name = 'FeaturePack2',
	description = 'Advanced job ad feature pack'
WHERE
	id = '066A2F5E-9F85-4B09-AC4A-08FF15454D3B'

UPDATE
	dbo.Product
SET
	name = 'ContactsUnlimited',
	description = 'Candidate contact credits'
WHERE
	id = '88B5F4EA-97E2-4E6B-A03A-112826D2D5F0'

UPDATE
	dbo.Product
SET
	name = 'ApplicantPack10',
	description = 'Job applicant credits'
WHERE
	id = '94170E5D-EFE1-49FF-984D-1BB450244BEB'

UPDATE
	dbo.Product
SET
	name = 'Applicants40',
	description = 'Job applicant credits'
WHERE
	id = 'AFF77C84-7B6F-4F1E-B777-27D5665DAB54'

UPDATE
	dbo.Product
SET
	name = 'Contacts20',
	description = 'Candidate contact credits'
WHERE
	id = 'DB6170F9-5E96-41A1-A58A-33521C060B6F'

UPDATE
	dbo.Product
SET
	name = 'iOSContacts10',
	description = 'Candidate contact credits'
WHERE
	id = '143A0D49-F6E8-4833-B82F-36348CAFB40A'

UPDATE
	dbo.Product
SET
	name = 'Applicants20Contacts10',
	description = '20 Applicants + 10 Contacts'
WHERE
	id = 'BB5BC37D-052C-4946-BA0A-382AA7FEF1F2'

UPDATE
	dbo.Product
SET
	name = 'Applicants25',
	description = 'Job applicant credits'
WHERE
	id = '6EE3D2E8-0FCD-4678-BF66-3BD987839418'

UPDATE
	dbo.Product
SET
	name = 'iOSContacts1',
	description = 'Candidate contact credits'
WHERE
	id = 'A9AA94C6-BFBD-45CA-861D-4DB94F215F89'

UPDATE
	dbo.Product
SET
	name = 'Applicants60',
	description = 'Job applicant credits'
WHERE
	id = 'BA45FA9C-0445-4BC2-B038-4FA56BB5D8D9'

UPDATE
	dbo.Product
SET
	name = 'Applicants20',
	description = 'Job applicant credits'
WHERE
	id = '18A662F3-64CC-4A80-9CED-5E9F94100DD4'

UPDATE
	dbo.Product
SET
	name = 'Applicants10Contacts3',
	description = '10 Applicants + 3 Contacts'
WHERE
	id = 'A46812F1-CAD2-47C1-B8C3-5F7EF150DFBF'

UPDATE
	dbo.Product
SET
	name = 'JobAds1',
	description = 'Job posting credits'
WHERE
	id = '31FE57BA-076A-4EFD-AF1F-62BC6E4180E7'

UPDATE
	dbo.Product
SET
	name = 'Applicants100Contacts40',
	description = '100 Applicants + 40 Contacts'
WHERE
	id = 'C9C3DA74-9DFD-42C9-99D6-6A3C8ABF04E1'

UPDATE
	dbo.Product
SET
	name = 'iOSContacts20',
	description = 'Candidate contact credits'
WHERE
	id = 'B8DF6223-EDB5-43F6-967D-74E366139B10'

UPDATE
	dbo.Product
SET
	name = 'JobAdsUnlimited',
	description = 'Job posting credits'
WHERE
	id = 'EDE89FC5-9B0B-45BA-A68F-8AD2EC22F7F1'

UPDATE
	dbo.Product
SET
	name = 'iOSContacts3',
	description = 'Candidate contact credits'
WHERE
	id = '287A25BF-4BCF-4B24-9C40-8EBEB7BA69FB'

UPDATE
	dbo.Product
SET
	name = 'Applicants15',
	description = 'Job applicant credits'
WHERE
	id = '8D472AC3-4027-4102-875C-91D92E4BC3E9'

UPDATE
	dbo.Product
SET
	name = 'FeaturePack1',
	description = 'Basic job ad feature pack'
WHERE
	id = '6D28E05B-8B82-4ABB-ABFB-9F5F8728BAC9'

UPDATE
	dbo.Product
SET
	name = 'Contacts3',
	description = 'Candidate contact credits'
WHERE
	id = '4B209ABC-95D2-4AD8-9C99-A2C9A4D0B915'

UPDATE
	dbo.Product
SET
	name = 'Contacts5',
	description = 'Candidate contact credits'
WHERE
	id = 'DEEB3222-4C5B-4325-8AD8-A91BF787E863'

UPDATE
	dbo.Product
SET
	name = 'JobAdPack20',
	description = 'Job posting credits'
WHERE
	id = '43F6DEC7-7458-493A-81AD-AF7177B989D5'

UPDATE
	dbo.Product
SET
	name = 'JobAds40',
	description = 'Job posting credits'
WHERE
	id = 'B0BB7CEB-7421-4B97-B92C-B81B000618F8'

UPDATE
	dbo.Product
SET
	name = 'Contacts10',
	description = 'Candidate contact credits'
WHERE
	id = '8615F10C-58B8-4D6B-9A43-BB12A24FF45A'

UPDATE
	dbo.Product
SET
	name = 'Contacts100',
	description = 'Candidate contact credits'
WHERE
	id = 'AF2C1176-98F8-4138-AE5B-BD39E518206B'

UPDATE
	dbo.Product
SET
	name = 'Contacts10',
	description = 'Candidate contact credits'
WHERE
	id = 'BB4BF45B-C0BE-4398-80B6-BE3555444AE5'

UPDATE
	dbo.Product
SET
	name = 'JobAds5',
	description = 'Job posting credits'
WHERE
	id = 'F9481346-4B6A-47D6-A681-C531E12F67E7'

UPDATE
	dbo.Product
SET
	name = 'ApplicantPack20',
	description = 'Job applicant credits'
WHERE
	id = '3E3410DF-A95A-4995-BC42-CA2A858D1EE8'

UPDATE
	dbo.Product
SET
	name = 'Contacts80',
	description = 'Candidate contact credits'
WHERE
	id = '6951B9EC-3EAE-4B2A-942F-D00430A9F2DD'

UPDATE
	dbo.Product
SET
	name = 'Applicants50',
	description = 'Job applicant credits'
WHERE
	id = 'E615D71B-5813-4551-97FD-D63E2042491A'

UPDATE
	dbo.Product
SET
	name = 'Applicants300Contacts100',
	description = '300 Applicants + 100 Contacts'
WHERE
	id = '5C6B8B88-8057-44A9-8011-D6E0BF38CBAE'

UPDATE
	dbo.Product
SET
	name = 'Applicants100',
	description = 'Job applicant credits'
WHERE
	id = '8FF5AC20-15B8-4D79-8506-DACB7CD6C251'

UPDATE
	dbo.Product
SET
	name = 'JobAds20',
	description = 'Job posting credits'
WHERE
	id = '069AB044-18B8-48D8-B3E1-DF2105739E9A'

UPDATE
	dbo.Product
SET
	name = 'Contacts40',
	description = 'Candidate contact credits'
WHERE
	id = '54901FDD-4518-4E40-9283-E5908338DBE7'

UPDATE
	dbo.Product
SET
	name = 'ApplicantPack100',
	description = 'Job applicant credits'
WHERE
	id = '8D96FBC8-8F47-4597-B559-EBB7B7D9C5DE'

UPDATE
	dbo.Product
SET
	name = 'iOSContacts5',
	description = 'Candidate contact credits'
WHERE
	id = 'FA584A3F-57EE-46CA-B6C0-EF25EA5D891D'

UPDATE
	dbo.Product
SET
	name = 'ApplicantPack300',
	description = 'Job applicant credits'
WHERE
	id = '05FE52A9-D7A7-4A96-9FCF-EFB4414CB6B0'

UPDATE
	dbo.Product
SET
	name = 'Contacts40',
	description = 'Candidate contact credits'
WHERE
	id = 'C882B78C-93B4-42E3-B86A-F0C5CAB91B10'

UPDATE
	dbo.Product
SET
	name = 'Contacts60',
	description = 'Candidate contact credits'
WHERE
	id = 'CCC4C837-C424-41EC-8D90-F376FC936A49'

UPDATE
	dbo.Product
SET
	name = 'JobAds10',
	description = 'Job posting credits'
WHERE
	id = 'E3909542-C381-4745-9B2E-F7B5C197C354'

GO

ALTER TABLE
	dbo.Product
ALTER COLUMN
	description NVARCHAR(200) NOT NULL
GO
