DECLARE @equivalentId UNIQUEIDENTIFIER

DELETE FROM dbo.EquivalentTerms

SET @equivalentId = 'fa0aab00-cbcd-4e05-b4c7-666d6f924bf9'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('1', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('one', @equivalentId)

SET @equivalentId = '09be3fe3-5b49-4688-b39b-6fa0003198df'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('2', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('two', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('ii', @equivalentId)

SET @equivalentId = 'f831a7fb-33ae-46c0-9401-d5ffca044d0f'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('3', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('three', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('iii', @equivalentId)

SET @equivalentId = 'ebfc4299-cc18-45eb-b474-74059b278cca'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('4', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('four', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('iv', @equivalentId)

SET @equivalentId = '703c1bd9-25c8-44fd-ae44-338bd8837fe6'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('5', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('five', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('v', @equivalentId)

SET @equivalentId = 'e7b1adc4-b36a-45f8-b491-a1168e688e7a'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('6', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('six', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('vi', @equivalentId)

SET @equivalentId = 'abcf25a2-62e7-4306-8f12-1bea29059218'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('7', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('seven', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('vii', @equivalentId)

SET @equivalentId = 'e89ae5b4-bf11-4bef-afdb-b9471f7ed71d'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('8', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('eight', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('viii', @equivalentId)

SET @equivalentId = '735ca1f5-eb81-48a6-8a49-fbc4472f8585'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('9', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('nine', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('ix', @equivalentId)

SET @equivalentId = '801c5492-412f-4b5a-ade3-096cdb7bc1fc'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('10', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('ten', @equivalentId)

SET @equivalentId = '6a429e9f-8858-4059-b788-0983af0e75e5'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('1st', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('first', @equivalentId)

SET @equivalentId = 'c39cede1-f7a8-4007-92fd-7ff1e038c4a9'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('3ic', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('3.i.c.', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('3i.c.', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Third in charge', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('3 IC', @equivalentId)

SET @equivalentId = 'd82f449d-a43c-446a-b4f1-dbe6995be120'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('2nd', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('second', @equivalentId)

SET @equivalentId = 'a522c31a-4375-47d4-9be1-dccfde48dcf5'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('3rd', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('third', @equivalentId)

SET @equivalentId = '0d5ee061-55e4-4bcf-bdb2-7ee165c05c8b'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('4th', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('forth', @equivalentId)

SET @equivalentId = 'c303e8d8-cf0f-45ad-b3bf-21cbb4671174'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('class 1', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('class1', @equivalentId)

SET @equivalentId = '3df45dbd-ef20-40b2-90de-01ae90140e41'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('class 2', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('class2', @equivalentId)

SET @equivalentId = 'a7fd3b05-d077-4f2a-86e5-b60f3af72325'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('class 3', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('class3', @equivalentId)

SET @equivalentId = 'a2dca740-0761-4133-9b8b-abb2622dad86'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('class 4', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('class4', @equivalentId)

SET @equivalentId = '144ebb2c-63cb-42bf-b99a-ed4bb132d0dc'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('class 5', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('class5', @equivalentId)

SET @equivalentId = '5b012ee7-af4e-4c0b-bdc2-af6f4be7af4e'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('phase 1', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('phase1', @equivalentId)

SET @equivalentId = '93017cdc-d963-4548-b5dc-124348ca65fb'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('phase 2', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('phase2', @equivalentId)

SET @equivalentId = 'bfe5feb5-0107-4fe4-900b-d25c5d82244c'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('phase 3', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('phase3', @equivalentId)

SET @equivalentId = '9c0d057d-5ccc-4311-986c-c775db4f33bd'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('phase 4', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('phase4', @equivalentId)

SET @equivalentId = '9d8fd423-a827-4117-8690-4d0369fbf2cf'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('phase 5', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('phase5', @equivalentId)

SET @equivalentId = '85e858a7-bdad-467f-894d-7e22d829f3b9'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('after sales', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('aftersales', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('post sales', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('postsales', @equivalentId)

SET @equivalentId = '475a4042-7a75-4cbf-9371-f94dfcab52a8'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Aboriginal', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('indigenous', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('torres straight islander', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('torres straight islands', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Koori', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('australian aboriginal', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Torres Strait Islander', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('koorie', @equivalentId)

SET @equivalentId = 'a7b748e7-4b9c-479c-a0de-b5619da1214a'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('ADSL', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Asymmetric Digital Subscriber Line', @equivalentId)

SET @equivalentId = '041e5b0c-1a76-48da-8086-5f8fd0fa6e67'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('fibre optic', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('fibreoptic', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('optical fibre', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('optical fiber', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('fiber optic', @equivalentId)

SET @equivalentId = '5717018a-e6fb-424c-bc57-4b938956d1b5'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('ag', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('agriculture', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('agricultural', @equivalentId)

SET @equivalentId = 'e3b55f3c-fb69-4e12-a3d3-13d7f4cc3a05'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('mx road', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('mxroad', @equivalentId)

SET @equivalentId = 'f97671b5-ffa3-43e8-8061-86dfcde723b6'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('substation', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('sub station', @equivalentId)

SET @equivalentId = '73739202-1f88-4845-b293-e6b6028c8636'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('qem', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('qem scan', @equivalentId)

SET @equivalentId = '1e118e58-4354-4e8e-9953-5efebae67a26'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('cold store', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('coldstore', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('coolroom', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('cool room', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('cold storage', @equivalentId)

SET @equivalentId = '54abe195-e756-4e55-bcf3-2e6137756086'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('asap', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('as soon as possible', @equivalentId)

SET @equivalentId = '0735fee6-64a6-4243-97db-1aa8dbc45862'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('ASX', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Australian stock exchange', @equivalentId)

SET @equivalentId = 'd546da76-8d72-4c85-ba11-2e268b1cd4d0'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('aust', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('australia', @equivalentId)

SET @equivalentId = '362e16d4-a29e-4a9e-a64e-497ee73c669e'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('bas', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('business activity statement', @equivalentId)

SET @equivalentId = '469fb0f7-b2b2-4ac8-868f-dbd08e8e296b'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('BHP', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('BHPBilliton', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('bhp billiton', @equivalentId)

SET @equivalentId = 'd508b6e1-c84b-4f30-bd40-7c38e3709dba'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('rio tinto', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('riotinto', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('rtz', @equivalentId)

SET @equivalentId = '6a5b04d7-c84d-412d-a262-5ea965fc2a54'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('ghd', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Gutteridge Haskins & Davey', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Haskins and Davey', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Gutteridge Haskins and Davey', @equivalentId)

SET @equivalentId = '743470a0-46f7-4c94-9a34-1e7c159b8fc4'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('bris', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('brisbane', @equivalentId)

SET @equivalentId = 'c2005556-99e8-4dfa-95da-06b556e2fed5'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('cam', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('computer aided manufacturing', @equivalentId)

SET @equivalentId = 'de6b85f1-65dc-49a7-a93a-978106e98259'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('cbd', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('central business district', @equivalentId)

SET @equivalentId = '2067420f-c1cb-44d4-b08a-7f57345c605a'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('cctv', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('closed circuit tv', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('closed circuit television', @equivalentId)

SET @equivalentId = '90d0d941-e6ba-45b5-8071-5221f3dc2b1c'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('cert', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('certificate', @equivalentId)

SET @equivalentId = '8ab99be1-021c-4d10-b98d-295a90fc171a'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('cert1', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('certificate 1', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('certi', @equivalentId)

SET @equivalentId = '9d03a2b2-1411-4bbb-b2f6-36192a00fc63'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('cert2', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('certificate 2', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('certii', @equivalentId)

SET @equivalentId = '5c52ec36-6b81-4a9d-a39b-0888c5b2b135'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('cert3', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('certificate 3', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('certiii', @equivalentId)

SET @equivalentId = 'a99efec0-da6b-4bff-890e-fd44257c139d'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('contract', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('agreement', @equivalentId)

SET @equivalentId = 'd32939ce-ac64-4198-b76f-a91f4513aea2'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('CPU', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('central processing unit', @equivalentId)

SET @equivalentId = 'cdca9512-77f2-40b9-8ab5-77467168d996'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('dispatch', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('despatch', @equivalentId)

SET @equivalentId = '87f6b690-a798-4975-a4be-d7c7cba14b0f'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('div', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('division', @equivalentId)

SET @equivalentId = '672ed1f9-d9d5-4cdd-96c0-847289e0e4ab'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('DNS', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Domain Name System', @equivalentId)

SET @equivalentId = '1af9d6ce-d71d-4618-82da-6f3797045d6e'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('drp', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('disaster recovery plan', @equivalentId)

SET @equivalentId = '6ccf460f-a4f9-4db3-ac06-3cf12a7e390a'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('DSL', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Digital Subscriber Line', @equivalentId)

SET @equivalentId = 'd27c6f66-90d0-431c-8e98-965fb07f0029'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('email', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('e-mail', @equivalentId)

SET @equivalentId = 'c3db1107-cbe2-42d1-8dbe-d73fff7cdb3e'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('epcm', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Engineering Procurement Construction Management', @equivalentId)

SET @equivalentId = '5373bb3f-388f-4061-a92b-8a53b1489104'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('EPS', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('earnings per share', @equivalentId)

SET @equivalentId = '56d0adfd-6b0f-40a4-bcaa-6b5efb0fd413'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('exp', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('experience', @equivalentId)

SET @equivalentId = 'ee5d724b-2b76-4e2e-b825-d5b3b7178985'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('fifo', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('first in first out', @equivalentId)

SET @equivalentId = 'f0a5534c-300c-4c59-95b8-5da294b06a06'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('govt', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('government', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('gov''t', @equivalentId)

SET @equivalentId = '987e4426-a67e-440a-a8fd-c692dd917b67'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('GPS', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Global Positioning System', @equivalentId)

SET @equivalentId = 'fa71532b-9a8b-4c02-b44f-f609e3c7e3c7'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('guard', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('gaurd', @equivalentId)

SET @equivalentId = '88ef5510-688c-413f-8cf6-b198986c2032'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('hp', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('hewlett packard', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('hewlettpackard', @equivalentId)

SET @equivalentId = 'cbc1078f-1d02-4470-8ae3-18b61cd35809'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('hris', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('human resource information system', @equivalentId)

SET @equivalentId = 'c831c88c-0e93-4413-8d50-59da3c2b2710'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('infra structure', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('infrastructure', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('infra-structure', @equivalentId)

SET @equivalentId = 'eb5592a6-0524-4e23-8326-ea01c9f37f05'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('jdedwards', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('jd edwards', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('j.d.edwards', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('jde', @equivalentId)

SET @equivalentId = 'bb05bce1-be15-4fc8-9f04-75ca6baf600f'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('jnr', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('junior', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('jr', @equivalentId)

SET @equivalentId = 'c0e481e7-55b2-4266-8db9-a0f2df1fbbd2'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('KPI', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('key performance indicator', @equivalentId)

SET @equivalentId = 'fadd27cb-91c5-42ad-b852-298934798534'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('l&d', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('learning and development', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('training', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('l and d', @equivalentId)

SET @equivalentId = 'a4d6bc97-3bda-4362-92f0-cc72515e60f6'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('labor', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('labour', @equivalentId)

SET @equivalentId = '8025b9b7-d231-4776-b190-9ff82dece5ff'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('lan', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('local area network', @equivalentId)

SET @equivalentId = 'a72ee217-a62d-4491-a83f-bd69163f5ae9'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('ltd', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('limited', @equivalentId)

SET @equivalentId = '48f6db7c-159c-47dd-8f55-2e2b137fba25'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Mac', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Macintosh', @equivalentId)

SET @equivalentId = 'f8ce7f8a-98b3-4984-bc3b-a1c87f691325'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('macq', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('macquarie', @equivalentId)

SET @equivalentId = 'c13b5d14-03ba-45d5-8ea0-2450409a1ba0'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('mba', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('master of business administration', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('master of business', @equivalentId)

SET @equivalentId = 'cc5f0bf5-8323-4c89-b9e0-2ad390bfe675'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('mech', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('mechanical', @equivalentId)

SET @equivalentId = '78812060-8ce2-40e5-90b7-d681e6f4847d'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('med', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('medical', @equivalentId)

SET @equivalentId = 'eafbbe14-d1a6-4b5f-b5bd-37a4dbeb1b96'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('melb', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('melbourne', @equivalentId)

SET @equivalentId = '00e4fdc3-1fe7-4578-8c6e-df4c9e915ca6'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('mngt', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('management', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('mgmt', @equivalentId)

SET @equivalentId = 'ff58217b-6599-4818-b62c-3e4d4440732b'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('mr', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('medium rigid', @equivalentId)

SET @equivalentId = 'c7276acf-5a20-4988-b6c8-f7dc9e715235'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('ms', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('microsoft', @equivalentId)

SET @equivalentId = 'd14fb93f-575c-4888-aac5-569887f9ab3d'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('NAB', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('National Australia Bank', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('National Bank of Australia', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('National Bank', @equivalentId)

SET @equivalentId = '68ab7456-f8dd-425e-940e-f8a19b9df84b'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('abigroup', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('abi group', @equivalentId)

SET @equivalentId = 'ad8747d0-7470-41a1-accb-64799147cbc2'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('north east', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('northeast', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('north-east', @equivalentId)

SET @equivalentId = 'a405526f-5c85-450b-b50d-0c0e409920a6'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('neg', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('negotiable', @equivalentId)

SET @equivalentId = 'a258753a-3b78-439d-8dc0-b5b17bed9c84'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('nsw', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('new south wales', @equivalentId)

SET @equivalentId = '88669b57-1e28-49c1-972a-e0b6bfdcf957'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('north west', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('northwest', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('north-west', @equivalentId)

SET @equivalentId = '5c965c59-8546-4ee3-8642-1ee5ebfb8ecb'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('nyse', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('new york stock exchange', @equivalentId)

SET @equivalentId = 'a064a320-d2a6-47f5-b250-9da97467e0c7'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('nz', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('new zealand', @equivalentId)

SET @equivalentId = '5256481d-6f64-4eb7-8f3a-b2917f9fe81a'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('OHS', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('OH&S', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Occupational Health and Safety', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Occupational Health & Safety', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('oh &s', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('ehs', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('environmental health and safety', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('oh/s', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Health & Safety', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Health and Safety', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Health&Safety', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('healthandsafety', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('hsse', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Health Safety Security Environment', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Health Safety Security and Environment', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('HSSE&T', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Health Safety Security Environment training', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Health Safety Security Environment and training', @equivalentId)

SET @equivalentId = '4058f18c-44cb-4a6c-8674-60bb8c281b30'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Oil and Gas', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Oil&Gas', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Oil & Gas', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('petroleum', @equivalentId)

SET @equivalentId = 'a553eb04-9377-4014-a0c1-545bb78c4fce'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('online', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('on-line', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('digital', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('internet based', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('web', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('internet', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('world wide web', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('www', @equivalentId)

SET @equivalentId = 'c8f6caf1-dc71-40c4-a18a-6ce9e85fb6fb'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('website', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('web site', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('internet site', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('webpage', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('web page', @equivalentId)

SET @equivalentId = 'c44a1131-67d8-44d0-876d-bfc494c5a070'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('os', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('operating system', @equivalentId)

SET @equivalentId = 'f1ee5582-ef82-4cb3-b57e-b2ad7a202695'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('ot', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('occupational therapist', @equivalentId)

SET @equivalentId = '38f400e1-64a4-4eb8-b15f-16d81494223c'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('p&l', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('profit and loss', @equivalentId)

SET @equivalentId = '5451eb5e-00fd-4e9f-8264-f99f1aed72ec'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('pae', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('participating administrative entity', @equivalentId)

SET @equivalentId = '456db54c-3c06-48ac-a91f-d66bbbda0d97'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('payg', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('pay as you go', @equivalentId)

SET @equivalentId = '92c888d2-eed9-456b-b99c-30e9b7a02acd'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('perm', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('permanent', @equivalentId)

SET @equivalentId = '55fe9e96-ddf1-46d6-a41e-7d64a88554ca'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('pi', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('personal injury', @equivalentId)

SET @equivalentId = 'dd7864c7-9e5f-44e1-9b45-f6e7bd4e9b4b'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('plc', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('programmable logic controller', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('programmed logic controller', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('programmable logic control', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('programmed logic control', @equivalentId)

SET @equivalentId = '45fe94a3-b005-4ec3-ab53-ed9bb6b172b6'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('POS', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Point of Sale', @equivalentId)

SET @equivalentId = '9b72f52a-861e-4c1c-a59d-2c965d8149d0'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('proactive', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('pro active', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('pro-active', @equivalentId)

SET @equivalentId = '961c9ef4-fc8c-4d74-8e1f-fb99574936e8'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('pty', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('proprietary', @equivalentId)

SET @equivalentId = '8f1aafa2-bfd6-4b5d-9352-c47f16315033'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Q&A', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Quality and Assurance', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Quality & Assurance', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('qa', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('quality assurance', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('quality control', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('qc', @equivalentId)

SET @equivalentId = '7b4e0795-cf49-4450-b7fa-7b9141eacdd9'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('qld', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('queensland', @equivalentId)

SET @equivalentId = 'eb844d9b-62fa-4447-bf2c-4554dbce4259'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('qual', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('qualification', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('quals', @equivalentId)

SET @equivalentId = '0fe0d794-1fa5-4044-9dca-117adf4e3c53'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('r and d', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('research and development', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('research & development', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('rnd', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('r & d', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('r&d', @equivalentId)

SET @equivalentId = '33abc5ed-a06a-4d47-971d-4e6b5cde90d5'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('reengineering', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('re engineering', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('re-engineering', @equivalentId)

SET @equivalentId = '53fba52d-922e-4159-94fa-07699dd9ad0b'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('ref', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('reference', @equivalentId)

SET @equivalentId = '0828cd36-2c6e-4da1-8b59-7c88407cf7c4'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('req', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('requirement', @equivalentId)

SET @equivalentId = 'fe0dc1a4-b260-4320-9445-e7ea3b67539f'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('rf', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('radio frequency', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('radio transmission', @equivalentId)

SET @equivalentId = 'a6ad0348-c03b-4cbc-a772-3abe2079407b'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('ROE', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('return on equity', @equivalentId)

SET @equivalentId = '06d4c5ac-cc04-43ea-b944-92ae8a75f603'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('ROI', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('return on investment', @equivalentId)

SET @equivalentId = 'be9fad22-8136-4ad4-973d-8e47544a33cc'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('romp', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Radiation Oncology Medical Physics', @equivalentId)

SET @equivalentId = 'd2343c8f-e1f3-4d65-8883-d634180af90b'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('rta', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('road traffic authority', @equivalentId)

SET @equivalentId = '3d183990-45e7-4495-9026-83254cd7f7d8'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('rtw', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('return to work', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('return-to-work', @equivalentId)

SET @equivalentId = '0685851d-5127-4e2c-84ad-0d4d06f97074'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('sov', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('sale of value', @equivalentId)

SET @equivalentId = '75bc0102-9ccf-4cc4-9c7f-9c314c141f54'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('sap', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('s.a.p.', @equivalentId)

SET @equivalentId = 'cb1cd456-7954-47e2-8428-a4d49df4241e'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('SAP EP', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Enterprise Portal', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('sapep', @equivalentId)

SET @equivalentId = '4f422a50-706f-4071-8b33-ce3faf008865'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('ess', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('enterprise security', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('ess/mss', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('enterprise security segment', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('enterprise security system', @equivalentId)

SET @equivalentId = 'bc983db2-f4dc-49bf-9d13-95bbf66cabb1'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('mss', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('mission support', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('mission support system', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('mission support segment', @equivalentId)

SET @equivalentId = 'c9582f0c-dd09-4644-994d-2be8ff4b8323'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('south east', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('southeast', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('south-east', @equivalentId)

SET @equivalentId = 'fa04344d-4071-4704-9775-a1531e51ee95'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('south west', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('southwest', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('south-west', @equivalentId)

SET @equivalentId = 'aab9d85c-cc00-4d9b-bc4d-e751e41ac3d0'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('SEM', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Search Engine Marketing', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('search marketing', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('S E M', @equivalentId)

SET @equivalentId = 'ccfbb856-36fe-489a-8f79-8e63547d8954'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('SEO', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Search Engine Optimisation', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('S E O', @equivalentId)

SET @equivalentId = '7868ec84-6d99-4400-b5a3-ae91433aceed'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('PPC', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('price per click', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('priceperclick', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('P P C', @equivalentId)

SET @equivalentId = 'fd6b74a6-7fe7-4e7f-add1-e1e3c0f7b59c'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('CPC', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('cost per click', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('costperclick', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('C P C', @equivalentId)

SET @equivalentId = '9f09b406-8a34-4d33-9c61-20a481c7d928'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('PPA', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('price per applicant', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('priceperapplicant', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('P P A', @equivalentId)

SET @equivalentId = '39097c36-4860-4c8d-9cb3-8c2f088f3f5c'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('cpm', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('cost per thousand', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('costperthousand', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('c p m', @equivalentId)

SET @equivalentId = '3d716809-e9a9-401b-be35-aedc705bdebf'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('share point', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('sharepoint', @equivalentId)

SET @equivalentId = '9d0417b4-4a57-4cc0-95ae-fb408964de5a'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('sme', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('small and medium enterprise', @equivalentId)

SET @equivalentId = 'ffa4fe74-6537-42b6-9e3e-35f4f50f0581'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('sml', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('small', @equivalentId)

SET @equivalentId = '2d5d9df9-ebe7-49df-9547-9898062031c2'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('snr', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('senior', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('sr', @equivalentId)

SET @equivalentId = '2d36f2b6-a5ea-4e9a-b720-a232afd1733c'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('software', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('soft ware', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('soft-ware', @equivalentId)

SET @equivalentId = '4c262c80-2701-4a04-b581-39e4ad511ae0'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('strategic', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('strategy', @equivalentId)

SET @equivalentId = '88be271c-4049-487d-b498-581ca963c89a'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('syd', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('sydney', @equivalentId)

SET @equivalentId = 'c1077672-b0bb-4b66-8752-50a325dda8ac'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('temp', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('temporary', @equivalentId)

SET @equivalentId = '3cd070d1-8404-4fa6-b1f2-5196b10c35c8'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('through', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('thru', @equivalentId)

SET @equivalentId = 'c9f9f945-17b7-4dfc-a95e-98eabd900b32'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('tire', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('tyre', @equivalentId)

SET @equivalentId = '7c5294bf-6dc9-47b5-86fc-fc35fe0a352f'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('tkt', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('ticket', @equivalentId)

SET @equivalentId = '8251c0fb-60b8-4b90-b455-6c3a39e3e14e'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('trim', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Total Records and Information Management', @equivalentId)

SET @equivalentId = '9e61efa2-7c98-4776-b690-b789a0189220'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('tv', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('television', @equivalentId)

SET @equivalentId = '39e00f7a-f798-4329-b8dc-3cc639424fe1'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('uat', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('user acceptance testing', @equivalentId)

SET @equivalentId = '6a561abc-c231-4b12-ab79-005c0be67a60'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('uni', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('university', @equivalentId)

SET @equivalentId = 'f0766152-a609-4a03-8a04-2f406da9f588'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('cipsa', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('cips', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('chartered institute of purchasing', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('chartered institute of purchasing and supply', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('chartered institute of purchasing and supply australasia', @equivalentId)

SET @equivalentId = 'c902b81f-37e7-4aa8-9686-c8189066a6c1'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('vic', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('victoria', @equivalentId)

SET @equivalentId = '39d301ab-9b54-430a-8378-61dc5f298823'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('act', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('australian captial territory', @equivalentId)

SET @equivalentId = '85d94468-5b1d-444c-b2a4-7e79d083c3a6'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('vpn', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('virtual private network', @equivalentId)

SET @equivalentId = 'ed8d3b6a-d042-47b7-af45-2c37bb2ae0f3'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('VSD', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Variable Speed Drives', @equivalentId)

SET @equivalentId = 'fee5e82e-d103-4c36-aae3-6a4acf3dce98'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('wa', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('western australia', @equivalentId)

SET @equivalentId = 'fec656f4-df1a-4f8b-b71f-ba29d52ba01f'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('wan', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('wide area network', @equivalentId)

SET @equivalentId = '8282c5d0-67b9-402b-93c4-11d55c9d48b9'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('wk', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('week', @equivalentId)

SET @equivalentId = '53078e89-0dd9-4e70-96c8-db4ec1d93c81'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Word', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Synonyms', @equivalentId)

SET @equivalentId = '6a8897a0-6047-4235-8ef8-d24af7801789'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('xray', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('x-ray', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('x ray', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('mri', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('medical resonance imaging', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('medical imaging', @equivalentId)

SET @equivalentId = '47595337-7476-4f13-a0b7-55941641fd19'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('blt', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('below the line', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('belowtheline', @equivalentId)

SET @equivalentId = 'adfbd137-ad5e-417e-ac5f-5eb15f70903f'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('pe', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('physical education', @equivalentId)

SET @equivalentId = '86268260-8735-4a3f-8b33-e1939c4823ef'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('program', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('programme', @equivalentId)

SET @equivalentId = '6545f1f6-f2c2-4646-85ce-b62dd94b07c0'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('expert', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('guru', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('specialist', @equivalentId)

SET @equivalentId = 'fdd77400-f428-4db2-94c4-01244480ae50'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('butcher', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('butchery', @equivalentId)

SET @equivalentId = '4f37adbc-a8b9-49a3-b024-7233b465ae4a'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('hvac', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('heating ventilation air conditioning', @equivalentId)

SET @equivalentId = '03500e57-81a3-4c37-9058-412c99b13fb2'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('air con', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('air conditioning', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('aircon', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('airconditioning', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('air conditioner', @equivalentId)

SET @equivalentId = 'b82ff8a1-ebcd-4cfe-a2f4-22fb2e779c1e'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('exec', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('executive', @equivalentId)

SET @equivalentId = '8beae03a-d152-4c72-b2bd-6d839318e648'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('apn', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('australian provincial news', @equivalentId)

SET @equivalentId = '54b98406-a83c-43d6-9dac-3ad3d7f8d163'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('student', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('undergrad', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('under grad', @equivalentId)

SET @equivalentId = 'bf1f8410-361e-46d3-a421-b8c462921bac'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('grad', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('graduate', @equivalentId)

SET @equivalentId = '9d473276-6014-4374-9f82-b159a773ae87'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('aqtf', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('australian quality and training frameworks', @equivalentId)

SET @equivalentId = 'a6f817d0-aab1-4f4b-b686-95e28f458536'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('rto', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('registered training organisation', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('registered trainer organisation', @equivalentId)

SET @equivalentId = '03055ad7-c0aa-4375-84f3-95c65d64bb49'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('organisation', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('organization', @equivalentId)

SET @equivalentId = 'd1cc20cf-8f11-4648-a9c9-753cbb91ce42'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('reg', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('regd', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('registered', @equivalentId)

SET @equivalentId = '475bcecb-4c99-48e4-a6e6-5bf4958f1cda'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('nlp', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('neuro linguistic programming', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('neurolinguistic programming', @equivalentId)

SET @equivalentId = '3769da57-a71c-4bda-b54c-5beacea3c392'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('bcg', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('boston consulting group', @equivalentId)

SET @equivalentId = '3fd0857c-acd0-48e3-8967-cf9e9e8b5332'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('mobil', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('exon mobil', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('exonmobil', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('exxon', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('exxonmobil', @equivalentId)

SET @equivalentId = 'd085deb6-96bb-46fc-bc6f-9bed6a1af8af'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('bp', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('brittish petroleum', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('british petroleum', @equivalentId)

SET @equivalentId = 'a7658040-91fc-449a-87fc-765108a62220'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('aicd', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('austrlalian institute of company directors', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('maicd', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('faicd', @equivalentId)

SET @equivalentId = '5aa224a8-6d0b-4d9e-bb47-33e59e374b98'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('circa', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('around', @equivalentId)

SET @equivalentId = '7fa20fb3-6ab6-449b-9d0b-fb87fa1f2b6c'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('afp', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('australian federal police', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('police force', @equivalentId)

SET @equivalentId = 'cedbac29-734e-4e8d-a2a1-eff3e669f1ae'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('police officer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('policeofficer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('policeman', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('police man', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('cop', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('coppa', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('policewoman', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('police woman', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('copper', @equivalentId)

SET @equivalentId = 'ed18d5d7-f614-40a9-aa3f-443270f3bb80'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('brittish', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('british', @equivalentId)

SET @equivalentId = '00c2adc5-92a8-4720-8947-86a9c15bb018'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('asic', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('australian securities and investment commission', @equivalentId)

SET @equivalentId = 'bff58603-73e5-476c-b3b6-2de574938e1a'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('jv', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('j v', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('joint venture', @equivalentId)

SET @equivalentId = '2b042dbd-9926-4799-a971-8a7e26c43db1'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('uk', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('united kingdom', @equivalentId)

SET @equivalentId = '7de00fb2-1151-4196-b41e-1b3746057ba1'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('tig', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('tungsten inert gas', @equivalentId)

SET @equivalentId = 'd3565954-e698-47d0-84b8-3770a7f8f182'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('mig', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('metal inert gas', @equivalentId)

SET @equivalentId = '2239a0d8-ec41-4d7c-8945-6b6b69a83afc'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('let', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('long term evolution', @equivalentId)

SET @equivalentId = '2f58628f-7b17-46f4-847c-5f5e262ea2d8'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('pro', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('professional', @equivalentId)

SET @equivalentId = 'f73bf08c-45a7-4b0c-b7a4-0d583f58b0da'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('satiam', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('satyam', @equivalentId)

SET @equivalentId = '969bad42-85be-4c08-b711-03d85bd88382'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('mother', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('maternity', @equivalentId)

SET @equivalentId = '5595d188-5e98-447d-a8e9-e4d94cbf38b9'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('father', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('paternity', @equivalentId)

SET @equivalentId = '106f570f-8cb4-4d3d-a9cf-3dd4107e2e31'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('csl', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Commonwealth Serum Laboratories', @equivalentId)

SET @equivalentId = 'fb6eb32a-02e7-4fba-8c63-7d924a760787'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('eds', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('electronic data systems', @equivalentId)

SET @equivalentId = '52b1ec1f-5856-4e24-b8c6-c7bbb05a699f'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('procter and gamble', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('p&g', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('proctor & gamble', @equivalentId)

SET @equivalentId = 'f5b9b988-9ef8-4836-a9e9-8573120647ac'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('alcatel', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('alcatel lucent', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('lucent', @equivalentId)

SET @equivalentId = '50d3fdec-e4c9-44a3-b39c-2e9d57344fd4'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('m&a', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('mergers and acquisitions', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('mergers & acquisitions', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('mergers&acquisitions', @equivalentId)

SET @equivalentId = '3e4d8e6a-ffde-460b-a5a5-5b375fec4672'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('arab', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('arabic', @equivalentId)

SET @equivalentId = '73223bb3-d74c-4ebf-9086-70f8142e7f9f'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('interior design', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('interior designer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('interior decorator', @equivalentId)

SET @equivalentId = '6544a8d6-4671-4395-82ca-09e03a250e5b'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('reporter', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('journalist', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('journalism', @equivalentId)

SET @equivalentId = 'dd18946d-5971-4af1-b2e9-f0d991ec9dd9'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('actuary', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('actuarial', @equivalentId)

SET @equivalentId = '8d3d177f-9ef1-4c66-8fbe-59777d5d7bbc'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('tresury', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('treasury', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('treasurer', @equivalentId)

SET @equivalentId = '90066b3e-8540-471b-a042-7d1c713a1063'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('apprentice', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('apprenticeship', @equivalentId)

SET @equivalentId = 'ab4b4fce-ede9-4825-af38-376c848813fe'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('waste water', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('wastewater', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('storm water', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('stormwater', @equivalentId)

SET @equivalentId = '7836c259-9332-4694-8c7b-2a7c221885e9'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('town', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('urban', @equivalentId)

SET @equivalentId = '495d53d4-5bab-43b9-81c0-01b178ee7f71'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('hairdressor', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('hair stylist', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('hairdresser', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('hair and makeup', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('hair dresser', @equivalentId)

SET @equivalentId = '2b1f1f81-38b7-4ff0-ba83-d9ecbf902cc3'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('footware', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('foot ware', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('footwear', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('foot wear', @equivalentId)

SET @equivalentId = '63aa56b2-033f-4759-ac3d-6608375a62d9'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('make up', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('makeup', @equivalentId)

SET @equivalentId = 'a63aafe2-3eb1-47f7-b906-5aa18a9b3822'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('uae', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('united arab emirates', @equivalentId)

SET @equivalentId = '538ba877-3a48-489c-b4f8-e9e376b82b71'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('geo technical', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('geotechnical', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('geo tech', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('geotech', @equivalentId)

SET @equivalentId = 'd0c33e76-71d6-41e9-95e3-bfe636724d54'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('lead', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('head', @equivalentId)

SET @equivalentId = 'a1546fcc-ee90-45fb-b218-cacff2c5946e'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('rail', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('railway', @equivalentId)

SET @equivalentId = '1830b12b-ec73-4218-a604-83adbcf59f40'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('nurse', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('nursing officer', @equivalentId)

SET @equivalentId = '5aa23796-88d7-4251-ada1-99c4dbe659e5'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('defence', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('defense', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('armed forces', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('defence personnel', @equivalentId)

SET @equivalentId = 'aefaf8b4-8eae-440c-bc9e-23394f1cb23c'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('air force', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('airforce', @equivalentId)

SET @equivalentId = '01650301-205a-47f4-95ad-fe2c6133c68f'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('aeroport', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('aero port', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('airport', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('air port', @equivalentId)

SET @equivalentId = 'bc0d43e1-fede-4ff7-97b0-d27ca3e02fba'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('ammunition', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('munition', @equivalentId)

SET @equivalentId = 'f10cdc69-a788-4861-9988-61d5f4719d09'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('cbms', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('central budget management system', @equivalentId)

SET @equivalentId = '906327a0-00c0-47c5-8a03-fafb0a7d911b'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('ict', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('information communication and technology', @equivalentId)

SET @equivalentId = '2742cfe4-ff52-4607-a4bf-b7721514a90d'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('icu', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('intensive care unit', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('intensive care', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('intensivecare', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('msicu', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('critical care medicine', @equivalentId)

SET @equivalentId = 'a38e6df2-39d6-40f0-99c4-e76eb3da71ce'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('picu', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('paedeatric intesive care', @equivalentId)

SET @equivalentId = 'c9b5eed2-3707-4e61-b1ee-cc983704168b'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('nicu', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('neonatal intesive care', @equivalentId)

SET @equivalentId = '77cf7690-1c28-4c68-8b07-321a98f09e47'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('safety officer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('safety inspector', @equivalentId)

SET @equivalentId = '75c0250e-e143-49f1-8703-ec28745b52c7'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('japan', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('japanese', @equivalentId)

SET @equivalentId = 'f9192e91-49f6-43dd-b078-166d1dece63b'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('china', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('chinese', @equivalentId)

SET @equivalentId = '2e54ee5d-7906-4cc5-ad25-e5dd62854f93'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('france', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('french', @equivalentId)

SET @equivalentId = '54b0a891-9ab9-414c-80b2-35e3cecc307f'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('germany', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('german', @equivalentId)

SET @equivalentId = 'b843247f-addc-4de6-a818-b6befc75ea20'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('ir', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('industrial relations', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('workplace relations', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('work place relations', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('employee relations', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('er', @equivalentId)

SET @equivalentId = '97a96bed-764c-4880-9eb2-a07a23572578'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('workplace', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('work place', @equivalentId)

SET @equivalentId = 'e769f613-4422-40ef-ac7c-eaede92699e5'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('job network', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('jobnetwork', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('job placement', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('jpo', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('centrelink', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('centre link', @equivalentId)

SET @equivalentId = 'ad9cdd5b-dcc8-4ba8-bb13-7b0f35f1e562'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('rpo', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('recruitment process outsourcing', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('onsite recruitment', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('hro', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('hr process outsourcing', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('on site', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('onsite', @equivalentId)

SET @equivalentId = '3b2625d2-23cb-4c43-b75f-2e50a8235859'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('od', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('organisation development', @equivalentId)

SET @equivalentId = '2d979822-1160-45c2-8fe4-158a209b81b6'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('cqi', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('continuous quality improvement', @equivalentId)

SET @equivalentId = 'de53a968-9268-4560-8444-d5dec1637b66'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('webmethods', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('web methods', @equivalentId)

SET @equivalentId = 'e09ea6f7-f3cc-4cfa-b023-4a5081df1811'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('gis', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('geographic information system', @equivalentId)

SET @equivalentId = 'cf142917-10c7-400c-b611-ed8dfa74cd01'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('hyper text', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('hypertext', @equivalentId)

SET @equivalentId = 'a6152960-e9b6-4c70-a4d3-63efaaf75715'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('3d', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('3 dimensional', @equivalentId)

SET @equivalentId = 'eb69377c-d280-4c7d-baa3-90ff76eb879b'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('mainframe', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('main frame', @equivalentId)

SET @equivalentId = 'ace818b3-8a9a-4152-aa51-7e9b4db606dd'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('scrum', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('agile development', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('agile software development', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('agile methodology', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('agile software methodology', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('agile software development methodology', @equivalentId)

SET @equivalentId = '75099b5b-3463-4d02-9ba1-f4a6712eca3a'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('biztalk', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('biz talk', @equivalentId)

SET @equivalentId = '895530da-62fc-4ebf-8429-9166a4994115'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('database', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('data base', @equivalentId)

SET @equivalentId = '5257557a-649e-4a28-861a-33406e5ea3fe'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('c#', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('c sharp', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('csharp', @equivalentId)

SET @equivalentId = 'd9712c6d-ef0d-411f-a54b-a308d9b8c65c'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('.com', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('dot com', @equivalentId)

SET @equivalentId = '9c6b777c-1428-429e-b01d-4422a041a1a8'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('.net', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('dot net', @equivalentId)

SET @equivalentId = '92cc2a3e-96a7-4956-a586-c60f87215517'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('gui', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('graphic user interface', @equivalentId)

SET @equivalentId = '456e2691-f985-479e-ba94-89cd2d8c6461'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('power builder', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('powerbuilder', @equivalentId)

SET @equivalentId = '3910ab6d-1fda-4698-988d-4ccd301c3769'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('cti', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('computer technology integration', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('computerised technology integration', @equivalentId)

SET @equivalentId = 'bc3ae177-c307-4ba1-ab24-a6e7e2eaf4cd'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('soa', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('service oriented architecture', @equivalentId)

SET @equivalentId = '1a3125c7-9ef4-49d7-bc03-c2a97bcd9c20'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('iso', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('international standards association', @equivalentId)

SET @equivalentId = '6f92e66f-b8c7-477c-b227-2c050aa7e2ba'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('rem', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('remuneration', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('salary', @equivalentId)

SET @equivalentId = '156bc240-e664-49de-807b-dba94416e4e1'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('smsf', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('self manager super fund', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('self manager superannuation fund', @equivalentId)

SET @equivalentId = '0371c4f7-f58d-4889-b9a4-8af2560695e2'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('worksafe', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('work safe', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('work cover', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('workcover', @equivalentId)

SET @equivalentId = '8b1514fd-77a7-4650-b2e8-e581d6a8e05e'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('j2ee', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('j2e', @equivalentId)

SET @equivalentId = '083dddc0-960f-4a61-9171-135c5dd4361a'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Java/J2ee', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Java & J2ee', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Java J2ee', @equivalentId)

SET @equivalentId = 'a40e2342-077a-4f9a-b8aa-5bb75365ae40'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('sql', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('structured query language', @equivalentId)

SET @equivalentId = 'b7753bbd-04a6-4527-a619-f1f3725fe210'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('javascript', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('java script', @equivalentId)

SET @equivalentId = 'a1835173-e75e-46b1-a1ba-997dae845092'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('4gl', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('4th general language', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('forth general language', @equivalentId)

SET @equivalentId = 'f3a5fd8b-d1e2-42bd-b501-059f69e0f2a5'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('sqlserver', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('sql server', @equivalentId)

SET @equivalentId = '32a13385-5630-4994-b813-0fc328107a1f'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('datawarehouse', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('data warehouse', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('DW', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('DWH', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('data storage', @equivalentId)

SET @equivalentId = '503b2125-3d52-4ba4-b950-9452f31e91e6'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('gl', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('general ledger', @equivalentId)

SET @equivalentId = '7eb5ab56-9a5c-458f-843e-167150200581'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('ldap', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('lightweight directory access protocol', @equivalentId)

SET @equivalentId = 'c8caf230-5f52-44d0-8312-cec66b5275df'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('inhouse', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('in house', @equivalentId)

SET @equivalentId = '3ef41e4d-e826-4ec7-ae0c-b5ce57b2e36e'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('cnc', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('computer numeric conrol', @equivalentId)

SET @equivalentId = '4bde76e0-2486-4cb6-bc14-dceeda6523a8'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('toolmaker', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('tool maker', @equivalentId)

SET @equivalentId = 'e9e053ff-cbb6-45d5-9329-0c08e4b9ec11'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('datacentre', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('data centre', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('data center', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('datacenter', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('hosting center', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('hosting centre', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('data facility', @equivalentId)

SET @equivalentId = 'acf4aeec-bf1b-4164-a791-6eebb5e85a63'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('mysql', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('my sql', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('m y sql', @equivalentId)

SET @equivalentId = '858ab152-2ad5-44ec-9a06-efa8bc82cea7'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('uml', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Unified Modeling Language', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('unified modelling language', @equivalentId)

SET @equivalentId = '02aa58a4-97c9-4d32-90b8-a3c2c0bc88cf'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('use case', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('usecase', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('user case', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('usercase', @equivalentId)

SET @equivalentId = 'f2bd733b-3192-46a5-8237-369302121d5d'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('as400', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('as 400', @equivalentId)

SET @equivalentId = '6719d940-301b-4a12-bf08-d2d658955245'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('ccnp', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('cisco certified network professional', @equivalentId)

SET @equivalentId = 'e0efa170-147e-4600-a8a8-270e250f1d89'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('ccna', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('cisco certified network associate', @equivalentId)

SET @equivalentId = '41b23879-f4be-4d61-9598-74004e136a39'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('prince2', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('prince 2', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('princeii', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('prince ii', @equivalentId)

SET @equivalentId = 'bf030510-20be-412c-923f-a30bb2007221'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('msce', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('microsoft certified systems engineer', @equivalentId)

SET @equivalentId = 'e877f7f6-6c16-4547-b827-fb3f9f603727'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('noc', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('netwrok oprations centre', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('network operations center', @equivalentId)

SET @equivalentId = 'd3b461de-082e-4a3d-8653-3e2f207af78c'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('san', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('storage area network', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('storage array', @equivalentId)

SET @equivalentId = '2849c51b-092f-4e18-8f70-651ca676fbda'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('pstn', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('public switched telephone network', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('public switched telephony network', @equivalentId)

SET @equivalentId = 'b3234566-4135-41b2-8675-956946b8af8d'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('fr', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('frame relay', @equivalentId)

SET @equivalentId = '9d046b3c-f84d-4386-b094-e72b181a4bb6'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('level 1', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('level1', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('1st level', @equivalentId)

SET @equivalentId = '5dbef2f4-22b8-444b-a044-35f85f2965b8'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('level 2', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('level2', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('2nd level', @equivalentId)

SET @equivalentId = 'e60b0752-3457-4a0c-a194-8daa81371c83'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('level 3', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('level3', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('3rd level', @equivalentId)

SET @equivalentId = 'd0aa9455-e174-4769-8caa-0e7b93bd91ea'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('desktop', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('desk top', @equivalentId)

SET @equivalentId = 'b1a542a4-3097-4222-8698-64ec8e8b2c74'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('pc', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('personal computer', @equivalentId)

SET @equivalentId = 'e2d6a862-46fb-4701-9f2e-e59856779980'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('blackbelt', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('black belt', @equivalentId)

SET @equivalentId = 'f250ecbc-56e8-4ad3-9094-813cb8134405'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('checkpoint', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('check point', @equivalentId)

SET @equivalentId = '34653dd0-d064-464f-b57f-7832a84ec9ab'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('firewall', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('fire wall', @equivalentId)

SET @equivalentId = '1f5964ec-cc22-4358-a844-d3de2278fac9'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('pki', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('public key infrastructure', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('online security', @equivalentId)

SET @equivalentId = '6984faba-ac51-4c7a-93a9-11dbfcbd5179'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('ipsec', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('ip sec', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('ip security', @equivalentId)

SET @equivalentId = 'e7682f32-caa8-49de-8ba3-cc035a2d05df'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('coax', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('coaxial', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('co ax', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('co axial', @equivalentId)

SET @equivalentId = '0da0ff32-361a-43c6-ac80-6839aed9fd61'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('fibre', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('fiber', @equivalentId)

SET @equivalentId = 'e4b711ee-b632-4027-9fea-3b354b0fbe87'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('itil', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('information technology infrastructure library', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('it infrastructure library', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('itil methodology', @equivalentId)

SET @equivalentId = 'fad91ed4-1c97-41e4-aeec-3588ee0c746d'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('pipeline', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('pipe line', @equivalentId)

SET @equivalentId = '5164b2a0-cf58-4737-af8a-ff359bc5e2e1'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('realestate', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('real estate', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('property', @equivalentId)

SET @equivalentId = '5a078e89-3031-4aa7-a898-9e75cddf2dea'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('facility', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('facilities', @equivalentId)

SET @equivalentId = '14794b8d-8a43-4eba-96c8-3bb815e2167d'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('stocktake', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('stock take', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('stock audit', @equivalentId)

SET @equivalentId = '192f9ac7-6554-4f1c-8cb2-73d126dce30d'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('mobile phone', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('mobile telephony', @equivalentId)

SET @equivalentId = 'a5212f44-2121-4cb8-92bf-b97bdcfdb49d'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('hv', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('high voltage', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('highvoltage', @equivalentId)

SET @equivalentId = '49b7efa9-6bc3-4086-a423-7de3ee6e3d8d'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('supplychain', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('supply chain', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('logistics', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('supply line', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('supplyline', @equivalentId)

SET @equivalentId = '7d4435cf-c216-4b75-9449-319021219695'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('marine', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('maritime', @equivalentId)

SET @equivalentId = 'e8d54d92-a3a9-4d06-86ff-16f9f43129ad'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('div1', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('division 1', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('div 1', @equivalentId)

SET @equivalentId = '9ed381d5-f94d-4ec4-935f-2b2a2adcc408'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('div2', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('division 2', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('div 2', @equivalentId)

SET @equivalentId = 'dbeb5e65-ffce-4464-8885-8d7225a494c0'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('div3', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('division 3', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('div 3', @equivalentId)

SET @equivalentId = '80c4c5db-f996-4e52-8f53-687e919d15bb'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('b2b', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('b 2 b', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('business to business', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('business 2 business', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('business2busines', @equivalentId)

SET @equivalentId = 'dac00477-40ef-4e68-9689-cf9f7fcc222c'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('b2c', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('b 2 c', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('business to consumer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('business 2 consumer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('business2consumer', @equivalentId)

SET @equivalentId = '8f25eb94-0f3c-431b-a664-a52dbafc9c5c'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('hk', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('h k', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('hongkong', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('hong kong', @equivalentId)

SET @equivalentId = '149aaf5c-9eeb-4446-9be8-df2f5825801b'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('png', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('papua new guinea', @equivalentId)

SET @equivalentId = 'acbffc8c-3e83-43ca-ae49-d8b32f2df9ce'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('heathfood', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('health food', @equivalentId)

SET @equivalentId = 'ef14bac3-6e89-460d-a40c-f87d0671c776'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('hifi', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('hi fi', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('high fidelity', @equivalentId)

SET @equivalentId = '1218c1b5-3cd9-4a6f-884b-6bd91bf2130e'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('pga', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('professional golfers association', @equivalentId)

SET @equivalentId = '6851c798-ca91-4e7f-acd1-c4a8fba312d9'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('MYOB', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Mind your own business', @equivalentId)

SET @equivalentId = 'f5bf1672-6cd0-47bc-a4b9-6bf76d871e77'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('OTE', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('O.T.E.', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('On target earnings', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('On target earning', @equivalentId)

SET @equivalentId = '089867c2-d5f9-4f32-83b0-35c70434e69f'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('service delivery', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('SD', @equivalentId)

SET @equivalentId = 'a0364e05-ea02-4319-a8c9-8641da14f62a'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('xml', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('eXtensible Markup Language', @equivalentId)

SET @equivalentId = '18468e1b-68b5-4f25-876a-312d113f00ae'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('html', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('hypertext markup language', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('hypertext mark up language', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('h t m l', @equivalentId)

SET @equivalentId = '5fe1c491-4751-4b5e-81aa-6a94ce72b2a8'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('tac', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('transport accident commission', @equivalentId)

SET @equivalentId = '5ddb5936-3034-405a-ab57-9d74b74c798f'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('pwc', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('price waterhouse coopers', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('pricewaterhouse coopers', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('price water house coopers', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('coopers and lybrand', @equivalentId)

SET @equivalentId = '871f0823-6d3e-48e0-aa78-d8a75d707b72'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('deloitte', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('deloitte touche', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('deloitte touche tohmatsu', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Duesburys', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('deloittetouche', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('deloittetouchetohmatsu', @equivalentId)

SET @equivalentId = 'fe85cd2f-93d2-42a3-b193-438a00ce111b'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('ey', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('e&y', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('ernst and young', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('ernst & young', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('ernst&young', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('ernst & whinney', @equivalentId)

SET @equivalentId = 'c70973fb-0990-44af-bdc6-7f5ceff73f48'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('kpmg', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('hungerfords', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('peat marwick', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('peatmarwick', @equivalentId)

SET @equivalentId = 'c0aea777-aaac-4099-8445-b03a9e507a9a'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('australia post', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('auspost', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('australiapost', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('austpost', @equivalentId)

SET @equivalentId = '1de2de6a-6677-4517-befa-56786e5dbe03'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('gsk', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('glaxo smith kline', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('glaxosmithkline', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('glaxo', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('glaxo smithkline', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('glaxo & smithkline', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('glaxo and smithkline', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('glaxo wellcome', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('glaxowellcome', @equivalentId)

SET @equivalentId = '64453ce3-5784-476c-938c-d327af2d744b'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('astrazeneca', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('astra zeneca', @equivalentId)

SET @equivalentId = 'f302e586-33bc-48da-abf4-f542aabf9313'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('julia ross', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('juliaross', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('ross human directions', @equivalentId)

SET @equivalentId = '5ac337dc-ff98-4ec0-9358-b6b5db58e6a6'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('ray white', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('raywhite', @equivalentId)

SET @equivalentId = '28fc749d-c24f-4ad2-b41a-f138aecfe019'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('hocking stuart', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('hockingstuart', @equivalentId)

SET @equivalentId = '8fd8460d-bf64-4a88-88d0-eb08cf3cea58'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('barry plant', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('barryplant', @equivalentId)

SET @equivalentId = '10e5662b-1165-4c90-8cd6-cc7ddea9ee87'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('tradelink', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('trade link', @equivalentId)

SET @equivalentId = '979659e1-91ad-478a-847d-c824eae38125'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('lockwood', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('lock wood', @equivalentId)

SET @equivalentId = 'edf6f057-9f8a-417d-8270-22eb063726c6'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('peoplesoft', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('people soft', @equivalentId)

SET @equivalentId = 'bbf47ede-431b-408f-8843-7fbc75e788bb'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('primus', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('iprimus', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('primus telecom', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('primus telecommunications', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('primustelecom', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('primustel', @equivalentId)

SET @equivalentId = '906ab441-4d0a-49aa-96ce-471d5969780c'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('optus', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('singtel', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('singteloptus', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('singtel optus', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('singapore telecom', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('singapore telecommunications', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('sing tel', @equivalentId)

SET @equivalentId = 'e5581a9e-c5b1-48dd-bdfe-5ccba726c03b'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('telstra', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('telstra corp', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('telstra ltd', @equivalentId)

SET @equivalentId = '4be80a08-b2f4-4e67-92e5-0ac7d3d20bfb'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('melbourne uni', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('melbourneuni', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('melbourne university', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('university of melbourne', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('universityofmelbourne', @equivalentId)

SET @equivalentId = '2649a7e0-2c26-47d4-bef6-232a0d04e75e'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('sydney uni', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('sydneyuni', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('sydney university', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('university of sydney', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('universityofsydney', @equivalentId)

SET @equivalentId = 'b3eb5344-0272-49f7-81f8-2baaa048df52'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('uni of queensland', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('university of queensland', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('queensland uni', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('qld uni', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('qld university', @equivalentId)

SET @equivalentId = '50224bd4-47d2-443b-89df-e98a541744f8'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('acu', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('australian catholic university', @equivalentId)

SET @equivalentId = '04585f16-e06c-41b5-b440-0e67c0d0bae4'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('bond uni', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('bond university', @equivalentId)

SET @equivalentId = '9ab09155-3250-487e-80db-c0a8d10ed5ca'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('cdu', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('charles darwin university', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('charles darwin uni', @equivalentId)

SET @equivalentId = '9a033f26-3493-430b-a35e-ecfe035f293d'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('csu', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('charles sturt university', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('charles sturt un', @equivalentId)

SET @equivalentId = '2135a79b-989e-44cc-a501-dd0d44c14b01'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('ecu', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('edith cowan university', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('edith cowan uni', @equivalentId)

SET @equivalentId = '1f49d6e0-c392-4c77-a583-5faa94dc02f6'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('jcu', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('james cook university', @equivalentId)

SET @equivalentId = '9e2b6f52-4e25-4d5a-93ed-03d13b6d5312'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('latrobe', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('la trobe', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('latrobe university', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('latrobe uni', @equivalentId)

SET @equivalentId = '61f90d29-7642-429d-9fb9-9402973a5c6c'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('scu', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('southern cross university', @equivalentId)

SET @equivalentId = '9a9813c8-ae9f-4890-9854-99640ca6774d'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('swinburn uni', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('swinburn university', @equivalentId)

SET @equivalentId = '80b09cc8-76a9-4aec-b0b8-df052385ad6d'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('university of ballarat', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('ballarat uni', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('ballarat university', @equivalentId)

SET @equivalentId = 'fb90c2b6-ef6d-4d78-858f-97a5bcfc78fa'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('university of canberra', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('canberra uni', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('canberra university', @equivalentId)

SET @equivalentId = '09410856-7ffd-4616-800d-8ab7c09d8bdd'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('une', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('university of new england', @equivalentId)

SET @equivalentId = 'b623eeef-4b24-40ba-8a40-56eb5e6191f8'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('university of newcastle', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('newcastle uni', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('newcastle university', @equivalentId)

SET @equivalentId = 'cffb4078-b61d-41f0-a508-799362a2aafb'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('unda', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('university of notre dame australia', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('university of notredame australia', @equivalentId)

SET @equivalentId = '1bce751e-f32a-4d09-96c0-17a9257b5127'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('unisa', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('university of south australia', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('uni sa', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('uni of south australia', @equivalentId)

SET @equivalentId = '234ee872-29eb-4ec3-8956-db7e82e90609'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('usq', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('university of southern queensland', @equivalentId)

SET @equivalentId = 'a11b7ba6-4095-480e-a99e-925de2462131'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('university of tasmania', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('uni of tasmania', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('utas', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('u tas', @equivalentId)

SET @equivalentId = '0b1db16f-9d80-4744-87d0-3bb99287d47f'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('usc', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('university of the sunshine coast', @equivalentId)

SET @equivalentId = '42915cd1-7307-4c35-8453-e82adf60e467'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('uow', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('university of wollongong', @equivalentId)

SET @equivalentId = '2fe5e641-79af-4e7c-b8ef-8a45fa46aa67'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('vu', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('victoria university', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('victoria uni', @equivalentId)

SET @equivalentId = '1021e35d-9da0-44ea-8855-9412e9b9b53b'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('ceo institute', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('ceoinstitute', @equivalentId)

SET @equivalentId = '1a3d27b8-6961-4186-84fc-5cc1b4ced6a5'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('ceo circle', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('ceocircle', @equivalentId)

SET @equivalentId = '672f0405-3339-483f-8fbf-228df616d0ff'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('goldcoast', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('gold coast', @equivalentId)

SET @equivalentId = '7d98f21b-8af5-474d-a65d-1b8ac5c8d51b'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('sunshinecoast', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('sunshine coast', @equivalentId)

SET @equivalentId = '00324978-35cc-448e-b049-67badfcc94cf'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('tassie', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('tasmania', @equivalentId)

SET @equivalentId = '3f1107a2-598e-4253-8099-62254a8a944f'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('mbs', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('melbourne business school', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('m b s', @equivalentId)

SET @equivalentId = '22dfae1e-a930-4147-a82c-9dc4bcac6083'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('news ltd', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('newsltd', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('news limited', @equivalentId)

SET @equivalentId = 'e71cf491-8ee7-4da5-a069-5809fea0bfed'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('qut', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('queensland university of technology', @equivalentId)

SET @equivalentId = 'cc782a62-31da-436a-aee8-ddfbccc26cf8'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('rmit', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('royal melbourne institute of technology', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('rmit university', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('rmit uni', @equivalentId)

SET @equivalentId = '5402007b-01dc-430c-bd87-dcff874d6051'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('inbound', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('in bound', @equivalentId)

SET @equivalentId = 'c7960005-dcec-4ab3-a1e7-4436c1f488a5'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('adelaide university', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('university of adelaide', @equivalentId)

SET @equivalentId = '6b81ec35-4696-4682-83fe-7d2c1f7ba6ae'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('anu', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('a n u', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('australian national univeristy', @equivalentId)

SET @equivalentId = '69a65671-f101-440c-9d9f-d51a7dedfb6d'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('unsw', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('university of new south wales', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('u nsw', @equivalentId)

SET @equivalentId = '43fec554-5642-4ff3-bb69-fd2a740b086e'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('uws', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('university of western sydney', @equivalentId)

SET @equivalentId = 'e2727114-04c3-4cca-a607-38c0ee76ed83'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('monash uni', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('monash university', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('monashuni', @equivalentId)

SET @equivalentId = 'fb906f7f-df9f-4d43-8810-c37c26f2bbbe'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('WAI', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Willliam Angliss Institute', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Sir Willliam Angliss Institute', @equivalentId)

SET @equivalentId = '539c933c-0ced-4fda-896c-694cb42394bc'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('deakin uni', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('deakin university', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('deakinuni', @equivalentId)

SET @equivalentId = '6d35dc52-9de6-4bcc-bd15-a76bc9247a05'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('college', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('school', @equivalentId)

SET @equivalentId = 'bdfdfe7e-225e-43f3-ad8e-045ef63ece33'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('baccalaureate', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('international baccalaureate', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('ib', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('bac', @equivalentId)

SET @equivalentId = 'eb61e063-3f3a-4b10-9418-c8753ef70c50'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('special education', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('special ed', @equivalentId)

SET @equivalentId = '793d69f0-bf0f-4089-a7ff-e353de0c9b54'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Chisholm', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Chisholm TAFE', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Chisholm Institute', @equivalentId)

SET @equivalentId = 'a05eda0b-d0f1-4d06-b41f-788751fced25'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Holmesglen institute of TAFE', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Holmesglen', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Holmesglen Institute', @equivalentId)

SET @equivalentId = 'b4146138-2a2d-40c8-98df-3ecaacfd1ec8'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('NMIT', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('N.M.I.T.', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Northern melbourne institute of technology', @equivalentId)

SET @equivalentId = 'bac6e3aa-8fc4-40cb-9daa-c74be3ffde38'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Swinburne', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Swinburne institute', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Swinburne TAFE', @equivalentId)

SET @equivalentId = 'e92895b4-f8c8-44e0-8f53-60292437505b'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('vce', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('victorian certificate of education', @equivalentId)

SET @equivalentId = 'eb6fcad7-480e-4600-bf07-cd8a9d052811'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('hsc', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('higher school certificate', @equivalentId)

SET @equivalentId = 'f126708b-6dfb-44b8-83d5-72c654b0e13e'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('kmart', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('k mart', @equivalentId)

SET @equivalentId = 'c6f0b150-f1ac-47c9-bc56-057028ba5f45'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('young and rubicam', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Y&R', @equivalentId)

SET @equivalentId = 'e6a21fec-9258-409d-add0-fd9956a7b396'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('officeworks', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('office works', @equivalentId)

SET @equivalentId = 'c984eec8-54e0-46d2-96bf-eaa98462f05a'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('coles', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('coles group', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('colesmyer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('coles myer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('cml', @equivalentId)

SET @equivalentId = '135dd5c5-f02b-4472-8fd0-196931e85d49'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('wool worths', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('woolworths', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('safeway', @equivalentId)

SET @equivalentId = '372708ed-909f-4873-903e-cbe61005eebb'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('big w', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('bigw', @equivalentId)

SET @equivalentId = '369079f8-803d-45c9-afb0-641367ce983b'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('mc donalds', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('mcdonalds', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('macdonalds', @equivalentId)

SET @equivalentId = '9e196c9e-1c3d-41fa-ab6d-4d108ee098c0'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('spotless', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('spotless catering', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('spotless group', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('spotless services', @equivalentId)

SET @equivalentId = '2d7f1ee7-1b2b-4833-98bf-0e6aa13b3cc9'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('ramsayhealth', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('ramsay health', @equivalentId)

SET @equivalentId = '7ed945c1-ca87-47a7-94fc-59a5be53dcbe'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('worley parsons', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('worleyparsons', @equivalentId)

SET @equivalentId = 'c652c617-d011-4225-be59-550e797adba6'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('racv', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('royal automobile club of victoria', @equivalentId)

SET @equivalentId = 'ec240da1-2dad-4ea7-a496-aca26e3eac95'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('mcc', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('melbourne cricket club', @equivalentId)

SET @equivalentId = '87814f68-6993-4568-8076-e6f524eeeb27'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('mcg', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('melbourne cricket ground', @equivalentId)

SET @equivalentId = '8b8597f3-7cf8-4f29-a70c-f8c1781d2d21'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('scg', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('sydney cricket ground', @equivalentId)

SET @equivalentId = '19f69bdb-a7e8-43cc-88ba-c81e99ceb715'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('cocacola', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('coca cola', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('coke', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('coca cola amatil', @equivalentId)

SET @equivalentId = '8b41c845-c6f1-49a4-956a-36200da96d33'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('blue scope', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('bluescope', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('bluescope steel', @equivalentId)

SET @equivalentId = '859ef0ea-42c4-481a-881f-6014bfd07fd8'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('salvos', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('salvation army', @equivalentId)

SET @equivalentId = 'e5551bef-f722-45a0-b3a7-5d4a8c0f9acd'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('campbell page', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('campbellpage', @equivalentId)

SET @equivalentId = 'a6c08078-5779-4a9e-9566-695c73cbc1a8'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('max employment', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('maxx employment', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('maxemployment', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('maxxemployment', @equivalentId)

SET @equivalentId = '0146bdde-8a9b-45f5-95ad-99c2def3ca98'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('wise employment', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('wiseemployment', @equivalentId)

SET @equivalentId = '061b7634-31b0-48fa-9b4f-c95d21870ab3'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('gm holden', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('beneral motors holden', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('general motors', @equivalentId)

SET @equivalentId = 'a669f1b8-238a-4bc9-83ce-a987db2f2bf5'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('justjeans', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('just jeans', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('just group', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('justgroup', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('pbl ltd', @equivalentId)

SET @equivalentId = 'd3bb8db5-90c2-4592-a6e3-239fbdd37013'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('pbl', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('publishing and broadcasting', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('publishing & broadcasting', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('publishing&broadcasting', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Mac Bank', @equivalentId)

SET @equivalentId = 'fee8225c-a542-48bf-8c21-9a5e650fb2ac'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('macq bank', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('macquarie bank', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('macquarrie bank', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('macbank', @equivalentId)

SET @equivalentId = 'eb8e84b8-9c41-41b5-8b0c-f90c56fbf387'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('virgin blue', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('virginblue', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('virginblue.com.au', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Virgin', @equivalentId)

SET @equivalentId = 'af7a24e6-192c-432a-805e-2ee0c2639f66'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('mitre10', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('mitre 10', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('mitre ten', @equivalentId)

SET @equivalentId = 'c4025325-6d69-4660-90b8-d4ac25da143b'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('starcity', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('star city', @equivalentId)

SET @equivalentId = '84661c37-bb07-485b-ba93-c5f4a9ca01e8'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('dicksmith', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('dick smith', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('DSE', @equivalentId)

SET @equivalentId = '85adc291-d438-40d4-b729-7bae39b10818'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('seek', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('seek.com.au', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('seek.com', @equivalentId)

SET @equivalentId = '1503739d-1ca2-4445-84f1-4f730542d3b4'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('yahoo', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('yahoo7', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('yahoo.com.au', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('yahoo7.com.au', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('yahoo 7', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('yahoo7!', @equivalentId)

SET @equivalentId = '9b5f309a-3845-4c85-b714-512933a7d10e'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('newcrest', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('newcrest mining', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('newmont', @equivalentId)

SET @equivalentId = 'f0855b7a-77f8-4615-ad3f-549e3146171f'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('ge', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('general electric', @equivalentId)

SET @equivalentId = 'a93b6837-965e-4212-9c3a-f4f1bb2160a5'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('talent2', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('talent 2', @equivalentId)

SET @equivalentId = 'cbe14dd7-88eb-49ac-84a8-5288e6700371'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('chandler macleod', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('chandlermacleod', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('cmg', @equivalentId)

SET @equivalentId = 'f6097cd8-9cf4-41ca-afe7-be4896c3eae6'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('lloyd morgan', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('lloydmorgan', @equivalentId)

SET @equivalentId = 'eae0ceac-8cb4-41bc-8cb2-b6fe1eed02f8'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('m&b', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('morgan and banks', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('morgan&banks', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('morgan & banks', @equivalentId)

SET @equivalentId = 'e92960c2-ce82-415d-aba3-e6306a4fd10a'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('dhs', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('department of human services', @equivalentId)

SET @equivalentId = '6740b2a9-5cb6-41ff-bf39-1c5ab2adbbcf'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('DOI', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Dept of Infrastructure', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Department of infrastructure', @equivalentId)

SET @equivalentId = 'e7e24ba5-d197-4906-b954-97809684f614'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('royal australian college of general practitioners', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('fracgp', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('f.r.a.g.p.', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('royal college of general practitioners', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('royal college of gp''s', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('racgp', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('r a c g p', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('f r a c g p', @equivalentId)

SET @equivalentId = 'bab724ff-2707-4faf-a614-609098c73549'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('maanz', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('marketing association of australia and new zealand', @equivalentId)

SET @equivalentId = 'f80c8de7-f60c-4a94-8c04-8e0299ede2e5'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('mia', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('marketing institute of austrlalia', @equivalentId)

SET @equivalentId = '8296e78d-27dd-4142-8d36-279b4eb698fb'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('aempe', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('association of marine and power engineers', @equivalentId)

SET @equivalentId = 'ebd5b4ec-6aac-48d5-b2f8-ad5103e334a0'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('vacc', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('victorian automotive chamber of commerce', @equivalentId)

SET @equivalentId = 'de6ce07a-cda3-4ce0-96ed-dfd7d6d934b6'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('sae', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('society of automotive engineers', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('society of automotive engineers australasia', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('saea', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('sae-a', @equivalentId)

SET @equivalentId = '1562bd1a-f802-4bd3-bc7b-d6b845f6c1c8'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('ahri', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('austrlalian human resources institute', @equivalentId)

SET @equivalentId = 'ae41a08e-b25d-463c-b3eb-d9d6893d7ba3'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('rcsa', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('recruitment and consulting services association', @equivalentId)

SET @equivalentId = '9b49b460-8ecb-445f-a5b2-929dc43a9a18'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('itcra', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('information technology contract and recruitment association', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('information technology contract & recruitment association', @equivalentId)

SET @equivalentId = '5bccb6d7-3ba0-4b47-b71e-9a4d8956be91'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('gta', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('group training australia', @equivalentId)

SET @equivalentId = '6fc35b06-2f4f-400f-a34b-383cb3cfa90e'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('finsia', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('financial services institute of australiasia', @equivalentId)

SET @equivalentId = 'aa6fa3f7-853b-4d71-8381-476c6ee33215'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('rdns', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('royal district nursing service', @equivalentId)

SET @equivalentId = '9aa68c20-d853-4837-b967-4aec48c2dc6e'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('pia', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('planning institute of australia', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('rapi', @equivalentId)

SET @equivalentId = '23701cb7-7d33-4b77-8daa-fc7ec6451fca'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('apa', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('australian physiotherapy association', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('australian physiotherapists association', @equivalentId)

SET @equivalentId = '9a1c98ab-3a1b-4c40-ba48-182e2e0433bc'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('engineers australia', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('institute of engineers', @equivalentId)

SET @equivalentId = '2b78b697-c7ed-4790-b999-8245abd1a161'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('yellowpages', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('yellow pages', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('yellowpages.com.au', @equivalentId)

SET @equivalentId = '3f71e94a-24e0-4ed3-8e36-3b6937549828'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('whitepages', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('white pages', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('whitepages.com.au', @equivalentId)

SET @equivalentId = 'd1b53188-cdbe-4737-ad82-7e5cca4b2988'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('license', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('licence', @equivalentId)

SET @equivalentId = '143296ae-9735-449a-86b3-e3901eb5eafa'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('va', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('voluntary administrator', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('voluntary administration', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('liquidator', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('insolvency', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('liquidation', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('insolvent', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('receiver', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('receiver manager', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('receivership', @equivalentId)

SET @equivalentId = '9b30f6b1-82fc-47cc-bb24-71bba7e324f4'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('asfa', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('association of superannuation funds of australia', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('superannuation association', @equivalentId)

SET @equivalentId = 'cc7c256a-7daf-4300-a996-87ea3366f8e7'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('retailer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('retail company', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('retail organisation', @equivalentId)

SET @equivalentId = '9c3c1391-0cb4-4376-8a1a-85d1949582f6'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('ara', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('australian retailers association', @equivalentId)

SET @equivalentId = '9e7af9ed-f0ff-4120-ab03-70210b16d090'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('bdouble', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('b double', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('road train', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('roadtrain', @equivalentId)

SET @equivalentId = '233b1706-3816-4a55-930b-a8f934ccd0c3'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('carpark', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('car park', @equivalentId)

SET @equivalentId = '39580a64-2111-4447-ab68-41b60b4ad1ea'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('formwork', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('form work', @equivalentId)

SET @equivalentId = 'a0b6893e-4a6b-4a6f-a586-5e2b80a29b63'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('agribusiness', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('agri business', @equivalentId)

SET @equivalentId = 'fc8a89e1-a0ec-4bfa-bc61-c3d550813add'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('freelance', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('free lance', @equivalentId)

SET @equivalentId = 'f859f93e-f898-47d8-888d-fec8de41ead4'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('voip', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('voice over internet protocol', @equivalentId)

SET @equivalentId = 'f907b363-7731-4796-a528-32690dcb3255'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('phd', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('doctorate', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('doctor of philosophy', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('p h d', @equivalentId)

SET @equivalentId = '645c756c-317f-4931-812f-5f4a74862ac6'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('statutory', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('stat', @equivalentId)

SET @equivalentId = '989abdd6-5c90-4f01-89c1-b6ce68c960bd'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('forex', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('foreign exchange', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('fx', @equivalentId)

SET @equivalentId = 'b0028cc0-6f28-4397-9e64-9a14e45e37fe'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('expat', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('ex pat', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('ex patriate', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('expatriate', @equivalentId)

SET @equivalentId = 'bbbab95e-d4e7-407a-bc6b-277226214612'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('six sigma', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('sixsigma', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('6sigma', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('6 sigma', @equivalentId)

SET @equivalentId = '05e6ec54-3b11-42e7-a8e1-3504621dcb57'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('streetwear', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('street wear', @equivalentId)

SET @equivalentId = '8fb0987f-4360-4595-9173-f43067fa7e71'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('menswear', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('mens wear', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('mens clothing', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('mens fashion', @equivalentId)

SET @equivalentId = 'e2f2c2df-852a-4a04-abd8-ffb0db94dae1'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('childrenswear', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('childrens wear', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('childrens clothing', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('childrens fashion', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('kids clothing', @equivalentId)

SET @equivalentId = '104ecf63-7865-4220-867b-f5220aaf06a6'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('womenswear', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('womens wear', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('womens clothing', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('womens fashion', @equivalentId)

SET @equivalentId = '49dcac64-ba7c-48b8-a89c-27dbf7700e61'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('lingerie', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('intimate apparel', @equivalentId)

SET @equivalentId = '2869994d-cdd6-4dd5-9978-edf80786be2c'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('lcms', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('learning content management system', @equivalentId)

SET @equivalentId = 'f91c1b7c-5590-4341-b068-f99393731bd1'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('super market', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('supermarket', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('hyper market', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('hypermarket', @equivalentId)

SET @equivalentId = '9ccc1d56-00a3-41e5-b225-9068a9c278ad'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('alumni', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('alumnus', @equivalentId)

SET @equivalentId = '103b1c54-b539-4707-8086-8cb33f9d9216'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('diecast', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('die cast', @equivalentId)

SET @equivalentId = 'f4830faf-9b9e-46d7-b595-8312ff1207b8'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('4wd', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('4 wheel drive', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('four wheel drive', @equivalentId)

SET @equivalentId = '994089ae-6a75-420e-9137-773c3c7fe2ad'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('corporation', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('corp', @equivalentId)

SET @equivalentId = 'b08d91b2-a951-4b54-ac9b-54ee14249daf'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('bachelor of arts', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('B arts', @equivalentId)

SET @equivalentId = '17db508b-f84b-45d7-9fa2-322fc9976209'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Bureau of meteorology', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('the bureau', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('weather bureau', @equivalentId)

SET @equivalentId = 'f91284ed-b426-4c68-8469-5955f68cbe03'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('CFA', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('C.F.A.', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Country fire authority', @equivalentId)

SET @equivalentId = 'f51d3d41-1321-42bd-917b-c3e31c92834b'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('MFB', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('M.F.B.', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Metropolitan Fire brigade', @equivalentId)

SET @equivalentId = 'f67af25a-11ed-4089-b121-e4e7bad8b3da'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('anf', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('australian nursing federation', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('australian nurses federation', @equivalentId)

SET @equivalentId = '95531589-69e2-473b-84c0-3de3ed4acc61'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Australian Council of Trade Unions', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('ACTU', @equivalentId)

SET @equivalentId = 'afc2b6eb-80b4-44ed-a619-47e5ffe23f1e'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('NSW Teachers Federation', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('NSWTF', @equivalentId)

SET @equivalentId = '61a4d111-46a4-4785-bde0-e435c7dc5fcd'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Construction Forestry Mining and Energy Union', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('CFMEU', @equivalentId)

SET @equivalentId = '16aa6192-bc96-4bbe-a2bc-e90858d4f71e'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Queensland Teachers Union', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('QTU', @equivalentId)

SET @equivalentId = '899be9b2-23b2-4504-9b41-eec68904005f'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Queensland Nurses Union', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('QNU', @equivalentId)

SET @equivalentId = '7b3ac1fc-f275-4458-90f1-703ef878b2d2'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Australian Education Union Victorian Branch', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('AEUVB', @equivalentId)

SET @equivalentId = '958b9620-e5ca-4b78-b405-e64dff815d74'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Australian Nursing Federation Victoria', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('ANFV', @equivalentId)

SET @equivalentId = 'c037be07-ee9b-4f6f-85bb-b4984a76d3c4'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Australian Services Union', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('ASU', @equivalentId)

SET @equivalentId = '368eff45-926a-46f7-94f1-81454b2f10c8'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('NSW Nurses'' Association', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('NSWNA', @equivalentId)

SET @equivalentId = '5bfd115e-0b8c-43ad-a210-e7adcfe109a5'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Transport Workers'' Union of Australia', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('TWUA', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('TWU', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('transport workers union', @equivalentId)

SET @equivalentId = 'd0996c41-9500-433b-ad65-c4926cc50f8f'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Communications Electrical and Plumbing Union of Australia', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('CEPUA', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('CEPU', @equivalentId)

SET @equivalentId = 'c13cdd4a-413e-44f0-98e1-d25c71edffc6'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Electrical Trades Union of Australia', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('ETU', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('ETUA', @equivalentId)

SET @equivalentId = '24ab39c2-01eb-44f1-947f-2d9ece73b54d'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Queensland Independent Education Union', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('QIEU', @equivalentId)

SET @equivalentId = 'ba08b123-37a1-49fa-add0-c8439ab3cc12'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Australian Nursing Federation South Australia', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('ANFSA', @equivalentId)

SET @equivalentId = 'eb3162fb-4228-4061-b073-ad6e193ca1b0'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Australian Education Union South Australian Branch', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('AEUSAB', @equivalentId)

SET @equivalentId = 'f59df511-e463-42b7-94c4-c9eb33455311'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('State School Teachers'' Union of Western Australia', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('SSTUWA', @equivalentId)

SET @equivalentId = '405f34ed-9368-4166-8668-8b728f48477b'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Victorian Independent Education Union', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('VIEU', @equivalentId)

SET @equivalentId = 'c122654e-9292-4e30-b3da-a4d7629a7f31'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Medical Scientists Association of Victoria', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('MSAV', @equivalentId)

SET @equivalentId = '1d6866a9-14b4-4753-adcb-8a1cb4e51f57'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Community and Public Sector Union', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('CPSU', @equivalentId)

SET @equivalentId = '5bb19d98-772a-469b-b87f-a052e768ce56'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Australian Education Union ACT Branch', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('AEUACT', @equivalentId)

SET @equivalentId = 'e1123935-a532-4e7a-933f-2ac535512928'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Australian Education Union Northern Territory Branch', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('AEUNT', @equivalentId)

SET @equivalentId = 'bcfc04ca-acab-4012-909d-d7f7c630b733'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Australian Services Union Victorian Branch', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('ASUV', @equivalentId)

SET @equivalentId = '281214b9-c7a3-4085-8062-8488a65d440c'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Australian Services Union New South Wales Branch', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('ASUNSW', @equivalentId)

SET @equivalentId = 'ba3b12ee-7d8a-4910-b64d-91779d5f0d3a'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Australian Education Union Tasmanian Branch', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('AEUT', @equivalentId)

SET @equivalentId = '9e2e41ac-d6cb-4015-8bb0-5fea0aa74452'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Australian Nursing Federation Western Australia', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('ANFWA', @equivalentId)

SET @equivalentId = 'b11649cf-14b3-4281-a8e7-afb75dd4f03f'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Health and Community Services Union', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('HACSU', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('HCSU', @equivalentId)

SET @equivalentId = 'dc740ecb-9a02-44c6-a69d-81f621066669'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Health Services Union of Australia', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('HSUA', @equivalentId)

SET @equivalentId = '04a2aa2f-9550-4638-9a11-77ec959c9acf'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('NSW/ACT Independent Education Union', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('NSWACTIEU', @equivalentId)

SET @equivalentId = '7b17c3db-6459-4ca7-840b-bb125db0a1a8'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Evatt Foundation', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('EVATT', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('EF', @equivalentId)

SET @equivalentId = '2e40c5ed-e5f3-4c7a-86d9-2b1e5b0b2e4d'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Tasmanian Catholic Education Employees'' Association', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('TCEEA', @equivalentId)

SET @equivalentId = '92729c66-3c44-469c-a4ff-25ab5e026429'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('CEPU New South Wales Communications Division', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('CEPUNSWCD', @equivalentId)

SET @equivalentId = 'febe604c-8c19-4b82-9ac2-9325aeffe60c'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Health and Research Employees Association', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('HREA', @equivalentId)

SET @equivalentId = '4dd2a2e1-b58c-45e1-bb7c-cdcd8a6be8e0'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Association of Non-Government Education Employees', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('ANGEE', @equivalentId)

SET @equivalentId = '0b983a07-a524-4375-a3e1-7697e31749c2'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Australian Services Union Queensland Branch', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('ASUQ', @equivalentId)

SET @equivalentId = '97a9b240-9485-4d7a-811f-b9334f91fcce'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Builders Labourers Federation Queensland', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('BLFQ', @equivalentId)

SET @equivalentId = 'a09299ca-e76a-4e52-b1d0-845785378102'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('NSW Fire Brigade Employees Union', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('FBEU', @equivalentId)

SET @equivalentId = '54645fd8-6ee4-4fe1-8557-1ff8617fb6ca'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Australian Workers Union Queensland', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('AWUQ', @equivalentId)

SET @equivalentId = 'd500c1a0-7628-49ba-9145-e66205b8ddab'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Shop Distributive & Allied Employee''s Union', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('SDAEU', @equivalentId)

SET @equivalentId = '09fc3fcc-4697-423c-8559-989a823ca2f2'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Electrical Trades Union Victorian Branch', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('ETUV', @equivalentId)

SET @equivalentId = 'b1215fed-d057-4a13-89c4-a6eab0becb86'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Labor Council of New South Wales', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('LCNSW', @equivalentId)

SET @equivalentId = '5f54fcfb-cb01-4ca1-a142-c0fe0d190e96'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('United Trades and Labor Council South Australia', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('UTLCSA', @equivalentId)

SET @equivalentId = '6beff491-659d-4135-9bcd-8a7eeecf5fc9'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Finance Sector Union of Australia', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('FSU', @equivalentId)

SET @equivalentId = 'b77ccb62-a087-4ab2-9a72-4dae578e9508'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('ACT Trades and Labour Council', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('ACTTLC', @equivalentId)

SET @equivalentId = '1542d692-f0d3-4936-bf2a-feec0ce849ca'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Liquor Hospitality and Miscellaneous Workers Union', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('LHMU', @equivalentId)

SET @equivalentId = 'a9481aaf-0fd7-4b8a-bb4a-9861a5befa1a'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Australian Workers Union South Australia', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('AWUSA', @equivalentId)

SET @equivalentId = 'e6db6453-0c1a-42e5-88b5-f9dfa1c18b5d'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Municipal and Shire Employees Union of NSW', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('MSEU', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('MSEUNSW', @equivalentId)

SET @equivalentId = '0fa06c38-05a6-4b67-875e-9c08f7f5b7f6'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Victorian Trades Hall Council', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('VTHC', @equivalentId)

SET @equivalentId = 'f00c5506-18a4-4039-950f-627b5a9f3b2e'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Australian Trade Union Archives', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('ATUA', @equivalentId)

SET @equivalentId = '43f39b74-10e6-4a07-89ce-502f09459c40'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('United Firefighters Union of Australia - Queensland Branch', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('UFUA', @equivalentId)

SET @equivalentId = 'b7e42a02-724c-49a4-8b9c-0d64b7f040ed'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Australian Manufacturing Workers'' Union', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('AMWU', @equivalentId)

SET @equivalentId = '39d76fdc-0317-47d6-88eb-a8982c8c34cd'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Australian Services Union ACT Branch', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('ASUACT', @equivalentId)

SET @equivalentId = 'fac6bdd4-ba3d-4517-aa4f-a4bf9f9b4a25'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Trades and Labor Council of Western Australia', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('TLCWA', @equivalentId)

SET @equivalentId = 'f36a0de2-1425-44b5-9fbd-49d0caa20e2b'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Electrical Trades Union New South Wales Branch', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('ELUNSW', @equivalentId)

SET @equivalentId = 'cb9de508-4f2c-405d-b249-be134fa9c574'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Unions Tasmania', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('UT', @equivalentId)

SET @equivalentId = '5de0fef7-f391-439b-aba0-e0fed2e19681'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('National Union of Workers', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('NUW', @equivalentId)

SET @equivalentId = '98aca5ff-e4c2-4cf5-a211-ed89482670e6'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('IT Workers Alliance', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('ITWA', @equivalentId)

SET @equivalentId = 'ae66757f-96a0-4627-a6be-8c9fa380677d'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('State Public Services Federation Tasmania', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('SPSFT', @equivalentId)

SET @equivalentId = '6fe1da84-c5f5-4f77-987d-9b9c03c55545'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Australian Services Union Northern Territory Branch', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('ASUNT', @equivalentId)

SET @equivalentId = '7333a4b3-5986-4873-91d8-5a9e21ae0a33'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Queensland Council of Unions', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('QCU', @equivalentId)

SET @equivalentId = '124e7af4-5efe-4bbf-bd6a-7dcb9fb836c0'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('United Firefighters Union of Western Australia', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('UFUWA', @equivalentId)

SET @equivalentId = '0b46893f-1be9-422e-abf1-b58ccb5b7110'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Australian Services Union Western Australia Branch', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('ASUWA', @equivalentId)

SET @equivalentId = '53409348-0d4b-462f-9ef4-45a0a5968929'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Institute of Chartered Accountants of New Zealand', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('ICANZ', @equivalentId)

SET @equivalentId = 'd53038b1-042d-4a06-9ca6-e06dddc055fd'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Australian Acupuncture and Chinese Medicine Association', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('AACMA', @equivalentId)

SET @equivalentId = 'ddb447ca-bf9d-4528-aa07-b3655904079a'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Australian Agricultural and Resource Economics Society Inc.', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('AARESI', @equivalentId)

SET @equivalentId = '4269f1b2-c9b3-41a5-9c96-47818df205c4'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Australian Institute Of Agricultural Science and Technology', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('AIAST', @equivalentId)

SET @equivalentId = '316f2025-b4de-4af6-90ce-88c15f0eb9ab'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('National Farmers'' Federation', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('NFF', @equivalentId)

SET @equivalentId = 'c62f6695-b17d-4a98-ae3e-f6cc9a5a0a93'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('The Society for Engineering in Agriculture', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('SEA', @equivalentId)

SET @equivalentId = '8832b2b3-7d20-482d-8283-2a346fa2a41b'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Australian & New Zealand College of Anaesthetists', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('ANZCA', @equivalentId)

SET @equivalentId = 'f8446719-fdb3-44fa-8ecb-c04e6b68ba1f'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Australian Society of Anaesthetists', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('ASA', @equivalentId)

SET @equivalentId = '9d1d6592-16a5-4d53-b278-ac7d2d60e8a4'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Royal Australian Institute of Architects', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('RAIA', @equivalentId)

SET @equivalentId = '6d639306-c73b-4d21-bed7-e67cf5ecd772'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Australian Housing and Urban Research Institute', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('AHURI', @equivalentId)

SET @equivalentId = 'ec1b57bf-e2f2-4ffa-b3f8-27468afa7196'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('New Zealand Society of Archivists', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('NZSA', @equivalentId)

SET @equivalentId = '1b7ad608-c112-456b-950f-03d2f85a0428'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Records Management Association of Australia', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('RMAA', @equivalentId)

SET @equivalentId = '8cc41a70-67a7-4a5e-88bf-e0be80416c73'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Australian Association of Audiologists in Private Practice Inc.', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('AAAPP', @equivalentId)

SET @equivalentId = '16fdac96-abbe-40c1-b50f-35077d5718f1'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('New Zealand Audiological Society', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('NZAS', @equivalentId)

SET @equivalentId = '82cec7f3-e72d-45ec-84d9-e0c8a5e17c6c'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Speech Pathology Australia', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('SPA', @equivalentId)

SET @equivalentId = '78bcdb9b-702b-4db2-9fcd-91b0d6787859'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Australian Society for Biochemistry and Molecular Biology', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('ASBMB', @equivalentId)

SET @equivalentId = '5c4a6699-4da3-4862-81c8-a2816b313657'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Australian and New Zealand Society for Comparative Physiology and Biochemistry', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('ANZSCPB', @equivalentId)

SET @equivalentId = '2471f5ac-3d2b-459a-8939-ddddf9d57ff3'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('The Australian Society For Fish Biology', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('ASFB', @equivalentId)

SET @equivalentId = '66a87c9f-ddfe-427d-844a-749aaaa71298'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Centre for Biomedical Engineering', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('CBE', @equivalentId)

SET @equivalentId = 'bde86866-83f1-4d90-a503-673e74e7f3ab'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('The Australian Society for Biophysics', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('ASB', @equivalentId)

SET @equivalentId = 'a412a145-14b6-4290-874f-36db765e9faa'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('New Zealand Biotechnology Association', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('NZBA', @equivalentId)

SET @equivalentId = '4af9ff09-8131-47d1-89c4-d803d39985c5'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Australian Systematic Botany Society Inc.', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('ASBS', @equivalentId)

SET @equivalentId = '22cfa387-6804-44aa-a233-49fde380ffc4'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Cardiac Society of Australia and New Zealand', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('CSANZ', @equivalentId)

SET @equivalentId = 'ad00db33-3517-482c-9df8-d72570973af5'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Royal Australian Chemical Institute', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('RACI', @equivalentId)

SET @equivalentId = '2076342a-9177-406d-987f-2fb4c777aa9f'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('New Zealand Institute of Chemistry', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('NZIC', @equivalentId)

SET @equivalentId = '216e7cc5-896c-4302-829d-2fbd5c72a64a'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Society of Technical Communication', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('STC', @equivalentId)

SET @equivalentId = '7ca376ce-0ec0-4678-bcc4-656cd59351de'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Australasian Society for Computers In Learning In Tertiary Education', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('ASCILITE', @equivalentId)

SET @equivalentId = '2e446d8d-dc05-4c34-baf1-b3e7bcbfc764'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Australian Council for Computers in Education', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('ACCE', @equivalentId)

SET @equivalentId = '18aadd68-e98e-43ae-834f-ef1bcd0e133f'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Technology Education Federation of Australia', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('TEFA', @equivalentId)

SET @equivalentId = '6366c4a1-93b9-4783-97e6-3c9e42f2d4d8'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('The Australian Computer Society', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('ASC', @equivalentId)

SET @equivalentId = 'c4b41881-5747-48de-bb82-bb17b17ab9c1'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('The Australian Pattern Recognition Society', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('APRS', @equivalentId)

SET @equivalentId = '0c139176-eb6b-4d53-b735-4684a8de3ae2'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('System Administrators Guild of Australia', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('SAGA', @equivalentId)

SET @equivalentId = 'c580bda5-f14f-4a6e-ac95-720cbbd9f325'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Society of Crystallogaphers in Australia and New Zealand', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('SCANZ', @equivalentId)

SET @equivalentId = 'a05d9095-ea1d-4b47-8b49-2ea504b388f3'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Australian Society of Forensic Dentistry', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('ASFD', @equivalentId)

SET @equivalentId = '9682106f-8871-4e9f-bd5c-4501d1140d6a'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Australian Dental Association', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('ADA', @equivalentId)

SET @equivalentId = '9c5b17d3-dab6-4117-baac-5b0f9b0134a3'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Australian Dental Association Queensland Branch', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('ADAQ', @equivalentId)

SET @equivalentId = '2a5a794e-1a2f-4f48-8091-d6544312c077'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Dental Board of Queensland', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('DBQ', @equivalentId)

SET @equivalentId = 'd2951350-0a19-4e09-8417-01ec487b6083'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Dental Technicians & Prosthetists Board of Queensland', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('DTPBQ', @equivalentId)

SET @equivalentId = 'acfbc4a9-4898-4964-b56a-10b759202ee9'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('NSW Dental Assistants'' Association', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('NSWDAA', @equivalentId)

SET @equivalentId = '72e21cba-273b-4005-b864-6effea867ce3'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Royal Australasian College of Dental Surgeons', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('RACDS', @equivalentId)

SET @equivalentId = '121cc00f-89c1-45e3-84f8-ffafa68b02ee'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('New Zealand Dental Association', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('NZDA', @equivalentId)

SET @equivalentId = '9eaddf57-e066-4542-a122-9f6e1ddf2a44'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Australasian College of Dermatologists', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('ACD', @equivalentId)

SET @equivalentId = 'ff91d0c8-1331-4ac5-8e3e-599c6a7e79cc'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Design Institute of Australia', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('DIA', @equivalentId)

SET @equivalentId = '8a22c640-9316-497b-96a6-1e61d2a316b1'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Dietitians Association of Australia', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('DAA', @equivalentId)

SET @equivalentId = '12b49db5-f308-4e43-a413-6ca9716c0d58'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Law and Economics Association of New Zealand Incorporated', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('LEANZI', @equivalentId)

SET @equivalentId = '53b0e319-bf27-4516-9cc6-b11e4621db92'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Australian Association for Research in Education', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('AARE', @equivalentId)

SET @equivalentId = 'cf2817df-4451-40d3-ad50-7577d0c7bed1'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Australian Society for Music Education', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('ASME', @equivalentId)

SET @equivalentId = '2e3769ac-6ae3-4e04-9049-7d898243419d'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Australian Society for Educational Technology', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('ASET', @equivalentId)

SET @equivalentId = 'cbb21967-77e2-471d-ba9d-2a0d1d26335f'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Association of Consulting Engineers Australia', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('ACEA', @equivalentId)

SET @equivalentId = 'f6baf212-c9d9-41c5-b2e1-1cf4398d69ab'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Institution of Engineering and Mining Surveyors Australia', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('IEMSA', @equivalentId)

SET @equivalentId = '3ce0864d-2776-4006-8a94-0904f9328215'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Australian Entomological Society', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('AES', @equivalentId)

SET @equivalentId = 'b64e9218-c677-4104-837b-15c74ca94c66'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('The Entomological Society of Victoria', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('ESV', @equivalentId)

SET @equivalentId = '6fe6049c-c1d8-4008-98c5-94cbf24cb39b'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Australian Association for Professional & Applied Ethics', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('AAPAE', @equivalentId)

SET @equivalentId = 'ea54f8ca-5a30-473c-b199-d25c76bc863e'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Victorian Institute of Forensic Medicine', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('VIFM', @equivalentId)

SET @equivalentId = '1748b9dd-c1bc-4735-bf4e-8557d1aa6a36'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Institute of Foresters of Australia', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('IFA', @equivalentId)

SET @equivalentId = '74dfddd8-faaa-42cb-9b5b-5bcc81451870'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Australian Funeral Directors Association', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('AFDA', @equivalentId)

SET @equivalentId = '0048cf43-1f47-4f13-a7cd-348c979f79b6'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('The Institute of Australian Geographers', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('IAG', @equivalentId)

SET @equivalentId = '82db1156-7a13-4585-876d-a84506af09d3'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('The Geological Society of Australia', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('GSA', @equivalentId)

SET @equivalentId = '6e5c92f5-764b-4cdb-83c1-282bd3cd1d62'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Australian Institute of Geoscientists', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('AIG', @equivalentId)

SET @equivalentId = '347f4cea-1cea-4ddf-8c81-b38ac42da7fa'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('The Australian Society of Exploration Geophysicists', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('ASEG', @equivalentId)

SET @equivalentId = '4539e3ae-8a5f-48dd-b33b-86821573833e'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Australian College of Health Service Executives', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('ACHSE', @equivalentId)

SET @equivalentId = '63ec383f-652a-450b-a369-f6f097043e35'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Australian Council of Deans of Education', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('ACDE', @equivalentId)

SET @equivalentId = 'd0252762-6b2a-4e73-b013-642abec161f1'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Association of Heads of Australian University Colleges and Halls Inc.', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('AHAUCH', @equivalentId)

SET @equivalentId = '09272a48-f642-4d19-a44c-097d076aebbb'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Association for Tertiary Education Management Inc.', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('ATEM', @equivalentId)

SET @equivalentId = '5cfc68ed-6523-48bb-baa3-455d7071adab'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Australasian Research Management Society', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('ARMS', @equivalentId)

SET @equivalentId = '5174c328-8408-4909-82b4-ff212bd4fadf'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Australasian Association for Institutional Research', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('AAIR', @equivalentId)

SET @equivalentId = 'bae45ad1-e731-4db4-a568-77525835d2f7'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Council of Australian University Directors of Information Technology', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('CAUDIT', @equivalentId)

SET @equivalentId = 'a825af02-f1c1-4907-bcc7-817e3729fa3d'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Council of Australian University Librarians', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('CAUL', @equivalentId)

SET @equivalentId = '795887ee-b7ff-425f-9a6b-14751e4fec8e'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('National Colloquium of Senior University Women', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('NCSUW', @equivalentId)

SET @equivalentId = '35dce998-7fbd-4386-8f4e-2776c5331871'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Royal Australian Historical Society', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('RAHS', @equivalentId)

SET @equivalentId = 'da00c041-fa8b-420e-96fd-094dc3f8eb7d'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Australian and New Zealand Intensive Care Society', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('ANZICS', @equivalentId)

SET @equivalentId = 'f6ab626f-0538-4ded-923e-e21c7166817c'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Media Entertainment and Arts Alliance', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('MEAA', @equivalentId)

SET @equivalentId = 'e5269698-2bc8-4540-8f79-a2f67e5ce3d3'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('New Zealand Property Institute', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('NZPI', @equivalentId)

SET @equivalentId = '08f7fb73-32f5-42e0-9bf9-32d04ee8964f'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Australian Institute of Judicial Administration', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('AIJA', @equivalentId)

SET @equivalentId = '7f2a9d88-4bbd-405f-bed0-4157c683996c'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Australian and New Zealand Sports Law Association', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('ANZSLA', @equivalentId)

SET @equivalentId = '8845ff80-0608-4deb-802e-1700cf4dfe72'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Law Society Northern Territory', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('LSNT', @equivalentId)

SET @equivalentId = 'd6b8f9f4-fa0e-4e4c-a23f-60e1ffb5c870'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Law Society of New South Wales', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('LSNSW', @equivalentId)

SET @equivalentId = 'bbd64246-53ec-423d-84d4-d8996be28d91'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Law Society of South Australia', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('LSSA', @equivalentId)

SET @equivalentId = 'b599c935-ae4c-4270-b9e1-0f77b1f90b68'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Law Society of Western Australia', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('LSWA', @equivalentId)

SET @equivalentId = 'a632298a-c748-4f8e-8c19-8b749c783b84'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('New South Wales Bar Association', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('NSWBA', @equivalentId)

SET @equivalentId = 'a16ecdfc-5495-4b71-ad51-95d236739825'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('New Zealand Law Society', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('NZLS', @equivalentId)

SET @equivalentId = '48e2936a-5413-4db3-a730-42533385dc30'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Queensland Law Society', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('QLS', @equivalentId)

SET @equivalentId = '740c482f-1782-42bc-a32e-4f5b30f0def7'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Australian Library and Information Association', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('ALIA', @equivalentId)

SET @equivalentId = '4281b6f1-d89b-46d5-8c41-66c8d3dcd9ec'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Council of Australian State Libraries', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('CASL', @equivalentId)

SET @equivalentId = 'c033660a-4a7a-45a6-bf3c-8c981d30ab74'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Institute of Management Consultants', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('IMC', @equivalentId)

SET @equivalentId = '078cac73-87e0-4243-bd11-1258fe1d5872'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Australian Marine Sciences Association', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('AMSA', @equivalentId)

SET @equivalentId = 'bd477086-dfeb-477d-80d8-a549d6187583'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('The Australian Mathematical Society', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('AMS', @equivalentId)

SET @equivalentId = 'fb924b6c-4a03-43d7-a5f0-39a82b3609f9'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Applied Mathematics Division of the Australian Mathematical Society', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('ANZIAM', @equivalentId)

SET @equivalentId = '8f53b0bb-3b52-4a2c-a2b4-d85eccd50330'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('New Zealand Mathematical Society', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('NZMS', @equivalentId)

SET @equivalentId = '7095f735-7e5d-448d-a229-8e107e37d10c'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Australian Institute of Medical Scientists', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('AIMS', @equivalentId)

SET @equivalentId = '06269280-4c2d-4835-ad8d-3d8def4e0dd9'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Australian Society for Medical Research', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('ASMR', @equivalentId)

SET @equivalentId = '328f49a1-3ed2-404e-af6f-1dd6fb6b3dbc'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Australian College of Rural and Remote Medicine', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('ACRRM', @equivalentId)

SET @equivalentId = '983a2660-69b2-41f2-86e3-6bc57f1eaa84'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Australian and New Zealand Head and Neck Society', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('ANZHNS', @equivalentId)

SET @equivalentId = 'e0f18b11-ec96-44be-8f46-5934f4ace07e'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Australian and New Zealand Association of Physicians in Nuclear Medicine', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('ANZAPNM', @equivalentId)

SET @equivalentId = '5faa56d2-b3e7-4cdb-b9a1-cda0f07bb7ad'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Australian Medical Writers Association', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('AMWA', @equivalentId)

SET @equivalentId = '6b8069b2-4616-4e68-854e-244e603dc307'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Australian Society for Geriatric Medicine', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('ASGM', @equivalentId)

SET @equivalentId = '23d7b52e-5ba5-4711-bdda-0f4ad5c6a40a'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Children''s Medical Research Institute', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('CMRI', @equivalentId)

SET @equivalentId = 'b9e57958-f959-4c83-bbad-9ad0ff1a4d52'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Doctors'' Reform Society of Australia', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('DRSA', @equivalentId)

SET @equivalentId = 'b4cbce1f-e170-4fca-983a-47d428038365'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Royal Australasian College of Physicians', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('RACP', @equivalentId)

SET @equivalentId = '0915119a-2f72-4977-8a4c-803f7e3199a3'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Australian Meteorological and Oceanographic Society', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('AMOS', @equivalentId)

SET @equivalentId = '5e2c07ee-4dac-4a97-82b9-d9807750b64d'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('The Australian Society for Microbiology', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('ASM', @equivalentId)

SET @equivalentId = 'd2383d2e-e55b-4b17-8b13-6a7169d977a1'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Australasian Institute of Mining and Metallurgy', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('AIMM', @equivalentId)

SET @equivalentId = 'a1d3dd31-8526-4de9-acb4-ec09fc433a6f'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Australian Association of Neurologists', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('AAN', @equivalentId)

SET @equivalentId = 'bc9f3158-ed01-4e32-a19f-1dc4aec8f265'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Australian Neuroscience Society', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('ANS', @equivalentId)

SET @equivalentId = 'f15a93f7-cdc6-4958-9f1b-475afe71c618'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Neurosurgical Society of Australasia', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('NSA', @equivalentId)

SET @equivalentId = '7477e9c8-778f-47e0-a0ea-66c32f4ea46a'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Australian and New Zealand College of Mental Health Nurses', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('ANZCMHN', @equivalentId)

SET @equivalentId = '8f17d81e-ef76-43d9-a288-672bce5be5b2'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Australian College of Critical Care Nurses', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('ACCCN', @equivalentId)

SET @equivalentId = 'dd32cbf6-1746-4be7-af6d-abbab9562eac'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Australian College of Midwifery Incorporated', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('ACMI', @equivalentId)

SET @equivalentId = 'a87d6361-e671-4566-b93b-9a976a378ca9'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Australian Neonatal Nurses Association', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('ANNA', @equivalentId)

SET @equivalentId = '3bd7cffa-13c9-4870-a326-4791a4872c14'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Australian Nursing Council Inc.', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('ANC', @equivalentId)

SET @equivalentId = '1cf835b0-c8c2-4596-873a-577c1c3f77c6'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Nursing Council of New Zealand', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('NCNZ', @equivalentId)

SET @equivalentId = 'c1eeffec-545d-408d-bb20-61cd766a4f9b'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Royal College of Nursing Australia', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('RCNA', @equivalentId)

SET @equivalentId = 'b6d8cce1-25b2-40d6-a626-34bf5ad6ec20'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Royal Australian College of Obstetricians & Gynaecologists', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('RACOG', @equivalentId)

SET @equivalentId = 'd3756d60-6747-42c1-8673-3c778f152e5f'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Occupational Therapy Australia', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('OTA', @equivalentId)

SET @equivalentId = '91cc1fdc-9b21-4107-a32a-c09dcdc731cf'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('New Zealand Association of Occupational Therapists', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('NZAOT', @equivalentId)

SET @equivalentId = '6f5a3692-1c41-4a25-bd9c-a0609a12cc03'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Royal Australian and New Zealand College of Ophthalmologists', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('RANZCO', @equivalentId)

SET @equivalentId = 'fb0841fb-6e4f-4fb9-ba24-4220f29ff3b4'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('The Australian Optical Society', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('AOS', @equivalentId)

SET @equivalentId = 'a2e09ebf-f451-4afd-a8fa-270277f9e2bf'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Optics Group at the University of Melbourne', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('OGUM', @equivalentId)

SET @equivalentId = '0299650b-6600-4277-8c73-ca0bfaf417ee'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Sydney University Physical Optics', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('SUPO', @equivalentId)

SET @equivalentId = '0f0579e9-bdd3-42d1-a99e-a7bb0e4b04a8'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Quantum Technology University of Queensland', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('QTUQ', @equivalentId)

SET @equivalentId = 'dba242c7-5a88-46cc-9b55-057c4d9962d1'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Centre for Quantum Computer Technology', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('CQCT', @equivalentId)

SET @equivalentId = 'fca33aee-3537-4d7c-9abf-55cd0ede7f0a'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Optometrists Association Australia', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('OAA', @equivalentId)

SET @equivalentId = '51ecd75a-96e3-4c21-9395-ce49d4113340'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Australian Orthodontic Institute', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('AOI', @equivalentId)

SET @equivalentId = 'faa6fa70-72e3-4c6b-913c-1d5119b5161f'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('International Association for Dental Research', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('IADR', @equivalentId)

SET @equivalentId = 'cb1d9286-a84c-4c71-a4d9-16fbbe6df2e5'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Asia-Pacific Orthopaedic Association', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('APOA', @equivalentId)

SET @equivalentId = '9e023c96-165e-4059-954d-c2e9370cd1db'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Australian Osteopathic Association', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('AOA', @equivalentId)

SET @equivalentId = 'eabb0b53-06de-4ed8-b74d-03b6409115e9'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Royal College of Pathologists of Australasia', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('RCPA', @equivalentId)

SET @equivalentId = '4eaf745e-df6b-4431-806c-aee187e99eae'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Australian Association of Consultant Pharmacy', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('AACP', @equivalentId)

SET @equivalentId = 'c6ba70d5-3e7d-455c-9f8c-f59b84e8b64d'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Australasian Society of Clinical & Experimental Pharmacologists & Toxicologists', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('ASCEPT', @equivalentId)

SET @equivalentId = 'c7fdd9a7-3492-434d-8868-f677aa9af12c'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Society of Hospital Pharmacists of Australia', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('SHPA', @equivalentId)

SET @equivalentId = '2ddbe39b-fcdb-457a-88ab-2e85addc536d'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Australasian Society for General Relativity and Gravitation', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('ASGPG', @equivalentId)

SET @equivalentId = '5c04d94c-f935-462f-b3be-bff47db6df42'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Australian Institute of Physics', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('AIP', @equivalentId)

SET @equivalentId = '4e3ab30c-5457-40e9-9f8c-22b25cd27bb4'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Australian Federal Police Association', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('AFPA', @equivalentId)

SET @equivalentId = 'f8e3b8e3-a00b-4e60-a990-46c91ba518db'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('New Zealand Police Association', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('NZPA', @equivalentId)

SET @equivalentId = '26f70b09-632f-4375-a34e-9bc27fe63cfd'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Police Association of New South Wales', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('PANSW', @equivalentId)

SET @equivalentId = 'daf5db05-b06b-4603-8e6c-7553715bda6f'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Police Association of South Australia', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('PASA', @equivalentId)

SET @equivalentId = 'a1444217-f14d-48da-8025-22bcbd089534'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Police Association of Victoria', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('PAV', @equivalentId)

SET @equivalentId = 'e64f366f-0dd3-4ffc-883a-ea535282f9d6'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Queensland Police Union', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('QPU', @equivalentId)

SET @equivalentId = '837d0e53-6fc5-470f-a876-54843b230360'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Western Australian Police Union', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('WAPU', @equivalentId)

SET @equivalentId = '1ac1146b-00f9-4f0f-9d9d-d814ef4a5e9b'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Royal Australian and New Zealand College of Psychiatrists', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('RANZCP', @equivalentId)

SET @equivalentId = 'd18ebf8c-0fcd-4ceb-8032-d7ce5186616f'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Australian Psychological Society', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('APS', @equivalentId)

SET @equivalentId = '4768e7ae-2744-4276-a6c0-0c190b0dd108'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Public Relations Institute of Australia', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('PRIA', @equivalentId)

SET @equivalentId = '770c3929-9ad6-43ac-a208-59d48df4faa1'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Australian Institute of Quantity Surveyors', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('AIQS', @equivalentId)

SET @equivalentId = '5ced7eb5-acc5-4dbb-b288-e1865f6b8dab'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Australian Diagnostic Imaging Association', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('ADIA', @equivalentId)

SET @equivalentId = '23897a3c-35ca-4f5f-9f21-bc551bdeeb0a'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('The Australian Institute of Radiography', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('AIR', @equivalentId)

SET @equivalentId = '62f60273-8801-417f-8c26-d82ecd97953c'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Royal Australian and New Zealand College of Radiologists', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('RANZCR', @equivalentId)

SET @equivalentId = 'c5e5d6f3-1039-4117-9da9-0c6ae140763e'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Australian Property Institute', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('API', @equivalentId)

SET @equivalentId = '135ad002-b2e8-4305-8de4-8e3406211f12'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Real Estate Institute of Australia', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('REIA', @equivalentId)

SET @equivalentId = 'f7d1c67d-4831-45c4-ab4c-523f8d3080b4'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Real Estate Institute of New South Wales', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('REINSW', @equivalentId)

SET @equivalentId = 'fc4fb2e9-c5ab-4609-8edf-7626c64dc37a'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Real Estate Institute of Queensland', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('REIQ', @equivalentId)

SET @equivalentId = 'e420d9d9-de4b-4b12-b23a-173d6001d232'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Real Estate Institute of South Australia', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('REISA', @equivalentId)

SET @equivalentId = 'd8ec73e9-b536-4b43-b0bc-3a5dc1a2abb6'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Real Estate Institute of Victoria', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('REIV', @equivalentId)

SET @equivalentId = '8cf40223-8402-4ead-839c-df43eb953601'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Real Estate Institute of Western Australia', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('REIWA', @equivalentId)

SET @equivalentId = '295fa61b-ef4d-4cb0-b13a-4333aab9f381'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Rheumatology Health Professionals Association', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('RHPA', @equivalentId)

SET @equivalentId = '21d3fee2-7a80-4ab6-a938-61292c9109e6'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Australian Academy of Science', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('AAS', @equivalentId)

SET @equivalentId = '069e185f-c31c-4660-9aa9-f1afa7075c75'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Australian Academy of Technological Sciences & Engineering', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('AATSE', @equivalentId)

SET @equivalentId = '63186586-fe3c-4dd2-bc5d-f84e28f97f27'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Royal Society of New Zealand', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('RSNZ', @equivalentId)

SET @equivalentId = '23f4babc-9fa0-4b90-88e2-06b189840594'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Australian Association of Social Workers', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('AASW', @equivalentId)

SET @equivalentId = '44d6a3cc-6be1-4486-bf08-167c7fc001af'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Australian Institute of Sport', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('AIS', @equivalentId)

SET @equivalentId = '2195dbdc-dfd2-4700-b0e2-adebf007ca24'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Australian Association for Exercise and Sport Science', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('AAESS', @equivalentId)

SET @equivalentId = '1eaf4839-c50f-4538-983a-9d5d89ecccac'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Statistical Society of Australia', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('SSA', @equivalentId)

SET @equivalentId = 'f0fbfeb3-35d6-4c77-97c5-d29126c8f372'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Royal Australasian College of Surgeons', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('RACS', @equivalentId)

SET @equivalentId = '7f03f988-4732-47ec-bdca-ab1f66b6bcca'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Australasian Society of Cataract and Refractive Surgeons', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('ASCRS', @equivalentId)

SET @equivalentId = '079833b1-3ec5-4b82-abbe-ccd440739062'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Institution of Surveyors Australia', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('ISA', @equivalentId)

SET @equivalentId = '8688d950-b86e-4c50-a045-fb2ed4f12f75'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Taxation Institute of Australia', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('TIA', @equivalentId)

SET @equivalentId = 'ee780f2a-3c8c-4991-8010-d248fa7983da'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Association of Professional Teachers', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('APT', @equivalentId)

SET @equivalentId = 'eed18430-238b-4389-841b-23790b93b4fe'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Australian Science Teachers Association', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('ASTA', @equivalentId)

SET @equivalentId = '86932ddf-c3b2-46ba-b139-a9ae85feeece'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Independent Teachers Union of Australia', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('ITUA', @equivalentId)

SET @equivalentId = '09151538-e9ce-433d-9624-31b1e80ff6d3'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('National Association of Agriculture Educators', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('NAAE', @equivalentId)

SET @equivalentId = '74d5c9d2-c787-4200-b9d2-d49adeef1901'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Tourism Council of Australia', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('TCA', @equivalentId)

SET @equivalentId = '99ec39b2-e946-4e43-a67a-b1ea276615bc'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Australasian Society for Ultrasound in Medicine', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('ASUM', @equivalentId)

SET @equivalentId = '0de1da86-8fb4-4166-89df-b9e9ae18cd75'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Urological Society of Australasia', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('USA', @equivalentId)

SET @equivalentId = '0bccc454-c5ec-4cce-924d-230be3d6e625'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('New Zealand Institute of Valuers', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('NZIV', @equivalentId)

SET @equivalentId = 'f832a957-6c66-4bfd-b8ac-63f7775284f9'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Australian Veterinary Association', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('AVA', @equivalentId)

SET @equivalentId = 'd248e9e5-65c6-4928-b8cb-c6121827ed9d'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('New Zealand Veterinary Association', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('NZVA', @equivalentId)

SET @equivalentId = 'f0e56370-736a-45cf-b864-9d66f29ba09d'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Australian Education Union', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('AEU', @equivalentId)

SET @equivalentId = 'bb219d06-df83-44b7-9bd8-8f59932a90bd'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('IT Manager', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('I.T. Manager', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('National IT Manager', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Group IT Manager', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('IT Project Manager', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('information technology manager', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Online Project Manager', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('IT Operations Manager', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Computer Operations Manager', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('IT operations', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Senior IT Operations Manager', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Senior Manager IT Operations', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Desktop Delivery Lead/IT Operations Specialist', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('IT Operations Specialist', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('IT Operations & Program Manager', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('IT & Operations Support Manager', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Technical Associate - IT Operations', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Deputy General Manager - IT & Operations', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Senior Manager - IT Operations and Infrastructure', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('IT Operations Service Delivery Consultant', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('IT Operations Administrator', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Operations Service Specialist IT Operations', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Head Of IT Operations', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('IT & Operations', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('IT Operations Support Analyst/Database Administrator', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Data Services Executive-IT Operations', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Operations Supervisor / IT Administrator', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Team leader within IT Operations and Maintenance Department', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('General Manager - IT Operations', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('2009 General Manager - IT Operations', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('IT Performance Manager', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('IT & Business Systems Manager', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('IT Operations Management', @equivalentId)

SET @equivalentId = 'c2bed039-1243-44bf-b408-64ec6ee755bc'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('IT Coordinator', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('coordinator IT', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('information technology coordinator', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('IT Co*Ordinator', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('IT co-ordinator', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('co-ordinator IT', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('information technology co-ordinator', @equivalentId)

SET @equivalentId = 'a72a6f4b-b241-4e5d-aaa3-75132fb93ffe'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Infrastructure Project Manager', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Network Manager', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Core Network Manager', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Systems IT and Network Manager', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('System Network Manager', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('DRP Manager & BAU Infrastructure Project Manager', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Senior Infrastructure Project Manager', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('IT Network Manager', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Global Network Manager', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('ICT Network Manager', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Information Communication Technology Manager', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Information Communications Technology Manager', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Information Communication Technologies Manager', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Network Manager/IT Consultant', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('IS Manager', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Information Systems Manager', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Info Systems Manager', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('ICT/Infrastructure Management', @equivalentId)

SET @equivalentId = 'ac15d418-2cee-41f6-9da9-bb75de356f97'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Network architect', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Network Technician', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Chief Network Architect', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('System Engineer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Systems Engineer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Solutions Architect', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Solution Architect', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('IT Enterprise Solution Lead Architect', @equivalentId)

SET @equivalentId = 'e6654961-5fc0-4b91-bdab-d8a795c5775f'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Systems administrator', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Network Administrator', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Network Admin', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Network Systems Administrator', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Network Administration', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Network', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('System administrator', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Senior Network Administrator', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Sr Network Administrator', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('sysadmin', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('sys admin', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('system administration', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('IT Network Administrator', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('I.T. Network Administrator', @equivalentId)

SET @equivalentId = 'c5ea2b97-89ae-4e6d-9c1e-b8c75c170a32'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Assistant Network Administrator', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Junior Network Administrator Cum Technical Support Assistant', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Junior Network Administrator', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Assistant Systems Administrator', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('IT System Administration Assistant', @equivalentId)

SET @equivalentId = 'e859da81-92fb-468e-b1ec-4dba4f7ef322'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Network Engineer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Network support engineer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Network Support', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('hardware engineer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Network Consultant', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Network Support Officer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Network Specialist', @equivalentId)

SET @equivalentId = 'fd79a5c8-cabd-41d9-ad9b-064264387054'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Database Administrator', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('DBA', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('D.B.A.', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('data base administrator', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('IT Database Administrator', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('I.T. Database Administrator', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('db administrator', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Database Analyst', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('database manager', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('database consultant', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Data base Analyst', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('data base manager', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('data base consultant', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('database admin', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('data base admin', @equivalentId)

SET @equivalentId = 'dcdd4526-a8de-474d-8f59-b725362ff7d9'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Software Development Manager', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('software dev manager', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('software development mgr', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('software dev mgr', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('application development manager', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('applications development manager', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('application engineering manager', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('software engineering manager', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('head of software development', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('applications manager', @equivalentId)

SET @equivalentId = '7171ae72-a619-4121-87a8-0e2f99d56f4c'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Helpdesk Manager', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('help desk manager', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('help desk supervisor', @equivalentId)

SET @equivalentId = '234a2a5d-962d-4fbe-916d-75d7406c8f6b'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Assistant IT Manager', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Asst IT Manager', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Assistant IT Mgr', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Assistant Information Technology Manager', @equivalentId)

SET @equivalentId = 'c350942b-6b82-4294-b485-e347cfe7a18d'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('IT Operations Support Graduate', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('IT Operations Support', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('IT Operations Assistant', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('IT Operations Executive', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('IT Operations Technician', @equivalentId)

SET @equivalentId = 'd8ec6a68-c3d4-4424-8496-a0c77ab4aa8a'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Software Architect', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Systems Architect', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Senior Analyst Programmer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Senior Software Architect', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Senior Software Design Engineer', @equivalentId)

SET @equivalentId = '1382f785-149c-4532-8b45-7322614af110'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Senior Developer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Lead Developer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Senior Programmer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Senior Software Engineer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Lead Software Engineer', @equivalentId)

SET @equivalentId = '53fcdb61-25ee-4432-aeb3-f77f8b06a169'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Developer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Database Developer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Application Developer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('System Developer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Systems Developer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Programmer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Software Developer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Software Programmer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Web Programmer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Applications Programmer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Programmer / Analyst', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Application Programmer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Software Engineer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('System Analyst / Software Engineer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Analyst Programmer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Contract Programmer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Analyst / Programmer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Systems Programmer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Computer Programmer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Embedded Linux Software Engineer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Software Eng', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Dev', @equivalentId)

SET @equivalentId = '029d194c-1839-4b79-a523-69ac61e1d99f'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Junior Programmer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Junior Developer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Trainee Programmer', @equivalentId)

SET @equivalentId = '88c43fa4-54b7-4e5f-9742-0ba850d3ef68'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Designer/Developer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Interface Developer', @equivalentId)

SET @equivalentId = 'fd66c017-e958-400f-8445-82a947cca675'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Systems Analyst/Programmer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Programmer/Analyst', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Programmer Analyst', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Analyst/Programmer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Senior Analyst/Programmer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Systems Analyst', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Senior Systems Analyst', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('System Analyst', @equivalentId)

SET @equivalentId = '9d09ef3b-f8ff-46d1-bec1-9e3ac6bcd691'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Test Team Leader', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('TEST MANAGER', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Test Coordinator', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Testing Manager', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Senior Test Analyst', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Sr Test Analyst', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Senior Test Engineer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Test co-ordinator', @equivalentId)

SET @equivalentId = 'e0533859-7b6a-439f-8b13-0ac655ffeef4'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Software Tester', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('System Tester', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('SYSTEMS TESTER', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('User Acceptance Tester', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('UAT Tester', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Software Test Engineer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('System Test analyst', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Systems Test Engineer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Test Administrator', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Test Analyst', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Test Engineer', @equivalentId)

SET @equivalentId = '772f0fe2-7f34-46ca-8e82-cdd43e82ba2c'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('usability', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('user interaction', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('user experience', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('ui', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('user interface', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('u.i.', @equivalentId)

SET @equivalentId = '7ce8da5f-e484-4c41-873f-5769767fd68e'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Usability Consultant', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Web experience specialist', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('User Experience Specialist', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('user interface designer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('UI Design', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('UI Designer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('GUI Designer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('GUI design', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('rft5', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Graphic User Interface designer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Graphic User Interface designing', @equivalentId)

SET @equivalentId = '94e55f4c-fdc2-4b61-ae11-f9e9c761bf93'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('IT Administrator', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('I.T. Administrator', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('IT Admin', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('I.T. Admin', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('IT Systems Officer', @equivalentId)

SET @equivalentId = 'c1233d3d-55b1-4b4b-a753-223ba38bfe3c'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('IT Officer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('IT Assistant', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('I.T. Officer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('I.T. Assistant', @equivalentId)

SET @equivalentId = '26691239-043c-4c46-8ba6-0643445fa588'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('IT Support Analyst', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('i.t. support analyst', @equivalentId)

SET @equivalentId = 'c05d3dc2-1739-490c-b9d9-db2b6a43b80e'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('IT Specialist', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('it guru', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('it expert', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('information technology specialist', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('information technology expert', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('i.t. specialist', @equivalentId)

SET @equivalentId = '846e1854-5949-41fd-b094-22f5bb67e788'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('IT Trainer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('IT Tutor', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('computer tutor', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('computer trainer', @equivalentId)

SET @equivalentId = '4e6fe987-c097-43e3-a793-6409249ce019'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('vb.net', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('visual basic.net', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('visual basic dot net', @equivalentId)

SET @equivalentId = '2132ef1f-28ee-4b99-a5da-6aa04f3e9b19'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('VB', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Visual basic', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('visualbasic', @equivalentId)

SET @equivalentId = '330706aa-c021-4211-a8f2-812bfc439d23'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Telco', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Telecoms', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Telecom', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Telecomms', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Telecommunication', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Telecommunications', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('tel co', @equivalentId)

SET @equivalentId = '053bdbe2-5448-46f5-b2b5-cc5f674431ac'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('PABX', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Private Automatic Branch eXchange.', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('pbx', @equivalentId)

SET @equivalentId = '9e3c49eb-f160-4667-888e-540422486a80'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Graphics/Multimedia', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('multimedia', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('multi media', @equivalentId)

SET @equivalentId = '218b559f-0a78-4084-970d-75907f73b69a'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Graphic designer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Graphic Design', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('graphic artist', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('freelance designer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('graphic Web Designer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Creative Graphic Designer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Front end graphic designer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('back end graphic designer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('creative web designer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('web design', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('site designer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('site design', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('online designer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Graphic Design Assistant', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Graphic Designer/ Coordinator', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Graphic Designer & Administration', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Freelance Photographer / Photoshop tutor / Graphic Designer', @equivalentId)

SET @equivalentId = 'ace24f75-5355-4796-9fad-33b8876040c7'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Artist', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('artiste', @equivalentId)

SET @equivalentId = '2e6d4b40-a772-4333-9d88-e8167cea12d0'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('freelance graphic designer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Freelance Graphic Design', @equivalentId)

SET @equivalentId = '4ac8ae05-f246-42d5-9fdd-2ad0c895043b'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Web / Graphic Designer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Web/Graphic Designer', @equivalentId)

SET @equivalentId = '208beecd-02bf-4549-9b7f-0d9760304408'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('MULTIMEDIA DEVELOPER', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Multimedia Designer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('MULTI MEDIA DEVELOPER', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Multi media Designer', @equivalentId)

SET @equivalentId = '77359533-cdd0-4b3f-8519-43e838fb9f0f'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Business Analyst', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Bus Analyst', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('BA', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('B.A.', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Business Analyst/Consultant', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Lead Business Analyst', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Business Analyst Team Leader', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('process analyst', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Project Manager/Business Analyst', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Project Manager cum Lead Business Analyst', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('business Analyst / Testing', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Technical Business Analyst', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Tech Business Analyst', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Tech BA', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Technical BA', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Business Improvement Analyst', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Business Analyst / Consultant', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Infrastructure Business Analyst', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('BI Business Analyst/Data Modeller', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Business Analyst/ UAT tester', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Functional Business Analyst', @equivalentId)

SET @equivalentId = '140435db-38bc-43cb-a3c8-08d373913cb1'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('SENIOR BUSINESS ANALYST', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Sr Business Analyst', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Senior Business Analyst/Project Manager', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Lead Senior Business Analyst', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Senior Business Analyst/ Test Lead', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Senior Business Analyst ICT Infrastructure', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Senior Business Analyst Business Assurance', @equivalentId)

SET @equivalentId = '4674a899-a396-4af0-9487-c59b7f1b9755'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('IT Business Analyst', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('I.T. Business Analyst', @equivalentId)

SET @equivalentId = '33f70529-096a-4324-a40b-284a4960cab1'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('data analyst', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('information analyst', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Business Information Analyst', @equivalentId)

SET @equivalentId = 'a5ec44c6-6cdf-43aa-a838-8f7e703df5f9'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Digital Producer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Online producer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('web producer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('site producer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('on line producer', @equivalentId)

SET @equivalentId = '120e2b61-b6d6-4d0a-83e6-a406b5bec064'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('IT Tester', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('IT Test Anaylst', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('IT Test Specialist', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Tester', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Testers', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Test/QA Specialist', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Test/QA Manager', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Test/QA Analyst', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('IT QA Analyst', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('IT QA Specialist', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('IT QA Manager', @equivalentId)

SET @equivalentId = 'd822b767-d91d-49e0-9103-39fa687e6ea6'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Mac operator', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('finished artist', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Apple Mac operator', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Macintosh operator', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Apple Macintosh operator', @equivalentId)

SET @equivalentId = '3582647d-35e5-4b27-960a-d18e68084a9b'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('IT Business Specialists', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('IT Business Specialist', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('IT Account Manager', @equivalentId)

SET @equivalentId = '91b62aca-8482-467d-823f-1750ee8223b0'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('IT development manager', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('IT Dev Mgr', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('IT Dev Manager', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('IT Project Manager Team Leader', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('IT Project Manager/Team Leader', @equivalentId)

SET @equivalentId = '44ac746a-9e74-497f-8941-c40665aa5440'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('IT Security Specialist', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('I.T. Security Specialist', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('IT Security Analyst', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('I.T. Security Analyst', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Security Analyst', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Security Specialist', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Security Consultant', @equivalentId)

SET @equivalentId = '1db6fd0f-1105-443b-b2de-f9562efa795c'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Fraud Detection', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('IT Fraud detection', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('I.T. Fraud detection', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('online fraud', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('online fraud detection', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('online fraud specialist', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('online fraud analyst', @equivalentId)

SET @equivalentId = 'c6f909b0-96dc-4dd0-bc67-50115c0b8940'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('IT consultant', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('I.T. Consultant', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Systems consultant', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Sole-trader IT consultant', @equivalentId)

SET @equivalentId = '3a843d5b-def5-41b3-879c-b8cf641aaf6b'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('java developer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('java', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Java Programmer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Java software programmer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Java Development Engineer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Java Designer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Java analyst', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('JAVA and Web developper', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('JAVA and Web developer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Java J2EE Programmer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Consultant Java Engineer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Analyst Java/J2EE Developer Contract', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('R&D Software Engineer JAVA/J2EE', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('java/j2ee developer web developer analyst develper', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Developer .Net and Java', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Java Engineer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Java Application Developer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Java Analyst/Programmer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('JAVA/J2EE analyst developer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Web application developer Java', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Java Developer contractor', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Java/J2EE Analyst Programmer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Java/J2EE Developer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Java / J2EE Developer', @equivalentId)

SET @equivalentId = '9f699f06-188a-4fe4-ac4f-7ca2750b5fd0'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Senior Java Developer/Technical Lead', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Senior Java Developer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Senior Java Programmer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Senior Java Software Programmer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Senior Java Software Developer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Senior Java Developer Software Architect', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Java Team Manager', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Senior Java/J2EE developer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Senior Java/PHP Developer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Senior JEE Java solutions developer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Java developer/designer - technical team leader', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('SENIOR JAVA/J2EE ANALYST PROGRAMMER', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Senior Java Consultant', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Technical Manager and Java Programmer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('System Analyst & Senior Java Developer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Sr. Analyst/Java Developer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Sr. Consultant/Senior Java developer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Senior Java Multimedia Developer', @equivalentId)

SET @equivalentId = '0c77da46-de03-4c07-b914-6c764b0da8fb'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('idn', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('identity driven networking', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('identity driven network', @equivalentId)

SET @equivalentId = '6abf38ba-a3de-4447-a2b7-22dddc6d0c3f'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Infrastructure Consultant', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Infrastructure Solutions Consultant', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('IT Infrastructure Consultant', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('IS Management Consultant', @equivalentId)

SET @equivalentId = 'fe6ada05-13a0-4d95-b2c6-ec393c9de3fb'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Senior Infrastructure Consultant', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Sr Infrastructure Consultant', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Program Director Infrastructure', @equivalentId)

SET @equivalentId = '6ea5c79f-8015-4157-b571-62e32e6cfac8'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('IT Contractor', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('IT Contracts', @equivalentId)

SET @equivalentId = 'e421dd49-20a9-4705-9ef1-69bab444e8e2'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Help Desk', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Helpdesk', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('PC Support/Help Desk', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Help Desk Consultant', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Helpdesk Consultant', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('HELP DESK OPERATOR', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('HelpDesk Operator', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Helpdesk Support', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Help Desk Support', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Help Desk Officer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('HelpDesk Officer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Helpdesk Support Officer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Help Desk Analyst', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('HelpDesk Analyst', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Helpdesk co-ordinator', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Helpdesk coordinator', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Helpdesk co ordinator', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('help desk engineer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Helpdesk/Support', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Help desk/Support', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('help desk co-ordinator', @equivalentId)

SET @equivalentId = '60793732-ed96-4503-bdc3-73eba1d23a07'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Intranet Developer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Intranet Application Developer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Intranet Software Developer', @equivalentId)

SET @equivalentId = 'a31789b2-d738-4b6a-adfb-7c139c018fb2'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Website Developer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Web Application Developer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Web Designer/Developer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Web Site Developer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Freelance Web Developer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Senior Web Developer', @equivalentId)

SET @equivalentId = '59ec5572-b5b1-46fb-8405-efebe2b7df59'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('IT Support Engineer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Computer Engineer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Desktop Support Engineer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('IT Support Technician', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Computer Technician', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('IT Technician', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('i.t. support engineer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('i.t. technician', @equivalentId)

SET @equivalentId = '7df370e8-6e9e-46db-ad1a-cac4d47e59bb'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('technical writer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('technical document writer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('document writer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('specification writer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Consultant/Technical Writer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Business Analyst / Technical Writer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Contract Technical Writer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Technical writer contract', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('SYSTEM ANALYST/ TECHNICAL WRITER', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Technical Process Analyst Technical Writer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Technical Process Analyst / Technical Writer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Technical Writer/Business Process Analyst', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Web site Developer and Technical Writer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Technical Writer one year contract', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Content/Technical Writer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Document Controller/Technical Writer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Technical Writer / Business Analyst', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Technical officer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Business Analyst Technical Writer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Technical Writer/Projects Officer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Data Analyst / Technical Writer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Business Analyst / Technical Writer / Trainer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Associate Technical Writer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Casual Technical Writer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Business / Data Analyst', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Business Systems Analyst', @equivalentId)

SET @equivalentId = '96338998-0340-4eb9-8cd5-4bdcfabdfe1a'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('cms', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('content management system', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('CMS Operator', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('CMS Specialist', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Web CMS', @equivalentId)

SET @equivalentId = 'eb9673df-7992-4d22-a771-cf6e5d656ec8'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Customer Management Support Representative CMS', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Customer Management Support Representative', @equivalentId)

SET @equivalentId = '7a8ccbb7-1c0b-442d-99a9-1bce4f0c2730'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('ColdFusion', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('cold fusion', @equivalentId)

SET @equivalentId = '5d4bd4f9-d093-4f89-8c9f-729c153e41a4'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Web Administrator', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Web Content Administrator', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('website administrator', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('website editor', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('website maintainer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('website support', @equivalentId)

SET @equivalentId = '43cdfaf5-e2d9-461a-8bcb-58b2178bcf0b'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('IT & T Business Development Manager', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('IT&T', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('IT and Telecoms', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('IT and Telecommunications', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('IT & Telecoms', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('IT & Telecommunications', @equivalentId)

SET @equivalentId = '131d9417-da04-4095-bb33-ebefd46e910f'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Exchange Administrator', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Exchange Engineer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Exchange Specialist', @equivalentId)

SET @equivalentId = '619b2175-dd75-4bee-9fdb-bde28b617723'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Business intelligence', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('BI', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Bus Intelligence', @equivalentId)

SET @equivalentId = '2e813045-4630-43a5-86e2-ad80ed3d4ff8'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('SEO Search Specialist', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('SEO Specialist', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Search engine optimisation specialist', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('seo consultant', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('seo contractor', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('seo professional', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('search consultant', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('search contractor', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('search professional', @equivalentId)

SET @equivalentId = '7704748c-e4d0-495e-bead-0f831394cd71'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('SEM specialist', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Search engine marketing specialist', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('SEM consultant', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('SEM contractor', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('SEM professional', @equivalentId)

SET @equivalentId = '8e720012-a3f6-44b1-85f1-dbd6480b9678'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Agile trainer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Agile coach', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Agile leader', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Agile coach / practitioner', @equivalentId)

SET @equivalentId = 'da00f23a-72b0-452c-9be3-93ba7c6707e2'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Integration tester', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('integration test specilist', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('integration testing', @equivalentId)

SET @equivalentId = '0b7d97ad-580d-4fe3-997f-8e7c8ec1f237'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('engineer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('eng', @equivalentId)

SET @equivalentId = '85c977d2-f76e-4de6-9612-41fdd507ab87'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Engineering Manager', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Senior Engineer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Chief Engineer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('director of engineering', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('head of engineering', @equivalentId)

SET @equivalentId = 'a27113e8-f36e-46a1-9b8e-aa156a5ead66'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('infrastructure engineer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('engineer - infrastructure', @equivalentId)

SET @equivalentId = 'b6bf1264-2942-49a0-af46-bcf702f84686'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('aircraft maintenance engineer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('aviation maintenance engineer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('aviation engineer', @equivalentId)

SET @equivalentId = '7e47ee23-ba2f-4b8b-8b37-bbe6aff9b6ad'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('site manager', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('site superintendant', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('superintendant', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('site foreman', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('site mgr', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('sub foreman', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('subforeman', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('construction manager', @equivalentId)

SET @equivalentId = '9ede361b-e677-40d5-ac71-afea698f0a5c'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('System Engineering Management Consultant', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('system engineering manager', @equivalentId)

SET @equivalentId = 'cfb1dc00-a6ec-45ef-973f-02194548b960'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('engineering project manager', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Senior Project Engineer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Project Engineer', @equivalentId)

SET @equivalentId = '02a7f358-7cb9-4a87-9997-e570f9f2c9a9'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Site Engineer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Site Supervisor', @equivalentId)

SET @equivalentId = '297ce6fe-c8e3-4961-91b2-bec655045a46'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Assistant Engineer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Engineering Assistant', @equivalentId)

SET @equivalentId = '3eba9937-a274-4ed9-8b5d-3fb0117e75f3'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Junior Engineer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Student Engineer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('graduate engineer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Undergraduate Engineer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('DESG Student Engineer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('trainee engineer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('engineering trainee', @equivalentId)

SET @equivalentId = 'c7cfdc65-d527-4405-ac56-2f191a790fe6'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Field Engineer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('FIELD SERVICE ENGINEER', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('service engineer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Customer Engineer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('customer service engineer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Field Eng', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Field Engineering', @equivalentId)

SET @equivalentId = '28ed8ab3-dca1-4c1e-8aa4-71b143c5dcc3'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Engineering Consultant', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Consultant engineer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Consulting Engineer', @equivalentId)

SET @equivalentId = '40ddf68a-bd3b-4b30-92a8-738635e92d86'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Customer Support Engineer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Support Engineer', @equivalentId)

SET @equivalentId = '75e9269e-d2ee-4740-9950-170813cd9b29'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Aircraft Engineer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Aircraft Mechanic', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('aerospace fitter', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('aircraft fitter', @equivalentId)

SET @equivalentId = '541cbfd1-a8a6-40c4-81f6-0f30f56ebe28'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('aeronautical engineer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('aerospace engineer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Design Engineer Aerospace', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('aeronautical design engineer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('aerospace design engineer', @equivalentId)

SET @equivalentId = '825eef79-27ac-43e4-8b63-8262f498d1f7'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Design Engineer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('engineering designer', @equivalentId)

SET @equivalentId = '52cdecbf-ed74-4364-b62c-7733424abfdc'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('communications engineer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('communications and electronic engineer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('comms engineer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Communications Eng', @equivalentId)

SET @equivalentId = '30f3c01e-f378-4e9d-9689-8418e98ce5c2'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('industrial engineer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('product engineer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Production Engineer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Manufacturing Engineer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Industrial Eng', @equivalentId)

SET @equivalentId = 'bfe2835f-5fe3-4508-bd46-b5e010fd5d5b'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('electrical engineer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Electrical Design Engineer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Electrical and Instrument Engineer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Electrical Project Engineer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Electrical Services Design Engineer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('electrical engineering', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Electrical/Electronics Engineer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Electrical Eng', @equivalentId)

SET @equivalentId = '70406d5c-a5a6-4673-8cd2-f58395133c45'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Mechanical Design Engineer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Mechanical Designer', @equivalentId)

SET @equivalentId = '93a8c76f-7eec-4d49-9b5a-a0593367acc4'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Planning Engineer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('project planning engineer', @equivalentId)

SET @equivalentId = '217517a1-bee1-46d7-b151-68e0ab8adc6e'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Electronics Engineer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Electronic Engineer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Electronics Eng', @equivalentId)

SET @equivalentId = '94ae94a1-fae4-45ba-a255-faafef432b60'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Estimator', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('quantity surveyor', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('qs', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Estimater', @equivalentId)

SET @equivalentId = 'f20cdd0a-20f3-4d22-8ea8-71cd5b137a73'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Automation Engineer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('robotic engineer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('automation design engineer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('control engineer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('electrical control engineer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('control systems engineer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('control system engineer', @equivalentId)

SET @equivalentId = '7497a4b3-fe4b-4f99-8498-01900589ecad'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('robot', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('robotic', @equivalentId)

SET @equivalentId = 'a56be2e7-353a-4696-92e2-075b605e6ada'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Sound Engineer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Audio Engineer', @equivalentId)

SET @equivalentId = '2a1a2be7-ca77-4051-becf-db6d8e26a8d0'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Quality Assurance Engineer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('qa engineer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('quality engineer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('reliability engineer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Quality Assurance Eng', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('QA Eng', @equivalentId)

SET @equivalentId = '51b3db1d-8d03-4749-b6ac-ba2c22c25f13'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Environmental Engineer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Environmental Officer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Environmental Health Officer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('sustainability engineer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('eho', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('health officer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('environmental coordinator', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('environmental co ordinator', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('sustainability coordinator', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Environmental Eng', @equivalentId)

SET @equivalentId = '7aa95979-5fca-4c2e-9593-f31ac2878f74'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('hydraulic engineer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('fluid engineer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('water engineer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('fluid power engineer', @equivalentId)

SET @equivalentId = '9c186943-040a-494c-8f22-9f2e510f9fcf'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('hydraulic fitter', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('hydraulic mechanic', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('hydraulic fittersales', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('hydraulic technician', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('hydraulic valve technican', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('hydraulic hose fitter', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('hose technican', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('industrial hydraulic technican', @equivalentId)

SET @equivalentId = '42731141-a993-463d-a536-a8b4d152b06f'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('mechanical engineer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('mech eng', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Maintenance Engineer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Plant Engineer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Mechanical Eng', @equivalentId)

SET @equivalentId = '0ed2cdea-0b32-4c07-85e4-7f0f2a98141a'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('chemical engineer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Chemical & Process Engineer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Chemical and Process Engineer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('chemical process engineer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Process Engineer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Petrochemical Engineer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Chemical Eng', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Process Eng', @equivalentId)

SET @equivalentId = '6757fe10-9b50-4d0e-8f90-766b19081a22'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('petro chemical', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('petrochemical', @equivalentId)

SET @equivalentId = 'd6f26eb2-484c-423d-8f11-757934eb3481'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Mining Manager', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Quarry Manager', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Quarry Operations', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('project manager mining', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('mining project manager', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('mine manager', @equivalentId)

SET @equivalentId = '56ddb54f-07e1-4a76-9369-f29433f147e0'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('mining engineer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Mine Geologist', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Mine Specialist', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('mines engineer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Mining Consultant', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Mine Planning engineer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('mine diesign engineer', @equivalentId)

SET @equivalentId = 'f1e0fadf-70bb-43d8-a8e4-a3dc2d167506'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('drill fitter', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('drill technician', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('drill rig operator', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('rig operator', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('rig maintenance', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('rig fitter', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('drillrig operator', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('drillrig worker', @equivalentId)

SET @equivalentId = '8c63ba0f-8d2e-4cff-867d-3b5e2d11b917'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('mine maintenance operator', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('mine site maintenance', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('minesite maintenance', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('mining maintenance', @equivalentId)

SET @equivalentId = '73f6a556-6066-4f0e-afcf-444b64592c7b'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('underground', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('under ground', @equivalentId)

SET @equivalentId = '8b1296dc-5694-40ee-97b6-1af5fe6bce33'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('aboveground', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('above ground', @equivalentId)

SET @equivalentId = 'd2d11e02-a757-45ec-82db-10a59fedf510'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Civil Engineer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('civilengineer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('civil engineering', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('construction engineer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Civil Eng', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('civil project engineer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Civil Works Design Engineer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Civil Works Engineer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('structural engineer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('civil structural engineer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Civil Structural Project Engineer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('structural design engineer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('structural consulting engineer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('civil consulting engineer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Structural eng', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Structural designer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Structural design', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('urban development engineer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('urban development', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('civil development', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Civil Designer', @equivalentId)

SET @equivalentId = 'bc4f6d42-0b42-497f-8bf7-7ac42072ca6e'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Trainee Civil Engineer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Junior Civil Engineer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Graduate Civil Engineer', @equivalentId)

SET @equivalentId = 'd7790a77-3e30-429a-a751-601f5357c08e'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('hydrology', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('hydrologist', @equivalentId)

SET @equivalentId = '39aef434-1938-4d16-ae42-c3152c6fabc3'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('town planner', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('town planning', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('townplanner', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('urban planner', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('urban planning', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('building planner', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('town planning officer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('town planning coordinator', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('town planning co ordinator', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('town planning consultant', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('urban and regional planning', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('urban & regional planning', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('planning officer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('spatial planner', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('spatial planning', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('town planning co-ordinator', @equivalentId)

SET @equivalentId = 'da3ebb89-b510-424c-9185-d385f1ea3a71'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('planning clerk', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('planning assistant', @equivalentId)

SET @equivalentId = 'b01206a3-68e2-4214-89b1-43b606f3e802'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('building inspector', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('asset inspector', @equivalentId)

SET @equivalentId = '4ef2a524-6f1e-401c-beaf-68ffdba5d188'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Aeronautics Engineer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Aeronautics Eng', @equivalentId)

SET @equivalentId = 'b84d0dce-a41c-4987-aa1f-f7ab5ba7f46b'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('energy and resources engineer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('ER engineer', @equivalentId)

SET @equivalentId = '133ef1c1-6352-4880-bd40-fb8f9a6899da'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Cost Estimator', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Cost Planner', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Cost / Schedule Analyst', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Cost and Schedule Analyst', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Cost & Schedule Analyst', @equivalentId)

SET @equivalentId = '80c506dc-e15b-4561-aca9-5576065912c4'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('signalling', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('signalling engineer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('signalling design', @equivalentId)

SET @equivalentId = '2abfdd58-e2f6-40e8-a2cd-c0592aa2ce01'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('overhead wiring', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('overhead wiring engineer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('ohw', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('ole', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('ohl design engineer', @equivalentId)

SET @equivalentId = '10d09447-1709-495c-b5d7-d66d810901c0'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('permanent way design engineer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('track design engineer', @equivalentId)

SET @equivalentId = '4b0aaf95-c379-4904-bcf2-3c9cb2fc73de'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('field service technician', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('service technician', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('field technician', @equivalentId)

SET @equivalentId = 'f62f2f2b-6832-433a-90ae-4032cb051bff'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Chief Executive Officer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('CEO', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('C.E.O.', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Managing Director', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('MD', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('M.D.', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Executive Director', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Exec Director', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('1ic', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('1.i.c.', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('1i.c.', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('chief executive', @equivalentId)

SET @equivalentId = 'fef1f305-349c-4146-bc28-7ff24c10e5ec'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Executive Chairman', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Exec Chairman', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('E.C.', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('EC', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('exec chairperson', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('executive chairperson', @equivalentId)

SET @equivalentId = '201edc9f-664b-4003-a872-a93c847b5b59'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('General Manager', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('GM', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('G.M.', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Group General Manager', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('GGM', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('G.G.M.', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Gen Mgr', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Gen. Mgr.', @equivalentId)

SET @equivalentId = '6ce06e4e-fbfb-4a42-ba22-851230f07a27'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Chief Operating Officer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('COO', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('C.O.O.', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('chief op. officer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('chief op officer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('cheif operating officer', @equivalentId)

SET @equivalentId = 'da72aa99-dd1e-4cf9-89c6-f58b81326e72'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('vp', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('v.p.', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('vice president', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('vice pres.', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('vice pres', @equivalentId)

SET @equivalentId = '5fb50e32-6c9e-4882-80f8-097babd4bc24'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('senior vp', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('senior v.p.', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Senior Vice President', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('snr vice president', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('snr vice pres.', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('snr vice pres', @equivalentId)

SET @equivalentId = '6e86e00b-1d62-4d65-b1ed-e4abf83a4b54'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('2ic', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('2.i.c.', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('2i.c.', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Second in charge', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('2 IC', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('2nd i.c.', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('2nd in charge', @equivalentId)

SET @equivalentId = 'c9178d39-f663-447a-a0b0-ff7969baa50b'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('director', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Dir', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Dir.', @equivalentId)

SET @equivalentId = 'de35e2de-a3b1-4daa-ac75-2b5c26dc7ea7'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('non executive director', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('non exec director', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('non-exec director', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('non exec dir.', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('non-exec dir.', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('ned', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('non ED', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('non e.d.', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('non-executive director', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('directorship', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('board member', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('board director', @equivalentId)

SET @equivalentId = '9eb43b0a-0e50-43b8-8850-053858f539a6'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('company secretary', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('company sec.', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('co sec', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('co.sec.', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('company sec', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('corporate secretary', @equivalentId)

SET @equivalentId = 'c956c94a-566b-4e92-b3b7-798e3cd080d4'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Chairman', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('chairperson', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('chairwoman', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('chair man', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('chair person', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('chair woman', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('chairman of the board', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('chairperson of the board', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('chair man of the board', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('chair woman of the board', @equivalentId)

SET @equivalentId = 'cbaf3c3d-01fc-41da-96b9-e3600ab8aa83'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('CIO', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('C.I.O.', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Chief Information Officer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('general manager it', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('it director', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('director it', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('director information technology', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('head of it', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('director of i.t.', @equivalentId)

SET @equivalentId = '37650542-2cf8-43a3-b313-6510c216367a'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('manager', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('mgr', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('manger', @equivalentId)

SET @equivalentId = '0586752a-9cdc-42bb-baa2-6e527e432d81'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('State Manager', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('statemanager', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('state mgr', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('regional manager', @equivalentId)

SET @equivalentId = '514bb411-ca5c-4e3b-ab11-3dbbf8193a1a'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Manager Governance', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Manager Accountability', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('compliance manager', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('mgr of compliance', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('compliance mgr', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('manager of compliance', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('project governance', @equivalentId)

SET @equivalentId = '051ec072-e993-49d6-8f01-29c58eeb90fc'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Program Director', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Director program', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('program manager', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('programme manager', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('programme mgr', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('programme director', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('programme dir', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('programme management', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('program management', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('programme specialist', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('program specialist', @equivalentId)

SET @equivalentId = 'fd1c817c-7890-402e-be64-7271ad58db49'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Assistant Manager', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('asst mgr', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('asst manager', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Assistant Manger', @equivalentId)

SET @equivalentId = '602519e5-2c4d-4bcf-b537-551e4b740226'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Senior Supervisor', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Snr Surpervisor', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('senior sup.', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('snr s''visor', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('senior s''visor', @equivalentId)

SET @equivalentId = '83f99620-89af-4384-aa18-e509ac2e07fa'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Assistant Supervisor', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Asst Supervisor', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('assist sup', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('assistant s''visor', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('assist. Sup.', @equivalentId)

SET @equivalentId = 'c896816a-1ba4-4924-bac0-bb41319cdab7'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('foreman', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('fore man', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('foreperson', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('fore person', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('overseer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('over seer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Foreman / Supervisor', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('General Foreman', @equivalentId)

SET @equivalentId = 'a05730f5-4d77-4f57-93c8-dd5f4edc8ff7'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Team Leader', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Team lead', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Teamleader', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('supervisor', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('team manager', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('teamlead', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('team mgr', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('field leader', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('team supervisor', @equivalentId)

SET @equivalentId = '233d4dcf-92df-48c5-b4b1-272cd8655641'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Project Manager', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Project Mgr', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('PM', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Project Manger', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('proj mgr', @equivalentId)

SET @equivalentId = 'c123f5f1-66f7-4f4f-b6b8-02a75fcc760d'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('General Manager Project', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Project Director', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('project coordinator', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Senior Project Manager', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Senior Project director', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('project co-ordinator', @equivalentId)

SET @equivalentId = '3c6f1616-f2fd-46ec-b575-101e98636dad'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('BPM', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('B.P.M.', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('business project manager', @equivalentId)

SET @equivalentId = '4797e99a-cf34-4d6d-a63a-e2abbff5139b'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Assistant Project Manager', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Asst Project Manager', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Asst Project Mgr', @equivalentId)

SET @equivalentId = 'ba12376b-bba2-450e-bfc6-3167934ec629'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Junior Project Manager', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Trainee Project Manager', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('junior pm', @equivalentId)

SET @equivalentId = 'ccc3beb0-d7ff-48cb-9ea1-a91f972cc76a'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('inhouse consultant', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('in-house consultant', @equivalentId)

SET @equivalentId = '3cbe3fe0-b02d-4a65-83af-c9df6d97ef04'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('BPR consultant', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('business process re-engineering', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('bpr', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('business process reengineering', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('process re-engineering', @equivalentId)

SET @equivalentId = 'bc408624-19ac-4be3-96c2-e9a13003e2da'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('BPO', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('business process outsourcing', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('b p o', @equivalentId)

SET @equivalentId = 'cbf9dbc9-a44e-49a8-89d8-00792ba8e4d1'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('development manager', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('dev mgr', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('development mgr', @equivalentId)

SET @equivalentId = '593c5b84-0b0b-48af-976c-fc6eb662f8a4'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('DM', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('D.M.', @equivalentId)

SET @equivalentId = 'dc7216e3-d788-400b-b1d1-52fc7d75500d'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('transition manager', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('transitions manager', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Transition Project Manager', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Transformation Project Manager', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Transformation Manager', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Manager Transition', @equivalentId)

SET @equivalentId = '4a7470a0-bcd7-45ed-ada0-9e7c644323be'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Manager Planning', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Manager- Planning', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('planning manager', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('planning mgr', @equivalentId)

SET @equivalentId = '77334770-e609-4ad3-b741-7967173b0cad'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Chief Finance Officer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('CFO', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('C.F.O.', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Finance Chief', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Chief Financial Officer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Finance Director', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Financial Director', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('FD', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Finance President', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Head Finance', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Group Financial Controller', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Group Finance Controller', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Divisional Finance Controller', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Divisional Financial Controller', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('gm finance', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('g.m. finance', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('general manager finance', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('director of finance', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('gen mgr finance', @equivalentId)

SET @equivalentId = '3c50885b-2398-4126-8a89-a7c5a1ec2134'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Assistant C.F.O.', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Assistant CFO', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Asst C.F.O.', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Asst CFO', @equivalentId)

SET @equivalentId = 'eb7391a3-ade9-430b-8a37-db5d829cc85f'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Financial Controller', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('FC', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('F.C.', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Fin Controller', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Finance Controller', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Business Finance Controller', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Business Financial Controller', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Company Accountant', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Company Acct', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Financial Manager', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Finance Manager', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('vice president finance', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('vp finance', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('v.p. finance', @equivalentId)

SET @equivalentId = 'f297a1d0-ff2f-4259-ba46-7f75fa919678'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Senior Financial Controller', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Sr. Financial Controller', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Sr Fin Controller', @equivalentId)

SET @equivalentId = 'c61c9f5e-ca40-4bfa-bd51-0eb1ef666e19'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Assistant Financial Controller', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Asst Financial Controller', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Asst Fin Controller', @equivalentId)

SET @equivalentId = '267096c3-4380-4e84-99b2-bea58878a758'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Regional Financial Controller', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Regional Finance Controller', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('regional fc', @equivalentId)

SET @equivalentId = '23a4e401-32a1-4caf-8ae6-ae886cc94edb'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('National Financial Controller', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('National Finance Controller', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('national fc', @equivalentId)

SET @equivalentId = '47ecf49b-5475-4bfb-96bb-9ee0a82160bc'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Accounting Manager', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Accounts Mgr', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('accnts mgr', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('accts manager', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('accts mgr', @equivalentId)

SET @equivalentId = 'dcbb55d7-3aaa-4860-8ed9-8ae20239667c'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Senior Finance Accountant', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Sr Finance Accountant', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Sr. Finance Accountant', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('snr finance accountant', @equivalentId)

SET @equivalentId = '5e746b25-fdfb-4578-b409-e13752e36396'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Senior Financial Accountant', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Sr Financial Accountant', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Sr. Financial Accountant', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('snr financial accountant', @equivalentId)

SET @equivalentId = '72ebd3d5-9540-47f2-a251-9ce5d29d0d50'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Administration Manager', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Admin Manager', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('admin mgr', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('administration mgr', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('National Administration Manager', @equivalentId)

SET @equivalentId = '05f59313-697d-4559-8cc9-3230efbf63eb'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Senior Consultant', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Sr Consultant', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Sr. Consultant', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('snr consultant', @equivalentId)

SET @equivalentId = '74b3c051-acea-4092-b72e-de20ebf8d4d5'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Principal Consultant', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Chief Consultant', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Managing Consultant', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('head consultant', @equivalentId)

SET @equivalentId = 'dd65f6a0-7995-4634-b12b-545478002366'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Accountant', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Acc', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Accountan', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('acct', @equivalentId)

SET @equivalentId = 'e53861dd-a18a-4920-add8-99305b637e85'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Financial accountant', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Fin Acc', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Finance Accountant', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Finance Acc', @equivalentId)

SET @equivalentId = '1227350c-1390-4ec5-a083-6825f66e705c'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Corporate Accountant', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Corp Accountant', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Sr. Mg Accountant', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Contract Accountant', @equivalentId)

SET @equivalentId = 'f4493ce7-4849-49bb-baf3-4b469827817a'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Business Accountant', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Bus Acct', @equivalentId)

SET @equivalentId = '434b696c-357d-461d-8693-ee5bc2ca8c8f'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Management accountant', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Management Acct', @equivalentId)

SET @equivalentId = 'b64b7c62-b9df-4962-aeb9-7363b1698844'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Senior Management Accountant', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Sr Management Accountant', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('snr management accountant', @equivalentId)

SET @equivalentId = '151a5ecc-7b32-4696-9088-97582b93114f'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Accounting', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Accountin', @equivalentId)

SET @equivalentId = 'b2ad8e79-bc9b-4c74-9aec-0a48abaf4bea'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('cpa', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('c.p.a.', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('certified practising accountant', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('certified accountant', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Australian Society of Certified Practising Accountants', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('CPA online', @equivalentId)

SET @equivalentId = '81d07e9f-48f4-41dc-9600-6cd7af6cb8e8'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('ca', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('c.a.', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('chartered accountant', @equivalentId)

SET @equivalentId = '56e041d0-55ab-4f1e-94ec-0f96030aa180'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Temporary Accountant', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Temp Accountant', @equivalentId)

SET @equivalentId = 'c3be4020-80f9-4e61-b261-2e165c6f1643'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Sr. Accountant', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Sr Accountant', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Senior Accountant', @equivalentId)

SET @equivalentId = '1b696413-2aae-42e8-98d6-c30da355a204'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Financial/Management Accountant', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Assistant Management Accountant', @equivalentId)

SET @equivalentId = '3f2a2e72-ded6-4d9e-9f8a-dca6460b5f94'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Junior Accountant', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Jr Accountant', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Jr. Accounts', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Junior Accounts Clerk', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('junior clerk', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('cadet accountant', @equivalentId)

SET @equivalentId = '15ebdc1c-258b-4fc8-81d8-ac422b9d1078'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Assistant Accountant', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Asst Accountant', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Accounts Assistant', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Acct Asst', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Accounting Assistant', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Account Assistant', @equivalentId)

SET @equivalentId = '94a2cab8-4fe0-486f-b330-5572df03103b'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Accounts Clerk', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Accounts Officer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Accounts Staff', @equivalentId)

SET @equivalentId = '9f69edfa-47fb-45f9-85b2-27a58bffddc2'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Project Accountant', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Proj Acct', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Project Management Accountant', @equivalentId)

SET @equivalentId = '6bb5f3ea-00c2-467f-821b-9cb9a3803870'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Auditor', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('auditing', @equivalentId)

SET @equivalentId = '6ff6df89-2d9b-450d-bf9f-974d13d28fde'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Audit Manager', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Audit Mgr', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('head of audit', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('audit director', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('audit partner', @equivalentId)

SET @equivalentId = 'f6981385-1b92-4d98-9d47-0d1354ccac38'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Senior Auditor', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Sr Auditor', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Sr. Auditor', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('snr auditor', @equivalentId)

SET @equivalentId = '8bdf0657-0a8e-4b10-a2f2-f8a5f506ad24'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Internal Auditor', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('In house Auditor', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('In-house Auditor', @equivalentId)

SET @equivalentId = '49d946f6-cd18-4cb3-9bfe-26a13806b4e6'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('External auditor', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Contract Auditor', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('financial auditor', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Revenue Auditor', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('State Auditor', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Statutory Accountant', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Senior Internal Auditor', @equivalentId)

SET @equivalentId = 'eb4d4966-f792-4560-bb6e-7195d490522d'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Junior Auditor', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Jr Auditor', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Jr. Auditor', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Apprentice Auditor', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('cadet auditor', @equivalentId)

SET @equivalentId = 'fc1c30e6-abc7-467a-b3f2-4d5e939f2431'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Quality Auditor', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('QA Auditor', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Quality Assurance Auditor', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('quality assessor', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('quality assurance officer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('quality assurance inspector', @equivalentId)

SET @equivalentId = 'c71740a8-1e92-4f00-85a0-d935581a31de'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('qas', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('quality assurance system', @equivalentId)

SET @equivalentId = 'a3b3e192-854c-4b89-9462-415e857a6ebe'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Operations Auditor', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Process Auditor', @equivalentId)

SET @equivalentId = '81e1cc34-b0fb-4074-9ec4-49343f004980'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Stock Auditor', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Night Auditor', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('stock counter', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('stocktake auditor', @equivalentId)

SET @equivalentId = 'b70f0c5f-a19e-4297-9e08-1d1b79dc41ae'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('stock controller', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('inventory controller', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('stockcontroller', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('stock supervisor', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('stock control', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('stock manager', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('inventory manager', @equivalentId)

SET @equivalentId = 'b8611f3e-cfbc-4590-a216-f9c2403c5c31'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('stock', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('inventory', @equivalentId)

SET @equivalentId = 'b7e40711-1349-4d11-a1f8-23a92dc4d913'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Bookkeeper', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('book keeper', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('bookeeper', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Accountant / Bookkeeper', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Receptionist/Cashier', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Assistant Office Manager', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Bookkeeper/Office Manager', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Office Manager/Bookkeeper', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Bookkeeper/Receptionist', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Receptionist/Bookkeeper', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Accounts Clerk/Receptionist', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Receptionist/Accounts Clerk', @equivalentId)

SET @equivalentId = '3c38c51a-aaa4-4104-bee3-16c0413734ac'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Accounts Payable Manager', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Accounts Payable Supervisor', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('head of accounts payable', @equivalentId)

SET @equivalentId = '79653b95-b3c3-4365-b090-44c788ef3a30'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Accounts Payable', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Accts Payable', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('AP', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Sales Ledger', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Accounts Payable Officer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Accounts Payable Clerk', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Accounts Payable Coordinator', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Accounts Payable Co-ordinator', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Accounts Payable Co ordinator', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Accounts Payable worker', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Accounts Payable person', @equivalentId)

SET @equivalentId = 'e8219a6c-3890-4629-bfe8-0e9eb88c6412'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Accounts Payable Assistant', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Assistant Accounts Payable', @equivalentId)

SET @equivalentId = 'a2c2949d-387e-43f1-8aad-08c48f082c17'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Accounts Payable/Receivable', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Accounts payable specialist', @equivalentId)

SET @equivalentId = '784433f7-5cd1-4352-a8a5-4d91b7af55af'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Reconciliation Clerk', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Reconciliation Officer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Reconciliation Assistant', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Reconciliation Asst', @equivalentId)

SET @equivalentId = '0f6adfcc-37f6-4f0c-9f48-a5a63ca38d76'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Accounts Receivable Manager', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Accounts Receivable Supervisor', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Collections Supervisor', @equivalentId)

SET @equivalentId = '59c732b7-25d9-4140-ae54-0eaa2c449821'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Accounts Receivable', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Accounts Recievable', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Accts Receivable', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Accounts Receivable Accounts Payable', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Accounts Receivable Clerk', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('AR', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Collections Manager', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Collections Mgr', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Collections Controller', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Collections Officer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Debt collections officer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Outbound Collections Officer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('credit collection advisors', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Collection Officer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Collection Controller', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Collections Clerk', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Collection Clerk', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Debt collection officer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Accounts Receivable officer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Accounts Receivable Assistant', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Assistant Accounts Receivable', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Accounts Receivable Coordinator', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Accounts Receivable Co-ordinator', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Accounts Receivable Co ordinator', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Accounts receivable specialist', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Credit Consultant', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Credit Administrator', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Credit Officer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Credit Controller', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Credit Control', @equivalentId)

SET @equivalentId = 'ecfae671-a18b-48cf-970a-8ec6f74214ef'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Mgr Credit Control', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Credit Control Manager', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Credit Control Mgr', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Manager Credit Control', @equivalentId)

SET @equivalentId = 'ad4f5e19-6fec-493c-a994-49995d501d73'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Finance Analyst', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Financial Analyst', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('financial management analyst', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('finance and investment analyst', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('investment analyst', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('analyst finance', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Finance Business Analyst', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Financial Business Analyst', @equivalentId)

SET @equivalentId = '8436091b-759b-481b-8570-8de874c9f04a'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Credit Analyst', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Credit Risk Analyst', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('credit management', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Credit cost Analyst', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('credit portfolio analyst', @equivalentId)

SET @equivalentId = 'da598e84-053e-45c8-a49d-501bd4644f07'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Senior Finance Analyst', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Sr Finance Analyst', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Sr. Finance Analyst', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Senior Financial Analyst', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Sr Financial Analyst', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Sr. Financial Analyst', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('snr financial analyst', @equivalentId)

SET @equivalentId = '6119d431-7a94-4a28-ad68-4df44a789d9e'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Senior Credit Analyst', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Sr Credit Analyst', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Sr. Credit Analyst', @equivalentId)

SET @equivalentId = '96e7334f-917d-4674-9a7e-356e0489767b'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Junior Finance Analyst', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Jr Finance Analyst', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Jr. Finance Analyst', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Junior Financial Analyst', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Jr Financial Analyst', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Jr. Financial Analyst', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('jnr financial analyst', @equivalentId)

SET @equivalentId = '791cb5f2-1862-4dcb-891c-8f05b69de0d6'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Junior Credit Analyst', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Jr Credit Analyst', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Jr. Credit Analyst', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('jnr credit analyst', @equivalentId)

SET @equivalentId = '514977f6-3ff0-427e-bdd4-0153444f6e7b'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Taxation Manager', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Taxation Mgr', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Tax Manager', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Tax Mgr', @equivalentId)

SET @equivalentId = '7001fb60-7ec5-4601-9221-7fac0bc5dd86'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('taxatiojn accountant', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Taxation Accountant', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('tax associate', @equivalentId)

SET @equivalentId = 'b88fd93f-1ebc-4265-aadf-fa75381fea2a'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('tax accountant', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('tax analyst', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('tax partner', @equivalentId)

SET @equivalentId = '590d69b6-6fdf-4d84-a552-cd3075e8d747'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('partner', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('equity partner', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('staff partner', @equivalentId)

SET @equivalentId = '22c8c7ac-0f1b-46fc-85de-3b31a3e44ae2'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Junior Tax Accountant', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Jr. Tax Accountant', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Jr Tax Accountant', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Junior Taxation Accountant', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Jr. Taxation Accountant', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Jr Taxation Accountant', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('jnr tax accountant', @equivalentId)

SET @equivalentId = '5bf32b2e-c52a-4ed3-8f52-9b89420cdb37'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Taxation Advisor', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Tax Advisor', @equivalentId)

SET @equivalentId = '3fbbc438-d56d-42ad-bbb4-001f0d9ad3e5'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Taxation analyst', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Tax Return Preparer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Tax Agent', @equivalentId)

SET @equivalentId = '2dc758e7-2f60-4cf8-bc40-eaebfbb68f40'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Senior Tax Accountant', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Sr. Tax Accountant', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Sr Tax Accountant', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Senior Taxation Accountant', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Sr. Taxation Accountant', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Sr Taxation Accountant', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('snr tax accountant', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('snr taxation accountant', @equivalentId)

SET @equivalentId = '66cf0482-0aad-423c-8443-f1e1a0b1d8fd'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Motor Claims', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Motor Claims Consultant', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('motor claims clerk', @equivalentId)

SET @equivalentId = '25248950-0d3a-4b21-9d88-d9ea25d2e2ca'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Injury Claims Consultant', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Workcover Claims', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('workers comp claims', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Workers Compensation Clerk', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('workers comp clerk', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Injury claims', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Workers Compensation Officer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Workers Compensation Administrator', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('WORKERS COMPENSATION CASE MANAGER', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('comp', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('compensation', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('compo', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('worker''s comp', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('workers comp', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('workers compensation', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('worker''s compensation', @equivalentId)

SET @equivalentId = 'dff0bd23-a9c0-4ed3-93ed-d50859c04a59'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Claims Administrator', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Claims Officer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Claims assessor', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Claims Assesor', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Claims Clerk', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Claims Consultant', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Claims Technician', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Claims Handler', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Claims Analyst', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Claims Manager', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Claims Mgr', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('National Claims Manager', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('claims processor', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('officer/claims assessor', @equivalentId)

SET @equivalentId = 'f49c36eb-9718-4631-9300-e96eaa753bcf'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Cost Accountant', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Cost Acct', @equivalentId)

SET @equivalentId = 'd637fdb3-74bc-4916-965d-e90706f92ef8'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Senior Purchasing Specialist', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Sr. Purchasing Specialist', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Sr Purchasing Specialist', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('snr purchasing specialist', @equivalentId)

SET @equivalentId = '8bc18428-0c1c-41f4-922a-61561cad888c'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Purchasing Manager', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Purchasing Mgr', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('procurement manager', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('head of procurement', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('procurement director', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('buying manager', @equivalentId)

SET @equivalentId = '041d17c7-b370-4530-9c0c-d48ed34b02b4'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('procurement', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('purchasing', @equivalentId)

SET @equivalentId = '8413cfbd-054d-4802-9c1e-f645a275be71'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Purchasing Clerk', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Purchasing Assistant', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Technical Purchasing Manager', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Purchasing Officer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Purchasing Administrator', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Purchasing Co-ordinator', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Purchasing Co ordinator', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Purchasing Coordinator', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Assistant Purchasing Officer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Purchasing Specialist', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('buying officer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('sourcing', @equivalentId)

SET @equivalentId = '2ab883f0-1858-4a69-880a-0b11f595d9ed'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Buyer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Senior  Buyer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Sr Buyer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Sr. Buyer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Regional Buyer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('National Buyer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('State Buyer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('snr buyer', @equivalentId)

SET @equivalentId = 'e9f3ec28-4a43-488b-bb64-dd5f5288e521'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Buying Assistant', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Assistant Buyer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Junior Buyer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Jr Buyer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Jr. Buyer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Trainee Buyer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('jnr buyer', @equivalentId)

SET @equivalentId = '59dc252f-d58a-4d86-82a8-2265b9f1c81b'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Mortgage Collections', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Mortgage Collection', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Mortgage Collections Officer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Mortgage Collections Clerk', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Mortgage Collection Clerk', @equivalentId)

SET @equivalentId = '7ff431c2-c6cc-4f51-b668-9bcd871c4171'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('online banking', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('electronic banking', @equivalentId)

SET @equivalentId = '60bedc37-9bc6-48d2-9be4-0b9e50a07d12'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Banking Manager', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Bank Manager', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('bank branch manager', @equivalentId)

SET @equivalentId = '8d6defca-0486-4154-a43b-3e5e9b01727c'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Branch Manager', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('branch head', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('branchmanager', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('branch supervisor', @equivalentId)

SET @equivalentId = '76322409-aa35-4420-b25e-ed2c038a6ee7'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Business Banking Manager', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Manager Business Banking', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('banking services manager', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('business banker', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('banker', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Retail Banking', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Banking Specialist', @equivalentId)

SET @equivalentId = 'f9f3524e-07dd-4878-a4d2-ed3cebc79e6a'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Banking Clerk', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Banking Consultant', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Banking Officer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('bank clerk', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('teller', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('bank teller', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Banking Representative', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Personal Banking Assistant', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('bankteller', @equivalentId)

SET @equivalentId = 'f17b0f16-69e8-44e1-b641-86593da76037'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Advisor', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Adviser', @equivalentId)

SET @equivalentId = '4530421c-c7b8-41a3-b028-09ed7fc9d378'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Insurance Manager', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Insurance Mgr', @equivalentId)

SET @equivalentId = '2e328379-41c8-4f95-a812-d1f3eb31689b'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Insurance Consultant', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Insurance Assistant', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Insurance Asst', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Insurance Clerk', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Insurance Administrator', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('insurance officer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Insurance Claim Assessor', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('insurance technician', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Insurance assessor', @equivalentId)

SET @equivalentId = '46e70186-97c7-411e-a2ff-8a3a91bb1cd7'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Jr. Insurance Administrator', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Jr Insurance Administrator', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Junior Insurance Administrator', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('jnr insurance administrator', @equivalentId)

SET @equivalentId = 'd9a440f2-112e-47a4-979f-470342e446d3'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Insurance Broker', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('General Insurance Broker', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Insurance Solutions Consultant', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('re-insurance broker', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('reinsurance broker', @equivalentId)

SET @equivalentId = '62128ed8-da0d-4ad7-aa64-38f2c13eb934'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Junior Insurance Broker', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Jr. Insurance Broker', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Jr Insurance Broker', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('jnr insurance broker', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('trainee insurance broker', @equivalentId)

SET @equivalentId = 'ee850069-fda7-468a-865b-46c067cf1e4b'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Chief Actuary', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('actuarial manager', @equivalentId)

SET @equivalentId = '7ff9a925-0c66-44c8-8993-7480e9e934a0'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Underwriting Manager', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('National Underwriting Manager', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Underwriting', @equivalentId)

SET @equivalentId = '45fce9ba-55aa-4794-9e66-a97247c93896'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Underwriting Executive', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Insurance Underwriter', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Assistant Underwriter', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('under writer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('re-insurance underwriter', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('reinsurance underwriter', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Underwriter', @equivalentId)

SET @equivalentId = 'aa552b66-ea31-4763-9444-2bb013fef9fc'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Chief Financial Planner', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('head of financial planning', @equivalentId)

SET @equivalentId = 'b22f6e36-5598-46b4-b71d-590780b681bf'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Financial planner', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Financial Planner Advisor', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('certified financial planner', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Financial Services Consultant', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Personal Financial Advisor', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('CFP', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('financial advisor', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('c f p', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('ps146', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('ps 146', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Regulatory Guide 146', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Policy Statement 146', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('RG146', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('rg 146', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Diploma Financial Services', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Diploma Financial planning', @equivalentId)

SET @equivalentId = '9f400451-7451-4002-b974-b7991f94f9e2'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Assistant Financial Planner', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Financial Planner Assistant', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Financial Planning Assistant', @equivalentId)

SET @equivalentId = '579eb1dd-1bd9-446e-9090-a082ac7ec85a'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Senior financial planner', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Sr. financial planner', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Sr financial planner', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('snr financial planner', @equivalentId)

SET @equivalentId = 'fe7dcba8-6b03-4371-ba1e-5d7ae84b299c'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Paraplanner', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Para planner', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Para-planner', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Para Planning', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Para-Planning', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Para-Financial Planner', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Para Financial Planner', @equivalentId)

SET @equivalentId = '894e4ed8-ca51-493e-858d-424ca46cf1c1'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Superannuation', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Super annuation', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Super-annuation', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('super fund', @equivalentId)

SET @equivalentId = '61846d7d-23d2-4d7d-b4e4-af453be4d8e8'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Superannuation Administrator', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Superannuation Administration', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Superannuation Service Consultant', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Superannuation assistant', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('FUND ADMINISTRATOR', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Superannuation Fund Administrator', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Fund Accountant', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Senior Fund Administrator', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Senior Superannuation Administrator', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Senior Funds Administrator', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Corporate Superannuation', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Corporate Superannuation Advisor', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('super administrator', @equivalentId)

SET @equivalentId = '9b38f0d3-d4dd-48a0-8403-0a1f14f9e9bb'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Stockbroker', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Stock broker', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Stock broking', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Stockbroking', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Stockbrokers', @equivalentId)

SET @equivalentId = '965578c2-82f5-4392-90eb-19683c7adeb6'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('cashflow', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('cash flow', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('cash-flow', @equivalentId)

SET @equivalentId = '16b20e6a-4555-4a3e-81bf-8e8fab954c57'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('CBA', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Commonwealth Bank of Australia', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('comm bank', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('commbank', @equivalentId)

SET @equivalentId = 'b0fc1f46-f8b7-447c-828d-fa7f08f0a88f'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('GDP', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('gross domestic product', @equivalentId)

SET @equivalentId = 'a07b720c-3484-4133-a484-d6448af25261'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('GNP', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('gross national product', @equivalentId)

SET @equivalentId = '93a8fe8b-9b8a-4459-b58a-0431b2c8cb37'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('quick books', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('quickbooks', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('quick-books', @equivalentId)

SET @equivalentId = '5de379b1-37b3-47d4-a52b-a40ca5bb6476'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('tax', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('taxation', @equivalentId)

SET @equivalentId = '2748b9cd-8120-457a-a3db-d0db2a1c0698'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Health Insurance Candidate', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Health insurance officer', @equivalentId)

SET @equivalentId = '92ad228c-ca57-49bd-af66-815fca8c73da'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('recruitment', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('onboarding', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('on boarding', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('on-boarding', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('candidate care', @equivalentId)

SET @equivalentId = 'aebf05da-0054-48dd-bdb6-26d5d402b2d4'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Recruitment Manager', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Onsite Recruitment Manager', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('National Recruitment Solutions Manager', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Regional Recruitment Manager', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('recruitment director', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('head of recruitment', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('recruitment dierctor', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Recruitment Team Leader', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Group Recruitment Manager', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Senior Recruitment Consultant', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Business Recruitment Team Leader', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Icon Recruitment and Ajilon Consulting ACT Manager', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('National Recruitment Manager', @equivalentId)

SET @equivalentId = '2ce4ac1f-0e5f-4f1d-a169-0c0959488f33'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Recruitment Consultant', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Executive Recruitment Consultant', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Recruitment Consultant - Executive', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Recruitment Specialist', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Recruitment Services Officer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Recruitment Advisor', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Recruitment Coordinator', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Recruitment Officer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Student Recruiter', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Defence Recruiter', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('HR Recruiter', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('H.R. Recruiter', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Recruitment Consultant / Team Leader', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('recruiter', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Recruitment Co-ordinator', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Assistant Recruitment Consultant', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('HR Recruitment Asst', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('HR and Recruitment Coordinator', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('recruitment concultant', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('end-to-end recruitment', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Contract Recruitment Consultant', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('/Recruitment Officer', @equivalentId)

SET @equivalentId = 'f9847153-7ef9-46a1-85a1-51bb32872419'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Candidate Resourcer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('candidate research', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('resourcher', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('recruitment researcher', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('candidate sourcing resourcer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('candidate sourcing researcher', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('candidate sourcing', @equivalentId)

SET @equivalentId = '9d22940e-19f7-47e9-bcb4-712edd75fc76'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Recruitment Administrator', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Recruitment Administration Officer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('admin assistant recruitment', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('recruitment administrative assistant', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('administrative assistant - recruitment', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('DBS / RECRUITMENT ADMINSTRATION / CUSTOMER SERVICE', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Recruitment Support Officer', @equivalentId)

SET @equivalentId = 'beb5ea66-7b04-4d02-a85c-266ffe3f3f81'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('recruitment recourcer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('recruitment resourcer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('candidate sourcer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('cnadidate researcher', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Recruitment Administrator Resourcer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Resourcer Researcher', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('it resourcer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('resourcer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('recruiting resourcer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('EA to the Director / Resourcer / Recruitment Consultant', @equivalentId)

SET @equivalentId = 'bacf7839-87c2-471f-85ec-5ab739f1c248'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('cv', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('curriculum vitae', @equivalentId)

SET @equivalentId = 'd1aff6d1-0038-4f3e-9c8d-fca14b8c7776'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('rec2rec', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('recruiter to recruiter', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('rec-to-rec', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('rec to rec', @equivalentId)

SET @equivalentId = 'ba1182f1-4d56-4e8f-b913-36a2e703bf12'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('career advisor', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('career cousellor', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('career coach', @equivalentId)

SET @equivalentId = '5878d60d-7161-4c83-955e-0847cbe767d4'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Onsite Recruitment Consultant', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Internal Recruitment Consultant Human Resource Team ongoing consulting role', @equivalentId)

SET @equivalentId = '12f966dc-d835-424f-b5a1-2bb3b7b20102'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('HR', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('H.R.', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Human Resources', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Human Resource', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('people and culture', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('human capital', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('personnel', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('h r', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('talent management', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('people & culture', @equivalentId)

SET @equivalentId = '8a720fb8-f19f-40b1-8eb2-29f72a4bf8e8'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('HR Manager', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('H.R. Manager', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Human Resources Manager', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Manager HR', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Senior Manager HR', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('HR General Manager', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('General Manager HR', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('General Manager - HR', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('vp hr', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('vice president human resources', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('director hr', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('human resources director', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('National HR Manager', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('National H.R. Manager', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('National Human Resources Manager', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Divisional HR Manager', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Divisional H.R. Manager', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Divisional Human Resources Manager', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Group HR Manager', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Group H.R. Manager', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Group Human Resources Manager', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Assistant Manager HR', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Assistant Manager Human Resources', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('General Manager Human Resources', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('director human resources', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('hr director', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('hrd', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('manager talent', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('head of talent', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('manager people and culture', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('general manager people and culture', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('chief human resources officer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Department Head - HR', @equivalentId)

SET @equivalentId = '15d18270-3ace-4f2e-a5b1-6959cd245a37'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('employment services manager', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('employment services operations manager', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('general manager employment', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('general manager employment services', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('gm employment', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('gm employment services', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('employment solutions manager', @equivalentId)

SET @equivalentId = '0beb6246-0b47-4cf0-a9a7-a55707e18fb5'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Senior HR Advisor', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Senior Human Resources Advisor', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('snr hr advisor', @equivalentId)

SET @equivalentId = 'ac1e28c8-7419-4d68-80e2-860dd9e95edb'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Senior Human Resources Consultant', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Senior Human Resources Officer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Senior HR Consultant', @equivalentId)

SET @equivalentId = 'b1f97655-deeb-48ce-9748-1447e9b60b26'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('HR Coordinator', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('HR Co ordinator', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('HR Co-ordinator', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('HR Officer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('H.R. Officer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Human Resources Officer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('General HR', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Personnel Officer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Personnel Services Officer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Human Resources Co-ordinator', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Human Resources Coordinator', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Human Resources Assistant', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('HR Assistant', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('H.R. Assistant', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Human Resources Administrator', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('HR Administrator', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('H.R. Administrator', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('talent coordinator', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('talent co-ordinator', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('hr consultant', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('human resources consultant', @equivalentId)

SET @equivalentId = 'c15b812e-f1e2-4924-a099-dabd91d0a552'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Human Resources Analyst', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('HR ANALYST', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('human resource analyst', @equivalentId)

SET @equivalentId = '143f9723-6705-4eb5-b14d-bcf4a941c0c1'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('HR Executive', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Human Resources Executive', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('human resources exec', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Human Resources Generalist', @equivalentId)

SET @equivalentId = '82119305-a2d0-4d9f-b93c-b7cc0334ee63'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('HR Payroll', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('HR/Payroll', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('H.R. Payroll', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('human resources/payroll', @equivalentId)

SET @equivalentId = '1adf5575-dcb1-440d-a545-026a808a32b2'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Payroll Officer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Finance - Payroll Officer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Payroll Clerk', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Payroll Consultant', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Payroll Administrator', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Personnel Payroll Officer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Payroll Admin', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('pay roll officer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Payroll Specialist', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Payroll Assistant', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Payroll Asst', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Payroll Co-Ordinator', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Payroll Coordinator', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Payroll Co ordinator', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Payroll Accountant', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('payroll person', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('payroll function', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('pay roll function', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('pay-roll function', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Payroll Officer - Full time', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Payroll Excecutive', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Payroll and Admin', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Clerical Officer Payroll', @equivalentId)

SET @equivalentId = '389c0af3-5d0a-4193-b431-f117dd686781'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Temporary Payroll Officer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Temp Payroll', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Payroll Temp', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('payroll officer - temps', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('payroll officer - perm', @equivalentId)

SET @equivalentId = '1a3f07ef-f531-4758-9409-72479a8a295c'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Senior Payroll Officer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Payroll Supervisor', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('payroll manager', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('head of payroll', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Group Payroll Manager', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Payroll Manager / Payroll Team Leader', @equivalentId)

SET @equivalentId = 'f0fe9dbd-8eab-44b8-9597-2b4f4407cea0'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Store Manager', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Shop Manager', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Store Supervisor', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Store Manger', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Weekend Store Manager', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Retail Store Manager', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('retail outlet manager', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('retail outlet supervisor', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('National Retail Manager', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Manager - Retail', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Retail Manager', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Retail Business Manager', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Retail Operations Manager', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Retail Operations', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('front of house manager', @equivalentId)

SET @equivalentId = 'a490a288-1a0b-4f7f-8058-da57c40a7c8c'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Retail Supervisor', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Retail Team Leader', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Retail Store Team Leader', @equivalentId)

SET @equivalentId = '1ceb5fae-8f17-47f1-8197-a95d1a6a98c1'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Shop assistant', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Retail Sales', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Retail assistant', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Retail Sales Representative', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Retail/Sales', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Retail Sales Consultant', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Retail Sales Officer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Retail Customer Service', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Retail Attendant', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Retail Sales  Assistant', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Retail Customer Service Assistant', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Retail Salesperson', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Retail Sales person', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Customer Service/Retail Sales', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('sales assistant', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Sales Ass', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('sales assitant', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('SALES ASSISSTANT', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Sales Assistant.', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Retail Sales Assistant', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Customer Sales Assistant', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('sales asst', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('checkout chick', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('check out chick', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Retail assistant Customer service', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Casual Retail Assistant', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Casual Retail Sales', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Retail-shop assistant', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Retail Shop Assistant', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Customer Service/ Retail', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('RETAIL/CUSTOMER SERVICE ASSISTANT', @equivalentId)

SET @equivalentId = '11c26cbb-50e6-42f3-a862-0e0198f56f7f'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('LinkMe', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('owner of this intellectual property', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('link me', @equivalentId)

SET @equivalentId = 'a7598e9f-4e1e-4e1b-bdbf-c5e77f9585eb'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Hotel Manager', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Hotel Duty Manager', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Hotel general manager', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('hotel supervisor', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('pub manager', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('pub duty manager', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('hotel operations manager', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('hotel night manager', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('venue manager', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('venue supervisor', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('duty manager', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('functions manager', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('functions coordinator', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('functions co ordinator', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('functions co-ordinator', @equivalentId)

SET @equivalentId = 'f5684362-eaec-4f73-adf4-5b63ca7c9a2a'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Restaurant manager', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Restaurant General Manager', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Bistro manager', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Restaurant Supervisor', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('restaurant duty manager', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('restaurant and bar manager', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('restaurant owner', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('restaurant mgr', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('banquet coordinator', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('banquet manager', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('banquet supervisor', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('banquet co ordinator', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('banquet co-ordinator', @equivalentId)

SET @equivalentId = '6b6d221a-7f0a-45f8-bbd7-cc47f1c4f211'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('duty free', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('dutyfree', @equivalentId)

SET @equivalentId = '6567ae57-64f3-410a-9b8f-a6c4a398149e'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Floor Manager', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Floor Supervisor', @equivalentId)

SET @equivalentId = '931515cf-9edc-47b2-9aca-cccdd2e84f47'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Bar Manager', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Bar Supervisor', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('bar owner', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('head barman', @equivalentId)

SET @equivalentId = 'ef5e1ce2-13cd-48d5-8fc7-e29f1f5d7d95'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Casino Manager', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('casino mgr', @equivalentId)

SET @equivalentId = '60c6f770-bd1f-4328-ab59-8bf9b284c29b'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('food and beverage', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('foodandbeverage', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('food & beverage', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('food&beverage', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('food service manager', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('f and b', @equivalentId)

SET @equivalentId = '4c6841ed-deb5-4551-a409-8becd03369f8'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Bar Attendant', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Bar tender', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('barman', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('bar man', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Bar staff', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('bar attendant/waitress', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Bartender', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Bar Person', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Bar Assistant', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Bar Service', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Bar', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Bar work', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('General bar', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Bar Staff/Waitress', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('BAR ATTENDANT/WAITPERSON', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('BarPerson', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('barperson/waitress', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Bar Waitress', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Casual Bar Staff', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Bartender/Waitress', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Casual Bar Attendant', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Bar Attendant Customer service', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Barman/Waiter', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Waiter/Bar attendant', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Waitress/ Bar Attendant', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Waitress/Bar Staff', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Waitress / Bar Attendant', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Waiter/Cashier', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('food and beverage attendant', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('f&b attendant', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('function staff', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Senior Bartender', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Bar Attendant/Supervisor', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Bar Attendant/Manager', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('café assistant', @equivalentId)

SET @equivalentId = '985b59ff-8eb3-4ebc-bc27-90a6977958f5'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('barrista', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('barista', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('coffee maker', @equivalentId)

SET @equivalentId = 'b1c71024-fd5b-4374-9d83-785e989bf16b'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Head Cashier', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('chief cashier', @equivalentId)

SET @equivalentId = '7c4c01ec-db78-41d1-a66c-8a802a15c73e'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Junior Cashier', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('jnr cashier', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('jr cashier', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('trainee cashier', @equivalentId)

SET @equivalentId = '546e712f-f1be-4db2-82a3-807773ca24b1'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Console Operator Customer Service', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Checkout Operator Customer service', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Cashier/Customer Service', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Cashier Customer Service', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Customer Service/Cashier', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Customer Service Cashier', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Cashier', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Cahier', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Casual Cashier', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Service Cashier', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Cashier operator', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Customer Service / Cashier', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Casual Service Cashier', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Cashier/Greeter', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Customer Service Officer/Cashier', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Cashier/Shop Assistant', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Checkout Operator', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('CUSTOMER SERVICE / CHECKOUT OPERATOR', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Checkout Operator Front end Controller', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Casual Checkout Operator', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Console Operator', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Casual Console Operator', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('cashier/supervisor', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('part time cashier', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('part-time cashier', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('p/t cashier', @equivalentId)

SET @equivalentId = 'ebf6d3a4-2077-4567-843f-25bcc79e8be8'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Gaming Manager', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Gaming Mgr', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('gambling manager', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('gambling mgr', @equivalentId)

SET @equivalentId = 'ebaff0a3-df1d-40ca-b3f2-3fa9d7244725'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Gaming Attendant', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Gaming staff', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Bar/Gaming Attendant', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Gaming Bar Attendant', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Gaming/Bar Attendant', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('gaming machine attendant', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('room attendant', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('dealer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('card dealer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('croupier', @equivalentId)

SET @equivalentId = '01750fa4-7440-4d81-8d50-779d8e8e38d4'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Head Waiter', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Head Waitress', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Wait Supervisor', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Maitre De', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Senior Waiter', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('front of house', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('maitred', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('maitre d', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('maitrede', @equivalentId)

SET @equivalentId = '5a3213f4-8cf4-4ce3-8dc7-343c3722adef'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Waiter', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Waitress', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Wait Staff', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Waitress Customer service', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Waitress/ Kitchen Hand', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Waitress/kitchenhand', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Waitress / Kitchen Hand', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Waiter/kitchen hand', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Waiter / kitchen hand', @equivalentId)

SET @equivalentId = 'c8ce6540-0ed9-4712-ac28-cff11b6b7e02'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Kitchen Manager', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Head Cook', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Head Chef', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Chef Manager', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Head Pastry Chef', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Senior Chef', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('number one chef', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('first chef', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('1st chef', @equivalentId)

SET @equivalentId = 'a8eac8ce-c4d6-456c-b02d-5c83a0e7929d'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Second Chef', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('2nd Chef', @equivalentId)

SET @equivalentId = '5515e766-6719-4fb0-be3f-516739a6f530'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Chef De Partie', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Sous Chef', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Executive Chef', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Commis Chef', @equivalentId)

SET @equivalentId = '574d1fff-fb2e-4f61-ac15-6edd7331e972'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Assistant Chef', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Chefs Assistant', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Chef assistant', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('chef''s assistant', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Chef''s assist', @equivalentId)

SET @equivalentId = 'ca72186b-de03-43dd-a8f4-e63d59109c1c'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('chef', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('che', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('shef', @equivalentId)

SET @equivalentId = '950184ca-0b3f-4e1f-af86-5c6f284e4e8d'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Executive Pastry Chef', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Pastry Chef', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('pastry cook', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Senior pastry chef', @equivalentId)

SET @equivalentId = 'f25e1efb-45e4-48e5-8efd-c1047ba4a9cd'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Apprentice Chef', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('2nd year Apprentice Chef', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('1st Year Apprentice Chef', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('4th Year Apprentice Chef', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Trainee chef', @equivalentId)

SET @equivalentId = '5087d5fb-691e-4142-82db-c795c3b2a9b2'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Cook/Chef', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Relief Chef', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Kitchen Hand/Cook', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Cook/Kitchen Hand', @equivalentId)

SET @equivalentId = '46e40c47-389f-4c8a-bb35-0104aa6e2115'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('kitchen hand', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('kitchenhand', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Kitchen-hand', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Kitchen Worker', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Kitchen Hand/Waiter', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Kitchen hand/waitress', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('casual kitchen hand', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Kitchen hand Customer service', @equivalentId)

SET @equivalentId = 'e47e40fc-9089-4798-bb6c-3aa751087fed'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Delicatessen Manager', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Deli Manager', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Service Deli Manager', @equivalentId)

SET @equivalentId = 'c2250708-f652-4d46-980a-166c50a64311'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Deli Assistant', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Service Deli Assistant', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Serviced Deli Assistant', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('DELICATESSEN ASSISTANT', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Deli', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Delicatessen', @equivalentId)

SET @equivalentId = 'de9139d3-c098-4780-a721-e4694e7fbf24'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Food Processor', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Food Processing', @equivalentId)

SET @equivalentId = 'a911f949-9c70-4b23-bd71-4f2eef0c4e3c'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('dish washer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('dishwasher', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('kitchen assistant', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('sandwich hand', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('sandwichhand', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('pot scrubber', @equivalentId)

SET @equivalentId = '171391ac-131b-41f6-a3a8-d4a5a48c4d3d'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Retail Business Consultant', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Consultant - Retail Business', @equivalentId)

SET @equivalentId = 'ca3ff663-5187-4e7a-9d83-14fb29cc17fc'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Pricing Coordinator', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Pricing Co ordinator', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Pricing Co-ordinator', @equivalentId)

SET @equivalentId = 'af0e7b3c-b92d-411c-9954-a6244066a274'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Call Centre Manager', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Call Centre Team Manager', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Call Centre Supervisor', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Telemarketing Manager', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Telemarketing Team Leader', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('contact centre manager', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('call centre mgr', @equivalentId)

SET @equivalentId = 'b0708f5e-22e9-4f5b-9f43-fb5a41d9d213'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Customer Service Manager', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Customer Services Manager', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Customer Service Supervisor', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Customer Service Team Leader', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('customer care manager', @equivalentId)

SET @equivalentId = 'e0227a61-74ce-470e-9ed1-55e93b6290ce'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Customer Service Officer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Customer Services Officer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('csr', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Customer Service Representative', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Customer Service Rep', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Customer Service Rep.', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Customer Services Representative', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Customer Service Clerk', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Customer Service Assistant', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Customer Service Consultant', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Customer Service Operator', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Customer Service Attendant', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Customer Service Executive', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Customer Service Team Member', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Customer Services', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Customer Services Assistant', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Customer Service Customer Service', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Customer Service Professional', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('CUSTOMER SERVICE AGENT', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Sales/Customer service', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Customer Service/Sales', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Customer Service Specialist', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Receptionist/Customer Service', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Customer Service/Receptionist', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Customer Service Co-ordinator', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Customer Service Co ordinator', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('CUSTOMER SERVICE COORDINATOR', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('csa', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('call centre operator', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('customer care operator', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('telemarketer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('outbound telemarketer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('client services consultant', @equivalentId)

SET @equivalentId = 'f629f338-ecb0-48f4-aa2b-f3b5fa188696'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('customer service', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('customer care', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('client service', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('client care', @equivalentId)

SET @equivalentId = 'b4ab67f1-6d21-497e-ad32-baa38d52bfa0'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('call centre', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('call center', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('contact centre', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('callcentre', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('callcenter', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('contactcentre', @equivalentId)

SET @equivalentId = '175ade81-cc4a-4aa8-a717-ad1910d01110'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('cold call', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('cold calling', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('outbound', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('out bound', @equivalentId)

SET @equivalentId = '61a6c5da-b313-4d4b-a892-bfd07f5eedaf'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('receptionist', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('receptionis', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Receptionist/Secretary', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Secretary/Receptionist', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Secretary / Receptionist', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Secretary/ Receptionist', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('General receptionist', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Receptionist / Administration', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Receptionist/Administration', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Receptionist/Administration Assistant', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Receptionist/clerk', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Receptionist/Administrator', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Receptionist/Admin Assistant', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Receptionist / Administration Assistant', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Receptionist/Telephonist', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Receptionist/Admin', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('ADMINISTRATION ASSISTANT/RECEPTIONIST', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Administrative assistant/Receptionist', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Receptionist/Administration Support', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Receptionist/Administrative Assistant', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Receptionist / Assistant', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Administration Officer / Receptionist', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Administrator/Receptionist', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('administration officer/receptionist', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Reception/Administration', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Reception/Admin', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Receptionist/Office Assistant', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Receptionist/Office Clerk', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Receptionist/Office Administrator', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Office Assistant/Receptionist', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Receptionist / Office Assistant', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('RECEPTIONIST/OFFICE ADMINISTRATION', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Receptionist/Office Manager', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Office Manager/Receptionist', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('reception', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Receptionist/Personal Assistant', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('receptionist / personal assistant', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('PERSONAL ASSISTANT/RECEPTIONIST', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Receptionist/PA', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Telephonist', @equivalentId)

SET @equivalentId = 'ca38ee76-e447-4938-a2c4-1ff71a05bfde'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Junior Receptionist', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Jr Receptionist', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Trainee Receptionist', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Assistant Receptionist', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Reception Assistant', @equivalentId)

SET @equivalentId = '0513dc42-827c-406a-a59b-cf07ade574b8'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Junior Secretary', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Jr Secretary', @equivalentId)

SET @equivalentId = 'ea9c4f08-9dc6-4015-a3ec-d211dbe4f587'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Front Office Receptionist', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Receptionist Front Office', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Front Desk Receptionist', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Frontline Receptionist/Office All-rounder', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('front office reception', @equivalentId)

SET @equivalentId = '7266bb37-5423-4e00-92cc-e2130c92e20a'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Front Office Manager', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Front Office Supervisor', @equivalentId)

SET @equivalentId = '18b5d6ae-0566-48a8-a902-f4674abec200'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Office manager', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Office Supervisor', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Office Manager/Personal Assistant', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Office Manager/PA', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Office Manager / Executive Assistant', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('office manager/ea', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Office Mgr', @equivalentId)

SET @equivalentId = '2e683d18-5957-4dc1-b0fb-cbbd89fced84'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Office Assistant', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Office Asst', @equivalentId)

SET @equivalentId = 'f5324715-fd10-4f85-8956-ea7891426cd3'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Bilingual Receptionist', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('bi lingual receptionist', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('bi-lingual receptionist', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('bi-linqual telephonist', @equivalentId)

SET @equivalentId = 'dc28f042-f746-42ab-aa40-1ebd08f9a33e'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('personal assistant', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('pa', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('p.a', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('p.a.', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('PERSONAL ASSISTANT TO MANAGER', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Acting Personal Assistant', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Personal Assistant/Office Administrator', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Executive Secretary', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Executive Assistant', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('ea', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('e.a', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('e.a.', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Executive Personal Assistant', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Personal Secretary', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Personal Assistant/Secretary', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Secretary / Personal Assistant', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Personal Assistant / Secretary', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Secretary/Personal Assistant', @equivalentId)

SET @equivalentId = 'a5f5a1b6-e863-427b-bebc-7acfd352964b'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Receptionist/Property Management', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Receptionist / Assistant Property Manager', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('property manager', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('strata manager', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('portfolio property manager', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('rental manager', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('realestate manager', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('real estate manager', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('asset manager', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('caretaker', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('care taker', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('caretaker/manager', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('caretaker/maintenance manager', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('maintenance co-ordinator', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('maintenance co ordinator', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('maintenance coordinator', @equivalentId)

SET @equivalentId = '941edc1f-aba4-4f86-91e5-1140a8be9b69'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('facility manager', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('facilities manager', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Facilities Officer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Facilities coordinator', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Facilities Maintenance Manager', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Manager Facilities', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Facilities Management', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Facilities management officer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Facilities Services Manager', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Facilities co-ordinator', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('facility mgr', @equivalentId)

SET @equivalentId = '284f38b0-e451-441b-ab0f-02ed1f111eb6'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Temporary Receptionist/Secretary', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Temporary Receptionist', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Casual Receptionist', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Relief Receptionist', @equivalentId)

SET @equivalentId = '4d0c492a-e50d-4728-a95d-179ad3f0bba2'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Administration Supervisor', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Admin Supervisor', @equivalentId)

SET @equivalentId = 'ddc89342-747b-49f7-9fcb-9914a8240363'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Administration Officer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Admin Officer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Admin Clerk', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Administration Clerk', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Administrative Officer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Administration/Clerical', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Clerical Assistant', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Clerical Officer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Clerical', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('General Clerical', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Clerical Administrator', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Clerical Officer 1/2', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Administration/Customer Service', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Admin Assistant', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Admin Asst', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Administration Assistant', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Administrative Assistant', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Assistant Administrator', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Office Administration Assistant', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Office Administrator', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Office Admin', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('General office administration', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Administration Office', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Office Administration', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Office Clerk', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('admin person', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('administration person', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('general administration', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('administration duties', @equivalentId)

SET @equivalentId = 'b46d6ea9-51ec-4a41-9bbc-c5a2aa3003ae'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('admin', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('administration', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('administrative', @equivalentId)

SET @equivalentId = '99813ba8-e18f-4c4f-8e94-7c897aa038a6'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Administrative Support Officer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Administrative Support', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Administration Support', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Admin support', @equivalentId)

SET @equivalentId = '58707e8f-c90c-494b-b012-02459eeaa4be'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Temporary Clerical Assistant', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Temporary Administrative Officer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Relieving Administrative Officer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Temporary Admin Officer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Temp Admin Officer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Temp Administration Officer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('temporary administration assistant', @equivalentId)

SET @equivalentId = 'c7fb6a7f-d300-4857-b39a-379bd27caf2c'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Junior Clerical Assistant', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Clerical Trainee', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Clerical Traineeship', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Trainee Clerical Assistant', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Office Junior', @equivalentId)

SET @equivalentId = '12044b10-9b72-42bf-995d-c3117d575c17'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Admin Trainee', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Office Administration Trainee', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Junior office administration', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Administration Trainee', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Junior Administration Assistant', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('junior admin', @equivalentId)

SET @equivalentId = 'c23fadf2-e2d1-4842-9245-3e1d0dcd24d1'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('copy writer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('copywriter', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('business writer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('content writer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('freelance writer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('copywriting', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('copy writing', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('content writing', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('online writer', @equivalentId)

SET @equivalentId = '45b6ffff-de3c-4706-bd3f-ff5dd3750ad2'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Data Entry Officer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('data entry', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Data Entry Clerk', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Data Entry Operator', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Administrator/Data Entry Clerk', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Data Entry Officer - Accounts', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Data Entry Supervisor', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('WP Operator', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Word Process Operator', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('WP', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('WPO', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('typist', @equivalentId)

SET @equivalentId = '1ed241ca-1cc4-4037-95ea-b47a5f726f61'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('records clerk', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('records officer', @equivalentId)

SET @equivalentId = 'c2f91507-a533-43cf-8c49-567e7c59c839'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Myob trainer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('MYOB Program Support Consultant', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('MYOB Certified Consultant & Trainer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Bookkeeper/Training Myob', @equivalentId)

SET @equivalentId = '4f4dc1d5-2515-4ab3-9f60-4446608ebb2b'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Data entry with MYOB', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('MYOB/Office Administration & Website setup', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('MYOB Specialist', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('MYOB Consultant', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('MYOB data entry', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Worked in MYOB', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('MYOB Accounting', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('MYOB Bookkeeper', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('MYOB Data Entry operator', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('MYOB accounts', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Accounts Assistant Casual MYOB', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Bookkeeper/MYOB Expert', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('ACCOUNTANT-MYOB', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('MYOB Bookkeeping Consultant', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Bookkeeping /Admin in MYOB', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Bookkeeper/Internal Accountant MYOB/EXCEL', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Qualified MYOB Bookkeeper', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Certified MYOB Consultant', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Assistant Bookkeeper MYOB', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Bookkeeper using MYOB', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('MYOB/ACCOUNTS ASSISTANT', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('MYOB qualified', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Bookkeeping / Admin in MYOB', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Bookkeeping & Admin in MYOB', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Consultant MYOB Bookkeeping', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('MYOB professional', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('MYOB officer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('MYOB admin officer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Admin officer in MYOB', @equivalentId)

SET @equivalentId = '59edb5f0-4cd8-42bb-9fba-ae98276fd2e3'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Myob Australia Major Accounts Manager/ Sales Representative', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('MYOB Program Support Consultant/ SME Accounting & Technical Support Representative', @equivalentId)

SET @equivalentId = '2a87845b-9a4f-4a74-8182-3baa24dd69a1'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('PA/Admin/Rec/MYOB', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('RECEPTION/MYOB', @equivalentId)

SET @equivalentId = '5188613b-a5a3-48e1-a0f0-c99c69250783'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Architectural Design', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Project Architect', @equivalentId)

SET @equivalentId = '52d1d4dc-d972-4988-afd4-2819bfd7a05f'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Senior Architect', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Senior Design Architect', @equivalentId)

SET @equivalentId = '9da9d926-b4b9-49ab-9fa8-86c47c7cfde5'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Assistant Architect', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Architectural Assistant', @equivalentId)

SET @equivalentId = '84631cfd-3ff2-4b37-a01d-c3b42fc8e56a'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Graduate Architect', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Architectural Trainee', @equivalentId)

SET @equivalentId = 'd807ca4f-4d0b-41d7-b9a4-e0dfdfbdbb96'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Drafter', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Draftsman', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Draftsperson', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Architectural Draftsperson', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('draft person', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('drafts person', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('drafts man', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('drafting officer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('drafting person', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('drafting work', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Drafts men', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Draftsmen', @equivalentId)

SET @equivalentId = 'f1171c5a-bda7-4acb-ba71-15459a676ff5'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('computer aided design', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Autocad', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Auto CAD', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('cad', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('autocad2000', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('autocad 2000', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('cad design', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('cad designer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('cad designing', @equivalentId)

SET @equivalentId = '68cc02a5-c47c-43b4-a89f-5f9a4412f939'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Senior Draftsman', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Senior Design Structural Draftsman', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('lead draftsman', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('drafting manager', @equivalentId)

SET @equivalentId = 'bf05bc08-2094-4e0c-b884-0b862dfe66cc'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('interior architect', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('interior architect/designer', @equivalentId)

SET @equivalentId = '0d2519f2-233d-4593-91a9-a1e97ce929f4'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Structural Draftsperson', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('structural drafter', @equivalentId)

SET @equivalentId = '42aa88aa-5766-4748-88a9-cb0f67600b5d'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Labourer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('manual labourer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('general labourer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('t/a', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('skilled labourer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('construction labourer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Trades Assistant', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Trade Assistant', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('ta', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('general trades assistant', @equivalentId)

SET @equivalentId = '2d2c6bdd-a332-4652-8899-db472b80ab58'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('tradesman', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('tradesperson', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('trades man', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('trades person', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('tradie', @equivalentId)

SET @equivalentId = 'eb567178-e6d7-4edb-8dbd-5e484050b8dc'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('plasterer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('solid plasterer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('fibrous plasterer', @equivalentId)

SET @equivalentId = '6e2bfd9e-c231-437d-9dda-3a6d0a626538'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('scaffolder', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('scaffolding coordinator', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('scaffolding labourer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('scaffolding', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('scaffolding co-ordinator', @equivalentId)

SET @equivalentId = '7e02392a-5cf7-4f1b-b4f0-7f49ca168eef'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Mechanic Manager', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Workshop Manager', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('MECHANICAL SUPERVISOR', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Mechanical Manager', @equivalentId)

SET @equivalentId = '36e45bc5-480d-4956-a66e-514cb354aed5'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('boilermaker', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Boilermaker/Welder', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Welder', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('boiler maker', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('welding', @equivalentId)

SET @equivalentId = '17b34744-a081-446c-8289-2c028628fa17'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Fitter', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Fitter/Welder', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Mechanical Fitter', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('fitter and turner', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('fitter & turner', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('maintenance fitter', @equivalentId)

SET @equivalentId = '1d7aa01a-d718-495a-ac28-c01ffe3cf47d'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('hdpe', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('High-density polyethylene', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Hi density polyethylene', @equivalentId)

SET @equivalentId = '4c61effe-e63c-4c4a-9fc4-c27dbde24aa8'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('pvc', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Polyvinylechloride', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Poly vinyle chloride', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Poly vinyl chloride', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Polyvinylchloride', @equivalentId)

SET @equivalentId = '222c645d-6ae4-4eac-b44a-c460e695bab7'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('mechanic', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Service Mechanic', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Motor Mechanic', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('automotive mechanic', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('auto mechanic', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('car mechanic', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('vehicle mechanic', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Automotive Eng', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('automotive engineer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('automotive technicican', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('automotive service engineer', @equivalentId)

SET @equivalentId = 'df6491d6-c40b-4e21-953b-9eafecd960ba'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('car washer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('vehicle washer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('car detailer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('carwash', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('car wash', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('carwash operator', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('car wash attendant', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('carwash attendant', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('car wash operator', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('car cleaner', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Cardetailer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Car detailing', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Detailer', @equivalentId)

SET @equivalentId = 'a52dcdfa-54ed-4545-96d1-82703a970bb5'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('suspension fitter', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('suspension mechanic', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('suspension tech', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('suspension technician', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('suspension specialist', @equivalentId)

SET @equivalentId = '15c7c93f-aeeb-4b5e-b088-8a6f6273746a'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('windscreen', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('wind screen', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('auto glass', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('automotive glass', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('windscreen replacer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('windscreen fitter', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('windscreen repairer', @equivalentId)

SET @equivalentId = 'dd7ca94e-cccf-4b96-b10b-8ee3e41bbb7e'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Assistant mechanic', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Trainee mechanic', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Apprentice Mechanic', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Apprentice Motor Mechanic', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('grease monkey', @equivalentId)

SET @equivalentId = '65ad8632-c1e4-4bc4-849b-f1b5a20d8b9e'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Electrical mechanic', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Maintenance Electrician', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Electrical Fitter', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('electrician', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('electrical contractor', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('sparky', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('sparkie', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('auto-electrician', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('autoelectrician', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('auto electrician', @equivalentId)

SET @equivalentId = 'cfea137e-baef-49a2-92e3-1b73d26c1103'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Apprentice Electrical Mechanic', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('trainee Electrical Mechanic', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('trainee auto-electrician', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('trainee autoelectrician', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('trainee auto electrician', @equivalentId)

SET @equivalentId = 'bb29b2bf-2ef1-4697-b55a-236e8f63e371'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Diesel Mechanic', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('diesel motor mechanic', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('diesel fitter', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('diesel engine mechanic', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('diesel mechanic/fitter', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('diesel plant fitter', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('truck mechanic', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('heavy vehicle mechanic', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('plant mechanic', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('diesle mechanic', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('diesel technician', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Diesel Fitter/Mechanic', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('diesel/mechanical fitter', @equivalentId)

SET @equivalentId = '0868b25a-34c2-495a-a06b-dfd14b286267'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Electrical Supervisor', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Leading Hand Electrician', @equivalentId)

SET @equivalentId = 'b97bb568-c8be-4fa6-8d02-5f17f942b932'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Apprentice Electrician', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('trainee electrician', @equivalentId)

SET @equivalentId = 'e6530667-4b53-4d85-b61a-3354ada21a00'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('tiler', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('roof tiler', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('roofer', @equivalentId)

SET @equivalentId = '1d0b2ebf-8a45-4b0f-b825-7f2d88ca0477'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('stable hand', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('stablehand', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('yardman', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('yardsman', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('strapper', @equivalentId)

SET @equivalentId = '73c5432c-2191-42af-a392-c1eb759ed6d4'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('cabling technician', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('cabler', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('telecommunications technician', @equivalentId)

SET @equivalentId = 'e99f1641-54e0-4510-ba1c-ca8a15784682'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('FARMHAND', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Farm hand', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('roust about', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('jackeroo', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('jackaroo', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('jillaroo', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('jilleroo', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('farm worker', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Farm Hand Labourer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('general farm hand', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('farmwork', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('farm work', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('farm attendant', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('farm assistant', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('farm duties', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('farm labourer', @equivalentId)

SET @equivalentId = '057829db-4b4a-4f40-b73a-4ef17e16b5db'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('farm manager', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('rural proerty manager', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('farm supervisor', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('farm owner', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('share farmer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('farmer', @equivalentId)

SET @equivalentId = 'fef34829-c8cd-49d8-970d-6dabca586d70'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('sheep', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('merino', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('lamb', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('mutton', @equivalentId)

SET @equivalentId = 'ac13a324-5668-4afa-bbca-dec1331e5841'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('beef', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('cattle', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('angus', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('bull', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('cow', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('heffa', @equivalentId)

SET @equivalentId = 'e631a86e-571f-4459-b250-1e1e9dbd6079'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('dairy', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('milk', @equivalentId)

SET @equivalentId = 'f2d8fad2-33cc-4870-bc61-2db4dbf834db'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Agronomist', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Agricultural scientist', @equivalentId)

SET @equivalentId = '2f84b6ed-6848-400a-a9d9-9522eee929c1'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('backhoe operator', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('digger operator', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('digger driver', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('excavator operator', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('excavator driver', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('bulldozer driver', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('dozer driver', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('bulldozer operator', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('excavator/dozer operator', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('excavator/dozer driver', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('bobcat operator', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('bobcat driver', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('skidsteer operator', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('heavy machine operator', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('heavy equipment operator', @equivalentId)

SET @equivalentId = '1770bf13-499f-4a98-ac88-860614827d33'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('A-Grade Electrical', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('A Grade Electrical', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('A Grade electrician', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('agrade electrician', @equivalentId)

SET @equivalentId = '59498ab6-ca1b-421f-b56a-916bec8d8236'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('elec', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('electrical', @equivalentId)

SET @equivalentId = '79aac74a-6487-431f-a49a-dc3996287de2'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('painter', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('house painter', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('painting worker', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('painting and decorating', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('painting & decorating', @equivalentId)

SET @equivalentId = 'c9c8a06f-329e-4090-88d2-227c661913d4'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('spray painter', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('spraypainter', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('auto painter', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('car painter', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('paint line operator', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('paint shop operator', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('paintshop operator', @equivalentId)

SET @equivalentId = 'b55479c5-6bb4-407e-9e9d-1c53ca427881'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('panel beater', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('panelbeater', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('automotive repairer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Panel beating', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Panelbeating', @equivalentId)

SET @equivalentId = 'df037ec9-8a31-46d7-a693-402a00364fee'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('freight clerk', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('freight officer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('freight forwarding clerk', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('freight forwarding officer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('shipping clerk', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('shipping officer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('freight staff', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Freight Forwards Operator', @equivalentId)

SET @equivalentId = 'ff339846-4c70-45d3-80c8-29cbe12b59cd'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('plumber', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('gas fitter', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('plombier', @equivalentId)

SET @equivalentId = '936d0739-e5ee-413b-abf2-dcaa60415ddc'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('sheetmetal worker', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('sheet metal worker', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('metal worker', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('sheet metal fabricator', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('sheetmetal fabricator', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('sheet metal', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('sheetmetal', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('sheet metal bender', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('sheet metal fabrication', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('sheet metal fitter', @equivalentId)

SET @equivalentId = '6a4f7923-ca9e-41cf-980c-8a54c0f1a762'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('baker', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('bread maker', @equivalentId)

SET @equivalentId = 'bbb6ea38-d9ca-448f-850c-579c59a8629b'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('landscaper', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('landscape gardner', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('landscape architect', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('landscaping', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('garden designer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('garden design', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('garden landscape engineer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('garden landscaping engineer', @equivalentId)

SET @equivalentId = '3fca318d-cc76-41fc-b0d4-d2e1570726f3'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('gardiner', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('gardner', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('gardener', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('garden worker', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('garden maintenance worker', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('garden maintenance assistant', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('garden labourer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('garden maintenance', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('gardening supervisor', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('gardening hand', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('garden assistant', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('garden maintaining', @equivalentId)

SET @equivalentId = 'd40b982c-44d7-401b-b2a5-89e9e80264c0'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('cabinetmaker', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('cabinet maker', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('furniture maker', @equivalentId)

SET @equivalentId = '316f17b7-e121-4dcc-9288-26847a7e3d6a'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('refridgeration technician', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('refridgeration mechanic', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('fridge technician', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('fridge mechanic', @equivalentId)

SET @equivalentId = '88c4e2e9-8fec-4b41-9e79-a06a3a2f0d3c'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('grounds man', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('groundsman', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('grounds keeper', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('groundskeeper', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('ground attendant', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('public area attendant', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('green keeper', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('greenkeeper', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('turf manager', @equivalentId)

SET @equivalentId = 'aa704c9a-7c7f-494b-b194-77dd8c08f243'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('flooring', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('floor covering', @equivalentId)

SET @equivalentId = '000b1872-071b-4213-9bec-3fa20120c69c'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('crane driver', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('crane operator', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('crane truck driver', @equivalentId)

SET @equivalentId = '359c7504-5153-44bc-aed6-f95f570be614'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('rigger', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('dogman', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('dog man', @equivalentId)

SET @equivalentId = '86ccec35-ec16-4ce0-b7bd-fcd5ca9fb01d'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('tyre fitter', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('tyrefitter', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('wheel fitter', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('tyre technician', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('wheel aligner', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('wheel balancer', @equivalentId)

SET @equivalentId = 'deb678dc-07b2-451e-b299-8715e7996e82'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('concreter', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('concrete finisher', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('concrete labourer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('concrete worker', @equivalentId)

SET @equivalentId = '657ade62-5e79-46e4-bc95-da1e5ac679b1'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('concrete truck driver', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Concrete Agitator Driver', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('truck driver concrete', @equivalentId)

SET @equivalentId = 'a2b3479b-6070-4804-9aff-4c4c185f9972'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('builder', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('framer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('carpenter', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('charpentier', @equivalentId)

SET @equivalentId = '7a0df747-881f-4d65-a07e-b0f124ff29b3'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('contracts administrator', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('contract adminstrator', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('contract manager', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Contract Compliance Officer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Contract Officer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Contracts Admin', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('contracts manager', @equivalentId)

SET @equivalentId = '5d672732-2170-4dfb-a135-6605d77f3df9'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('bricklayer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('brick layer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('bricky', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('brickie', @equivalentId)

SET @equivalentId = '7868ae5c-8530-4660-8595-1b92cfe73c2e'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('renderer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('rendering', @equivalentId)

SET @equivalentId = '71d44eaa-94ad-4350-abbf-9bbc533eb45e'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Operations Manager', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Ops manager', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Manager Operations', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('General Manager Operations', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('National Operations Manager', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Area Operations Manager', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Regional Operations Manager', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Group Operations Manager', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Business Operations Manager', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('State Operations Manager', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('operational manager', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('operations mgr', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('head of operations', @equivalentId)

SET @equivalentId = '0e5cb836-feb5-4239-b1e4-5ca1be1517b8'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('plant manager', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('National Plant Manager', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('manufacturing manager', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Production Supervisor', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Production Manager', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('production mgr', @equivalentId)

SET @equivalentId = '2741b4f6-8a18-48d7-b87a-8df774b7df64'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Operations Coordinator', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('operations co ordinator', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('operations co-ordinator', @equivalentId)

SET @equivalentId = '6eeb3cb2-8fc0-4aa6-ae1e-0580cc67333a'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Operations Analyst', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Customer Operations Specialist', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Operations Support Officer', @equivalentId)

SET @equivalentId = 'f1e70651-5ded-4b3d-864f-21d393ecbb57'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Production Coordinator', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Production Co-Ordinator', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Production Co Ordinator', @equivalentId)

SET @equivalentId = '1316f121-8a41-45e7-9d03-64457b0b394d'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Production Team Leader', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Production team lead', @equivalentId)

SET @equivalentId = '9dabc01b-90ad-4bef-ac12-067dc72733c7'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('PRODUCTION SHIFT MANAGER', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('shift manager production', @equivalentId)

SET @equivalentId = 'd22152d8-21c5-4684-9902-722671f0a5cf'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('crew member', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('crewmember', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('team member', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('teammember', @equivalentId)

SET @equivalentId = 'd7554271-24f0-4594-863d-9c1a23496152'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Assistant Production Manager', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Asst production manager', @equivalentId)

SET @equivalentId = '66d43908-0a3e-4337-ad9d-489164454ba4'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('under manager', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('undermanager', @equivalentId)

SET @equivalentId = 'd995a26c-4f33-4524-94b1-32619958f37e'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Production Planner', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Production Scheduler', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Production Analyst', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Production Controller', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Production Assistant', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('PRODUCTION CLERK', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('mrp controller', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('supply chain planner', @equivalentId)

SET @equivalentId = '165da3e9-2106-4f9c-80ab-00e5d5ec8c54'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Production Operator', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Production / Machine Operator', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Machine Operator', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('machinist', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Production Worker', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('machiner', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('assembly line worker', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('assembly line operator', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('factory worker', @equivalentId)

SET @equivalentId = 'c022f677-16f0-4ee0-8bd5-73ee9642f728'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('process worker', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Process Worker/Packer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('food production worker', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Meat Packer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('process operator', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('general hand', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('generalhand', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('processor', @equivalentId)

SET @equivalentId = '3abc2ab4-0f4f-4ce2-add1-56a5f880e569'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Casual Process worker', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('temp process worker', @equivalentId)

SET @equivalentId = '6c769042-b793-4345-aeb2-f6c827dc8cb7'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Warehouse Supervisor', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Warehouse manager', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Warehouse superintendant', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Warehouse Mgr', @equivalentId)

SET @equivalentId = 'e199b120-5451-4913-aad1-d558b220dc19'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Warehouse Assistant', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Warehouse', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Warehouse worker', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Warehouse Clerk', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Warehouse Person', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Warehouse Asst', @equivalentId)

SET @equivalentId = 'de44d20d-7018-4abc-886d-29cabac23b7c'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Forklift Operator', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('forklift driver', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('fork lift driver', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('fork lift operator', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('forklift', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('fork-lift', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('fork lift', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('fork hoist', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('fork truck', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('forktruck', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Forktruck Operator', @equivalentId)

SET @equivalentId = 'f3adbf6d-d8f9-45cd-9081-978a27bb672d'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Maintenance Supervisor', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Maintenance manager', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Maintenance superintendant', @equivalentId)

SET @equivalentId = 'b24ae718-9ac5-45fa-ac3f-4276ab79c139'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Packer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Picker/Packer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('NIGHT PACKER', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('pick packer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Casual Packer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Picker / Packer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Pick/Packer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Picker packer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Picker', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('pick pack', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('order picker', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('pickerpack', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('seasonal picker', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('seasonal fruit picker', @equivalentId)

SET @equivalentId = 'b9be6cad-d499-4143-a26d-b8936f17fdd3'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('jit', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('just in time', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('justintime', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('j I t', @equivalentId)

SET @equivalentId = 'c03b3f3c-0d39-4110-8702-d17f66f5e353'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('tqc', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('total quality control', @equivalentId)

SET @equivalentId = '5cf807a1-c9fc-46ef-916a-93b162e0d3a2'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Factory Hand', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Casual Factory Hand', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('General Factory Hand', @equivalentId)

SET @equivalentId = '0f7331fb-483c-4e7a-9c20-5bee424c5490'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Storeman', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Warehouse Storeman', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Storeman / driver', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Storeman/driver', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Storeman/Frontcounter Sales Counter Sales', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Storeman/Forklift Driver', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Casual Storeman', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Warehousing/Storeman', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('storeperson', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Forklift Operator/Storeperson', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Store Person', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('store man', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('store people', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('storepeople', @equivalentId)

SET @equivalentId = '38468069-22e7-44ef-95da-b517f6e005c9'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Quality Assurance Specialist', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('QA Specialist', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Q.A. Specialist', @equivalentId)

SET @equivalentId = '09437837-ab4e-4958-ba0c-454dd0305a56'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('general counsel', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('head counsel', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('head lawyer', @equivalentId)

SET @equivalentId = '9356828c-5bcc-49bc-8462-8f3da3b0eb6d'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Lawyer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Solicitor', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Legal Counsel', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('cousel', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Corporate Solicitor', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Litigation Solicitor', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Law Graduate', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('counsel', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('barrister', @equivalentId)

SET @equivalentId = 'a15e093b-998d-4bb4-a7b4-b9d89cb1aa57'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Legal Secretary', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Legal Secretary/Receptionist', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Legal Secretary/Personal Assistant', @equivalentId)

SET @equivalentId = 'bdfe7597-b2a2-4d9e-88f3-9bb669b21da5'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Corporate legal secretary', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Commercial legal secretary', @equivalentId)

SET @equivalentId = '813ceb72-42be-4b15-86b9-6e7be3bacb75'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Legal Clerk', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Legal Officer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('law clerk', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('law officer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Legal Assistant', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Legal Administration Assistant', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Legal Administration Officer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('para legal', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Paralegal', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Junior Legal Secretary', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Trainee Legal Secretary', @equivalentId)

SET @equivalentId = 'b998a479-46ad-45b5-a725-0bbbf1fb5d44'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('jp', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('justice of the peace', @equivalentId)

SET @equivalentId = '09acd33e-78fa-44ad-a612-3c8b73fe73c9'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('queens counsel', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('queen''s counsel', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('senior councel', @equivalentId)

SET @equivalentId = 'da336c38-2e7b-486a-974b-2da08a3c7935'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Associate Nurse Unit Manager', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Nurse Unit Manager', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Charge Nurse', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('head of nursing', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('nursing manager', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('director of nursing', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('nursing director', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('num', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('n.u.m.', @equivalentId)

SET @equivalentId = 'afe3dec1-bf7d-4ad3-a634-6f8b2964e998'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Registered Nurse', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Registered nurse - Level', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Practice Nurse', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Staff Nurse', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('State Enrolled Nurse', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('ren', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('sen', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('rn', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('registered enrolled nurse', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('graduate nurse', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('palliative care nurse', @equivalentId)

SET @equivalentId = '85ac7bc5-86ab-4e27-82c2-35829085c371'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Clinical Nurse Specialist', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Clinical Nurse', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Clinical Nurse Consultant', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('theatre sister', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('trauma nurse', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('theatre nurse', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Nurse Operating Theatre', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Operating Theatre nurse', @equivalentId)

SET @equivalentId = '94d078aa-1643-4458-a98c-8afcba65a5a0'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('orderly', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('nursing attendant', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('personal service assistant', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('hospital orderly', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('medical orderly', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('shift orderly', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('psa', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('p s a', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('wardsman', @equivalentId)

SET @equivalentId = '5300ad1d-202b-438d-9c7e-e90327d98de2'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Clinical Nurse Educator', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('General Nurse Training', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('nurse educator', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('nursing educator', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('nurse education officer', @equivalentId)

SET @equivalentId = '5e1b43ce-6902-4067-8282-e8a288534030'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Nursing Assistant', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('assistant nurse', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Nurse Aid', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('ain', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('nurses aid', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('nurses'' aid', @equivalentId)

SET @equivalentId = 'c97cc61d-4f8f-4cb7-b701-bcda71e35ab0'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('trainee nurse', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Student Nurse', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Trainee Enrolled Nurse', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Student Enrolled Nurse', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('undergraduate  nurse', @equivalentId)

SET @equivalentId = '6e8d4eff-4840-44e9-bc08-48c275e46ae1'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Nurseryhand', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('nursery hand', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('paediatric nurse', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('ICU nurse', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('maternity nurse', @equivalentId)

SET @equivalentId = 'da6b1310-a185-438d-8372-a3b32b378e1d'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Paediatric Specialist', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Paediatrician', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Baby doctor', @equivalentId)

SET @equivalentId = '8c913156-147b-4d26-9433-c065619ee0c4'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Medical Receptionist', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Medical Secretary', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Senior Medical Receptionist', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Casual Medical Receptionist', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('medical centre receptionist', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('medical reception', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('medical centre reception', @equivalentId)

SET @equivalentId = '754208c5-5e3d-4861-b583-7d9ecb7d07ce'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('medical records clerk', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('medical records officer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('medical information officer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('medical information clerk', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('medical records administrator', @equivalentId)

SET @equivalentId = 'bb1881ad-b5d4-4191-8170-20a30316765e'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('dentist', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('dental surgeon', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Endodontist', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Periodontist', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('general dentist', @equivalentId)

SET @equivalentId = '65a136fb-2418-4111-9eff-ae1b8f7752da'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Dental Nurse', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Dental Assistant/Receptionist', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Dental Assistant/Reception', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('dental assistant', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('dental assisting', @equivalentId)

SET @equivalentId = 'ec3a38c4-6170-4c1b-8538-e4d8ba47ab72'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('dental hygienist', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('dental technician', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('dental therapist', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('dental hygiene educator', @equivalentId)

SET @equivalentId = '4c527be5-2061-42a4-b1fa-f1fa33bc5f81'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Junior Dental Nurse', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Trainee Dental Nurse', @equivalentId)

SET @equivalentId = 'd84430b0-e8e4-4c17-be99-787854076d15'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Veterinary Nurse', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Vet Nurse', @equivalentId)

SET @equivalentId = '8783a275-84aa-4950-9739-8ea7b45763c7'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('vet', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('vetinary', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('vetinarian', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('veterinarian', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('veterinary', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('veterinary practitioner', @equivalentId)

SET @equivalentId = 'd11e520a-fabb-4bde-9822-3aa3d5ae11fd'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Radiographer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('sonographer', @equivalentId)

SET @equivalentId = '6a09c81f-2edc-4940-9199-cd65d90baa15'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('gp', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('general practisioner', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('general practitioner', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('medical officer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('vmo', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('registrar', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('physician', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('general physician', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('medical practitioner', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('house medical officer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('hmo', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('senior house officer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('medical doctor', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('rmo', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('registered medical officer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('G.P.', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('doctor', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('dr', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('registrant', @equivalentId)

SET @equivalentId = '0b5c35ff-7add-44ec-a82c-249c67686eb7'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('obstetric', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('gynaecology', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('gynaecologist', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('obstetrician', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('gyno', @equivalentId)

SET @equivalentId = '871756b6-4f08-4b9a-9ca6-eecc34b73e32'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('ward clerk', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('ward officer', @equivalentId)

SET @equivalentId = '75407eb1-76c2-4b29-9627-1a9fa86928d3'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('nanny', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('nannies', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('aupair', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('au-pair', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('au pair', @equivalentId)

SET @equivalentId = '6d4c6142-85c1-4052-a674-c80e8b5efc61'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('osteopath', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('osteo', @equivalentId)

SET @equivalentId = 'bbd21ef8-e226-4e14-a429-8318bb25e423'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('clinical research associate', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('clinical research assistant', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('clinical research coordinator', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('clinical research co ordinator', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('clinical research consultant', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('clinical research professional', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('clinical trial assistant', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('clinical trial coordinator', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('trial coordinator', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('trial assistant', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('study coordinator', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('study manager', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('clinical research manager', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('clinical project manager', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('clinical research co-ordinator', @equivalentId)

SET @equivalentId = '7c0d32b5-7711-471f-9835-d795dee41ddc'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('pbac', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Pre-qualifications Bids and Awards Committee', @equivalentId)

SET @equivalentId = 'e803467f-4fdd-4cc2-b280-9e0a03099b7e'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('PHARMACY ASSISTANT', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Pharmacy Assistant Customer Service', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('dispensary technician', @equivalentId)

SET @equivalentId = '7c79a01f-50af-4d4e-b338-c9f7cd2f16bb'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('pharmacist', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('pharmacy manager', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('retail pharmacist', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('pharmacy owner', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Chemist', @equivalentId)

SET @equivalentId = '3bbab74e-5375-4f32-96a4-8af00a61025a'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('pharmaceutical', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('pharmaceutics', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('pharmacy', @equivalentId)

SET @equivalentId = '7474f68b-350f-439f-8bd9-780c71c1f939'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Laboratory Technician', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('medical laboratory technician', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('lab technician', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Laboratory Assistant', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Lab Assistant', @equivalentId)

SET @equivalentId = '08faa01a-f251-4219-8fa3-b4225984e9c2'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('cardio', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('cardiovascular', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('cardio-vascular', @equivalentId)

SET @equivalentId = 'e0b8a637-6f17-40a4-85fa-0606e579ee67'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('first aid', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('firstaid', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('first-aid', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('St John''s', @equivalentId)

SET @equivalentId = '9e11780b-1b1c-41f8-9c29-50cc1ceeccf7'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Psych', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('psychologist', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('psychology', @equivalentId)

SET @equivalentId = '5e3cd68b-6e3a-4867-9f09-4ba31dc535a2'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('rehab', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('rehabilitation', @equivalentId)

SET @equivalentId = '3fac5eda-53ca-44d1-baff-6e48825278e8'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('occ', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('occupational', @equivalentId)

SET @equivalentId = 'cea19601-ffe1-44af-9de7-a074e3da2af1'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('rec', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('recreational', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('recreation', @equivalentId)

SET @equivalentId = 'b732b91d-512c-4e7d-9374-28fd16e9b3b6'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('speech therapist', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('speech pathologist', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('language therapist', @equivalentId)

SET @equivalentId = 'e0510ec8-c20f-4eee-b759-2a4e8cab1274'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('ambulance', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('paramedic', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('para medic', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('ambulance driver', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('ambo', @equivalentId)

SET @equivalentId = '2c032db2-b9b3-410b-9f73-d11213fd6b41'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('scra', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('society of clinical research associates', @equivalentId)

SET @equivalentId = 'eaab6451-0468-49a3-b1c3-a6c1bc50a36a'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('anaesthetic', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('anaesthetist', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('anesthetist', @equivalentId)

SET @equivalentId = '11a50d6c-5b8b-46a5-9808-63aebfded4c0'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('a&e', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('accident and emergency', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('emergency medicine', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('casualty', @equivalentId)

SET @equivalentId = 'dbeea3c5-a09e-45f2-aef6-09ef402e7296'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('midwife', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('mid wife', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('mid wifery', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('midwifery', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('mid wive', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('midwive', @equivalentId)

SET @equivalentId = 'b217d8e8-2662-4dde-8f60-558c83d6aac0'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('chemo', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('chemotherapy', @equivalentId)

SET @equivalentId = '2ff6d0a4-4a04-4cfd-9475-403fd0faa0b1'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('en', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('enrolled nurse', @equivalentId)

SET @equivalentId = 'ca674f5c-6f57-4619-a12f-a5b2cc5e17d3'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('een', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('endorsed enrolled nurse', @equivalentId)

SET @equivalentId = 'cd9a9987-d64a-4424-9a31-4375c301301b'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('physio', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('physiotherapy', @equivalentId)

SET @equivalentId = '922b36b7-bb20-4452-9ff1-b54e8ea51531'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('chiropractor', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('CHIRO', @equivalentId)

SET @equivalentId = '339e3f8c-4423-4d67-8f9c-51ccc4688792'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('chiropractic assistant', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('chiropractic technician', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('chiropractors assistant', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('chiropractor''s assistant', @equivalentId)

SET @equivalentId = 'cffed89f-4e9a-467e-b833-fb8a74358345'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('nutriciantist', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('dietician', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('food nutritionist', @equivalentId)

SET @equivalentId = '81e953d0-5988-4e89-8c4e-36389a9d92f0'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('ddon', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('deputy director of nursing', @equivalentId)

SET @equivalentId = '5828d75a-6a7b-42b3-b133-f13028d43d75'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('ultrasound', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('ultra sound', @equivalentId)

SET @equivalentId = 'cb006b1a-d35e-48c2-9a1c-eb7a08176719'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('outplacement', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('out placement', @equivalentId)

SET @equivalentId = 'f3afa5e8-1f8a-4246-b623-d39627f8ce4e'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('radiotherapy', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('radio therapy', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('radiotherapist', @equivalentId)

SET @equivalentId = 'd4c5d4ab-b890-43fb-a1cb-49ce6a397450'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('outplacement clerk', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('outplacement officer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('outplacement consultant', @equivalentId)

SET @equivalentId = '48d22db0-fea6-4b34-9f9f-1666a10c9ba0'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('aod', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('alcohol and other drugs', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('alcohol & other drugs', @equivalentId)

SET @equivalentId = '41d04801-4c7b-474a-97f4-e952e46518e9'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('neurology', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('neuro', @equivalentId)

SET @equivalentId = '0808c5eb-588c-4709-b868-2bfa83b37176'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('outpatient clerk', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('outpatient officer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('out patient officer', @equivalentId)

SET @equivalentId = 'e5f5dc05-b1a8-4a6f-9e9c-375dbe54f66e'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('SMO', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('senior medical officer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('senior medical officers', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('practitioner', @equivalentId)

SET @equivalentId = '2fcb88ad-3d91-4d38-9b9d-6297b792b58f'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Clinical Care Manager', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Clinical Care Mgr', @equivalentId)

SET @equivalentId = 'c8fa625f-a5ea-46e5-b841-21138497117f'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Radiologist', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Radiology', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Nuclear medicine', @equivalentId)

SET @equivalentId = '687d1286-8aa3-4cd2-b5f8-38006a8b97f5'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Child psychiatrist', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Child psychiatry', @equivalentId)

SET @equivalentId = 'b7c4884b-341d-497e-9d5d-0708c4376087'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Senior Resident Medical Officers', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('SRMO', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Senior Resident Medical Officer', @equivalentId)

SET @equivalentId = '4b8df198-5246-422d-99eb-f9c111858713'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('business development manager', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('business development', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('bdm', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('b.d.m.', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('salesman', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Sales Person', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Salesperson', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('sales rep', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('sales representative', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('sales consultant', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Sales Executive', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Sales Exec', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Sales Professional', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Area Sales Representative', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Technical Sales Manager', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Senior Sales Consultant', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Senior Sales Representative', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Field Sales Executive', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('National Business Development Manager', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Market Development Manager', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Business Development Consultant', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('BDC', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('B.D.C.', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('sales man', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('saleswoman', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('BDE', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('B.D.E.', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Business Development Executive', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('sales people', @equivalentId)

SET @equivalentId = 'f192bfff-5615-4670-bed7-36f5942949e3'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Account Manager', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Account executive', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Sales Account Manager', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Accounts Supervisor', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Acct Supervisor', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Senior Account Executive', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('National Account Executive', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Senior Account Manager', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Key Account Manager', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Corporate Account Manager', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Internal Account Manager', @equivalentId)

SET @equivalentId = 'f1b7dbe5-5cb9-4d2b-95e7-64d048d6daaf'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Sales Manager', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('area manager', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Area Sales Manager', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('National Sales Manager', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('General Manager Business Development', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('gm business development', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Regional Sales Manager', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Territory Sales Manager', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Field Sales Manager', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('District Sales Manager', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('International Sales Manager', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('cluster manager', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('cluster leader', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('region manager', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Territory Manager', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('vp sales', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('vice president sales', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('sales vice president', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('sales vp', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('sales v.p.', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Sales Director', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('GENERAL SALES MANAGER', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Sales Team Leader', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Sales Leader', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('State Sales Manager', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('NSW Sales Manager', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Victorian Sales Manager', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Group Sales Manager', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Corporate Sales Manager', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Sales Supervisor', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Senior Sales Executive', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Corporate Sales Executive', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Marketing/Sales Manager', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('director of sales', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Sales and Marketing Manager', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Sales & Marketing Manager', @equivalentId)

SET @equivalentId = 'b0625b5c-be2f-4994-b25e-391af3aac72b'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Assistant Sales Manager', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Asst Sales Manager', @equivalentId)

SET @equivalentId = '1c53bcd3-4aa9-4faf-a145-3f36e25a1f26'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Channel Manager', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('channels manager', @equivalentId)

SET @equivalentId = '743e4a90-0254-4ed6-8ab8-c7bec72c46df'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Presales', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('pre-sales', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('pre sales', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('presales engineer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('pre-sales engineer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('presales coordinator', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('sales support manager', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Sales Engineer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('sales engineering manager', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Technical Sales', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Technical Sales Representative', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Senior Sales Engineer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('presales co-ordinator', @equivalentId)

SET @equivalentId = 'f67175f7-8e8e-471e-ab2b-b34833be24de'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Sales admin', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Sales Administrator', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Sales Coordinator', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Sales Co ordinator', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Sales Co-ordinator', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('sales clerk', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Sales Support Officer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Sales Assistance', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Sales Support Representative', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Sales Administration', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Receptionist/Sales Support', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Sales Secretary', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Team Secretary', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('sales support', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Sales supp', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Key Account Support Officer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Key Account Support Assistant', @equivalentId)

SET @equivalentId = '602bea7f-15c0-4d0a-bb7f-7034c87c4196'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Internal Sales Representative', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Internal Sales Rep', @equivalentId)

SET @equivalentId = '573e7c26-56df-4ee2-bc44-f4aaa7f83caf'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('casual Sales assistant', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('p/t sales assistant', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('p/time sales assistant', @equivalentId)

SET @equivalentId = 'ec966a44-607e-4c6d-8b54-84fb680359c1'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Sales Agent', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Sales Advisor', @equivalentId)

SET @equivalentId = 'd16dae70-e080-4cf1-b450-63d4b2402b4a'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('SALES ASSOCIATE', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Sales Assoc', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Senior Sales Associate', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Sr Sales Associate', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Sr. Sales Associate', @equivalentId)

SET @equivalentId = '4a785d72-5ed0-4b3b-8522-6302b702106c'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Sales Assistant/Manager', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Senior Sales Assistant', @equivalentId)

SET @equivalentId = '147e36c1-a4c5-48c7-b1ea-006fcadf5a45'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Junior Sales Assistant', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Sales Assistant/Cashier', @equivalentId)

SET @equivalentId = '80b5515a-918a-48d6-96e7-697c3079ff42'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Sales Assistant/Supervisor', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('supervisor/sales assistant', @equivalentId)

SET @equivalentId = 'd73041b1-c17b-4c71-8559-a0021b31c099'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Sales Assistant Customer service', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Customer Service/Sales Assistant', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Receptionist/Sales Secretary', @equivalentId)

SET @equivalentId = '346d393d-879a-4d70-9f2a-3ffda66505ec'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Advertising Sales Manager', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Advertising Sales Representative', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Advertising Sales Rep', @equivalentId)

SET @equivalentId = '52db5189-f30e-4ca6-8f28-eb2549c7611a'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Reservations Sales Agent', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Travel Agent', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Agent -Reservations', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Travel Consultant', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('travel', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('wholesale consultant', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('ticketing consultant', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('travel coordinator', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('travel manager', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('travel specialist', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('travelagent', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('inbound tour', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('travel co ordinator', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('inbound reservations', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('reservations agent', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('reservations consultant', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('travel co-ordinator', @equivalentId)

SET @equivalentId = 'e9766a3b-ae0e-48dc-8736-34c49c91a9f2'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('crm', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('customer relationship management', @equivalentId)

SET @equivalentId = '64fadf86-9fa3-4b4d-874b-30ab0f7f7d3a'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Major Account Manager', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Major Accounts Manager', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Major Accts Manager', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Major Accts Mgr', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Major Acct Mgr', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Major Acct Manager', @equivalentId)

SET @equivalentId = '244f9e64-bfc8-4fff-a6ad-6faa308b1782'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Marketing Manager', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('National Marketing Manager', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('GM Marketing', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Marketing GM', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Marketing general manager', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('general manager marketing', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('marketing director', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Group Marketing Manager', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Regional Marketing Manager', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Marketing communications executive', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Marketing Executive', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Marketing Exec', @equivalentId)

SET @equivalentId = 'b776b24c-2a33-419d-b6a1-004e1314a4e5'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('vp marketing', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('vice president marketing', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('marketing vice president', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('marketing vp', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('marketing v.p.', @equivalentId)

SET @equivalentId = 'ce06c3f8-cdb0-4b86-9fbe-6af470062a37'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('membership manager', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('membership development manager', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('manager membership services', @equivalentId)

SET @equivalentId = 'a6c8c614-336c-4a96-b950-93144cd15884'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('membership consultant', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('membership officer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('membership services officer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('membership administrator', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('membership sales consultant', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('membership development consultant', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('membership development offcier', @equivalentId)

SET @equivalentId = '1ed8af5b-be9e-4421-87e2-ac796bbe10da'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Online Marketing Manager', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Internet marketing manager', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Internet marketing mgr', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('online marketing mgr', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('web marketing manager', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('web marketing mgr', @equivalentId)

SET @equivalentId = '5a0ad96a-954a-4a80-a090-fdda88862ccc'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Assistant Marketing Manager', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('asst marketing manager', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('assistant marketing mgr', @equivalentId)

SET @equivalentId = 'ebb96572-9d83-44f2-8206-e2b3ef2ce0ed'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Marketing Assistant', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Marketing asst', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Casual Marketing Assistant', @equivalentId)

SET @equivalentId = 'fca83c9a-2947-4420-a37c-a7d4d5130c88'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('PR Manager', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('P.R. Manager', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Public Relations Manager', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('PR Director', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Public Relations Director', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Public relations', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Marketing Communications Manager', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Communications Manager', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('communications officer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('communications advisor', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('media liason officer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('media liason', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('PR mgr', @equivalentId)

SET @equivalentId = 'aa386649-f326-4bdf-9c90-664819d863b8'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('PR', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('p r', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('pblic relations', @equivalentId)

SET @equivalentId = '69a6dfba-d2cd-4428-9051-4b0a7f9fbf6f'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Marketing Communications Specialist', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('marketing communications', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('marketing coordinator', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('marketing co ordinator', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('marketing co-ordinator', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Marketing Communications Co-ordinator', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Marketing Communications Co ordinator', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Marketing Communications Coordinator', @equivalentId)

SET @equivalentId = 'fb4b114b-cecf-40e3-b503-dae5b71c285e'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('creative director', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('head of creative', @equivalentId)

SET @equivalentId = 'dfcc60af-e700-4e82-b428-4bf0899254a1'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Communications', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Communications Consultant', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('COMMUNICATIONS COORDINATOR', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Communications Specialist', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('comms', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('COMMUNICATIONS CO-ORDINATOR', @equivalentId)

SET @equivalentId = '53177ff1-e2e4-43dc-b590-acd0a81870f9'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Market Research', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Market researcher', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Market Research Interviewer', @equivalentId)

SET @equivalentId = '1ad92c29-8275-4ea4-9cdc-c3300bab3ec7'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('fundraising', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('fund raising', @equivalentId)

SET @equivalentId = 'b62bc381-0a5b-41ca-b316-c65248fc218e'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Promotions', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Promotions Officer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Promotions Co-ordinator', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Promotions Manager', @equivalentId)

SET @equivalentId = '0490f4a3-a3e6-4dc1-ae11-dea433a194a4'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Senior Publicist', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Sr Publicist', @equivalentId)

SET @equivalentId = '1fc06dbd-caf6-48d0-9085-e7b945ab6ed8'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Event Manager', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Events Manager', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('event management', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('event coordinator', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('events coordinator', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('event co-ordinator', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('events co-ordinator', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Events Managing', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Event Mgr', @equivalentId)

SET @equivalentId = 'afecd693-1e1a-45ea-8bab-ab40e6ac9b76'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('event staff', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('event hostess', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Function Hostess', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('hostess', @equivalentId)

SET @equivalentId = 'd3572d4a-49f6-4f60-bf90-2cda8cebf042'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Advertising Sales Executive', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Advertising Sales', @equivalentId)

SET @equivalentId = 'b32938b4-09c1-419c-b68a-88ab78cb0d7a'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Product Manager', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Senior Product Manager', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('National Product Manager', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Assistant Product Manager', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Group product manager', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('New Product Manager', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('product design manager', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('product management', @equivalentId)

SET @equivalentId = '3143abe7-f682-4546-ba81-dae0afe752cd'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Market Research Officer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Market Research Consultant', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Market Research Analyst', @equivalentId)

SET @equivalentId = 'a54c8e2d-365c-4967-931c-3f9306b99217'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Merchandiser', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Visual Merchandiser', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Sales Merchandiser', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Assistant Merchandiser', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Sales Representative / Merchandiser', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Sales Representative/Merchandiser', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Casual Merchandiser', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('store Merchandiser', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Senior Merchandiser', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('merchandise', @equivalentId)

SET @equivalentId = 'a09be62c-fbc9-4659-920b-b990f5ad4571'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Advertising Account Manager', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('account manager advertising', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('advertising consultant', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('advertising account executive', @equivalentId)

SET @equivalentId = 'd2d8669f-19fc-48f4-8a07-f1ddad18c75f'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('fmcg', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('fast moving consumer goods', @equivalentId)

SET @equivalentId = 'ae395a02-bb52-4a14-9651-af7de7ff674a'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Merchandise Planner', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Merchandise Planning', @equivalentId)

SET @equivalentId = 'c674de5a-c4e6-4af8-ad8c-dd653c6c07e2'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Research Assistant', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Research Asst', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Senior Research Assistant', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Sr Research Assistant', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('clinical research', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('research officer', @equivalentId)

SET @equivalentId = '389d9979-3627-4177-aad0-3c31f873c7ec'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('school principal', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Senior Teacher', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Acting Principal', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Vice Principal', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('head of school', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Deputy head', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Headmaster', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('headmistress', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('head master', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('head mistress', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('head of junior school', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('head of middle school', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('head of senior school', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('associate principal', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('principal', @equivalentId)

SET @equivalentId = '8baf649f-e8c5-4ca8-920c-494f4a23528e'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Secondary Teacher', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Middle School Teacher', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('teacher secondary', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('secondary and primary teacher', @equivalentId)

SET @equivalentId = 'a85734d6-ad85-4993-96f3-d39c00310f8e'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Primary Teacher', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Classroom Teacher', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Primary School Teacher', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('junior school teacher', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('PS teacher', @equivalentId)

SET @equivalentId = 'a8f64f77-dd9d-48cc-af96-f0eec5e545e8'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Senior grades teacher', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('high school teacher', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('vce teacher', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('v.c.e. teacher', @equivalentId)

SET @equivalentId = '9a09545c-a019-413b-8860-5c84a08ff24f'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Teacher', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('school teacher', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Teacher/Trainer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('primary and secondary teacher', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('class teacher', @equivalentId)

SET @equivalentId = '110c0b5c-5235-4fe6-bde6-a6af2317fa19'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('kindergarten teacher', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('kindy teacher', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('infants school teacher', @equivalentId)

SET @equivalentId = 'ff12d59a-c57f-401c-976e-f07e5e68077b'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('librarian', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('library teacher', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('teacher librarian', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('campus librarian', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('branch librarian', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('knowledge centre manager', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('resource librarian', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('resources librarian', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('resources technician', @equivalentId)

SET @equivalentId = 'e485320f-2261-4443-8c89-6c55039b1647'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('library technician', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('library assistant', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('library attendant', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('libraries officer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('library officer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('library worker', @equivalentId)

SET @equivalentId = '8547e73b-0a16-460f-a987-a795860cb25a'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Teacher Aide', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Teachers aide', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Teacher''s Aide', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Teachers'' Aide', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Teachers Assistant', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Teacher''s Assistant', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Assistant Teacher', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('TEACHERS AIDE SPECIAL', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Teacher Assistant', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Teachers aid', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Volunteer Teachers Aid', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('learning support teacher', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Teacher''s aid', @equivalentId)

SET @equivalentId = '4e542132-ecab-4350-8f20-4ecc4d00aec3'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Casual Teacher', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Casual Relief Teacher', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Relief Teacher', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Temporary Relief Teacher', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('CRT', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('CRT teaching', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('CRT teacher', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('casual relief teaching', @equivalentId)

SET @equivalentId = 'aff3f0a7-5e1a-43c9-9554-82118830eca8'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('English Teacher', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('English Language Teacher', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('ESL Teacher', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('esl', @equivalentId)

SET @equivalentId = '39c78491-9fc0-4588-b1e9-216b64ee0aa7'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Computer Teacher', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Information Technology Teacher', @equivalentId)

SET @equivalentId = '44e09d2b-ec36-4399-b19d-57b3c2d8fd08'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('TAFE Teacher', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('TAFE sessional teaching', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('T.A.F.E. Teacher', @equivalentId)

SET @equivalentId = '5cd4f4ce-c90a-4f18-b912-5f292d88a788'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Mathematics Teacher', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Mathematics/Science Teacher', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Maths Teacher', @equivalentId)

SET @equivalentId = '43b0294b-9449-482f-95db-de49b68f198a'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('math', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('mathmatics', @equivalentId)

SET @equivalentId = 'dcc2bf67-a3cb-43bc-8a24-57df570f48f7'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Art Teacher', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Graphic Art Teacher', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Visual Art Teacher', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Teacher Art', @equivalentId)

SET @equivalentId = 'e6bdfcb1-ae39-468c-979a-749d09075f6b'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Piano Teacher', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Music Teacher', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Instrumental Teacher', @equivalentId)

SET @equivalentId = '8c1d5166-5fc2-4bbf-a0e5-6ffd37e2ef49'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Dance teacher', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Belly dance teacher', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('ballroom dance teacher', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('classical dance teacher', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('latin dance teacher', @equivalentId)

SET @equivalentId = '61be67fa-9d9e-4817-855c-d98ea55b5b4f'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('LOTE Teacher', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('L.O.T.E. Teacher', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Foreign Language Teacher', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Chinese Teacher', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Italian teacher', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('japanese teacher', @equivalentId)

SET @equivalentId = '553676b3-0fb7-4df6-bc50-c9391a20ac5d'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Production Technician', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Production Crew', @equivalentId)

SET @equivalentId = 'b56cc189-fe64-400b-8efc-980dde6cc983'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Child Protection Worker', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Child Protection Officer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('cpo', @equivalentId)

SET @equivalentId = '066e3bdf-6071-45a3-9b33-bacd1e532b81'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('lecturer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Associate Lecturer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Assistant Lecturer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Senior Lecturer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Guest Lecturer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('university lecturer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('uni lecturer', @equivalentId)

SET @equivalentId = 'eb0e3fcb-0ae2-45ef-bb11-669c347bee0d'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('professor', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('dean', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('university professor', @equivalentId)

SET @equivalentId = '733ac31e-1f00-4957-9874-347daf9d3096'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('University Tutor', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('uni tutor', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('uni tutoring', @equivalentId)

SET @equivalentId = 'a238b647-17e3-4c0d-bd86-71d82af996cd'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Coordinator', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Co ordinator', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Co-ordinator', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('ordinator', @equivalentId)

SET @equivalentId = '8e786842-75f0-4359-ab5b-f6205728d276'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Child Care Assistant', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Child Care Worker', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('child care', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Child Carer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Qualified Child Care Worker', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Casual child care assistant', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('childcare worker', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('childcare professional', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('creche attendant', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('preschool teacher', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('pre school teacher', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Early Childhood Teacher', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('childcare', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('creche leader', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('creche team leader', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('creche worker', @equivalentId)

SET @equivalentId = '2a39ef9e-1875-4c7b-9f0f-da1b47a2bd91'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('educator', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('tutor', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('trainer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('instructor', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('trainor', @equivalentId)

SET @equivalentId = '6fbecb88-9d28-4a5d-923e-88a5a7439b76'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('kinder', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('kindergarten', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('kinda', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('childcare centre', @equivalentId)

SET @equivalentId = '90064bf3-14da-4ae1-babd-610cbf2b32cf'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('centre', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('center', @equivalentId)

SET @equivalentId = 'f519668b-ae63-4945-98e3-59e48fd57a35'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('child', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('children', @equivalentId)

SET @equivalentId = 'd7e89be2-1c54-46b7-852f-c24573d9e376'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('training manager', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('training leader', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Training and Delivery Manager', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('learning and development manager', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('training coordinator', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('learning manager', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Coodinator of Learning & Development', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Coodinator of Learning and Development', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Learning & Development Manager', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('training co ordinator', @equivalentId)

SET @equivalentId = '632bbcc0-008c-46f5-acc8-2b12b9ef925a'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('training facilitator', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('learning and development facilitator', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('training officer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('learning and development officer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('training consultant', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('learning and development consultant', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('training assistant', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('learning and development assistant', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Learning and Development Specialist', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('training specialist', @equivalentId)

SET @equivalentId = '8c2c223f-a6e5-45ac-9909-d906d611f4fe'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('swim teacher', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('swimming teacher', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('swimming instructor', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('aquatic education', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('aquatic education teacher', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('swim coach', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('swimming coach', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('aquatic education instructor', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('swim instructor', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('swim lesson teacher', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('aquatic officer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('aquatic teacher', @equivalentId)

SET @equivalentId = 'fb632f86-e64d-4b16-aea4-d11c7f5de736'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('lifeguard', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('aquatic supervisor', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('life guard', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('pool guard', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('poolguard', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('lifesaver', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('life saver', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('pool attendant', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('pool lifeguard', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('pool life guard', @equivalentId)

SET @equivalentId = 'c9503108-4ab4-4761-bff4-eb59e18eed20'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('aquatic coordinator', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('aquatic facility assistant', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('pool operations manager', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('pool operations co ordinator', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('pool operations coordinator', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('pool superintendant', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('pool operations attendant', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('aquatic co-ordinator', @equivalentId)

SET @equivalentId = '49491b46-244a-4428-859d-6188e30b913f'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('pool builder', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('pool installer', @equivalentId)

SET @equivalentId = '6c0fb7b1-f8c3-42ff-86d1-7514d51ce18e'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('poolman', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('pool man', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('pool cleaner', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('pool technician', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('pool maintenance', @equivalentId)

SET @equivalentId = 'd604cdc7-c4d5-49e6-87cf-901590718537'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('personal trainer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('fitness trainer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('group fitness trainer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('gym instructor', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('fitness coash', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('fitness technician', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('health and fitness instructor', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('fitness manager', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('fitness coordinator', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('fitness director', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('health club instructor', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('health trainer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('fitness program trainer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('fitness camp instructor', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('fitness instructor', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('fitness co-ordinator', @equivalentId)

SET @equivalentId = 'ac6f1ea4-547d-4b7c-b78a-572d3faa41eb'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('recreation centre manager', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('fitness centre manager', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('swim centre manager', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('rec centre manager', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('centre director', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('leisure centre manager', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('leisure centre director', @equivalentId)

SET @equivalentId = '13352b12-a407-4cdc-9f25-baf1cb5bf8a7'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('masseuse', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('massage therapist', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('massage specialist', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('masseur', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('massage therapy', @equivalentId)

SET @equivalentId = '3d699ccc-f429-409f-b372-2241fc121ca9'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('cpr', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('cardio pulmonary resuscitation', @equivalentId)

SET @equivalentId = 'a850f8a3-62d5-4359-8be3-d241e73ca781'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('environmental', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('sustainability', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('environment', @equivalentId)

SET @equivalentId = 'd0fd15ba-8ff7-42c9-81d0-ca686aa9963c'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('vacation', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('holiday', @equivalentId)

SET @equivalentId = '60066758-e9dc-4488-95c6-ebaca09965e7'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('afl', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('australian football league', @equivalentId)

SET @equivalentId = 'f21dc60c-b838-443e-be16-d38650e13bcf'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('football', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('footy', @equivalentId)

SET @equivalentId = 'a6c04885-d450-47de-a67f-bbc91afce137'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('nrl', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('national rugby league', @equivalentId)

SET @equivalentId = '2ba1f9de-cd95-4866-8958-fe6867cf5304'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('motor sport', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('motorsport', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('car racing', @equivalentId)

SET @equivalentId = 'b7b83e6e-2144-4bff-8be3-906767cc2856'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('nbl', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('national basketball league', @equivalentId)

SET @equivalentId = 'c66514f0-e3a4-4903-8919-0cbc77ace7da'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('nba', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('national basketball association', @equivalentId)

SET @equivalentId = '6ad5aefa-10ba-451b-95d6-c446f45acbb2'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('aoc', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('australian olympic committee', @equivalentId)

SET @equivalentId = '4214f1de-939c-4414-b770-5e6b6cfcf9e0'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('winter sport', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('wintersport', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('skiing', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('snow skiing', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('snowboarding', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('snow boarding', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('snow sport', @equivalentId)

SET @equivalentId = 'dd6130ef-220b-4afc-820a-8d2b85b5d3aa'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('train driver', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('rail driver', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('traindriver', @equivalentId)

SET @equivalentId = '1b4bddd6-e73d-4141-9c35-31523a0c63f6'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('tram driver', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('tramdriver', @equivalentId)

SET @equivalentId = 'c3599883-c9f4-4560-a979-1a418f0eaeee'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('cyclist', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('bike rider', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('bicycle rider', @equivalentId)

SET @equivalentId = 'e36b8619-200a-4424-8a9b-5bc54f6d92ad'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('parking inspector', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('traffic officer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('council parketing inspector', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('infringements officer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('parking enforcement officer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('car parking officer', @equivalentId)

SET @equivalentId = 'e534c568-2964-4eca-b32b-5d2dc5b46fa7'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('carpark manager', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('carpark supervisor', @equivalentId)

SET @equivalentId = '5eb998cc-5afb-4adc-a004-d1ac688e0fa0'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('catering supervisor', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('catering manager', @equivalentId)

SET @equivalentId = '21cd0ff5-b816-4c08-9366-d1179d176100'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('catering assistant', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('catering service assistant', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('catering officer', @equivalentId)

SET @equivalentId = '9b1fcacc-8dc4-4266-b48d-7f13f9f6fac5'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('cellarhand', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('cellar hand', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('cellarman', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('store and cellar', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('cellar man', @equivalentId)

SET @equivalentId = '8a856b1e-6276-47c5-9cde-2c3933747611'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('aquaculture', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('aqua cluture', @equivalentId)

SET @equivalentId = '8387269b-af9f-4e2e-8ac3-637dd1c5cdf5'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('rifle', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('gun', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('firearm', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('fire arm', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('shotgun', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('pistol', @equivalentId)

SET @equivalentId = '4f0b5d88-00e5-4825-b403-0fe5b16ceae3'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('bid', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('proposal', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('tender', @equivalentId)

SET @equivalentId = '72c2bb3f-e4fd-4942-bf83-2dce3f29c28f'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('dress maker', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('dressmaker', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('couture', @equivalentId)

SET @equivalentId = 'abc5a289-deaa-416e-ba82-df9d991dd126'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('fitness first', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('fitnessfirst', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('fitness 1st', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('fitness1st', @equivalentId)

SET @equivalentId = 'bd542fb2-c111-4f88-bc7a-76a339790634'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('project development officer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('project development coordinator', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('project development co-ordinator', @equivalentId)

SET @equivalentId = '1c707568-ecac-48a7-83a3-29ba48e696c2'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('subcontractor', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('sub contractor', @equivalentId)

SET @equivalentId = '1e07ce4a-44aa-43c7-a32f-96038db894e5'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('fernwood', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('fern wood', @equivalentId)

SET @equivalentId = '83a2527a-e908-4a98-8e56-02d3e964541d'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('ymca', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('the y', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('young men''s christian association', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('y m c a', @equivalentId)

SET @equivalentId = '54a7eee8-7205-4439-aed0-34e73cb54d66'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('oshc', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('out of school hours care', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('before school care', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('after school care', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('bsc', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('holiday care', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('holiday program', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('oshc leader', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('oshc manager', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('oshc assistant', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('oshc asst', @equivalentId)

SET @equivalentId = 'cf361a7b-514e-4929-8c56-7529a120d7dc'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('concierge', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('bell attendant', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('porter', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('bell hop', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('bellhop', @equivalentId)

SET @equivalentId = '68e0d4d8-43c3-4b3a-afbe-720b72673dba'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('tour guide', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('tour director', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('bus guide', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('tourguide', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('resort guide', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('tour driver', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('tour driver guide', @equivalentId)

SET @equivalentId = 'f8d77597-bb76-418e-bc61-d0b384878d79'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Cleaner', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Casual Cleaner', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Contract Cleaner', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Part time Cleaner', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Commercial Cleaner', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Office Cleaner', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Night Cleaner', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('House Cleaner', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Housekeeper', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('House keeper', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Domestic Cleaner', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Contract Domestic Cleaner', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Commercial / Industrial Cleaner', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Industrial Cleaner', @equivalentId)

SET @equivalentId = 'ad8f9490-bf45-4d02-adc2-6e1307c51c75'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Head cleaner', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Cleaning Manager', @equivalentId)

SET @equivalentId = 'e66618f7-0ab0-4eb3-a2b9-2fb6b7c40dfa'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Shopping Centre Cleaner', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('centre cleaner', @equivalentId)

SET @equivalentId = '7c8035d6-700f-471c-8056-fe5fb985be4b'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Support Worker', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Disability Support Worker', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Lifestyle Support Worker', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Community Support Worker', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Youth Support Worker', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Residential Support Worker', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Personal Support Worker', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('welfare worker', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('care worker', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('personal carer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('personal care', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('carer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('home carer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('community development worker', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('community participation worker', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('community development officer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('community development coordinator', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('community development practictioner', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('pca', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('personal care assistant', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('p c a', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('disability carer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('disability care specialist', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('personal care attendant', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('community development co-ordinator', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Aged care worker', @equivalentId)

SET @equivalentId = '84124173-69c5-4d5a-b22b-18e87b139a93'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('acfi', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Aged Care Funding Instrument', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('ACFI Specialist', @equivalentId)

SET @equivalentId = 'f78e4178-6d53-4ac6-87c7-063790db9d7e'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Healthcare', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Health Care', @equivalentId)

SET @equivalentId = '96ff12b9-72f7-49cf-a8ed-c1fa01f3e4c9'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('case manager', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('case worker', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('case officer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('inury manager', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('rtw officer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('case coordinator', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('elo', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('employment liaison officer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('social worker', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('case management officer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('case management coordinator', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('case management co ordinator', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('case management worker', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('case management consultant', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('career recruitment officer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('career recruitment worker', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('career recruitment coordinator', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('job placement coordinator', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('case administrator', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('employment consultant', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('case service officer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('employment coordinator', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('case management', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Employment Placement Officer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('epo', @equivalentId)

SET @equivalentId = '812a5add-114e-4d00-a360-4e1a52eca504'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('aged care', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('agedcare', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('elderly care', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('elderlycare', @equivalentId)

SET @equivalentId = '1ca86f4a-de22-437f-9297-c7644fec0278'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('disability', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('handicapped', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('handicap', @equivalentId)

SET @equivalentId = '318e37b4-3d32-4245-94c2-23dc5d17d83d'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('hospice', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('palliative', @equivalentId)

SET @equivalentId = 'c6a3e7bf-03a6-492a-80d9-f22746a81f3a'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('worker', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('officer', @equivalentId)

SET @equivalentId = 'c9db74d7-ad53-4244-9ef4-9617063fee8f'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('youth', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('adolescent', @equivalentId)

SET @equivalentId = '1c9e6aa5-25c9-4610-982b-61f69d3f0933'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('family services', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('community services', @equivalentId)

SET @equivalentId = 'cb039f0c-770e-4411-a523-9164b60cad1e'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('housing officer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('housing services officer', @equivalentId)

SET @equivalentId = '96ad52f9-926e-4919-a5a0-b0583727248e'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('wellness', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('fitness', @equivalentId)

SET @equivalentId = '11e07714-c718-46fb-bfa0-d8454bb47d2b'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('crisis response officer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('crisis support officer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('crisis assistance officer', @equivalentId)

SET @equivalentId = '9247e361-8b03-4e8f-8614-7bdba93bc983'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('maternal health', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('mothercraft', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('mother craft', @equivalentId)

SET @equivalentId = '39eb1515-8b40-4c70-8ea9-8b8f31be6754'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('zookeeper', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('zoo keeper', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('animal keeper', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('animal handler', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('animal carer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('animal technician', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('animal management officer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('animal attendant', @equivalentId)

SET @equivalentId = '7c7c3efb-a7dc-4dac-b0bc-a8118e4845b7'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('pco', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('professional conference organiser', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('professional conference organizer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('conference program manager', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('conference organiser', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('conferencing account manager', @equivalentId)

SET @equivalentId = '99c2d23f-5a83-40e8-bf67-740576c7fb01'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('beautician', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('beaty therapist', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('beauty therapist', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('beauty consultant', @equivalentId)

SET @equivalentId = 'd1371f22-a10b-438f-90d6-29ba26493ed2'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('garbage collector', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('garbo', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('garbage truck driver', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('garbage supervisor', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('refuse collector', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('refuse truck driver', @equivalentId)

SET @equivalentId = '9054919d-3a8a-42c6-8591-fb489cfabe1a'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('typesetter', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('type setter', @equivalentId)

SET @equivalentId = 'e049b7ac-b37e-4cc2-a340-b9783005acf2'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('priest', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('vicar', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('minister of religon', @equivalentId)

SET @equivalentId = '32dc906c-6521-4a50-8b89-f590f6a86bc2'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('mower man', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('mowerman', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('lawn mower', @equivalentId)

SET @equivalentId = '7bc12abd-c3ad-45f7-a08c-687d2f1cfdc8'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('signwriter', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('sign writer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('sign designer', @equivalentId)

SET @equivalentId = 'd2235d41-e8a9-4fd8-b9c4-713aa5510b9b'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('funeral director', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('funeral assistant', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('funeral coordinator', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('funeral consultant', @equivalentId)

SET @equivalentId = 'be6f877a-154e-4d97-bd3a-50f63236c7d6'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('vigneron', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('winemaker', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('wine maker', @equivalentId)

SET @equivalentId = 'a2bdb1d0-180e-4277-a8da-8273612c6076'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('dj', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('disc jokey', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('radio announcer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('disc jockey', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('voice over announcer', @equivalentId)

SET @equivalentId = 'edce3407-e9ff-4752-a8b1-942b780c1533'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('arborist', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('tree doctor', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('tree worker', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('tree climber', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('tree lopper', @equivalentId)

SET @equivalentId = 'add279dd-ecb0-4cac-aa09-9b8974b0a63a'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('carpark attendant', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('carpark operator', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('car park attendant', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('car park operator', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('parking attendant', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('parking staff', @equivalentId)

SET @equivalentId = 'e0d2b9be-1495-4db0-9e61-7df7dec6235f'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('postman', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('post man', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('postie', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('postal officer', @equivalentId)

SET @equivalentId = '07635619-900c-4de2-93f9-ea6bce99ea7a'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('horticulturalist', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('horti culturalist', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('horticulture', @equivalentId)

SET @equivalentId = 'ab8fe373-5af2-4f8d-87ab-b001a92967f7'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('armaguard', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('arma guard', @equivalentId)

SET @equivalentId = '5024ec58-a958-4dd0-85dd-d7a6a536f901'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('valuer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('valuation', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('property valuer', @equivalentId)

SET @equivalentId = '80aa2efa-e672-4cb1-b4e7-f0744e0c9cfd'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('diesetter', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('die setter', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('die operator', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('diecast setter', @equivalentId)

SET @equivalentId = 'f4a17705-fc72-430c-81e9-97b7df15469c'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('marine engineer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('maritime engineer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('marine technician', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('maritime technician', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('tugboat engineer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('tug boat engineer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('ship engineer', @equivalentId)

SET @equivalentId = '4a97725a-38a2-4db5-9b5f-dd43ddbbd2ae'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('airtraffic controller', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('air traffic controller', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('air traffic services officer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('air traffic', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('airtraffic', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('air traffic scheduler', @equivalentId)

SET @equivalentId = '96eedc55-e877-4430-a274-091fee9e4800'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('baggage handler', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('baggagehandler', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('luggage handler', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('baggage coordinator', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('ground handler', @equivalentId)

SET @equivalentId = '897b9247-be74-4de8-a929-ddb43ea9ca38'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('customs officer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('quarantine officer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('customs clerk', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('customs controller', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('customs supervisor', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('customs inspector', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('quarantine inspector', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('quarantine inspection officer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('quarantine clerk', @equivalentId)

SET @equivalentId = 'd791e5e7-af3f-4f6e-9f3d-57df45e19cf6'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('deckhand', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('deck hand', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('integrated rating', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('able bodied seaman', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('greaser', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('ab', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('general purpose hand', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('gp hand', @equivalentId)

SET @equivalentId = '8ec2f08f-308a-44c9-b808-f0f51309923b'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('skipper', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('master v', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('master 5', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('master 4', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('master4', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('master5', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('masterv', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('coxswain', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('ship captain', @equivalentId)

SET @equivalentId = '7c564152-9228-4046-9d2c-a62c7c6f3196'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('cabin crew', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('cabincrew', @equivalentId)

SET @equivalentId = 'f58e4828-8012-4334-88ac-b57bcf1d77b0'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('steward', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('caterer', @equivalentId)

SET @equivalentId = 'd4413522-e053-48bd-8317-040208910539'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('course super', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('course superintendant', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('golfcourse superintendant', @equivalentId)

SET @equivalentId = 'e4e74686-7e64-4f24-aa6e-4ac24e7afb75'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('golf pro', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('golf professional', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('golfpro', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('pga professional', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('golf coach', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('golfing professional', @equivalentId)

SET @equivalentId = '723bcddb-7ad8-4c57-bce7-23bb3801e557'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('golf shop', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('pro shop', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('proshop', @equivalentId)

SET @equivalentId = '650a7c12-30c6-4401-a838-74da7ead0b3e'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('golf manager', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('golfclub manager', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('golf club manager', @equivalentId)

SET @equivalentId = '16c2707e-42de-49cb-91cc-a233e3a4674f'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('pest controller', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('pest technician', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('pest control officer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('pest control technician', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('pest officer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('pest control', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('pest control assistant', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('pest manager', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('pest control manager', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('pest control operator', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('pest inspector', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('vermin controller', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('vermin control', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('vermin control inspector', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('vermin control officer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('ferel exterminator', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('pest exterminator', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('pest extermination', @equivalentId)

SET @equivalentId = '1c5e46af-bc9d-4ee8-af69-5e494c09d59a'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('fire safety officer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('fireman', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('fire fighter', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('firey', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('fire man', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('pompier', @equivalentId)

SET @equivalentId = '3e192a89-53f5-4a42-8da6-e8df11492f29'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('dry cleaner', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('drycleaner', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('drycleaning', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('dry cleaning', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('dri cleaner', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('dricleaner', @equivalentId)

SET @equivalentId = '074cef55-da19-4c69-9acc-2a90406c9e25'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('security installer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('security technician', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('signwriter/painter', @equivalentId)

SET @equivalentId = 'e96837fe-0752-492e-93fb-f13fe1811798'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('parts interpreter', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('spare parts interpreter', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('parts cataloguer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('parts clerk', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('parts coordinator', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('parts co ordinator', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Parts Interrupter', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('parts assistant', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('telephone parts sales', @equivalentId)

SET @equivalentId = '3c3965bb-5da8-4988-92e9-849134722f6a'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('real estate agent', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('officer in effective control', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('realestate consultant', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('property consultant', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('buyers advocate', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('buyer s advocate', @equivalentId)

SET @equivalentId = '744b0035-72df-4052-8e0a-f583c6e79196'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Regional Survey Manager', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('State Survey Manager', @equivalentId)

SET @equivalentId = '65ed7113-e60c-4945-90e5-e4e20f79ccb7'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Policy Manager', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Polices Manager', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Senior Policy Officer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Senior Policies Officer', @equivalentId)

SET @equivalentId = '5b91d849-2380-4b67-93c0-6cf09c2a8f8c'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Unit Manager', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Unit Operations Manager', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Unit Director', @equivalentId)

SET @equivalentId = 'ee46a592-a00b-4859-b2cf-13f10420420a'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Assistant', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Asst', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Asst.', @equivalentId)

SET @equivalentId = '776c2f33-9a2f-4ebd-9ca3-cbb968e21630'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('administrator', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('ADMINISTRATIVE MANAGER', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('BUSINESS ADMINISTRATOR', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Senior Administration Officer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Business Administration', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Executive Administrator', @equivalentId)

SET @equivalentId = '85a26bdc-2449-40ef-bd40-c4e9abb98277'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Operations Administrator', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Operations Admin', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('ops admin', @equivalentId)

SET @equivalentId = 'd3b5162b-2580-4cff-9cef-75c9a4e91314'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('house wife', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('housewife', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('domestic manager', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('home manager', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('domestic duties', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('homemaker', @equivalentId)

SET @equivalentId = 'e584bb53-ff30-437c-a240-f6b9f0f42de1'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Security Officer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Security Guard', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Security Officer/Crowd Controller', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Security Officer/Patrolman', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('security guard / crowd controller', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Corporate Security', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('crowd controller', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('bouncer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('patrol car driver', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('cash in transit', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('cashintransit', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('cash and transit', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('monitoring centre operator', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('monitoring centrer operator', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('response centre operator', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('response center operator', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('alarm centre operator', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('alarm center operator', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('static guard', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('loos prevention offcier', @equivalentId)

SET @equivalentId = '75b2b129-6e3b-419a-9a82-5d3e5613b074'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Proprietor', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Sole Proprietor', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Business Proprietor', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Manager/Proprietor', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Joint Proprietor', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Owner - Manager', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Business Owner/Operator', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Small business owner', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Owner-Operator', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('shareholder', @equivalentId)

SET @equivalentId = 'ca2d1ded-e207-49fe-8196-6f727a80de2a'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Owner/Manager', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Owner/Operator', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Owner / Manager', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Owner Operator', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Business Owner', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('owner manager', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Owner / Operator', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Owner/ Manager', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('OWNER/DIRECTOR', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Manager/Owner', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Owner/ Operator', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Director/Owner', @equivalentId)

SET @equivalentId = '23c0e102-4de0-47bd-b764-13619c001b62'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('technician', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('tech', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('tec', @equivalentId)

SET @equivalentId = 'afdc3c87-007b-44fc-ae8f-6795241cc97a'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Delivery Driver', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Pizza delivery driver', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Multi-drop Delivery Driver', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Delivery Driver/Kitchen hand', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Driver', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('courier', @equivalentId)

SET @equivalentId = '551f167f-2637-4815-b610-67a33173ea93'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Bus Driver', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Coach Driver', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('school bus driver', @equivalentId)

SET @equivalentId = '49b1091c-d5ef-4d9a-9582-ab1325dae7e3'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Taxi Driver', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Cab Driver', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('HGV Driver', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('cabbie', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('taxidriver', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('taxi operator', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('cabdriver', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('chauffer', @equivalentId)

SET @equivalentId = 'd351a0f5-34ee-44b2-a5fa-ef4786fb814e'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Interstate Truck Driver', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Truck Driver/Plant Operator', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Truck Driver', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('truckdriver', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('truckie', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('dump truck operator', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('truck driving', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('line haul', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('linehaul', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('long haul', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('linehaul driver', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('line haul driver', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Freight Driver', @equivalentId)

SET @equivalentId = '55ec8577-cf59-4565-bf07-04d08f04813f'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Hyperbaric Technician', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Hyperbaric operator', @equivalentId)

SET @equivalentId = '33d2f187-0fa3-4c19-8773-87183fda39cc'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Work Experience', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Work Experience Student', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('WORK EXPERIENCE - TAFE', @equivalentId)

SET @equivalentId = '15ee0715-6508-4a8a-8a01-2db2fbba7e3f'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Trainee', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Traineeship', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Student trainee', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Graduate Trainee', @equivalentId)

SET @equivalentId = '15a054a2-5cc1-4585-83c1-3488623e64f4'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Volunteer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Volunteer Worker', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Volunteer Member', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('NSW Fire Brigade', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('St John''s Ambulance', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('State Emergency Service', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('SES', @equivalentId)

SET @equivalentId = '17f9e995-1769-4b23-af23-2f04a4ddb18b'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('segment manager', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('category manager', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('brand manager', @equivalentId)

SET @equivalentId = '715c3673-7861-448c-992e-6ab284110cb4'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Computer Operator', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Senior Computer Operator', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Trainee Computer Operator', @equivalentId)

SET @equivalentId = 'e0992749-06a6-4b13-bccb-96cf21889609'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('photographer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('photo taker', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('freelance photographer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('photography', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('professional photographer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('contract photographer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('photog', @equivalentId)

SET @equivalentId = '28dbdab2-eb9a-41c4-9d43-9cb0ac73973d'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Technical Consultant', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Senior Technical Consultant', @equivalentId)

SET @equivalentId = '611a8a12-6cdb-4262-bfee-027272c18ce1'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Various', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('varied', @equivalentId)

SET @equivalentId = 'c63d02b3-5658-41e7-9c97-c5b49db2a858'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Business Consultant', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Senior Business Consultant', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('New Business Consultant', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Telephone Business Consultant/Customer Service Representive', @equivalentId)

SET @equivalentId = '05272ca1-80b9-484d-9ee3-ea33b2ef814a'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('dialler', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('power dialler', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('predictive dialler', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('automated dialler', @equivalentId)

SET @equivalentId = 'c2b8ae86-3d47-4dd0-a4b7-c5ad742dbeb6'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Translator', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Interpreter', @equivalentId)

SET @equivalentId = 'cc370268-b107-4754-a175-b92beeb641c7'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('pattern maker', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('pattermaker', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('pattern designer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('pattern design', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('pattern making', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('pattern designing', @equivalentId)

SET @equivalentId = '18931b76-d294-4063-b853-085420de207c'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('textile designer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('textile design', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('textile designing', @equivalentId)

SET @equivalentId = 'fb26ee23-ba77-4928-950f-911514f42d7d'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Folder & Stitcher operator', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Folder and Stitcher operator', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Folder / Stitcher operator', @equivalentId)

SET @equivalentId = 'caeb5715-5f58-4e67-a186-6cd57febeede'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('ticket inspector', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('gumby', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('ticket officer', @equivalentId)

SET @equivalentId = '90afde08-ea15-4cf2-ab72-53e67f920599'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Manicurist', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Nail Technician', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Hand technology', @equivalentId)

GO
