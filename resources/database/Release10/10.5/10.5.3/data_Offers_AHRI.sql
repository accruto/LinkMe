DECLARE @providerId UNIQUEIDENTIFIER
DECLARE @parentCategoryId UNIQUEIDENTIFIER

-- Further Education & Online Courses

SET @providerId = '{EA49445C-0231-47c3-B0AA-F1EB75A21859}'
EXEC dbo.CreateOfferProvider @providerId, 'AHRI Further Education & Online Courses'

SET @parentCategoryId = '97151BFF-CE44-4D57-8DFD-031E4FBFE7D1'
EXEC dbo.CreateOffering '{BDAEB859-4B0F-41a2-95EA-0C3C997CEEC6}', 'AHRI Short Courses - Human Resource Essentials', @providerId, '{32CF3BCD-3CDE-4cd7-BF18-C1BD7A38FF21}', @parentcategoryId
EXEC dbo.CreateOffering '{B851FDA1-09E3-4502-9BF4-98E60C34481E}', 'AHRI Short Courses - People Management Essentials', @providerId, '{5A59F5E2-6482-4038-A189-A234868B4D25}', @parentcategoryId
EXEC dbo.CreateOffering '{F9087A00-AC32-4432-A9E8-B5BD4A3DEBDA}', 'AHRI Foundations of Human Resources', @providerId, '{A22A75DF-77CF-4fc4-BD27-BC5836E668F5}', @parentcategoryId
EXEC dbo.CreateOffering '{367FA42F-BCAD-4bb4-AFCA-912A11C5E0F2}', 'AHRI Professional Diploma of Human Resources', @providerId, '{7D45BE55-E1C9-4e59-AF75-9D4C1FEBDFC8}', @parentcategoryId

SET @parentCategoryId = 'B1653CCA-4256-499D-8583-B186512DBA38'
EXEC dbo.CreateOffering '{401CD880-4F56-4c4f-ABEB-78383C28E1DB}', 'Leadership and Management', @providerId, '{F46B79EF-F1C6-45d1-B292-4B02DAA8DEB3}', @parentcategoryId
EXEC dbo.CreateOffering '{FB2A6FE9-4317-49bf-A6C0-8644AD1A4969}', 'Human Resources', @providerId, '{32992517-BC2B-4ae0-B73A-F8A8F89EE7A0}', @parentcategoryId
EXEC dbo.CreateOffering '{7388077E-C8AC-46c0-919E-47FC55C03E1C}', 'Project Management', @providerId, '{A082108C-8DDB-4c81-ABBC-5AEC882CF1E3}', @parentcategoryId
EXEC dbo.CreateOffering '{DC3366C6-C0B5-475f-8157-865B1A6558CB}', 'Compliance Training', @providerId, '{6AF41510-16DF-4186-B87B-AC5AF11E3AE8}', @parentcategoryId
EXEC dbo.CreateOffering '{2ADCD00D-D41B-4eb6-BF85-3F649A97148F}', 'Employability Skills', @providerId, '{275745E3-FED9-4a95-AE82-88DBB0B7537D}', @parentcategoryId
EXEC dbo.CreateOffering '{70402FE7-63DD-4340-9938-A9E94932BF8D}', 'IT & Computing Skills', @providerId, '{9A6EA658-F837-4f02-A07F-D1566010B126}', @parentcategoryId
EXEC dbo.CreateOffering '{9A250928-A1D4-41f4-8CC0-73DE5324445A}', 'Customer Service and Sales', @providerId, '{31E6C3B5-8571-4560-8D76-7B3606DF5C4D}', @parentcategoryId

-- Professional Associations

SET @parentCategoryId = 'F9D6868D-5EF4-4DE5-94BC-7474DBD700E2'

SET @providerId = '{85C63823-988F-410e-AE82-BBE7CF3D7B5D}'
EXEC dbo.CreateOfferProvider @providerId, 'AHRI Professional Member'
EXEC dbo.CreateOffering '{6FFE6723-5907-4bb7-BE38-1236AE1FE55A}', 'Are you a current HR Practitioner looking to join a recognised professional HR Association?', @providerId, '{3AF591B5-32BD-4073-AD67-B4A9CCB93420}', @parentCategoryId

SET @providerId = '{9B313157-0D51-49d6-95A5-AA4D1B2DD0FA}'
EXEC dbo.CreateOfferProvider @providerId, 'AHRI Affiliate Member'
EXEC dbo.CreateOffering '{EF46C836-3BA3-4d0b-9C7F-3B32600022DA}', 'Are you an individual with people management and HR responsibilities seeking affiliation with a recognised professional HR Association?', @providerId, '{94993DEC-1AD6-439a-8013-50AA9F1CBDBE}', @parentCategoryId

SET @providerId = '{855E7888-86A1-4e54-9064-32CAFC605D53}'
EXEC dbo.CreateOfferProvider @providerId, 'AHRI Organisational Member'
EXEC dbo.CreateOffering '{907E01AA-EFEE-40fa-B43F-8B826B6933AE}', 'Does you organisation have 2 or more employees looking to join a recognised professional HR Association?', @providerId, '{21E777E9-8888-44d7-B0BD-8A0657E39C62}', @parentCategoryId

SET @providerId = '{6C1C4C73-8537-4772-9524-57FB0ECE14D5}'
EXEC dbo.CreateOfferProvider @providerId, 'AHRI Student Member'
EXEC dbo.CreateOffering '{8E0F3648-AA4A-4362-B94C-671FAE271F44}', 'Are you a current student studying a HR related course looking to join a recognised professional HR Association?', @providerId, '{4F2AD31C-E5CA-4547-879D-1886C4F05FDC}', @parentCategoryId



