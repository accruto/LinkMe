DECLARE @providerId UNIQUEIDENTIFIER
DECLARE @parentCategoryId UNIQUEIDENTIFIER
DECLARE @offeringId UNIQUEIDENTIFIER

-- IT

SET @providerId = '20D4488A-9852-40AB-ABF8-B5FE1F3A5057'
SET @parentCategoryId = 'AE0DB911-537D-46DE-B971-4B46DC266372'

SET @offeringId = '{017ED2B5-7E66-4cd9-B3B3-0E9311F31D3B}'
EXEC dbo.CreateOffering @offeringId, 'Photoshop', @providerId, '{C29E8D72-4E0E-40b3-85B7-A07299280A08}', @parentCategoryId
EXEC dbo.CreateOfferingLocation '{26B0C83D-174F-43a9-8A8A-81AC2BCEBD94}', @offeringId, 'VIC'

SET @offeringId = '{191CA888-61FC-4142-AEE1-1005DB740925}'
EXEC dbo.CreateOffering @offeringId, 'Illustrator', @providerId, '{9D4744E0-3729-4b70-B2CC-D31BF7EC0C45}', @parentCategoryId
EXEC dbo.CreateOfferingLocation '{BD550F8D-843C-4e84-BAF5-7C2B75370BA9}', @offeringId, 'VIC'

SET @offeringId = '{23F2E93C-FACD-4211-9387-FA4C2A810891}'
EXEC dbo.CreateOffering @offeringId, 'Indesign', @providerId, '{E4A2309A-3D44-4f1a-9142-36E9E268CA75}', @parentCategoryId
EXEC dbo.CreateOfferingLocation '{155E4646-BD1D-4b09-86CB-09076AA79BDC}', @offeringId, 'VIC'

SET @offeringId = '{06A7A29F-AEE1-40e7-9619-417A746D1B1F}'
EXEC dbo.CreateOffering @offeringId, 'Flash', @providerId, '{14E33BEE-C904-4f98-A5AB-D91789226E21}', @parentCategoryId
EXEC dbo.CreateOfferingLocation '{2E22FA10-9BE2-4641-B84C-C4BE38495CFB}', @offeringId, 'VIC'

SET @offeringId = '{0630531A-09D8-4148-BAA1-D22B563129D5}'
EXEC dbo.CreateOffering @offeringId, 'Dreamweaver', @providerId, '{03FAD0D7-77B5-45fe-886A-B65561AC583D}', @parentCategoryId
EXEC dbo.CreateOfferingLocation '{B8091A70-A7B5-4e57-B148-94EEEE0294C4}', @offeringId, 'VIC'

