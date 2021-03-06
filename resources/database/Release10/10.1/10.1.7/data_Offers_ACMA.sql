UPDATE
	Offering
SET
	name = 'Migrants (already residing in Australia) Job Search Assistance'
WHERE
	name = 'Migrant Job Search Assistance'

UPDATE
	OfferCategory
SET
	name = 'Migrants (already residing in Australia) Job Search Assistance'
WHERE
	name = 'Migrant Job Search Assistance'

-- Only Australian memebrs

EXEC CreateOfferingLocation '85B3728D-60D3-4FE0-9059-6219D3A38EC5', '{9F432786-F580-4df1-9FC1-30C3738B7358}', NULL, 1
EXEC CreateOfferingLocation '22C57322-0B46-4AE0-BEC2-6673C9756A77', '{7D5A2BE4-BA14-40a7-B5CC-F83C97FB1ABC}', NULL, 1
EXEC CreateOfferingLocation '55A990D3-2C6B-4FBF-B3E4-7696E1B55AB6', '{3B23B738-8A04-4aa6-8C84-CD2E2B92413C}', NULL, 1
EXEC CreateOfferingLocation 'FAD9BDAD-799B-4731-A3FC-842F5A4599C9', '{5DCE87C2-FD3A-4c60-89B9-1A1104624BA9}', NULL, 1
EXEC CreateOfferingLocation '66B1E6F8-892A-45C2-99F0-1B5DB8B1294C', '{6130887F-C4B8-4428-BAE7-700AB31B9027}', NULL, 1
EXEC CreateOfferingLocation '1318D29E-D7AE-4D58-A2C0-A733816EE4E2', '{7367F4E1-314C-4628-8197-B55058022617}', NULL, 1
EXEC CreateOfferingLocation 'C0134225-1185-4851-AF53-AFF69172A3D3', '{7994CF68-C3D1-4c55-91A7-5D81BC7F0C36}', NULL, 1
EXEC CreateOfferingLocation '7D4CB719-83F5-40A2-8420-DAF8500D7171', '{F4FC51CA-0857-406d-8D5B-FAEDB92B9358}', NULL, 1
EXEC CreateOfferingLocation '7DC9D0DD-BC9A-4C36-8ADA-3D34028DCC67', '{CCCB0A1E-7891-4fea-AAF7-1CC2DE7BCC00}', NULL, 1
EXEC CreateOfferingLocation 'FFAD2715-CB01-4BA9-B3AB-F08F995CDFF6', '{5B047C55-C9CC-42de-9E18-AA3F567A26B2}', NULL, 1
EXEC CreateOfferingLocation 'D98A8B4B-2E40-4E4A-A7A9-81C9F6947DA4', '{1CCD028E-AE0C-40b8-A2CF-DE9ADE452CBE}', NULL, 1
EXEC CreateOfferingLocation '28643C53-8110-4C63-8753-E05831053EAB', '{B1AF0853-2D15-4fd1-A363-914C9EF63CD2}', NULL, 1

