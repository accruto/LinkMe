DECLARE @equivalentId UNIQUEIDENTIFIER

DELETE FROM dbo.EquivalentTerms

SET @equivalentId = '06b2ca7b-fb06-413d-bd46-7dadcc1a278f'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('1', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('one', @equivalentId)

SET @equivalentId = 'f163d410-56b6-4375-8536-9775201990a5'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('2', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('two', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('ii', @equivalentId)

SET @equivalentId = '5831f59f-bd0e-4a99-93ee-3c06d2dd3ad4'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('3', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('three', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('iii', @equivalentId)

SET @equivalentId = '4857fbc6-ec5e-4391-a95b-960559646512'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('4', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('four', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('iv', @equivalentId)

SET @equivalentId = '44d1f7ae-42f9-4741-90a7-6e25bb7d42f6'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('5', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('five', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('v', @equivalentId)

SET @equivalentId = 'f0b9317b-0051-4a29-a06c-0c1c1695edb3'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('6', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('six', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('vi', @equivalentId)

SET @equivalentId = '0b3a6a81-f391-4e35-b8d2-a9159d577c44'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('7', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('seven', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('vii', @equivalentId)

SET @equivalentId = '8ba53b8f-87db-4461-a0e4-7e1d35908c4c'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('8', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('eight', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('viii', @equivalentId)

SET @equivalentId = '524c1d08-bb85-4551-900a-b635c8490117'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('9', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('nine', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('ix', @equivalentId)

SET @equivalentId = '199e9735-d72a-4acd-9159-ce307411da51'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('10', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('ten', @equivalentId)

SET @equivalentId = '0d19b22b-c13e-4645-9b58-cdb2ed065264'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('1st', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('first', @equivalentId)

SET @equivalentId = '26a2575a-8b51-44a4-9f07-69fab8d87114'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('3ic', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('3.i.c.', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('3i.c.', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Third in charge', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('3 IC', @equivalentId)

SET @equivalentId = '400a97d1-a888-4e66-a698-3935ca3685a9'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('2nd', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('second', @equivalentId)

SET @equivalentId = 'fbeb1855-6939-45c0-9fbc-476e16f32618'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('3rd', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('third', @equivalentId)

SET @equivalentId = 'b01ffbdc-0ba4-4b90-9f75-fd7ed12c3777'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('4th', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('forth', @equivalentId)

SET @equivalentId = '129361f2-0bb5-4f7a-9843-96b07d0d7807'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('class 1', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('class1', @equivalentId)

SET @equivalentId = '1ed06813-7eaf-4fce-a93b-fdc36ed68ae2'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('class 2', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('class2', @equivalentId)

SET @equivalentId = 'c809f438-6e8b-4a9f-9052-3e89ae3256d1'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('class 3', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('class3', @equivalentId)

SET @equivalentId = '1aefecd5-5c80-4de3-81d2-9b0af7bcfe6d'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('class 4', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('class4', @equivalentId)

SET @equivalentId = '9d912d14-149d-4b0a-b12b-febdceeb1e3a'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('class 5', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('class5', @equivalentId)

SET @equivalentId = '2cbe49f4-f538-434c-b183-e844936aa730'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('phase 1', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('phase1', @equivalentId)

SET @equivalentId = '4feac893-7d57-4de7-bc14-8c25a454e2cb'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('phase 2', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('phase2', @equivalentId)

SET @equivalentId = '7a331676-136f-4178-a067-11d71ac41eb6'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('phase 3', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('phase3', @equivalentId)

SET @equivalentId = 'deaf9f6c-d7bc-445c-8fd2-657947d584fb'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('phase 4', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('phase4', @equivalentId)

SET @equivalentId = 'cda2393e-bd75-4f2c-9de5-83d4d18bbc46'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('phase 5', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('phase5', @equivalentId)

SET @equivalentId = 'cc5fa696-2674-4a4a-ab81-67f6a6c1366e'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('after sales', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('aftersales', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('post sales', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('postsales', @equivalentId)

SET @equivalentId = '47266eaa-f7f3-458d-8ae2-04dde3b12816'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Aboriginal', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('indigenous', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('torres straight islander', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('torres straight islands', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Koori', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('australian aboriginal', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Torres Strait Islander', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('koorie', @equivalentId)

SET @equivalentId = 'b9480e26-01c5-466b-b91f-6fb35d74a149'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('ADSL', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Asymmetric Digital Subscriber Line', @equivalentId)

SET @equivalentId = '68b981c2-db67-43e9-9b01-2c0c1324c73c'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('fibre optic', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('fibreoptic', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('optical fibre', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('optical fiber', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('fiber optic', @equivalentId)

SET @equivalentId = '9c2d4fca-fea0-4584-bec2-9750143e6a1e'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('ag', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('agriculture', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('agricultural', @equivalentId)

SET @equivalentId = '58358468-7d85-471e-bcc1-a166d92d6297'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('mx road', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('mxroad', @equivalentId)

SET @equivalentId = '736d64d2-f987-45b0-892e-fab0ce2b681a'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('substation', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('sub station', @equivalentId)

SET @equivalentId = 'a11a8f5c-4f11-4c58-bd49-7c8a0e2e10e6'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('qem', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('qem scan', @equivalentId)

SET @equivalentId = '47aa2e26-e300-4566-9785-11456ee85cec'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('cold store', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('coldstore', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('coolroom', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('cool room', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('cold storage', @equivalentId)

SET @equivalentId = '4a895d9d-5d9f-45b7-82d0-ad13610f1b6f'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('asap', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('as soon as possible', @equivalentId)

SET @equivalentId = '9daf62d1-af05-43c6-9c58-9b1fab83b45b'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('ASX', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Australian stock exchange', @equivalentId)

SET @equivalentId = '100ef939-2589-4dcb-81c8-9ea5427b04ee'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('aust', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('australia', @equivalentId)

SET @equivalentId = 'a92cdff3-9358-431c-bc39-9e9aba1d0643'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('bas', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('business activity statement', @equivalentId)

SET @equivalentId = '702dcbd7-eebb-4c6f-a6c4-f743c313c416'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('BHP', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('BHPBilliton', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('bhp billiton', @equivalentId)

SET @equivalentId = '3d3c896b-6c1e-49b5-bdd6-c94d34eb1dc5'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('rio tinto', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('riotinto', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('rtz', @equivalentId)

SET @equivalentId = 'a5258d17-eec1-4504-a371-e1874f10e5a4'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('ghd', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Gutteridge Haskins & Davey', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Haskins and Davey', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Gutteridge Haskins and Davey', @equivalentId)

SET @equivalentId = 'e2803924-603d-4d58-8f95-869be3fc4f37'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('bris', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('brisbane', @equivalentId)

SET @equivalentId = '821b16a4-35d2-4665-bd8a-4720d52e7205'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('cam', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('computer aided manufacturing', @equivalentId)

SET @equivalentId = '52a1b53f-7641-4ccc-9c1d-aabe7ccd92ee'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('cbd', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('central business district', @equivalentId)

SET @equivalentId = 'd5aa4fc8-a3d6-4fa5-bb8c-0c549d6daf60'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('cctv', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('closed circuit tv', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('closed circuit television', @equivalentId)

SET @equivalentId = '1a8d9cb0-b867-4841-b067-577b9be95778'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('cert', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('certificate', @equivalentId)

SET @equivalentId = 'ee22e754-e5ef-4680-806f-28254d9c2e08'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('cert1', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('certificate 1', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('certi', @equivalentId)

SET @equivalentId = '95f855b9-84b3-4af5-8f1b-ccc8752efdbd'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('cert2', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('certificate 2', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('certii', @equivalentId)

SET @equivalentId = 'dbe3c519-7d9a-4255-b6f3-9cddc1c40852'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('cert3', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('certificate 3', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('certiii', @equivalentId)

SET @equivalentId = 'de2bea25-c2dc-47b3-ad33-830b04de5c38'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('contract', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('agreement', @equivalentId)

SET @equivalentId = '920348f4-9c0e-41d4-a2aa-853e1d09a575'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('CPU', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('central processing unit', @equivalentId)

SET @equivalentId = '70144523-16a1-4d95-8caf-711afea68b4b'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('dispatch', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('despatch', @equivalentId)

SET @equivalentId = '755caf98-cb40-4e80-9f1c-22bfde4eef66'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('div', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('division', @equivalentId)

SET @equivalentId = '89c3f118-6113-4dfe-815d-a480ea5af76a'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('DNS', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Domain Name System', @equivalentId)

SET @equivalentId = 'c5e9c5fc-0144-49de-8ee8-3814f7675896'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('drp', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('disaster recovery plan', @equivalentId)

SET @equivalentId = '7c5645fe-dd14-4704-9684-98ce6cd4c275'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('DSL', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Digital Subscriber Line', @equivalentId)

SET @equivalentId = '1fad9f97-52bd-4604-8ac4-078fe1be5443'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('email', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('e-mail', @equivalentId)

SET @equivalentId = '9f9c723f-104e-49f3-9147-428e113022e5'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('epcm', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Engineering Procurement Construction Management', @equivalentId)

SET @equivalentId = '24607f6d-fb76-4371-b68d-d8068a7a8ae7'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('EPS', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('earnings per share', @equivalentId)

SET @equivalentId = '48dfe97e-a7dd-4a91-90fd-e5ca50e8a52b'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('exp', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('experience', @equivalentId)

SET @equivalentId = 'aa2525dc-979e-46e8-89fa-869d98a207df'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('fifo', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('first in first out', @equivalentId)

SET @equivalentId = '406304f6-ad95-45d1-8d00-5aad94cf803d'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('govt', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('government', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('gov''t', @equivalentId)

SET @equivalentId = '41cda537-af4d-4f7e-a941-0413894c0b07'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('GPS', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Global Positioning System', @equivalentId)

SET @equivalentId = 'f9cfb2ea-cf4d-4099-97f6-171105ee992f'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('guard', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('gaurd', @equivalentId)

SET @equivalentId = 'd48f7633-60a2-4054-a4ba-d1bd8d101a07'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('hp', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('hewlett packard', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('hewlettpackard', @equivalentId)

SET @equivalentId = '791480e5-99d4-4045-bf0c-7d3bd75ac7bb'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('hris', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('human resource information system', @equivalentId)

SET @equivalentId = '8032c637-6b94-4462-b66c-4f564fb92245'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('infra structure', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('infrastructure', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('infra-structure', @equivalentId)

SET @equivalentId = '846dcaad-faf4-4820-82cf-e7ac00cc6357'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('jdedwards', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('jd edwards', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('j.d.edwards', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('jde', @equivalentId)

SET @equivalentId = '3e9e8774-a242-4d57-9086-2136a55ebcd2'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('jnr', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('junior', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('jr', @equivalentId)

SET @equivalentId = 'a2c0531f-7e26-41df-95d6-123ecc9024e2'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('KPI', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('key performance indicator', @equivalentId)

SET @equivalentId = '8398ee7e-8194-42f8-93f3-715d3cc182bb'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('l&d', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('learning and development', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('training', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('l and d', @equivalentId)

SET @equivalentId = 'd0d6429a-da43-435d-aebd-70512ebafab9'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('labor', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('labour', @equivalentId)

SET @equivalentId = '33713c0b-80a4-49e3-89b8-549f7880e7c2'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('lan', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('local area network', @equivalentId)

SET @equivalentId = '57e73cc0-4484-460b-9145-2a7dacad7aae'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('ltd', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('limited', @equivalentId)

SET @equivalentId = '011ff520-2c43-4824-b79b-b395b9ee1e7d'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Mac', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Macintosh', @equivalentId)

SET @equivalentId = '77495053-228b-4820-b1ab-f5ead5514838'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('macq', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('macquarie', @equivalentId)

SET @equivalentId = '75e07fbf-b5af-4907-9ffc-927c5ab32279'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('mba', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('master of business administration', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('master of business', @equivalentId)

SET @equivalentId = '3e4a428c-e50d-4808-a9d0-ba65205efd09'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('mech', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('mechanical', @equivalentId)

SET @equivalentId = '293b38da-7914-4d5e-9ccb-e6b887c45023'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('med', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('medical', @equivalentId)

SET @equivalentId = '0cf31c9a-8b10-42c0-9b57-1c8d53f2864e'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('melb', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('melbourne', @equivalentId)

SET @equivalentId = '44c79de7-1cd7-49cb-b352-6a21eb60470e'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('mngt', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('management', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('mgmt', @equivalentId)

SET @equivalentId = '6bbd6892-9165-4380-ae23-d41cd826fd77'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('mr', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('medium rigid', @equivalentId)

SET @equivalentId = '93be2900-524e-43de-a6bb-4c850171eca0'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('ms', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('microsoft', @equivalentId)

SET @equivalentId = '358c147e-611d-49ae-88b0-7d55feaeea04'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('NAB', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('National Australia Bank', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('National Bank of Australia', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('National Bank', @equivalentId)

SET @equivalentId = '6d738c93-d0c5-46ae-ad46-941e37b4cac7'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('abigroup', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('abi group', @equivalentId)

SET @equivalentId = '670c7089-c273-42b2-ae64-fe5277490647'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('north east', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('northeast', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('north-east', @equivalentId)

SET @equivalentId = '10a47c3a-2b76-4d7c-95b2-2690bf0a171e'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('neg', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('negotiable', @equivalentId)

SET @equivalentId = '7acf74fc-b293-4903-a0ed-97152775b3db'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('nsw', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('new south wales', @equivalentId)

SET @equivalentId = '5ecb4961-3c40-46a8-82c1-ad288c3c1a6f'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('north west', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('northwest', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('north-west', @equivalentId)

SET @equivalentId = 'a4c533ab-3057-4748-8688-db3f876fa51d'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('nyse', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('new york stock exchange', @equivalentId)

SET @equivalentId = 'f3819d40-8128-4240-a2c1-be3408b4e546'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('nz', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('new zealand', @equivalentId)

SET @equivalentId = '312a557a-01ee-4434-af89-d0d0de6c5418'

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

SET @equivalentId = 'b9a421e8-0bdb-424b-9b6f-b1fc6f6c1617'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Oil and Gas', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Oil&Gas', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Oil & Gas', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('petroleum', @equivalentId)

SET @equivalentId = '27b9defe-6764-4b43-b834-6cfe11375243'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('online', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('on-line', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('digital', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('internet based', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('web', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('internet', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('world wide web', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('www', @equivalentId)

SET @equivalentId = 'e521df2d-f4e2-4116-b21d-f54483c175b8'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('website', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('web site', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('internet site', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('webpage', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('web page', @equivalentId)

SET @equivalentId = '458fd1a8-9ed2-4f1a-bc47-b271e8841ea6'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('os', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('operating system', @equivalentId)

SET @equivalentId = '73892798-5022-4f09-823e-4b183bd534c0'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('ot', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('occupational therapist', @equivalentId)

SET @equivalentId = '586e6e69-86ef-42bc-8acd-276af18f62dc'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('p&l', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('profit and loss', @equivalentId)

SET @equivalentId = '814bee49-b0ca-4e97-8b44-2fc2e9d9286a'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('pae', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('participating administrative entity', @equivalentId)

SET @equivalentId = 'f5857f04-3e3a-4716-a7af-f86ffb19d104'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('payg', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('pay as you go', @equivalentId)

SET @equivalentId = '4a15c50e-08d7-4494-af14-da781915fbd1'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('perm', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('permanent', @equivalentId)

SET @equivalentId = '35c3d3d5-8601-4564-94e8-9bd19f053257'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('pi', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('personal injury', @equivalentId)

SET @equivalentId = 'acdacab9-2907-4a36-8562-9c458f01342f'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('plc', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('programmable logic controller', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('programmed logic controller', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('programmable logic control', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('programmed logic control', @equivalentId)

SET @equivalentId = '45a20447-9c0b-486e-bb38-7ac304de0f27'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('POS', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Point of Sale', @equivalentId)

SET @equivalentId = 'af53c0cd-246d-40b6-8dd8-b0ffc83dbc1a'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('proactive', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('pro active', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('pro-active', @equivalentId)

SET @equivalentId = 'f9c92699-7077-4737-ad28-c975a46a2dc1'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('pty', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('proprietary', @equivalentId)

SET @equivalentId = '946942e2-736c-4c5e-9886-b5ce6e28bee3'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Q&A', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Quality and Assurance', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Quality & Assurance', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('qa', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('quality assurance', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('quality control', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('qc', @equivalentId)

SET @equivalentId = 'cbe6a46b-3772-489c-aff0-6b4254f76e60'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('qld', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('queensland', @equivalentId)

SET @equivalentId = 'e232ea6e-e5f8-46e9-8d8e-19a4a7c43303'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('qual', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('qualification', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('quals', @equivalentId)

SET @equivalentId = 'fae1fc03-e1f8-4946-82f8-b82257da041c'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('r and d', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('research and development', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('research & development', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('rnd', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('r & d', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('r&d', @equivalentId)

SET @equivalentId = '033d5031-2f43-40dc-a7ba-81f9bb7449c9'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('reengineering', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('re engineering', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('re-engineering', @equivalentId)

SET @equivalentId = 'd455d7c0-a800-4f03-b26c-01d08db1f12e'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('ref', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('reference', @equivalentId)

SET @equivalentId = '7f6db0f4-7069-4cec-bbb6-6f526f48ccd6'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('req', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('requirement', @equivalentId)

SET @equivalentId = 'a908bea0-850c-4276-a479-1eff0af0689a'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('rf', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('radio frequency', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('radio transmission', @equivalentId)

SET @equivalentId = 'aa024b8a-d11d-46a6-a4cc-fc4838831472'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('ROE', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('return on equity', @equivalentId)

SET @equivalentId = 'a7a9c68a-81bf-4329-a5d7-469ee47c58ea'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('ROI', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('return on investment', @equivalentId)

SET @equivalentId = '58bef5d7-2a19-4ce8-b3b6-0cd5514eda8c'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('romp', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Radiation Oncology Medical Physics', @equivalentId)

SET @equivalentId = '1fe53e5b-e3f2-4c73-88c8-da6b20ca4ce1'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('rta', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('road traffic authority', @equivalentId)

SET @equivalentId = '0869ef9b-9268-489c-8ef5-486cbf3cabbf'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('rtw', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('return to work', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('return-to-work', @equivalentId)

SET @equivalentId = 'df468268-8add-4e29-87e7-4a2236024cf5'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('sov', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('sale of value', @equivalentId)

SET @equivalentId = '4d1e6071-3c45-45a9-a301-bfe76ee6ec2c'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('sap', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('s.a.p.', @equivalentId)

SET @equivalentId = '8fbc12d3-d806-4d75-a199-b1d14a642b6d'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('SAP EP', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Enterprise Portal', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('sapep', @equivalentId)

SET @equivalentId = '8ad24d09-14a1-4eb5-a228-b7830c5ca284'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('ess', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('enterprise security', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('ess/mss', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('enterprise security segment', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('enterprise security system', @equivalentId)

SET @equivalentId = '006d42f3-0d21-4539-93d4-dc42bf301028'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('mss', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('mission support', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('mission support system', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('mission support segment', @equivalentId)

SET @equivalentId = 'fbe767f4-9d2a-4e2b-9767-c7ded82369c1'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('south east', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('southeast', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('south-east', @equivalentId)

SET @equivalentId = '0ddcc696-d7ae-4000-9505-718c4cfcd578'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('south west', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('southwest', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('south-west', @equivalentId)

SET @equivalentId = '4bd2ef2f-44cd-4937-acb7-4ffaf70bbd04'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('SEM', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Search Engine Marketing', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('search marketing', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('S E M', @equivalentId)

SET @equivalentId = '7dd545a8-81de-406b-ac53-3b6d564584e9'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('SEO', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Search Engine Optimisation', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('S E O', @equivalentId)

SET @equivalentId = '244e4eed-12d8-4194-bf51-bb14a9dc5c53'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('PPC', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('price per click', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('priceperclick', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('P P C', @equivalentId)

SET @equivalentId = 'b0ffbca8-8b3f-4272-8386-869eae09f978'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('CPC', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('cost per click', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('costperclick', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('C P C', @equivalentId)

SET @equivalentId = '9da468ed-3d3a-41d4-bc0c-25c1b3dd3638'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('PPA', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('price per applicant', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('priceperapplicant', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('P P A', @equivalentId)

SET @equivalentId = '2c1e0c6a-3cdc-44cc-ad54-9c9a1a6de2d8'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('cpm', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('cost per thousand', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('costperthousand', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('c p m', @equivalentId)

SET @equivalentId = '1c594f26-9e1e-461e-ba8c-37133f9d1394'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('share point', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('sharepoint', @equivalentId)

SET @equivalentId = '16fc3c70-a302-468e-bdec-55a7e4004f42'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('sme', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('small and medium enterprise', @equivalentId)

SET @equivalentId = '9211420f-c168-4401-b4b7-2877e6b54717'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('sml', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('small', @equivalentId)

SET @equivalentId = 'e4fdf7dd-7f18-4dd7-b782-e5e93fe1a2aa'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('snr', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('senior', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('sr', @equivalentId)

SET @equivalentId = 'e510f683-cfee-4a70-9ed7-74439557a97a'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('software', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('soft ware', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('soft-ware', @equivalentId)

SET @equivalentId = '75062025-f6d3-49fb-89c9-0c13a89024b0'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('strategic', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('strategy', @equivalentId)

SET @equivalentId = '889337e4-6cf7-4ce4-bac0-e8f915be409a'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('syd', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('sydney', @equivalentId)

SET @equivalentId = '67159216-6ea5-4018-9373-afece92bb481'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('temp', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('temporary', @equivalentId)

SET @equivalentId = 'a8843d28-6eb5-4385-ab29-93bb43cf74cb'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('through', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('thru', @equivalentId)

SET @equivalentId = '132ed1ed-7923-46a1-9ea6-ffba4df63a4a'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('tire', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('tyre', @equivalentId)

SET @equivalentId = '2522057f-aed2-4ac5-94a5-b99ed342a578'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('tkt', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('ticket', @equivalentId)

SET @equivalentId = 'ee1af1cf-b280-4a05-a5de-58cf1a701ccb'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('trim', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Total Records and Information Management', @equivalentId)

SET @equivalentId = '9a833f61-31bb-4b6c-9865-607632552a7b'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('tv', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('television', @equivalentId)

SET @equivalentId = 'b26c0011-3ee3-4564-99c6-112d0955b038'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('uat', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('user acceptance testing', @equivalentId)

SET @equivalentId = '84e411e7-0685-494e-becb-85bddbbb35a1'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('uni', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('university', @equivalentId)

SET @equivalentId = '1f899d4a-c385-4001-b430-55390def3595'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('cipsa', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('cips', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('chartered institute of purchasing', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('chartered institute of purchasing and supply', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('chartered institute of purchasing and supply australasia', @equivalentId)

SET @equivalentId = '543e43c7-42d9-4c83-a8c1-1061424a6a7a'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('vic', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('victoria', @equivalentId)

SET @equivalentId = '3d8cbe18-8bc3-468d-ab65-7324381844c4'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('act', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('australian captial territory', @equivalentId)

SET @equivalentId = '948da60f-01e4-41f2-b39b-356d5c4df8e1'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('vpn', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('virtual private network', @equivalentId)

SET @equivalentId = '59bcdf61-0d12-467d-8b6f-cd3d34f0a95a'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('VSD', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Variable Speed Drives', @equivalentId)

SET @equivalentId = 'efa82178-986d-4951-8d75-c41bf69294bd'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('wa', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('western australia', @equivalentId)

SET @equivalentId = '35c539e9-1943-4ffe-bf45-672c75345dde'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('wan', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('wide area network', @equivalentId)

SET @equivalentId = 'c3e934d2-ff44-43f9-aba9-0329541ef7dc'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('wk', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('week', @equivalentId)

SET @equivalentId = 'ca51f42c-c77a-4b2e-a07a-95580c09f013'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Word', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Synonyms', @equivalentId)

SET @equivalentId = '513a5503-0ab3-42b4-a930-27275660dfbb'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('xray', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('x-ray', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('x ray', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('mri', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('medical resonance imaging', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('medical imaging', @equivalentId)

SET @equivalentId = 'ba8f152f-69c2-4932-b7da-4f815fc8b67e'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('blt', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('below the line', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('belowtheline', @equivalentId)

SET @equivalentId = '32d3c5c6-887e-43a1-86cc-91592232134a'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('pe', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('physical education', @equivalentId)

SET @equivalentId = '95325776-874a-4acf-a6d1-b663a6eb1d52'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('program', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('programme', @equivalentId)

SET @equivalentId = 'cefa791c-17f3-43e9-a013-0623e698950c'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('expert', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('guru', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('specialist', @equivalentId)

SET @equivalentId = 'c479cdc1-bb66-4f5d-9744-308432803cb5'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('butcher', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('butchery', @equivalentId)

SET @equivalentId = 'f1888b83-7e00-44e5-b14f-aad7dd683053'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('hvac', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('heating ventilation air conditioning', @equivalentId)

SET @equivalentId = '16001cf5-0ee6-4dd0-ae77-5cc216e787ad'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('air con', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('air conditioning', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('aircon', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('airconditioning', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('air conditioner', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('air cond', @equivalentId)

SET @equivalentId = 'a8818687-500d-4587-ae0c-8c64234b8060'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('exec', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('executive', @equivalentId)

SET @equivalentId = '2ddafdef-68d3-4a1d-bf99-5fe493b16629'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('apn', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('australian provincial news', @equivalentId)

SET @equivalentId = '6c2193a7-6d65-4594-9429-fd03a44d1473'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('student', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('undergrad', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('under grad', @equivalentId)

SET @equivalentId = 'b2386d3f-68fc-4142-b6ac-268f7e318b53'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('grad', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('graduate', @equivalentId)

SET @equivalentId = '03e074fd-ad81-42b6-a584-1b37f09c7220'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('aqtf', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('australian quality and training frameworks', @equivalentId)

SET @equivalentId = '6dda617a-dda9-41c5-a59e-428b79e34fc1'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('rto', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('registered training organisation', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('registered trainer organisation', @equivalentId)

SET @equivalentId = '026107f3-95cb-4006-a4c1-47fd17f1335e'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('organisation', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('organization', @equivalentId)

SET @equivalentId = '1479144a-04bf-4e38-b97e-dbe1846ce0ab'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('reg', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('regd', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('registered', @equivalentId)

SET @equivalentId = '1d03872e-5bb4-4dda-b1db-22c52fc39e6a'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('nlp', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('neuro linguistic programming', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('neurolinguistic programming', @equivalentId)

SET @equivalentId = '9aefc8c3-653a-4349-a3df-81c74d7a54c6'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('bcg', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('boston consulting group', @equivalentId)

SET @equivalentId = '0060f030-644b-4bea-8a35-9f3379e0f17b'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('mobil', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('exon mobil', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('exonmobil', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('exxon', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('exxonmobil', @equivalentId)

SET @equivalentId = '8e89be40-76cf-49d9-8165-623c84c630e3'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('bp', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('brittish petroleum', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('british petroleum', @equivalentId)

SET @equivalentId = 'c32541df-85d6-47b1-807f-cd67eb7d1071'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('aicd', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('austrlalian institute of company directors', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('maicd', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('faicd', @equivalentId)

SET @equivalentId = '927feb43-772a-4e26-99af-b919b9605d94'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('circa', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('around', @equivalentId)

SET @equivalentId = 'f405db26-86e4-4d70-b79a-37edd890c110'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('afp', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('australian federal police', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('police force', @equivalentId)

SET @equivalentId = '493dae87-fad4-43f9-b553-927d76853dcf'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('police officer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('policeofficer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('policeman', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('police man', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('cop', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('coppa', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('policewoman', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('police woman', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('copper', @equivalentId)

SET @equivalentId = '8bec62df-9289-4886-a48a-1e7dfa16b7bd'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('brittish', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('british', @equivalentId)

SET @equivalentId = '3e3794ac-fd00-40fd-80d3-9bcc6507203e'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('asic', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('australian securities and investment commission', @equivalentId)

SET @equivalentId = 'ef431b5e-fc6b-401e-b41a-8924871f5fce'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('jv', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('j v', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('joint venture', @equivalentId)

SET @equivalentId = '1efe783c-035d-4cbc-b794-58739d8f0a8e'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('uk', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('united kingdom', @equivalentId)

SET @equivalentId = '23259832-1017-4603-9e3d-424da94ad821'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('tig', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('tungsten inert gas', @equivalentId)

SET @equivalentId = '1a69c23f-58ff-4fa1-9f82-a09aef3ef82a'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('mig', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('metal inert gas', @equivalentId)

SET @equivalentId = '8d54a538-a47e-41f0-ba33-12d45565e039'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('let', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('long term evolution', @equivalentId)

SET @equivalentId = '5c4c256c-f187-4efb-8d60-2230d7374f2f'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('pro', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('professional', @equivalentId)

SET @equivalentId = '11d41c74-130f-43aa-82ee-d18a540b64ef'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('satiam', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('satyam', @equivalentId)

SET @equivalentId = '1adbe8f2-693d-40ff-a173-ecf7831cc9de'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('mother', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('maternity', @equivalentId)

SET @equivalentId = '4b1f0275-65b0-449f-bde7-ed244a29d62f'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('father', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('paternity', @equivalentId)

SET @equivalentId = '48540950-4700-4005-8f1b-2990b091b7e8'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('csl', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Commonwealth Serum Laboratories', @equivalentId)

SET @equivalentId = '8b89b1cd-41d8-4d47-be72-a2acaba6879d'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('eds', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('electronic data systems', @equivalentId)

SET @equivalentId = 'a2da16d9-0e07-4d40-90ff-641b9c1a3588'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('procter and gamble', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('p&g', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('proctor & gamble', @equivalentId)

SET @equivalentId = 'b87e77c6-e814-4964-b7f6-0b2f5cd46b6c'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('alcatel', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('alcatel lucent', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('lucent', @equivalentId)

SET @equivalentId = '87d4153e-f266-4376-ab36-617041003f97'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('m&a', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('mergers and acquisitions', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('mergers & acquisitions', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('mergers&acquisitions', @equivalentId)

SET @equivalentId = '2e18ccdd-1f87-4595-a574-ac490f0f2b53'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('arab', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('arabic', @equivalentId)

SET @equivalentId = '64c7ac85-7ac6-43bf-b5ea-bd973d70b91c'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('interior design', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('interior designer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('interior decorator', @equivalentId)

SET @equivalentId = '6105ceda-893e-42cf-9fbf-970a24f4420e'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('reporter', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('journalist', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('journalism', @equivalentId)

SET @equivalentId = 'f9734e42-a245-49dd-b7bd-0000bb750856'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('actuary', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('actuarial', @equivalentId)

SET @equivalentId = 'fd74b933-2094-4773-af33-d6739abb98e0'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('tresury', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('treasury', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('treasurer', @equivalentId)

SET @equivalentId = 'a9e9c90f-1239-47e0-897e-5156229e2d09'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('apprentice', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('apprenticeship', @equivalentId)

SET @equivalentId = 'b2ac4620-9e84-4460-8852-9649c6904897'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('waste water', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('wastewater', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('storm water', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('stormwater', @equivalentId)

SET @equivalentId = '5379c933-9266-47d0-9f4f-b70bb464340d'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('town', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('urban', @equivalentId)

SET @equivalentId = '66b9c8e4-63c8-42bb-be8e-d886e74c3f7f'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('hairdressor', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('hair stylist', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('hairdresser', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('hair and makeup', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('hair dresser', @equivalentId)

SET @equivalentId = '5bbd3989-9efc-4e79-b017-94d666033dd9'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('footware', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('foot ware', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('footwear', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('foot wear', @equivalentId)

SET @equivalentId = 'd3e8c7f7-9425-4739-b58f-c4dd9d842957'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('make up', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('makeup', @equivalentId)

SET @equivalentId = '3e857ecf-1d1c-41ee-b45d-0f5f0c40ce46'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('uae', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('united arab emirates', @equivalentId)

SET @equivalentId = 'f4699bbb-8253-4dca-b09f-d8d484d1dad9'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('geo technical', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('geotechnical', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('geo tech', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('geotech', @equivalentId)

SET @equivalentId = '2c8e6353-fbad-4dc0-98f9-3f61e12fb483'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('lead', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('head', @equivalentId)

SET @equivalentId = 'f01126fb-fb53-42f2-a15f-487f9c5bb550'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('rail', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('railway', @equivalentId)

SET @equivalentId = '5f0190b9-ac8e-472e-8fd8-c02e6d9775ae'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('nurse', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('nursing officer', @equivalentId)

SET @equivalentId = '88292f6e-013c-4fdf-a54f-ffe103024463'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('defence', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('defense', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('armed forces', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('defence personnel', @equivalentId)

SET @equivalentId = '8bf970ff-b05d-4b39-b839-9a42ca48e3eb'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('air force', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('airforce', @equivalentId)

SET @equivalentId = 'f60d22af-3b0e-4caa-87a7-0963f2c18e44'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('aeroport', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('aero port', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('airport', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('air port', @equivalentId)

SET @equivalentId = '6c188062-1c68-4313-bbdd-459c92a31c36'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('ammunition', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('munition', @equivalentId)

SET @equivalentId = '9f162199-3002-40cb-84ce-0bcbda4314d7'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('cbms', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('central budget management system', @equivalentId)

SET @equivalentId = 'b1634cbd-da4a-4aa4-aaa2-765989f5f3c3'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('ict', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('information communication and technology', @equivalentId)

SET @equivalentId = '57347492-795b-4e4c-b0c7-95fafba3b840'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('icu', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('intensive care unit', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('intensive care', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('intensivecare', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('msicu', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('critical care medicine', @equivalentId)

SET @equivalentId = 'a3b9ca66-f4d3-4c89-a480-8ebff1e44823'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('picu', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('paedeatric intesive care', @equivalentId)

SET @equivalentId = 'b6db1307-fe3b-43a6-9b80-ac8ea6c76cef'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('nicu', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('neonatal intesive care', @equivalentId)

SET @equivalentId = '5409feec-0988-4e5c-96fc-d8b2396b7395'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('safety officer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('safety inspector', @equivalentId)

SET @equivalentId = '2789e8da-a5bf-4bb2-8432-1767cb27c870'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('japan', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('japanese', @equivalentId)

SET @equivalentId = '8d1b42df-c544-44a7-9125-6dce33487cfe'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('china', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('chinese', @equivalentId)

SET @equivalentId = '461a5935-dce8-4a4c-b089-9aee0d6398a0'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('france', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('french', @equivalentId)

SET @equivalentId = '69b23be0-4bbe-4956-98ce-ddd3d74d7d6a'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('germany', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('german', @equivalentId)

SET @equivalentId = 'b98bb4ef-7cc5-43aa-9df1-f46a503dde90'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('ir', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('industrial relations', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('workplace relations', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('work place relations', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('employee relations', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('er', @equivalentId)

SET @equivalentId = '09729597-c9f7-44d8-8e88-16ed0ba94a35'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('workplace', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('work place', @equivalentId)

SET @equivalentId = 'b8857ca3-2eb9-4801-a636-7b958d9fca19'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('job network', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('jobnetwork', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('job placement', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('jpo', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('centrelink', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('centre link', @equivalentId)

SET @equivalentId = '2e6009a3-e06f-47a2-92d3-b973c55fe0f2'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('rpo', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('recruitment process outsourcing', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('onsite recruitment', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('hro', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('hr process outsourcing', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('on site', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('onsite', @equivalentId)

SET @equivalentId = 'dca232a1-3229-4e26-b0a4-41ba7284cb77'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('od', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('organisation development', @equivalentId)

SET @equivalentId = '27fdbe58-b9ff-417a-827f-6a3a188aac6f'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('cqi', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('continuous quality improvement', @equivalentId)

SET @equivalentId = 'd0762b83-e231-4927-9436-e65a67b26003'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('webmethods', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('web methods', @equivalentId)

SET @equivalentId = 'dc32bc11-d1f5-4db9-93d4-30d7a7ab64a5'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('gis', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('geographic information system', @equivalentId)

SET @equivalentId = '24b84138-94a3-4022-82bf-5590234ea3b8'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('hyper text', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('hypertext', @equivalentId)

SET @equivalentId = 'cf03d314-40a9-4e32-9b94-946a46029c44'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('3d', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('3 dimensional', @equivalentId)

SET @equivalentId = '55638349-60f7-463f-8f5a-7efdbde2472e'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('mainframe', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('main frame', @equivalentId)

SET @equivalentId = '9cb068cd-9dff-49b2-8bc4-0085225ae980'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('scrum', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('agile development', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('agile software development', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('agile methodology', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('agile software methodology', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('agile software development methodology', @equivalentId)

SET @equivalentId = 'eb86304c-168d-46f8-a2aa-b64bba28d428'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('biztalk', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('biz talk', @equivalentId)

SET @equivalentId = '2490471d-5e53-4a87-83e0-fb559d0376ff'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('database', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('data base', @equivalentId)

SET @equivalentId = '663bc392-c0c6-4ea2-855d-3d49313869db'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('c#', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('c sharp', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('csharp', @equivalentId)

SET @equivalentId = 'd4a8b478-97de-4cc8-8cc8-c4b66c124c2a'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('.com', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('dot com', @equivalentId)

SET @equivalentId = 'b94e079e-0972-4762-834f-adc708ca6c08'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('.net', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('dot net', @equivalentId)

SET @equivalentId = 'd1f93225-ae5c-45bc-b793-ba0b7893696f'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('gui', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('graphic user interface', @equivalentId)

SET @equivalentId = '2aa7ed92-a547-48a2-8e3f-47a8f8a4644f'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('power builder', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('powerbuilder', @equivalentId)

SET @equivalentId = '61fb0269-66e2-4803-9476-3be5cb9fc190'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('cti', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('computer technology integration', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('computerised technology integration', @equivalentId)

SET @equivalentId = '16d60b47-28b2-4693-bf72-1edc9d8e081e'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('soa', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('service oriented architecture', @equivalentId)

SET @equivalentId = '34938f2c-35b5-4e09-b0f1-a238335b8b14'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('iso', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('international standards association', @equivalentId)

SET @equivalentId = '1112e21f-55fc-4322-82ae-9254ee181f9d'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('rem', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('remuneration', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('salary', @equivalentId)

SET @equivalentId = '59cceae9-7552-4fe5-ad41-5d6dd286175c'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('smsf', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('self manager super fund', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('self manager superannuation fund', @equivalentId)

SET @equivalentId = '6210bc5e-7030-4d39-8686-d2339fb01da1'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('worksafe', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('work safe', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('work cover', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('workcover', @equivalentId)

SET @equivalentId = 'ac209337-17a2-498a-a62d-ccb49086b407'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('j2ee', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('j2e', @equivalentId)

SET @equivalentId = '54768880-ac96-4f76-b9ac-830600795a5b'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Java/J2ee', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Java & J2ee', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Java J2ee', @equivalentId)

SET @equivalentId = 'df3e9f25-1242-40cb-a6f0-68accd28e10b'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('sql', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('structured query language', @equivalentId)

SET @equivalentId = '22f4c518-38a8-427c-a76e-ee555f93e466'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('javascript', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('java script', @equivalentId)

SET @equivalentId = '3cabc5a9-bef8-4c78-b5bb-cc3aca2ace9b'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('4gl', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('4th general language', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('forth general language', @equivalentId)

SET @equivalentId = '2eb82158-39e5-43b0-891a-ad2afee91f95'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('sqlserver', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('sql server', @equivalentId)

SET @equivalentId = '6c117575-eac8-4eff-983d-44dacf99b095'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('datawarehouse', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('data warehouse', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('DW', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('DWH', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('data storage', @equivalentId)

SET @equivalentId = '8e616356-9d7e-4d03-ab9c-b07f6a220903'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('gl', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('general ledger', @equivalentId)

SET @equivalentId = '29d9de83-3a48-4e0b-9f9d-110185cb1e1f'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('ldap', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('lightweight directory access protocol', @equivalentId)

SET @equivalentId = '5f039e6d-ea59-467e-ac34-a21c4f95457a'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('inhouse', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('in house', @equivalentId)

SET @equivalentId = '569ee975-0f5e-4168-8477-7ac484d9876f'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('cnc', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('computer numeric conrol', @equivalentId)

SET @equivalentId = '8d248db7-9820-4fed-b0f7-9dd4c41923db'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('toolmaker', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('tool maker', @equivalentId)

SET @equivalentId = '7dec4d1f-8b6d-4378-b1ae-cbad42a90487'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('datacentre', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('data centre', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('data center', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('datacenter', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('hosting center', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('hosting centre', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('data facility', @equivalentId)

SET @equivalentId = '6b134bc9-ff72-421b-9654-eb4081899b27'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('mysql', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('my sql', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('m y sql', @equivalentId)

SET @equivalentId = '5bedc70d-a50a-4e0b-97c0-528929314ceb'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('uml', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Unified Modeling Language', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('unified modelling language', @equivalentId)

SET @equivalentId = '92ae5205-08ec-456c-a914-c1041e7dca39'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('use case', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('usecase', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('user case', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('usercase', @equivalentId)

SET @equivalentId = 'b08f6057-a16f-4e8d-b05d-8916f839101c'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('as400', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('as 400', @equivalentId)

SET @equivalentId = '8b520b01-17e1-4a21-b6eb-604c655654af'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('ccnp', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('cisco certified network professional', @equivalentId)

SET @equivalentId = '4354f3ff-7c9c-48f2-acb0-7c6b585027c0'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('ccna', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('cisco certified network associate', @equivalentId)

SET @equivalentId = 'a45b72f2-dbb1-4388-9342-643da06b87f8'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('prince2', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('prince 2', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('princeii', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('prince ii', @equivalentId)

SET @equivalentId = '45286df3-5372-4872-813e-e15ac22aa6ac'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('msce', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('microsoft certified systems engineer', @equivalentId)

SET @equivalentId = '53496a76-e639-43ad-a82c-6112807befe4'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('noc', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('netwrok oprations centre', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('network operations center', @equivalentId)

SET @equivalentId = 'bae2168c-c5b4-4d0e-9cae-656d176a706d'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('san', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('storage area network', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('storage array', @equivalentId)

SET @equivalentId = '4057e978-b86e-4800-9a7f-c5f590f825c0'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('pstn', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('public switched telephone network', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('public switched telephony network', @equivalentId)

SET @equivalentId = 'c42fbd76-dbac-4591-adcd-c96b2af15bf3'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('fr', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('frame relay', @equivalentId)

SET @equivalentId = '86953fae-70cf-4fbe-9b1b-20e186b31e6d'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('level 1', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('level1', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('1st level', @equivalentId)

SET @equivalentId = 'c4c10e00-014f-4cb0-9275-693f75216618'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('level 2', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('level2', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('2nd level', @equivalentId)

SET @equivalentId = 'a2ce178c-26e7-4fb5-a62c-2afd50d8ba11'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('level 3', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('level3', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('3rd level', @equivalentId)

SET @equivalentId = 'd7095d05-6ddd-4c31-a6b8-fac564fc0755'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('desktop', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('desk top', @equivalentId)

SET @equivalentId = '6ffa821b-5965-42ee-81aa-237e3c784567'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('pc', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('personal computer', @equivalentId)

SET @equivalentId = 'c862578a-d0e7-4172-8d48-8a292b316025'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('blackbelt', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('black belt', @equivalentId)

SET @equivalentId = '89f1dc30-0ad9-4418-8faf-d70d8ee746a2'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('checkpoint', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('check point', @equivalentId)

SET @equivalentId = '6e774999-e553-461c-80fc-aa4058da724f'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('firewall', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('fire wall', @equivalentId)

SET @equivalentId = 'c1d9d156-5864-44d6-960b-c74db7eb7544'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('pki', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('public key infrastructure', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('online security', @equivalentId)

SET @equivalentId = '1bb0975c-325e-4fb1-843b-d975bcfcd2cd'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('ipsec', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('ip sec', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('ip security', @equivalentId)

SET @equivalentId = '61427357-b678-46ac-a51d-8725a05d273a'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('coax', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('coaxial', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('co ax', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('co axial', @equivalentId)

SET @equivalentId = '48fd07f1-9de0-4923-8bbb-d30525b3f7ae'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('fibre', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('fiber', @equivalentId)

SET @equivalentId = '7d716d75-2e46-4bd4-a2cf-10d9b0f84459'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('itil', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('information technology infrastructure library', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('it infrastructure library', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('itil methodology', @equivalentId)

SET @equivalentId = 'eb14314a-e393-4e49-9078-dab97e91019c'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('pipeline', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('pipe line', @equivalentId)

SET @equivalentId = 'da61a201-665d-413b-9660-791a0354c675'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('realestate', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('real estate', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('property', @equivalentId)

SET @equivalentId = 'e2e3d5ff-f42e-4f25-b13c-45b02ee9e4e8'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('facility', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('facilities', @equivalentId)

SET @equivalentId = 'b7bb4352-6961-4605-8598-efe03f7a41c3'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('stocktake', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('stock take', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('stock audit', @equivalentId)

SET @equivalentId = 'ea958557-1f51-4a40-a904-a640e00d7b73'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('mobile phone', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('mobile telephony', @equivalentId)

SET @equivalentId = 'ede64797-2d2d-40c6-b768-b585f359e51a'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('hv', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('high voltage', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('highvoltage', @equivalentId)

SET @equivalentId = 'e891060c-b702-437d-85db-b2bfe5b94486'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('supplychain', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('supply chain', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('logistics', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('supply line', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('supplyline', @equivalentId)

SET @equivalentId = '5792d7c7-7f99-4e17-a865-0f5dc4568a71'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('marine', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('maritime', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('sea', @equivalentId)

SET @equivalentId = 'c36d0801-9542-445e-9068-c22fb64719db'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('div1', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('division 1', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('div 1', @equivalentId)

SET @equivalentId = '932d88fd-dd7e-46b6-9adf-60c36409669d'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('div2', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('division 2', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('div 2', @equivalentId)

SET @equivalentId = '29b35dc2-59f8-4844-b556-b09811b75b6c'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('div3', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('division 3', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('div 3', @equivalentId)

SET @equivalentId = '7a703c5e-e7db-4a7a-9953-0459724916cf'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('b2b', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('b 2 b', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('business to business', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('business 2 business', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('business2busines', @equivalentId)

SET @equivalentId = '9996fb0a-e6b4-40ec-b31f-41abece6fba3'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('b2c', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('b 2 c', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('business to consumer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('business 2 consumer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('business2consumer', @equivalentId)

SET @equivalentId = 'c01ae35c-f8cc-4a5d-93f1-4d36dacfbea2'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('hk', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('h k', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('hongkong', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('hong kong', @equivalentId)

SET @equivalentId = 'f3d0786d-3fa8-43df-b824-43661ee14854'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('png', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('papua new guinea', @equivalentId)

SET @equivalentId = '96a892ac-83c4-4ca9-8e20-0a1497db429c'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('heathfood', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('health food', @equivalentId)

SET @equivalentId = '1626974f-1536-4120-bd1a-b99b17853e43'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('hifi', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('hi fi', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('high fidelity', @equivalentId)

SET @equivalentId = '266e2dfb-7d4d-4105-8b26-b53b9a61a4d8'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('pga', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('professional golfers association', @equivalentId)

SET @equivalentId = '452470eb-06dc-4394-864b-5519211ad2bb'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('MYOB', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Mind your own business', @equivalentId)

SET @equivalentId = '79fd80d4-e9de-4d57-8390-4c40fd0854fd'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('OTE', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('O.T.E.', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('On target earnings', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('On target earning', @equivalentId)

SET @equivalentId = 'b6ceb23f-aabf-4629-943c-76303b0897d4'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('service delivery', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('SD', @equivalentId)

SET @equivalentId = '4038a798-3a36-410f-8789-cc1817c69fa2'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('xml', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('eXtensible Markup Language', @equivalentId)

SET @equivalentId = 'd41f1842-868c-4b06-b5a6-d3ca0396b16e'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('html', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('hypertext markup language', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('hypertext mark up language', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('h t m l', @equivalentId)

SET @equivalentId = '664edc3c-52db-47b3-84a1-2e4036fb0290'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('tac', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('transport accident commission', @equivalentId)

SET @equivalentId = '46379c1a-c80e-4b60-ada6-3dca0ac34f8f'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('pwc', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('price waterhouse coopers', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('pricewaterhouse coopers', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('price water house coopers', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('coopers and lybrand', @equivalentId)

SET @equivalentId = '345f0d01-a287-4e90-9a07-38e10aa5b2f2'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('deloitte', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('deloitte touche', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('deloitte touche tohmatsu', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Duesburys', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('deloittetouche', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('deloittetouchetohmatsu', @equivalentId)

SET @equivalentId = '89d7201a-ce7c-43d8-9383-0282bbf2b0f7'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('ey', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('e&y', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('ernst and young', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('ernst & young', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('ernst&young', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('ernst & whinney', @equivalentId)

SET @equivalentId = 'c017260f-4135-488a-84df-d4d357d6f902'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('kpmg', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('hungerfords', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('peat marwick', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('peatmarwick', @equivalentId)

SET @equivalentId = '713d6fae-1d87-44ad-a6cf-13d491faf0c6'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('australia post', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('auspost', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('australiapost', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('austpost', @equivalentId)

SET @equivalentId = '41b15ff9-6c95-4c62-96ed-b672bcec4f3b'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('gsk', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('glaxo smith kline', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('glaxosmithkline', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('glaxo', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('glaxo smithkline', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('glaxo & smithkline', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('glaxo and smithkline', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('glaxo wellcome', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('glaxowellcome', @equivalentId)

SET @equivalentId = 'a0e1265f-4b34-4dfb-840d-e472962d9a16'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('astrazeneca', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('astra zeneca', @equivalentId)

SET @equivalentId = 'f3a3d415-3127-4e3c-a1cc-6b934e0a9d95'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('julia ross', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('juliaross', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('ross human directions', @equivalentId)

SET @equivalentId = '3e4ccea2-1521-4b33-b4d3-de1bb1830641'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('ray white', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('raywhite', @equivalentId)

SET @equivalentId = '86d1f648-236c-432a-9c93-a060ff2da3ea'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('hocking stuart', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('hockingstuart', @equivalentId)

SET @equivalentId = '34b0f0c7-4f40-41dd-9945-90232f8bf4b2'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('barry plant', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('barryplant', @equivalentId)

SET @equivalentId = 'e058fa2f-b2d9-464b-9edd-e5ebbb1ac93d'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('tradelink', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('trade link', @equivalentId)

SET @equivalentId = '6bc05675-1f79-4334-8541-7f72b1b4ae29'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('lockwood', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('lock wood', @equivalentId)

SET @equivalentId = '09291e20-65ff-45a0-ab1c-845fee30d459'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('peoplesoft', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('people soft', @equivalentId)

SET @equivalentId = 'c5ed9cc0-86c2-4123-94cf-49c0e9941cc7'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('primus', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('iprimus', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('primus telecom', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('primus telecommunications', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('primustelecom', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('primustel', @equivalentId)

SET @equivalentId = '36211b20-5665-43b9-ae1b-ae9284ed2c3a'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('optus', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('singtel', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('singteloptus', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('singtel optus', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('singapore telecom', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('singapore telecommunications', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('sing tel', @equivalentId)

SET @equivalentId = '71e55294-a72b-41a2-ac18-481d056c5333'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('telstra', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('telstra corp', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('telstra ltd', @equivalentId)

SET @equivalentId = '0c37ba4e-429f-4ce4-9653-c02420b04225'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('melbourne uni', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('melbourneuni', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('melbourne university', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('university of melbourne', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('universityofmelbourne', @equivalentId)

SET @equivalentId = 'a7a82d8c-0260-4bc3-8cea-b3adac2acb28'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('sydney uni', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('sydneyuni', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('sydney university', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('university of sydney', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('universityofsydney', @equivalentId)

SET @equivalentId = 'b0da8f5f-45e1-4337-9f9a-4eeb174083c0'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('uni of queensland', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('university of queensland', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('queensland uni', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('qld uni', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('qld university', @equivalentId)

SET @equivalentId = '543cbaf9-b3e9-4317-bf84-0a93a8353ed6'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('acu', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('australian catholic university', @equivalentId)

SET @equivalentId = 'ea63c4c4-5218-4254-8210-122b78e61308'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('bond uni', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('bond university', @equivalentId)

SET @equivalentId = '32a335ad-f7b1-40e7-9187-f93fff8b135f'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('cdu', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('charles darwin university', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('charles darwin uni', @equivalentId)

SET @equivalentId = '1dcd06c0-b978-4023-b166-bd2c1b5abf38'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('csu', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('charles sturt university', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('charles sturt un', @equivalentId)

SET @equivalentId = 'dea298a5-6028-4e78-ba81-a0e3997f92bb'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('ecu', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('edith cowan university', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('edith cowan uni', @equivalentId)

SET @equivalentId = 'a0e222af-0186-42d8-84a8-5819c0ad8653'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('jcu', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('james cook university', @equivalentId)

SET @equivalentId = 'dd2480a5-8282-4cca-8f19-caf015d0258c'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('latrobe', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('la trobe', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('latrobe university', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('latrobe uni', @equivalentId)

SET @equivalentId = '9c0cb2a3-d37d-40d2-be09-2c305bf874f8'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('scu', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('southern cross university', @equivalentId)

SET @equivalentId = '7b5d51c4-0558-4f8e-ae81-b9362a506220'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('swinburn uni', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('swinburn university', @equivalentId)

SET @equivalentId = '72ead0b9-e953-4c69-91a6-1783216e0fb3'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('university of ballarat', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('ballarat uni', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('ballarat university', @equivalentId)

SET @equivalentId = '162a5bd2-8c0c-493b-bdcb-684d5e865457'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('university of canberra', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('canberra uni', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('canberra university', @equivalentId)

SET @equivalentId = '96a30374-7f44-4122-b15e-9a247067155d'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('une', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('university of new england', @equivalentId)

SET @equivalentId = '2c8f267b-f27c-4417-ba45-a4f598869f94'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('university of newcastle', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('newcastle uni', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('newcastle university', @equivalentId)

SET @equivalentId = 'f13c5bf2-5130-4ce2-bf83-4f1acffea18a'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('unda', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('university of notre dame australia', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('university of notredame australia', @equivalentId)

SET @equivalentId = 'de101e07-8774-46eb-a457-2762d75cce20'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('unisa', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('university of south australia', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('uni sa', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('uni of south australia', @equivalentId)

SET @equivalentId = '97944a5e-f3be-4aeb-aa79-625182ff41a1'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('usq', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('university of southern queensland', @equivalentId)

SET @equivalentId = '98681cbb-3ba2-4501-bc91-526f1dc13127'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('university of tasmania', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('uni of tasmania', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('utas', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('u tas', @equivalentId)

SET @equivalentId = 'fd89d873-a331-4f79-8729-4e6bb14aad55'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('usc', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('university of the sunshine coast', @equivalentId)

SET @equivalentId = '055a5bb7-41d7-4e2e-86f4-b95a928ba045'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('uow', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('university of wollongong', @equivalentId)

SET @equivalentId = '5b928ce2-4492-44df-a105-1e443d343725'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('vu', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('victoria university', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('victoria uni', @equivalentId)

SET @equivalentId = '43a02e23-23af-4a4b-8918-eab14972488f'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('ceo institute', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('ceoinstitute', @equivalentId)

SET @equivalentId = 'fb8dab91-c006-4809-90a7-8fe22fcf8caa'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('ceo circle', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('ceocircle', @equivalentId)

SET @equivalentId = '1a093fc9-e6ce-471c-8d3e-5e66a659c845'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('goldcoast', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('gold coast', @equivalentId)

SET @equivalentId = '9d6b7dfc-6009-4501-96b6-8fe388993e77'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('sunshinecoast', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('sunshine coast', @equivalentId)

SET @equivalentId = '125c6829-f2b1-4815-8bc0-29d350c28167'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('tassie', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('tasmania', @equivalentId)

SET @equivalentId = '1c642fff-3c03-44af-9732-d4dda7dcd4e9'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('mbs', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('melbourne business school', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('m b s', @equivalentId)

SET @equivalentId = 'affe62b6-203e-4ccf-982e-b943450a7360'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('news ltd', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('newsltd', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('news limited', @equivalentId)

SET @equivalentId = 'be181bcc-69fc-4013-9bec-dd2d40f2d7f7'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('qut', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('queensland university of technology', @equivalentId)

SET @equivalentId = '72a426ab-4a95-4c4f-8596-f6c824a2ce28'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('rmit', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('royal melbourne institute of technology', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('rmit university', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('rmit uni', @equivalentId)

SET @equivalentId = '41bac667-2af5-4069-84a5-66db997885b1'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('inbound', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('in bound', @equivalentId)

SET @equivalentId = '0c8d555b-3045-4ef7-b154-5e37f3f8cdd8'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('adelaide university', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('university of adelaide', @equivalentId)

SET @equivalentId = 'f1d2788a-3401-4e09-94b0-afb09603efd7'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('anu', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('a n u', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('australian national univeristy', @equivalentId)

SET @equivalentId = '0f52f2c7-7052-49f4-a641-a88448352835'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('unsw', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('university of new south wales', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('u nsw', @equivalentId)

SET @equivalentId = '2e1574a3-31f9-4205-83fa-db575ee4010f'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('uws', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('university of western sydney', @equivalentId)

SET @equivalentId = '26e917af-af0f-4695-b8cf-ea85629aad74'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('monash uni', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('monash university', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('monashuni', @equivalentId)

SET @equivalentId = 'f9600712-997a-4550-b769-458ffd47d7ef'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('WAI', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Willliam Angliss Institute', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Sir Willliam Angliss Institute', @equivalentId)

SET @equivalentId = '8ee01201-e200-494c-b355-21c7e2a48efe'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('deakin uni', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('deakin university', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('deakinuni', @equivalentId)

SET @equivalentId = 'df3e9e7b-c5c6-4d0c-bae6-a5e677edfe6b'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('college', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('school', @equivalentId)

SET @equivalentId = '1932894d-9d9b-4cec-ba2b-2a3ebb3ab794'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('baccalaureate', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('international baccalaureate', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('ib', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('bac', @equivalentId)

SET @equivalentId = 'f4659357-0538-47f5-9098-beb1bd8e0c7f'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('special education', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('special ed', @equivalentId)

SET @equivalentId = '92126ea5-c4fd-409e-9db9-89832de9bcf2'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Chisholm', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Chisholm TAFE', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Chisholm Institute', @equivalentId)

SET @equivalentId = 'b96f15e4-3c2e-4b8f-acd0-272eb0348dee'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Holmesglen institute of TAFE', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Holmesglen', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Holmesglen Institute', @equivalentId)

SET @equivalentId = 'c62fbffd-dbae-47dc-9a1c-7a525a6fa772'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('NMIT', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('N.M.I.T.', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Northern melbourne institute of technology', @equivalentId)

SET @equivalentId = 'c6f38b95-42c6-4b03-88c1-65c6fa49e178'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Swinburne', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Swinburne institute', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Swinburne TAFE', @equivalentId)

SET @equivalentId = '6bec1f67-ce3c-4d99-b507-a8e7f5c871c2'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('vce', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('victorian certificate of education', @equivalentId)

SET @equivalentId = '28ebaae8-fc2a-47f2-9e1d-39e22fa12ed2'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('hsc', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('higher school certificate', @equivalentId)

SET @equivalentId = 'c0db38f5-9651-48f8-98f6-e676ec2e16fd'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('kmart', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('k mart', @equivalentId)

SET @equivalentId = '41f2d8ed-af23-43d2-b818-aa208be7b46c'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('young and rubicam', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Y&R', @equivalentId)

SET @equivalentId = '26cd3875-ceaa-420c-b241-06537855c78e'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('officeworks', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('office works', @equivalentId)

SET @equivalentId = 'a2020d0c-b85e-456e-992e-e0e8e8ec83b8'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('coles', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('coles group', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('colesmyer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('coles myer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('cml', @equivalentId)

SET @equivalentId = '05665211-74b0-4604-a0d1-c62d20791824'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('wool worths', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('woolworths', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('safeway', @equivalentId)

SET @equivalentId = '1c5a0500-d606-42c6-8baa-88a3741c476f'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('big w', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('bigw', @equivalentId)

SET @equivalentId = '3c5d7573-cde5-4827-9ef2-8c2df210348f'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('mc donalds', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('mcdonalds', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('macdonalds', @equivalentId)

SET @equivalentId = '15f93211-3ff8-4ac0-992a-aeaeddae202b'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('spotless', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('spotless catering', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('spotless group', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('spotless services', @equivalentId)

SET @equivalentId = '1069fc21-80bc-4af8-a70a-3326d2264e2f'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('ramsayhealth', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('ramsay health', @equivalentId)

SET @equivalentId = 'f7d22aab-e1db-438a-86d5-f43190e4e4aa'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('worley parsons', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('worleyparsons', @equivalentId)

SET @equivalentId = '0babdd6a-c311-4546-9246-9aca12eeba31'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('racv', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('royal automobile club of victoria', @equivalentId)

SET @equivalentId = '2bc8b6c1-4094-44a9-8f08-064c770276c1'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('mcc', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('melbourne cricket club', @equivalentId)

SET @equivalentId = 'f25e8983-09f2-4fd6-beeb-bae4ed649693'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('mcg', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('melbourne cricket ground', @equivalentId)

SET @equivalentId = '1f04562f-64ad-42ff-a69d-9452d9f78230'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('scg', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('sydney cricket ground', @equivalentId)

SET @equivalentId = '1b132ce6-53e9-430a-a3a0-1e810ebe078a'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('cocacola', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('coca cola', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('coke', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('coca cola amatil', @equivalentId)

SET @equivalentId = 'a4750bc6-a249-446d-b7d3-7cff027fc324'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('blue scope', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('bluescope', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('bluescope steel', @equivalentId)

SET @equivalentId = '9b45aff3-fb82-493a-870c-607764bc29d3'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('salvos', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('salvation army', @equivalentId)

SET @equivalentId = '48eb1a35-9a1f-42bf-9576-6e0eea29cf35'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('campbell page', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('campbellpage', @equivalentId)

SET @equivalentId = '4f5dcc71-a5fa-4f01-ab41-432358d7a6a2'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('max employment', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('maxx employment', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('maxemployment', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('maxxemployment', @equivalentId)

SET @equivalentId = '03984135-f705-4ea9-91b7-dcdc73a04d2a'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('wise employment', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('wiseemployment', @equivalentId)

SET @equivalentId = '1c10c6a2-ab07-4568-a1fd-4722a55ae572'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('gm holden', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('beneral motors holden', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('general motors', @equivalentId)

SET @equivalentId = 'dba36c89-1194-491d-a408-a61543e159c0'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('justjeans', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('just jeans', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('just group', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('justgroup', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('pbl ltd', @equivalentId)

SET @equivalentId = '541a884f-7524-4a51-adce-c2b05f639429'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('pbl', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('publishing and broadcasting', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('publishing & broadcasting', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('publishing&broadcasting', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Mac Bank', @equivalentId)

SET @equivalentId = '55a1af91-0e69-496c-a6c7-abc67015f88b'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('macq bank', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('macquarie bank', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('macquarrie bank', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('macbank', @equivalentId)

SET @equivalentId = 'fd51dc5f-b0e8-4589-82d0-df265dff48cb'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('virgin blue', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('virginblue', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('virginblue.com.au', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Virgin', @equivalentId)

SET @equivalentId = '45b86db9-464c-4704-b570-d7508d265154'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('mitre10', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('mitre 10', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('mitre ten', @equivalentId)

SET @equivalentId = '990563d8-bd6b-41b0-858d-340fe4582b6d'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('starcity', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('star city', @equivalentId)

SET @equivalentId = 'a192759d-56dd-4de4-a7e1-5b7e3029aa60'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('dicksmith', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('dick smith', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('DSE', @equivalentId)

SET @equivalentId = 'd47d5c92-a70b-46f5-9e47-04ca4f72d5de'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('seek', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('seek.com.au', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('seek.com', @equivalentId)

SET @equivalentId = '0776d52a-676a-469e-9289-841072e5e8d0'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('yahoo', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('yahoo7', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('yahoo.com.au', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('yahoo7.com.au', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('yahoo 7', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('yahoo7!', @equivalentId)

SET @equivalentId = 'e98e03af-4b49-4e22-8b71-7d044cffe663'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('newcrest', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('newcrest mining', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('newmont', @equivalentId)

SET @equivalentId = '941f07bc-d4b7-47a2-ae79-4877ca9f3a46'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('ge', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('general electric', @equivalentId)

SET @equivalentId = 'e872410a-d067-4a11-b5cb-678facf0ddfa'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('talent2', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('talent 2', @equivalentId)

SET @equivalentId = '598ebdb0-a31b-484b-a832-30ed24a6651c'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('chandler macleod', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('chandlermacleod', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('cmg', @equivalentId)

SET @equivalentId = 'df2e496e-f3d0-4a0e-986a-548283fd1c86'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('lloyd morgan', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('lloydmorgan', @equivalentId)

SET @equivalentId = 'e1bb2d0a-700f-4004-9779-7c066d5137f2'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('m&b', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('morgan and banks', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('morgan&banks', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('morgan & banks', @equivalentId)

SET @equivalentId = 'bf427255-aa54-49e1-8afd-90e3e8bf22da'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('dhs', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('department of human services', @equivalentId)

SET @equivalentId = 'be08bcfe-de01-4b46-8d53-6bc3e0ded6f6'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('DOI', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Dept of Infrastructure', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Department of infrastructure', @equivalentId)

SET @equivalentId = 'c9ec4fc2-5871-441c-94de-49218fce93aa'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('royal australian college of general practitioners', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('fracgp', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('f.r.a.g.p.', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('royal college of general practitioners', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('royal college of gp''s', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('racgp', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('r a c g p', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('f r a c g p', @equivalentId)

SET @equivalentId = 'fb1f2396-4d82-4631-804c-5da734e8ec3d'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('maanz', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('marketing association of australia and new zealand', @equivalentId)

SET @equivalentId = '30117028-7c62-429e-9636-b0fb7f6123ac'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('mia', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('marketing institute of austrlalia', @equivalentId)

SET @equivalentId = '35b4ba3d-5ceb-4f2a-a925-3b342f610afe'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('aempe', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('association of marine and power engineers', @equivalentId)

SET @equivalentId = '6d812369-4098-4e37-966a-063a26a5f947'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('vacc', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('victorian automotive chamber of commerce', @equivalentId)

SET @equivalentId = 'ab23e75d-708a-4904-87e6-9f0013b01e95'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('sae', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('society of automotive engineers', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('society of automotive engineers australasia', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('saea', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('sae-a', @equivalentId)

SET @equivalentId = '50edd28d-2d09-467a-b5b8-5dec424d8a0e'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('ahri', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('austrlalian human resources institute', @equivalentId)

SET @equivalentId = 'e2e232d2-15ec-4c23-ba12-25abb4b882f8'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('rcsa', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('recruitment and consulting services association', @equivalentId)

SET @equivalentId = 'd4d1e6e6-555e-4892-96bb-36675d1ee21d'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('itcra', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('information technology contract and recruitment association', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('information technology contract & recruitment association', @equivalentId)

SET @equivalentId = 'abc6c07a-7d81-405b-b8e0-b6f6bb97e1dd'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('gta', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('group training australia', @equivalentId)

SET @equivalentId = '6eef8b0b-616b-4cd7-b9cb-3adf8f8a157c'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('finsia', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('financial services institute of australiasia', @equivalentId)

SET @equivalentId = 'c6ae9fcb-ce03-4c99-a694-2dabbc7bf0b2'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('rdns', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('royal district nursing service', @equivalentId)

SET @equivalentId = 'e6d0ef44-f99f-4da5-92f4-7a7fbc6314ac'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('pia', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('planning institute of australia', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('rapi', @equivalentId)

SET @equivalentId = 'c8d5558f-faf8-4ed8-b884-3975c9a3cefa'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('apa', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('australian physiotherapy association', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('australian physiotherapists association', @equivalentId)

SET @equivalentId = 'c2d07afb-b5a6-4937-9ee4-29d5736a7d74'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('engineers australia', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('institute of engineers', @equivalentId)

SET @equivalentId = '6b2c01a5-014d-4afa-89fb-ce6c08189e39'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('yellowpages', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('yellow pages', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('yellowpages.com.au', @equivalentId)

SET @equivalentId = 'd33e9920-a1ab-4179-bbe7-2b5fb85ff77a'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('whitepages', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('white pages', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('whitepages.com.au', @equivalentId)

SET @equivalentId = '7d9d661f-3fde-468f-8324-42b3a5ccf266'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('license', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('licence', @equivalentId)

SET @equivalentId = 'fe392716-8a4a-4079-9035-b772dc1fde6a'

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

SET @equivalentId = 'bf4fbadf-5f43-4131-9727-bc321a63697f'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('asfa', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('association of superannuation funds of australia', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('superannuation association', @equivalentId)

SET @equivalentId = '6aca9dd9-57e0-495f-9a35-ca98a0d7c1a8'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('retailer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('retail company', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('retail organisation', @equivalentId)

SET @equivalentId = '89f52336-1ad4-46b7-87e2-844aac06004d'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('ara', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('australian retailers association', @equivalentId)

SET @equivalentId = '7d1e8675-a340-483f-a904-a7473332621c'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('bdouble', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('b double', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('road train', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('roadtrain', @equivalentId)

SET @equivalentId = 'a921b1cd-1c3d-4d94-8dc4-764a82544b9a'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('carpark', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('car park', @equivalentId)

SET @equivalentId = 'de80efca-8c03-4460-a46d-986a2ddad1fa'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('formwork', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('form work', @equivalentId)

SET @equivalentId = '7eca10c1-9e95-4932-83f9-039c3f098e36'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('agribusiness', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('agri business', @equivalentId)

SET @equivalentId = '761bc6e0-fbff-4769-91c5-ec34dffb0e9f'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('freelance', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('free lance', @equivalentId)

SET @equivalentId = 'e6140aaa-ebb8-4e0d-a715-b2e68e6825a7'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('voip', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('voice over internet protocol', @equivalentId)

SET @equivalentId = '28bcba42-908a-4714-89c4-1f1fd5ee9f4a'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('statutory', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('stat', @equivalentId)

SET @equivalentId = '23295cc5-0c0a-4915-8e90-0e1d35b0193e'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('forex', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('foreign exchange', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('fx', @equivalentId)

SET @equivalentId = 'cd0e8818-071e-4c34-8f53-0f7da5d7d31d'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('expat', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('ex pat', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('ex patriate', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('expatriate', @equivalentId)

SET @equivalentId = '9a4025a3-de32-41b8-b04f-c8603df9f4f1'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('six sigma', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('sixsigma', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('6sigma', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('6 sigma', @equivalentId)

SET @equivalentId = '68c735a8-83c1-4ec6-b588-ce785178b18e'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('streetwear', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('street wear', @equivalentId)

SET @equivalentId = '2d8bb2cd-5114-4a4a-bfde-4ad75325a0ee'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('menswear', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('mens wear', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('mens clothing', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('mens fashion', @equivalentId)

SET @equivalentId = '9ef02be3-e4bd-4c1a-ba1a-7319497b5128'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('childrenswear', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('childrens wear', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('childrens clothing', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('childrens fashion', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('kids clothing', @equivalentId)

SET @equivalentId = '2e03df3f-c435-4b81-a3a9-d2a480b51752'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('womenswear', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('womens wear', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('womens clothing', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('womens fashion', @equivalentId)

SET @equivalentId = '73520bc2-8431-4ae8-bfa4-927002e5ed63'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('lingerie', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('intimate apparel', @equivalentId)

SET @equivalentId = 'd766d12a-5aef-477d-b7c6-4d7f2d0d3001'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('lcms', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('learning content management system', @equivalentId)

SET @equivalentId = 'a5b77388-a488-4753-996f-1b28d6352315'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('super market', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('supermarket', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('hyper market', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('hypermarket', @equivalentId)

SET @equivalentId = 'fc17bbee-1371-44ac-b34c-f3c4fb382342'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('alumni', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('alumnus', @equivalentId)

SET @equivalentId = 'f5062ad6-b5aa-4aff-9ea5-b0bf5175d8d0'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('diecast', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('die cast', @equivalentId)

SET @equivalentId = '16a45632-82d9-4b72-8191-9b5d3ad25f13'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('4wd', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('4 wheel drive', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('four wheel drive', @equivalentId)

SET @equivalentId = '44324504-b3f2-47ed-9a9a-f23d151eaca9'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('corporation', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('corp', @equivalentId)

SET @equivalentId = 'c0661ee3-e918-4911-9d23-2f1a7188bb87'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('bachelor of arts', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('B arts', @equivalentId)

SET @equivalentId = 'b592e796-d78a-40a9-b671-ecaf34a83439'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('diploma', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('associate diploma', @equivalentId)

SET @equivalentId = '1fbaaaa1-7177-4708-bf49-20daa58d8ba8'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('degree', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('bachelor', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('bachelor''s degree', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('bachelors degree', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('bachelor of', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('graduate degree', @equivalentId)

SET @equivalentId = '604add2e-6bbe-447d-a03b-fad327daca07'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('masters', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('masters degree', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('master''s degree', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('masters in', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('post grad diploma', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('postgraduate diploma', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('post graduate degree', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('postgraduate degree', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('postgrad degree', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('postgrad', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('post graduate', @equivalentId)

SET @equivalentId = '1be48635-ea85-4279-aa24-7f6af59e73a0'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('phd', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('doctorate', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('doctor of philosophy', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('p h d', @equivalentId)

SET @equivalentId = '786bc01b-395a-40eb-9010-977a83a57fdd'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Bureau of meteorology', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('the bureau', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('weather bureau', @equivalentId)

SET @equivalentId = '49a52b4b-5834-4588-80dc-38f642de768e'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('CFA', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('C.F.A.', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Country fire authority', @equivalentId)

SET @equivalentId = 'c35789f1-3f2f-43ce-977e-9684c4eae34f'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('MFB', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('M.F.B.', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Metropolitan Fire brigade', @equivalentId)

SET @equivalentId = 'ef2e5154-0560-414a-be87-0f49020d3f54'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('anf', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('australian nursing federation', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('australian nurses federation', @equivalentId)

SET @equivalentId = '76ff64ea-c2c8-4392-8c56-01e6a9a356c0'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Australian Council of Trade Unions', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('ACTU', @equivalentId)

SET @equivalentId = 'c5e84823-0e72-46df-ad7d-9aa413b86327'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('NSW Teachers Federation', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('NSWTF', @equivalentId)

SET @equivalentId = 'ff871538-128d-4bf8-9112-efef0bed5741'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Construction Forestry Mining and Energy Union', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('CFMEU', @equivalentId)

SET @equivalentId = '126dccc7-1c36-4669-9669-0c2af3e9cde1'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Queensland Teachers Union', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('QTU', @equivalentId)

SET @equivalentId = 'd5c51029-e820-40f0-bcb4-5810b16d3564'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Queensland Nurses Union', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('QNU', @equivalentId)

SET @equivalentId = 'a53747fd-e514-4843-919e-8f13e8865de8'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Australian Education Union Victorian Branch', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('AEUVB', @equivalentId)

SET @equivalentId = 'd4140133-da2f-4152-8dde-f6ca9065b889'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Australian Nursing Federation Victoria', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('ANFV', @equivalentId)

SET @equivalentId = '717798f6-e450-4b97-b95d-6d9260146d71'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Australian Services Union', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('ASU', @equivalentId)

SET @equivalentId = '35fba6dd-62a0-4d34-8314-3826ff7810b0'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('NSW Nurses'' Association', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('NSWNA', @equivalentId)

SET @equivalentId = '9c191358-c143-479b-9029-88a7dd31ed58'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Transport Workers'' Union of Australia', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('TWUA', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('TWU', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('transport workers union', @equivalentId)

SET @equivalentId = '2ff04906-88e2-464c-ac91-4b1a6d06f5cc'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Communications Electrical and Plumbing Union of Australia', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('CEPUA', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('CEPU', @equivalentId)

SET @equivalentId = 'e52d807f-50de-48cf-9400-0d140de6970c'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Electrical Trades Union of Australia', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('ETU', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('ETUA', @equivalentId)

SET @equivalentId = '8cd3dfac-8000-423b-9655-1edb4292ef06'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Queensland Independent Education Union', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('QIEU', @equivalentId)

SET @equivalentId = 'aa01cb9b-ada5-49ef-a854-54f8c6337346'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Australian Nursing Federation South Australia', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('ANFSA', @equivalentId)

SET @equivalentId = 'a0499972-cb2d-4f82-84eb-44d8ae0abe22'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Australian Education Union South Australian Branch', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('AEUSAB', @equivalentId)

SET @equivalentId = 'f0075de8-9101-4cb4-8a6b-3cbdec668f22'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('State School Teachers'' Union of Western Australia', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('SSTUWA', @equivalentId)

SET @equivalentId = '500fcd02-829f-42aa-a3aa-3bf212324357'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Victorian Independent Education Union', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('VIEU', @equivalentId)

SET @equivalentId = '571df8e5-b031-43fc-8cd0-c2df591dc20b'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Medical Scientists Association of Victoria', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('MSAV', @equivalentId)

SET @equivalentId = 'bd3d5289-e340-403d-be1e-ec5e0c11c91e'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Community and Public Sector Union', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('CPSU', @equivalentId)

SET @equivalentId = '5eee816b-adf3-4133-a97e-0174b39384fc'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Australian Education Union ACT Branch', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('AEUACT', @equivalentId)

SET @equivalentId = '92a41034-24aa-4b01-9085-3dbf5bc9698d'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Australian Education Union Northern Territory Branch', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('AEUNT', @equivalentId)

SET @equivalentId = '81202106-d802-4ae9-b3e9-bc1065446f1e'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Australian Services Union Victorian Branch', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('ASUV', @equivalentId)

SET @equivalentId = '7cc81e8b-c717-477f-aa5d-1f215523300d'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Australian Services Union New South Wales Branch', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('ASUNSW', @equivalentId)

SET @equivalentId = 'a00717c2-7398-49a2-bfb7-338d5890f7f5'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Australian Education Union Tasmanian Branch', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('AEUT', @equivalentId)

SET @equivalentId = 'd50aea6e-b04f-4d1b-ab3d-cabaeabbc57a'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Australian Nursing Federation Western Australia', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('ANFWA', @equivalentId)

SET @equivalentId = 'd429295c-a17b-4125-bdc8-57a13c3fd861'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Health and Community Services Union', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('HACSU', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('HCSU', @equivalentId)

SET @equivalentId = '6c8b0812-14ec-4221-bfe9-2eeaf8d3a1fb'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Health Services Union of Australia', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('HSUA', @equivalentId)

SET @equivalentId = 'ba023e6d-9c07-457b-a92b-be65d36a3cd6'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('NSW/ACT Independent Education Union', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('NSWACTIEU', @equivalentId)

SET @equivalentId = '8fa38674-d5c7-47a1-abde-e41a269864c1'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Evatt Foundation', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('EVATT', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('EF', @equivalentId)

SET @equivalentId = '9343241a-c313-443a-9095-b0851f76486d'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Tasmanian Catholic Education Employees'' Association', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('TCEEA', @equivalentId)

SET @equivalentId = 'dcb61989-0558-4316-9dac-0fd3a5f59ac7'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('CEPU New South Wales Communications Division', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('CEPUNSWCD', @equivalentId)

SET @equivalentId = '0d5136f9-d41d-4887-b346-975e5f6ab7d0'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Health and Research Employees Association', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('HREA', @equivalentId)

SET @equivalentId = '00729cc2-1a90-42ec-badd-7ec7925a2cd0'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Association of Non-Government Education Employees', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('ANGEE', @equivalentId)

SET @equivalentId = 'c15ad402-f321-42e9-a752-45e00068b320'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Australian Services Union Queensland Branch', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('ASUQ', @equivalentId)

SET @equivalentId = '6214eb44-8886-4518-9bd5-9dbaf6f5c32e'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Builders Labourers Federation Queensland', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('BLFQ', @equivalentId)

SET @equivalentId = 'e8d05878-85e7-4f43-a1d4-b20b3caa7aca'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('NSW Fire Brigade Employees Union', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('FBEU', @equivalentId)

SET @equivalentId = '8e7cfab6-aca1-4eb1-9ce0-9bc28415d0f6'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Australian Workers Union Queensland', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('AWUQ', @equivalentId)

SET @equivalentId = 'dfbfe0eb-ec56-449c-9a1e-feeede3624bb'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Shop Distributive & Allied Employee''s Union', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('SDAEU', @equivalentId)

SET @equivalentId = 'c4747581-79de-4871-8fd5-db8f0c3b78d4'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Electrical Trades Union Victorian Branch', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('ETUV', @equivalentId)

SET @equivalentId = 'd5a3de20-f2c5-4864-8217-73d94e90e872'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Labor Council of New South Wales', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('LCNSW', @equivalentId)

SET @equivalentId = 'd9f3be5f-d96a-4ae5-baa9-9017665fc4aa'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('United Trades and Labor Council South Australia', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('UTLCSA', @equivalentId)

SET @equivalentId = '7fb5726f-ade4-4f6a-8223-060a8e33e152'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Finance Sector Union of Australia', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('FSU', @equivalentId)

SET @equivalentId = '396a70de-976e-4572-8334-45337c23453d'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('ACT Trades and Labour Council', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('ACTTLC', @equivalentId)

SET @equivalentId = '1e04c7ff-5cfe-495e-88f1-41d347cbbeaa'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Liquor Hospitality and Miscellaneous Workers Union', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('LHMU', @equivalentId)

SET @equivalentId = '77626899-46d6-46d0-9a05-7bf7cc737f97'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Australian Workers Union South Australia', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('AWUSA', @equivalentId)

SET @equivalentId = '05dc2fbe-2995-42bc-a160-fe22d594a988'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Municipal and Shire Employees Union of NSW', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('MSEU', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('MSEUNSW', @equivalentId)

SET @equivalentId = '33cf06b2-0db9-4964-abf7-7160dbaccb3d'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Victorian Trades Hall Council', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('VTHC', @equivalentId)

SET @equivalentId = '0fd5ae14-afb7-4681-b393-21a16d7eec56'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Australian Trade Union Archives', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('ATUA', @equivalentId)

SET @equivalentId = '38dc73f3-91a3-4df3-8a04-b62c3e03af6e'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('United Firefighters Union of Australia - Queensland Branch', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('UFUA', @equivalentId)

SET @equivalentId = '6003f759-dc7d-4d24-87f5-e02f2e5b0106'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Australian Manufacturing Workers'' Union', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('AMWU', @equivalentId)

SET @equivalentId = '4b6c4631-f61a-4cfe-90d9-ea97028c8642'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Australian Services Union ACT Branch', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('ASUACT', @equivalentId)

SET @equivalentId = '61db5337-f9bd-492a-81ce-4a90d127433e'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Trades and Labor Council of Western Australia', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('TLCWA', @equivalentId)

SET @equivalentId = 'b369f4f1-cf9e-4d87-bc75-2ab3e0981956'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Electrical Trades Union New South Wales Branch', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('ELUNSW', @equivalentId)

SET @equivalentId = '9a9ddf22-9aed-4945-8429-3408e1fb2633'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Unions Tasmania', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('UT', @equivalentId)

SET @equivalentId = 'c20207d2-8453-4757-bd3b-bad3a4dc1ff3'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('National Union of Workers', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('NUW', @equivalentId)

SET @equivalentId = 'c1b8694c-7725-4587-b8c2-5fb50bcbd8ee'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('IT Workers Alliance', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('ITWA', @equivalentId)

SET @equivalentId = '05f59f4f-66db-44bd-8a66-71e65d9428ca'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('State Public Services Federation Tasmania', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('SPSFT', @equivalentId)

SET @equivalentId = 'a9615d70-7f6f-4dcd-8452-af0f548daf32'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Australian Services Union Northern Territory Branch', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('ASUNT', @equivalentId)

SET @equivalentId = 'c95115c3-f02b-4120-b0d6-864c6f9384aa'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Queensland Council of Unions', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('QCU', @equivalentId)

SET @equivalentId = '08364776-c855-4497-b3ca-dded727b2d19'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('United Firefighters Union of Western Australia', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('UFUWA', @equivalentId)

SET @equivalentId = 'e2f3f5b9-8fb0-405b-bd0a-6fef31f55c53'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Australian Services Union Western Australia Branch', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('ASUWA', @equivalentId)

SET @equivalentId = 'a418b908-dd92-4a12-ad88-82d2b6e3548f'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Institute of Chartered Accountants of New Zealand', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('ICANZ', @equivalentId)

SET @equivalentId = 'caedf783-eb5b-40d8-a323-b90fb027d3e4'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Australian Acupuncture and Chinese Medicine Association', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('AACMA', @equivalentId)

SET @equivalentId = 'b7593cdc-c88b-471d-8494-1c01a05bf8e1'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Australian Agricultural and Resource Economics Society Inc.', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('AARESI', @equivalentId)

SET @equivalentId = '771dbe15-41d5-4daa-a07c-151df47d8e1f'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Australian Institute Of Agricultural Science and Technology', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('AIAST', @equivalentId)

SET @equivalentId = 'c14de3e5-aee8-4706-90ec-7ec78dc9f11d'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('National Farmers'' Federation', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('NFF', @equivalentId)

SET @equivalentId = '48b37a4a-84ff-4c5c-94b3-c774e95d4c90'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Australian & New Zealand College of Anaesthetists', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('ANZCA', @equivalentId)

SET @equivalentId = 'e29b3039-cd38-4f0a-8a1e-d6ffb510cb0f'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Australian Society of Anaesthetists', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('ASA', @equivalentId)

SET @equivalentId = 'c03edc6f-c8ed-471c-bedb-cdcf9c926d3d'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Royal Australian Institute of Architects', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('RAIA', @equivalentId)

SET @equivalentId = '849426bb-ee22-46e9-a343-2109355c3895'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Australian Housing and Urban Research Institute', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('AHURI', @equivalentId)

SET @equivalentId = '3c99eb98-eef0-4963-be32-c3c372e958df'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('New Zealand Society of Archivists', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('NZSA', @equivalentId)

SET @equivalentId = 'c17c7c0a-d9db-40ff-9c91-e4711dc02f5b'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Records Management Association of Australia', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('RMAA', @equivalentId)

SET @equivalentId = 'd1958bab-0c35-40ad-a0b1-d50c50323325'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Australian Association of Audiologists in Private Practice Inc.', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('AAAPP', @equivalentId)

SET @equivalentId = 'f9d6e86d-964a-47be-83ec-b6dd3f3f347b'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('New Zealand Audiological Society', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('NZAS', @equivalentId)

SET @equivalentId = '7a829814-9ad0-40ae-bb4c-870f7469396f'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Speech Pathology Australia', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('SPA', @equivalentId)

SET @equivalentId = '60c56864-544b-4da5-b98f-d1b79d4c03ac'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Australian Society for Biochemistry and Molecular Biology', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('ASBMB', @equivalentId)

SET @equivalentId = '0304cc7b-31b3-461b-922b-c78ce2acf332'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Australian and New Zealand Society for Comparative Physiology and Biochemistry', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('ANZSCPB', @equivalentId)

SET @equivalentId = '277c137c-8c42-4ac9-94d4-705aef83566f'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('The Australian Society For Fish Biology', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('ASFB', @equivalentId)

SET @equivalentId = 'add5e754-7404-4bba-a955-8a465bb6360d'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Centre for Biomedical Engineering', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('CBE', @equivalentId)

SET @equivalentId = '2c32e098-15f0-4dad-9bdd-767a9e6ce8b2'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('The Australian Society for Biophysics', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('ASB', @equivalentId)

SET @equivalentId = '6671ae6b-2096-4940-8115-fc0135d51091'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('New Zealand Biotechnology Association', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('NZBA', @equivalentId)

SET @equivalentId = 'aea3f544-429e-4d99-9ef1-0616a699e4a3'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Australian Systematic Botany Society Inc.', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('ASBS', @equivalentId)

SET @equivalentId = '6e6211ad-c8e2-4ec0-a30d-d74227a72dd6'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Cardiac Society of Australia and New Zealand', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('CSANZ', @equivalentId)

SET @equivalentId = 'b7434e34-4120-4636-a80a-dd0fc5d53d13'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Royal Australian Chemical Institute', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('RACI', @equivalentId)

SET @equivalentId = '78f15c51-ed49-4587-9948-3f4a151deae1'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('New Zealand Institute of Chemistry', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('NZIC', @equivalentId)

SET @equivalentId = 'b3c79b62-0b29-489b-be14-35cc74e641b8'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Society of Technical Communication', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('STC', @equivalentId)

SET @equivalentId = 'e3f59c8e-e7cb-4195-a247-1a24ac02d07c'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Australasian Society for Computers In Learning In Tertiary Education', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('ASCILITE', @equivalentId)

SET @equivalentId = 'd3d1a50d-539c-47bb-971e-087d580cafe4'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Australian Council for Computers in Education', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('ACCE', @equivalentId)

SET @equivalentId = '6b5289ad-67e7-4128-bd7f-7378d9691980'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Technology Education Federation of Australia', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('TEFA', @equivalentId)

SET @equivalentId = 'c5eba3c3-8718-43ae-a7a9-8e8342385b3c'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('The Australian Computer Society', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('ASC', @equivalentId)

SET @equivalentId = 'a9803c33-8168-4604-aae1-c88905d390a1'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('The Australian Pattern Recognition Society', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('APRS', @equivalentId)

SET @equivalentId = '91f01f18-63cf-4901-9614-1862defe7db3'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('System Administrators Guild of Australia', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('SAGA', @equivalentId)

SET @equivalentId = '7bba3780-2b12-4549-9276-3a197cd3a56b'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Society of Crystallogaphers in Australia and New Zealand', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('SCANZ', @equivalentId)

SET @equivalentId = '3fb4b9c0-4749-4832-aded-28edeff065d4'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Australian Society of Forensic Dentistry', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('ASFD', @equivalentId)

SET @equivalentId = 'ef7ad04c-ecc3-470a-a081-eae9700b8468'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Australian Dental Association', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('ADA', @equivalentId)

SET @equivalentId = 'ae08198d-5ac9-4ee5-8612-0d3962540023'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Australian Dental Association Queensland Branch', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('ADAQ', @equivalentId)

SET @equivalentId = 'edbad0d5-7a90-42c3-a56b-ca42b2537a71'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Dental Board of Queensland', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('DBQ', @equivalentId)

SET @equivalentId = '5ad6004f-a956-4026-99ec-0eb90e8ba8e8'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Dental Technicians & Prosthetists Board of Queensland', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('DTPBQ', @equivalentId)

SET @equivalentId = '72431a06-a672-4988-b16d-2353ed453719'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('NSW Dental Assistants'' Association', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('NSWDAA', @equivalentId)

SET @equivalentId = '193f7d52-77d5-48c9-8f88-5fcc0d314b95'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Royal Australasian College of Dental Surgeons', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('RACDS', @equivalentId)

SET @equivalentId = 'cbb058e2-9b2b-4054-968e-f22c74754cf2'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('New Zealand Dental Association', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('NZDA', @equivalentId)

SET @equivalentId = '79ab2c61-2606-4f12-bb27-eb9d535ea44c'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Australasian College of Dermatologists', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('ACD', @equivalentId)

SET @equivalentId = '8fe53e7d-5073-4bec-9708-e27da4950aaf'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Design Institute of Australia', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('DIA', @equivalentId)

SET @equivalentId = '73aa8d0b-9fc0-4208-9442-dc74382209e9'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Dietitians Association of Australia', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('DAA', @equivalentId)

SET @equivalentId = 'd8b691f2-96b2-4e18-98b4-d396d2e5cc7b'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Law and Economics Association of New Zealand Incorporated', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('LEANZI', @equivalentId)

SET @equivalentId = 'b8d555e4-c3bf-4df6-8dc5-6257ef1e9fd4'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Australian Association for Research in Education', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('AARE', @equivalentId)

SET @equivalentId = '83fb399c-83ae-4795-bd29-7b1696489770'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Australian Society for Music Education', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('ASME', @equivalentId)

SET @equivalentId = 'b0a6d54b-7098-4848-9146-fe3058960cc7'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Australian Society for Educational Technology', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('ASET', @equivalentId)

SET @equivalentId = '9d74fb68-b9c3-47f8-94bc-681d45adcc54'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Association of Consulting Engineers Australia', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('ACEA', @equivalentId)

SET @equivalentId = 'b0e0d84b-7c59-4190-9b54-60a11983cb59'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Institution of Engineering and Mining Surveyors Australia', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('IEMSA', @equivalentId)

SET @equivalentId = 'ace236ef-039c-45a5-8222-a5a4da401c72'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Australian Entomological Society', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('AES', @equivalentId)

SET @equivalentId = '5f0a2cd8-c23c-48cd-9d53-62f9f0f72c63'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('The Entomological Society of Victoria', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('ESV', @equivalentId)

SET @equivalentId = 'f6efb221-28b8-4698-8811-a9a64bc58296'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Australian Association for Professional & Applied Ethics', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('AAPAE', @equivalentId)

SET @equivalentId = '990a710e-01df-49e2-aa20-60b996b0c8ca'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Victorian Institute of Forensic Medicine', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('VIFM', @equivalentId)

SET @equivalentId = '6a242264-5a3a-4336-a70d-d2ac64000c41'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Institute of Foresters of Australia', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('IFA', @equivalentId)

SET @equivalentId = '792ba260-77ca-47f0-8d47-4ae7488dbbbc'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Australian Funeral Directors Association', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('AFDA', @equivalentId)

SET @equivalentId = '3e87629e-af38-449e-ace8-658cbca00c3b'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('The Institute of Australian Geographers', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('IAG', @equivalentId)

SET @equivalentId = 'a0b3b943-da34-4502-b66f-37d31115e694'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('The Geological Society of Australia', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('GSA', @equivalentId)

SET @equivalentId = 'e489dc36-0e9e-495d-83fe-b847c5c78927'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Australian Institute of Geoscientists', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('AIG', @equivalentId)

SET @equivalentId = '73463903-fee6-4e51-a280-41e831609e38'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('The Australian Society of Exploration Geophysicists', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('ASEG', @equivalentId)

SET @equivalentId = 'd42a82cc-745a-45ea-abd1-abb91e23f623'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Australian College of Health Service Executives', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('ACHSE', @equivalentId)

SET @equivalentId = '5f275d3e-32af-4712-b725-e7002a28de71'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Australian Council of Deans of Education', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('ACDE', @equivalentId)

SET @equivalentId = 'a7a02131-9509-48e5-a40e-5daca210c6ef'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Association of Heads of Australian University Colleges and Halls Inc.', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('AHAUCH', @equivalentId)

SET @equivalentId = 'ae18e538-71bb-4758-9289-e17017101a02'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Association for Tertiary Education Management Inc.', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('ATEM', @equivalentId)

SET @equivalentId = 'c181a481-d24e-4da9-8778-8983ecb8ed41'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Australasian Research Management Society', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('ARMS', @equivalentId)

SET @equivalentId = '1278959a-a4c8-429f-a51f-b0dea6d37944'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Australasian Association for Institutional Research', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('AAIR', @equivalentId)

SET @equivalentId = 'd1438ea5-6158-4bf8-b361-e86ada75b7c7'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Council of Australian University Directors of Information Technology', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('CAUDIT', @equivalentId)

SET @equivalentId = 'a52e2ee9-849b-4a90-bba9-16eaac3c4c90'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Council of Australian University Librarians', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('CAUL', @equivalentId)

SET @equivalentId = '372c2ea6-19e5-416c-9db4-cc6611fdc038'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('National Colloquium of Senior University Women', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('NCSUW', @equivalentId)

SET @equivalentId = '9787561b-3841-4aab-a557-fd5bd97a5ff0'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Royal Australian Historical Society', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('RAHS', @equivalentId)

SET @equivalentId = 'c35f3216-5cb1-4ef8-9508-b273fc75612b'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Australian and New Zealand Intensive Care Society', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('ANZICS', @equivalentId)

SET @equivalentId = '05ab581a-5cfd-413f-b0a8-4804e032c6f8'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Media Entertainment and Arts Alliance', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('MEAA', @equivalentId)

SET @equivalentId = '1ae110df-5e59-4f2d-b438-6793d8d159f1'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('New Zealand Property Institute', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('NZPI', @equivalentId)

SET @equivalentId = '56c45ca1-836a-4eb6-9267-38827d7692a4'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Australian Institute of Judicial Administration', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('AIJA', @equivalentId)

SET @equivalentId = '35f7c65d-9bae-477d-9a20-2398e3d37090'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Australian and New Zealand Sports Law Association', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('ANZSLA', @equivalentId)

SET @equivalentId = '18334a5e-6696-4c3f-864b-bcc2fbdc1c34'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Law Society Northern Territory', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('LSNT', @equivalentId)

SET @equivalentId = '7e2cb510-4b66-4ea5-8e5d-8e81902b3d53'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Law Society of New South Wales', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('LSNSW', @equivalentId)

SET @equivalentId = 'd98f647f-2011-45d7-9ef6-b4fa48867aed'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Law Society of South Australia', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('LSSA', @equivalentId)

SET @equivalentId = '95d72dd7-2349-40f4-b334-cef514981cea'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Law Society of Western Australia', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('LSWA', @equivalentId)

SET @equivalentId = '702f9ae8-6af2-49df-a4f3-8efa73b0ec0a'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('New South Wales Bar Association', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('NSWBA', @equivalentId)

SET @equivalentId = 'c8dff2a5-c350-4c8f-9f0f-5160d36526cc'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('New Zealand Law Society', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('NZLS', @equivalentId)

SET @equivalentId = 'da43720f-3e09-4e98-879c-bac2eee363ea'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Queensland Law Society', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('QLS', @equivalentId)

SET @equivalentId = 'aa1a3a30-13d1-4ebd-a86b-b141bb40900e'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Australian Library and Information Association', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('ALIA', @equivalentId)

SET @equivalentId = '07456286-19be-467d-8404-47624d70c6bb'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Council of Australian State Libraries', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('CASL', @equivalentId)

SET @equivalentId = 'd70e3a3a-8961-4f88-8d55-66cb06e4f842'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Institute of Management Consultants', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('IMC', @equivalentId)

SET @equivalentId = 'b162d3aa-91be-4ec7-a7d1-24358a43cfdd'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Australian Marine Sciences Association', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('AMSA', @equivalentId)

SET @equivalentId = '6f1e0f3e-e56f-4d7d-b43f-4eeed5c69181'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('The Australian Mathematical Society', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('AMS', @equivalentId)

SET @equivalentId = 'f002b56d-4f59-4446-9656-f0f0fb3034a5'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Applied Mathematics Division of the Australian Mathematical Society', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('ANZIAM', @equivalentId)

SET @equivalentId = '09e07757-e47a-4341-a7d3-935af6d28b0b'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('New Zealand Mathematical Society', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('NZMS', @equivalentId)

SET @equivalentId = 'a076113c-8653-4248-8aa3-3795f760ee0b'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Australian Institute of Medical Scientists', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('AIMS', @equivalentId)

SET @equivalentId = '8317d3be-b9ec-4693-8087-15a894bb6545'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Australian Society for Medical Research', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('ASMR', @equivalentId)

SET @equivalentId = '69bc109e-b3f2-48ce-9a77-3bfdd24665cf'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Australian College of Rural and Remote Medicine', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('ACRRM', @equivalentId)

SET @equivalentId = '84185743-04cb-493d-8b8a-3c39790474ee'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Australian and New Zealand Head and Neck Society', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('ANZHNS', @equivalentId)

SET @equivalentId = 'd25cb54d-f5a6-4f69-bc69-1f29462a6888'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Australian and New Zealand Association of Physicians in Nuclear Medicine', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('ANZAPNM', @equivalentId)

SET @equivalentId = '613a58fc-5141-462a-aaa9-d16c17820952'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Australian Medical Writers Association', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('AMWA', @equivalentId)

SET @equivalentId = '3bc458b8-925a-4e57-89d4-7d4ce34f4e1a'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Australian Society for Geriatric Medicine', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('ASGM', @equivalentId)

SET @equivalentId = '747298de-4af9-47b3-a5d2-001d77d99496'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Children''s Medical Research Institute', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('CMRI', @equivalentId)

SET @equivalentId = 'a59aa502-0014-43d3-a857-5ca651f2c444'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Doctors'' Reform Society of Australia', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('DRSA', @equivalentId)

SET @equivalentId = '88c86b21-662d-4e2c-99b7-50daf6e99a42'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Royal Australasian College of Physicians', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('RACP', @equivalentId)

SET @equivalentId = '4524cd80-565f-4045-bdf9-b4f9e53d18fa'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Australian Meteorological and Oceanographic Society', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('AMOS', @equivalentId)

SET @equivalentId = 'f4e428ec-4f9a-459d-aa62-8d69e57d67d3'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('The Australian Society for Microbiology', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('ASM', @equivalentId)

SET @equivalentId = '6243af97-2cb4-428b-98a2-75e3465164f8'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Australasian Institute of Mining and Metallurgy', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('AIMM', @equivalentId)

SET @equivalentId = 'dd75f909-6114-45d7-a71f-7a53b6d84f3a'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Australian Association of Neurologists', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('AAN', @equivalentId)

SET @equivalentId = 'c45b63eb-61a3-43ed-be33-2410d09bce46'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Australian Neuroscience Society', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('ANS', @equivalentId)

SET @equivalentId = '4de661a9-8f45-4ea8-80d9-b069cc3a10be'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Neurosurgical Society of Australasia', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('NSA', @equivalentId)

SET @equivalentId = '6381b614-a619-4e70-af06-c79a13899b3f'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Australian and New Zealand College of Mental Health Nurses', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('ANZCMHN', @equivalentId)

SET @equivalentId = '3761df03-dd8e-4300-b387-3bac9819edb7'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Australian College of Critical Care Nurses', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('ACCCN', @equivalentId)

SET @equivalentId = 'a697bfdf-274e-420c-8444-582bf073fa45'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Australian College of Midwifery Incorporated', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('ACMI', @equivalentId)

SET @equivalentId = '498a8537-f974-4ec1-9635-ddcf95512be5'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Australian Neonatal Nurses Association', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('ANNA', @equivalentId)

SET @equivalentId = '14180e5c-a4a4-464c-bb85-c5303c632e66'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Australian Nursing Council Inc.', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('ANC', @equivalentId)

SET @equivalentId = '119f71f3-050d-4b70-a2a9-dda59b882213'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Nursing Council of New Zealand', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('NCNZ', @equivalentId)

SET @equivalentId = 'c15c5967-d78b-4f39-a938-e51656f0fc0f'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Royal College of Nursing Australia', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('RCNA', @equivalentId)

SET @equivalentId = '9e6c5be8-3284-4ab5-8e50-4a92cd1366b9'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Royal Australian College of Obstetricians & Gynaecologists', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('RACOG', @equivalentId)

SET @equivalentId = 'a0854e8b-8332-4342-a88c-5093d67365ee'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Occupational Therapy Australia', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('OTA', @equivalentId)

SET @equivalentId = 'caf634a7-ad93-47f6-a23a-80c0564568c2'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('New Zealand Association of Occupational Therapists', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('NZAOT', @equivalentId)

SET @equivalentId = '157b780e-0839-49c3-8d40-2be15e0eb9c5'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Royal Australian and New Zealand College of Ophthalmologists', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('RANZCO', @equivalentId)

SET @equivalentId = 'fa25f3f9-5872-415a-a89a-424a27344952'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('The Australian Optical Society', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('AOS', @equivalentId)

SET @equivalentId = 'c660b1de-c0ca-4ae4-98a8-0d1ad6229c5f'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Optics Group at the University of Melbourne', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('OGUM', @equivalentId)

SET @equivalentId = '6626c707-bb81-4687-a52c-78308424348b'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Sydney University Physical Optics', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('SUPO', @equivalentId)

SET @equivalentId = '8ed2a852-b564-4604-affe-d4fc6b206778'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Quantum Technology University of Queensland', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('QTUQ', @equivalentId)

SET @equivalentId = 'ca6817ed-9ad2-40db-a1c1-f029e9123038'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Centre for Quantum Computer Technology', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('CQCT', @equivalentId)

SET @equivalentId = 'b3f76ef5-8c7f-443d-991a-0c94a8f7a489'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Optometrists Association Australia', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('OAA', @equivalentId)

SET @equivalentId = 'c29aa08d-06b1-4fb2-a1c5-a0e65b90197b'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Australian Orthodontic Institute', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('AOI', @equivalentId)

SET @equivalentId = '55601803-c2d2-4cd6-b1da-219f432bb653'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('International Association for Dental Research', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('IADR', @equivalentId)

SET @equivalentId = 'b24a787d-6aef-4873-b0bc-afb6583f91ba'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Asia-Pacific Orthopaedic Association', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('APOA', @equivalentId)

SET @equivalentId = '2bdcc60c-e391-4896-81e2-81fcf4f040dc'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Australian Osteopathic Association', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('AOA', @equivalentId)

SET @equivalentId = 'a7d06c26-0997-4d81-8679-4b45c2d8ba27'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Royal College of Pathologists of Australasia', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('RCPA', @equivalentId)

SET @equivalentId = '762543d4-a29b-4127-8e60-906249e2b3a8'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Australian Association of Consultant Pharmacy', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('AACP', @equivalentId)

SET @equivalentId = '9690b510-3408-43a5-850a-a95a4f70ef01'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Australasian Society of Clinical & Experimental Pharmacologists & Toxicologists', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('ASCEPT', @equivalentId)

SET @equivalentId = 'a1582c5a-02b5-4a68-95f2-0382320c211f'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Society of Hospital Pharmacists of Australia', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('SHPA', @equivalentId)

SET @equivalentId = 'e77d9b0f-6f30-4adb-8eea-a75706b7242c'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Australasian Society for General Relativity and Gravitation', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('ASGPG', @equivalentId)

SET @equivalentId = '2a20e89f-3d47-4549-86f6-20344ee19bc0'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Australian Institute of Physics', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('AIP', @equivalentId)

SET @equivalentId = '3220c91e-858c-4907-b6d8-f7acdfed0006'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Australian Federal Police Association', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('AFPA', @equivalentId)

SET @equivalentId = '0ae3e478-9535-4cd7-9a2d-ae82228e40f9'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('New Zealand Police Association', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('NZPA', @equivalentId)

SET @equivalentId = '384c8f5e-dd7e-4404-8a17-6df97830e356'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Police Association of New South Wales', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('PANSW', @equivalentId)

SET @equivalentId = '6978363f-4e5f-4ed9-975b-adc1246c51c9'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Police Association of South Australia', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('PASA', @equivalentId)

SET @equivalentId = '7e22441b-1999-444b-bac5-ef65e5f0bc55'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Police Association of Victoria', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('PAV', @equivalentId)

SET @equivalentId = '0fcbe934-aaf2-482a-9773-b14b8e683f1b'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Queensland Police Union', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('QPU', @equivalentId)

SET @equivalentId = '55516d25-ee0e-4bbf-a54a-b67ec23330e4'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Western Australian Police Union', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('WAPU', @equivalentId)

SET @equivalentId = 'c87d978a-5c15-45e5-894f-8325d992f5e2'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Royal Australian and New Zealand College of Psychiatrists', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('RANZCP', @equivalentId)

SET @equivalentId = '172ff2c2-b998-4315-bb2e-7bddcdb6875f'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Australian Psychological Society', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('APS', @equivalentId)

SET @equivalentId = '9639345a-e6c3-4a58-8029-38194de833cb'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Public Relations Institute of Australia', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('PRIA', @equivalentId)

SET @equivalentId = '4b3b665f-79e0-4615-947d-be98c483f127'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Australian Institute of Quantity Surveyors', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('AIQS', @equivalentId)

SET @equivalentId = '4b2febe1-e59d-4264-aed1-31c97b6b57b2'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Australian Diagnostic Imaging Association', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('ADIA', @equivalentId)

SET @equivalentId = 'e01ab5a1-5822-428f-b1bc-b496d8c50681'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('The Australian Institute of Radiography', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('AIR', @equivalentId)

SET @equivalentId = '50968473-5494-4586-a702-f5db507d4421'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Royal Australian and New Zealand College of Radiologists', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('RANZCR', @equivalentId)

SET @equivalentId = '2c16020f-0e53-4847-99d6-97d032d8d429'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Australian Property Institute', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('API', @equivalentId)

SET @equivalentId = '1bce041f-93f4-4d9f-88a7-3022d71413b2'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Real Estate Institute of Australia', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('REIA', @equivalentId)

SET @equivalentId = '7c882f38-d5f2-4f64-a320-2e873f91957b'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Real Estate Institute of New South Wales', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('REINSW', @equivalentId)

SET @equivalentId = '581c1d8a-6a7f-4c27-b969-531806714588'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Real Estate Institute of Queensland', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('REIQ', @equivalentId)

SET @equivalentId = 'c4c10cc9-6ac1-4b81-aaba-ca9b313b762e'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Real Estate Institute of South Australia', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('REISA', @equivalentId)

SET @equivalentId = 'a3359fa0-46a6-490e-8e18-5d92ebc1606c'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Real Estate Institute of Victoria', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('REIV', @equivalentId)

SET @equivalentId = '15e68f87-cef3-4b06-8412-240eb7eddcfe'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Real Estate Institute of Western Australia', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('REIWA', @equivalentId)

SET @equivalentId = '1f537205-59f3-4ed7-8c29-bef68188f7c7'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Rheumatology Health Professionals Association', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('RHPA', @equivalentId)

SET @equivalentId = 'a9599294-822c-40a8-82fd-f43e02c88924'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Australian Academy of Science', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('AAS', @equivalentId)

SET @equivalentId = 'c336e7b2-95f0-4222-b52c-3f37bb3b4462'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Australian Academy of Technological Sciences & Engineering', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('AATSE', @equivalentId)

SET @equivalentId = '0a04e8a2-076d-44e0-b6ff-dc5acd330e3b'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Royal Society of New Zealand', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('RSNZ', @equivalentId)

SET @equivalentId = 'cc4b24da-007c-424e-a911-093b4f97860f'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Australian Association of Social Workers', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('AASW', @equivalentId)

SET @equivalentId = '66c5b059-4271-4c98-a5ef-910e73ce8b31'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Australian Institute of Sport', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('AIS', @equivalentId)

SET @equivalentId = '025517ed-3c09-468e-b2e1-869133bb6990'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Australian Association for Exercise and Sport Science', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('AAESS', @equivalentId)

SET @equivalentId = 'f14b8db2-146e-4dd1-9cbe-f77518b44b05'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Statistical Society of Australia', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('SSA', @equivalentId)

SET @equivalentId = '1d016669-5538-4fac-8ae4-70c0933b8216'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Royal Australasian College of Surgeons', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('RACS', @equivalentId)

SET @equivalentId = '80143ec1-be47-49d3-918c-0fde9243a94e'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Australasian Society of Cataract and Refractive Surgeons', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('ASCRS', @equivalentId)

SET @equivalentId = '82bb561e-1fbf-4236-88fe-364ff3396e1f'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Institution of Surveyors Australia', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('ISA', @equivalentId)

SET @equivalentId = 'e313e459-3128-4412-b701-dc22f700e91a'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Taxation Institute of Australia', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('TIA', @equivalentId)

SET @equivalentId = '65f85563-7bd9-46bb-bada-e2ef446c338f'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Association of Professional Teachers', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('APT', @equivalentId)

SET @equivalentId = 'bcd05bdf-b3aa-43ef-926e-e531f1149979'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Australian Science Teachers Association', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('ASTA', @equivalentId)

SET @equivalentId = 'f12efe40-a7cb-4d4f-ba58-3c52f93179d1'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Independent Teachers Union of Australia', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('ITUA', @equivalentId)

SET @equivalentId = '719ceb41-1e21-4391-8095-8f7cb3759a7e'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('National Association of Agriculture Educators', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('NAAE', @equivalentId)

SET @equivalentId = '0dc0d799-5d0b-4d39-ae12-ef47fb5a532b'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Tourism Council of Australia', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('TCA', @equivalentId)

SET @equivalentId = 'ac8631dc-6d65-4536-8273-7def63ac3c45'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Australasian Society for Ultrasound in Medicine', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('ASUM', @equivalentId)

SET @equivalentId = '300a9333-0109-4485-8009-9bdf6ab47210'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Urological Society of Australasia', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('USA', @equivalentId)

SET @equivalentId = 'c45c03ea-1577-4ac2-8978-456aa7419c2e'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('New Zealand Institute of Valuers', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('NZIV', @equivalentId)

SET @equivalentId = '8aeb13d3-5677-4dbf-9c4c-161c05a06ced'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Australian Veterinary Association', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('AVA', @equivalentId)

SET @equivalentId = 'bbdc0c33-77e0-45e8-9128-5152b69a918b'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('New Zealand Veterinary Association', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('NZVA', @equivalentId)

SET @equivalentId = 'f399f045-0f75-4c93-a152-2b673194e51d'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Australian Education Union', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('AEU', @equivalentId)

SET @equivalentId = 'c152d8d2-f43d-4f49-9c21-6b050d78513c'

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

SET @equivalentId = '11814bcf-9172-4a88-b8da-ad4959ff3863'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('IT Coordinator', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('coordinator IT', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('information technology coordinator', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('IT Co*Ordinator', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('IT co-ordinator', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('co-ordinator IT', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('information technology co-ordinator', @equivalentId)

SET @equivalentId = 'a1ccea37-0d57-4147-b1e8-61c5cf9346db'

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

SET @equivalentId = 'd48f17ae-a844-4aff-8481-0216c678d760'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Network architect', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Network Technician', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Chief Network Architect', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('System Engineer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Systems Engineer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Solutions Architect', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Solution Architect', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('IT Enterprise Solution Lead Architect', @equivalentId)

SET @equivalentId = '821d69f5-2772-434a-8bb5-0df2cdac0e92'

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

SET @equivalentId = 'c71dd77a-781c-47d8-b8ce-6552b0326ecb'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Assistant Network Administrator', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Junior Network Administrator Cum Technical Support Assistant', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Junior Network Administrator', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Assistant Systems Administrator', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('IT System Administration Assistant', @equivalentId)

SET @equivalentId = '19f883bc-2aff-470d-aafe-91c9d07ecfa3'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Network Engineer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Network support engineer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Network Support', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('hardware engineer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Network Consultant', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Network Support Officer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Network Specialist', @equivalentId)

SET @equivalentId = '655f8bdf-2f8d-478b-804a-d897d50b8fe7'

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

SET @equivalentId = 'c28f02ac-e420-41c0-9921-6bd7b543ddc8'

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

SET @equivalentId = 'ed4647df-a1f8-473a-82f2-c3b44023bcd0'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Helpdesk Manager', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('help desk manager', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('help desk supervisor', @equivalentId)

SET @equivalentId = '46e6ef27-12e9-43b5-b95c-6243d5687ee1'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Assistant IT Manager', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Asst IT Manager', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Assistant IT Mgr', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Assistant Information Technology Manager', @equivalentId)

SET @equivalentId = 'df0f496d-259b-4d49-b6fe-85759a3ca3d2'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('IT Operations Support Graduate', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('IT Operations Support', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('IT Operations Assistant', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('IT Operations Executive', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('IT Operations Technician', @equivalentId)

SET @equivalentId = 'f3173312-f7f0-4b0d-93a7-071e8cbf07d4'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Software Architect', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Systems Architect', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Senior Analyst Programmer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Senior Software Architect', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Senior Software Design Engineer', @equivalentId)

SET @equivalentId = '8036000f-8d3f-4633-bcfa-01943c20a406'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Senior Developer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Lead Developer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Senior Programmer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Senior Software Engineer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Lead Software Engineer', @equivalentId)

SET @equivalentId = '6a9e93e0-3ca3-4389-b5fe-a7d12e569a7e'

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

SET @equivalentId = '6fb0a258-af16-4ed1-9f12-a33ae55e8aa4'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Junior Programmer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Junior Developer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Trainee Programmer', @equivalentId)

SET @equivalentId = '3e8c5bcc-47a4-4969-881e-f468187228cb'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Designer/Developer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Interface Developer', @equivalentId)

SET @equivalentId = '7ca5de89-08cc-4135-829f-d6dde190597c'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Systems Analyst/Programmer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Programmer/Analyst', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Programmer Analyst', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Analyst/Programmer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Senior Analyst/Programmer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Systems Analyst', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Senior Systems Analyst', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('System Analyst', @equivalentId)

SET @equivalentId = '8a7a2de0-6a6b-4bb0-a736-1ee8465e34bd'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Test Team Leader', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('TEST MANAGER', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Test Coordinator', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Testing Manager', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Senior Test Analyst', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Sr Test Analyst', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Senior Test Engineer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Test co-ordinator', @equivalentId)

SET @equivalentId = 'ccdee55d-ec7f-43a9-9f21-5d42bab4d55a'

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

SET @equivalentId = '936c4d38-4f2e-42c7-bb00-6527800c0203'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('usability', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('user interaction', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('user experience', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('ui', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('user interface', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('u.i.', @equivalentId)

SET @equivalentId = '7cd7be02-365a-4058-956b-f6617a71c259'

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

SET @equivalentId = '3f8618cc-f2db-454c-b764-c2a81f0af04e'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('IT Administrator', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('I.T. Administrator', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('IT Admin', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('I.T. Admin', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('IT Systems Officer', @equivalentId)

SET @equivalentId = 'af3e554b-e614-4c8a-9df5-c8ae652e8efb'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('IT Officer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('IT Assistant', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('I.T. Officer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('I.T. Assistant', @equivalentId)

SET @equivalentId = '31f95a73-abcb-413d-8f10-aead9153f2c1'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('IT Support Analyst', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('i.t. support analyst', @equivalentId)

SET @equivalentId = 'b91bdc04-857c-498e-adf2-8aca0b959090'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('IT Specialist', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('it guru', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('it expert', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('information technology specialist', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('information technology expert', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('i.t. specialist', @equivalentId)

SET @equivalentId = '9f32bdce-6311-4218-b910-b4ba46671718'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('IT Trainer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('IT Tutor', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('computer tutor', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('computer trainer', @equivalentId)

SET @equivalentId = '04bf136b-b6e9-4a8b-a6a0-6405ed1d7443'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('vb.net', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('visual basic.net', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('visual basic dot net', @equivalentId)

SET @equivalentId = '7eca9b9f-1ad8-45cf-b3cc-7a992e8232dd'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('VB', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Visual basic', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('visualbasic', @equivalentId)

SET @equivalentId = '413ac16c-941a-451d-8916-bd62668db02c'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('datacom', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('datacomm', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('data commuications', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('data com', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('data comm', @equivalentId)

SET @equivalentId = 'db2f1aec-f90d-4678-9382-6a11fd055e5f'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Telco', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Telecoms', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Telecom', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Telecomms', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Telecommunication', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Telecommunications', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('tel co', @equivalentId)

SET @equivalentId = 'c78a130e-4fc4-44b8-9f24-7dc420558c1d'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('PABX', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Private Automatic Branch eXchange.', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('pbx', @equivalentId)

SET @equivalentId = '708f2514-9c16-4709-9a20-e910aa712793'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Graphics/Multimedia', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('multimedia', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('multi media', @equivalentId)

SET @equivalentId = '16fc1182-c597-4d66-b0cf-f170eee5fd71'

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

SET @equivalentId = '104cfec0-e399-4a5a-9e5f-d03c3c1d2285'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Artist', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('artiste', @equivalentId)

SET @equivalentId = '092428f3-55ca-42c4-ac17-7ae54bf7754e'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('freelance graphic designer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Freelance Graphic Design', @equivalentId)

SET @equivalentId = '6d3d977c-9c0d-4d1a-9acc-30c7960ee4c7'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Web / Graphic Designer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Web/Graphic Designer', @equivalentId)

SET @equivalentId = 'bb36e3ed-bddd-48ad-befc-8354c6e80af5'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('MULTIMEDIA DEVELOPER', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Multimedia Designer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('MULTI MEDIA DEVELOPER', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Multi media Designer', @equivalentId)

SET @equivalentId = 'd5b0d4f6-9f9d-4c90-a6ca-13f6b9382eb6'

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
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('technical Analyst', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('technical process analyst', @equivalentId)

SET @equivalentId = '42d75901-6c65-4087-a985-1a313e13fbb4'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('SENIOR BUSINESS ANALYST', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Sr Business Analyst', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Senior Business Analyst/Project Manager', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Lead Senior Business Analyst', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Senior Business Analyst/ Test Lead', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Senior Business Analyst ICT Infrastructure', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Senior Business Analyst Business Assurance', @equivalentId)

SET @equivalentId = 'c0e67d89-53f3-47ac-ad77-70f756e6f784'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('IT Business Analyst', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('I.T. Business Analyst', @equivalentId)

SET @equivalentId = '87671e26-4392-4ac1-99fe-800d036509d7'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('data analyst', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('information analyst', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Business Information Analyst', @equivalentId)

SET @equivalentId = 'ea49716c-9b00-4e8f-bb37-aa7e1bd37b80'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Digital Producer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Online producer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('web producer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('site producer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('on line producer', @equivalentId)

SET @equivalentId = '6ecf3e7e-ced6-4fc9-9f2a-a2474b7022ed'

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

SET @equivalentId = 'ad2f563c-e0d7-4daf-9b22-79ae597231c8'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Mac operator', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('finished artist', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Apple Mac operator', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Macintosh operator', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Apple Macintosh operator', @equivalentId)

SET @equivalentId = 'c8823da5-e68f-47a9-8fd7-1f9748f02c7c'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('IT Business Specialists', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('IT Business Specialist', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('IT Account Manager', @equivalentId)

SET @equivalentId = '25aed1dc-22cb-4bf6-b938-ee85b82a43af'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('IT development manager', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('IT Dev Mgr', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('IT Dev Manager', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('IT Project Manager Team Leader', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('IT Project Manager/Team Leader', @equivalentId)

SET @equivalentId = 'd397a0da-03de-42d0-a24e-3c9511bb6e6d'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('IT Security Specialist', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('I.T. Security Specialist', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('IT Security Analyst', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('I.T. Security Analyst', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Security Analyst', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Security Specialist', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Security Consultant', @equivalentId)

SET @equivalentId = 'c122c339-6148-4bd1-80db-f6c9244a9dc5'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Fraud Detection', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('IT Fraud detection', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('I.T. Fraud detection', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('online fraud', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('online fraud detection', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('online fraud specialist', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('online fraud analyst', @equivalentId)

SET @equivalentId = '5b27b7ae-237e-4bac-893e-e90dd9389824'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('IT consultant', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('I.T. Consultant', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Systems consultant', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Sole-trader IT consultant', @equivalentId)

SET @equivalentId = '3bb35750-54ca-48bf-b72c-6d9e71689603'

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

SET @equivalentId = 'db9852f2-3b42-4c6a-8c9e-b769b83b4236'

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

SET @equivalentId = '771299af-aec9-4d17-8c04-b266072dd944'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('idn', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('identity driven networking', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('identity driven network', @equivalentId)

SET @equivalentId = '491856d3-b6b6-4525-b8d4-94278bc48e56'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Infrastructure Consultant', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Infrastructure Solutions Consultant', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('IT Infrastructure Consultant', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('IS Management Consultant', @equivalentId)

SET @equivalentId = '4213ae88-d4db-4994-ae92-38f568e391c6'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Senior Infrastructure Consultant', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Sr Infrastructure Consultant', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Program Director Infrastructure', @equivalentId)

SET @equivalentId = 'd74a4581-a050-41bb-a13c-eaf15d754425'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('IT Contractor', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('IT Contracts', @equivalentId)

SET @equivalentId = 'f0b00609-c534-400d-ad2a-df3cc8311b9d'

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

SET @equivalentId = '7182f22f-2552-4d50-82e1-29115620ba45'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Intranet Developer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Intranet Application Developer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Intranet Software Developer', @equivalentId)

SET @equivalentId = 'a227362b-5551-4596-ac48-1b897a478424'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Website Developer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Web Application Developer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Web Designer/Developer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Web Site Developer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Freelance Web Developer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Senior Web Developer', @equivalentId)

SET @equivalentId = 'e7de60bc-368a-410e-a48c-9ff4560fd1ee'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('IT Support Engineer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Computer Engineer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Desktop Support Engineer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('IT Support Technician', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Computer Technician', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('IT Technician', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('i.t. support engineer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('i.t. technician', @equivalentId)

SET @equivalentId = '39672248-6f69-4732-bbdf-6c3363d48fa6'

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

SET @equivalentId = '4f18636c-0900-40c3-8bdc-ea6fccec6baa'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('cms', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('content management system', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('CMS Operator', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('CMS Specialist', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Web CMS', @equivalentId)

SET @equivalentId = '6202af64-67d4-4052-8bc8-9a4fa5c7dc83'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Customer Management Support Representative CMS', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Customer Management Support Representative', @equivalentId)

SET @equivalentId = '188857f5-fa09-47cf-8976-6e407204d98e'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('ColdFusion', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('cold fusion', @equivalentId)

SET @equivalentId = '63fac661-4a65-4507-9d4a-992a4ab364ac'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Web Administrator', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Web Content Administrator', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('website administrator', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('website editor', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('website maintainer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('website support', @equivalentId)

SET @equivalentId = 'adf775c1-2aa3-44a9-81e9-8545468d4a90'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('IT & T Business Development Manager', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('IT&T', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('IT and Telecoms', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('IT and Telecommunications', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('IT & Telecoms', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('IT & Telecommunications', @equivalentId)

SET @equivalentId = '52aab95b-a3fb-4516-8104-11edfcd9d3a9'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Exchange Administrator', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Exchange Engineer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Exchange Specialist', @equivalentId)

SET @equivalentId = 'c3e5bff0-3f81-440b-babd-2d3468818c0e'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Business intelligence', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('BI', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Bus Intelligence', @equivalentId)

SET @equivalentId = '46c216a0-3851-46ba-a9e0-d1d120e18234'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('SEO Search Specialist', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('SEO Specialist', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Search engine optimisation specialist', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('seo consultant', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('seo contractor', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('seo professional', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('search consultant', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('search contractor', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('search professional', @equivalentId)

SET @equivalentId = '04b7f83e-4532-4c28-a0d8-8167dd72e347'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('SEM specialist', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Search engine marketing specialist', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('SEM consultant', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('SEM contractor', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('SEM professional', @equivalentId)

SET @equivalentId = '72820b4d-2a1c-49e2-97ff-8da975da2b8b'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Agile trainer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Agile coach', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Agile leader', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Agile coach / practitioner', @equivalentId)

SET @equivalentId = 'df5ed9cc-57d3-41ec-9aa1-141205012c47'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Integration tester', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('integration test specilist', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('integration testing', @equivalentId)

SET @equivalentId = 'ea42f7e9-dac3-42cf-a8aa-f097928ef5fe'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('engineer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('eng', @equivalentId)

SET @equivalentId = '051b8298-751f-4560-be0f-0ba46b9076f8'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Engineering Manager', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Senior Engineer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Chief Engineer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('director of engineering', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('head of engineering', @equivalentId)

SET @equivalentId = '2ba1a9f4-16b1-489a-8ab6-0ca2524389d8'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('infrastructure engineer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('engineer - infrastructure', @equivalentId)

SET @equivalentId = 'a416e38a-385b-4245-991e-3b70f52639bc'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('aircraft maintenance engineer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('aviation maintenance engineer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('aviation engineer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('aircraft maintenance mechanic', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('aviation maintenance mechanic', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Aircraft Technician/Mechanic', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Aircraft Technician', @equivalentId)

SET @equivalentId = 'bbfbd8cd-309d-4ba0-8f1d-104ed285ab03'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('site manager', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('site superintendant', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('superintendant', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('site foreman', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('site mgr', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('sub foreman', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('subforeman', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('construction manager', @equivalentId)

SET @equivalentId = '4e42ecd3-ca20-4d87-a7fe-afb71b3fd5ed'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('System Engineering Management Consultant', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('system engineering manager', @equivalentId)

SET @equivalentId = '806143ae-cbce-4200-b6ba-1b4f78993205'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('engineering project manager', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Senior Project Engineer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Project Engineer', @equivalentId)

SET @equivalentId = '1200649c-ee28-4e86-86c5-085c89f08371'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Site Engineer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Site Supervisor', @equivalentId)

SET @equivalentId = 'ea2c9e37-1d55-43a0-9546-dba9f7e5f490'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Assistant Engineer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Engineering Assistant', @equivalentId)

SET @equivalentId = 'b44aea9f-adfb-4c39-ba86-a6193f9bf2ec'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Junior Engineer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Student Engineer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('graduate engineer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Undergraduate Engineer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('DESG Student Engineer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('trainee engineer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('engineering trainee', @equivalentId)

SET @equivalentId = 'b6f2cd5a-2ed8-4f35-a48b-afbfae7995ce'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Field Engineer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('FIELD SERVICE ENGINEER', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('service engineer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Customer Engineer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('customer service engineer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Field Eng', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Field Engineering', @equivalentId)

SET @equivalentId = '3f001168-4968-42f3-8cf8-e14d70816743'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Engineering Consultant', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Consultant engineer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Consulting Engineer', @equivalentId)

SET @equivalentId = 'ef93eff5-f0f8-4a8a-adde-c37beee9384b'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Customer Support Engineer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Support Engineer', @equivalentId)

SET @equivalentId = '5c2c14ea-3c97-4abe-ad11-5f19b5896933'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Aircraft Engineer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Aircraft Mechanic', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('aerospace fitter', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('aircraft fitter', @equivalentId)

SET @equivalentId = '1080dd14-3474-4692-9268-07c810b2b4f0'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('aeronautical engineer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('aerospace engineer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Design Engineer Aerospace', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('aeronautical design engineer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('aerospace design engineer', @equivalentId)

SET @equivalentId = '86074698-6a28-4355-b64e-6aafc4e85dbd'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Design Engineer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('engineering designer', @equivalentId)

SET @equivalentId = 'bcaa06a1-2a01-4665-b566-00fff7132bc3'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('communications engineer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('communications and electronic engineer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('comms engineer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Communications Eng', @equivalentId)

SET @equivalentId = 'b4361939-2428-40d2-86e1-0e4f9055b5f5'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('industrial engineer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('product engineer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Production Engineer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Manufacturing Engineer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Industrial Eng', @equivalentId)

SET @equivalentId = 'd89ebdd4-06c4-4d6b-b9b9-50e6a0f64216'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('electrical engineer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Electrical Design Engineer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Electrical and Instrument Engineer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Electrical Project Engineer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Electrical Services Design Engineer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('electrical engineering', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Electrical/Electronics Engineer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Electrical Eng', @equivalentId)

SET @equivalentId = '41b9ce58-d583-40e5-9c4f-5b93c8748a89'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Mechanical Design Engineer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Mechanical Designer', @equivalentId)

SET @equivalentId = 'e8710802-d359-4248-badb-f8e1a028fd7c'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Planning Engineer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('project planning engineer', @equivalentId)

SET @equivalentId = '19f22c77-3947-4b89-a9d8-48fd0bec7fc8'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Electronics Engineer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Electronic Engineer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Electronics Eng', @equivalentId)

SET @equivalentId = '74edbeb4-84cb-4cfa-8c55-97b6fc8f9de9'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Estimator', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('quantity surveyor', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('qs', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Estimater', @equivalentId)

SET @equivalentId = '47925bfe-d1fa-4f1a-93b0-5a23ca8120ec'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Automation Engineer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('robotic engineer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('automation design engineer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('control engineer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('electrical control engineer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('control systems engineer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('control system engineer', @equivalentId)

SET @equivalentId = '88bb3517-d6ec-4ac8-9388-4c17da2361a3'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('robot', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('robotic', @equivalentId)

SET @equivalentId = '0f50681f-91b0-42d4-a3ad-117c17888b82'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Sound Engineer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Audio Engineer', @equivalentId)

SET @equivalentId = '9fcd66ab-bbc5-4096-968c-1e4f83405327'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Quality Assurance Engineer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('qa engineer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('quality engineer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('reliability engineer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Quality Assurance Eng', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('QA Eng', @equivalentId)

SET @equivalentId = 'f68e6aaf-2ad8-4120-a54f-0e9ff3aa09e2'

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

SET @equivalentId = '364e2eb9-a039-4ebd-b35d-a7f44328a70b'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('hydraulic engineer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('fluid engineer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('water engineer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('fluid power engineer', @equivalentId)

SET @equivalentId = '260ba435-f91c-46f5-baec-27e0488ba5e4'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('hydraulic fitter', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('hydraulic mechanic', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('hydraulic fittersales', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('hydraulic technician', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('hydraulic valve technican', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('hydraulic hose fitter', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('hose technican', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('industrial hydraulic technican', @equivalentId)

SET @equivalentId = 'aa2a75a8-fa14-44d7-b1ca-b746961e9cbb'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('mechanical engineer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('mech eng', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Maintenance Engineer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Plant Engineer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Mechanical Eng', @equivalentId)

SET @equivalentId = 'b748734a-88a8-4e52-a14f-7f32dfe4fdbc'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('chemical engineer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Chemical & Process Engineer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Chemical and Process Engineer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('chemical process engineer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Process Engineer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Petrochemical Engineer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Chemical Eng', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Process Eng', @equivalentId)

SET @equivalentId = 'b081762a-1e1e-4634-81d4-dbad48bea26a'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('petro chemical', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('petrochemical', @equivalentId)

SET @equivalentId = '2f801ac3-dd77-422a-b1a7-59817697ef9d'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Mining Manager', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Quarry Manager', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Quarry Operations', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('project manager mining', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('mining project manager', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('mine manager', @equivalentId)

SET @equivalentId = 'e760eb2c-7323-4885-b786-4a980ae5fe83'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('mining engineer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Mine Geologist', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Mine Specialist', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('mines engineer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Mining Consultant', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Mine Planning engineer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('mine diesign engineer', @equivalentId)

SET @equivalentId = '52dd8053-c46d-4e31-8e60-08134d73dcd0'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('hydrogeologist', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('hydro geologist', @equivalentId)

SET @equivalentId = 'ac200ad4-538d-48be-8a7d-2df896116279'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('drill fitter', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('drill technician', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('drill rig operator', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('rig operator', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('rig maintenance', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('rig fitter', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('drillrig operator', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('drillrig worker', @equivalentId)

SET @equivalentId = '060c0ad0-9165-4c1a-81b2-7a685f89fd12'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('mine maintenance operator', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('mine site maintenance', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('minesite maintenance', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('mining maintenance', @equivalentId)

SET @equivalentId = '8d18b652-fdf8-45b1-a3af-48650d227594'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('underground', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('under ground', @equivalentId)

SET @equivalentId = '20190b47-4f4e-4d24-828d-16f397c6b806'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('aboveground', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('above ground', @equivalentId)

SET @equivalentId = '7cb8fa57-739d-45ce-92f5-875c43ebd34b'

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

SET @equivalentId = 'd36f0204-361a-4845-833f-ff7cc2536696'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Trainee Civil Engineer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Junior Civil Engineer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Graduate Civil Engineer', @equivalentId)

SET @equivalentId = 'b2044c36-374c-45af-90a8-c48191f7b8b3'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('hydrology', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('hydrologist', @equivalentId)

SET @equivalentId = '26f5980f-281e-414d-ad57-e9e40d0be2f3'

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

SET @equivalentId = '428464aa-1e86-4296-a355-319654bb910b'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('planning clerk', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('planning assistant', @equivalentId)

SET @equivalentId = '6b51471d-b100-4e52-9335-6ea4c2b70f05'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('building inspector', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('asset inspector', @equivalentId)

SET @equivalentId = '0326ca0e-950c-4c0b-bafc-0718c21e76af'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Aeronautics Engineer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Aeronautics Eng', @equivalentId)

SET @equivalentId = 'be6bf3a6-5b82-48b4-bc90-7f74f37315da'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('energy and resources engineer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('ER engineer', @equivalentId)

SET @equivalentId = 'b6e03946-00fe-49fc-a30b-6b043357e351'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Cost Estimator', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Cost Planner', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Cost / Schedule Analyst', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Cost and Schedule Analyst', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Cost & Schedule Analyst', @equivalentId)

SET @equivalentId = '56b04889-db68-49c8-b636-262e7e856deb'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('signalling', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('signalling engineer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('signalling design', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('signal engineer', @equivalentId)

SET @equivalentId = '63880ea4-9314-43fb-872c-a67cf8ffe943'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('overhead wiring', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('overhead wiring engineer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('ohw', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('ole', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('ohl design engineer', @equivalentId)

SET @equivalentId = '756e25dc-3862-4d8d-a76e-397303e50101'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('permanent way design engineer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('track design engineer', @equivalentId)

SET @equivalentId = 'e6a2fd1b-b30c-4994-9f24-f08ae0cb5a5e'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('field service technician', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('service technician', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('field technician', @equivalentId)

SET @equivalentId = 'c0b0ec28-b85c-41db-a66c-1e215da94f56'

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

SET @equivalentId = '530a74f8-c68a-4cd7-8c1a-5ca9fa1a4201'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Executive Chairman', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Exec Chairman', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('E.C.', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('EC', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('exec chairperson', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('executive chairperson', @equivalentId)

SET @equivalentId = '8ec7f2ae-a2bd-45d2-87a0-9ddda506296c'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('General Manager', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('GM', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('G.M.', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Group General Manager', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('GGM', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('G.G.M.', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Gen Mgr', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Gen. Mgr.', @equivalentId)

SET @equivalentId = '455e8335-9de1-4f49-b0c1-30654e2bf54e'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Chief Operating Officer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('COO', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('C.O.O.', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('chief op. officer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('chief op officer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('cheif operating officer', @equivalentId)

SET @equivalentId = 'f365bcee-62f2-4fdd-baae-0c6b03b336e8'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('vp', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('v.p.', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('vice president', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('vice pres.', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('vice pres', @equivalentId)

SET @equivalentId = 'dbd7c53f-c9af-4aaa-a356-de09fbe78e0f'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('senior vp', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('senior v.p.', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Senior Vice President', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('snr vice president', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('snr vice pres.', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('snr vice pres', @equivalentId)

SET @equivalentId = '943bd737-03b7-425a-a023-0bc0d9b48ae2'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('2ic', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('2.i.c.', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('2i.c.', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Second in charge', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('2 IC', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('2nd i.c.', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('2nd in charge', @equivalentId)

SET @equivalentId = '9836271d-fc00-42a9-b814-78ffd7f96a05'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('director', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Dir', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Dir.', @equivalentId)

SET @equivalentId = '919fcb03-8f52-4ae3-95a3-0d8238329295'

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

SET @equivalentId = 'bdcc1a31-a11d-492a-8788-23633da34a03'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('company secretary', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('company sec.', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('co sec', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('co.sec.', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('company sec', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('corporate secretary', @equivalentId)

SET @equivalentId = 'ff8640cf-1634-4e6d-8932-f59cef096acd'

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

SET @equivalentId = 'd15f0f6c-f2a7-4ba8-9649-d5ae16846ead'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('CIO', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('C.I.O.', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Chief Information Officer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('general manager it', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('it director', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('director it', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('director information technology', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('head of it', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('director of i.t.', @equivalentId)

SET @equivalentId = 'f78cc99d-1edb-4abf-bc99-12b21c05e79a'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('manager', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('mgr', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('manger', @equivalentId)

SET @equivalentId = 'ccb577a7-8f7d-4d38-9aa9-7e14debabf1e'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('State Manager', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('statemanager', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('state mgr', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('regional manager', @equivalentId)

SET @equivalentId = '823a3fbb-5894-4f0a-ae6c-eb5ffed2a5c7'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Manager Governance', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Manager Accountability', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('compliance manager', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('mgr of compliance', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('compliance mgr', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('manager of compliance', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('project governance', @equivalentId)

SET @equivalentId = '7456eb07-8868-47d7-b416-046e7d7d8c8f'

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

SET @equivalentId = '54ba1579-0880-4e8d-8add-ea8a2d0e22a3'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Assistant Manager', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('asst mgr', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('asst manager', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Assistant Manger', @equivalentId)

SET @equivalentId = '7870b4d3-0ce6-4ce6-8e34-e0df1664c786'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Senior Supervisor', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Snr Surpervisor', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('senior sup.', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('snr s''visor', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('senior s''visor', @equivalentId)

SET @equivalentId = '0ad00ef4-2f62-4bd1-af90-8f41c80fe566'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Assistant Supervisor', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Asst Supervisor', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('assist sup', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('assistant s''visor', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('assist. Sup.', @equivalentId)

SET @equivalentId = '73971526-1e59-4583-ab14-c1916fb6980e'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('foreman', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('fore man', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('foreperson', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('fore person', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('overseer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('over seer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Foreman / Supervisor', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('General Foreman', @equivalentId)

SET @equivalentId = '517ebbf5-0e61-4817-b9c2-a432960865f6'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Team Leader', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Team lead', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Teamleader', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('supervisor', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('team manager', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('teamlead', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('team mgr', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('field leader', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('team supervisor', @equivalentId)

SET @equivalentId = '8b7fe8fc-5c98-43df-8e58-d8b8e94c3ec4'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Project Manager', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Project Mgr', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('PM', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Project Manger', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('proj mgr', @equivalentId)

SET @equivalentId = '5e1f8980-ed65-471e-aad1-76e9f1194272'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('General Manager Project', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Project Director', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('project coordinator', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Senior Project Manager', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Senior Project director', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('project co-ordinator', @equivalentId)

SET @equivalentId = '19898fae-404e-4906-90ed-de559a3682b0'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('BPM', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('B.P.M.', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('business project manager', @equivalentId)

SET @equivalentId = 'fe89ff24-3b33-48f4-9198-1557a98179ee'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Assistant Project Manager', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Asst Project Manager', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Asst Project Mgr', @equivalentId)

SET @equivalentId = '7d436b14-89ec-466c-b757-da0953216bf2'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Junior Project Manager', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Trainee Project Manager', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('junior pm', @equivalentId)

SET @equivalentId = 'f95df967-3001-48b8-b475-f18643bd95e2'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('inhouse consultant', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('in-house consultant', @equivalentId)

SET @equivalentId = '0ff80574-327a-4b09-8ea5-6806ac4569ba'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('BPR consultant', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('business process re-engineering', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('bpr', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('business process reengineering', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('process re-engineering', @equivalentId)

SET @equivalentId = 'e417b0e1-187e-4178-aec2-c1511c833cac'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('BPO', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('business process outsourcing', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('b p o', @equivalentId)

SET @equivalentId = '459c2a6e-addf-48b7-909f-af736ac960f7'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('development manager', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('dev mgr', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('development mgr', @equivalentId)

SET @equivalentId = 'fd531d08-2fc6-4de6-b2ad-48180eb4356a'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('DM', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('D.M.', @equivalentId)

SET @equivalentId = '0a12d430-125d-4f76-bb81-408b59acdbbb'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('transition manager', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('transitions manager', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Transition Project Manager', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Transformation Project Manager', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Transformation Manager', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Manager Transition', @equivalentId)

SET @equivalentId = '1f71c3c1-6fb0-4f8a-9e61-d363a1d9b00f'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Manager Planning', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Manager- Planning', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('planning manager', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('planning mgr', @equivalentId)

SET @equivalentId = 'cf000d15-d005-4ab0-b84f-036ee98edf31'

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

SET @equivalentId = 'ab588f80-6ad9-4a9e-91b6-7beac7966626'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Assistant C.F.O.', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Assistant CFO', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Asst C.F.O.', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Asst CFO', @equivalentId)

SET @equivalentId = '6dc65acc-0bdc-4577-8a6a-e6bc949f9c20'

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

SET @equivalentId = '92aee222-71ed-4cd4-a6c6-32d4c50c1fd1'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Senior Financial Controller', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Sr. Financial Controller', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Sr Fin Controller', @equivalentId)

SET @equivalentId = '0a907cd6-0a37-4e9f-8574-651de32df796'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Assistant Financial Controller', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Asst Financial Controller', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Asst Fin Controller', @equivalentId)

SET @equivalentId = 'b985647e-d23b-4fa8-bbf3-62b5685dfaf1'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Regional Financial Controller', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Regional Finance Controller', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('regional fc', @equivalentId)

SET @equivalentId = 'c772d513-fe6d-4429-bd5f-5590ef8b624e'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('National Financial Controller', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('National Finance Controller', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('national fc', @equivalentId)

SET @equivalentId = '3d027f44-73ac-4268-a7b2-9224018cfde1'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Accounting Manager', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Accounts Mgr', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('accnts mgr', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('accts manager', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('accts mgr', @equivalentId)

SET @equivalentId = 'bfe12a39-0723-4303-bb30-1f078087cb1e'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Senior Finance Accountant', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Sr Finance Accountant', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Sr. Finance Accountant', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('snr finance accountant', @equivalentId)

SET @equivalentId = 'ed2f7706-4765-4156-8d2a-6f39b6e1a5a8'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Senior Financial Accountant', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Sr Financial Accountant', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Sr. Financial Accountant', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('snr financial accountant', @equivalentId)

SET @equivalentId = '1f2d6217-c45f-4cf8-809c-776ad07e8858'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Administration Manager', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Admin Manager', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('admin mgr', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('administration mgr', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('National Administration Manager', @equivalentId)

SET @equivalentId = '35d2d705-2961-404b-bfc7-3340c059df56'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Senior Consultant', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Sr Consultant', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Sr. Consultant', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('snr consultant', @equivalentId)

SET @equivalentId = '4f3bf72f-7824-4573-97c4-95ccd0870bb1'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Principal Consultant', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Chief Consultant', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Managing Consultant', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('head consultant', @equivalentId)

SET @equivalentId = 'dfca6daa-4d6d-4e69-b303-5fa3aaf5ea48'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Accountant', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Acc', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Accountan', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('acct', @equivalentId)

SET @equivalentId = 'c51adc4b-9466-4ebd-b5fb-71fce6843081'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Financial accountant', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Fin Acc', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Finance Accountant', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Finance Acc', @equivalentId)

SET @equivalentId = '1e69bec9-1219-4adc-9fc7-6a85f97b71d6'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Corporate Accountant', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Corp Accountant', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Sr. Mg Accountant', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Contract Accountant', @equivalentId)

SET @equivalentId = '42bac970-bf1d-4d51-aa29-79b498ff2b8d'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Business Accountant', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Bus Acct', @equivalentId)

SET @equivalentId = '3510a1fc-ac19-41c2-bbe6-c6a580be1ac0'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Management accountant', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Management Acct', @equivalentId)

SET @equivalentId = '61ba5bc1-6a7b-431a-b68f-0d757448b1a5'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Senior Management Accountant', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Sr Management Accountant', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('snr management accountant', @equivalentId)

SET @equivalentId = 'aa6e4dd0-2d05-4e10-94e3-3464a2932db0'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Accounting', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Accountin', @equivalentId)

SET @equivalentId = '272363b3-1cb1-4702-a907-b4b21056e8ab'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('cpa', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('c.p.a.', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('certified practising accountant', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('certified accountant', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Australian Society of Certified Practising Accountants', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('CPA online', @equivalentId)

SET @equivalentId = 'e2166e28-c24c-4af2-b35c-bbd312f87931'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('ca', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('c.a.', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('chartered accountant', @equivalentId)

SET @equivalentId = '85ce803e-face-4043-87d2-27489f9b60c2'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Temporary Accountant', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Temp Accountant', @equivalentId)

SET @equivalentId = '92f52362-eeb5-4c1d-8d81-4359512cfa57'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Sr. Accountant', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Sr Accountant', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Senior Accountant', @equivalentId)

SET @equivalentId = '2e556059-2c6d-4e47-8c12-9b45fa62e5f4'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Financial/Management Accountant', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Assistant Management Accountant', @equivalentId)

SET @equivalentId = 'bd6e4eca-0db0-4f93-b434-1acbfcb4672e'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Junior Accountant', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Jr Accountant', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Jr. Accounts', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Junior Accounts Clerk', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('junior clerk', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('cadet accountant', @equivalentId)

SET @equivalentId = '111bc90b-925f-4283-95a6-d3ef62b4087e'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Assistant Accountant', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Asst Accountant', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Accounts Assistant', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Acct Asst', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Accounting Assistant', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Account Assistant', @equivalentId)

SET @equivalentId = '20da7562-bb76-407e-9f48-52b63509c8a7'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Accounts Clerk', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Accounts Officer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Accounts Staff', @equivalentId)

SET @equivalentId = 'f4dd0f5d-1043-437d-a2ba-f06285e8790e'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Project Accountant', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Proj Acct', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Project Management Accountant', @equivalentId)

SET @equivalentId = 'f7ea3f92-43fb-4ee5-b1ff-03d7d27c86e3'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Auditor', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('auditing', @equivalentId)

SET @equivalentId = 'e434d5c2-7915-4143-bde3-4eee05d2f48d'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Audit Manager', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Audit Mgr', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('head of audit', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('audit director', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('audit partner', @equivalentId)

SET @equivalentId = 'ff6a2672-33dc-4306-bf60-dba8e45d0b6a'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Senior Auditor', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Sr Auditor', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Sr. Auditor', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('snr auditor', @equivalentId)

SET @equivalentId = 'ecb3e83a-65de-4819-a9fd-6db60be9c4aa'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Internal Auditor', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('In house Auditor', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('In-house Auditor', @equivalentId)

SET @equivalentId = '1d1b92e3-1ab6-4ba9-95ff-097bec600459'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('External auditor', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Contract Auditor', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('financial auditor', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Revenue Auditor', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('State Auditor', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Statutory Accountant', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Senior Internal Auditor', @equivalentId)

SET @equivalentId = 'de72cf10-cc41-435f-8ce7-86602cbda166'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Junior Auditor', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Jr Auditor', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Jr. Auditor', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Apprentice Auditor', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('cadet auditor', @equivalentId)

SET @equivalentId = '0fa7c793-bcf4-453f-a40d-1bf0c3b3319e'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Quality Auditor', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('QA Auditor', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Quality Assurance Auditor', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('quality assessor', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('quality assurance officer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('quality assurance inspector', @equivalentId)

SET @equivalentId = 'd1768f1c-d959-4688-ab99-16e5e8e710ed'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('qas', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('quality assurance system', @equivalentId)

SET @equivalentId = 'e4ed189f-9b09-4997-a1e2-99fd99d333b5'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Operations Auditor', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Process Auditor', @equivalentId)

SET @equivalentId = 'd941230b-3d72-4a67-b5a2-cc737f89d2be'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Stock Auditor', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Night Auditor', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('stock counter', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('stocktake auditor', @equivalentId)

SET @equivalentId = '73c469b0-10fd-41c2-a45e-6ee31ec205d3'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('stock controller', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('inventory controller', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('stockcontroller', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('stock supervisor', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('stock control', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('stock manager', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('inventory manager', @equivalentId)

SET @equivalentId = 'e1cdd7f5-10cc-4c03-8dae-5b4d0851d3dd'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('stock', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('inventory', @equivalentId)

SET @equivalentId = 'e18639e0-556d-48b8-b59a-ad8871c6f010'

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

SET @equivalentId = 'deac8fa9-3a55-4e49-abc9-d36c3c9467d4'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Accounts Payable Manager', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Accounts Payable Supervisor', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('head of accounts payable', @equivalentId)

SET @equivalentId = '79228dfd-b830-433b-ac42-b7f2c2b79efc'

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

SET @equivalentId = '3857cef3-2783-4f42-9943-23afeb0802f7'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Accounts Payable Assistant', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Assistant Accounts Payable', @equivalentId)

SET @equivalentId = '29c29a05-b4f1-48e1-9fcb-909a7c7c978c'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Accounts Payable/Receivable', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Accounts payable specialist', @equivalentId)

SET @equivalentId = 'ee1de79b-9c57-4ad9-85d3-32729b8d150b'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Reconciliation Clerk', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Reconciliation Officer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Reconciliation Assistant', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Reconciliation Asst', @equivalentId)

SET @equivalentId = '2a17ee0d-ee85-4fc4-9680-639200ffb2f2'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Accounts Receivable Manager', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Accounts Receivable Supervisor', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Collections Supervisor', @equivalentId)

SET @equivalentId = '6b4929b1-dc6d-4401-8831-961ca23adfa9'

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

SET @equivalentId = '5b0d8771-209b-4c46-8cbb-5b9dcf07c9fa'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Mgr Credit Control', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Credit Control Manager', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Credit Control Mgr', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Manager Credit Control', @equivalentId)

SET @equivalentId = '3997364a-dccc-412b-9bac-5dbec7e9ec9b'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Finance Analyst', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Financial Analyst', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('financial management analyst', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('finance and investment analyst', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('investment analyst', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('analyst finance', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Finance Business Analyst', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Financial Business Analyst', @equivalentId)

SET @equivalentId = 'c86f1041-14f5-468b-ab25-8b84c4a3b09f'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Credit Analyst', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Credit Risk Analyst', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('credit management', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Credit cost Analyst', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('credit portfolio analyst', @equivalentId)

SET @equivalentId = '84934997-c792-4ed7-af0e-c0908286f0bd'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Senior Finance Analyst', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Sr Finance Analyst', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Sr. Finance Analyst', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Senior Financial Analyst', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Sr Financial Analyst', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Sr. Financial Analyst', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('snr financial analyst', @equivalentId)

SET @equivalentId = '87be4506-23bb-42b6-b6c6-da3adea99c1a'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Senior Credit Analyst', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Sr Credit Analyst', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Sr. Credit Analyst', @equivalentId)

SET @equivalentId = '4b549c66-e245-4446-ba15-21d82773cf97'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Junior Finance Analyst', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Jr Finance Analyst', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Jr. Finance Analyst', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Junior Financial Analyst', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Jr Financial Analyst', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Jr. Financial Analyst', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('jnr financial analyst', @equivalentId)

SET @equivalentId = '8e99787b-9ad3-439c-be4a-6e00e6f0b110'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Junior Credit Analyst', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Jr Credit Analyst', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Jr. Credit Analyst', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('jnr credit analyst', @equivalentId)

SET @equivalentId = '8dc504fd-175e-4d85-b366-817f96ed408d'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Taxation Manager', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Taxation Mgr', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Tax Manager', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Tax Mgr', @equivalentId)

SET @equivalentId = '1ab6ddd4-6879-4047-bc3a-d56094f5c212'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('taxatiojn accountant', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Taxation Accountant', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('tax associate', @equivalentId)

SET @equivalentId = 'e689dc83-9541-4c28-9904-41d247a949b3'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('tax accountant', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('tax analyst', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('tax partner', @equivalentId)

SET @equivalentId = 'd798e141-0d16-4a0b-a853-ba81fe04debe'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('partner', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('equity partner', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('staff partner', @equivalentId)

SET @equivalentId = 'f4205605-6811-4d79-8ba7-b0d31836472b'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Junior Tax Accountant', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Jr. Tax Accountant', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Jr Tax Accountant', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Junior Taxation Accountant', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Jr. Taxation Accountant', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Jr Taxation Accountant', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('jnr tax accountant', @equivalentId)

SET @equivalentId = '4ba3bad9-a09e-44ab-947b-fe719abfdbf2'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Taxation Advisor', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Tax Advisor', @equivalentId)

SET @equivalentId = '21ecee80-331d-4216-a251-422c2a49877c'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Taxation analyst', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Tax Return Preparer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Tax Agent', @equivalentId)

SET @equivalentId = '2992cb41-dfda-4490-ae34-cd89dae00918'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Senior Tax Accountant', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Sr. Tax Accountant', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Sr Tax Accountant', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Senior Taxation Accountant', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Sr. Taxation Accountant', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Sr Taxation Accountant', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('snr tax accountant', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('snr taxation accountant', @equivalentId)

SET @equivalentId = 'bd49bfc6-bec6-4d4e-a2aa-5ecd472c982b'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Motor Claims', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Motor Claims Consultant', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('motor claims clerk', @equivalentId)

SET @equivalentId = '68d13df6-d816-49b2-bae8-8a5a2ad7b8eb'

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

SET @equivalentId = '55e3519e-bdf5-4763-8eaa-94bd28471bf3'

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

SET @equivalentId = '16ed7374-a66e-4d19-a1e1-ff425c30936d'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Cost Accountant', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Cost Acct', @equivalentId)

SET @equivalentId = '388b6d22-dee0-4cdd-998e-e7fd409d44b3'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Senior Purchasing Specialist', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Sr. Purchasing Specialist', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Sr Purchasing Specialist', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('snr purchasing specialist', @equivalentId)

SET @equivalentId = '0e0239f8-8804-43aa-8f33-3d8eef813e23'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Purchasing Manager', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Purchasing Mgr', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('procurement manager', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('head of procurement', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('procurement director', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('buying manager', @equivalentId)

SET @equivalentId = '81118163-73de-4448-9d68-0f923846e8de'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('procurement', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('purchasing', @equivalentId)

SET @equivalentId = '0834e53b-45a4-4cbb-b5a4-f6aa1c63ce46'

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

SET @equivalentId = '2906f865-08d0-4e9c-8adb-fa65081c1c7f'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Buyer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Senior  Buyer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Sr Buyer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Sr. Buyer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Regional Buyer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('National Buyer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('State Buyer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('snr buyer', @equivalentId)

SET @equivalentId = '74c25312-e581-4cab-916e-01dd3de66312'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Buying Assistant', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Assistant Buyer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Junior Buyer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Jr Buyer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Jr. Buyer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Trainee Buyer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('jnr buyer', @equivalentId)

SET @equivalentId = 'd72f3e5c-4175-4db2-b265-91a02f177760'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Mortgage Collections', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Mortgage Collection', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Mortgage Collections Officer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Mortgage Collections Clerk', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Mortgage Collection Clerk', @equivalentId)

SET @equivalentId = 'b1c20128-6968-42b7-aa76-df5e427de060'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('online banking', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('electronic banking', @equivalentId)

SET @equivalentId = '3d95d966-6b2d-4e47-b055-4f62dd9ceae9'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Banking Manager', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Bank Manager', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('bank branch manager', @equivalentId)

SET @equivalentId = '06e152d5-c129-4955-8b23-07161747d586'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Branch Manager', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('branch head', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('branchmanager', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('branch supervisor', @equivalentId)

SET @equivalentId = 'f22a0b14-c445-40c8-bff8-fe68840d9b08'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Business Banking Manager', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Manager Business Banking', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('banking services manager', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('business banker', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('banker', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Retail Banking', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Banking Specialist', @equivalentId)

SET @equivalentId = 'af440cd3-8d57-4a13-81b0-5c950aeb9da7'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Banking Clerk', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Banking Consultant', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Banking Officer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('bank clerk', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('teller', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('bank teller', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Banking Representative', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Personal Banking Assistant', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('bankteller', @equivalentId)

SET @equivalentId = '6508d5d0-e955-4e6f-b955-8bdcaa63fde8'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Advisor', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Adviser', @equivalentId)

SET @equivalentId = 'f46a5403-4b6b-4eee-871d-797f57812ea3'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Insurance Manager', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Insurance Mgr', @equivalentId)

SET @equivalentId = '1c0678d7-52f4-4178-a2a0-51dddbbeab2f'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Insurance Consultant', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Insurance Assistant', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Insurance Asst', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Insurance Clerk', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Insurance Administrator', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('insurance officer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Insurance Claim Assessor', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('insurance technician', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Insurance assessor', @equivalentId)

SET @equivalentId = 'cfef6eb9-0321-4d35-904c-7986606efd3e'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Jr. Insurance Administrator', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Jr Insurance Administrator', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Junior Insurance Administrator', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('jnr insurance administrator', @equivalentId)

SET @equivalentId = '84193855-3ed6-4aae-9db2-44a8bfedaf4b'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Insurance Broker', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('General Insurance Broker', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Insurance Solutions Consultant', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('re-insurance broker', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('reinsurance broker', @equivalentId)

SET @equivalentId = '831b8049-c2ce-4f22-9671-636a57f77a9e'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Junior Insurance Broker', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Jr. Insurance Broker', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Jr Insurance Broker', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('jnr insurance broker', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('trainee insurance broker', @equivalentId)

SET @equivalentId = '42eb988a-9084-4bea-a92e-0e8cf9af86da'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Chief Actuary', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('actuarial manager', @equivalentId)

SET @equivalentId = 'b425cd85-5eed-447c-84a5-57d4400b23da'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Underwriting Manager', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('National Underwriting Manager', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Underwriting', @equivalentId)

SET @equivalentId = '3abe3fea-dfc8-44fe-a745-f35244d53f47'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Underwriting Executive', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Insurance Underwriter', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Assistant Underwriter', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('under writer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('re-insurance underwriter', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('reinsurance underwriter', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Underwriter', @equivalentId)

SET @equivalentId = '47e2f138-9a59-455a-a911-e049c3b3f00a'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Chief Financial Planner', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('head of financial planning', @equivalentId)

SET @equivalentId = '4ed1c65f-f771-4e64-9b88-f4d10d4ae628'

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

SET @equivalentId = '5590b746-8a86-4414-a2eb-693d19d7933d'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Assistant Financial Planner', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Financial Planner Assistant', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Financial Planning Assistant', @equivalentId)

SET @equivalentId = '4dfedac5-0b15-461e-bd4f-ec0e92680d70'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Senior financial planner', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Sr. financial planner', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Sr financial planner', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('snr financial planner', @equivalentId)

SET @equivalentId = 'a921d406-0f07-44d6-a13e-2de489b26c92'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Paraplanner', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Para planner', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Para-planner', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Para Planning', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Para-Planning', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Para-Financial Planner', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Para Financial Planner', @equivalentId)

SET @equivalentId = 'f6e960cd-a4da-4736-be78-44b53686cdf9'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Superannuation', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Super annuation', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Super-annuation', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('super fund', @equivalentId)

SET @equivalentId = 'adf791ba-c791-43ab-94a8-463d77dfe7e7'

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

SET @equivalentId = 'b4d94d54-3c5b-4892-a10d-a27778d13ad5'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Stockbroker', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Stock broker', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Stock broking', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Stockbroking', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Stockbrokers', @equivalentId)

SET @equivalentId = 'b3fc3be3-0602-475d-b7e3-d461ef512256'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('cashflow', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('cash flow', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('cash-flow', @equivalentId)

SET @equivalentId = '2383c27f-03ef-4115-9f0c-64dd70fef47e'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('CBA', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Commonwealth Bank of Australia', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('comm bank', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('commbank', @equivalentId)

SET @equivalentId = '99feb522-57f0-44c4-8056-a99ddda95a7f'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('GDP', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('gross domestic product', @equivalentId)

SET @equivalentId = '9efdad8e-a975-4b71-9786-0f772888549e'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('GNP', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('gross national product', @equivalentId)

SET @equivalentId = '5342f6ce-2a18-4c67-b527-fd288f3473ce'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('quick books', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('quickbooks', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('quick-books', @equivalentId)

SET @equivalentId = 'b57075b5-05c6-4cac-b3b9-83e7f1db5be9'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('tax', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('taxation', @equivalentId)

SET @equivalentId = 'c91bad4d-ffbf-4755-9d42-edcbff0f6927'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Health Insurance Candidate', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Health insurance officer', @equivalentId)

SET @equivalentId = 'e61d4051-3ebd-455f-a951-9caa1f056f41'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('recruitment', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('onboarding', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('on boarding', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('on-boarding', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('candidate care', @equivalentId)

SET @equivalentId = 'f314f311-7c4e-4a5d-9648-467f84328742'

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

SET @equivalentId = '52654b8b-2884-4128-bd8b-8dd832a041bf'

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

SET @equivalentId = '9383b23d-efa5-40a0-9df9-d36a47f5785e'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Candidate Resourcer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('candidate research', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('resourcher', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('recruitment researcher', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('candidate sourcing resourcer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('candidate sourcing researcher', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('candidate sourcing', @equivalentId)

SET @equivalentId = '548c3953-2b1d-4f86-96c9-2218b2a8cea0'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Recruitment Administrator', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Recruitment Administration Officer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('admin assistant recruitment', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('recruitment administrative assistant', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('administrative assistant - recruitment', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('DBS / RECRUITMENT ADMINSTRATION / CUSTOMER SERVICE', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Recruitment Support Officer', @equivalentId)

SET @equivalentId = '13949be9-4d4e-44f0-b044-fa3c53fbb496'

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

SET @equivalentId = '969f35a4-8885-4f0c-a55d-9ca060e7a907'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('cv', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('curriculum vitae', @equivalentId)

SET @equivalentId = '1ca9ca5d-cc01-4cbd-941e-a06105e4fba8'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('rec2rec', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('recruiter to recruiter', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('rec-to-rec', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('rec to rec', @equivalentId)

SET @equivalentId = '79f1798f-682e-4597-aad8-32363d79a96b'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('career advisor', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('career cousellor', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('career coach', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('career consultant', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('career couseller', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('careers and employment', @equivalentId)

SET @equivalentId = 'b6fb49ca-8978-4534-9008-ff4a3e4d0196'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Onsite Recruitment Consultant', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Internal Recruitment Consultant Human Resource Team ongoing consulting role', @equivalentId)

SET @equivalentId = '0fcbd3b6-1907-4733-8d2d-c39a1ae29bd5'

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

SET @equivalentId = '12823596-3c64-4009-9d9e-00aece8fcab2'

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

SET @equivalentId = 'c25ebe83-5d3a-403f-8dd0-dfb634872c45'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('employment services manager', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('employment services operations manager', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('general manager employment', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('general manager employment services', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('gm employment', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('gm employment services', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('employment solutions manager', @equivalentId)

SET @equivalentId = '505bb123-5c4d-493c-8add-124c5f9cbff1'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Senior HR Advisor', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Senior Human Resources Advisor', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('snr hr advisor', @equivalentId)

SET @equivalentId = '7e24f24f-d1ab-4f37-add4-ae4de39237b6'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Senior Human Resources Consultant', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Senior Human Resources Officer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Senior HR Consultant', @equivalentId)

SET @equivalentId = '5e29ba81-4725-48cb-b190-224da47b1c47'

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
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('hr generalist', @equivalentId)

SET @equivalentId = '2145b1c6-85a2-4e5a-b409-f4d282fa578d'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Human Resources Analyst', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('HR ANALYST', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('human resource analyst', @equivalentId)

SET @equivalentId = '45786b96-15e5-4895-8ab4-cf142f8c1ea2'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('HR Executive', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Human Resources Executive', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('human resources exec', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Human Resources Generalist', @equivalentId)

SET @equivalentId = '0013261e-bcdd-4076-8e05-0116e0bbfd7e'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('HR Payroll', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('HR/Payroll', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('H.R. Payroll', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('human resources/payroll', @equivalentId)

SET @equivalentId = '912ebc36-754a-42a4-bb5e-7f5627cbb1ef'

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

SET @equivalentId = '4e4bad6a-800a-4a7e-8a81-395c0d495e75'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Temporary Payroll Officer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Temp Payroll', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Payroll Temp', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('payroll officer - temps', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('payroll officer - perm', @equivalentId)

SET @equivalentId = '91a4836b-86e0-48d4-8e53-874b58130449'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Senior Payroll Officer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Payroll Supervisor', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('payroll manager', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('head of payroll', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Group Payroll Manager', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Payroll Manager / Payroll Team Leader', @equivalentId)

SET @equivalentId = 'a4d005e3-9cc7-43dc-a9a0-d9dc47680d27'

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

SET @equivalentId = 'df6ea506-78f2-4ba9-b800-508375b25733'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Retail Supervisor', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Retail Team Leader', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Retail Store Team Leader', @equivalentId)

SET @equivalentId = '294c6be4-97b8-48db-b624-165779db80e2'

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

SET @equivalentId = 'bfb26dda-88de-4fcd-9197-d19bc2d8cf31'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('LinkMe', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('owner of this intellectual property', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('link me', @equivalentId)

SET @equivalentId = '6e182b61-5291-4228-8b0f-28608fac1ee1'

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

SET @equivalentId = '0cd4a2bd-5120-4578-96c7-c1a7d4a4258c'

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

SET @equivalentId = '5b26b7ef-439e-4e59-97e7-3d6813231bc1'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('duty free', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('dutyfree', @equivalentId)

SET @equivalentId = '935374f8-52a3-45c7-b162-7e2277046781'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Floor Manager', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Floor Supervisor', @equivalentId)

SET @equivalentId = 'a9233220-aabd-4a44-889a-4715f2ddfd77'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Bar Manager', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Bar Supervisor', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('bar owner', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('head barman', @equivalentId)

SET @equivalentId = 'd4def403-9f0f-4610-9429-7579beab6245'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Casino Manager', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('casino mgr', @equivalentId)

SET @equivalentId = 'abe9b20b-0142-4564-883a-a7b03c9887eb'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('food and beverage', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('foodandbeverage', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('food & beverage', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('food&beverage', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('food service manager', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('f and b', @equivalentId)

SET @equivalentId = 'af80b555-c74f-4bb6-9dcc-b554597742fc'

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

SET @equivalentId = '550e49c3-9307-4bff-98dd-e14b4924547c'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('barrista', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('barista', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('coffee maker', @equivalentId)

SET @equivalentId = 'c44113f6-a5e9-49c5-ae73-66e1bd82731a'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Head Cashier', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('chief cashier', @equivalentId)

SET @equivalentId = '18076dad-fa53-432d-835e-d6a8266f0ff6'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Junior Cashier', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('jnr cashier', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('jr cashier', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('trainee cashier', @equivalentId)

SET @equivalentId = '697c326b-7e95-4a97-972f-1d6c0a09b46e'

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

SET @equivalentId = '54075fb9-908b-41cb-9dfb-e91b93fcb879'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Gaming Manager', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Gaming Mgr', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('gambling manager', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('gambling mgr', @equivalentId)

SET @equivalentId = '9c82b982-e1af-44cf-bdb4-97dea6869f5d'

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

SET @equivalentId = '3dd61316-0446-4d2f-bf59-9ccf47813381'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Head Waiter', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Head Waitress', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Wait Supervisor', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Maitre De', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Senior Waiter', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('front of house', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('maitred', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('maitre d', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('maitrede', @equivalentId)

SET @equivalentId = 'f46700ae-3da3-425d-a32d-acc2b9ac4032'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Waiter', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Waitress', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Wait Staff', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Waitress Customer service', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Waitress/ Kitchen Hand', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Waitress/kitchenhand', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Waitress / Kitchen Hand', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Waiter/kitchen hand', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Waiter / kitchen hand', @equivalentId)

SET @equivalentId = '5d9132c8-8722-49fe-9ead-667be1ec1444'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Kitchen Manager', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Head Cook', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Head Chef', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Chef Manager', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Head Pastry Chef', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Senior Chef', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('number one chef', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('first chef', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('1st chef', @equivalentId)

SET @equivalentId = 'a2af282a-ce27-414d-8410-76f684b43af5'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Second Chef', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('2nd Chef', @equivalentId)

SET @equivalentId = 'db9cfcb1-03a6-4a66-abbe-cafe0ab7a333'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Chef De Partie', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Sous Chef', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Executive Chef', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Commis Chef', @equivalentId)

SET @equivalentId = '8250ed77-2509-45f2-9638-4db5b4a03e54'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Assistant Chef', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Chefs Assistant', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Chef assistant', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('chef''s assistant', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Chef''s assist', @equivalentId)

SET @equivalentId = 'dfb118cd-12b7-49f5-bc99-3d37fe5feac9'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('chef', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('che', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('shef', @equivalentId)

SET @equivalentId = '7361c294-20fe-4ade-bb7a-7a413ceb40e7'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Executive Pastry Chef', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Pastry Chef', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('pastry cook', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Senior pastry chef', @equivalentId)

SET @equivalentId = '7abeadf4-410d-46d2-a62b-50b4a498baef'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Apprentice Chef', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('2nd year Apprentice Chef', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('1st Year Apprentice Chef', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('4th Year Apprentice Chef', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Trainee chef', @equivalentId)

SET @equivalentId = '581bd816-9c21-444d-8619-7bf736b53e04'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Cook/Chef', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Relief Chef', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Kitchen Hand/Cook', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Cook/Kitchen Hand', @equivalentId)

SET @equivalentId = '8167f02f-0f38-438e-b0b2-4b9b808976c7'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('kitchen hand', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('kitchenhand', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Kitchen-hand', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Kitchen Worker', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Kitchen Hand/Waiter', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Kitchen hand/waitress', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('casual kitchen hand', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Kitchen hand Customer service', @equivalentId)

SET @equivalentId = 'f3162950-a8d5-4e47-a873-0814a2a0ee67'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Delicatessen Manager', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Deli Manager', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Service Deli Manager', @equivalentId)

SET @equivalentId = 'ade23df3-2170-49f1-95ca-b06781c4ce9d'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Deli Assistant', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Service Deli Assistant', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Serviced Deli Assistant', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('DELICATESSEN ASSISTANT', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Deli', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Delicatessen', @equivalentId)

SET @equivalentId = '2f5f6c1c-6f78-44ec-a62e-c57a08861a4d'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Food Processor', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Food Processing', @equivalentId)

SET @equivalentId = '4125a214-c60e-4b80-a71c-959af6ca6f66'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('dish washer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('dishwasher', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('kitchen assistant', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('sandwich hand', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('sandwichhand', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('pot scrubber', @equivalentId)

SET @equivalentId = '5c27f532-5904-4610-a5a3-17dd3a981fb0'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Retail Business Consultant', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Consultant - Retail Business', @equivalentId)

SET @equivalentId = '0aa59954-6e14-42ca-915e-f251f8b75cc7'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Pricing Coordinator', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Pricing Co ordinator', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Pricing Co-ordinator', @equivalentId)

SET @equivalentId = '0be953fc-9402-4409-931c-990dff1a7812'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Call Centre Manager', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Call Centre Team Manager', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Call Centre Supervisor', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Telemarketing Manager', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Telemarketing Team Leader', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('contact centre manager', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('call centre mgr', @equivalentId)

SET @equivalentId = '3628cd23-bf58-4604-b75c-f274ba206340'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Customer Service Manager', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Customer Services Manager', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Customer Service Supervisor', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Customer Service Team Leader', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('customer care manager', @equivalentId)

SET @equivalentId = 'e4e0edeb-a322-4bcd-9abb-d6c3d27be978'

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

SET @equivalentId = '293bf9a9-acd4-44c5-ba1c-9defbbcdc86c'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('customer service', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('customer care', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('client service', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('client care', @equivalentId)

SET @equivalentId = '9b602f95-cb26-4512-bbfd-ad8aeadeb026'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('call centre', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('call center', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('contact centre', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('callcentre', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('callcenter', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('contactcentre', @equivalentId)

SET @equivalentId = '6020edd9-4229-4bf6-ae40-25ceafdd041a'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('cold call', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('cold calling', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('outbound', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('out bound', @equivalentId)

SET @equivalentId = '98b446d8-da20-4efb-8e1f-afb812e886a0'

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

SET @equivalentId = 'ca04c5d6-af58-417a-a923-733833c4f4f0'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Junior Receptionist', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Jr Receptionist', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Trainee Receptionist', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Assistant Receptionist', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Reception Assistant', @equivalentId)

SET @equivalentId = '73404c41-0453-4b98-8b6d-99a77f32eed3'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Junior Secretary', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Jr Secretary', @equivalentId)

SET @equivalentId = '5fe9a1d8-c0be-47f9-a801-5e76e7d09a6c'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Front Office Receptionist', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Receptionist Front Office', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Front Desk Receptionist', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Frontline Receptionist/Office All-rounder', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('front office reception', @equivalentId)

SET @equivalentId = '5c9a0087-db7d-4e43-84e5-b9abe3602852'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Front Office Manager', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Front Office Supervisor', @equivalentId)

SET @equivalentId = '5d4273a9-f4b6-43e7-ab01-04c09d601bd9'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Office manager', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Office Supervisor', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Office Manager/Personal Assistant', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Office Manager/PA', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Office Manager / Executive Assistant', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('office manager/ea', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Office Mgr', @equivalentId)

SET @equivalentId = '5b0df8d8-7771-4317-8926-0299239a7af2'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Office Assistant', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Office Asst', @equivalentId)

SET @equivalentId = '139e9857-ed07-4651-b4f0-527478bc1676'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Bilingual Receptionist', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('bi lingual receptionist', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('bi-lingual receptionist', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('bi-linqual telephonist', @equivalentId)

SET @equivalentId = 'e3af1a87-5a12-4eef-8f6c-e5f8263b9443'

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

SET @equivalentId = '18fc5944-daf9-4a33-a853-fd1c3394fbc4'

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

SET @equivalentId = '997a603a-1f0e-4bc6-b230-ef01c12092b5'

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

SET @equivalentId = '8251c1ab-ae55-4d2d-b74c-91e3fc2ee103'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Temporary Receptionist/Secretary', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Temporary Receptionist', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Casual Receptionist', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Relief Receptionist', @equivalentId)

SET @equivalentId = '7b279a63-32b0-40a3-8f40-3d19482fc1e6'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Administration Supervisor', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Admin Supervisor', @equivalentId)

SET @equivalentId = '6f04b40e-d2aa-486f-bd66-df39cca4d714'

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

SET @equivalentId = 'b4f8ad13-a703-493b-8f19-fadccf7d1d3e'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('admin', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('administration', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('administrative', @equivalentId)

SET @equivalentId = '91f441e5-6e80-4ae5-80fd-b18927816f86'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Administrative Support Officer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Administrative Support', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Administration Support', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Admin support', @equivalentId)

SET @equivalentId = '58650085-1182-4373-a220-c9f7c01fa19a'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Temporary Clerical Assistant', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Temporary Administrative Officer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Relieving Administrative Officer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Temporary Admin Officer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Temp Admin Officer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Temp Administration Officer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('temporary administration assistant', @equivalentId)

SET @equivalentId = '01d5a69a-ada6-4f71-811d-78c18728dcb5'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Junior Clerical Assistant', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Clerical Trainee', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Clerical Traineeship', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Trainee Clerical Assistant', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Office Junior', @equivalentId)

SET @equivalentId = 'd90faf80-b991-447a-91e1-5cabae01ad46'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Admin Trainee', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Office Administration Trainee', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Junior office administration', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Administration Trainee', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Junior Administration Assistant', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('junior admin', @equivalentId)

SET @equivalentId = 'e0b49483-7c05-4667-8b3b-909ab0d42f89'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('copy writer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('copywriter', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('business writer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('content writer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('freelance writer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('copywriting', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('copy writing', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('content writing', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('online writer', @equivalentId)

SET @equivalentId = 'c34f86fc-e8e2-4b36-b64a-c722dd6ac860'

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

SET @equivalentId = '2de21211-0ade-4f3c-b461-0538af37e2d2'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('records clerk', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('records officer', @equivalentId)

SET @equivalentId = '10151aff-c1a0-4530-af6f-a18c8eed1d6b'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Myob trainer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('MYOB Program Support Consultant', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('MYOB Certified Consultant & Trainer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Bookkeeper/Training Myob', @equivalentId)

SET @equivalentId = 'c23909c9-eb81-49c5-bdfe-b81151e3cb37'

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

SET @equivalentId = '80a0c44c-6f3e-400d-ae47-55cfa715a198'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Myob Australia Major Accounts Manager/ Sales Representative', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('MYOB Program Support Consultant/ SME Accounting & Technical Support Representative', @equivalentId)

SET @equivalentId = '3f2d5735-6857-47a9-b681-23b73deed803'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('PA/Admin/Rec/MYOB', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('RECEPTION/MYOB', @equivalentId)

SET @equivalentId = '362cedf9-6b82-4488-9032-64b39a283a9c'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Architectural Design', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Project Architect', @equivalentId)

SET @equivalentId = '79e1398e-fecd-49b8-8536-3d0c1a33ce13'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Senior Architect', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Senior Design Architect', @equivalentId)

SET @equivalentId = '15c02711-faf9-447c-80b0-83a298eb5c03'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Assistant Architect', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Architectural Assistant', @equivalentId)

SET @equivalentId = 'fcaf411b-6be4-4f26-bcc4-abf6c53ab7a4'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Graduate Architect', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Architectural Trainee', @equivalentId)

SET @equivalentId = 'ee621efd-fa32-4793-8108-fca6b88ec3de'

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

SET @equivalentId = '489f6990-b200-4e18-8ee7-d253ab36b533'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('computer aided design', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Autocad', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Auto CAD', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('cad', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('autocad2000', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('autocad 2000', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('cad design', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('cad designer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('cad designing', @equivalentId)

SET @equivalentId = '96af9cec-a0a7-4628-8c9d-1ea79921ae16'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Senior Draftsman', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Senior Design Structural Draftsman', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('lead draftsman', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('drafting manager', @equivalentId)

SET @equivalentId = '04b7fd0b-855f-48b5-89fe-5a6f26664447'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('interior architect', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('interior architect/designer', @equivalentId)

SET @equivalentId = 'be543373-2e5d-4b42-80e9-79c4c63c3da1'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Structural Draftsperson', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('structural drafter', @equivalentId)

SET @equivalentId = 'd9cb5161-1837-4992-b564-3469a2bde176'

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

SET @equivalentId = 'fd295bfc-9af7-4ddd-aa86-a3c5b1fe95f7'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('tradesman', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('tradesperson', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('trades man', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('trades person', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('tradie', @equivalentId)

SET @equivalentId = '61fb2407-9fc0-4fc4-8e99-e6db0fbd66c8'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('plasterer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('solid plasterer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('fibrous plasterer', @equivalentId)

SET @equivalentId = 'ed78e72d-527c-4b27-bf7a-958505bb0ef5'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('scaffolder', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('scaffolding coordinator', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('scaffolding labourer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('scaffolding', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('scaffolding co-ordinator', @equivalentId)

SET @equivalentId = '6570830f-01ec-4102-ac3d-7d895ff9e6c7'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Mechanic Manager', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Workshop Manager', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('MECHANICAL SUPERVISOR', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Mechanical Manager', @equivalentId)

SET @equivalentId = '88786f71-cf41-485b-8cb6-d06d39939837'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('boilermaker', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Boilermaker/Welder', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Welder', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('boiler maker', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('welding', @equivalentId)

SET @equivalentId = 'bbf4b004-5fad-4d0c-8069-990e0225a3ea'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Fitter', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Fitter/Welder', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Mechanical Fitter', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('fitter and turner', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('fitter & turner', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('maintenance fitter', @equivalentId)

SET @equivalentId = 'ad3746ce-9432-42a5-b771-554b95c75bb6'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('hdpe', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('High-density polyethylene', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Hi density polyethylene', @equivalentId)

SET @equivalentId = '4b277639-8faa-46e6-898d-ba4be8b0cc46'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('pvc', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Polyvinylechloride', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Poly vinyle chloride', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Poly vinyl chloride', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Polyvinylchloride', @equivalentId)

SET @equivalentId = '1ef49394-ccfe-46c0-9fef-dc01ad30f64f'

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

SET @equivalentId = '96dff940-c3cc-4667-afbc-9e9248f29757'

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

SET @equivalentId = '6606721c-e367-4ab0-8a46-366775b15767'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('suspension fitter', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('suspension mechanic', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('suspension tech', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('suspension technician', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('suspension specialist', @equivalentId)

SET @equivalentId = '08b06184-3637-464a-ba97-d470d3cc9b84'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('windscreen', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('wind screen', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('auto glass', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('automotive glass', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('windscreen replacer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('windscreen fitter', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('windscreen repairer', @equivalentId)

SET @equivalentId = 'cda9437b-43e8-4fef-8cfe-34486925d9bd'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Assistant mechanic', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Trainee mechanic', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Apprentice Mechanic', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Apprentice Motor Mechanic', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('grease monkey', @equivalentId)

SET @equivalentId = '46fe4fdb-b5f8-4b76-8e6b-0d075a472b9f'

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

SET @equivalentId = '323975e8-0089-4fe2-b864-fe9a3c6a992b'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Apprentice Electrical Mechanic', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('trainee Electrical Mechanic', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('trainee auto-electrician', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('trainee autoelectrician', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('trainee auto electrician', @equivalentId)

SET @equivalentId = '7f3fd01d-5507-4767-ab99-3f1051bc19c5'

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

SET @equivalentId = '7f42d6ff-e070-48a6-9628-b982ae640f96'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Electrical Supervisor', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Leading Hand Electrician', @equivalentId)

SET @equivalentId = 'd9afa04d-e50f-40f5-9290-44fc65cdde29'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Apprentice Electrician', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('trainee electrician', @equivalentId)

SET @equivalentId = 'd39e3b2e-eb3d-4d6a-b4df-e9e4a52b0fb6'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('tiler', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('roof tiler', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('roofer', @equivalentId)

SET @equivalentId = '1812ad94-3979-4c27-9e67-8d962f8acc70'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('stable hand', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('stablehand', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('yardman', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('yardsman', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('strapper', @equivalentId)

SET @equivalentId = 'f737cd7a-18f8-4451-bf59-c9a57e1bf7ec'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('cabling technician', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('cabler', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('telecommunications technician', @equivalentId)

SET @equivalentId = '48d79149-528c-439d-b0b5-b25ae9d8e781'

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

SET @equivalentId = 'ed59804e-6c44-407b-95c7-59c6aa878a17'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('farm manager', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('rural proerty manager', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('farm supervisor', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('farm owner', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('share farmer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('farmer', @equivalentId)

SET @equivalentId = 'b524a378-3ca2-4e7f-b474-edfbbdf11149'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('sheep', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('merino', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('lamb', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('mutton', @equivalentId)

SET @equivalentId = 'fdb383f7-08f5-472b-81b5-a67d1473351e'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('beef', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('cattle', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('angus', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('bull', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('cow', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('heffa', @equivalentId)

SET @equivalentId = 'bdd2c162-4fc6-4e4a-8df1-6d7bec0a03cf'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('dairy', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('milk', @equivalentId)

SET @equivalentId = '47ea1af7-fd6e-455e-8356-787c4e32b6a4'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Agronomist', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Agricultural scientist', @equivalentId)

SET @equivalentId = '22e7ed16-2418-476d-a867-d57f853ec989'

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

SET @equivalentId = '2c14d7ed-5258-40f1-828c-067c99cac054'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('A-Grade Electrical', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('A Grade Electrical', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('A Grade electrician', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('agrade electrician', @equivalentId)

SET @equivalentId = 'e754d626-20ee-4d2f-9e34-d7c9c444692f'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('elec', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('electrical', @equivalentId)

SET @equivalentId = '4b40889a-4a07-4d2d-a7b2-135343b084ff'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('painter', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('house painter', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('painting worker', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('painting and decorating', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('painting & decorating', @equivalentId)

SET @equivalentId = 'b360ca0f-5612-41a4-894d-36f919fcdc89'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('spray painter', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('spraypainter', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('auto painter', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('car painter', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('paint line operator', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('paint shop operator', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('paintshop operator', @equivalentId)

SET @equivalentId = '4027f6d7-8e29-4496-bdb2-8ceb502ce9d3'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('panel beater', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('panelbeater', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('automotive repairer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Panel beating', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Panelbeating', @equivalentId)

SET @equivalentId = 'db16f4ae-7ebd-45c2-9ed4-34b0033a4929'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('freight clerk', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('freight officer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('freight forwarding clerk', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('freight forwarding officer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('shipping clerk', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('shipping officer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('freight staff', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Freight Forwards Operator', @equivalentId)

SET @equivalentId = '974fbd78-5c2d-459c-aec7-542a421e03e6'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('plumber', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('gas fitter', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('plombier', @equivalentId)

SET @equivalentId = '49a88a1a-2c6b-4861-b3f6-65c58abb09cc'

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

SET @equivalentId = '97c04b8e-4937-44da-b612-879ce484f5aa'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('baker', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('bread maker', @equivalentId)

SET @equivalentId = '09da8125-09ca-40d6-8750-77c6bb461684'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('landscaper', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('landscape gardner', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('landscape architect', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('landscaping', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('garden designer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('garden design', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('garden landscape engineer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('garden landscaping engineer', @equivalentId)

SET @equivalentId = 'd71c246c-220a-4610-aabe-7bb8b09813c3'

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

SET @equivalentId = '95d5161c-8453-4b20-ab8f-468337143ac5'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('cabinetmaker', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('cabinet maker', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('furniture maker', @equivalentId)

SET @equivalentId = 'c5a47aaa-f0c9-47e4-ac23-b6b2cd27bc68'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('refridgeration technician', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('refridgeration mechanic', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('fridge technician', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('fridge mechanic', @equivalentId)

SET @equivalentId = '1b895d97-5081-4c8d-8945-19c57b5b8832'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('grounds man', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('groundsman', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('grounds keeper', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('groundskeeper', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('ground attendant', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('public area attendant', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('green keeper', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('greenkeeper', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('turf manager', @equivalentId)

SET @equivalentId = 'eb6b84c9-bfec-4dab-839e-41bc5115f956'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('flooring', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('floor covering', @equivalentId)

SET @equivalentId = '51404e7c-ad5f-4c33-94f7-145ee6db4d2d'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('crane driver', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('crane operator', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('crane truck driver', @equivalentId)

SET @equivalentId = '5c81f222-120c-4550-89e5-10aa784d4a5e'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('rigger', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('dogman', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('dog man', @equivalentId)

SET @equivalentId = '1b138d8f-7519-4e8b-981b-23b04a891fb9'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('tyre fitter', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('tyrefitter', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('wheel fitter', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('tyre technician', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('wheel aligner', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('wheel balancer', @equivalentId)

SET @equivalentId = 'b5e2fc03-5cd4-4397-aacc-6d11240226bb'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('concreter', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('concrete finisher', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('concrete labourer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('concrete worker', @equivalentId)

SET @equivalentId = '589c44e2-fe88-48f4-8a92-29e40d4c7820'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('concrete truck driver', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Concrete Agitator Driver', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('truck driver concrete', @equivalentId)

SET @equivalentId = '204f18e0-cdd5-4c6b-b577-31d9af7b9bb4'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('builder', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('framer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('carpenter', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('charpentier', @equivalentId)

SET @equivalentId = 'df686f81-37ff-465c-8bfb-7880343b2fbc'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('contracts administrator', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('contract adminstrator', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('contract manager', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Contract Compliance Officer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Contract Officer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Contracts Admin', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('contracts manager', @equivalentId)

SET @equivalentId = 'a3c94a3d-b58e-4eb9-b4f6-0f415d410cf6'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('bricklayer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('brick layer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('bricky', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('brickie', @equivalentId)

SET @equivalentId = 'f369eda2-6d7b-43cb-b119-94793dcb9c4b'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('renderer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('rendering', @equivalentId)

SET @equivalentId = 'b12db15a-9a9c-48d8-aacf-038e2d380b6e'

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

SET @equivalentId = '167d4f0f-8886-41b3-b8c8-1bf02fe4e8cf'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('plant manager', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('National Plant Manager', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('manufacturing manager', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Production Supervisor', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Production Manager', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('production mgr', @equivalentId)

SET @equivalentId = '0ec1b9ab-110c-4589-a017-9edc96e1c70f'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Operations Coordinator', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('operations co ordinator', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('operations co-ordinator', @equivalentId)

SET @equivalentId = 'ae32b1ac-798a-4655-af11-1f535a696024'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Operations Analyst', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Customer Operations Specialist', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Operations Support Officer', @equivalentId)

SET @equivalentId = '4ab1d9ef-2a91-442e-9589-40a832bbd435'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Production Coordinator', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Production Co-Ordinator', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Production Co Ordinator', @equivalentId)

SET @equivalentId = '3e8d3c9b-8d70-41c9-a106-c545d7cab665'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Production Team Leader', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Production team lead', @equivalentId)

SET @equivalentId = 'c1899eaa-1555-4314-b591-893a3b4c3f61'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('PRODUCTION SHIFT MANAGER', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('shift manager production', @equivalentId)

SET @equivalentId = '104f4e94-92b8-4e42-9b7d-42405e8cf846'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('crew member', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('crewmember', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('team member', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('teammember', @equivalentId)

SET @equivalentId = '7cb196dc-3ff1-4cd6-a630-404767f1bc88'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Assistant Production Manager', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Asst production manager', @equivalentId)

SET @equivalentId = '0d8344a2-9f35-4f05-8634-d6b25e74b941'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('under manager', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('undermanager', @equivalentId)

SET @equivalentId = '4689cc8f-a588-4786-9175-03bd28556968'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Production Planner', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Production Scheduler', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Production Analyst', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Production Controller', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Production Assistant', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('PRODUCTION CLERK', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('mrp controller', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('supply chain planner', @equivalentId)

SET @equivalentId = '79428bb6-01a6-4130-b794-454e533d7426'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Production Operator', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Production / Machine Operator', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Machine Operator', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('machinist', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Production Worker', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('machiner', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('assembly line worker', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('assembly line operator', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('factory worker', @equivalentId)

SET @equivalentId = '57aa6a1e-3dd8-4488-ad35-059a47189776'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('process worker', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Process Worker/Packer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('food production worker', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Meat Packer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('process operator', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('general hand', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('generalhand', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('processor', @equivalentId)

SET @equivalentId = '9d43d255-1923-4f35-ba1c-8d1e90960adb'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Casual Process worker', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('temp process worker', @equivalentId)

SET @equivalentId = '06e30979-0a3d-4a5d-b9da-842ae6551d3f'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Warehouse Supervisor', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Warehouse manager', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Warehouse superintendant', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Warehouse Mgr', @equivalentId)

SET @equivalentId = 'b5431353-54df-46eb-bb0d-4cbee75a74e8'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Warehouse Assistant', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Warehouse', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Warehouse worker', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Warehouse Clerk', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Warehouse Person', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Warehouse Asst', @equivalentId)

SET @equivalentId = 'bfc9929f-89dc-4f6a-9e15-cbdf10453739'

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
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Forktruck driver', @equivalentId)

SET @equivalentId = 'e451e301-00b0-4972-bb4a-4b8bca3f1448'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Maintenance Supervisor', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Maintenance manager', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Maintenance superintendant', @equivalentId)

SET @equivalentId = '5a8ea69b-50f5-4eb3-a4ee-c26940a53e70'

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

SET @equivalentId = '26427f51-d32b-46a2-8e8e-2e3aae1677d4'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('jit', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('just in time', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('justintime', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('j I t', @equivalentId)

SET @equivalentId = 'b9147e2b-d60c-4d0a-b1fe-86a32f9d413a'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('tqc', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('total quality control', @equivalentId)

SET @equivalentId = '88e4292c-620f-4ce9-bf70-e08bb9f92350'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Factory Hand', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Casual Factory Hand', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('General Factory Hand', @equivalentId)

SET @equivalentId = '4493d308-e421-42db-b121-df88622c95c1'

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

SET @equivalentId = '04fe426a-a8f4-495f-b402-588be17f1d8e'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Quality Assurance Specialist', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('QA Specialist', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Q.A. Specialist', @equivalentId)

SET @equivalentId = '947cda43-a194-4293-91ec-47d341cb759b'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('general counsel', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('head counsel', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('head lawyer', @equivalentId)

SET @equivalentId = '881e74ad-1999-4174-98e0-f95f6eb33dc5'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Lawyer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Solicitor', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Legal Counsel', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('cousel', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Corporate Solicitor', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Litigation Solicitor', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Law Graduate', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('counsel', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('barrister', @equivalentId)

SET @equivalentId = '1e8df393-3d02-437e-b639-cc58a81e4fa8'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Legal Secretary', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Legal Secretary/Receptionist', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Legal Secretary/Personal Assistant', @equivalentId)

SET @equivalentId = 'a4026e3e-8b6b-4024-9f3f-d5798be8d32e'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Corporate legal secretary', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Commercial legal secretary', @equivalentId)

SET @equivalentId = 'c731e601-a139-40bf-aef7-14b8d69d0e5e'

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

SET @equivalentId = '8079769d-81de-4734-bd4e-50b742cdc901'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('jp', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('justice of the peace', @equivalentId)

SET @equivalentId = '3cd6655a-3268-4d4a-8831-75afea0bc379'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('queens counsel', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('queen''s counsel', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('senior councel', @equivalentId)

SET @equivalentId = 'f4178da8-5676-45ea-8904-3c8d77977706'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Associate Nurse Unit Manager', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Nurse Unit Manager', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Charge Nurse', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('head of nursing', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('nursing manager', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('director of nursing', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('nursing director', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('num', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('n.u.m.', @equivalentId)

SET @equivalentId = '92779fa9-2a00-4056-8c0f-9aaf92273b00'

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
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('RegisteredNurse', @equivalentId)

SET @equivalentId = 'f9a69235-b795-4435-a62a-ab4dce4b09b5'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Clinical Nurse Specialist', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Clinical Nurse', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Clinical Nurse Consultant', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('theatre sister', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('trauma nurse', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('theatre nurse', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Nurse Operating Theatre', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Operating Theatre nurse', @equivalentId)

SET @equivalentId = 'b71219f2-f67d-4422-9a4d-9e9906a1cb6a'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('orderly', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('nursing attendant', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('personal service assistant', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('hospital orderly', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('medical orderly', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('shift orderly', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('psa', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('p s a', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('wardsman', @equivalentId)

SET @equivalentId = '977ff4b0-386e-4568-a624-0a5818ab74f3'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Clinical Nurse Educator', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('General Nurse Training', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('nurse educator', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('nursing educator', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('nurse education officer', @equivalentId)

SET @equivalentId = 'eb11b5e9-8bb0-4df2-9504-60f7d36d5be4'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Nursing Assistant', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('assistant nurse', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Nurse Aid', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('ain', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('nurses aid', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('nurses'' aid', @equivalentId)

SET @equivalentId = '11b69b48-f95b-4d79-bcf8-8bba0e683858'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('trainee nurse', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Student Nurse', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Trainee Enrolled Nurse', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Student Enrolled Nurse', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('undergraduate  nurse', @equivalentId)

SET @equivalentId = '06d29cdb-9ee5-4d56-94cd-098cb3b132f9'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Nurseryhand', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('nursery hand', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('paediatric nurse', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('ICU nurse', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('maternity nurse', @equivalentId)

SET @equivalentId = '067f3bc1-3452-41e1-8646-73b469b75e16'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Paediatric Specialist', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Paediatrician', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Baby doctor', @equivalentId)

SET @equivalentId = '4ec2c294-1861-4fa5-8a29-90688b02ce16'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Medical Receptionist', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Medical Secretary', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Senior Medical Receptionist', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Casual Medical Receptionist', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('medical centre receptionist', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('medical reception', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('medical centre reception', @equivalentId)

SET @equivalentId = '275480bf-8a68-4750-b4ad-757abb2fc6d1'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('medical records clerk', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('medical records officer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('medical information officer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('medical information clerk', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('medical records administrator', @equivalentId)

SET @equivalentId = 'e44fac26-e6b0-4b32-8237-f8aecc4e22dc'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('dentist', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('dental surgeon', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Endodontist', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Periodontist', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('general dentist', @equivalentId)

SET @equivalentId = 'e9a1efc0-e0b7-4215-8075-5c19258fe89d'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Dental Nurse', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Dental Assistant/Receptionist', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Dental Assistant/Reception', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('dental assistant', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('dental assisting', @equivalentId)

SET @equivalentId = '2780224e-0f8e-41f6-bdc1-dea12f85b3dd'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('dental hygienist', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('dental technician', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('dental therapist', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('dental hygiene educator', @equivalentId)

SET @equivalentId = 'b7102424-192d-410f-beb7-a2d8fa28b299'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Junior Dental Nurse', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Trainee Dental Nurse', @equivalentId)

SET @equivalentId = 'd859c289-4811-47f3-ace0-f2a5892e05ed'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Veterinary Nurse', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Vet Nurse', @equivalentId)

SET @equivalentId = '70bccfdc-95a0-4f04-bcf2-26faf8050825'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('vet', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('vetinary', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('vetinarian', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('veterinarian', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('veterinary', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('veterinary practitioner', @equivalentId)

SET @equivalentId = '19beaf37-f96c-4b69-90ec-2226002f4df9'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Radiographer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('sonographer', @equivalentId)

SET @equivalentId = 'f50aeb61-c1c7-47c7-80a7-f7cb56e0b0af'

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

SET @equivalentId = 'd9c1b188-44b3-44f5-bc5d-cf30a61d3611'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('obstetric', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('gynaecology', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('gynaecologist', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('obstetrician', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('gyno', @equivalentId)

SET @equivalentId = '4b1d5000-8651-416b-b6f4-c534a0faf6b8'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('ward clerk', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('ward officer', @equivalentId)

SET @equivalentId = '6967cac0-441b-491f-8b4e-b493065ffcb8'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('nanny', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('nannies', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('aupair', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('au-pair', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('au pair', @equivalentId)

SET @equivalentId = '49a0600c-2790-428d-b5a8-d43b2e6ee043'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('osteopath', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('osteo', @equivalentId)

SET @equivalentId = 'cc51009a-d2b4-4e23-9df6-d4f92e893a0b'

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

SET @equivalentId = '0761cf79-c600-4ca8-90e3-3ce99a63ba0b'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('pbac', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Pre-qualifications Bids and Awards Committee', @equivalentId)

SET @equivalentId = 'f2b4ffb1-0104-40bc-b829-87081f543c7f'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('PHARMACY ASSISTANT', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Pharmacy Assistant Customer Service', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('dispensary technician', @equivalentId)

SET @equivalentId = '0357d9ad-f231-4a00-b4e4-fa755aef85b1'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('pharmacist', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('pharmacy manager', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('retail pharmacist', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('pharmacy owner', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Chemist', @equivalentId)

SET @equivalentId = 'b4d95897-a8e5-4f6c-8797-9fc6195a2fdf'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('pharmaceutical', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('pharmaceutics', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('pharmacy', @equivalentId)

SET @equivalentId = 'f398263b-e85f-406b-9b3d-a6f38725773c'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Laboratory Technician', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('medical laboratory technician', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('lab technician', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Laboratory Assistant', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Lab Assistant', @equivalentId)

SET @equivalentId = '7a4540e8-9d31-4082-9480-dd114c8f55ff'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('cardio', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('cardiovascular', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('cardio-vascular', @equivalentId)

SET @equivalentId = 'e4bf73ed-4198-438b-872f-214ddeebd61c'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('first aid', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('firstaid', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('first-aid', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('St John''s', @equivalentId)

SET @equivalentId = '79ccdbc5-1e13-4035-80c8-cb19ee7a058b'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Psych', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('psychologist', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('psychology', @equivalentId)

SET @equivalentId = 'be4e797d-0a68-445a-ba49-dd9d8d8a591f'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('rehab', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('rehabilitation', @equivalentId)

SET @equivalentId = '49c42de8-ba2c-49de-b78d-27623b7d0505'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('occ', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('occupational', @equivalentId)

SET @equivalentId = '086ca78c-3ac5-411e-86d3-a0f61fd6ec4e'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('rec', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('recreational', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('recreation', @equivalentId)

SET @equivalentId = 'e245b316-0860-408d-9bbe-712a3eaadc33'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('speech therapist', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('speech pathologist', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('language therapist', @equivalentId)

SET @equivalentId = 'ef3dd57e-dc3a-4a23-b56b-8b83c8c4e573'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('ambulance', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('paramedic', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('para medic', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('ambulance driver', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('ambo', @equivalentId)

SET @equivalentId = '7c990129-f038-4a4c-b71b-96fbc7686b30'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('scra', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('society of clinical research associates', @equivalentId)

SET @equivalentId = '100ca4f8-d69c-4cf8-9e40-eed731583a71'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('anaesthetic', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('anaesthetist', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('anesthetist', @equivalentId)

SET @equivalentId = '8eb16e2b-3754-426a-875f-05415c96e683'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('a&e', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('accident and emergency', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('emergency medicine', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('casualty', @equivalentId)

SET @equivalentId = 'e68386aa-3427-4b50-aed0-c1fb740dbad2'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('midwife', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('mid wife', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('mid wifery', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('midwifery', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('mid wive', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('midwive', @equivalentId)

SET @equivalentId = '47dec5af-b58c-4ce8-b27b-ce536adb137f'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('chemo', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('chemotherapy', @equivalentId)

SET @equivalentId = '314350e4-d41c-44b7-84fe-eb394226215e'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('en', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('enrolled nurse', @equivalentId)

SET @equivalentId = 'df5c6264-bfbe-49d4-8b4a-b8e13de44674'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('een', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('endorsed enrolled nurse', @equivalentId)

SET @equivalentId = '47aadf87-4246-4e7f-80cd-4ba0a44a1588'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('physio', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('physiotherapy', @equivalentId)

SET @equivalentId = 'd9e0ff01-e93c-4fca-a5b9-97631f7ea5ea'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('chiropractor', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('CHIRO', @equivalentId)

SET @equivalentId = '5cdeaef6-f22b-4638-9dc0-45152d0c7e52'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('chiropractic assistant', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('chiropractic technician', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('chiropractors assistant', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('chiropractor''s assistant', @equivalentId)

SET @equivalentId = 'b7a78c2c-7f78-4811-b621-e4d7a704512f'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('nutriciantist', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('dietician', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('food nutritionist', @equivalentId)

SET @equivalentId = 'b73a578f-95a6-4a56-a7ea-7715de5125ca'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('ddon', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('deputy director of nursing', @equivalentId)

SET @equivalentId = '74ebc818-3180-4f0c-bc2b-1bb0d105de0a'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('ultrasound', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('ultra sound', @equivalentId)

SET @equivalentId = '9c0e9d90-55d3-49e0-963a-53fd8d6e2eee'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('outplacement', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('out placement', @equivalentId)

SET @equivalentId = 'b3ff00db-5b72-4e57-bf47-4f64f3387e0d'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('radiotherapy', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('radio therapy', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('radiotherapist', @equivalentId)

SET @equivalentId = '49700b4f-73b0-46be-b73b-a8e45016d88a'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('outplacement clerk', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('outplacement officer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('outplacement consultant', @equivalentId)

SET @equivalentId = '38b4e3ae-15e3-45bd-a259-0b5863ac0233'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('aod', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('alcohol and other drugs', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('alcohol & other drugs', @equivalentId)

SET @equivalentId = '9c1479bb-235d-45ef-af9b-ec89f725255b'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('neurology', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('neuro', @equivalentId)

SET @equivalentId = 'ed2b75a8-2025-4f2b-867a-c6353335ab66'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('outpatient clerk', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('outpatient officer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('out patient officer', @equivalentId)

SET @equivalentId = '69c2e5be-1173-402d-bb30-d775e7d8e387'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('SMO', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('senior medical officer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('senior medical officers', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('practitioner', @equivalentId)

SET @equivalentId = '4187b615-20dc-4177-86d2-41607b72f062'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Clinical Care Manager', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Clinical Care Mgr', @equivalentId)

SET @equivalentId = 'c9269412-92d5-46e7-ba75-8920777d9ae9'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Radiologist', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Radiology', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Nuclear medicine', @equivalentId)

SET @equivalentId = '919bbfe3-a7b1-4220-ad93-ae633f34e4a2'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Child psychiatrist', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Child psychiatry', @equivalentId)

SET @equivalentId = '5e1e9b68-c858-4416-bc36-eade81b81d78'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Senior Resident Medical Officers', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('SRMO', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Senior Resident Medical Officer', @equivalentId)

SET @equivalentId = '66f50261-c19b-474f-a187-01a0e371d6a6'

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

SET @equivalentId = '9f6181e3-ea20-4983-8a4d-d9645c0052c3'

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

SET @equivalentId = '300413cc-7875-456a-a14a-3344d43522df'

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

SET @equivalentId = '61f42717-a9e9-4a19-8eee-6ccf5cdf38b0'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Assistant Sales Manager', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Asst Sales Manager', @equivalentId)

SET @equivalentId = '39ed4806-ff28-4cc1-aa10-37d2ddb368b4'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Channel Manager', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('channels manager', @equivalentId)

SET @equivalentId = '91d3f952-211d-4c60-96a1-4ba9c7879b1d'

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

SET @equivalentId = 'a8f50945-8d89-488b-8121-c8f839520177'

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

SET @equivalentId = '36ebe581-5097-4a5b-ac1b-d387f24d7709'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Internal Sales Representative', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Internal Sales Rep', @equivalentId)

SET @equivalentId = '361438b2-c67f-404d-a76e-1c93f9704116'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('casual Sales assistant', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('p/t sales assistant', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('p/time sales assistant', @equivalentId)

SET @equivalentId = '003cef8f-f1a7-4c68-905d-97c71fc70224'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Sales Agent', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Sales Advisor', @equivalentId)

SET @equivalentId = '7556e8b4-c9a5-4ac9-8aab-e990849577f3'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('SALES ASSOCIATE', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Sales Assoc', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Senior Sales Associate', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Sr Sales Associate', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Sr. Sales Associate', @equivalentId)

SET @equivalentId = 'be8c05f9-22fc-4890-b3d1-1ddbc2101f56'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Sales Assistant/Manager', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Senior Sales Assistant', @equivalentId)

SET @equivalentId = 'd41f134f-0e8c-4a10-92a7-d5f9f75355a7'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Junior Sales Assistant', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Sales Assistant/Cashier', @equivalentId)

SET @equivalentId = '868d4680-9add-4f80-9d60-fdbd90ec453b'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Sales Assistant/Supervisor', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('supervisor/sales assistant', @equivalentId)

SET @equivalentId = '9b218b49-6523-4e03-8396-0154fa9e6ed7'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Sales Assistant Customer service', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Customer Service/Sales Assistant', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Receptionist/Sales Secretary', @equivalentId)

SET @equivalentId = '9ee9a424-4ce6-452a-b5c4-3754d9d3a290'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Advertising Sales Manager', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Advertising Sales Representative', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Advertising Sales Rep', @equivalentId)

SET @equivalentId = '3079da72-361c-44e0-b086-b670494784e0'

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

SET @equivalentId = '064d9c51-4782-4f27-8450-89c217cc7186'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('crm', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('customer relationship management', @equivalentId)

SET @equivalentId = '14fb3c0a-ac0d-4ea9-93ac-58c179947fe9'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Major Account Manager', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Major Accounts Manager', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Major Accts Manager', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Major Accts Mgr', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Major Acct Mgr', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Major Acct Manager', @equivalentId)

SET @equivalentId = 'baf2087d-64eb-4825-ab13-7ea8080027eb'

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

SET @equivalentId = '2025eeaf-4f24-4799-9389-00ea64e30a6e'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('vp marketing', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('vice president marketing', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('marketing vice president', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('marketing vp', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('marketing v.p.', @equivalentId)

SET @equivalentId = '88736ecf-01d9-49bf-8ae2-31b4af38bbbb'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('membership manager', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('membership development manager', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('manager membership services', @equivalentId)

SET @equivalentId = 'e4865aaf-0115-4166-9b7f-0210f60e39c0'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('membership consultant', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('membership officer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('membership services officer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('membership administrator', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('membership sales consultant', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('membership development consultant', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('membership development offcier', @equivalentId)

SET @equivalentId = 'a1a6ce50-374c-4423-a3f7-af5486851093'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Online Marketing Manager', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Internet marketing manager', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Internet marketing mgr', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('online marketing mgr', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('web marketing manager', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('web marketing mgr', @equivalentId)

SET @equivalentId = 'f3fe2445-c920-45ae-85e9-05e9e7b167f7'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Assistant Marketing Manager', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('asst marketing manager', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('assistant marketing mgr', @equivalentId)

SET @equivalentId = '1becdcc2-ced9-4b97-8d4c-64573fad2aca'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Marketing Assistant', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Marketing asst', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Casual Marketing Assistant', @equivalentId)

SET @equivalentId = '52da2502-74c0-45e2-a3dd-86e969f935e6'

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

SET @equivalentId = '91596da1-3ed4-4580-8ca8-7caf147f1611'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('PR', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('p r', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('pblic relations', @equivalentId)

SET @equivalentId = '71f55006-6d68-4bf8-ac66-6c58e57c2a76'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Marketing Communications Specialist', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('marketing communications', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('marketing coordinator', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('marketing co ordinator', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('marketing co-ordinator', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Marketing Communications Co-ordinator', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Marketing Communications Co ordinator', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Marketing Communications Coordinator', @equivalentId)

SET @equivalentId = 'b69f84d0-a269-4025-b4ef-a23fe18a174c'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('creative director', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('head of creative', @equivalentId)

SET @equivalentId = '3071092a-c72b-4394-a6c2-349528b6dc4f'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Communications', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Communications Consultant', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('COMMUNICATIONS COORDINATOR', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Communications Specialist', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('comms', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('COMMUNICATIONS CO-ORDINATOR', @equivalentId)

SET @equivalentId = '10d0a0ed-92d9-4479-8c12-70b66dc6a49c'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Market Research', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Market researcher', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Market Research Interviewer', @equivalentId)

SET @equivalentId = '0ed2dddd-4afc-4fa5-8de9-153d0e571ca0'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('fundraising', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('fund raising', @equivalentId)

SET @equivalentId = '08ee818e-b0bb-4427-8703-28a3559cf3a2'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Promotions', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Promotions Officer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Promotions Co-ordinator', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Promotions Manager', @equivalentId)

SET @equivalentId = 'c9cc3a19-1b03-4d5a-8822-0c39e11017ad'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Senior Publicist', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Sr Publicist', @equivalentId)

SET @equivalentId = '232a348a-b477-4b8c-9151-c72d5f45612c'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Event Manager', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Events Manager', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('event management', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('event coordinator', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('events coordinator', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('event co-ordinator', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('events co-ordinator', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Events Managing', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Event Mgr', @equivalentId)

SET @equivalentId = 'd4e7a1a3-dc9a-42d8-abfd-3fad5f3cf035'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('event staff', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('event hostess', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Function Hostess', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('hostess', @equivalentId)

SET @equivalentId = 'c30f22d4-53c0-4d9a-bf29-bb4eb32c3eca'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Advertising Sales Executive', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Advertising Sales', @equivalentId)

SET @equivalentId = 'eef48576-4e44-4478-8ed8-e990d29c3bc4'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Product Manager', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Senior Product Manager', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('National Product Manager', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Assistant Product Manager', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Group product manager', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('New Product Manager', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('product design manager', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('product management', @equivalentId)

SET @equivalentId = '2ee1f4cd-8e68-4741-918f-f8fc3f63f841'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Market Research Officer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Market Research Consultant', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Market Research Analyst', @equivalentId)

SET @equivalentId = 'f06032c5-85eb-414e-93a9-31cc5b50082e'

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

SET @equivalentId = 'aad4be43-0c26-4e46-a422-1e2fe725f8c9'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Advertising Account Manager', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('account manager advertising', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('advertising consultant', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('advertising account executive', @equivalentId)

SET @equivalentId = '50234eb2-ee28-4200-bf43-17cd09a43cd0'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('fmcg', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('fast moving consumer goods', @equivalentId)

SET @equivalentId = '2d157ad5-5531-421a-a996-fcf65f3b05f5'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Merchandise Planner', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Merchandise Planning', @equivalentId)

SET @equivalentId = '82cdf71b-be29-40c5-a999-f0c0ce58af68'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Research Assistant', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Research Asst', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Senior Research Assistant', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Sr Research Assistant', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('clinical research', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('research officer', @equivalentId)

SET @equivalentId = '8d6f870d-8f89-4b2d-b06b-dca2fe2fb62d'

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

SET @equivalentId = 'ac9cdde8-ef70-45b9-bd18-9516401eaa01'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Secondary Teacher', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Middle School Teacher', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('teacher secondary', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('secondary and primary teacher', @equivalentId)

SET @equivalentId = '2cd2a975-f468-475b-8b80-07738191344e'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Primary Teacher', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Classroom Teacher', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Primary School Teacher', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('junior school teacher', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('PS teacher', @equivalentId)

SET @equivalentId = '84acebca-a24f-4ca3-bc20-e39402d163b6'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Senior grades teacher', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('high school teacher', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('vce teacher', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('v.c.e. teacher', @equivalentId)

SET @equivalentId = 'b1aef1d4-f8b0-42ba-b629-3a2c594ed722'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Teacher', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('school teacher', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Teacher/Trainer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('primary and secondary teacher', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('class teacher', @equivalentId)

SET @equivalentId = '4c39090a-981d-404f-b930-e8ae7b968faa'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('kindergarten teacher', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('kindy teacher', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('infants school teacher', @equivalentId)

SET @equivalentId = 'ecba940a-ae80-40ca-9d2d-97c03b0b7e45'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('librarian', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('library teacher', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('teacher librarian', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('campus librarian', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('branch librarian', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('knowledge centre manager', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('resource librarian', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('resources librarian', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('resources technician', @equivalentId)

SET @equivalentId = '47b9cf14-fc6f-4b39-95e2-a19f9aa72da3'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('library technician', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('library assistant', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('library attendant', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('libraries officer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('library officer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('library worker', @equivalentId)

SET @equivalentId = '1edfd3e3-3080-4c6b-abbc-0810dfa46d86'

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

SET @equivalentId = 'f4161e1d-dbe7-4ceb-a40d-dc4db0ff4e09'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Casual Teacher', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Casual Relief Teacher', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Relief Teacher', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Temporary Relief Teacher', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('CRT', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('CRT teaching', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('CRT teacher', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('casual relief teaching', @equivalentId)

SET @equivalentId = 'ea1ea4c2-5d11-435c-97b6-3353102100f5'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('English Teacher', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('English Language Teacher', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('ESL Teacher', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('esl', @equivalentId)

SET @equivalentId = '96c5b1d5-0199-4ea0-8d1e-1ccfc9849e60'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Computer Teacher', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Information Technology Teacher', @equivalentId)

SET @equivalentId = '9b7b9d9f-702c-4fc0-bae3-a086dcf933ca'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('TAFE Teacher', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('TAFE sessional teaching', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('T.A.F.E. Teacher', @equivalentId)

SET @equivalentId = 'b29ed13c-e15c-4796-a337-04698f75f181'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Mathematics Teacher', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Mathematics/Science Teacher', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Maths Teacher', @equivalentId)

SET @equivalentId = '00be0383-4cce-43f1-8132-6a5b7ed3ee43'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('math', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('mathmatics', @equivalentId)

SET @equivalentId = '991befc2-4aaf-4331-95c6-16d6216dd2fa'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Art Teacher', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Graphic Art Teacher', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Visual Art Teacher', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Teacher Art', @equivalentId)

SET @equivalentId = 'e6ccd476-5cde-4859-9714-3a44bf79848f'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Piano Teacher', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Music Teacher', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Instrumental Teacher', @equivalentId)

SET @equivalentId = '5fb0d019-407c-4e56-93af-9e72debeda69'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Dance teacher', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Belly dance teacher', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('ballroom dance teacher', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('classical dance teacher', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('latin dance teacher', @equivalentId)

SET @equivalentId = 'aa1b05ec-7e90-446d-af70-3d5b613c8233'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('LOTE Teacher', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('L.O.T.E. Teacher', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Foreign Language Teacher', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Chinese Teacher', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Italian teacher', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('japanese teacher', @equivalentId)

SET @equivalentId = '94f4d057-7411-4f9d-8ae8-1ed20440445c'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Production Technician', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Production Crew', @equivalentId)

SET @equivalentId = '619a3200-9fdf-45b8-924c-f496e3d7373a'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Child Protection Worker', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Child Protection Officer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('cpo', @equivalentId)

SET @equivalentId = '33aa1388-4a50-40fb-9e47-e6b120cae9d2'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('lecturer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Associate Lecturer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Assistant Lecturer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Senior Lecturer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Guest Lecturer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('university lecturer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('uni lecturer', @equivalentId)

SET @equivalentId = '106bb580-5426-43b3-96d4-89feecd90a72'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('professor', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('dean', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('university professor', @equivalentId)

SET @equivalentId = '1c106bd9-9a9e-4424-938e-94591ccbd12f'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('University Tutor', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('uni tutor', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('uni tutoring', @equivalentId)

SET @equivalentId = 'c0381b49-4d7e-4da4-a427-fe4e84cae16d'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Coordinator', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Co ordinator', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Co-ordinator', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('ordinator', @equivalentId)

SET @equivalentId = '79c436f2-0809-4ef1-a052-8439add094ce'

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

SET @equivalentId = '8fdb328d-5aea-4bdf-8aca-53eabdb8f52f'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('educator', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('tutor', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('trainer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('instructor', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('trainor', @equivalentId)

SET @equivalentId = 'e5eef65b-f97c-4249-b151-3de92bfe216f'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('kinder', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('kindergarten', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('kinda', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('childcare centre', @equivalentId)

SET @equivalentId = '570184d3-e068-4db0-897e-e9b7ffbb32c3'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('centre', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('center', @equivalentId)

SET @equivalentId = 'e8dcb594-5ab3-4958-bd88-0a41e61cfb21'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('child', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('children', @equivalentId)

SET @equivalentId = '4f8594ac-0492-4489-a62d-955411a389ce'

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

SET @equivalentId = '9460e518-bf47-4c9e-b889-8239130a079c'

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

SET @equivalentId = '6f4adeac-f04b-4658-b29b-c3d050c9a081'

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

SET @equivalentId = '64894079-9ea1-43da-9aa4-fb1181d3d10f'

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

SET @equivalentId = '1f106b0f-e622-4106-8fbf-9730a5879d16'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('aquatic coordinator', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('aquatic facility assistant', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('pool operations manager', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('pool operations co ordinator', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('pool operations coordinator', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('pool superintendant', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('pool operations attendant', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('aquatic co-ordinator', @equivalentId)

SET @equivalentId = '4271c404-c6aa-4211-ad49-d6a6c2f0a5b6'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('pool builder', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('pool installer', @equivalentId)

SET @equivalentId = 'c33d989d-2507-4824-b8d6-d221980195cc'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('poolman', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('pool man', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('pool cleaner', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('pool technician', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('pool maintenance', @equivalentId)

SET @equivalentId = '65aa5b50-775f-4069-a2e7-d9ccae9a6828'

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

SET @equivalentId = 'cfa3d55e-09ac-47e6-a5dd-46b401de453c'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('recreation centre manager', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('fitness centre manager', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('swim centre manager', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('rec centre manager', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('centre director', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('leisure centre manager', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('leisure centre director', @equivalentId)

SET @equivalentId = 'f547b0c8-f4da-443b-a7b3-efc53b4077c8'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('masseuse', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('massage therapist', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('massage specialist', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('masseur', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('massage therapy', @equivalentId)

SET @equivalentId = 'f21ddd90-2535-4a7e-9a06-273060f02cdc'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('cpr', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('cardio pulmonary resuscitation', @equivalentId)

SET @equivalentId = 'ec99915b-f1ea-4a67-9684-bf2f0e9cb0f8'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('environmental', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('sustainability', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('environment', @equivalentId)

SET @equivalentId = '5fb1318c-2898-40a4-b554-440345b28b09'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('vacation', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('holiday', @equivalentId)

SET @equivalentId = 'a7b6776d-2f6a-4266-b32c-c27662154d18'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('afl', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('australian football league', @equivalentId)

SET @equivalentId = 'e61d399b-a091-48da-85d6-543b22ebc3c1'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('football', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('footy', @equivalentId)

SET @equivalentId = 'e72a1783-2473-4b2b-b0da-a597b677ef49'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('nrl', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('national rugby league', @equivalentId)

SET @equivalentId = '395a29fb-060d-47aa-8ee6-3874b34f90cb'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('motor sport', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('motorsport', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('car racing', @equivalentId)

SET @equivalentId = 'b738e601-284c-4ae4-8073-cebbf093dd27'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('nbl', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('national basketball league', @equivalentId)

SET @equivalentId = '7d024930-9361-41cc-aa1e-62a4066504e5'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('nba', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('national basketball association', @equivalentId)

SET @equivalentId = 'ccf493bf-bd12-40df-8e38-76ae3871d461'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('aoc', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('australian olympic committee', @equivalentId)

SET @equivalentId = '6a99c135-cc94-441c-8952-9afe6fa8050f'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('winter sport', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('wintersport', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('skiing', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('snow skiing', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('snowboarding', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('snow boarding', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('snow sport', @equivalentId)

SET @equivalentId = 'f3e05ec6-4fbe-494e-a449-75a390173627'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('train driver', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('rail driver', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('traindriver', @equivalentId)

SET @equivalentId = '88999ff7-cdf2-4a07-a596-e74b424dabfd'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('tram driver', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('tramdriver', @equivalentId)

SET @equivalentId = '3eb28727-26b0-40d6-ae01-cf84ca79748f'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('cyclist', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('bike rider', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('bicycle rider', @equivalentId)

SET @equivalentId = 'a85a9967-d14d-42cd-9a2c-bf250bfa1820'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('parking inspector', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('traffic officer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('council parketing inspector', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('infringements officer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('parking enforcement officer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('car parking officer', @equivalentId)

SET @equivalentId = '2a48757a-a645-4485-a040-eabc6b634432'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('carpark manager', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('carpark supervisor', @equivalentId)

SET @equivalentId = '8de57920-b564-4795-98d9-e0155af282b6'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('catering supervisor', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('catering manager', @equivalentId)

SET @equivalentId = 'fe5abd83-1243-4d13-9289-33bddb3b9e39'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('catering assistant', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('catering service assistant', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('catering officer', @equivalentId)

SET @equivalentId = 'c04880b3-ff85-4e7f-923f-207d28a01187'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('cellarhand', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('cellar hand', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('cellarman', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('store and cellar', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('cellar man', @equivalentId)

SET @equivalentId = 'b0343c80-252a-4d34-b47b-8f30c96bf269'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('aquaculture', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('aqua culture', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('aquaculturalist', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('aqua culturalist', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('fish farmer', @equivalentId)

SET @equivalentId = '5371d092-8102-45c6-8ec3-7050e9f2e87f'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('rifle', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('gun', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('firearm', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('fire arm', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('shotgun', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('pistol', @equivalentId)

SET @equivalentId = '2f8c7b91-fe94-44e0-af41-0757c530a618'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('bid', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('proposal', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('tender', @equivalentId)

SET @equivalentId = '8584e9da-7bec-4252-9015-47c8282cdaf3'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('dress maker', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('dressmaker', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('couture', @equivalentId)

SET @equivalentId = '8e1ed1ed-eba9-470e-9a97-2145d86bdc55'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('fitness first', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('fitnessfirst', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('fitness 1st', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('fitness1st', @equivalentId)

SET @equivalentId = '71a66275-6f5a-4ffe-8280-3957fb38616b'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('project development officer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('project development coordinator', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('project development co-ordinator', @equivalentId)

SET @equivalentId = '8c43ee80-030a-46f6-a565-80e0723f807e'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('subcontractor', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('sub contractor', @equivalentId)

SET @equivalentId = '1629ce73-a2b6-4256-836f-8430c49595f5'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('fernwood', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('fern wood', @equivalentId)

SET @equivalentId = '192ee8be-a601-49bb-9438-724f9b599b2e'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('ymca', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('the y', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('young men''s christian association', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('y m c a', @equivalentId)

SET @equivalentId = 'e0d6e5a2-ce02-4381-98b5-4e1fd5134b54'

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

SET @equivalentId = '117ed436-4430-4c70-9b17-51de152cae33'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('concierge', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('bell attendant', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('porter', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('bell hop', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('bellhop', @equivalentId)

SET @equivalentId = '83d42bd6-3945-4e63-81e6-3fdde4f3124c'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('tour guide', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('tour director', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('bus guide', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('tourguide', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('resort guide', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('tour driver', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('tour driver guide', @equivalentId)

SET @equivalentId = '0f56a51f-7ac7-4452-98f7-9b3514647351'

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

SET @equivalentId = '4fa2bd01-08da-466f-bd62-ae5614fbc652'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Head cleaner', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Cleaning Manager', @equivalentId)

SET @equivalentId = '3f35019b-0a3f-469e-a417-d8e9d52c7ea8'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Shopping Centre Cleaner', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('centre cleaner', @equivalentId)

SET @equivalentId = 'bdc42332-c895-49eb-b6a3-1c27ff875b89'

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

SET @equivalentId = '856584c5-45e2-4800-ac02-e5b84e9b962f'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('acfi', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Aged Care Funding Instrument', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('ACFI Specialist', @equivalentId)

SET @equivalentId = 'f5fb2135-7e09-46dd-870d-a742f81f589d'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Healthcare', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Health Care', @equivalentId)

SET @equivalentId = 'cd1c072a-8afe-496f-a200-d93bd8cd0b56'

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
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('employment officer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Employment Placement Officer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('epo', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('employment career officer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('placement coordinator', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('placement co ordinator', @equivalentId)

SET @equivalentId = '28fbfa57-840a-406d-8fef-43911151bb1f'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('aged care', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('agedcare', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('elderly care', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('elderlycare', @equivalentId)

SET @equivalentId = 'd173a9b7-7b8c-48a3-a573-4d5f434a1a22'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('disability', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('handicapped', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('handicap', @equivalentId)

SET @equivalentId = 'd2f66880-b16f-4f68-8572-e966fd799f2d'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('hospice', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('palliative', @equivalentId)

SET @equivalentId = '6f430d15-da60-4d06-8454-76e6c3e61c45'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('worker', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('officer', @equivalentId)

SET @equivalentId = '3aab08ea-3299-44ef-a91d-d1e3a841ad03'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('youth', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('adolescent', @equivalentId)

SET @equivalentId = 'f7d53d5c-9501-4143-acb3-d418f7bca3f5'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('family services', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('community services', @equivalentId)

SET @equivalentId = '529d1b68-2729-4b67-b9bf-bc32a91b936b'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('housing officer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('housing services officer', @equivalentId)

SET @equivalentId = 'd2e417c4-1734-47e7-b317-437cfa6f0009'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('wellness', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('fitness', @equivalentId)

SET @equivalentId = 'de953c55-02ef-4776-96ed-df4a7e3cd53d'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('crisis response officer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('crisis support officer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('crisis assistance officer', @equivalentId)

SET @equivalentId = 'a6cdde53-712b-4be4-9c31-bc182b402f41'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('maternal health', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('mothercraft', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('mother craft', @equivalentId)

SET @equivalentId = '934acc01-3b71-4276-a780-73ff56ee9417'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('zookeeper', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('zoo keeper', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('animal keeper', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('animal handler', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('animal carer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('animal technician', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('animal management officer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('animal attendant', @equivalentId)

SET @equivalentId = 'ee7b84c8-e8b1-407c-90ef-5147f654a10d'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('pco', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('professional conference organiser', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('professional conference organizer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('conference program manager', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('conference organiser', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('conferencing account manager', @equivalentId)

SET @equivalentId = 'a5ad33ce-7ef1-46cd-aa3c-088be52266b6'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('beautician', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('beaty therapist', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('beauty therapist', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('beauty consultant', @equivalentId)

SET @equivalentId = '368dcaf9-9db2-4a23-963a-04779e2ec2e9'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('garbage collector', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('garbo', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('garbage truck driver', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('garbage supervisor', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('refuse collector', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('refuse truck driver', @equivalentId)

SET @equivalentId = 'c5677ed9-8350-4d35-acf3-8a13b34be292'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('typesetter', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('type setter', @equivalentId)

SET @equivalentId = '7551311f-da65-430d-9201-f7e3ada7b68b'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('priest', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('vicar', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('minister of religon', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('chaplain', @equivalentId)

SET @equivalentId = '20c4784c-f3f9-42bb-8fb2-25b6a05621fc'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('mower man', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('mowerman', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('lawn mower', @equivalentId)

SET @equivalentId = 'ffae70b8-d5b7-452a-97a6-3fb372ae826f'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('signwriter', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('sign writer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('sign designer', @equivalentId)

SET @equivalentId = '3960e83d-3905-4455-8461-c0948334ffe8'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('funeral director', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('funeral assistant', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('funeral coordinator', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('funeral consultant', @equivalentId)

SET @equivalentId = '71e036ac-2a9d-4864-9ccd-afd8f239c891'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('vigneron', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('winemaker', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('wine maker', @equivalentId)

SET @equivalentId = '8d795b85-50cf-44f8-8f25-3cf6ccc71d80'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('dj', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('disc jokey', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('radio announcer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('disc jockey', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('voice over announcer', @equivalentId)

SET @equivalentId = '9cb5c047-a377-4fe1-817e-fa4f4507b074'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('arborist', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('tree doctor', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('tree worker', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('tree climber', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('tree lopper', @equivalentId)

SET @equivalentId = '8224ce0f-22f0-40f4-bbb2-7dcc4394299e'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('carpark attendant', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('carpark operator', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('car park attendant', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('car park operator', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('parking attendant', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('parking staff', @equivalentId)

SET @equivalentId = '63a8fa15-9f10-422e-b4e4-0a0394c36d81'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('postman', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('post man', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('postie', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('postal officer', @equivalentId)

SET @equivalentId = '902e1de8-d845-4cc6-96bc-c015e12ea9df'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('horticulturalist', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('horti culturalist', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('horticulture', @equivalentId)

SET @equivalentId = 'bac4ae7d-e6f6-4fb5-b1cd-7fbdcfdc428b'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('armaguard', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('arma guard', @equivalentId)

SET @equivalentId = 'bf30e950-d5a7-4da7-8391-4bf759aa8800'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('valuer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('valuation', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('property valuer', @equivalentId)

SET @equivalentId = '9e41f24e-5de7-4025-b0fb-5d907d294a58'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('diesetter', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('die setter', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('die operator', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('diecast setter', @equivalentId)

SET @equivalentId = 'e8ec6473-36b6-4938-bdee-b766a008a119'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('marine engineer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('maritime engineer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('marine technician', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('maritime technician', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('tugboat engineer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('tug boat engineer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('ship engineer', @equivalentId)

SET @equivalentId = 'e33c920c-af4a-4813-9a96-d8751383c517'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('airtraffic controller', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('air traffic controller', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('air traffic services officer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('air traffic', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('airtraffic', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('air traffic scheduler', @equivalentId)

SET @equivalentId = '0f3aeca7-5787-4faf-90e8-9426ee0a48be'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('baggage handler', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('baggagehandler', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('luggage handler', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('baggage coordinator', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('ground handler', @equivalentId)

SET @equivalentId = 'fbdad940-99be-4bd7-828f-924e354681c5'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('customs officer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('quarantine officer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('customs clerk', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('customs controller', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('customs supervisor', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('customs inspector', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('quarantine inspector', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('quarantine inspection officer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('quarantine clerk', @equivalentId)

SET @equivalentId = 'b59267b8-a65e-490b-a86c-35704d0a771d'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('deckhand', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('deck hand', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('integrated rating', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('able bodied seaman', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('greaser', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('ab', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('general purpose hand', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('gp hand', @equivalentId)

SET @equivalentId = '44744715-3177-4474-8215-1dc61100be85'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('skipper', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('master v', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('master 5', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('master 4', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('master4', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('master5', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('masterv', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('coxswain', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('ship captain', @equivalentId)

SET @equivalentId = '7a107fc2-a72f-4ad3-84c2-5f09566a27f6'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('cabin crew', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('cabincrew', @equivalentId)

SET @equivalentId = '6cad5552-1479-4e97-b92b-0d19ca3388a1'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('steward', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('caterer', @equivalentId)

SET @equivalentId = '33959c3b-e7cf-4465-9b24-6bc12811e8ec'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('course super', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('course superintendant', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('golfcourse superintendant', @equivalentId)

SET @equivalentId = '19a6dbf6-cd95-4ef9-8029-1081446b177b'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('golf pro', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('golf professional', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('golfpro', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('pga professional', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('golf coach', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('golfing professional', @equivalentId)

SET @equivalentId = '722c4c94-e361-470e-8cdd-9a2c1d124000'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('golf shop', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('pro shop', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('proshop', @equivalentId)

SET @equivalentId = '83db4d76-c544-4880-8ffc-8a66f344fb9d'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('golf manager', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('golfclub manager', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('golf club manager', @equivalentId)

SET @equivalentId = '98b465a8-11bf-4891-8213-84bfd878f636'

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

SET @equivalentId = 'b6f0968d-5a48-4cb1-874f-3dce9d52bbe6'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('fire safety officer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('fireman', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('fire fighter', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('firey', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('fire man', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('pompier', @equivalentId)

SET @equivalentId = '33c4c154-e02e-4097-8fdd-7320298e2ef7'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('dry cleaner', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('drycleaner', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('drycleaning', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('dry cleaning', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('dri cleaner', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('dricleaner', @equivalentId)

SET @equivalentId = '17cded52-c689-4e1a-a239-b3c078401354'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('security installer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('security technician', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('signwriter/painter', @equivalentId)

SET @equivalentId = '4037d990-0bcd-4f08-ad86-4bbb4e876d90'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('parts interpreter', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('spare parts interpreter', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('parts cataloguer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('parts clerk', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('parts coordinator', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('parts co ordinator', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Parts Interrupter', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('parts assistant', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('telephone parts sales', @equivalentId)

SET @equivalentId = '711812e8-91b2-4f4a-b68b-a5366db5fb48'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('real estate agent', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('officer in effective control', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('realestate consultant', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('property consultant', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('buyers advocate', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('buyer s advocate', @equivalentId)

SET @equivalentId = '203dbe79-f7cf-44b0-a4e3-0290321dd8f0'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Regional Survey Manager', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('State Survey Manager', @equivalentId)

SET @equivalentId = '13c21f9a-8fc4-4436-b99a-9fe53a3e74e4'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Policy Manager', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Polices Manager', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Senior Policy Officer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Senior Policies Officer', @equivalentId)

SET @equivalentId = '19a701cf-cab8-4991-9876-52f10f11857d'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Unit Manager', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Unit Operations Manager', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Unit Director', @equivalentId)

SET @equivalentId = '857795c2-91f6-4e61-b19c-c0703c163a9c'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Assistant', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Asst', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Asst.', @equivalentId)

SET @equivalentId = '88e032e4-f2f3-460e-bf55-4576008976a9'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('administrator', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('ADMINISTRATIVE MANAGER', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('BUSINESS ADMINISTRATOR', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Senior Administration Officer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Business Administration', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Executive Administrator', @equivalentId)

SET @equivalentId = 'bbeb6420-4065-4eb4-b7f3-64b32186ec58'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Operations Administrator', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Operations Admin', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('ops admin', @equivalentId)

SET @equivalentId = '9268fff1-8926-4c01-b5ae-fb933d11d555'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('house wife', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('housewife', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('domestic manager', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('home manager', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('domestic duties', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('homemaker', @equivalentId)

SET @equivalentId = 'bea6af0b-4dca-4194-a88e-02c0e2526147'

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

SET @equivalentId = '2461b51d-8822-4f47-a60c-648055470703'

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

SET @equivalentId = 'ffed1615-3497-404e-a3dc-82ec01255702'

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

SET @equivalentId = '258fd127-5207-4606-a621-4cae61892c1f'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('technician', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('tech', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('tec', @equivalentId)

SET @equivalentId = '56081f7b-b75c-4c48-a736-2fff818be900'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Delivery Driver', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Pizza delivery driver', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Multi-drop Delivery Driver', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Delivery Driver/Kitchen hand', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Driver', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('courier', @equivalentId)

SET @equivalentId = 'a14a785f-be80-4cf0-b87f-d96efb68e6dd'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Bus Driver', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Coach Driver', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('school bus driver', @equivalentId)

SET @equivalentId = '75104cce-a8ab-4247-b2e9-8bacc2c0ad4c'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Taxi Driver', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Cab Driver', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('HGV Driver', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('cabbie', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('taxidriver', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('taxi operator', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('cabdriver', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('chauffer', @equivalentId)

SET @equivalentId = '1356c067-ac2a-4d77-be85-b6ba2f2b7a59'

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

SET @equivalentId = 'b8cbcac2-e439-4a98-9cbf-dc3fe5e3dce5'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Hyperbaric Technician', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Hyperbaric operator', @equivalentId)

SET @equivalentId = 'c1bc4e31-1b37-4a57-9d84-63ba9f5e48c9'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Work Experience', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Work Experience Student', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('WORK EXPERIENCE - TAFE', @equivalentId)

SET @equivalentId = '03a61886-a752-4dd3-936c-e211685d349f'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Trainee', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Traineeship', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Student trainee', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Graduate Trainee', @equivalentId)

SET @equivalentId = '4a2e3fcb-9966-458e-810e-8ab4d95483b0'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Volunteer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Volunteer Worker', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Volunteer Member', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('NSW Fire Brigade', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('St John''s Ambulance', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('State Emergency Service', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('SES', @equivalentId)

SET @equivalentId = '56a26a54-03d8-4e4c-a5fe-247260c410d4'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('segment manager', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('category manager', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('brand manager', @equivalentId)

SET @equivalentId = '9bb3365d-7e5c-425a-a176-e30e39964866'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Computer Operator', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Senior Computer Operator', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Trainee Computer Operator', @equivalentId)

SET @equivalentId = 'aa66d441-4658-4a47-8dc1-0ec1e4e727fc'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('photographer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('photo taker', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('freelance photographer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('photography', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('professional photographer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('contract photographer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('photog', @equivalentId)

SET @equivalentId = 'e423f980-f401-45a5-bc80-bbff0469b8c6'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Technical Consultant', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Senior Technical Consultant', @equivalentId)

SET @equivalentId = '28b78964-86c4-4a72-9b70-1bc33e142658'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Various', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('varied', @equivalentId)

SET @equivalentId = '89dce4b1-d13d-4a8c-98b5-7b2b8ea3ae7a'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Business Consultant', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Senior Business Consultant', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('New Business Consultant', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Telephone Business Consultant/Customer Service Representive', @equivalentId)

SET @equivalentId = '5abd31e3-7822-4571-9447-11cb8fd604d4'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('dialler', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('power dialler', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('predictive dialler', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('automated dialler', @equivalentId)

SET @equivalentId = 'd8a45d67-737d-440d-9acf-f49b85dd0755'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Translator', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Interpreter', @equivalentId)

SET @equivalentId = '4d98c422-f953-43ac-b7f6-0af2cd1b9404'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('pattern maker', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('pattermaker', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('pattern designer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('pattern design', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('pattern making', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('pattern designing', @equivalentId)

SET @equivalentId = '8ddbd36e-dfe4-41fd-bb59-cefcb14f559b'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('textile designer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('textile design', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('textile designing', @equivalentId)

SET @equivalentId = 'e543e9f2-2fa7-4d32-85ad-20e85c4d33a8'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Folder & Stitcher operator', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Folder and Stitcher operator', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Folder / Stitcher operator', @equivalentId)

SET @equivalentId = '44bdbd55-0985-4a80-9d18-be8af4993cdc'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('ticket inspector', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('gumby', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('ticket officer', @equivalentId)

SET @equivalentId = 'b99fa645-5dc2-4e79-90e7-f776b5b654b2'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Manicurist', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Nail Technician', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Hand technology', @equivalentId)

GO
