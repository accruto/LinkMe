DECLARE @providerId UNIQUEIDENTIFIER
DECLARE @parentCategoryId UNIQUEIDENTIFIER
DECLARE @categoryId UNIQUEIDENTIFIER
DECLARE @offeringId UNIQUEIDENTIFIER

SET @providerId = '{3246EEA6-1935-445d-AB9B-72F9F84D6BB3}'
EXEC dbo.CreateOfferProvider @providerId, 'CIPSA'

-- Employer Benefits

SET @parentCategoryId = 'CE1FD7FB-A333-44EC-904B-1CDE392943B5'

SET @categoryId = '{D79A39CF-08CC-4771-BFCC-3EE0A604E60B}'
EXEC dbo.CreateOfferCategory @categoryId, @parentCategoryId, 'I am an employer or manager interested in on site procurement and purchasing training for my team'

SET @parentCategoryId = @categoryId
SET @offeringId = '{190372DC-A61B-4779-B9D9-CB51CEF13B3B}'
EXEC dbo.CreateOffering @offeringId, 'I would like to discuss a customised training solution for my team in this area', @providerId, '{874C2925-6B13-47fa-A246-91EDC83DC582}', @parentCategoryId
EXEC dbo.CreateOfferingCriteria '{AB2126A6-A167-4758-B1C4-667539340527}', @offeringId, 'Country', 1, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL
EXEC dbo.CreateOfferingCriteria '{C1ED396B-478F-4299-831A-D98A6BBE7E44}', @offeringId, 'Country', 8, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL

-- Further Education

SET @parentCategoryId = 'E69254E0-F5E6-47C2-899C-8041D77EEF8B'

SET @categoryId = '{06B334B1-8351-489b-BD3D-DD75D8C32199}'
EXEC dbo.CreateOfferCategory @categoryId, @parentCategoryId, 'Procurement'

SET @parentCategoryId = @categoryId

SET @offeringId = '{C7C4B850-3509-43a8-94B0-F0D23E021A56}'
EXEC dbo.CreateOffering @offeringId, 'Introduction to Procurement', @providerId, '{F590E5BE-59D8-4412-81DC-C3F263106F73}', @parentCategoryId
EXEC dbo.CreateOfferingCriteria '{E126C54A-7164-4668-AB5E-18CCBD91642E}', @offeringId, 'Country', 1, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL
EXEC dbo.CreateOfferingCriteria '{04AEE690-F395-43d4-B280-FB11E16ED769}', @offeringId, 'Country', 8, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL

SET @offeringId = '{CDDB5B98-48DE-400e-B778-41232F52C472}'
EXEC dbo.CreateOffering @offeringId, 'Developing Procurement Skills', @providerId, '{B9EF7263-1A91-4b47-B393-84B2D31BA62A}', @parentCategoryId
EXEC dbo.CreateOfferingCriteria '{90759617-9E55-40d9-8B85-D3484A28DE51}', @offeringId, 'Country', 1, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL
EXEC dbo.CreateOfferingCriteria '{D6E796EE-59B3-40ab-A926-3CB5E9B76F7E}', @offeringId, 'Country', 8, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL

SET @offeringId = '{F60851D9-D3EB-4b18-A759-A7A48E4C1DE5}'
EXEC dbo.CreateOffering @offeringId, 'Fundamentals of Contract Law', @providerId, '{5CFCA654-9AB5-4d6e-8316-84837C6C5FBE}', @parentCategoryId
EXEC dbo.CreateOfferingCriteria '{FC5050CF-6DE9-4d3f-83FC-BF359C32247A}', @offeringId, 'Country', 1, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL
EXEC dbo.CreateOfferingCriteria '{CB50F6D0-640C-455f-B4B5-3071358CBA16}', @offeringId, 'Country', 8, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL

SET @offeringId = '{2F9AB9C8-EECB-4d8c-A697-8A9F60ED4106}'
EXEC dbo.CreateOffering @offeringId, 'Contract Law Master Class', @providerId, '{22290CC1-7AD6-403d-AF46-507C21481E40}', @parentCategoryId
EXEC dbo.CreateOfferingCriteria '{576519A2-011E-43eb-AA41-A07EDB2126B7}', @offeringId, 'Country', 1, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL
EXEC dbo.CreateOfferingCriteria '{2E699033-D28B-41db-B534-55ACF7DF0E80}', @offeringId, 'Country', 8, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL

SET @offeringId = '{DCCB9C2E-C4CC-480d-9441-0108C4DD0DA5}'
EXEC dbo.CreateOffering @offeringId, 'Negotiating Professionally', @providerId, '{D73D9BB2-CB59-41cd-8DA3-3EE9D42A9441}', @parentCategoryId
EXEC dbo.CreateOfferingCriteria '{0DAC6E6C-E46E-473f-8D42-A7E84634BCA9}', @offeringId, 'Country', 1, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL
EXEC dbo.CreateOfferingCriteria '{80715E2E-EECB-4962-AADC-802A7D32B2F5}', @offeringId, 'Country', 8, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL

SET @offeringId = '{D9376E0D-1719-4178-BFE5-3D303AA6C42B}'
EXEC dbo.CreateOffering @offeringId, 'Negotiation Masterclass', @providerId, '{1F1FD096-E189-4dc9-A7E2-BD2FF861F735}', @parentCategoryId
EXEC dbo.CreateOfferingCriteria '{ED99E778-AF01-4a75-A3C9-9796C6E29499}', @offeringId, 'Country', 1, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL
EXEC dbo.CreateOfferingCriteria '{A44554EC-631A-4b66-A0FB-DF834AAF2DD3}', @offeringId, 'Country', 8, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL

SET @offeringId = '{A6C1258F-F98C-4b95-9AD2-E70E8E09B43F}'
EXEC dbo.CreateOffering @offeringId, 'Introduction to Category Management', @providerId, '{39786D1C-8767-4909-84A4-5D2278B58066}', @parentCategoryId
EXEC dbo.CreateOfferingCriteria '{644458A6-09D7-4458-9A48-B54F79EFB6B0}', @offeringId, 'Country', 1, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL
EXEC dbo.CreateOfferingCriteria '{FE9B9593-2716-45f6-9638-A59693215D1E}', @offeringId, 'Country', 8, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL

SET @offeringId = '{2EBD4F7E-EEF1-4cb0-BB75-924C06FE52B8}'
EXEC dbo.CreateOffering @offeringId, 'Advanced Category Management', @providerId, '{24D4879D-2740-49d4-B5A2-E91D8D35E30F}', @parentCategoryId
EXEC dbo.CreateOfferingCriteria '{E69608B2-0CC2-4aa6-AED7-9C68E5653BA8}', @offeringId, 'Country', 1, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL
EXEC dbo.CreateOfferingCriteria '{A612E970-0DAD-47fe-8C8D-969D5DF6049C}', @offeringId, 'Country', 8, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL

SET @offeringId = '{89E94157-D8D1-4821-8899-D61941950317}'
EXEC dbo.CreateOffering @offeringId, 'Whole of Life Contract Management', @providerId, '{DD1D6689-FF5C-4550-91EA-2530D2D893D2}', @parentCategoryId
EXEC dbo.CreateOfferingCriteria '{6E6DFF85-7ABC-464c-B28F-2FD465A68686}', @offeringId, 'Country', 1, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL
EXEC dbo.CreateOfferingCriteria '{8238CAB5-DC9D-44b0-85BF-7656BC80ADD2}', @offeringId, 'Country', 8, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL

SET @offeringId = '{445EE3BE-9B1F-4b17-8876-8E440EF16392}'
EXEC dbo.CreateOffering @offeringId, 'Supplier Relationship Management', @providerId, '{6E8C6B92-A95F-40b0-8810-3D5E8C08AE8A}', @parentCategoryId
EXEC dbo.CreateOfferingCriteria '{665B9F27-5E6C-421a-8FDC-E742B147B0F2}', @offeringId, 'Country', 1, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL
EXEC dbo.CreateOfferingCriteria '{9A4B740D-67DD-472a-8A10-03EA3A99200E}', @offeringId, 'Country', 8, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL

SET @offeringId = '{6474D24D-B181-4dfe-99D8-B4280E2AEA18}'
EXEC dbo.CreateOffering @offeringId, 'Specification Writing', @providerId, '{E5981F80-EE94-4b49-BE58-CFF84FE5A27A}', @parentCategoryId
EXEC dbo.CreateOfferingCriteria '{65F7B068-D4DB-4887-8E77-216C5F21735A}', @offeringId, 'Country', 1, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL
EXEC dbo.CreateOfferingCriteria '{BF69C733-5C2F-40ee-8900-D046F379815C}', @offeringId, 'Country', 8, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL

SET @offeringId = '{BEF50B38-40E1-4af4-A157-F29AEA7C3386}'
EXEC dbo.CreateOffering @offeringId, 'E-Auction Techniques and Management', @providerId, '{DEE54174-452F-4112-8784-7C3D35A40893}', @parentCategoryId
EXEC dbo.CreateOfferingCriteria '{F7452DC6-D4CC-44e1-B5DB-1377B0101415}', @offeringId, 'Country', 1, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL
EXEC dbo.CreateOfferingCriteria '{05B37D5B-B5CB-440d-BEA7-C39EC8CCACA4}', @offeringId, 'Country', 8, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL

SET @offeringId = '{9EA3F558-B331-4ff0-8CDE-97D59B863A63}'
EXEC dbo.CreateOffering @offeringId, 'Tender Evaluation', @providerId, '{2D9589C7-56EB-4b3f-BF29-F1F0BF72F89C}', @parentCategoryId
EXEC dbo.CreateOfferingCriteria '{37CE788D-2AC2-44a2-AC41-CE49444947C2}', @offeringId, 'Country', 1, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL
EXEC dbo.CreateOfferingCriteria '{53E8013C-A08E-432c-9CDF-CDD9C885B55B}', @offeringId, 'Country', 8, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL

SET @offeringId = '{5DCDEF4C-F9E3-4c83-BC06-94FBFC4FDACE}'
EXEC dbo.CreateOffering @offeringId, 'Risk Management', @providerId, '{B96066B6-39F5-4593-AB49-B7E43ED05A95}', @parentCategoryId
EXEC dbo.CreateOfferingCriteria '{F30611E9-9661-41e2-9B06-D93EA222FCFE}', @offeringId, 'Country', 1, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL
EXEC dbo.CreateOfferingCriteria '{D08CC09C-DE58-4efd-A5A6-5719F355C5F2}', @offeringId, 'Country', 8, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL

SET @offeringId = '{180FCE64-1FC8-469e-A73C-3DBABC9139F4}'
EXEC dbo.CreateOffering @offeringId, 'Reducing Total Cost', @providerId, '{ABAAA19A-C060-4241-9B23-0D728A1BAFF3}', @parentCategoryId
EXEC dbo.CreateOfferingCriteria '{6F8C855F-CAD0-4f25-AB87-C174B63A8C03}', @offeringId, 'Country', 1, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL
EXEC dbo.CreateOfferingCriteria '{826FA8FC-FA1E-47e5-98EC-92DAD9259E82}', @offeringId, 'Country', 8, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL

SET @offeringId = '{79AF6BB4-7B67-4320-8382-9BD3B7C8CAF7}'
EXEC dbo.CreateOffering @offeringId, 'Understanding Sales Techniques', @providerId, '{E1E53B68-D4D8-4cf6-B484-6658E22F2EDD}', @parentCategoryId
EXEC dbo.CreateOfferingCriteria '{E05B5A41-FD0C-48a7-9254-999913F938D8}', @offeringId, 'Country', 1, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL
EXEC dbo.CreateOfferingCriteria '{CEE7B514-D87A-49b7-A70C-5C0123F5BDC1}', @offeringId, 'Country', 8, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL

SET @offeringId = '{6EE6A35F-F98F-4852-A41F-0FEA85240480}'
EXEC dbo.CreateOffering @offeringId, 'Finance for Purchasers', @providerId, '{D6714141-3AFE-49d4-84B5-D10AAD7ABCAD}', @parentCategoryId
EXEC dbo.CreateOfferingCriteria '{0384838E-BF2D-4449-94FB-88DEBEA79869}', @offeringId, 'Country', 1, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL
EXEC dbo.CreateOfferingCriteria '{1F4CA0EF-3BA3-4ef5-8419-39487FE1B80E}', @offeringId, 'Country', 8, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL

SET @offeringId = '{D4A1E452-94E8-46b3-BB64-962473A73DF2}'
EXEC dbo.CreateOffering @offeringId, 'Change Management', @providerId, '{D541DD52-235A-48fc-BF6C-FC6029D39792}', @parentCategoryId
EXEC dbo.CreateOfferingCriteria '{F62D794F-CBF0-4c8f-BE8F-08F2BFCD6936}', @offeringId, 'Country', 1, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL
EXEC dbo.CreateOfferingCriteria '{CF280905-C048-4042-AA23-8378849AFE48}', @offeringId, 'Country', 8, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL

SET @offeringId = '{B6B42D90-2847-49f0-A71E-8F80C5A6CF84}'
EXEC dbo.CreateOffering @offeringId, 'Service Level Agreement', @providerId, '{3B350B1C-2D31-4914-9BE4-0A33FC189931}', @parentCategoryId
EXEC dbo.CreateOfferingCriteria '{ADFC3C31-F0FB-4761-9796-139DF229BA42}', @offeringId, 'Country', 1, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL
EXEC dbo.CreateOfferingCriteria '{D2C3C0A9-C1A9-4f46-BA29-E9A99B6D0E2F}', @offeringId, 'Country', 8, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL

SET @offeringId = '{B1E30063-3E08-45ba-AF60-4DA8A7DEEC05}'
EXEC dbo.CreateOffering @offeringId, 'Gain accreditation in Procurement & Purchasing', @providerId, '{BD15A677-251A-438f-9A07-CDE16355ACA0}', @parentCategoryId
EXEC dbo.CreateOfferingCriteria '{6EFE5C9D-CFEE-4cc8-911F-43786D94B5F3}', @offeringId, 'Keywords', 'Purchasing OR Procurement', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL

-- Professional Associations

SET @parentCategoryId = '23662854-51CF-4824-AB3D-8BC72D46E5CB'

SET @categoryId = '{8FC5C45D-8786-40d1-8084-A1D25E28222C}'
EXEC dbo.CreateOfferCategory @categoryId, @parentCategoryId, 'Procurement & Purchasing'

SET @parentCategoryId = @categoryId
SET @offeringId = '{992BE620-DA3B-49ea-8F1E-DE4FACBABC86}'
EXEC dbo.CreateOffering @offeringId, 'Association for Purchasing & Supply Chain Management', @providerId, '{9D709818-C74D-42ba-8689-3DEAB876AAFC}', @parentCategoryId
