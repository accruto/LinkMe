DECLARE @providerId UNIQUEIDENTIFIER
DECLARE @parentCategoryId UNIQUEIDENTIFIER
DECLARE @offeringId UNIQUEIDENTIFIER

-- IT

SET @providerId = '20D4488A-9852-40AB-ABF8-B5FE1F3A5057'
SET @parentCategoryId = 'AE0DB911-537D-46DE-B971-4B46DC266372'

SET @offeringId = '{12EEF049-6AA8-4e63-B58C-62E57C2A357C}'
EXEC dbo.CreateOffering @offeringId, 'Apple Server Administration', @providerId, '{72663E5F-73B9-4fc2-B698-377FDDAC2D5E}', @parentCategoryId
EXEC dbo.CreateOfferingLocation '{89854070-B961-476e-B6BD-F151F546D237}', @offeringId, 'VIC'

SET @offeringId = '{56950FCD-3623-473e-B293-F06F9A9277AC}'
EXEC dbo.CreateOffering @offeringId, 'ITIL Certification', @providerId, '{E0F327D8-9D8D-4085-B6C1-08C231402B21}', @parentCategoryId
EXEC dbo.CreateOfferingLocation '{443BBADF-8624-4e0f-A98C-06D757CB3FD9}', @offeringId, 'VIC'

SET @offeringId = '{AD8A9EFE-A2EE-448a-8EAD-AC61E0A343F8}'
EXEC dbo.CreateOffering @offeringId, 'PMP Certification', @providerId, '{4AA7C7CA-D9AB-4f01-BFA9-EFF46EBFE763}', @parentCategoryId
EXEC dbo.CreateOfferingLocation '{6054AE51-4B5F-4833-A587-CE5B1E28E106}', @offeringId, 'VIC'

SET @offeringId = '{F4528797-8780-460e-992E-7F5C84D5315B}'
EXEC dbo.CreateOffering @offeringId, 'Service Desk Analysis', @providerId, '{A286891A-4895-419c-817F-D3169E320581}', @parentCategoryId
EXEC dbo.CreateOfferingLocation '{ECF4D20D-1E4D-4857-89C2-8CA9937C65B2}', @offeringId, 'VIC'

SET @offeringId = '{9ABFA2ED-2448-453b-AF04-6A411AD0E471}'
EXEC dbo.CreateOffering @offeringId, 'Adobe Certification', @providerId, '{F03434BD-0B60-4a49-9442-E67DDD73AC50}', @parentCategoryId
EXEC dbo.CreateOfferingLocation '{49F3C478-3C72-48a1-AE59-0251ACC258F8}', @offeringId, 'VIC'

-- Art, Design & Digital Media

SET @parentCategoryId = 'AB12D51C-92CC-4717-A3B2-A50BFEE6A097'
EXEC dbo.CreateOffering @offeringId, 'Adobe Certification', @providerId, '{4D570FCA-7D1B-4ce9-8F4F-FA1AE0F01DF6}', @parentCategoryId
