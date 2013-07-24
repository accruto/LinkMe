DECLARE @equivalentId UNIQUEIDENTIFIER

DELETE FROM dbo.EquivalentTerms

SET @equivalentId = '93f4bf93-c6ff-4537-92d2-288315601fd7'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Chief Executive Officer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('CEO', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('C.E.O.', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Managing Director', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('MD', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('M.D.', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Executive Director', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Exec Director', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('ED', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('E.D.', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('1ic', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('1.i.c.', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('1i.c.', @equivalentId)

SET @equivalentId = '3a420c08-9ea1-45f9-91fe-b35c3995a128'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Executive Chairman', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Exec Chairman', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('E.C.', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('EC', @equivalentId)

SET @equivalentId = '0e026dc3-b015-44b1-ac5e-9dd7ae1f3af9'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('General Manager', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('GM', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('G.M.', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Group General Manager', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('GGM', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('G.G.M.', @equivalentId)

SET @equivalentId = '7381b356-d9ad-4e00-bee1-abe6a792640d'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Chief Operating Officer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('COO', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('C.O.O.', @equivalentId)

SET @equivalentId = '8f4a18ac-56da-4f76-9254-442bf1948dfe'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('vp', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('v.p.', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('vice president', @equivalentId)

SET @equivalentId = 'dd94a20e-2b71-4126-84bd-c29cebcf6a85'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('senior vp', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('senior v.p.', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Senior Vice President', @equivalentId)

SET @equivalentId = 'ac242b9e-fd44-4af5-aac6-5de582e585b5'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('2ic', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('2.i.c.', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('2i.c.', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Second in charge', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('2 IC', @equivalentId)

SET @equivalentId = '63a43dcb-9427-4f26-a553-23f52b7f05b3'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Company Director', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Dir', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Dir.', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('director', @equivalentId)

SET @equivalentId = '27d8e518-2761-4a4d-9d24-81054e8c4f2c'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('non executive director', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('non exec director', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('non-exec director', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('non exec dir.', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('non-exec dir.', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('ned', @equivalentId)

SET @equivalentId = '74d3fb8a-6352-4d91-98a8-3fa9e8404cd6'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('company secretary', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('company sec.', @equivalentId)

SET @equivalentId = '9d22c538-cd82-476b-af3e-c574f81052b8'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Chairman', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('chairperson', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('chairwoman', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('chair man', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('chair person', @equivalentId)

SET @equivalentId = '4e1a1e59-e7c1-4870-837b-1a2e5a873c2a'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('CIO', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('C.I.O.', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Chief Information Officer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('general manager it', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('it director', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('director it', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('director information technology', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('head of it', @equivalentId)

SET @equivalentId = '8b769393-0e93-4179-bcc5-2975abb39bb4'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('manager', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('mgr', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('manger', @equivalentId)

SET @equivalentId = '1caba13f-6d58-48c6-9137-dce9567063c4'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('State Manager', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('statemanager', @equivalentId)

SET @equivalentId = 'e6f4ade9-622d-4708-82fc-2dc9ec84ed81'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Manager Governance', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Manager Accountability', @equivalentId)

SET @equivalentId = 'eb24f851-b6c8-4dad-84d3-6492069e7c43'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Program Director', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Director program', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('program manager', @equivalentId)

SET @equivalentId = 'd2a7d94a-b137-422d-b041-240577a1428f'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Assistant Manager', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('asst mgr', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('asst manager', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Assistant Manger', @equivalentId)

SET @equivalentId = 'b8cdefa1-0447-4e55-9753-56ad2b92de24'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Senior Supervisor', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Snr Surpervisor', @equivalentId)

SET @equivalentId = '5c1e83c6-8751-4016-a511-135cb7752be8'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Assistant Supervisor', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Asst Supervisor', @equivalentId)

SET @equivalentId = '18bac14d-5ed2-4c18-acf7-a35b29fa3cd3'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Team Leader', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Team lead', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Teamleader', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('supervisor', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('foreman', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('fore man', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('foreperson', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('fore person', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('team manager', @equivalentId)

SET @equivalentId = '01ed7af1-9e5c-4f8d-8903-d152ce4a2af2'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Project Manager', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Project Mgr', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('PM', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Project Manger', @equivalentId)

SET @equivalentId = 'd95a9471-9ba5-4ed5-afc1-6950fbde21d2'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('General Manager Project', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Project Director', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('project coordinator', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Senior Project Manager', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Senior Project director', @equivalentId)

SET @equivalentId = '5b036e5b-c320-4f0d-bdcb-b5e1d6d03fea'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('BPM', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('B.P.M.', @equivalentId)

SET @equivalentId = 'd313d119-6314-4504-93f1-33f4916b81ee'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Assistant Project Manager', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Asst Project Manager', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Asst Project Mgr', @equivalentId)

SET @equivalentId = 'b5752c4b-9023-40ed-83e9-caec5930d533'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Junior Project Manager', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Trainee Project Manager', @equivalentId)

SET @equivalentId = 'f87e79c5-83a3-4c84-b3d7-ae4febdd9512'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('inhouse consultant', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('in-house consultant', @equivalentId)

SET @equivalentId = '7941e87d-cc94-4654-adfc-da8315c113a2'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('BPR consultant', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('business process re-engineering', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('bpr', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('business process reengineering', @equivalentId)

SET @equivalentId = '1cfb82f6-6df9-4fc7-9768-51f666096566'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('DM', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('D.M.', @equivalentId)

SET @equivalentId = 'b6827f76-af85-4611-8bf2-e1843a64d5f4'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Manager Planning', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Manager- Planning', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('planning manager', @equivalentId)

SET @equivalentId = 'ab57c28f-74c4-4128-9667-db33e19f741e'

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

SET @equivalentId = '1e2a17da-2643-48bd-9b88-2c98da387fab'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Assistant C.F.O.', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Assistant CFO', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Asst C.F.O.', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Asst CFO', @equivalentId)

SET @equivalentId = 'e521707f-5201-4d9c-a9ca-26f8ed61040d'

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

SET @equivalentId = 'c01cdc52-8d71-4040-a067-aebccd3372e0'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Senior Financial Controller', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Sr. Financial Controller', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Sr Fin Controller', @equivalentId)

SET @equivalentId = '71dffa0c-605d-437e-ba79-fe4bab80dc56'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Assistant Financial Controller', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Asst Financial Controller', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Asst Fin Controller', @equivalentId)

SET @equivalentId = '38cb6cf7-428f-4fec-b7c7-8a5a8b69b961'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Regional Financial Controller', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Regional Finance Controller', @equivalentId)

SET @equivalentId = 'ceff9734-8f83-4729-9bc9-f46191dd53b3'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('National Financial Controller', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('National Finance Controller', @equivalentId)

SET @equivalentId = '1b2b5591-a9e4-491f-8b4f-d1d2b14e2611'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Accounting Manager', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Accounts Mgr', @equivalentId)

SET @equivalentId = '7d1164d5-501a-4bf1-af97-b827684d8049'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Senior Finance Accountant', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Sr Finance Accountant', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Sr. Finance Accountant', @equivalentId)

SET @equivalentId = '28e9c87f-ad93-4e50-96e7-e5e6b229452d'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Senior Financial Accountant', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Sr Financial Accountant', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Sr. Financial Accountant', @equivalentId)

SET @equivalentId = '9d40d228-e4c7-4eb3-b33c-78fd7b41d0da'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Administration Manager', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Admin Manager', @equivalentId)

SET @equivalentId = '983a99a0-0156-4b04-8ef4-5d6e9d9fbe64'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('National Administration Manager', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Assistant Administration Manager', @equivalentId)

SET @equivalentId = 'f886e16e-3f5d-4aac-b29e-d5a93d8efa90'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Senior Consultant', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Sr Consultant', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Sr. Consultant', @equivalentId)

SET @equivalentId = '1e04d85e-8c67-4805-b4ad-fb3c9bc2959b'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Principal Consultant', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Chief Consultant', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Managing Consultant', @equivalentId)

SET @equivalentId = '71942207-3300-48e0-8a54-6ca24472f5db'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Accountant', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Acc', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Accountan', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('acct', @equivalentId)

SET @equivalentId = '8195904b-bed7-4fb2-baa2-c10e85daccc6'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Financial accountant', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Fin Acc', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Finance Accountant', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Finance Acc', @equivalentId)

SET @equivalentId = 'dbf1fe9d-b453-4c2a-b7fe-3e5c84a071c4'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Corporate Accountant', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Corp Accountant', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Sr. Mg Accountant', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Contract Accountant', @equivalentId)

SET @equivalentId = '9795e32d-5d2f-449c-90d9-3442664e6b71'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Business Accountant', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Bus Acct', @equivalentId)

SET @equivalentId = '833bd06f-8a8e-43ea-9ee6-b56346d541fd'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Management accountant', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Management Acct', @equivalentId)

SET @equivalentId = '4bfa024a-0db5-49d2-bcaf-3acb8330b3ca'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Senior Management Accountant', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Sr Management Accountant', @equivalentId)

SET @equivalentId = '68da3397-af9c-42b3-8c83-1d185a7371c8'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Accounting', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Accountin', @equivalentId)

SET @equivalentId = '7893c5a9-465a-4d77-a33f-11d76eea0d37'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('cpa', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('c.p.a.', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('certified practising accountant', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('certified accountant', @equivalentId)

SET @equivalentId = '9d7b7590-8fe7-439d-9115-44d7e536bd21'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('ca', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('c.a.', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('chartered accountant', @equivalentId)

SET @equivalentId = 'e3dac40d-6f1f-40ab-b176-565a6b2206e5'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Temporary Accountant', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Temp Accountant', @equivalentId)

SET @equivalentId = '7bef7a9b-6473-44e1-acfa-9d984aa77fcf'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Sr. Accountant', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Sr Accountant', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Senior Accountant', @equivalentId)

SET @equivalentId = '5e13fffb-c993-41f5-ba1e-c08c87a724b8'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Financial/Management Accountant', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Assistant Management Accountant', @equivalentId)

SET @equivalentId = 'b715827d-e474-4aa6-aa88-c7819374e6c8'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Junior Accountant', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Jr Accountant', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Jr. Accounts', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Junior Accounts Clerk', @equivalentId)

SET @equivalentId = '513a3a52-fd30-403b-bf13-e00df031d8dd'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Assistant Accountant', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Asst Accountant', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Accounts Assistant', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Acct Asst', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Accounting Assistant', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Account Assistant', @equivalentId)

SET @equivalentId = 'e4a5a8fe-c9fd-407c-a3d9-ca5d544c1573'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Accounts Clerk', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Accounts Officer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Accounts Staff', @equivalentId)

SET @equivalentId = '72130aac-dd75-40e8-b613-e028330a531c'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Project Accountant', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Proj Acct', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Project Management Accountant', @equivalentId)

SET @equivalentId = '4c6a4c86-27d5-4405-8e84-2f9183bce2ee'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Auditor', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('auditing', @equivalentId)

SET @equivalentId = '8d63a868-561f-4f04-a86b-c9b4215b848b'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Audit Manager', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Audit Mgr', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('head of audit', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('audit director', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('audit partner', @equivalentId)

SET @equivalentId = 'cf112c92-7bb7-45d7-a501-def2636b7bf1'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Senior Auditor', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Sr Auditor', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Sr. Auditor', @equivalentId)

SET @equivalentId = '119eafd4-5d8f-4968-97c0-67792d5bdb83'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Internal Auditor', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('In house Auditor', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('In-house Auditor', @equivalentId)

SET @equivalentId = '446a0717-84e7-4381-a59d-a25dd873d309'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('External auditor', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Contract Auditor', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('financial auditor', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Revenue Auditor', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('State Auditor', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Statutory Accountant', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Senior Internal Auditor', @equivalentId)

SET @equivalentId = '4d877ca7-cfb6-4cfe-a801-f48cf083e89c'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Junior Auditor', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Jr Auditor', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Jr. Auditor', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Apprentice Auditor', @equivalentId)

SET @equivalentId = 'a062f185-8d82-47b2-8805-f47fc3139cf7'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Quality Auditor', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('QA Auditor', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Quality Assurance Auditor', @equivalentId)

SET @equivalentId = '8ebffb65-2c36-428f-893e-9a28f11a9055'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Operations Auditor', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Process Auditor', @equivalentId)

SET @equivalentId = '174621f7-ff6d-4761-81ea-26b9e0723627'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Stock Auditor', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Night Auditor', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('stock counter', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('stocktake auditor', @equivalentId)

SET @equivalentId = 'afa65b9d-3810-41cb-abe0-02e3cb338bfd'

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

SET @equivalentId = 'bb09bf3b-bb6f-4f0a-a312-cb5897fdd6bd'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Accounts Payable Manager', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Accounts Payable Supervisor', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('head of accounts payable', @equivalentId)

SET @equivalentId = '28314eda-8ad7-4c78-9bc5-3beddf703b25'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Accounts Payable', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Accts Payable', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('AP', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Sales Ledger', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Accounts Payable Officer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Accounts Payable Clerk', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Accounts Payable Coordinator', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Accounts Payable Co-ordinator', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Accounts Payable Co ordinator', @equivalentId)

SET @equivalentId = '7f9566bf-8858-4170-9594-66b5ac2809b0'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Accounts Payable Assistant', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Assistant Accounts Payable', @equivalentId)

SET @equivalentId = '1844cc47-edc4-4839-b265-25cc135f14c3'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Accounts Payable/Receivable', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Accounts payable specialist', @equivalentId)

SET @equivalentId = '606867a1-23ba-430d-96b0-1a9626ae5b0b'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Reconciliation Clerk', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Reconciliation Officer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Reconciliation Assistant', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Reconciliation Asst', @equivalentId)

SET @equivalentId = 'f9a6bbcd-52ab-4d38-8dc6-d0e666e5c7af'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Accounts Receivable Manager', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Accounts Receivable Supervisor', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Collections Supervisor', @equivalentId)

SET @equivalentId = '034bdc0b-3e22-4a4b-ad75-0dd8810d1f59'

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

SET @equivalentId = '56ba369b-f796-4402-be64-0c3944964a5c'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Mgr Credit Control', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Credit Control Manager', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Credit Control Mgr', @equivalentId)

SET @equivalentId = '577a51f5-8eca-4dcf-9a42-fc275c50f2e3'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Senior Credit Controller', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Sr Credit Controller', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Sr. Credit Controller', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Manager Credit Control', @equivalentId)

SET @equivalentId = '803a22ff-2d28-48d2-8d56-53027ae26ba5'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Junior Credit Controller', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Jr Credit Controller', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Jr. Credit Controller', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Jnr Credit Officer', @equivalentId)

SET @equivalentId = '22017938-6ef3-4392-85f3-c255b1a4711a'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Finance Analyst', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Financial Analyst', @equivalentId)

SET @equivalentId = '89d27fe0-9fa5-4d09-b861-d274bbd0dcf7'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Credit Analyst', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Credit Risk Analyst', @equivalentId)

SET @equivalentId = 'cbf16ea5-6700-4539-94bb-e15028329562'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Senior Finance Analyst', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Sr Finance Analyst', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Sr. Finance Analyst', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Senior Financial Analyst', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Sr Financial Analyst', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Sr. Financial Analyst', @equivalentId)

SET @equivalentId = '80e4c407-3f2a-42d2-be38-103fc5dc1738'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Senior Credit Analyst', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Sr Credit Analyst', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Sr. Credit Analyst', @equivalentId)

SET @equivalentId = '1e75ec2b-2062-4cba-95e6-bb6020e7a98b'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Junior Finance Analyst', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Jr Finance Analyst', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Jr. Finance Analyst', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Junior Financial Analyst', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Jr Financial Analyst', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Jr. Financial Analyst', @equivalentId)

SET @equivalentId = 'a70616c1-86bc-4d21-bdd4-993b6465d19d'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Junior Credit Analyst', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Jr Credit Analyst', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Jr. Credit Analyst', @equivalentId)

SET @equivalentId = '21934c45-a15f-4e38-b509-6bc0d66a7400'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Taxation Manager', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Taxation Mgr', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Tax Manager', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Tax Mgr', @equivalentId)

SET @equivalentId = 'f0b30c88-cd35-4afc-859d-3806cdf9903b'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('taxatiojn accountant', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Taxation Accountant', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('tax associate', @equivalentId)

SET @equivalentId = 'faf3119f-2412-4bf0-b09b-bc38b11bb898'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('tax accountant', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('tax analyst', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('tax partner', @equivalentId)

SET @equivalentId = 'fe60f7c5-e2d2-45d5-9613-d053bd3e7420'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Junior Tax Accountant', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Jr. Tax Accountant', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Jr Tax Accountant', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Junior Taxation Accountant', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Jr. Taxation Accountant', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Jr Taxation Accountant', @equivalentId)

SET @equivalentId = '23a6ccbd-4191-4f1a-aff0-7df483238775'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Taxation Advisor', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Tax Advisor', @equivalentId)

SET @equivalentId = 'ab57d1da-64b5-4814-942f-40b0fe1517ff'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Taxation analyst', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Tax Return Preparer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Tax Agent', @equivalentId)

SET @equivalentId = 'a39b2e4e-3e0a-4e11-bc84-1bde1daacca6'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Senior Tax Accountant', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Sr. Tax Accountant', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Sr Tax Accountant', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Senior Taxation Accountant', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Sr. Taxation Accountant', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Sr Taxation Accountant', @equivalentId)

SET @equivalentId = 'aa56c358-cb94-4e79-bfe9-cd7328246a2e'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Motor Claims', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Motor Claims Consultant', @equivalentId)

SET @equivalentId = 'dd17bf7e-9938-4576-a003-c37f2d34c701'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Injury Claims Consultant', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Workcover Claims', @equivalentId)

SET @equivalentId = '8a6323fb-7377-4c38-b194-b87e3d3ff541'

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

SET @equivalentId = 'eae612c0-345d-44f6-82f9-6f3ce0b40cc0'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Senior Purchasing Specialist', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Sr. Purchasing Specialist', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Sr Purchasing Specialist', @equivalentId)

SET @equivalentId = '23c0eb0c-e185-4756-9942-2ead3808a358'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Purchasing Manager', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Purchasing Mgr', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('procurement manager', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('head of procurement', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('procurement director', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('buying manager', @equivalentId)

SET @equivalentId = 'e1245c83-67f5-4cb4-9699-427dc408e158'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Purchasing Clerk', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Purchasing Assistant', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Purchasing', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Technical Purchasing Manager', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Purchasing Officer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Purchasing Administrator', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Purchasing Co-ordinator', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Purchasing Co ordinator', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Purchasing Coordinator', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Assistant Purchasing Officer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Purchasing Specialist', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('buying officer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('procurement', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('sourcing', @equivalentId)

SET @equivalentId = '6606c984-30c7-4073-830e-d1cf717fb792'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Buyer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Senior  Buyer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Sr Buyer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Sr. Buyer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Regional Buyer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('National Buyer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('State Buyer', @equivalentId)

SET @equivalentId = 'c130f8a5-c21d-44a8-9101-60457c84a0bd'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Buying Assistant', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Assistant Buyer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Junior Buyer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Jr Buyer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Jr. Buyer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Trainee Buyer', @equivalentId)

SET @equivalentId = '15eb60f4-c51a-416e-801e-e3a7320d29f7'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Mortgage Collections', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Mortgage Collection', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Mortgage Collections Officer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Mortgage Collections Clerk', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Mortgage Collection Clerk', @equivalentId)

SET @equivalentId = '26415744-1b2b-4d04-88e4-f4a00b981149'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Banking Manager', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Bank Manager', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('bank branch manager', @equivalentId)

SET @equivalentId = 'd9bce9bf-29cc-4644-a776-6c005506bb9d'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Branch Manager', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('branch head', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('branchmanager', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('branch supervisor', @equivalentId)

SET @equivalentId = '25bdfd95-19c7-47e3-ab15-e9009766aec1'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Business Banking Manager', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Manager Business Banking', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('banking services manager', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('business banker', @equivalentId)

SET @equivalentId = 'f6bb5e8d-f14c-4e49-95b9-4add5ab1002c'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Banking Clerk', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Banking Consultant', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Banking Officer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('bank clerk', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('teller', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('bank teller', @equivalentId)

SET @equivalentId = '058fff1b-9351-42d8-89a1-363d3ce05c35'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Insurance Manager', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Insurance Mgr', @equivalentId)

SET @equivalentId = 'c782561c-61e6-473d-be51-f36d70e825d1'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Insurance Consultant', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Insurance Assistant', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Insurance Asst', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Insurance Clerk', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Senior Insurance Administrator', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Sr. Insurance Administrator', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Sr Insurance Administrator', @equivalentId)

SET @equivalentId = '1d9cb2bc-66a8-4c4d-9b48-bc4c983661bd'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Jr. Insurance Administrator', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Jr Insurance Administrator', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Junior Insurance Administrator', @equivalentId)

SET @equivalentId = '711a4dd1-1950-498f-ae50-71e3b11029e8'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Insurance Broker', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('General Insurance Broker', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Insurance Solutions Consultant', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Senior Insurance Broker', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Sr. Insurance Broker', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Sr Insurance Broker', @equivalentId)

SET @equivalentId = '4dfe2ef7-f88e-407a-9860-e1005cc46cbf'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Junior Insurance Broker', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Jr. Insurance Broker', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Jr Insurance Broker', @equivalentId)

SET @equivalentId = 'b9f7f0c2-4f34-4083-9d98-1d301db724be'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Chief Actuary', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('actuarial manager', @equivalentId)

SET @equivalentId = '7b4d79ec-c9a4-4598-8c81-e0b5c0c013ff'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Workers Compensation Officer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Workers Compensation Administrator', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('WORKERS COMPENSATION CASE MANAGER', @equivalentId)

SET @equivalentId = '9e349378-a9f2-464b-8f8f-617ae189fff0'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Underwriting Manager', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('National Underwriting Manager', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Underwriting', @equivalentId)

SET @equivalentId = '0d6ec1fd-3d0e-42ad-a5b6-2eb4b4328331'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Underwriting Executive', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Insurance Underwriter', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Assistant Underwriter', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('under writer', @equivalentId)

SET @equivalentId = '5bec81b5-81ea-40ab-bf06-be6903d03bd7'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Chied Financial Planner', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Chief Financial Planner', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('head of financial planning', @equivalentId)

SET @equivalentId = '907d124b-ce79-45bf-bf81-54b54808bb26'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Financial planner', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Financial Planner Advisor', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('certified financial planner', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Financial Services Consultant', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Personal Financial Advisor', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('CFP', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('financial advisor', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('c f p', @equivalentId)

SET @equivalentId = '95869913-9baa-46cc-932a-ef409472d658'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Assistant Financial Planner', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Financial Planner Assistant', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Financial Planning Assistant', @equivalentId)

SET @equivalentId = '74096682-6b12-4282-adf7-5fc20921f315'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Senior financial planner', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Sr. financial planner', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Sr financial planner', @equivalentId)

SET @equivalentId = '9a4671fa-72b8-42fd-99d1-5712a1f7ad89'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Paraplanner', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Para planner', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Para-planner', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Para Planning', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Para-Planning', @equivalentId)

SET @equivalentId = 'e3877ef8-5450-4414-a285-b4bda440bff6'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Para-Financial Planner', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Para Financial Planner', @equivalentId)

SET @equivalentId = '78c7426a-0a30-4c23-8a7c-870056e0a82d'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Superannuation', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Super annuation', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Super-annuation', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('super fund', @equivalentId)

SET @equivalentId = 'd183224e-86cd-45ae-aa1a-5d60c5f58cd9'

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

SET @equivalentId = '36fb850f-03be-4b8a-9b41-130dddb9cde2'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('recruitment', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('onboarding', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('on boarding', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('on-boarding', @equivalentId)

SET @equivalentId = 'c11062b1-0145-4a7f-821f-0fc7d507bedb'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Recruitment Manager', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Onsite Recruitment Manager', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('National Recruitment Solutions Manager', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Regional Recruitment Manager', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('recruitment director', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('head of recruitment', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('recruitment dierctor', @equivalentId)

SET @equivalentId = 'eed8c709-cacb-4494-9641-d491a4f1a5da'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Senior Recruitment Consultant', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Sr Recruitment Consultant', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Sr. Recruitment Consultant', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Executive Recruitment Consultant', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Recruitment Consultant - Executive', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Recruitment consultant', @equivalentId)
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

SET @equivalentId = '8428c2f9-836a-4734-b8d0-db42407d0eb6'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Recruitment Administrator', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Recruitment Administration Officer', @equivalentId)

SET @equivalentId = '3decf01a-c461-4a22-98f5-50215a386030'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('HR', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('H.R.', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Human Resources', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Human Resource', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('people and culture', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('human capital', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('personnel', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('h r', @equivalentId)

SET @equivalentId = 'b85af73d-36b1-48c8-9aa1-e75bb562d8fb'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Senior HR Advisor', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Senior Human Resources Advisor', @equivalentId)

SET @equivalentId = 'b3fe17f7-41ad-4602-be37-941c41006621'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Senior Human Resources Consultant', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Senior Human Resources Officer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Senior HR Consultant', @equivalentId)

SET @equivalentId = '1b87c955-6404-449d-aa07-fa08b7544337'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Human Resources Analyst', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('HR ANALYST', @equivalentId)

SET @equivalentId = 'd2a0be28-5627-43eb-8ed4-4cab2bd7e6b1'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('HR Executive', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Human Resources Executive', @equivalentId)

SET @equivalentId = '7d26c525-39a2-4fdb-825a-11213b2fc579'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('HR Payroll', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('HR/Payroll', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('H.R. Payroll', @equivalentId)

SET @equivalentId = '181e9f25-f98c-4557-b2de-0ea0fe35ba29'

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

SET @equivalentId = '8ce3d80b-0589-4680-b0d6-a49b6e200996'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Temporary Payroll Officer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Temp Payroll', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Payroll Temp', @equivalentId)

SET @equivalentId = '7fce0e18-9b58-4a40-8bbc-f7ff28665702'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Senior Payroll Officer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Payroll Supervisor', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('payroll manager', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('head of payroll', @equivalentId)

SET @equivalentId = '49538f56-6e36-4b54-ac33-49737833819b'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('National Retail Manager', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Manager - Retail', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Retail Manager', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Retail Business Manager', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Retail Operations Manager', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Retail Operations', @equivalentId)

SET @equivalentId = '56b7971c-dd9d-4f34-91d0-d95bcc68d21a'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Store Manager', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Shop Manager', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Store Supervisor', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Store Manger', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Weekend Store Manager', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Retail Store Manager', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('retail outlet manager', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('retail outlet supervisor', @equivalentId)

SET @equivalentId = '2699825f-af49-497d-9ed4-88c7333c0520'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Retail Supervisor', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Retail Team Leader', @equivalentId)

SET @equivalentId = '24539cf5-1c9d-4472-a8e6-fc8128810082'

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

SET @equivalentId = '138dc1cb-033b-49be-804d-efd1ec4525d3'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('LinkMe', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('owner of this intellectual property', @equivalentId)

SET @equivalentId = 'd0c5b7d7-c745-4915-98c4-62a4f3b067c2'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Hotel Manager', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Hotel Duty Manager', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Hotel general manager', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('hotel supervisor', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('pub manager', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('pub duty manager', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('hotel operations manager', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('hotel night manager', @equivalentId)

SET @equivalentId = 'a54f18ab-feda-40a6-96a8-6b00ea4bc916'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Floor Manager', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Floor Supervisor', @equivalentId)

SET @equivalentId = 'a59ae5ba-2efd-406f-b8a1-2f76f2986800'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Bar Manager', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Bar Supervisor', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('bar owner', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('head barman', @equivalentId)

SET @equivalentId = '20251b97-d3ce-49a5-bb19-6f596ba08979'

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

SET @equivalentId = '7dd4b4f1-179e-488c-bd3c-a5af454d3bb6'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('barrista', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('barista', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('coffee maker', @equivalentId)

SET @equivalentId = '6956f260-3a3a-4f9a-a078-89b929e0a874'

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

SET @equivalentId = '3b9a177c-6a63-4e53-acf4-5f79f15114c7'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Gaming Manager', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Gaming Mgr', @equivalentId)

SET @equivalentId = '9f50740d-43f9-4567-9240-b4cdaea51814'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Gaming Attendant', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Gaming staff', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Bar/Gaming Attendant', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Gaming Bar Attendant', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Gaming/Bar Attendant', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('gaming machine attendant', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('room attendant', @equivalentId)

SET @equivalentId = '6224b5f5-7781-409b-bc4b-7a6f21c4060f'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Head Waiter', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Head Waitress', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Wait Supervisor', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Maitre De', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Senior Waiter', @equivalentId)

SET @equivalentId = '58a093ba-6c12-4b56-84b9-de0588c71d94'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Waiter', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Waitress', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Wait Staff', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Waitress Customer service', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Waitress/ Kitchen Hand', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Waitress/kitchenhand', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Waitress / Kitchen Hand', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Waiter/kitchen hand', @equivalentId)

SET @equivalentId = '44968e91-f2e0-44b0-a71d-c348349367a8'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Kitchen Manager', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Head Cook', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Head Chef', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Chef Manager', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Head Pastry Chef', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Senior Chef', @equivalentId)

SET @equivalentId = '17df0457-b326-4c68-95a3-b7a6ebc490e8'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Second Chef', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('2nd Chef', @equivalentId)

SET @equivalentId = '8b2793e2-5420-4c4d-bfee-59eb12afbceb'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Chef De Partie', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Sous Chef', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Executive Chef', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Commis Chef', @equivalentId)

SET @equivalentId = '1560dae4-7efc-4ba5-8636-0ca9a2d9aa02'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Assistant Chef', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Chefs Assistant', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Chef assistant', @equivalentId)

SET @equivalentId = '42fbec4d-92ba-4f3a-84f3-0b4ca41575e4'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('chef', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('che', @equivalentId)

SET @equivalentId = '7a80cf8f-f05d-4ca4-87d4-fd5140cdac14'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Executive Pastry Chef', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Pastry Chef', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('pastry cook', @equivalentId)

SET @equivalentId = '90b4395f-64aa-4f62-a966-20dfd49618ae'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Apprentice Chef', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('2nd year Apprentice Chef', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('1st Year Apprentice Chef', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('4th Year Apprentice Chef', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Trainee chef', @equivalentId)

SET @equivalentId = '5be23597-241f-4c1e-84a6-a2e0d3df3d2a'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Cook/Chef', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Relief Chef', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Kitchen Hand/Cook', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Cook/Kitchen Hand', @equivalentId)

SET @equivalentId = 'ef96a4e8-0f94-4bb7-b2c8-2152af064fe9'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('kitchen hand', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('kitchenhand', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Kitchen-hand', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Kitchen Worker', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Kitchen Hand/Waiter', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Kitchen hand/waitress', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('casual kitchen hand', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Kitchen hand Customer service', @equivalentId)

SET @equivalentId = 'ba3bf590-c367-4152-b1c3-ed2e1817a993'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Call Centre Manager', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Call Centre Team Manager', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Call Centre Supervisor', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Telemarketing Manager', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Telemarketing Team Leader', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('contact centre manager', @equivalentId)

SET @equivalentId = '7461978a-d45a-4ce8-8b89-3b71b18115e3'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Customer Service Manager', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Customer Services Manager', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Customer Service Supervisor', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Customer Service Team Leader', @equivalentId)

SET @equivalentId = '2c14c410-d7ff-484a-ba3b-78a0c7649979'

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

SET @equivalentId = '0028cada-6d68-413a-98a4-36c01b0079a7'

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

SET @equivalentId = '13313464-db3f-4069-befe-f768002b700e'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Junior Receptionist', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Jr Receptionist', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Trainee Receptionist', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Assistant Receptionist', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Reception Assistant', @equivalentId)

SET @equivalentId = '2e9b6a28-38a0-4a8c-ab19-41b47eff549e'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Junior Secretary', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Jr Secretary', @equivalentId)

SET @equivalentId = '86a248e9-c8ca-44eb-815d-6fb0412801c3'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Front Office Receptionist', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Receptionist Front Office', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Front Desk Receptionist', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Frontline Receptionist/Office All-rounder', @equivalentId)

SET @equivalentId = 'c36285cd-8dbd-4951-8e63-95331733ab0e'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Front Office Manager', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Front Office Supervisor', @equivalentId)

SET @equivalentId = 'dd4ad0bf-8d89-427f-9a31-3ee396b175af'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Office manager', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Office Supervisor', @equivalentId)

SET @equivalentId = '8151fc69-1223-4a49-b699-d5e234c5f691'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Office Assistant', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Office Asst', @equivalentId)

SET @equivalentId = '57b5d914-3d45-4485-a619-73854bde2f92'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Bilingual Receptionist', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('bi lingual receptionist', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('bi-lingual receptionist', @equivalentId)

SET @equivalentId = 'e7ddae0a-8cf0-4062-804b-4578b4922a6f'

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

SET @equivalentId = '1096c070-e660-4991-bdcc-0e9eacdff279'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Receptionist/Property Management', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Receptionist / Assistant Property Manager', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('property manager', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('facility manager', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('facilities manager', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('strata manager', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('portfolio property manager', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('rental manager', @equivalentId)

SET @equivalentId = '328a92b8-d93d-46db-af60-15e16196f3e6'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Temporary Receptionist/Secretary', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Temporary Receptionist', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Casual Receptionist', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Relief Receptionist', @equivalentId)

SET @equivalentId = '80694a3b-e5d6-4f3c-aeea-8dcad45e7143'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Office Manager/Personal Assistant', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Office Manager/PA', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Office Manager / Executive Assistant', @equivalentId)

SET @equivalentId = '44973014-672f-4757-9046-df537fe3287a'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Administration Supervisor', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Admin Supervisor', @equivalentId)

SET @equivalentId = '4c81c135-6cd9-4433-8742-3ea98e3838fb'

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

SET @equivalentId = 'd1ad700b-cdec-4f8f-8d6e-06fb5143a86a'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('admin', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('administration', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('administrative', @equivalentId)

SET @equivalentId = 'd812f144-76c7-466d-a7c6-819bca7a73c1'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Administrative Support Officer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Administrative Support', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Administration Support', @equivalentId)

SET @equivalentId = 'a246cb73-3e9c-441d-a882-95052f26e58b'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Temporary Clerical Assistant', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Temporary Administrative Officer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Relieving Administrative Officer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Temporary Admin Officer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Temp Admin Officer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Temp Administration Officer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('temporary administration assistant', @equivalentId)

SET @equivalentId = '7a08de5f-67e3-4d8d-9c1f-c014fcde7f62'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Junior Clerical Assistant', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Clerical Trainee', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Clerical Traineeship', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Trainee Clerical Assistant', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Office Junior', @equivalentId)

SET @equivalentId = '5f014675-3d80-407b-aed6-33ba739a5631'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Admin Trainee', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Office Administration Trainee', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Junior office administration', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Administration Trainee', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Junior Administration Assistant', @equivalentId)

SET @equivalentId = 'e49b6c5b-1c86-432f-8137-7c4983dc581b'

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

SET @equivalentId = 'a4c766d0-8c85-4837-aed6-68a42a4fa182'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Engineering Manager', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Senior Engineer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Chief Engineer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('director of engineering', @equivalentId)

SET @equivalentId = '0736d07c-31c8-4edd-b6b4-24e9103212e9'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('engineering project manager', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Senior Project Engineer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Project Engineer', @equivalentId)

SET @equivalentId = 'f5e670fa-a5af-448f-b2d9-3143fa66e0fe'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Site Engineer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Site Supervisor', @equivalentId)

SET @equivalentId = 'cf45ae2d-5c37-4e73-88a2-073d57167ae4'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Assistant Engineer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Engineering Assistant', @equivalentId)

SET @equivalentId = 'ed259244-10e5-45b0-93b6-da199f7ea3b5'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Junior Engineer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Student Engineer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('graduate engineer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Undergraduate Engineer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('DESG Student Engineer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('trainee engineer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('engineering trainee', @equivalentId)

SET @equivalentId = '0ed305c2-d159-4ebc-99a6-a3e9d8f57f1b'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Field Engineer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('FIELD SERVICE ENGINEER', @equivalentId)

SET @equivalentId = '7c80b62c-1443-41e4-a14b-3e9b1aad6a00'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Customer Engineer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('customer service engineer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('service engineer', @equivalentId)

SET @equivalentId = 'dbec69c6-e9ee-45a3-a767-ba40748e8014'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Engineering Consultant', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Consultant engineer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Consulting Engineer', @equivalentId)

SET @equivalentId = '14ad402f-d2b5-49b7-b73a-f83adae80231'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Customer Support Engineer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Support Engineer', @equivalentId)

SET @equivalentId = 'e5aead50-45ef-4e0b-b36b-72572b6074fe'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Aircraft Engineer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Aircraft Mechanic', @equivalentId)

SET @equivalentId = '744de471-86af-4b23-970f-301047b52140'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Design Engineer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('engineering designer', @equivalentId)

SET @equivalentId = 'f88d8323-a6e8-43cf-8837-83c700902b45'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Electrical Design Engineer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('electrical engineer', @equivalentId)

SET @equivalentId = 'a4577c99-507b-4e98-819a-2e13d782809d'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Mechanical Design Engineer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Mechanical Designer', @equivalentId)

SET @equivalentId = '35475ccb-aaef-439e-a630-db135fcabf91'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Planning Engineer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('project planning engineer', @equivalentId)

SET @equivalentId = '06d5bab1-237e-4f73-a3f0-9a0e8e286946'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Electronics Engineer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Electronic Engineer', @equivalentId)

SET @equivalentId = 'b0db7489-1248-4f99-b9b4-15614d793068'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Estimator', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('quantity surveyor', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('qs', @equivalentId)

SET @equivalentId = '6924021d-f2fc-45c0-be99-5f30ea9c78e9'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Sound Engineer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Audio Engineer', @equivalentId)

SET @equivalentId = 'aec876d0-6437-4f92-8c3c-ba8399c994a2'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Quality Assurance Engineer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('qa engineer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('quality engineer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('reliability engineer', @equivalentId)

SET @equivalentId = 'ed3b87ac-b5d2-4bfd-9607-c7d6f11d1fba'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Environmental Engineer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Environmental Officer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Environmental Health Officer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('sustainability engineer', @equivalentId)

SET @equivalentId = 'df9cf1b4-88ba-4bc3-a02e-f014bab1e3a1'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('hydraulic fitter', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('hydraulic mechanic', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('hydraulic fittersales', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('hydraulic technician', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('hydraulic valve technican', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('hydraulic hose fitter', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('hose technican', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('industrial hydraulic technican', @equivalentId)

SET @equivalentId = 'a92c0116-443d-4bab-9ea0-302f67b771df'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('mechanical engineer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('mech eng', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Maintenance Engineer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Plant Engineer', @equivalentId)

SET @equivalentId = '28f2ce10-3f6c-45f7-a97e-73ec52a62db1'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Mining Manager', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Quarry Manager', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Quarry Operations', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('project manager mining', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('mining project manager', @equivalentId)

SET @equivalentId = '60c2a610-0ed3-4680-a6cb-df283356d428'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('mining engineer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Mine Geologist', @equivalentId)

SET @equivalentId = 'd0e9759d-a465-461a-b926-8780d6b2d42d'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Civil Engineer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('civilengineer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Senior Civil Engineer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Sr Civil Engineer', @equivalentId)

SET @equivalentId = 'c2738273-989a-4040-8865-9e8065cf84b4'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Trainee Civil Engineer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Junior Civil Engineer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Graduate Civil Engineer', @equivalentId)

SET @equivalentId = '0b6f71ef-f8e4-4472-b317-9683eb493396'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Process Engineer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Production Engineer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Manufacturing Engineer', @equivalentId)

SET @equivalentId = '10d42c9e-9bec-4e08-a730-f2db12d22163'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Architectural Design', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Project Architect', @equivalentId)

SET @equivalentId = '1909e800-239e-43f5-b509-1aad18bf7178'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Senior Architect', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Senior Design Architect', @equivalentId)

SET @equivalentId = '1ef7829e-5703-4acb-8c11-0ce2f4468bc5'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Assistant Architect', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Architectural Assistant', @equivalentId)

SET @equivalentId = 'cd89bf34-15f0-4c03-b05b-c7ae3e8a6b9c'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Graduate Architect', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Architectural Trainee', @equivalentId)

SET @equivalentId = '586636c3-a0fd-4821-bf60-58ec16bdb1f5'

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

SET @equivalentId = '24414bfe-27d0-4db9-9514-58780cc76d3f'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('computer aided design', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Autocad', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Auto CAD', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('cad', @equivalentId)

SET @equivalentId = '29b00aea-f812-4bcd-9881-49c48507b0a5'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Senior Draftsman', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Senior Design Structural Draftsman', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('lead draftsman', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('drafting manager', @equivalentId)

SET @equivalentId = 'c843c2a2-2db6-4b30-b5ef-310bccecbcdf'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Labourer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('manual labourer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('general labourer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('skilled labourer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('construction labourer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Trades Assistant', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Trade Assistant', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('ta', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('general trades assistant', @equivalentId)

SET @equivalentId = '99251fde-052d-43de-ae18-19a49ffccf66'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('tradesman', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('tradesperson', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('trades man', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('trades person', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('tradie', @equivalentId)

SET @equivalentId = 'd94635b8-2da7-4058-9281-7307ef8b5b36'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('plasterer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('solid plasterer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('fibrous plasterer', @equivalentId)

SET @equivalentId = 'eab633f3-e3a4-4ea3-b9b7-728cdc806edb'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('scaffolder', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('scaffolding coordinator', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('scaffolding labourer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('scaffolding', @equivalentId)

SET @equivalentId = 'e46f4380-20ad-4554-a265-8bef60d5a5d4'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Mechanic Manager', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Workshop Manager', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('MECHANICAL SUPERVISOR', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Mechanical Manager', @equivalentId)

SET @equivalentId = 'c54f24f5-85ce-4a97-946e-3ce854b7dc44'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('boilermaker', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Boilermaker/Welder', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Welder', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('boiler maker', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('welding', @equivalentId)

SET @equivalentId = '93784aa4-c21d-4929-aa98-137c98d24f60'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Fitter', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Fitter/Welder', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Mechanical Fitter', @equivalentId)

SET @equivalentId = 'bc2fb809-859a-482a-98d6-190fe62c73b7'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('hdpe', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('High-density polyethylene', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Hi density polyethylene', @equivalentId)

SET @equivalentId = '9af714ea-6b19-4047-9733-66d2a0336991'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('pvc', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Polyvinylechloride', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Poly vinyle chloride', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Poly vinyl chloride', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Polyvinylchloride', @equivalentId)

SET @equivalentId = '8cae1f7d-4fa9-44da-87d3-72074bfd4e72'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('mechanic', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Service Mechanic', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Motor Mechanic', @equivalentId)

SET @equivalentId = '52012981-b0b5-410d-b019-f69dada7059c'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Assistant mechanic', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Trainee mechanic', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Apprentice Mechanic', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Apprentice Motor Mechanic', @equivalentId)

SET @equivalentId = '78eb428f-8521-4149-9590-369a4ba7043d'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Electrical mechanic', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Maintenance Electrician', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Electrical Fitter', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('electrician', @equivalentId)

SET @equivalentId = '4ff4ad01-67c0-43f3-955b-5afba7f2399f'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Apprentice Electrical Mechanic', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('trainee Electrical Mechanic', @equivalentId)

SET @equivalentId = '8a845dd2-779d-4bde-898a-c7c8f8baa9d4'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Diesel Mechanic', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('diesel motor mechanic', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('diesel fitter', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('diesel engine mechanic', @equivalentId)

SET @equivalentId = '45101aa6-2dd7-478d-a0e4-0d582645a9a3'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Electrical Supervisor', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Leading Hand Electrician', @equivalentId)

SET @equivalentId = 'a6dc820e-9c07-461f-ad53-7cd0940678cb'

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

SET @equivalentId = '18a9bfcb-db78-4763-9459-c9778790b4de'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('plant manager', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('National Plant Manager', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('manufacturing manager', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Production Supervisor', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Production Manager', @equivalentId)

SET @equivalentId = '6e485a80-c949-4677-a26f-0dbcf815f3ef'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Operations Coordinator', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('operations co ordinator', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('operations co-ordinator', @equivalentId)

SET @equivalentId = '8ee82039-6b85-4d96-a2d7-77c12e402af1'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Operations Analyst', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Customer Operations Specialist', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Operations Support Officer', @equivalentId)

SET @equivalentId = '82c20170-dc17-434e-80a5-8fc5ac6cbf1c'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Production Coordinator', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Production Co-Ordinator', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Production Co Ordinator', @equivalentId)

SET @equivalentId = '83642145-aa31-43fd-b260-6a156ce13932'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Production Team Leader', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Production team lead', @equivalentId)

SET @equivalentId = 'ded47f55-fd09-4dc0-8078-7de21ca97608'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('PRODUCTION SHIFT MANAGER', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('shift manager production', @equivalentId)

SET @equivalentId = '13d24c7b-9af3-4f97-b854-3889f69bfbac'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('crew member', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('crewmember', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('team member', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('teammember', @equivalentId)

SET @equivalentId = 'f1576a21-e73c-456d-a934-d50763936504'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Assistant Production Manager', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Asst production manager', @equivalentId)

SET @equivalentId = '0b936f06-19ee-43a5-a631-ffb2076aba94'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('under manager', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('undermanager', @equivalentId)

SET @equivalentId = '4538da3c-e2bc-4aa6-8533-b7ea185f9eb4'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Production Planner', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Production Scheduler', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Production Analyst', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Production Controller', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Production Assistant', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Production Worker', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('mrp controller', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('supply chain planner', @equivalentId)

SET @equivalentId = '7cfd2394-9137-4958-b872-9b1c323950e9'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Production Operator', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('PRODUCTION CLERK', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Production / Machine Operator', @equivalentId)

SET @equivalentId = '022e6b29-dfb1-4d92-8511-1176d59ba6e1'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Machine Operator', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('machinist', @equivalentId)

SET @equivalentId = 'f7c44f17-8b56-4083-929e-82fd8c884a07'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('process worker', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Process Worker/Packer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('food production worker', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Meat Packer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('process operator', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('general hand', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('generalhand', @equivalentId)

SET @equivalentId = '7f68c877-e08b-4c8c-b5ff-517e53396f38'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Casual Process worker', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('temp process worker', @equivalentId)

SET @equivalentId = 'e0c2d698-e795-485b-a5ed-7ee8449eb368'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Warehouse Supervisor', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Warehouse manager', @equivalentId)

SET @equivalentId = '0c9fd12b-d897-4793-879e-a84d5aca73e1'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Warehouse Assistant', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Warehouse', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Warehouse worker', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Warehouse Clerk', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Warehouse Person', @equivalentId)

SET @equivalentId = '08f585c0-bdca-4af4-8bd6-b39489f44488'

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

SET @equivalentId = 'ef74317b-eb80-4151-b5a8-149f8eb73fbf'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Maintenance Supervisor', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Maintenance manager', @equivalentId)

SET @equivalentId = '0244547f-c396-47aa-87d6-22475a7815e4'

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

SET @equivalentId = '1951a60b-a54d-42f4-ad30-5b8fcbdb059f'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('IT Manager', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('I.T. Manager', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('National IT Manager', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Group IT Manager', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('IT Project Manager', @equivalentId)

SET @equivalentId = 'b53344f6-e47a-4747-bb93-65079be2e552'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Infrastructure Project Manager', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Network Manager', @equivalentId)

SET @equivalentId = '6d566ba1-f30a-4f16-85a4-f212611ce5da'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Assistant IT Manager', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Asst IT Manager', @equivalentId)

SET @equivalentId = 'baa3d795-d622-4562-bbec-f40ffde72162'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('IT Operations Manager', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Computer Operations Manager', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('IT operations', @equivalentId)

SET @equivalentId = 'b2790afe-33ff-4411-94c7-9e697562c114'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Software Architect', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Systems Architect', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Senior Analyst Programmer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Senior Software Architect', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Senior Software Design Engineer', @equivalentId)

SET @equivalentId = 'd3b2e701-6890-49ea-a4e1-42d833e0e22b'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Senior Developer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Lead Developer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Senior Programmer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Senior Software Engineer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Lead Software Engineer', @equivalentId)

SET @equivalentId = '5f2ba7ff-6515-484d-a0e5-d9af5e0a528b'

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

SET @equivalentId = '935a5e4b-1c34-4dd4-8552-c642fa29a3f4'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Junior Programmer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Junior Developer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Trainee Programmer', @equivalentId)

SET @equivalentId = 'd2bf13e2-1f35-4432-a022-9b52d58434db'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Designer/Developer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Interface Developer', @equivalentId)

SET @equivalentId = '07c2334b-7fd2-43e0-a61c-4397835fa5a6'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Systems Analyst/Programmer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Programmer/Analyst', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Programmer Analyst', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Analyst/Programmer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Senior Analyst/Programmer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Systems Analyst', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Senior Systems Analyst', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('System Analyst', @equivalentId)

SET @equivalentId = 'f1bf5e3c-fc32-455e-8ea3-57f9e1cd42bf'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('System Engineer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Systems Engineer', @equivalentId)

SET @equivalentId = 'f6a0a210-78c8-4979-be74-66214cd9f872'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Test Team Leader', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('TEST MANAGER', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Test Coordinator', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Testing Manager', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Senior Test Analyst', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Sr Test Analyst', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Senior Test Engineer', @equivalentId)

SET @equivalentId = '159066f4-61e1-4f6a-aded-35404d7c7160'

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

SET @equivalentId = '2152d173-2afa-4dbb-8651-fdabc67b2906'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('IT Administrator', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('I.T. Administrator', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('IT Admin', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('I.T. Admin', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('IT Systems Officer', @equivalentId)

SET @equivalentId = '2d3ed625-f42f-4708-a3e9-2f6fadf71ee0'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('IT Officer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('IT Assistant', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('I.T. Officer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('I.T. Assistant', @equivalentId)

SET @equivalentId = 'c0dbc375-51e6-48a0-9d05-b31dc91d6d19'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('IT Specialist', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('it guru', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('it expert', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('information technology specialist', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('information technology expert', @equivalentId)

SET @equivalentId = '24d94173-0561-46b1-913f-d056d34efd95'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('IT Trainer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('IT Tutor', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('computer tutor', @equivalentId)

SET @equivalentId = '289c598c-0121-4df2-bac7-ca47bc563871'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Network architect', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Network Technician', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Chief Network Architect', @equivalentId)

SET @equivalentId = 'af8463ca-73d1-49a6-a820-72f463d54dc1'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Network Systems Administrator', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Network Administrator', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Network Admin', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Systems administrator', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Network Administration', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Network', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('System administrator', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Senior Network Administrator', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Sr Network Administrator', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Assistant Systems Administrator', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('sysadmin', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('sys admin', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('system administration', @equivalentId)

SET @equivalentId = 'ca295397-e508-43d8-9317-2e21e0751afd'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Network Engineer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Network support engineer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Network Support', @equivalentId)

SET @equivalentId = '847e853e-db07-4feb-8895-a32aa4e0257d'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Network Consultant', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Network Support Officer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Network Specialist', @equivalentId)

SET @equivalentId = '49fd755d-4bad-4a1c-9f84-2e76fee4e916'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Database Administrator', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('DBA', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('D.B.A.', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('data base administrator', @equivalentId)

SET @equivalentId = 'c7dade6a-a8f9-4061-a532-d7996fba6f7c'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('IT consultant', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('I.T. Consultant', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Systems consultant', @equivalentId)

SET @equivalentId = '730c4771-c091-47af-aac5-2faedcfc7130'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('java developer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('java', @equivalentId)

SET @equivalentId = '0f49aafa-34ac-4b4e-9f9f-52dd1d719550'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('IT Contractor', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('IT Contracts', @equivalentId)

SET @equivalentId = '32390a8c-3b59-41fa-bd3e-b585042908b1'

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

SET @equivalentId = '55b4478e-c23e-417d-89d4-253843d13a91'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Intranet Developer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Intranet Application Developer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Intranet Software Developer', @equivalentId)

SET @equivalentId = '46ac4a9e-db38-40ef-b497-f40ee240d563'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Website Developer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Web Application Developer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Web Designer/Developer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Web Site Developer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Freelance Web Developer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Senior Web Developer', @equivalentId)

SET @equivalentId = '0a9d7792-b81a-4869-b597-72aca01a9fe0'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('IT Support Engineer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Computer Engineer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Desktop Support Engineer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('IT Support Technician', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Computer Technician', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('IT Technician', @equivalentId)

SET @equivalentId = '301f7bd1-a0a9-4ea0-9dc7-f62d34fef306'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('ColdFusion', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('cold fusion', @equivalentId)

SET @equivalentId = 'bd06b5c5-4fd7-48be-bc0e-38407b8c3eb0'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('vb.net', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('visual basic.net', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('visual basic dot net', @equivalentId)

SET @equivalentId = '4c221f1f-a87f-4b15-8bed-91ea48cdc99e'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('VB', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Visual basic', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('visualbasic', @equivalentId)

SET @equivalentId = 'b6509f4e-cb19-4a8a-91a4-a4821dc8b1d5'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Oracle Database Administrator', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Oracle DBA', @equivalentId)

SET @equivalentId = 'e25e2191-48be-4e91-a70e-7ebae587e86c'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('UNIX Systems Administrator', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Senior Unix Systems Administrator', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Unix Consultant', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Unix Consoultant', @equivalentId)

SET @equivalentId = 'd88695c8-85b9-463c-ac45-25ed17668540'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Telco', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Telecoms', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Telecom', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Telecomms', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Telecommunication', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Telecommunications', @equivalentId)

SET @equivalentId = '09dfe7c9-56be-4602-ac78-40928f2fc574'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('PABX', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Private Automatic Branch eXchange.', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('pbx', @equivalentId)

SET @equivalentId = '4e32559f-fce1-4ae9-a522-5c7c8b841e33'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Solutions Architect', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Solution Architect', @equivalentId)

SET @equivalentId = '355ab36f-6801-4595-bc17-acdc48d148c8'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Graphics/Multimedia', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('multimedia', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('multi media', @equivalentId)

SET @equivalentId = '4cac24ed-afbe-4351-9fc8-0d04fb4c3b33'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Graphic designer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Graphic Design', @equivalentId)

SET @equivalentId = 'df9f56ec-80f7-4380-a8f9-dafef74506a6'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('freelance graphic designer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Freelance Graphic Design', @equivalentId)

SET @equivalentId = 'aaedaae7-1b25-453c-8eec-965542f7ea00'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Junior Graphic Designer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Trainee Graphic Designer', @equivalentId)

SET @equivalentId = 'd07b12d4-3612-40a1-adf0-a340fe37b4a6'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Web / Graphic Designer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Web/Graphic Designer', @equivalentId)

SET @equivalentId = 'f281c2e9-7c19-4c12-8522-6d115111a331'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('general counsel', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('head counsel', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('head lawyer', @equivalentId)

SET @equivalentId = '73d7dba9-1646-49ce-a455-9ae71dca6da6'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Lawyer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Solicitor', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Legal Counsel', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('cousel', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Corporate Solicitor', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Litigation Solicitor', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Law Graduate', @equivalentId)

SET @equivalentId = '3e446928-8f16-49ec-abc1-8153ed53cd49'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Legal Secretary', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Legal Secretary/Receptionist', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Legal Secretary/Personal Assistant', @equivalentId)

SET @equivalentId = 'b5828da6-f28a-41fe-b098-96c79e35f027'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Corporate legal secretary', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Commercial legal secretary', @equivalentId)

SET @equivalentId = '16d80694-f011-48fe-8270-db942e14c1e1'

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

SET @equivalentId = '87e634e4-8372-4ff2-b977-00f02af5a62f'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Registered Nurse', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Registered nurse - Level', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Practice Nurse', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Staff Nurse', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('State Enrolled Nurse', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('ren', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('sen', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('rn', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('registered enrolled nurse', @equivalentId)

SET @equivalentId = '8a411023-362b-40a7-90ae-e6bd2c24d76d'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Clinical Nurse Specialist', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Clinical Nurse', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Clinical Nurse Consultant', @equivalentId)

SET @equivalentId = '3de246c5-379d-4549-b840-4a74d8dda5a3'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('orderly', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('nursing attendant', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('hospital orderly', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('medical orderly', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('shift orderly', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('patient care assistant', @equivalentId)

SET @equivalentId = '914bfea2-9a57-4c80-8326-c0e6fa4a9597'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Clinical Nurse Educator', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('General Nurse Training', @equivalentId)

SET @equivalentId = '1e86e1d9-1e70-4ffc-ad0c-908673dceb60'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Nursing Assistant', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('assistant nurse', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Nurse Aid', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('ain', @equivalentId)

SET @equivalentId = '72fdb988-3831-44a8-9453-1560d5661782'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('trainee nurse', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Student Nurse', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Trainee Enrolled Nurse', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Student Enrolled Nurse', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('enrolled nurse', @equivalentId)

SET @equivalentId = 'd29abe78-c07d-44cc-a2f5-5ab89bd691fc'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Nurseryhand', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('nursery hand', @equivalentId)

SET @equivalentId = '7b949be5-482f-4c79-b11e-7a973d0527b7'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Medical Receptionist', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Medical Secretary', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Senior Medical Receptionist', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Casual Medical Receptionist', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('medical centre receptionist', @equivalentId)

SET @equivalentId = 'de213e03-d9c8-471b-a838-69c4af0594ab'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Dental Nurse', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Senior Dental Nurse', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Dental Assistant/Receptionist', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('dental assistant', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('dental hygienist', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('dental technician', @equivalentId)

SET @equivalentId = 'e07fb24c-d208-4e8c-a973-773c29862d1b'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Junior Dental Nurse', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Trainee Dental Nurse', @equivalentId)

SET @equivalentId = 'd36d650a-24c8-4f91-852f-b8068e2195f2'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Veterinary Nurse', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Vet Nurse', @equivalentId)

SET @equivalentId = '2bf1e175-531a-4044-8864-767bfe220e6e'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('vet', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('vetinary', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('vetinarian', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('veterinarian', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('veterinary', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('veterinary practitioner', @equivalentId)

SET @equivalentId = 'a6375b99-4856-46ef-b48f-39befa146048'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('gp', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('general practisioner', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('general practitioner', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('medical officer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('vmo', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('registrar', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('physician', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('general physician', @equivalentId)

SET @equivalentId = '3a24970b-66ff-4f75-9656-2f3a45dabc51'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('obstetric', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('gynaecology', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('gynaecologist', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('obstetrician', @equivalentId)

SET @equivalentId = '76468376-5840-4dc6-b768-4d597dc5e547'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('ward clerk', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('ward officer', @equivalentId)

SET @equivalentId = '5ba14402-0d68-4aad-8b88-fb0b7fc18331'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('nanny', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('nannies', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('aupair', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('au-pair', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('au pair', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('childcare worker', @equivalentId)

SET @equivalentId = '61ca005a-a693-487b-b416-3a315887551b'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('business development manager', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('business development', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('bdm', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('b.d.m.', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('salesman', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Sales Person', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Salesperson', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('sales rep', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('sales rep.', @equivalentId)
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

SET @equivalentId = '579b6e85-dfda-47e3-a20a-5b32373fc692'

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

SET @equivalentId = '1fb1ab55-c584-4512-acda-8f144a3639fb'

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

SET @equivalentId = 'beade905-bade-4533-b4c5-b5aca3257b86'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Assistant Sales Manager', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Asst Sales Manager', @equivalentId)

SET @equivalentId = '33b2c528-752d-40a2-b550-6c66e29022e8'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Channel Manager', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('channels manager', @equivalentId)

SET @equivalentId = 'a2ae984a-bfb3-4734-9de2-17cee1f33031'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Presales', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('pre-sales', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('pre sales', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('presales engineer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('pre-sales engineer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('presales coordinator', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Sales Support', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('sales support manager', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Sales Engineer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('sales engineering manager', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Technical Sales', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Technical Sales Representative', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Senior Sales Engineer', @equivalentId)

SET @equivalentId = '13e6c5de-4c9f-4ddc-91a4-9bd5883290d2'

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

SET @equivalentId = '5b7e77b7-13c9-466e-ade6-c07c9cb0fb60'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Internal Sales Representative', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Internal Sales Rep', @equivalentId)

SET @equivalentId = '8063f85a-cd4a-4d22-b099-343b983099f2'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Sales Agent', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Sales Advisor', @equivalentId)

SET @equivalentId = '694353ba-324e-4e12-84a9-fd6ff197523a'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('SALES ASSOCIATE', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Sales Assoc', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Senior Sales Associate', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Sr Sales Associate', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Sr. Sales Associate', @equivalentId)

SET @equivalentId = 'd557898e-0359-45be-9413-aca2f2ac628c'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Sales Assistant/Manager', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Senior Sales Assistant', @equivalentId)

SET @equivalentId = 'ee60f6f3-b54b-473d-a4d1-132fd405e0c9'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Junior Sales Assistant', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Sales Assistant/Cashier', @equivalentId)

SET @equivalentId = '65128add-7e28-4c95-b950-17d771e7c439'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Sales Assistant/Supervisor', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('supervisor/sales assistant', @equivalentId)

SET @equivalentId = '373edb5f-6188-4ed9-a422-96754055c30d'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Sales Assistant Customer service', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Customer Service/Sales Assistant', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Receptionist/Sales Secretary', @equivalentId)

SET @equivalentId = '525dc3af-60c5-4c7d-ab45-f95661e5bc98'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Advertising Sales Manager', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Advertising Sales Representative', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Advertising Sales Rep', @equivalentId)

SET @equivalentId = '0f8a2220-b173-4962-91f7-784baacc8d55'

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

SET @equivalentId = 'd4f0e429-145e-4947-8091-e73a8a0f3ee4'

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

SET @equivalentId = 'db0c441e-1945-4f34-a524-b6e5594ff7f0'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('vp marketing', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('vice president marketing', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('marketing vice president', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('marketing vp', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('marketing v.p.', @equivalentId)

SET @equivalentId = '7f266df7-d7cf-41f6-92a6-ffad68de3987'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('membership manager', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('membership development manager', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('manager membership services', @equivalentId)

SET @equivalentId = '989cc111-0b89-46a2-9454-bf7139c0ad2e'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('membership consultant', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('membership officer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('membership services officer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('membership administrator', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('membership salees consultant', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('membership development consultant', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('membership development offcier', @equivalentId)

SET @equivalentId = '59820218-4a52-4731-9432-7aa8e7c3bb93'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Marketing Assistant', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Marketing asst', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Casual Marketing Assistant', @equivalentId)

SET @equivalentId = '998c4c44-b7e9-41fa-bfe8-bbb0a5d03dfc'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('PR Manager', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('P.R. Manager', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Public Relations Manager', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('PR Director', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Public Relations Director', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Public relations', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Marketing Communications Manager', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Communications Manager', @equivalentId)

SET @equivalentId = 'f5a94003-073d-4dad-b7d9-873cad22cf08'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('PR', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('p r', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('pblic relations', @equivalentId)

SET @equivalentId = '0b461862-b94b-424a-ab91-1035936ddfb7'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Marketing Communications Specialist', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('marketing communications', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('marketing coordinator', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('marketing co ordinator', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('marketing co-ordinator', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Marketing Communications Co-ordinator', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Marketing Communications Co ordinator', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Marketing Communications Coordinator', @equivalentId)

SET @equivalentId = '03bb2644-c870-4931-a8cc-50fe5acd9c37'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Communications', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Communications Officer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Communications Consultant', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('COMMUNICATIONS COORDINATOR', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Communications Specialist', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('comms', @equivalentId)

SET @equivalentId = '2cac4393-b840-4e68-a186-bb44099fd107'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Market Research', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Market researcher', @equivalentId)

SET @equivalentId = 'b689d17c-22f7-43d2-b51c-495ae8b51e90'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Promotions', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Promotions Officer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Promotions Co-ordinator', @equivalentId)

SET @equivalentId = '480a4ade-eed0-4049-97ff-d340215f1c81'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Senior Publicist', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Sr Publicist', @equivalentId)

SET @equivalentId = '1bf78bd0-e48d-413a-ab53-9482353ce601'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Event Manager', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Events Manager', @equivalentId)

SET @equivalentId = 'cf38ecf5-b81f-47fc-bde5-44754426f57d'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Advertising Sales Executive', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Advertising Sales', @equivalentId)

SET @equivalentId = 'de55a286-c99a-4493-81ba-f6ad374ffa3d'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Product Manager', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Senior Product Manager', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('National Product Manager', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Assistant Product Manager', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Group product manager', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('New Product Manager', @equivalentId)

SET @equivalentId = 'c75fe56e-e496-4d07-8428-15a8f7e7b0b8'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Market Research Officer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Market Research Consultant', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Market Research Analyst', @equivalentId)

SET @equivalentId = '2a266968-cd7c-41a5-ac85-5f0de0528171'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Business Analyst', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Bus Analyst', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('BA', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('B.A.', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Business Analyst/Consultant', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('SENIOR BUSINESS ANALYST', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Sr Business Analyst', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Lead Business Analyst', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Senior Business Analyst/Project Manager', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Business Analyst Team Leader', @equivalentId)

SET @equivalentId = '2c0f57a2-5963-4e59-aac2-ffed9c7dbb68'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('IT Business Analyst', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('I.T. Business Analyst', @equivalentId)

SET @equivalentId = '0b608a10-e38d-497d-84ad-c3bdc8494eaf'

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

SET @equivalentId = '20372060-4f10-48a4-8b8c-70917bff6f18'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Head cleaner', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Cleaning Manager', @equivalentId)

SET @equivalentId = '62b9f039-950b-4e9f-bf7f-309e9be42960'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Shopping Centre Cleaner', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('centre cleaner', @equivalentId)

SET @equivalentId = 'cb0b9517-527b-4ec5-8936-699785d8d857'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Research Assistant', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Research Asst', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Senior Research Assistant', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Sr Research Assistant', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('clinical research', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('research officer', @equivalentId)

SET @equivalentId = '3bfb0aa1-1b22-4d14-a209-6b0af8d87798'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Principal', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Senior Teacher', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Acting Principal', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Vice Principal', @equivalentId)

SET @equivalentId = '7ad24e2c-73bc-4fab-bc85-a39280266d73'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Teacher', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('school teacher', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Teacher/Trainer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('High School Teacher', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Middle School Teacher', @equivalentId)

SET @equivalentId = '1d8378f7-ac2b-482e-8c31-0e65932c5eff'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Classroom Teacher', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Primary Teacher', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Primary School Teacher', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Senior grades teacher', @equivalentId)

SET @equivalentId = 'dfb253ff-f2b2-4326-b256-2deeb598076b'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Kindergarten Teacher', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Early Childhood Teacher', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Pre School Teacher', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Preschool Teacher', @equivalentId)

SET @equivalentId = 'f405bdd4-22de-484c-8d80-fee0ff71e6b1'

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

SET @equivalentId = '330b5021-4a9c-4866-bd24-cf981d917f22'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Casual Teacher', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Casual Relief Teacher', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Relief Teacher', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Temporary Relief Teacher', @equivalentId)

SET @equivalentId = '85e40855-94a3-4325-9801-c9b397e5dafc'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('English Teacher', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('English Language Teacher', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('ESL Teacher', @equivalentId)

SET @equivalentId = '7dd44680-cf4f-4586-a4e9-98161ee9f8bd'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Computer Teacher', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Information Technology Teacher', @equivalentId)

SET @equivalentId = 'ca3f93ce-958c-4ef1-96cd-ed3aaddcae64'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('TAFE Teacher', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('TAFE sessional teaching', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('T.A.F.E. Teacher', @equivalentId)

SET @equivalentId = 'ccff9cc4-fcd0-4823-ba0a-96999f3050ec'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Mathematics Teacher', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Mathematics/Science Teacher', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Maths Teacher', @equivalentId)

SET @equivalentId = '0940b761-ca08-43db-99a9-4fe977997dc8'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Art Teacher', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Graphic Art Teacher', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Visual Art Teacher', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Teacher Art', @equivalentId)

SET @equivalentId = '35b9dc72-51fe-4025-bc36-ed75b73499fd'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Piano Teacher', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Music Teacher', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Instrumental Teacher', @equivalentId)

SET @equivalentId = '1e7b6667-e879-4e9c-badb-f7ff319d9fd8'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Dance teacher', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Belly dance teacher', @equivalentId)

SET @equivalentId = '16bad583-c86a-467d-b8ed-3ee74168fdd9'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('LOTE Teacher', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('L.O.T.E. Teacher', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Foreign Language Teacher', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Chinese Teacher', @equivalentId)

SET @equivalentId = '6b9550de-8253-41f4-b7d8-9d0e7b9834ca'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Production Technician', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Production Crew', @equivalentId)

SET @equivalentId = '54a14158-ef9b-49c8-8878-99259acd6d7b'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Child Protection Worker', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Child Protection Officer', @equivalentId)

SET @equivalentId = '33fe19b6-be5f-44c1-a027-348ac35f9e19'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Regional Survey Manager', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('State Survey Manager', @equivalentId)

SET @equivalentId = '4275092c-3712-461a-a228-c5fd895e8b18'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Policy Manager', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Polices Manager', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Senior Policy Officer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Senior Policies Officer', @equivalentId)

SET @equivalentId = 'd79d0acb-ab00-4f60-a896-ae9a8e4e3dde'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Unit Manager', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Unit Operations Manager', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Unit Director', @equivalentId)

SET @equivalentId = '681e8677-abac-40a0-a990-f47e937c6bbf'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Assistant', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Asst', @equivalentId)

SET @equivalentId = '7c7e04d4-b117-4e6f-8129-4afb6d52fa09'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('administrator', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('ADMINISTRATIVE MANAGER', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('BUSINESS ADMINISTRATOR', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Senior Administration Officer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Business Administration', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Executive Administrator', @equivalentId)

SET @equivalentId = 'd5488cd3-3ad2-4929-af25-0691f879eed2'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Operations Administrator', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Operations Admin', @equivalentId)

SET @equivalentId = '39bc2483-0131-4869-a875-13032dade275'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Factory Hand', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Casual Factory Hand', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('General Factory Hand', @equivalentId)

SET @equivalentId = 'c01827fe-c9fe-44bc-953b-003f6e0835c5'

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

SET @equivalentId = 'f1b05d02-2d26-42ff-ab9a-9989e54245dc'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Proprietor', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Sole Proprietor', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Business Proprietor', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Manager/Proprietor', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Joint Proprietor', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Owner - Manager', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Business Owner/Operator', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Small business owner', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Owner-Operator', @equivalentId)

SET @equivalentId = '1187a31a-79c5-4596-9354-ff7447533d87'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Owner', @equivalentId)
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

SET @equivalentId = '7f3ac810-1134-406b-8b37-d26d2dbe8cca'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Storeman', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Warehouse Storeman', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Senior Storeman', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Storeman / driver', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Storeman/driver', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Storeman/Frontcounter Sales Counter Sales', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Storeman/Forklift Driver', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Casual Storeman', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Warehousing/Storeman', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('storeperson', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Forklift Operator/Storeperson', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Store Person', @equivalentId)

SET @equivalentId = '48bc2979-6034-4cab-933f-5f59391d7baf'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Delivery Driver', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Pizza delivery driver', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Multi-drop Delivery Driver', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Delivery Driver/Kitchen hand', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Owner Driver', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Driver', @equivalentId)

SET @equivalentId = 'e9a2e542-82df-48b4-ae01-fc457182f9a8'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Bus Driver', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Coach Driver', @equivalentId)

SET @equivalentId = '45b89dc9-d1c9-4ede-be8b-c61adf774931'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Taxi Driver', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Cab Driver', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('HGV Driver', @equivalentId)

SET @equivalentId = '940e983f-ee44-4baf-9809-35f9c8a5cb74'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Interstate Truck Driver', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Truck Driver/Plant Operator', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Truck Driver', @equivalentId)

SET @equivalentId = '184e3900-7521-4ddf-af14-545f0fb45bbf'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('lecturer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Music Technology Lecturer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Associate Lecturer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Assistant Lecturer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Senior Lecturer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Guest Lecturer', @equivalentId)

SET @equivalentId = 'd19a78c2-3300-4e90-971c-f2783c1648cb'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Coordinator', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Co ordinator', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Co-ordinator', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('ordinator', @equivalentId)

SET @equivalentId = '7c046297-74b9-4f4b-89d8-4a61f15b71aa'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Work Experience', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Work Experience Student', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('WORK EXPERIENCE - TAFE', @equivalentId)

SET @equivalentId = '4c1d789f-db18-405c-a294-9d77fe76e347'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('PHARMACY ASSISTANT', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Pharmacy Assistant Customer Service', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('dispensary technician', @equivalentId)

SET @equivalentId = 'a9a7146a-e57a-4563-aaae-f8bef1845111'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('pharmacist', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('pharmacy manager', @equivalentId)

SET @equivalentId = '183b574d-2312-4a15-a1c2-b515b6aeaad5'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Trainee', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Traineeship', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Student trainee', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Graduate Trainee', @equivalentId)

SET @equivalentId = 'a3783027-eccb-439f-9551-6aa7a16f461e'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Volunteer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Volunteer Worker', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Volunteer Member', @equivalentId)

SET @equivalentId = 'c6cb45e1-7c56-45d4-b85f-a3a69153e7ff'

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

SET @equivalentId = 'ebf10589-87f4-4a27-bd11-98215ef546cd'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Child Care Assistant', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Child Care Worker', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('child care', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Child Carer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Qualified Child Care Worker', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Casual child care assistant', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('childcare', @equivalentId)

SET @equivalentId = 'a3bda450-f744-4046-835c-a39314d5ef98'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Computer Operator', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Senior Computer Operator', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Trainee Computer Operator', @equivalentId)

SET @equivalentId = '13fa2c9b-f7d3-4eb6-aff8-8d33efe6ef92'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('FARMHAND', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Farm hand', @equivalentId)

SET @equivalentId = '31814fdc-e3ee-40c0-ba26-b31762181839'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Technical Consultant', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Senior Technical Consultant', @equivalentId)

SET @equivalentId = '6c96efa2-0cd5-4912-b98f-07ed64b8cce3'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Various', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('varied', @equivalentId)

SET @equivalentId = '5f6b6e22-96da-452e-bb4a-68f4eb561772'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Business Consultant', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Senior Business Consultant', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('New Business Consultant', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Telephone Business Consultant/Customer Service Representive', @equivalentId)

SET @equivalentId = '2a984778-e20c-47cb-a8cc-49875d73b875'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Laboratory Technician', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('medical laboratory technician', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('lab technician', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Laboratory Assistant', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Lab Assistant', @equivalentId)

SET @equivalentId = 'a88e59e1-d50e-4659-86ed-9114ff39e15a'

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

SET @equivalentId = '6ee333f1-a78b-402d-bbb1-e094157c2dcc'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Delicatessen Manager', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Deli Manager', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Service Deli Manager', @equivalentId)

SET @equivalentId = '38592e70-c117-4dd7-8421-2cbf844bb8f4'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Deli Assistant', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Service Deli Assistant', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Serviced Deli Assistant', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('DELICATESSEN ASSISTANT', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Deli', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Delicatessen', @equivalentId)

SET @equivalentId = '15e60b7e-12c7-4325-b8e9-1dc03be6ca9d'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Translator', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Interpreter', @equivalentId)

SET @equivalentId = '0d48b6bc-d566-4883-b685-158a5eb1bc5e'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Stockbroker', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Stock broker', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Stock broking', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Stockbroking', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Stockbrokers', @equivalentId)

SET @equivalentId = '34275790-77ca-4b3f-9bb6-2c744ecf1065'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('1', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('one', @equivalentId)

SET @equivalentId = '47fe02b7-7757-43dc-a60e-6cf8e1bfbb85'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('2', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('two', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('ii', @equivalentId)

SET @equivalentId = '9aa304a7-4e86-4c25-b938-c87f2aaa5113'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('3', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('three', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('iii', @equivalentId)

SET @equivalentId = 'b16a369c-d9f6-4076-af1c-003c0c43750c'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('4', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('four', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('iv', @equivalentId)

SET @equivalentId = 'fabe1cd0-d476-4429-be11-c85f50da7f52'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('5', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('five', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('v', @equivalentId)

SET @equivalentId = '1483646c-29dd-4bcf-b29d-feb6996d4e29'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('6', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('six', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('vi', @equivalentId)

SET @equivalentId = 'a70f551a-3e07-483e-86b9-f3c64fd413a4'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('7', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('seven', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('vii', @equivalentId)

SET @equivalentId = '45a94631-fd29-4b2d-9f46-87e5d3428456'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('8', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('eight', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('viii', @equivalentId)

SET @equivalentId = 'cc762128-b7da-45a7-9c9b-a4a56cb4534b'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('9', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('nine', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('ix', @equivalentId)

SET @equivalentId = '144df85d-19fb-475f-be3a-aacbcad9faae'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('10', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('ten', @equivalentId)

SET @equivalentId = '6e4f7197-59e2-42cc-abe6-a73e61f31c4e'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('1st', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('first', @equivalentId)

SET @equivalentId = '6e33466b-cb19-4074-973e-be682206bdbd'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('3ic', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('3.i.c.', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('3i.c.', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Third in charge', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('3 IC', @equivalentId)

SET @equivalentId = 'dd8aa0d3-5592-4d55-8319-2dd208a0ea18'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('2nd', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('second', @equivalentId)

SET @equivalentId = 'ed1129a6-0bd3-4c94-bb46-92312eb694bc'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('3rd', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('third', @equivalentId)

SET @equivalentId = 'b0d714e0-3180-45d4-8f33-ea4355626459'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('4th', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('forth', @equivalentId)

SET @equivalentId = '92a0fb38-0d8e-4a55-935b-18eee868bb3a'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('class 1', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('class1', @equivalentId)

SET @equivalentId = 'c978d2fc-a951-43cd-9722-58099926f92d'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('class 2', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('class2', @equivalentId)

SET @equivalentId = '3807cf96-e93f-4680-b9c5-1a629623ff01'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('class 3', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('class3', @equivalentId)

SET @equivalentId = 'fe5ce95a-95a4-4d52-8869-29e121500262'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('class 4', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('class4', @equivalentId)

SET @equivalentId = 'fecc1f33-8d86-4b34-bdc2-efa9dcb79d89'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('class 5', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('class5', @equivalentId)

SET @equivalentId = '8e7b8c2b-dcc2-405e-8154-9c86bf0f96b7'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Aboriginal', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('indigenous', @equivalentId)

SET @equivalentId = 'b2317466-e167-4373-b29d-dfb11eb694d9'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('acfi', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Aged Care Funding Instrument', @equivalentId)

SET @equivalentId = '97e3f3fd-c202-4841-9ab0-ce1ed589b732'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('ADSL', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Asymmetric Digital Subscriber Line', @equivalentId)

SET @equivalentId = '314ccc14-55d6-453a-97d9-894459eda3d6'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('ag', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('agriculture', @equivalentId)

SET @equivalentId = 'b36a52e7-ab5b-477f-9acb-1aff5d5d62fb'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('A-Grade Electrical', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('A Grade Electrical', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('A Grade electrician', @equivalentId)

SET @equivalentId = '6c0fa8c4-403e-48fa-a594-a1d323fbe6e9'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('asap', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('as soon as possible', @equivalentId)

SET @equivalentId = '21d57e29-9c98-4863-8d99-b0b3c195d78c'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('ASX', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Australian stock exchange', @equivalentId)

SET @equivalentId = '228e5ee6-c12c-4052-a154-b1c892966d25'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('aust', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('australia', @equivalentId)

SET @equivalentId = '036cffd5-ba9a-46b7-aa7e-66aec3fe079e'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('bas', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('business activity statement', @equivalentId)

SET @equivalentId = '66a7d38f-4808-497e-a090-ffd11fd69273'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('BHP', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('BHPBilliton', @equivalentId)

SET @equivalentId = 'b8118c4c-9a88-44fa-97f6-e1a8453c3e28'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('rio tinto', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('riotinto', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('rtz', @equivalentId)

SET @equivalentId = '99c17317-fa65-4880-a506-5954acf3b686'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('bris', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('brisbane', @equivalentId)

SET @equivalentId = '7d503e11-9ec3-4d78-828d-84b8f21e3cab'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('cam', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('computer aided manufacturing', @equivalentId)

SET @equivalentId = '15e17c35-947c-4df3-8195-06d63fa6a11f'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('cardio', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('cardiovascular', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('cardio-vascular', @equivalentId)

SET @equivalentId = '7df417c5-40f0-4c6d-a971-708129429f0c'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('cashflow', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('cash flow', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('cash-flow', @equivalentId)

SET @equivalentId = '4b731f6e-58cb-4539-a40e-55dfbb185d55'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('CBA', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Commonwealth Bank of Australia', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('comm bank', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('commbank', @equivalentId)

SET @equivalentId = 'cbbe7791-3a90-47ed-a20d-b5ced63bd194'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('cbd', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('central business district', @equivalentId)

SET @equivalentId = '6f6533b7-eb42-43bd-8f8b-29762c59e63f'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('cctv', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('closed circuit tv', @equivalentId)

SET @equivalentId = 'fc82f3a3-ea64-4818-b96f-209d8b631334'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('cert', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('certificate', @equivalentId)

SET @equivalentId = '9ffec598-ea38-48c2-9075-cfb850265acb'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('cert1', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('certificate 1', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('certi', @equivalentId)

SET @equivalentId = '842a735f-0673-4e9b-a011-130720e6c886'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('cert2', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('certificate 2', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('certii', @equivalentId)

SET @equivalentId = '510c1279-ec6b-474d-9db9-64880792d961'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('cert3', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('certificate 3', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('certiii', @equivalentId)

SET @equivalentId = 'ebb22f5e-db6e-4e31-ab0d-5824c0a02336'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('comp', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('compensation', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('compo', @equivalentId)

SET @equivalentId = 'bbd3ae41-1386-45ab-a007-1de7c8948db2'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('contract', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('agreement', @equivalentId)

SET @equivalentId = '81bba0ee-2c25-4cfe-93ef-0fa51e780377'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('CPU', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('central processing unit', @equivalentId)

SET @equivalentId = 'f75fc6fb-f998-4088-8799-23b54c3a19b0'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('crm', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('customer relationship management', @equivalentId)

SET @equivalentId = 'bb90826f-1ba4-4e87-aa49-17f4934d421b'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('cv', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('curriculum vitae', @equivalentId)

SET @equivalentId = 'ec070948-24e4-4fa7-aade-e57f64d63e61'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('dispatch', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('despatch', @equivalentId)

SET @equivalentId = '509d15a5-7a17-47da-9eaf-c57d9faf9d39'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('div', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('division', @equivalentId)

SET @equivalentId = 'ad47c013-2d38-4ad2-8ff3-a9acd4408b6e'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('DNS', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Domain Name System', @equivalentId)

SET @equivalentId = 'c908b98c-6b0b-48ca-a304-0f7b7bfdaa15'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('dr', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('direct response', @equivalentId)

SET @equivalentId = '7591cbde-901e-4c51-8d2e-517b0f8b0682'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('drp', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('disaster recovery plan', @equivalentId)

SET @equivalentId = '9971baf6-4981-4dd1-afba-6dbec52e648f'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('DSL', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Digital Subscriber Line', @equivalentId)

SET @equivalentId = '94ca4e61-8189-423a-be00-e64810048851'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('elec', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('electrical', @equivalentId)

SET @equivalentId = 'b9c488ef-57cc-4a22-9d29-37bbac5cc28b'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('email', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('e-mail', @equivalentId)

SET @equivalentId = 'f28b1b23-e719-4466-b6bd-24d93f4274ed'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('epcm', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Engineering Procurement Construction Management', @equivalentId)

SET @equivalentId = '2246f53d-c593-4ba5-841b-02fc139b28a8'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('EPS', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('earnings per share', @equivalentId)

SET @equivalentId = '5d7509b1-1a49-4678-b013-24eff0bd0bdb'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('exp', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('experience', @equivalentId)

SET @equivalentId = '954687d0-6885-4293-b290-47be6b4ded79'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('fifo', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('first in first out', @equivalentId)

SET @equivalentId = '0cbd1daa-195e-4bed-ba28-08d6ef5d1d35'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('first aid', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('firstaid', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('first-aid', @equivalentId)

SET @equivalentId = 'e8736430-55b4-44ba-b878-257cfe53f1b7'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('fmcg', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('fast moving consumer goods', @equivalentId)

SET @equivalentId = 'e8af9a60-3590-4b65-a84b-dfb7d9c9e22a'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Food Processor', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Food Processing', @equivalentId)

SET @equivalentId = 'a82afaea-97ee-41ad-ab4a-edf095ecab7b'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('GDP', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('gross domestic product', @equivalentId)

SET @equivalentId = '8c49c514-6366-4418-8d74-fe5dd466609f'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('GNP', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('gross national product', @equivalentId)

SET @equivalentId = '8e317324-75b4-4542-9d2a-d1298c4def38'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('govt', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('government', @equivalentId)

SET @equivalentId = '063cde0b-7239-4aad-8552-3837a672cc98'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('GPS', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Global Positioning System', @equivalentId)

SET @equivalentId = '8e023b72-62e9-49e2-8e59-62c7fbf77cbb'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('guard', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('gaurd', @equivalentId)

SET @equivalentId = 'f90042a3-f910-405d-b0eb-9443eec98ca6'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Health & Safety', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Health and Safety', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Health&Safety', @equivalentId)

SET @equivalentId = 'd6a0bf05-c246-47ed-81e0-29d0491bc6bf'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Healthcare', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Health Care', @equivalentId)

SET @equivalentId = 'b7a1e10c-6997-4b8a-9382-6302a49f4208'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('hp', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('hewlett packard', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('hewlettpackard', @equivalentId)

SET @equivalentId = '211d2e21-36fd-4c5c-ac0f-460f6e361bbc'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('hris', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('human resource information system', @equivalentId)

SET @equivalentId = '56d982d1-79d2-4103-a749-35c5c94c3fe9'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('ie', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('institute of engineers', @equivalentId)

SET @equivalentId = '3ad90676-c27e-4426-980d-79d7e810d839'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('infra structure', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('infrastructure', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('infra-structure', @equivalentId)

SET @equivalentId = '2e88e66d-7f2c-4129-8de1-8106a9935a9a'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('jdedwards', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('jd edwards', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('j.d.edwards', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('jde', @equivalentId)

SET @equivalentId = 'ccfacebb-e557-40e3-9528-c61d5e052037'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('jnr', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('junior', @equivalentId)

SET @equivalentId = '98f2daa1-0603-4fa8-b07b-4dad9dbfba8d'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('KPI', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('key performance indicator', @equivalentId)

SET @equivalentId = 'c45528e9-e0ac-439c-9696-409bafc120bb'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('l&d', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('learning and development', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('training', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('l and d', @equivalentId)

SET @equivalentId = 'eb50be06-a9a9-45a6-b6d0-d8df89983383'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('labor', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('labour', @equivalentId)

SET @equivalentId = '9210a456-c1ec-48b5-89ea-0d2ccca4c939'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('lan', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('local area network', @equivalentId)

SET @equivalentId = '8294df58-4c6b-4107-9b9e-beb7a1a6de9f'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('ltd', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('limited', @equivalentId)

SET @equivalentId = '655c6997-dcef-4a8a-b0b1-16089c7ec470'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Mac', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Macintosh', @equivalentId)

SET @equivalentId = 'a6890142-53ac-412b-9001-508f90d6aee2'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('macq', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('macquarie', @equivalentId)

SET @equivalentId = '7bbba06e-3629-47e7-bc5e-d8e42baf4fc4'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('mba', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('master of business administration', @equivalentId)

SET @equivalentId = 'd7fba3ca-6fb0-4406-9ecd-186e996e732d'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('mech', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('mechanical', @equivalentId)

SET @equivalentId = '65aedaac-5c8e-4639-8000-a7210543a56c'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('med', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('medical', @equivalentId)

SET @equivalentId = 'aa4a3c83-722c-44e5-9738-523099e9941b'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('melb', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('melbourne', @equivalentId)

SET @equivalentId = 'c5a5a631-d839-4a23-b876-6e1b6ceb5628'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('mngt', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('management', @equivalentId)

SET @equivalentId = '497c729d-5a58-426c-a2cc-44c9a365e000'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('mr', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('medium rigid', @equivalentId)

SET @equivalentId = '78d36bc7-c614-4749-ad0b-64b188feac45'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('ms', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('microsoft', @equivalentId)

SET @equivalentId = 'f041bd29-e461-4c88-a50b-bcd97bf075c5'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('NAB', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('National Australia Bank', @equivalentId)

SET @equivalentId = '99973082-6461-4c56-80e6-5ab6fde13630'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('north east', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('northeast', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('north-east', @equivalentId)

SET @equivalentId = '13d3c42a-be68-49c1-a666-ed1ac94d2f5e'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('neg', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('negotiable', @equivalentId)

SET @equivalentId = 'f0756484-b983-41c0-ae96-709115a16528'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('nsw', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('new south wales', @equivalentId)

SET @equivalentId = 'd584faa4-1909-4a87-98a0-91de1addc4f6'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('north west', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('northwest', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('north-west', @equivalentId)

SET @equivalentId = 'd41de7b6-e157-4c5a-a1ea-698c2f5f1071'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('nyse', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('new york stock exchange', @equivalentId)

SET @equivalentId = '2c243d57-14ac-4c9b-9225-935832acc726'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('nz', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('new zealand', @equivalentId)

SET @equivalentId = '6a004592-c90b-4cbf-9663-117f9733aa06'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('OHS', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('OH&S', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Occupational Health and Safety', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Occupational Health & Safety', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('oh &s', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('ehs', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('environmental health and safety', @equivalentId)

SET @equivalentId = 'a5880d71-95c4-488d-83e3-e1557734a7b9'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Oil and Gas', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Oil&Gas', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Oil & Gas', @equivalentId)

SET @equivalentId = 'bd10b221-b9d9-415f-ae7e-238649d78f5f'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('online', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('on-line', @equivalentId)

SET @equivalentId = '52336689-c15b-4d7f-9a4d-40d9ed21a858'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('os', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('operating system', @equivalentId)

SET @equivalentId = 'b20451f4-cec2-4e92-b5f1-323217cbbbc9'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('ot', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('occupational therapist', @equivalentId)

SET @equivalentId = '8647c102-99fd-41d4-ac7a-9783db395048'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('p&l', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('profit and loss', @equivalentId)

SET @equivalentId = '2431329b-e94c-4600-9cab-4ee00aea6cfe'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('pae', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('participating administrative entity', @equivalentId)

SET @equivalentId = 'c23eb59f-f13b-46da-b940-deb616508c94'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('payg', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('pay as you go', @equivalentId)

SET @equivalentId = '68daacaa-823d-4fd3-bd3e-ce0cfca83af3'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('perm', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('permanent', @equivalentId)

SET @equivalentId = '7c568776-dd13-44ba-9b7d-a25b01994d42'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('pi', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('personal injury', @equivalentId)

SET @equivalentId = '41d2f8fe-a36b-498e-9b59-a9e14ddbbb6a'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('plc', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('publically listed company', @equivalentId)

SET @equivalentId = '12f0dcc5-c851-490f-a256-ff27d4524db9'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('POS', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Point of Sale', @equivalentId)

SET @equivalentId = '89241f2e-aedc-47bd-83a2-5793d5817ad5'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('proactive', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('pro active', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('pro-active', @equivalentId)

SET @equivalentId = '0f73c0e8-baec-49f3-99ff-9a062b28e4b7'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Psych', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('psychologist', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('psychology', @equivalentId)

SET @equivalentId = '0b9ce664-ffb9-47af-aa14-27f25c474453'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('pty', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('proprietary', @equivalentId)

SET @equivalentId = 'f8f426b0-89eb-4e91-b820-67dbbafde308'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Q&A', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Quality and Assurance', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Quality & Assurance', @equivalentId)

SET @equivalentId = '0a656e5a-26b3-495b-bd3a-1bf064717c6d'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('qa', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('quality assurance', @equivalentId)

SET @equivalentId = 'e539103f-f3e2-4b67-94e7-c5a6781c2ea9'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('qld', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('queensland', @equivalentId)

SET @equivalentId = 'a1748e6f-950c-4991-ac0a-c0d658785da4'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('qual', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('qualification', @equivalentId)

SET @equivalentId = '28a66a45-e0cb-4f77-a325-57e48c327c2b'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('quick books', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('quickbooks', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('quick-books', @equivalentId)

SET @equivalentId = '592311a9-886d-4e74-a9ef-3bd34cadfaff'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('r and d', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('research and development', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('research & development', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('rnd', @equivalentId)

SET @equivalentId = '1f22101d-2b87-4064-b24b-30d7aaf00b0a'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('real estate agent', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('officer in effective control', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('realestate consultant', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('property consultant', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('buyers advocate', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('buyer s advocate', @equivalentId)

SET @equivalentId = '1515021e-fe20-4111-b7d8-4446b30bda87'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('rec2rec', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('recruiter to recruiter', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('rec-to-rec', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('rec to rec', @equivalentId)

SET @equivalentId = '1af5f454-efcb-4db9-88a4-2b8e31104ee0'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('reengineering', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('re engineering', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('re-engineering', @equivalentId)

SET @equivalentId = 'a9c189fa-d84a-4991-a87a-6454f42643ed'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('ref', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('reference', @equivalentId)

SET @equivalentId = '918a12fe-c567-4239-a230-6e3ddb424b03'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('rehab', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('rehabilitation', @equivalentId)

SET @equivalentId = '0721267d-87b1-47e6-a7fd-af2cc0fb7f81'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('req', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('requirement', @equivalentId)

SET @equivalentId = '967db6e9-412c-4c9c-878e-2aa4be06761c'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('rf', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('radio frequency', @equivalentId)

SET @equivalentId = '950406b5-51d7-46e9-bf2a-1987146b165b'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('ROE', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('return on equity', @equivalentId)

SET @equivalentId = '3a97dd52-acb3-4351-b688-4b199218b352'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('ROI', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('return on investment', @equivalentId)

SET @equivalentId = '76c5b3ae-dcb0-4305-8315-324f5b7f148d'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('romp', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Radiation Oncology Medical Physics', @equivalentId)

SET @equivalentId = 'b05e0dc2-9220-4327-a59e-508506ad6415'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('rta', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('road traffic authority', @equivalentId)

SET @equivalentId = '4a1dea4b-e48e-4fe4-b503-f51864214842'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('rtw', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('return to work', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('return-to-work', @equivalentId)

SET @equivalentId = '18a2fadd-db9c-40f5-a689-2c8776fd19a3'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('sap', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('s.a.p.', @equivalentId)

SET @equivalentId = '773bf110-47e2-4ef0-832d-d1ad175da41a'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('SAP EP', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Enterprise Portal', @equivalentId)

SET @equivalentId = '3d67df8d-7a79-4f81-8566-dbd81c1ad0c8'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('south east', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('southeast', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('south-east', @equivalentId)

SET @equivalentId = '72b6d81e-69c5-49b2-a720-340214f074e9'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('south west', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('southwest', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('south-west', @equivalentId)

SET @equivalentId = '616e034b-84c2-433f-9116-564de953153e'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('SEM', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Search Engine Marketing', @equivalentId)

SET @equivalentId = '9515c278-d9e9-4a44-badf-816bdf26d7fd'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('SEO', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Search Engine Optimisation', @equivalentId)

SET @equivalentId = '42b98c53-3d93-497a-9bb6-8a7c1a21082c'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('share point', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('sharepoint', @equivalentId)

SET @equivalentId = '04fa8b26-2790-4d2f-8ece-83bc848b3cb8'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('sme', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('small and medium enterprise', @equivalentId)

SET @equivalentId = 'da7c67a7-ed4c-42e1-b8e8-61e8b69d2552'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('sml', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('small', @equivalentId)

SET @equivalentId = '293ad159-b2c5-41cd-8d6e-46edf2d80512'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('snr', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('senior', @equivalentId)

SET @equivalentId = '683693b1-0d68-4348-8508-f2e8e7c90d09'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('software', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('soft ware', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('soft-ware', @equivalentId)

SET @equivalentId = '85c4f63d-7e69-42d1-852b-4eba7d6aecbf'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('strategic', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('strategy', @equivalentId)

SET @equivalentId = '92d16c14-7234-407e-88e1-25d744660d5a'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('syd', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('sydney', @equivalentId)

SET @equivalentId = 'a63c30ca-ecf1-4c64-a8de-2aafb3d93017'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('tech', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('technician', @equivalentId)

SET @equivalentId = '8ed67ff2-88e0-4a24-a790-8cc0c09456dc'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('temp', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('temporary', @equivalentId)

SET @equivalentId = 'ff526bc4-05a5-4707-91d8-3fc768449599'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('through', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('thru', @equivalentId)

SET @equivalentId = '898a6457-8e85-4f55-9943-2c403a32551b'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('tire', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('tyre', @equivalentId)

SET @equivalentId = 'aac93858-9855-4997-9938-52c16e1307d3'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('tkt', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('ticket', @equivalentId)

SET @equivalentId = 'aea4cc1e-a844-4d40-9a3e-821ec2d2369d'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('trim', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Total Records and Information Management', @equivalentId)

SET @equivalentId = '723d7080-5f28-4509-b0ae-37ffa3f0c9e5'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('tv', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('television', @equivalentId)

SET @equivalentId = '61ad9cb8-3c52-467a-8457-75d83b012c82'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('uat', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('user acceptance testing', @equivalentId)

SET @equivalentId = 'db9be8b6-1a68-4768-b5c2-0469fd6234e5'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('ui', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('user interface', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('u.i.', @equivalentId)

SET @equivalentId = '3e234b07-3feb-485d-a6dc-9cd264693b3c'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('uni', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('university', @equivalentId)

SET @equivalentId = '543a69d0-67c0-4094-bfe5-b95eedea3754'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('vic', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('victoria', @equivalentId)

SET @equivalentId = '1c1962ca-bdad-4817-adbd-b3041a7cf8bb'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('act', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('australian captial territory', @equivalentId)

SET @equivalentId = 'ffc66c6a-9425-466f-9205-4bc53e20190c'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('vpn', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('virtual private network', @equivalentId)

SET @equivalentId = 'dd3f6d67-fe2d-4055-bf80-9b494288f9cf'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('VSD', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Variable Speed Drives', @equivalentId)

SET @equivalentId = '683a2897-9b0c-4244-b2f9-c7fec57c5530'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('wa', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('western australia', @equivalentId)

SET @equivalentId = '4633ae15-a95e-4947-9a15-cdb173a9fec6'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('wan', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('wide area network', @equivalentId)

SET @equivalentId = '79cec061-b208-4e0b-ba9d-86c829dd2c9e'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('wk', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('week', @equivalentId)

SET @equivalentId = 'e125898f-2409-4835-aa90-8e3ba0abce0e'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Word', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Synonyms', @equivalentId)

SET @equivalentId = '87044a37-5a98-4e37-9b73-44b182e51075'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('xray', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('x-ray', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('x ray', @equivalentId)

SET @equivalentId = '9d246792-9f18-4cdb-801b-46ca20c6f094'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('program', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('programme', @equivalentId)

SET @equivalentId = '5c31a6eb-c4a3-47f0-a5ec-178c2719d08d'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('expert', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('guru', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('specialist', @equivalentId)

SET @equivalentId = '6cd6a143-5596-4de6-9880-008fa5312ae6'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('butcher', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('butchery', @equivalentId)

SET @equivalentId = '96e2cd40-76e6-4d83-a702-d6ee0a5b0e77'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('hvac', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('heating ventilation air conditioning', @equivalentId)

SET @equivalentId = 'a9dda1f9-8f4d-4a0b-99b7-d243bcfdbc54'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('air con', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('air conditioning', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('aircon', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('airconditioning', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('air conditioner', @equivalentId)

SET @equivalentId = '1a1cb014-d3d7-4182-87a3-7d52346c41f5'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('exec', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('executive', @equivalentId)

SET @equivalentId = 'fa444475-c7f7-417e-80f1-37ba81209d63'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('apn', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('australian provincial news', @equivalentId)

SET @equivalentId = '31a3765d-bdf1-4cbf-b6ff-550421138adb'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('student', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('undergrad', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('under grad', @equivalentId)

SET @equivalentId = 'ecd4c7ac-cc71-4bf0-b1b9-a92cb99a358c'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('grad', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('graduate', @equivalentId)

SET @equivalentId = '30717e04-269d-427e-a2a5-a7efe9acbd57'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('aqtf', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('australian quality and training frameworks', @equivalentId)

SET @equivalentId = '1176af90-6831-4d73-8fc3-3f1f6ecd2777'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('rto', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('registered training organisation', @equivalentId)

SET @equivalentId = 'ec0ad965-9e23-40e3-a2e4-552bb2b8b983'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('organisation', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('organization', @equivalentId)

SET @equivalentId = '9f064671-cb1a-4506-b757-f3eff3d92da2'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('reg', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('regd', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('registered', @equivalentId)

SET @equivalentId = 'd34c629a-d371-4639-8d52-fc60f40e50fe'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('nlp', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('neuro linguistic programming', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('neurolinguistic programming', @equivalentId)

SET @equivalentId = '5ea3bfc9-cf74-46f9-ab11-6d42f9351ce2'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('bcg', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('boston consulting group', @equivalentId)

SET @equivalentId = 'c7c4ccc7-8802-42b8-8ef0-cbd32386ddd6'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('mobil', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('exon mobil', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('exonmobil', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('exxon', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('exxonmobil', @equivalentId)

SET @equivalentId = 'b2fb3b8d-e15b-4287-857a-826ba0006a9a'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('bp', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('brittish petroleum', @equivalentId)

SET @equivalentId = 'f068a875-987b-4672-b994-d059c21186bc'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('aicd', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('austrlalian institute of company directors', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('maicd', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('faicd', @equivalentId)

SET @equivalentId = '3f7b0017-49f4-4e89-b10b-487b9a07ba0b'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('circa', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('around', @equivalentId)

SET @equivalentId = '3d416f6c-5638-4013-9b31-744fde93b163'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('afp', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('australian federal police', @equivalentId)

SET @equivalentId = 'cbc6812b-8484-41b4-ba2f-e2f41788e540'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('asic', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('australian securities and investment commission', @equivalentId)

SET @equivalentId = '8f120a1f-1c84-4b00-9228-d59b70ff23ec'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('jv', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('j v', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('joint venture', @equivalentId)

SET @equivalentId = '29c30aa7-b26b-47b2-9ffd-e803d24dfb88'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('uk', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('united kingdom', @equivalentId)

SET @equivalentId = '2324d6bb-fc0b-4f95-8f45-4e77a3f7d016'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('pwc', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('price waterhouse coopers', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('pricewaterhouse coopers', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('price water house coopers', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('coopers and lybrand', @equivalentId)

SET @equivalentId = '510bcfea-04eb-4af2-bf8d-60b0d20a4a03'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('deloitte', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('deloitte touche', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('deloitte touche tohmatsu', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Duesburys', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('deloittetouche', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('deloittetouchetohmatsu', @equivalentId)

SET @equivalentId = 'af8da9d6-7219-4e23-9d93-3218ef0c878b'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('ey', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('e&y', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('ernst and young', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('ernst & young', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('ernst&young', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('ernst & whinney', @equivalentId)

SET @equivalentId = '0ee95d85-29c3-498b-a112-5cb9c8a7cb08'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('kpmg', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('hungerfords', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('peat marwick', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('peatmarwick', @equivalentId)

SET @equivalentId = 'ccbb7c22-c444-4fcc-a300-cc8cc421a985'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('csl', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Commonwealth Serum Laboratories', @equivalentId)

SET @equivalentId = '036b8107-4cd5-4dc8-b071-6933b1eec7cc'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('eds', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('electronic data systems', @equivalentId)

SET @equivalentId = '768a5328-300b-4277-97c3-db8706359ebd'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('m&a', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('mergers and acquisitions', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('mergers & acquisitions', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('mergers&acquisitions', @equivalentId)

SET @equivalentId = '460c50af-9c4a-440a-bf92-cd91a1a3fd1b'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('arab', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('arabic', @equivalentId)

SET @equivalentId = 'ebb30e29-2bfa-40a6-8894-b69cf51d73ba'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('tac', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('transport accident commission', @equivalentId)

SET @equivalentId = '4a7e06cf-01b0-4224-bbf8-1a6b4730bf79'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('tax', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('taxation', @equivalentId)

SET @equivalentId = 'a6db53fb-00ef-4282-abaf-16fd77d2ab97'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('interor design', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('interior designer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('interior decorator', @equivalentId)

SET @equivalentId = '005dafc4-75ef-4c45-af43-b5b340917f13'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('reporter', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('journalist', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('journalism', @equivalentId)

SET @equivalentId = '646e5363-f7d5-48b3-9c2e-d55b14c13736'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('actuary', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('actuarial', @equivalentId)

SET @equivalentId = '0d0d2f9a-6303-4842-9d54-5e19d73403bf'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('tresury', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('treasury', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('treasurer', @equivalentId)

SET @equivalentId = '3573be0b-d8d2-4cb7-bce5-315aa2035a34'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('call centre', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('call center', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('contact centre', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('callcentre', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('callcenter', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('contactcentre', @equivalentId)

SET @equivalentId = 'f30c8470-7384-4fa3-8217-956826499316'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('cold call', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('cold calling', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('outbound', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('out bound', @equivalentId)

SET @equivalentId = '3fa375d5-f54b-40d1-94df-fbb43c6aff9a'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('worker', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('officer', @equivalentId)

SET @equivalentId = '02f4a549-7ab9-48d1-97e2-76777a8a4273'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('youth', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('adolescent', @equivalentId)

SET @equivalentId = '1daa94c4-41a9-4b1d-8bdf-79bf57ce52a2'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('family services', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('community services', @equivalentId)

SET @equivalentId = '85344786-3c21-43f9-9f90-19891834b6d4'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('apprentice', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('apprenticeship', @equivalentId)

SET @equivalentId = '741c1a77-d7fb-4af1-af8b-67337cc5ef99'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('kinder', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('kindergarten', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('kinda', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('childcare centre', @equivalentId)

SET @equivalentId = '56b2aefa-a20d-48cb-945b-1ddb3cefdc52'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('centre', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('center', @equivalentId)

SET @equivalentId = 'de968433-7dcd-4a5d-93d0-64a3af1541cf'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('child', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('children', @equivalentId)

SET @equivalentId = '56ac9fec-95c1-4925-8df6-bc0dd95c28f1'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('educator', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('tutor', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('trainer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('instructor', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('trainor', @equivalentId)

SET @equivalentId = '0d621421-70d3-4b04-b7c7-b5507c2fbe8e'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('waste water', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('wastewater', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('storm water', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('stormwater', @equivalentId)

SET @equivalentId = '24efb5c3-d046-4437-a685-7a56215a8cb0'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('hydrology', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('hydrologist', @equivalentId)

SET @equivalentId = '8e03feb8-700d-4f27-9403-71e0ca7a38d3'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('town planner', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('town planning', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('townplanner', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('urban planner', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('urban planning', @equivalentId)

SET @equivalentId = '4e12ccd4-79ee-48ba-a732-b13fb688277e'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('town', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('urban', @equivalentId)

SET @equivalentId = '59e42414-2918-49c2-85e2-93c2e81b52a2'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('uae', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('united arab emirates', @equivalentId)

SET @equivalentId = 'fa260bf8-b400-4e95-baa9-a004d952900d'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('geo technical', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('geotechnical', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('geo tech', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('geotech', @equivalentId)

SET @equivalentId = 'e4aa540a-69b4-4e63-bd52-75bb386e9c97'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('lead', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('head', @equivalentId)

SET @equivalentId = 'aa3896f6-fb6a-40f0-9473-327180471dc0'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('rail', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('railway', @equivalentId)

SET @equivalentId = '71f2678e-ae8e-4a21-a3b6-4ad0e0a29de5'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('nurse', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('nursing officer', @equivalentId)

SET @equivalentId = 'b0b907ba-bd77-483d-be61-515e3dc593af'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('defence', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('defense', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('armed forces', @equivalentId)

SET @equivalentId = 'b92e567b-c313-44a2-9c37-fef1c7d05f5d'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('air force', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('airforce', @equivalentId)

SET @equivalentId = '340c891e-714a-4379-b032-506fc632596c'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('aeroport', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('aero port', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('airport', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('air port', @equivalentId)

SET @equivalentId = '290df5d0-f93f-4327-90cf-da7e663aa799'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('ammunition', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('munition', @equivalentId)

SET @equivalentId = 'ff458527-09e4-43e1-9491-1fac16349efa'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('cbms', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('central budget management system', @equivalentId)

SET @equivalentId = 'a7c73c98-a82b-4ea3-822b-73acfe09c240'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('australia post', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('auspost', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('australiapost', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('austpost', @equivalentId)

SET @equivalentId = '6807add8-32a2-49e3-990f-caf180af159e'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('ict', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('information communication and technology', @equivalentId)

SET @equivalentId = '4f0fcef6-800a-4792-9652-8ad715ec29c1'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('aged care', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('agedcare', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('elderly care', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('elderlycare', @equivalentId)

SET @equivalentId = '999e0dd1-2959-4d5f-b485-89a1a8dfd78d'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('disability', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('handicapped', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('handicap', @equivalentId)

SET @equivalentId = '66939887-2293-42fa-9e93-e6d1ef78978b'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('hospice', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('palliative', @equivalentId)

SET @equivalentId = 'bd510592-0edb-4822-9d3d-83dbb9d56e42'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('chemo', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('chemotherapy', @equivalentId)

SET @equivalentId = 'ffaaafa4-9bc6-4267-b4dd-257d634b13ab'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('een', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('endorsed enrolled nurse', @equivalentId)

SET @equivalentId = 'e541e0ec-71ac-437f-997d-d41b153f476f'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('physio', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('physiotherapy', @equivalentId)

SET @equivalentId = 'ab856c4a-11fa-4809-bc1d-76a02801852b'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('nutriciantist', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('dietician', @equivalentId)

SET @equivalentId = '646b1273-1d79-4454-a342-ee933efe08bb'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('case manager', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('case worker', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('case officer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('inury manager', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('rtw officer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('case coordinator', @equivalentId)

SET @equivalentId = 'eb7823a5-0353-4242-a5de-e91b36d247ba'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('occ', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('occupational', @equivalentId)

SET @equivalentId = '3b669b84-e1da-4829-9aae-02d5f0801718'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('rec', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('recreational', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('recreation', @equivalentId)

SET @equivalentId = '38628078-0ea5-460a-959e-52a27ae9357c'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('speech therapist', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('speech pathologist', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('language therapist', @equivalentId)

SET @equivalentId = '739bbfc3-4d83-46e9-bfeb-5b985236a3ca'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('ambulance', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('paramedic', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('para medic', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('ambulance driver', @equivalentId)

SET @equivalentId = 'a3d4be7d-f0d9-4ffb-86fe-bfca55e9e85c'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('scra', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('society of clinical research associates', @equivalentId)

SET @equivalentId = '8ab29e52-ef42-4925-b4a2-86a4abebac60'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('anaesthetic', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('anaesthetist', @equivalentId)

SET @equivalentId = '2b3a5157-79dd-46f7-b111-1af5e684abf0'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('a&e', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('accident and emergency', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('emergency medicine', @equivalentId)

SET @equivalentId = 'b94ca289-0d55-42d6-a8f9-5360b139348c'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('midwife', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('mid wife', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('mid wifery', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('midwifery', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('mid wive', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('midwive', @equivalentId)

SET @equivalentId = '7b40e188-3afc-4210-bad1-1527a07d345e'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('icu', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('intensive care unit', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('intensive care', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('intensivecare', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('msicu', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('critical care medicine', @equivalentId)

SET @equivalentId = '52b64068-ffa8-4f0c-8e7e-7b45dbcd1fc6'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('picu', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('paedeatric intesive care', @equivalentId)

SET @equivalentId = '28bd587a-d5be-4968-8b50-0bf3222c0086'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('nicu', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('neonatal intesive care', @equivalentId)

SET @equivalentId = 'e449b2f4-9d16-462c-a6bf-395277fcdb80'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('safety officer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('safety inspector', @equivalentId)

SET @equivalentId = '86fc05a0-d3e0-4050-add0-888a9e501e3f'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('concierge', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('bell attendant', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('porter', @equivalentId)

SET @equivalentId = '2d3ee204-97e7-4bc2-94d8-787b03982251'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('grounds man', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('groundsman', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('grounds keeper', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('groundskeeper', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('ground attendant', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('public area attendant', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('green keeper', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('greenkeeper', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('turf manager', @equivalentId)

SET @equivalentId = '568cfbf7-a475-4eab-9fb4-9b99baceb494'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('dish washer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('dishwasher', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('kitchen assistant', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('sandwich hand', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('sandwichhand', @equivalentId)

SET @equivalentId = 'ffa95d70-f706-4254-a545-c78d413576ec'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('tour guide', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('tour director', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('bus guide', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('tourguide', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('resort guide', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('tour driver', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('tour driver guide', @equivalentId)

SET @equivalentId = 'bae55e67-721b-441a-8ed9-5e234e30c6e1'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('japan', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('japanese', @equivalentId)

SET @equivalentId = '01ea2c8f-5fe6-4be9-9700-35fad24f7b05'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('china', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('chinese', @equivalentId)

SET @equivalentId = 'f26631d7-27f2-4623-b6de-059cc3ba8f2b'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('france', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('french', @equivalentId)

SET @equivalentId = '85f229e6-24e3-449d-92f0-0fa97780cd33'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('germany', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('german', @equivalentId)

SET @equivalentId = '98a863f8-07fd-420a-904b-372e490dcfa6'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('ir', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('industrial relations', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('workplace relations', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('work place relations', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('employee relations', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('er', @equivalentId)

SET @equivalentId = 'c6fed7e7-6781-4100-b75e-96f82c71a7e5'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('job network', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('jobnetwork', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('job placement', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('jpo', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('centrelink', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('centre link', @equivalentId)

SET @equivalentId = '3c370cc5-2fcc-4f7a-beca-21beed999f32'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('rpo', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('recruitment process outsourcing', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('onsite recruitment', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('hro', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('hr process outsourcing', @equivalentId)

SET @equivalentId = '28f6598c-3356-4db8-b4b6-4c0f990e4b22'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('od', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('organisation development', @equivalentId)

SET @equivalentId = '223d18d5-c939-405c-bda0-68fc57111312'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('cqi', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('continuous quality improvement', @equivalentId)

SET @equivalentId = '479a9941-b389-4fde-8176-1959ec677fe5'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('webmethods', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('web methods', @equivalentId)

SET @equivalentId = 'c12c7ea0-a6a0-4a67-9beb-23b2765ec9bb'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('gis', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('geographic information system', @equivalentId)

SET @equivalentId = 'e4324d8b-da17-4124-ac2d-89f749120ca6'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('hyper text', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('hypertext', @equivalentId)

SET @equivalentId = '3004a0b5-b4f2-40b2-8026-09c354a7fa13'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('3d', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('3 dimensional', @equivalentId)

SET @equivalentId = '3141c19d-ec0b-4042-9270-73e9282600b8'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('mainframe', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('main frame', @equivalentId)

SET @equivalentId = 'ed3c14f1-ee2f-4bb0-a84f-c30c3f365ced'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('scrum', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('agile development', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('agile software development', @equivalentId)

SET @equivalentId = 'b278275a-5b8d-47f6-bbf0-25c95900b26a'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('biztalk', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('biz talk', @equivalentId)

SET @equivalentId = '8a45efdf-96af-407e-9e2e-1edd26ed8bea'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('database', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('data base', @equivalentId)

SET @equivalentId = 'c45234b5-cdca-49c2-9eed-98dc9f2b0032'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('c#', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('c sharp', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('csharp', @equivalentId)

SET @equivalentId = '21697bd1-de9d-40c2-ba47-b5dd1a2dc937'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('.net', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('dot net', @equivalentId)

SET @equivalentId = 'ad1b0293-c8d6-49e7-966c-1136dddf290f'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('gui', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('graphic user interface', @equivalentId)

SET @equivalentId = '3a247dd9-d273-4a50-af67-79cad3e4966a'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('power builder', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('powerbuilder', @equivalentId)

SET @equivalentId = 'c0c847dd-5f80-442b-a984-38f040d88df0'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('cti', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('computer technology integration', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('computerised technology integration', @equivalentId)

SET @equivalentId = '9651bd59-e4e0-42aa-b578-332a353119d2'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('soa', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('service oriented architecture', @equivalentId)

SET @equivalentId = '51efde58-093a-42ff-a71a-fab7b050449b'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('iso', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('international standards association', @equivalentId)

SET @equivalentId = '3de3a847-f148-474e-b000-f9f96834ebec'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('rem', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('remuneration', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('salary', @equivalentId)

SET @equivalentId = 'fbc9b8db-c868-4fd1-bb32-569abe132b95'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('smsf', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('self manager super fund', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('self manager superannuation fund', @equivalentId)

SET @equivalentId = '93c91e19-efed-4ed1-91a3-42662c57549d'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('worksafe', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('work safe', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('work cover', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('workcover', @equivalentId)

SET @equivalentId = 'ff80123d-c70d-435c-a7a3-5fe55de617de'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('j2ee', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('j2e', @equivalentId)

SET @equivalentId = '75b061c6-e691-40f9-b617-e80bf0e31ed6'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('sql', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('structured query language', @equivalentId)

SET @equivalentId = 'babf494b-31f5-4cbf-85cb-67eca5e82cb5'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('javascript', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('java script', @equivalentId)

SET @equivalentId = 'f22aec3e-7f9d-4004-944b-86063b3baf43'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('4gl', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('4th general language', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('forth general language', @equivalentId)

SET @equivalentId = '100b7642-1b41-485a-865b-57939d0778ee'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('sqlserver', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('sql server', @equivalentId)

SET @equivalentId = 'd3709e56-2bdb-4f95-80c5-95930f5fca29'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('datawarehouse', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('data warehouse', @equivalentId)

SET @equivalentId = '42100c5d-7290-4644-bbc7-536ed370250c'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('gl', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('general ledger', @equivalentId)

SET @equivalentId = '45dc0881-b2a1-4bc2-b2c6-7f5ce6106765'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('ldap', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('lightweight directory access protocol', @equivalentId)

SET @equivalentId = 'ab57687b-6345-497f-8359-36f3325ec4e0'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('records clerk', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('records officer', @equivalentId)

SET @equivalentId = '7578be0a-c3dc-421b-9af9-3c656576fe9e'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('inhouse', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('in house', @equivalentId)

SET @equivalentId = '24664fe2-da6b-4d95-992d-aa18148690e3'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('womenswear', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('womens wear', @equivalentId)

SET @equivalentId = 'bb3360c6-0255-4234-a5ff-951848f5ddf0'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('menswear', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('mens wear', @equivalentId)

SET @equivalentId = '166b5aa3-b6a0-4781-8863-697ced728a7a'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('cnc', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('computer numeric conrol', @equivalentId)

SET @equivalentId = 'c9cb3822-1eff-46bc-b70f-85d3af24ec45'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('toolmaker', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('tool maker', @equivalentId)

SET @equivalentId = '7e00ca16-4fd4-4b36-8f81-5befba1f033c'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('datacentre', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('data centre', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('data center', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('datacenter', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('hosting center', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('hosting centre', @equivalentId)

SET @equivalentId = 'cb91c837-c043-4f16-b5be-1afd309901f2'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('mysql', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('my sql', @equivalentId)

SET @equivalentId = 'a63760c2-3602-46d0-9e61-019992eae21c'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('as400', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('as 400', @equivalentId)

SET @equivalentId = '297287e5-4173-48d5-bafe-2bb4d9951f80'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('ccnp', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('cisco certified network professional', @equivalentId)

SET @equivalentId = 'f4c4db1b-c4dd-4184-90c1-e9bfdc9dbb83'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('ccna', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('cisco certified network associate', @equivalentId)

SET @equivalentId = '862c9fd3-a301-412b-879d-7136741cc51d'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('msce', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('microsoft certified systems engineer', @equivalentId)

SET @equivalentId = 'a7d0276d-c21d-4a65-8816-ccac660f9e5d'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('noc', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('netwrok oprations centre', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('network operations center', @equivalentId)

SET @equivalentId = '71c4333f-4504-4fb3-b289-1756e4fb9c8f'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('san', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('storage area network', @equivalentId)

SET @equivalentId = '80054df4-dc78-4153-b4c2-f82714f4ea0a'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('pstn', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('public switched telephone network', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('public switched telephony network', @equivalentId)

SET @equivalentId = '10d9ccdd-5d13-4b24-8ad7-153b3660d838'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('fr', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('frame relay', @equivalentId)

SET @equivalentId = 'e12ad0f7-49b8-47a2-b82c-7abcc06040f1'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('level 1', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('level1', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('1st level', @equivalentId)

SET @equivalentId = '524d4df0-0960-4545-aed5-3f5795313ab6'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('level 2', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('level2', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('2nd level', @equivalentId)

SET @equivalentId = '428e0f87-1726-4b0c-ab78-57dd523b124d'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('level 3', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('level3', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('3rd level', @equivalentId)

SET @equivalentId = '91c3ec93-4966-499a-a888-0f59bdc3172b'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('desktop', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('desk top', @equivalentId)

SET @equivalentId = 'b3d2b927-3b01-443a-81fd-7507cf0f0c69'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('pc', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('personal computer', @equivalentId)

SET @equivalentId = '5e9ce471-9348-4084-a480-9a6427258ced'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('blackbelt', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('black belt', @equivalentId)

SET @equivalentId = 'd763d719-c7bd-4bac-b53b-c874de07c87c'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('checkpoint', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('check point', @equivalentId)

SET @equivalentId = '2568f935-d0a5-4e90-a1ce-43bf39cffa58'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('firewall', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('fire wall', @equivalentId)

SET @equivalentId = '60307e93-15a0-4ea6-aa15-a3c86805b7e4'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('pki', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('public key infrastructure', @equivalentId)

SET @equivalentId = 'c3b90ac1-e6a5-46de-87a7-0cacbd0e884e'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('ipsec', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('ip sec', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('ip security', @equivalentId)

SET @equivalentId = '97b28abd-646c-45a3-994e-177ccd567a93'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('coax', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('coaxial', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('co ax', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('co axial', @equivalentId)

SET @equivalentId = 'cce3c057-c8f0-41b3-8f1d-2bd8b3420d2a'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('fibre', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('fiber', @equivalentId)

SET @equivalentId = 'ec6cf38c-4b59-43ea-8a7c-1d792dac25c5'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('itil', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('information technology infrastructure library', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('it infrastructure library', @equivalentId)

SET @equivalentId = '7920ea07-df9d-46b3-a817-90da397e364b'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('pipeline', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('pipe line', @equivalentId)

SET @equivalentId = '0e2e74d8-25b0-4f71-bf94-f3fa687df17d'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('realestate', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('real estate', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('property', @equivalentId)

SET @equivalentId = 'cc93f0f9-59d6-4ab9-815c-5dbd75d74793'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('facility', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('facilities', @equivalentId)

SET @equivalentId = '8e5639d3-9916-4faf-9250-b28721b43556'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('stocktake', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('stock take', @equivalentId)

SET @equivalentId = '41b2e8d7-59a8-4abd-960e-e699f48669f1'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('mobile phone', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('mobile telephony', @equivalentId)

SET @equivalentId = '202abda9-422c-4cb0-97c0-dc4e0fc0ccfb'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('refridgeration technician', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('refridgeration mechanic', @equivalentId)

SET @equivalentId = 'ce97230e-97b3-4c56-b54c-84c553c2e144'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('cabinetmaker', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('cabinet maker', @equivalentId)

SET @equivalentId = '39544d3c-1ddd-44d7-9689-f02e06b6ed56'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('hv', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('high voltage', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('highvoltage', @equivalentId)

SET @equivalentId = 'f7699d60-0d21-4b97-a41b-b663ce542ac7'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('landscaper', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('landscape gardner', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('landscape architect', @equivalentId)

SET @equivalentId = 'f94939ee-9234-49f3-ae0b-100dd80c964c'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('plumber', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('gas fitter', @equivalentId)

SET @equivalentId = '11576278-8040-4b66-8e07-d077b6b8be1d'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('security installer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('security technician', @equivalentId)

SET @equivalentId = '9074c69e-64a9-471e-9a96-2348796c0c96'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('supplychain', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('supply chain', @equivalentId)

SET @equivalentId = 'd43b1731-7725-4577-9198-7ea6e97ba99c'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('marine', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('maritime', @equivalentId)

SET @equivalentId = 'ed59f2c8-5e0d-4aa8-9cd5-7de7895f5673'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('div1', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('division 1', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('div 1', @equivalentId)

SET @equivalentId = '38e97944-2c0b-4599-83e2-9b52d741067e'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('div2', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('division 2', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('div 2', @equivalentId)

SET @equivalentId = '9932fa3a-f689-4fbe-84b2-97d11d1e6898'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('div3', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('division 3', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('div 3', @equivalentId)

SET @equivalentId = 'd9533ab1-02ad-4c6e-ae44-374c021dfd3f'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('marine engineer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('maritime engineer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('marine technician', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('maritime technician', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('tugboat engineer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('tug boat engineer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('ship engineer', @equivalentId)

SET @equivalentId = 'af1b477a-908a-476b-b942-00da5e458be2'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('air traffic controller', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('airtraffic', @equivalentId)

SET @equivalentId = '9696e96b-d33a-4bca-a4c8-d37496172c61'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('deckhand', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('deck hand', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('integrated rating', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('able bodied seaman', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('greaser', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('ab', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('general purpose hand', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('gp hand', @equivalentId)

SET @equivalentId = '7aef1c7b-4b4d-4cda-98b0-e70cdebddc90'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('skipper', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('master v', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('master 5', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('master 4', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('master4', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('master5', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('masterv', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('coxswain', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('captain', @equivalentId)

SET @equivalentId = 'cc909e59-814b-4f4c-a29c-414017e42156'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('cabin crew', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('cabincrew', @equivalentId)

SET @equivalentId = '16e29ee2-c0b2-463c-bb20-2d5c8a4ad624'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('steward', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('caterer', @equivalentId)

SET @equivalentId = '5b67ca4f-dc6d-4f65-87e2-f8d931b4becf'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('course super', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('course superintendant', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('golfcourse superintendant', @equivalentId)

SET @equivalentId = 'b1bd72e5-14f4-4abd-99ad-5c9997bc438d'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('golf pro', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('golf professional', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('golfpro', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('pga professional', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('golf coach', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('golfing professional', @equivalentId)

SET @equivalentId = '6fa67ffb-d3b5-458b-949c-be07c30bc77f'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('pga', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('professional golfers association', @equivalentId)

SET @equivalentId = 'a5721797-76ca-445f-88b2-3d85508b20cf'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('golf shop', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('pro shop', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('proshop', @equivalentId)

SET @equivalentId = '6a119826-428a-4c85-a58d-80707f9235f1'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('golf manager', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('golfclub manager', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('golf club manager', @equivalentId)

SET @equivalentId = 'a5fbaa6d-f648-41db-875d-009dc096ba3b'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('peoplesoft', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('people soft', @equivalentId)

SET @equivalentId = 'd8ca7d19-fdda-453a-a525-d24e5bcf18dc'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('ps146', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('ps 146', @equivalentId)

SET @equivalentId = 'bac49b68-86de-45d4-b660-787fdbcd2ae5'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('b2b', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('b 2 b', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('business to business', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('business 2 business', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('business2busines', @equivalentId)

SET @equivalentId = '2cee7d49-f9e2-4c00-bb07-1fb51a54539b'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('b2c', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('b 2 c', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('business to consumer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('business 2 consumer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('business2consumer', @equivalentId)

SET @equivalentId = '80c0023d-c66a-4680-ae48-d5d8cb9331ec'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('psa', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('p s a', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('preferred supplier agreement', @equivalentId)

SET @equivalentId = 'eabe549e-8456-4490-a62d-93604a7114bc'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('maternal health', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('mothercraft', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('mother craft', @equivalentId)

SET @equivalentId = '48f30707-8038-460f-886f-b3f0ee3413f7'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('hk', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('h k', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('hongkong', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('hong kong', @equivalentId)

SET @equivalentId = '3c1ed695-fe77-4180-bcab-2b6d385c5573'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('png', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('papua new guinea', @equivalentId)

SET @equivalentId = '65748ec8-2c5f-421c-9552-c1ceb7a1b506'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('heathfood', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('health food', @equivalentId)

SET @equivalentId = '4bca1c0d-e21c-4791-9595-b51ad08ce797'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('hifi', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('hi fi', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('high fidelity', @equivalentId)

SET @equivalentId = '7a4906c1-88b8-4c25-b1f4-3230e7b9fe67'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('line haul', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('linehaul', @equivalentId)

SET @equivalentId = '224ae650-0e3b-4228-ad5d-d97135fae60e'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('primus', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('iprimus', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('primus telecom', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('primus telecommunications', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('primustelecom', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('primustel', @equivalentId)

SET @equivalentId = '8ae0d7b1-e971-4549-8a58-df319f402e1f'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('optus', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('singtel', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('singteloptus', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('singtel optus', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('singapore telecom', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('singapore telecommunications', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('sing tel', @equivalentId)

SET @equivalentId = '798f9107-6a77-4ad7-acc6-b10f3c49be6e'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('melbourne uni', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('melbourneuni', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('melbourne university', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('university of melbourne', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('universityofmelbourne', @equivalentId)

SET @equivalentId = '46e89488-4cfc-4104-a75e-b8bce6a3bfcc'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('sydney uni', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('sydneyuni', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('sydney university', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('university of sydney', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('universityofsydney', @equivalentId)

SET @equivalentId = '6d0033f9-d7b7-4b2f-941c-fc1a1e7d535e'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('uni of queensland', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('university of queensland', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('queensland uni', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('qld uni', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('qld university', @equivalentId)

SET @equivalentId = 'fa710ac9-aa1a-4393-8e6d-5f5e0ca14cd2'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('acu', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('australian catholic university', @equivalentId)

SET @equivalentId = '6ae9afe9-9292-45dd-872c-7123f8433a4a'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('bond uni', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('bond university', @equivalentId)

SET @equivalentId = '20a35b91-195c-4a58-89c6-6dadfa4b7b20'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('cdu', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('charles darwin university', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('charles darwin uni', @equivalentId)

SET @equivalentId = 'c612aa3f-287a-4804-bd28-7e596744c88c'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('csu', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('charles sturt university', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('charles sturt un', @equivalentId)

SET @equivalentId = '29b67b7e-bea3-4117-ae1b-d249a9f0e701'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('ecu', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('edith cowan university', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('edith cowan uni', @equivalentId)

SET @equivalentId = 'ff7e1111-a374-4701-a92b-f04f40ddc874'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('jcu', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('james cook university', @equivalentId)

SET @equivalentId = '36fa9e49-3a26-4110-94f2-adbe75e4a99b'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('latrobe', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('la trobe', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('latrobe university', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('latrobe uni', @equivalentId)

SET @equivalentId = '03320b3e-fd3c-4ac2-a784-4cb74b69d35f'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('scu', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('southern cross university', @equivalentId)

SET @equivalentId = '27df1ceb-6067-4f99-9c5a-6a853b44440e'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('swinburn uni', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('swinburn university', @equivalentId)

SET @equivalentId = 'f7ee2c29-871b-41d9-98d5-e328945da1b7'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('university of ballarat', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('ballarat uni', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('ballarat university', @equivalentId)

SET @equivalentId = 'c22ba4f4-8aec-4289-9d90-427c73543f96'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('university of canberra', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('canberra uni', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('canberra university', @equivalentId)

SET @equivalentId = '25e36b78-d389-4f2c-ba27-5efde8cc26e1'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('une', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('university of new england', @equivalentId)

SET @equivalentId = '31bd09c1-3e7b-4781-a24b-aea487923790'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('university of newcastle', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('newcastle uni', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('newcastle university', @equivalentId)

SET @equivalentId = 'e8c753b3-2de1-4af1-9592-7b30ee360261'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('unda', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('university of notre dame australia', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('university of notredame australia', @equivalentId)

SET @equivalentId = 'e37bd6aa-b5a3-4f86-862e-71ebc80f4732'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('unisa', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('iniversity of south australia', @equivalentId)

SET @equivalentId = '25ef3fd1-7e9b-4fa3-b49c-2d2c79cfcbe8'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('usq', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('university of southern queensland', @equivalentId)

SET @equivalentId = '9a436a46-f7bd-4418-b76b-367529940b8d'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('university of tasmania', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('uni of tasmania', @equivalentId)

SET @equivalentId = '86d73603-1348-4c51-8009-54b8ffc94249'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('usc', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('university of the sunshine coast', @equivalentId)

SET @equivalentId = '06096c9e-8938-432d-bfc3-ec6bd501b8f0'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('uow', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('university of wollongong', @equivalentId)

SET @equivalentId = 'd319b120-63f3-4d0d-93d5-43da5dfc0a19'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('vu', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('victoria university', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('victoria uni', @equivalentId)

SET @equivalentId = '85aafbbd-1eec-4e19-bd69-44b9c459a5df'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('goldcoast', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('gold coast', @equivalentId)

SET @equivalentId = '6e2807f1-4ebf-48ac-8640-ab7a38cfcb14'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('sunshinecoast', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('sunshine coast', @equivalentId)

SET @equivalentId = 'd1b0d44b-c188-4cd0-9aa6-d2b70fdf39e9'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('tassie', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('tasmania', @equivalentId)

SET @equivalentId = 'fb233593-3a79-4339-9a36-46d05e42ceb0'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('mbs', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('melbourne business school', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('m b s', @equivalentId)

SET @equivalentId = 'e5858b52-37ae-469b-a82a-b3a73f208a4e'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('news ltd', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('newsltd', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('news limited', @equivalentId)

SET @equivalentId = '91147c76-c4ff-44ad-95aa-c656526d2d74'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('qut', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('queensland university of technology', @equivalentId)

SET @equivalentId = '51483473-bf88-4607-8b7d-50602a99be26'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('rmit', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('royal melbourne institute of technology', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('rmit university', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('rmit uni', @equivalentId)

SET @equivalentId = 'abaf7b90-54ce-42af-b7b2-f913dedc6f4c'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('inbound', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('in bound', @equivalentId)

SET @equivalentId = 'd9b06d95-f740-4302-b854-9661fcdbf505'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('adelaide university', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('university of adelaide', @equivalentId)

SET @equivalentId = '48060f99-702d-4880-9d8b-7380b7f0ff35'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('anu', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('a n u', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('australian national univeristy', @equivalentId)

SET @equivalentId = '1a477234-03d7-4e45-a94b-c54cb806442f'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('unsw', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('university of new south wales', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('u nsw', @equivalentId)

SET @equivalentId = 'd7e13a00-039c-4bf8-a1bd-da156668bee1'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('uws', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('university of western sydney', @equivalentId)

SET @equivalentId = '6afb254f-f354-4531-99ca-8f5d318cb501'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('monash uni', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('monash university', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('monashuni', @equivalentId)

SET @equivalentId = 'fa1643e1-5cce-4772-b262-cb69b252d5cd'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('deakin uni', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('deakin university', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('deakinuni', @equivalentId)

SET @equivalentId = 'f6bd76d7-35b6-4985-8905-de2f3b03a861'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('officeworks', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('office works', @equivalentId)

SET @equivalentId = 'f4a24266-89bf-403e-adb6-99601214db4f'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('coles', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('coles group', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('colesmyer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('coles myer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('cml', @equivalentId)

SET @equivalentId = '6c547427-6152-4842-97be-5f75523c73ce'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('wool worths', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('woolworths', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('safeway', @equivalentId)

SET @equivalentId = '168774a7-3605-4763-ab33-d46439b070c6'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('mc donalds', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('mcdonalds', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('macdonalds', @equivalentId)

SET @equivalentId = '07fe7163-c76b-4fe2-b020-bd33597c422e'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('spotless', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('spotless catering', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('spotless group', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('spotless services', @equivalentId)

SET @equivalentId = '8875831c-40c5-47fe-8f9d-b10cd88a9cb6'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('ramsayhealth', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('ramsay health', @equivalentId)

SET @equivalentId = 'c98dace8-ea66-46d3-83e9-30a83c7814d1'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('worley parsons', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('worleyparsons', @equivalentId)

SET @equivalentId = 'c894dc86-0bae-468d-9c82-15daafa58099'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('racv', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('royal automobile club of victoria', @equivalentId)

SET @equivalentId = '56eeae2e-09da-4481-b7fc-b16cb8e6ad6f'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('mcc', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('melbourne cricket club', @equivalentId)

SET @equivalentId = '88fa2b40-8abf-4d53-8251-b2acb5741490'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('mcg', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('melbourne cricket ground', @equivalentId)

SET @equivalentId = '75357713-340e-4ddd-84fa-a42d82f7a33e'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('scg', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('sydney cricket ground', @equivalentId)

SET @equivalentId = 'fece989c-c836-4831-a935-681c6e7f1bb6'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('cocacola', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('coca cola', @equivalentId)

SET @equivalentId = 'af01e499-039f-4473-9714-b22ad15fa212'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('blue scope', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('bluescope', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('bluescope steel', @equivalentId)

SET @equivalentId = '5e6b03ff-7fe3-4c3b-87aa-53d8ab71e2e5'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('salvos', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('salvation army', @equivalentId)

SET @equivalentId = 'a9763b44-965e-4113-b792-175f7c4a704f'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('gm holden', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('beneral motors holden', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('general motors', @equivalentId)

SET @equivalentId = 'ecfe1f7f-2409-4d7c-8ac0-79085d2c5eb1'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('justjeans', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('just jeans', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('just group', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('justgroup', @equivalentId)

SET @equivalentId = '3b7e498b-781f-419a-a0d4-175ca0e39a1e'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('pbl', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('publishing and broadcasting', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('publishing & broadcasting', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('publishing&broadcasting', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('pbl ltd', @equivalentId)

SET @equivalentId = '1ef6179e-c2ca-46e6-b7d8-f3409e8d276e'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('macq bank', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('macquarie bank', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('macquarrie bank', @equivalentId)

SET @equivalentId = '2afcdad3-294d-4953-a144-ad46253db5ef'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('virgin blue', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('virginblue', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('virginblue.com.au', @equivalentId)

SET @equivalentId = '36829777-8d28-4f23-9d9b-5fab4c4bb2df'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('mitre10', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('mitre 10', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('mitre ten', @equivalentId)

SET @equivalentId = 'd2238e20-7ad0-42ae-bed3-d9868508f720'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('starcity', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('star city', @equivalentId)

SET @equivalentId = '61ca9b6b-f2d8-4cf5-aea8-c8a69492cfc7'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('dicksmith', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('dick smith', @equivalentId)

SET @equivalentId = '59424c07-8409-4952-855f-d5b146fa1d5a'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('seek', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('seek.com.au', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('seek.com', @equivalentId)

SET @equivalentId = '72d73196-a43a-45ef-a5ae-4d7aac899cfa'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('yahoo', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('yahoo7', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('yahoo.com.au', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('yahoo7.com.au', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('yahoo 7', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('yahoo7!', @equivalentId)

SET @equivalentId = '819733b8-3b77-46d4-b10a-5fed66ef3745'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('armaguard', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('arma guard', @equivalentId)

SET @equivalentId = '436aea44-beba-4781-9306-4ca25e812082'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('ge', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('general electric', @equivalentId)

SET @equivalentId = '0e356907-ebb7-458d-a527-0f7bd3c4f712'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('maanz', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('marketing association of australia and new zealand', @equivalentId)

SET @equivalentId = 'bd7f0b93-0df8-444c-ada6-7f6ed0972553'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('mia', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('marketing institute of austrlalia', @equivalentId)

SET @equivalentId = '17c73a28-0df7-4aab-a188-b10e83c9c209'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('aempe', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('association of marine and power engineers', @equivalentId)

SET @equivalentId = '59735ea6-708c-4f43-b686-03edd7a8e943'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('vacc', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('victorian automotive chamber of commerce', @equivalentId)

SET @equivalentId = 'ae2ce500-c5fa-45e6-bdc0-1e2e707bda94'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('sae', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('society of automotive engineers', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('society of automotive engineers australasia', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('saea', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('sae-a', @equivalentId)

SET @equivalentId = '7d25447e-0ae9-412e-8ec4-2fd71fe5ac7c'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('ahri', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('austrlalian human resources institute', @equivalentId)

SET @equivalentId = '77e5ba70-079a-430c-87f0-5c32c77ec9ad'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('rcsa', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('recruitment and consulting services association', @equivalentId)

SET @equivalentId = '7998b7a2-6bba-4ca8-a25e-493be854b994'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('gta', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('group training australia', @equivalentId)

SET @equivalentId = '5dc7209d-e711-4a0a-9a60-174a18c9ddc4'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('yellowpages', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('yellow pages', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('yellowpages.com.au', @equivalentId)

SET @equivalentId = 'e27a3173-925a-4688-8004-e9c421ca389b'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('whitepages', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('white pages', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('whitepages.com.au', @equivalentId)

SET @equivalentId = '7d4512cd-a810-432d-b6a4-d1c1c6755e28'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Hyperbaric Technician', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Hyperbaric operator', @equivalentId)

SET @equivalentId = 'b7cd2119-0ce0-447a-a00b-b1b58e481180'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('license', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('licence', @equivalentId)

SET @equivalentId = '28398e23-b1ce-4354-8991-c08eed2c45c8'

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

SET @equivalentId = 'e5c95c97-8495-40e4-a6c6-f0830a00383f'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('asfa', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('association of superannuation funds of australia', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('superannuation association', @equivalentId)

SET @equivalentId = '4f4e4840-f97b-4564-95de-820bbad83d65'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('retailer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('retail company', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('retail organisation', @equivalentId)

SET @equivalentId = '63c43d68-b4c3-4108-a2a5-26b4f52336fa'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('valuer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('valuation', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('property valuer', @equivalentId)

SET @equivalentId = 'd95aff19-a13d-472e-87ee-6d99cf193be8'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('ara', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('australian retailers association', @equivalentId)

SET @equivalentId = 'f96e337f-05d0-433a-b345-dad319a5f47c'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('horticulturalist', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('horti culturalist', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('horticulture', @equivalentId)

SET @equivalentId = '5af749fc-4a73-4b0f-9c56-f1286356d3f3'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('bdouble', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('b double', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('road train', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('roadtrain', @equivalentId)

SET @equivalentId = '6e8cdaf1-6c22-4269-a7ef-8f5c6c31b8c7'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('crane driver', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('crane operator', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('crane truck driver', @equivalentId)

SET @equivalentId = '30e51d5a-89e4-475d-a832-3b63c9bc05b4'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('rigger', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('dogman', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('dog man', @equivalentId)

SET @equivalentId = '0316562b-5ab7-447b-84dc-78f11b004568'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('postman', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('post man', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('postie', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('postal officer', @equivalentId)

SET @equivalentId = 'cbc7d72c-d97f-47f6-9db5-8a81f77559a3'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('carpark', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('car park', @equivalentId)

SET @equivalentId = '65009eea-5af1-4cda-854b-56f7d1f8c0e2'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('carpark attendant', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('carpark operator', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('car park attendant', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('car park operator', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('parking attendant', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('parking staff', @equivalentId)

SET @equivalentId = '73f18ad0-9e6c-4e02-b05c-0b313708693b'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('formwork', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('form work', @equivalentId)

SET @equivalentId = '213b7706-44ac-451f-a022-41bb775fa043'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('agribusiness', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('agri business', @equivalentId)

SET @equivalentId = '59c1427d-0cba-4a01-91a7-2c0f44f3c52d'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('freelance', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('free lance', @equivalentId)

SET @equivalentId = 'ac0cd154-3ccf-4564-8ba0-a9e801c5644c'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('voip', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('voice over internet protocol', @equivalentId)

SET @equivalentId = '59fe175e-3d74-4b49-b251-190b673e93d8'

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

SET @equivalentId = 'fe9f3068-fbb8-4d61-9072-0f6de8178668'

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

SET @equivalentId = 'd2f8dc88-59ac-4d6d-adda-ea27de118e2d'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('aquatic coordinator', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('aquatic facility assistant', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('pool operations manager', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('pool operations co ordinator', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('pool operations coordinator', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('pool superintendant', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('pool operations attendant', @equivalentId)

SET @equivalentId = '477d249a-2cae-4ff3-bc64-e2cb87aa82f4'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('pool builder', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('pool installer', @equivalentId)

SET @equivalentId = '794d7a11-f3ca-40eb-8ae1-0ca3e077204f'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('poolman', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('pool man', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('pool cleaner', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('pool technician', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('pool maintenance', @equivalentId)

SET @equivalentId = 'e9327942-90b9-49c9-99c4-7c64a4d416bd'

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

SET @equivalentId = '3a698751-5e1c-434e-8152-17f1f0a81aa7'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('recreation centre manager', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('fitness centre manager', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('swim centre manager', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('rec centre manager', @equivalentId)

SET @equivalentId = 'e730b8d3-b587-4620-afe0-76e706e5e5c7'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('cpr', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('cardio pulmonary resuscitation', @equivalentId)

SET @equivalentId = '95a5fbf4-546a-49db-a74b-bcb75277e068'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('environmental', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('sustainability', @equivalentId)

SET @equivalentId = '60545dc2-20f8-4541-8cd0-129f1ae4ff92'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('vacation', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('holiday', @equivalentId)

SET @equivalentId = '45eae707-abd7-48f9-8417-0d7bab6a2c8e'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('parking inspector', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('traffic officer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('council parketing inspector', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('infringements officer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('parking enforcement officer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('car parking officer', @equivalentId)

SET @equivalentId = '1e9185a1-8737-4bcd-827e-a4e98d721316'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('carpark manager', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('carpark supervisor', @equivalentId)

SET @equivalentId = 'aa67ee2c-6f1d-4fad-af96-82d96cb25aa4'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('catering supervisor', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('catering manager', @equivalentId)

SET @equivalentId = '56df17d6-be0f-4f1b-a438-b990345817f4'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('catering assistant', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('catering service assistant', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('catering officer', @equivalentId)

SET @equivalentId = '56560b74-1214-49c2-86bd-ecddefa064bf'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('rifle', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('gun', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('firearm', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('fire arm', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('shotgun', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('pistol', @equivalentId)

GO
