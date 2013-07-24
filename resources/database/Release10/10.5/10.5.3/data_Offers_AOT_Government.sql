DECLARE @providerId UNIQUEIDENTIFIER
SET @providerId = '{0FB4D3E3-D4EE-467f-A768-C3BB29BED19D}'
DECLARE @parentCategoryId UNIQUEIDENTIFIER
DECLARE @offeringId UNIQUEIDENTIFIER

-- Provider

EXEC dbo.CreateOfferProvider @providerId, 'Accredited Online Training (Government)'

SET @parentCategoryId = '945BDF92-49CC-49ED-AA66-2E8336471E76'
EXEC dbo.CreateOfferCategory '{66380EE5-2292-4fb1-83FE-A1C68E81153D}', @parentCategoryId, 'Government Funded Courses (you must be currently employed and not degree qualified)'
SET @parentCategoryId = '{66380EE5-2292-4fb1-83FE-A1C68E81153D}'

SET @offeringId = '42462D83-2DE6-482c-BCC6-1F032ECA9052'
EXEC dbo.CreateOffering @offeringId, 'Diploma of Business', @providerId, '{76B04F3F-176E-433c-9FE8-709BC33D426E}', @parentcategoryId
EXEC dbo.CreateOfferingLocation '{6805ECEE-5005-4949-8548-EE9890647195}', @offeringId, 'SA'

SET @offeringId = '{7AB5E6F2-E9B4-4185-A384-27CA5E0D9944}'
EXEC dbo.CreateOffering @offeringId, 'Diploma of Business Administration', @providerId, '{80C821F5-B741-47c5-B93A-89906195AD23}', @parentcategoryId
EXEC dbo.CreateOfferingLocation '{7C7914C5-1C83-4b9d-A63D-51530E8ADE85}', @offeringId, 'SA'

SET @offeringId = '{B92F0714-7DBA-4622-9536-9BFA1B59E2BF}'
EXEC dbo.CreateOffering @offeringId, 'Diploma of Project Management', @providerId, '{61AA4884-B982-481f-A40D-2082EE460947}', @parentcategoryId
EXEC dbo.CreateOfferingLocation '{7470E2B0-9C5C-4ecb-8560-DC76FAE5A98F}', @offeringId, 'QLD'

SET @offeringId = '{55E85022-7746-45d3-A9BA-0A94585CBB23}'
EXEC dbo.CreateOffering @offeringId, 'Diploma of Management', @providerId, '{1C14DD93-DB5B-47a8-9231-E40A01914768}', @parentcategoryId
EXEC dbo.CreateOfferingLocation '{3B6D650E-61EC-4671-84E3-EC3124E62E0D}', @offeringId, 'QLD'
EXEC dbo.CreateOfferingLocation '{6597ACFE-426A-423e-B6DE-954600A7DE64}', @offeringId, 'SA'
EXEC dbo.CreateOfferingLocation '{F2494DEA-BBD9-4033-97A3-D3BC501108DD}', @offeringId, 'ACT'

SET @offeringId = '{1458FFB2-C42F-4edb-A3AA-1FB18AF0FB5B}'
EXEC dbo.CreateOffering @offeringId, 'Certificate IV in Business', @providerId, '{DC991D36-3D19-466e-B086-71F88F56B551}', @parentcategoryId
EXEC dbo.CreateOfferingLocation '{F532A10C-7782-4c8e-9A33-7D909B5EC047}', @offeringId, 'QLD'
EXEC dbo.CreateOfferingLocation '{9B2475B8-6CC9-44cd-BFCA-C35C7252ED2B}', @offeringId, 'NSW'
EXEC dbo.CreateOfferingLocation '{FF1EAE9D-41E8-44f6-8B56-E19DAAAB0C75}', @offeringId, 'TAS'
EXEC dbo.CreateOfferingLocation '{8949E13B-2940-4b95-A8E6-08E15C98BB52}', @offeringId, 'SA'
EXEC dbo.CreateOfferingLocation '{52DD356B-44A1-450a-AF2C-896567175CC1}', @offeringId, 'ACT'

SET @offeringId = '{1B905DB8-5903-4328-92D5-D956FA2C78BD}'
EXEC dbo.CreateOffering @offeringId, 'Certificate IV in Business Administration', @providerId, '{64721CB5-54D3-40d2-AC1C-92A9068F1666}', @parentcategoryId
EXEC dbo.CreateOfferingLocation '{DFE6EFF1-F3EC-4566-B060-1896135899D4}', @offeringId, 'QLD'
EXEC dbo.CreateOfferingLocation '{490278DD-58FA-4e25-8DF4-6978DAE98D25}', @offeringId, 'NSW'
EXEC dbo.CreateOfferingLocation '{5ED29B3E-80ED-4795-A51C-AE33767A74A1}', @offeringId, 'TAS'
EXEC dbo.CreateOfferingLocation '{643A9887-E6C7-4362-B677-74842C60A9A2}', @offeringId, 'SA'
EXEC dbo.CreateOfferingLocation '{8C0CF854-0F29-4574-80F0-6AC2201AABA3}', @offeringId, 'VIC'
EXEC dbo.CreateOfferingLocation '{D332A4FE-E795-43a3-9958-C8F0A160B25F}', @offeringId, 'NT'
EXEC dbo.CreateOfferingLocation '{EC17AB31-771C-46c7-B3F7-713128414275}', @offeringId, 'ACT'

SET @offeringId = '{5C822958-743B-4b3e-A973-C0891B5BCB96}'
EXEC dbo.CreateOffering @offeringId, 'Certificate IV in Frontline Management', @providerId, '{802596C7-65AF-4216-B532-77B99F07AECE}', @parentcategoryId
EXEC dbo.CreateOfferingLocation '{0C656AD6-5C9F-4f38-AADF-DABB5A623EBD}', @offeringId, 'QLD'
EXEC dbo.CreateOfferingLocation '{4F6F90E5-15EA-42e5-BACA-20678092AD62}', @offeringId, 'NSW'
EXEC dbo.CreateOfferingLocation '{6EC4FEE0-EAC7-441a-B7A0-296D6E5A4921}', @offeringId, 'TAS'
EXEC dbo.CreateOfferingLocation '{E654823B-5CD9-491c-93D3-0A7F7B530603}', @offeringId, 'SA'
EXEC dbo.CreateOfferingLocation '{7DEA7F5F-9E60-4ed1-9B2D-5DD4AD88A47D}', @offeringId, 'ACT'

SET @offeringId = '{BF40C952-9CBF-47e4-B97B-D54C23FD7130}'
EXEC dbo.CreateOffering @offeringId, 'Certificate IV in Human Resources', @providerId, '{9A9C551B-D4F5-41e3-8056-603E73DD983A}', @parentcategoryId
EXEC dbo.CreateOfferingLocation '{8D5735FC-37E2-42a5-A6E2-EA7553296979}', @offeringId, 'QLD'
EXEC dbo.CreateOfferingLocation '{A247683A-E345-41a3-B360-B95B62D8F0E1}', @offeringId, 'NSW'
EXEC dbo.CreateOfferingLocation '{D8A95A05-1F9A-40e0-ADA2-3CA63A533D38}', @offeringId, 'TAS'
EXEC dbo.CreateOfferingLocation '{17358465-7FF5-4abe-929C-85B74BDD5F0B}', @offeringId, 'ACT'

SET @offeringId = '{3B95E545-6EF2-4e26-A6C2-D902F8379A4C}'
EXEC dbo.CreateOffering @offeringId, 'Certificate IV in Project Management', @providerId, '{02288A29-BE4C-4915-883E-D202780C1B3B}', @parentcategoryId
EXEC dbo.CreateOfferingLocation '{18BEA0C3-1988-4447-A9E4-A653DC2B7949}', @offeringId, 'QLD'
EXEC dbo.CreateOfferingLocation '{8B7048D0-98C6-4ef4-89BD-6FE1D7684507}', @offeringId, 'TAS'
EXEC dbo.CreateOfferingLocation '{6FA47713-FDE5-44c1-9E46-782AF48F63BF}', @offeringId, 'ACT'

SET @offeringId = '{55C2D021-C9D0-4147-9650-4AF77248F545}'
EXEC dbo.CreateOffering @offeringId, 'Certificate IV in Small Business Management', @providerId, '{F8269FF1-75BA-4fd0-B397-5C8A56182632}', @parentcategoryId
EXEC dbo.CreateOfferingLocation '{AC151013-045E-4d19-AF4A-B8B88FF14D84}', @offeringId, 'QLD'
EXEC dbo.CreateOfferingLocation '{88004DB6-6397-4bc3-ACF5-2F6010F4AE93}', @offeringId, 'NSW'
EXEC dbo.CreateOfferingLocation '{22EC020D-53A5-4a23-8BF8-A52B8D6D9B9B}', @offeringId, 'TAS'
EXEC dbo.CreateOfferingLocation '{67F12C57-3346-40f6-B612-FAFBD9257FC6}', @offeringId, 'ACT'

SET @offeringId = '{D436065E-C936-42ec-AFA4-56657CBF08CD}'
EXEC dbo.CreateOffering @offeringId, 'Certificate IV in Training & Assessment', @providerId, '{147BA296-ACCB-4525-87CD-2B1BDADD55E1}', @parentcategoryId
EXEC dbo.CreateOfferingLocation '{439B36D4-0295-4418-B087-402D9CB85FB9}', @offeringId, 'SA'
EXEC dbo.CreateOfferingLocation '{63F3483D-15BA-49a6-8EC7-024E73000470}', @offeringId, 'ACT'

SET @offeringId = '{A0DDBB18-A307-499e-AC08-C11414FAD38F}'
EXEC dbo.CreateOffering @offeringId, 'Certificate III in Business Administration', @providerId, '{8384F247-9884-4001-A2E5-758B4D06DABB}', @parentcategoryId
EXEC dbo.CreateOfferingLocation '{7BAB1BA8-D7AC-4e19-84F1-30DE19C9FDC2}', @offeringId, 'ACT'

SET @offeringId = '{C2D5B1B1-5C6E-403f-81E2-92C81C6876D0}'
EXEC dbo.CreateOffering @offeringId, 'Certificate III in Transport & Logistics (Warehouse & Storage)', @providerId, '{3131F758-8A2E-490b-9474-C691927C71F0}', @parentcategoryId
EXEC dbo.CreateOfferingLocation '{31F0B660-82F5-4b68-8835-D0254E5CC90F}', @offeringId, 'QLD'
EXEC dbo.CreateOfferingLocation '{B479EF39-9812-4a44-A9FD-A0561A52487B}', @offeringId, 'NSW'
EXEC dbo.CreateOfferingLocation '{3592F65F-CFCD-4797-A6D9-81F68EA6B57B}', @offeringId, 'TAS'
EXEC dbo.CreateOfferingLocation '{40FFF60F-DB17-4769-993C-0810924FD0DF}', @offeringId, 'SA'
EXEC dbo.CreateOfferingLocation '{9B2908C8-6661-4958-9897-89AB6A265EB4}', @offeringId, 'VIC'
EXEC dbo.CreateOfferingLocation '{B2098540-E04F-4a7d-A8C4-14A5493737E8}', @offeringId, 'NT'
EXEC dbo.CreateOfferingLocation '{2C8F38CE-2487-4bf4-8B9A-35BDBAABECBE}', @offeringId, 'ACT'


