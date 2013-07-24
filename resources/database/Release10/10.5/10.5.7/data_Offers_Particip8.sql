DECLARE @providerId UNIQUEIDENTIFIER
SET @providerId = '{3C805047-423D-4f0e-9A63-EC7D863F2348}'

DECLARE @parentCategoryId UNIQUEIDENTIFIER
DECLARE @offeringId UNIQUEIDENTIFIER

-- Provider

EXEC dbo.CreateOfferProvider @providerId, 'Particip8'

-- Further education

SET @parentCategoryId = 'FAF62B20-0302-4E86-BDB5-8EE27443FF71'
EXEC dbo.CreateOffering '{C4E80233-DED6-48ea-A807-5416EC27453B}', 'MYOB online training', @providerId, '{410B4C71-33F5-4d21-91C2-53D23B29A4A6}', @parentcategoryId

SET @parentCategoryId = 'E69254E0-F5E6-47C2-899C-8041D77EEF8B'
EXEC dbo.CreateOfferCategory '{9E3AD77F-3128-4e04-AEF8-10344067E416}', @parentCategoryId, 'Computer Skills Courses from $39.95 (online)'

SET @parentCategoryId = '{9E3AD77F-3128-4e04-AEF8-10344067E416}'
EXEC dbo.CreateOffering '{4BC25CD2-DDCD-4592-8917-14DB6B506E48}', 'MYOB online training package', @providerId, '{8497C426-D052-43a2-BC3B-8AA09E1B72C2}', @parentcategoryId
EXEC dbo.CreateOffering '{BDAB8D4E-0AB0-44a2-81E5-1C55ED504C51}', 'Microsoft Office 2003 Pack', @providerId, '{2BE6FCD6-6EF7-4b00-9815-462D9D9AE9FE}', @parentcategoryId
EXEC dbo.CreateOffering '{B0FE3F0B-7473-4316-BAF6-903CE0A0057E}', 'Microsoft Office 2007 Pack', @providerId, '{C2A373D5-6AD9-4878-93C5-A00248AFF3F0}', @parentcategoryId
EXEC dbo.CreateOffering '{5C013A7E-D2E7-4831-8566-31E843C3AE6E}', 'Word 2003', @providerId, '{B3F724BA-895A-4470-975B-B3B2F182A8C4}', @parentcategoryId
EXEC dbo.CreateOffering '{F1B84577-9733-414b-A808-55E8F680BCE2}', 'Word 2003 Advanced', @providerId, '{58B68143-6C67-4c1a-9EA2-B9797DC41A97}', @parentcategoryId
EXEC dbo.CreateOffering '{EAD75599-5BBC-46d8-8FF5-3CBFC6D1B518}', 'Excel 2003', @providerId, '{D1D6EA04-24A9-45b7-A928-D3530E644CA5}', @parentcategoryId
EXEC dbo.CreateOffering '{D7E4B1A9-D645-46bb-A49E-899462D0847B}', 'Excel 2003 Advanced', @providerId, '{7BCF4D46-844E-4306-9139-189FB58DC58F}', @parentcategoryId
EXEC dbo.CreateOffering '{3770F586-95B9-47ff-B787-DCB1CFD9746C}', 'Powerpoint 2003', @providerId, '{9C5EDE77-7EB2-4b20-9F40-941C870DCA94}', @parentcategoryId
EXEC dbo.CreateOffering '{60EB8DE4-C733-4311-B722-F4148B39AC59}', 'Outlook 2003', @providerId, '{3CDBCCAA-22A0-4cea-853B-376C112DFB00}', @parentcategoryId
EXEC dbo.CreateOffering '{DFF60B66-7FA6-4bfa-A184-FAFD88801871}', 'Access 2003', @providerId, '{211C508A-3B10-4d41-B0C5-B85686A0EB7A}', @parentcategoryId
EXEC dbo.CreateOffering '{510E576E-CE2E-4f67-B174-56055860B05C}', 'Windows XP', @providerId, '{81F3A673-E731-463a-BD7B-815238163685}', @parentcategoryId
EXEC dbo.CreateOffering '{60390CF6-24A2-4817-AE40-763A5FA8E5ED}', 'Word 2007', @providerId, '{8D94BC09-AFB4-4caa-913B-35983E01405E}', @parentcategoryId
EXEC dbo.CreateOffering '{A5836383-EE98-44b9-AD47-1A6430BBA7A9}', 'Excel 2007', @providerId, '{3F556625-63E8-4e09-AA61-B8021E4059A7}', @parentcategoryId
EXEC dbo.CreateOffering '{DAEB2CBF-6556-4ee8-B5A1-68A600A901D5}', 'Powerpoint 2007', @providerId, '{1DAFC3B6-52D5-48cf-9B30-A12129CF8E5F}', @parentcategoryId
EXEC dbo.CreateOffering '{4DA6D015-7F68-4c7b-90AF-34971B85FBAA}', 'Access 2007', @providerId, '{A2F9F905-5E3D-47fd-9C32-725887217155}', @parentcategoryId
EXEC dbo.CreateOffering '{5DEAEFD4-7F01-44ea-8E2E-AF84A1915CB0}', 'Publisher 2007', @providerId, '{C56086A5-FF65-4809-8318-28349E12B61A}', @parentcategoryId
EXEC dbo.CreateOffering '{F0368B92-C2CA-44f8-8523-06465F751FC9}', 'Outlook 2007', @providerId, '{DD273442-9396-41f4-8F38-12A558B5BDDC}', @parentcategoryId
EXEC dbo.CreateOffering '{A2D7D662-06CF-4839-BD5F-148E5D187892}', 'Windows Vista 2007', @providerId, '{5C02C246-8DF7-4f81-A02F-D4AC0FA09898}', @parentcategoryId
EXEC dbo.CreateOffering '{091EC460-6D21-4e3d-B8CE-529E95692E5A}', 'Introduction to Personal Computers', @providerId, '{A16672A2-4B05-4fd5-8C9C-46F82170A9F8}', @parentcategoryId
EXEC dbo.CreateOffering '{623013C9-BC58-4f24-A02A-BF3AC932EAE7}', 'The World Wide Web', @providerId, '{9E0D45B8-F7EE-4cb3-B45F-93A48DE8E26C}', @parentcategoryId
EXEC dbo.CreateOffering '{2FD3C218-7BDB-4202-95CD-C47FBB323DEF}', 'Searching the internet effectively', @providerId, '{58227AB0-25DF-453e-B95A-4CE75640BE88}', @parentcategoryId

-- Online education

SET @parentCategoryId = '945BDF92-49CC-49ED-AA66-2E8336471E76'
EXEC dbo.CreateOfferCategory '{E723702C-45DF-497a-8015-4C00BA161707}', @parentCategoryId, 'Computer Skills Courses from $39.95 (online)'

SET @parentCategoryId = '{E723702C-45DF-497a-8015-4C00BA161707}'
EXEC dbo.CreateOffering '{4BC25CD2-DDCD-4592-8917-14DB6B506E48}', 'MYOB online training package', @providerId, '{4C6011E8-A2F1-4b00-8A45-5E242AA596CF}', @parentcategoryId
EXEC dbo.CreateOffering '{BDAB8D4E-0AB0-44a2-81E5-1C55ED504C51}', 'Microsoft Office 2003 Pack', @providerId, '{541C6733-E431-4337-999B-DC970FE9A3AD}', @parentcategoryId
EXEC dbo.CreateOffering '{B0FE3F0B-7473-4316-BAF6-903CE0A0057E}', 'Microsoft Office 2007 Pack', @providerId, '{B0B9DAC2-B225-4d6c-BDBA-8396EF05E26B}', @parentcategoryId
EXEC dbo.CreateOffering '{5C013A7E-D2E7-4831-8566-31E843C3AE6E}', 'Word 2003', @providerId, '{C7EF6EF6-8EEB-4f15-9EBF-56498F917349}', @parentcategoryId
EXEC dbo.CreateOffering '{F1B84577-9733-414b-A808-55E8F680BCE2}', 'Word 2003 Advanced', @providerId, '{7FE50D46-A432-4675-9B73-727303459059}', @parentcategoryId
EXEC dbo.CreateOffering '{EAD75599-5BBC-46d8-8FF5-3CBFC6D1B518}', 'Excel 2003', @providerId, '{45FD4769-D926-47b3-873F-3A1749D9EB85}', @parentcategoryId
EXEC dbo.CreateOffering '{D7E4B1A9-D645-46bb-A49E-899462D0847B}', 'Excel 2003 Advanced', @providerId, '{0CB83F2F-6582-4419-BBD3-EA0AD74A6FD6}', @parentcategoryId
EXEC dbo.CreateOffering '{3770F586-95B9-47ff-B787-DCB1CFD9746C}', 'Powerpoint 2003', @providerId, '{FAE41D6C-E864-4e45-A767-90D87E6CD9BF}', @parentcategoryId
EXEC dbo.CreateOffering '{60EB8DE4-C733-4311-B722-F4148B39AC59}', 'Outlook 2003', @providerId, '{04244965-3B4D-4e86-AE5B-10809FBAD534}', @parentcategoryId
EXEC dbo.CreateOffering '{DFF60B66-7FA6-4bfa-A184-FAFD88801871}', 'Access 2003', @providerId, '{6C082BFE-8236-4ff6-A9B3-6373757CA578}', @parentcategoryId
EXEC dbo.CreateOffering '{510E576E-CE2E-4f67-B174-56055860B05C}', 'Windows XP', @providerId, '{C17D7FFF-76C1-4793-8E99-CE21ECBF2D0C}', @parentcategoryId
EXEC dbo.CreateOffering '{60390CF6-24A2-4817-AE40-763A5FA8E5ED}', 'Word 2007', @providerId, '{435D1A5B-E53C-4861-8C5A-10ACC61AC8EE}', @parentcategoryId
EXEC dbo.CreateOffering '{A5836383-EE98-44b9-AD47-1A6430BBA7A9}', 'Excel 2007', @providerId, '{B4319093-77CF-479e-85CB-5EE4D3A5BC4B}', @parentcategoryId
EXEC dbo.CreateOffering '{DAEB2CBF-6556-4ee8-B5A1-68A600A901D5}', 'Powerpoint 2007', @providerId, '{46ADFBEC-9BB7-42e4-BD2C-1AF1F8771A50}', @parentcategoryId
EXEC dbo.CreateOffering '{4DA6D015-7F68-4c7b-90AF-34971B85FBAA}', 'Access 2007', @providerId, '{2A51A8D9-415F-4c8a-89EC-C246AA01F838}', @parentcategoryId
EXEC dbo.CreateOffering '{5DEAEFD4-7F01-44ea-8E2E-AF84A1915CB0}', 'Publisher 2007', @providerId, '{457121DA-D6E2-45a1-B059-F8018651B5E9}', @parentcategoryId
EXEC dbo.CreateOffering '{F0368B92-C2CA-44f8-8523-06465F751FC9}', 'Outlook 2007', @providerId, '{0EDCEF10-D051-4edf-916B-987B48DC5252}', @parentcategoryId
EXEC dbo.CreateOffering '{A2D7D662-06CF-4839-BD5F-148E5D187892}', 'Windows Vista 2007', @providerId, '{41D82291-DFAC-4603-94C3-A756236C0D69}', @parentcategoryId
EXEC dbo.CreateOffering '{091EC460-6D21-4e3d-B8CE-529E95692E5A}', 'Introduction to Personal Computers', @providerId, '{E9365897-414A-4ac3-9AAE-C7FE03CEF87F}', @parentcategoryId
EXEC dbo.CreateOffering '{623013C9-BC58-4f24-A02A-BF3AC932EAE7}', 'The World Wide Web', @providerId, '{4F9AC91D-924F-4235-ADEB-6A2B41813414}', @parentcategoryId
EXEC dbo.CreateOffering '{2FD3C218-7BDB-4202-95CD-C47FBB323DEF}', 'Searching the internet effectively', @providerId, '{DD9E2EDD-4F5F-4d38-8B45-03D2C036C890}', @parentcategoryId

