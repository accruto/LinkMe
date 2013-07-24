DECLARE @equivalentId UNIQUEIDENTIFIER

DELETE FROM dbo.EquivalentTerms

SET @equivalentId = '0ef89baa-50f2-4cce-9a77-52453177b119'

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

SET @equivalentId = '325d3821-e48f-4828-8f40-2a7f18496be4'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Executive Chairman', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Exec Chairman', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('E.C.', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('EC', @equivalentId)

SET @equivalentId = '1775faa7-4e34-4885-a399-1da7f8a67ad0'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('General Manager', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('GM', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('G.M.', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Group General Manager', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('GGM', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('G.G.M.', @equivalentId)

SET @equivalentId = '97b9e942-2a1c-45b7-8bb5-9b6be0ddfa6f'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Chief Operating Officer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('COO', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('C.O.O.', @equivalentId)

SET @equivalentId = '0db4eac3-e799-4009-bdfe-10c56a0e3e2f'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('vp', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('v.p.', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('vice president', @equivalentId)

SET @equivalentId = '2d8e453b-d6fa-44be-b1e3-47f3f7b346d7'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('senior vp', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('senior v.p.', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Senior Vice President', @equivalentId)

SET @equivalentId = '2c9d1163-64a1-4e57-86fc-7451d9e445fe'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('2ic', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('2.i.c.', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('2i.c.', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Second in charge', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('2 IC', @equivalentId)

SET @equivalentId = '7f7cccc1-7082-42b1-a9b2-36ed388d1962'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Company Director', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Dir', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Dir.', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('director', @equivalentId)

SET @equivalentId = '1f708729-89a8-408e-b9e8-cb021aa0ac29'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('non executive director', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('non exec director', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('non-exec director', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('non exec dir.', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('non-exec dir.', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('ned', @equivalentId)

SET @equivalentId = 'f4f2d428-0ac7-4be2-bff7-26a59dac9cd1'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('company secretary', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('company sec.', @equivalentId)

SET @equivalentId = 'dc06e096-e866-4586-8c63-9c64012489c8'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Chairman', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('chairperson', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('chairwoman', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('chair man', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('chair person', @equivalentId)

SET @equivalentId = '580ac8e1-2704-4401-85e8-49ffbe12977a'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('CIO', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('C.I.O.', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Chief Information Officer', @equivalentId)

SET @equivalentId = '02b66d48-3408-4032-8a82-634b98e43178'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('manager', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('mgr', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('manger', @equivalentId)

SET @equivalentId = '025380ec-7981-467b-8f93-642785f85d9d'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('State Manager', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('statemanager', @equivalentId)

SET @equivalentId = '4a908fd3-1bd4-426d-bbd4-366dac86082c'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Manager Governance', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Manager Accountability', @equivalentId)

SET @equivalentId = '4d93d0c3-4f4d-46f3-a8b4-015ba60446df'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Program Director', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Director program', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('program manager', @equivalentId)

SET @equivalentId = '2c9a0b83-b2cd-4530-b6fc-66d02fdfc143'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Assistant Manager', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('asst mgr', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('asst manager', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Assistant Manger', @equivalentId)

SET @equivalentId = '56165069-a13c-4e60-9f19-1188f4d477e3'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Senior Supervisor', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Snr Surpervisor', @equivalentId)

SET @equivalentId = '3849b7d2-c737-49bc-95bb-e1db6a61a379'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Assistant Supervisor', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Asst Supervisor', @equivalentId)

SET @equivalentId = 'd8d26e3a-13f9-42c8-aa8f-2ffb741457e7'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Team Leader', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Team lead', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Teamleader', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('supervisor', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('foreman', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('fore man', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('foreperson', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('fore person', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('team manager', @equivalentId)

SET @equivalentId = '2e1e47fd-8680-4364-9bce-df84dd33725e'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Project Manager', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Project Mgr', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('PM', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Project Manger', @equivalentId)

SET @equivalentId = 'b9d283de-bc6b-4492-9336-d6451d5f1434'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('General Manager Project', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Project Director', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('project coordinator', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Senior Project Manager', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Senior Project director', @equivalentId)

SET @equivalentId = 'd3962c08-693e-471b-96ca-1a4cf2946464'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('BPM', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('B.P.M.', @equivalentId)

SET @equivalentId = 'fb5167f0-b196-4c3a-8fb9-8160a331e0c5'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Assistant Project Manager', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Asst Project Manager', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Asst Project Mgr', @equivalentId)

SET @equivalentId = '68d1dde4-fe15-4021-b645-9f476eba79b0'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Junior Project Manager', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Trainee Project Manager', @equivalentId)

SET @equivalentId = '80ce6c1a-520b-4a85-8bf4-c876372367f7'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('inhouse consultant', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('in-house consultant', @equivalentId)

SET @equivalentId = '52c9b840-fe0b-4fc6-8ad3-62899890140d'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('BPR consultant', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('business process re-engineering', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('bpr', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('business process reengineering', @equivalentId)

SET @equivalentId = '3d83b832-c846-48f6-8c7b-bd94a24716db'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('DM', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('D.M.', @equivalentId)

SET @equivalentId = '1eccf270-a852-4379-8f0d-13c98d6cafdc'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Manager Planning', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Manager- Planning', @equivalentId)

SET @equivalentId = '07e844c4-f230-40ec-aa01-eaf5ce7de450'

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

SET @equivalentId = '88b058c1-574c-49b5-858b-ebe3bccd3fa4'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('gm finance', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('g.m. finance', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('general manager finance', @equivalentId)

SET @equivalentId = '7b77206e-fa59-4f0b-b193-cab824e14183'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('vice president finance', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('vp finance', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('v.p. finance', @equivalentId)

SET @equivalentId = 'bb896605-4dc3-4330-9839-146ae97fb5de'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Assistant C.F.O.', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Assistant CFO', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Asst C.F.O.', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Asst CFO', @equivalentId)

SET @equivalentId = '292f1883-8645-4ab5-a53b-64adc94e8ddd'

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

SET @equivalentId = 'a6d5beb3-8c25-4d7b-ac90-9c87b2fe9c4b'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Senior Financial Controller', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Sr. Financial Controller', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Sr Fin Controller', @equivalentId)

SET @equivalentId = '7d254497-42cc-4f17-9490-788e96f2ff1d'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Assistant Financial Controller', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Asst Financial Controller', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Asst Fin Controller', @equivalentId)

SET @equivalentId = 'b9e6820a-5c66-4d43-9880-a85130878c8c'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Regional Financial Controller', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Regional Finance Controller', @equivalentId)

SET @equivalentId = 'a3ba7343-3273-43e7-8cc4-2c612691f711'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('National Financial Controller', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('National Finance Controller', @equivalentId)

SET @equivalentId = 'c104c2b7-1ad0-4232-a9a2-6608a59af73c'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Accounting Manager', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Accounts Mgr', @equivalentId)

SET @equivalentId = 'fa5aeb8c-1cf6-4bc8-b390-49c2373b95b2'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Senior Finance Accountant', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Sr Finance Accountant', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Sr. Finance Accountant', @equivalentId)

SET @equivalentId = '829649e4-31a0-434b-a506-64ffd68c4161'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Senior Financial Accountant', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Sr Financial Accountant', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Sr. Financial Accountant', @equivalentId)

SET @equivalentId = '9ba00483-5e6e-4161-bfd8-eca48515f3d3'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Administration Manager', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Admin Manager', @equivalentId)

SET @equivalentId = '15258aec-cd25-47f9-a946-e113825646f7'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('National Administration Manager', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Assistant Administration Manager', @equivalentId)

SET @equivalentId = 'dbe3dea7-8173-4f69-9360-4e54ea69274c'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Senior Consultant', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Sr Consultant', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Sr. Consultant', @equivalentId)

SET @equivalentId = 'bcdfc33e-65e9-4e04-8f28-2e4d7fc7500b'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Principal Consultant', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Chief Consultant', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Managing Consultant', @equivalentId)

SET @equivalentId = 'd8fbc26a-a89c-44c8-804c-e82a45662306'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Accountant', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Acc', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Accountan', @equivalentId)

SET @equivalentId = '8a59ed50-4558-44e7-bfd7-3057eccc95a6'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Financial accountant', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Fin Acc', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Finance Accountant', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Finance Acc', @equivalentId)

SET @equivalentId = '1cd3414d-45fd-4aa2-9644-6037d6399926'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Corporate Accountant', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Corp Accountant', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Sr. Mg Accountant', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Contract Accountant', @equivalentId)

SET @equivalentId = 'a164e9b6-8fc0-4dd7-9470-2bb9811b44a1'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Business Accountant', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Bus Acct', @equivalentId)

SET @equivalentId = '61b846e5-e6c1-47d0-8026-9fb9f554713e'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Management accountant', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Management Acct', @equivalentId)

SET @equivalentId = '94ec40ae-94c0-4533-8df7-51800f26d6a7'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Senior Management Accountant', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Sr Management Accountant', @equivalentId)

SET @equivalentId = 'bd2d9d96-ad8a-4da0-8656-539f3a2ac14c'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Accounting', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Accountin', @equivalentId)

SET @equivalentId = '0fb9c907-b530-45f5-8dfe-c615fa21c34f'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('cpa', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('c.p.a.', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('certified practising account', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('certified accountant', @equivalentId)

SET @equivalentId = '1d5e0886-905c-418a-91e5-04f933f52f43'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('ca', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('c.a.', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('chartered accountant', @equivalentId)

SET @equivalentId = '9bbe24ea-41ec-4ff2-8401-f533201208dc'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Temporary Accountant', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Temp Accountant', @equivalentId)

SET @equivalentId = 'fa563357-c5bc-4ee9-aefb-cc918db14f8d'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Sr. Accountant', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Sr Accountant', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Senior Accountant', @equivalentId)

SET @equivalentId = '17ccc9ce-4fee-44b6-96ab-69a234f20620'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Financial/Management Accountant', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Assistant Management Accountant', @equivalentId)

SET @equivalentId = '8d391570-c5d8-4336-b715-a89f58e2edb1'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Junior Accountant', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Jr Accountant', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Jr. Accounts', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Junior Accounts Clerk', @equivalentId)

SET @equivalentId = 'd9a23384-9327-4385-82d8-bd2cd6e989c5'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Assistant Accountant', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Asst Accountant', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Accounts Assistant', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Acct Asst', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Accounting Assistant', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Account Assistant', @equivalentId)

SET @equivalentId = '87df9a72-65e3-4401-af83-9192b9d65376'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Accounts Clerk', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Accounts Officer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Accounts Staff', @equivalentId)

SET @equivalentId = '250791e5-a6a0-4486-9306-922ec3203426'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Project Accountant', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Proj Acct', @equivalentId)

SET @equivalentId = '50ec64b2-f01d-4773-b031-709ece0479c7'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Auditor', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('auditing', @equivalentId)

SET @equivalentId = 'c3d76739-82cc-4ccd-bf01-d49150f32948'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Audit Manager', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Audit Mgr', @equivalentId)

SET @equivalentId = 'c0d4e45b-d29f-40bf-8011-ddb8bd330d3a'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Senior Auditor', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Sr Auditor', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Sr. Auditor', @equivalentId)

SET @equivalentId = '4db93f46-84e0-4191-b291-84d844a34ea4'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Internal Auditor', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('In house Auditor', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('In-house Auditor', @equivalentId)

SET @equivalentId = 'c3e5aedc-1c24-41a5-85fe-0f3c733d98a3'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('External auditor', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Contract Auditor', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('financial auditor', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Revenue Auditor', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('State Auditor', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Statutory Accountant', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Senior Internal Auditor', @equivalentId)

SET @equivalentId = '55366e69-2ed1-4d46-95d9-0bc3f9dcefe8'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Junior Auditor', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Jr Auditor', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Jr. Auditor', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Apprentice Auditor', @equivalentId)

SET @equivalentId = 'b2bbdffc-8f5d-4381-bd78-aee3a544a799'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Quality Auditor', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('QA Auditor', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Quality Assurance Auditor', @equivalentId)

SET @equivalentId = '8860b9d1-e2bf-4dfd-ad24-b1cabdd06567'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Operations Auditor', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Process Auditor', @equivalentId)

SET @equivalentId = '81d46031-d641-406d-b25b-4d482bee343f'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Stock Auditor', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Night Auditor', @equivalentId)

SET @equivalentId = 'e68c0816-408f-4493-bd47-4bb5259bb189'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Bookkeeper', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('book keeper', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('bookeeper', @equivalentId)

SET @equivalentId = '44ed992d-7df1-4dcb-933d-2a6c4defea67'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Accountant / Bookkeeper', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Receptionist/Cashier', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Assistant Office Manager', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Bookkeeper/Office Manager', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Office Manager/Bookkeeper', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Bookkeeper/Receptionist', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Receptionist/Bookkeeper', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Accounts Clerk/Receptionist', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Receptionist/Accounts Clerk', @equivalentId)

SET @equivalentId = '15154ef1-b66f-4ae8-8a63-992881627904'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Accounts Payable Manager', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Accounts Payable Supervisor', @equivalentId)

SET @equivalentId = '7e903d16-17c7-4d82-ae46-4b9393474413'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Accounts Payable', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Accts Payable', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('AP', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Sales Ledger', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Accounts Payable Officer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Accounts Payable Clerk', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Accounts Payable Coordinator', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Accounts Payable Co-ordinator', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Accounts Payable Co ordinator', @equivalentId)

SET @equivalentId = '928ed245-5653-42b6-8bfd-9bec77759831'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Accounts Payable Assistant', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Assistant Accounts Payable', @equivalentId)

SET @equivalentId = '44929470-1e2c-4013-a062-891cbdbd8b09'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Accounts Payable/Receivable', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Accounts payable specialist', @equivalentId)

SET @equivalentId = '26e080cf-d229-4be2-a32a-fe01c31aed4b'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Reconciliation Clerk', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Reconciliation Officer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Reconciliation Assistant', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Reconciliation Asst', @equivalentId)

SET @equivalentId = 'e6b1271f-c4e0-4bd5-bbfe-335713bfb6bc'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Accounts Receivable Manager', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Accounts Receivable Supervisor', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Collections Supervisor', @equivalentId)

SET @equivalentId = 'd5a243f5-6b32-4b8d-aae8-7f0473b3210c'

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

SET @equivalentId = '5046347a-593c-4667-be3d-ff557eb6d0bf'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Accounts Receivable officer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Accounts Receivable Assistant', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Assistant Accounts Receivable', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Accounts Receivable Coordinator', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Accounts Receivable Co-ordinator', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Accounts Receivable Co ordinator', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Accounts receivable specialist', @equivalentId)

SET @equivalentId = 'c810da9b-444e-4857-9039-41f4650ee5bc'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Mgr Credit Control', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Credit Control Manager', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Credit Control Mgr', @equivalentId)

SET @equivalentId = '8af3e61b-0182-442a-be2d-7da954950abe'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Credit Controller', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Credit Control', @equivalentId)

SET @equivalentId = '1e5f32f6-0ea7-4aee-96ff-8449a38109ac'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Credit Consultant', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Credit Administrator', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Credit Officer', @equivalentId)

SET @equivalentId = '122d617a-8b2a-464c-a8e2-44b615996ef1'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Senior Credit Controller', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Sr Credit Controller', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Sr. Credit Controller', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Manager Credit Control', @equivalentId)

SET @equivalentId = 'db44646a-2a3a-4967-aeec-b822c3c12680'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Junior Credit Controller', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Jr Credit Controller', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Jr. Credit Controller', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Jnr Credit Officer', @equivalentId)

SET @equivalentId = 'c7197d44-79f2-4452-96fb-60cb6f78cb2c'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Finance Analyst', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Financial Analyst', @equivalentId)

SET @equivalentId = 'edcf4421-a0cc-4849-aca9-bd1de0185203'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Credit Analyst', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Credit Risk Analyst', @equivalentId)

SET @equivalentId = 'fa6b0bbe-2a8f-4fa3-997c-2df2b7bdfdf3'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Senior Finance Analyst', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Sr Finance Analyst', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Sr. Finance Analyst', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Senior Financial Analyst', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Sr Financial Analyst', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Sr. Financial Analyst', @equivalentId)

SET @equivalentId = '94b4f005-ba23-49cc-affd-8c10e42682a3'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Senior Credit Analyst', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Sr Credit Analyst', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Sr. Credit Analyst', @equivalentId)

SET @equivalentId = '28ceb713-9c96-418c-a25d-5fe7dd7b20fe'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Junior Finance Analyst', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Jr Finance Analyst', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Jr. Finance Analyst', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Junior Financial Analyst', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Jr Financial Analyst', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Jr. Financial Analyst', @equivalentId)

SET @equivalentId = '1ae43799-7ecf-47fd-bac4-abc9a87eb365'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Junior Credit Analyst', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Jr Credit Analyst', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Jr. Credit Analyst', @equivalentId)

SET @equivalentId = '891f925a-c382-425c-a52d-a87c04e6cec2'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Taxation Manager', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Taxation Mgr', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Tax Manager', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Tax Mgr', @equivalentId)

SET @equivalentId = '39fa884b-299e-4a7b-b223-aada4626ff49'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('taxatiojn accountant', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Taxation Accountant', @equivalentId)

SET @equivalentId = '0dc47b19-defc-4e38-828c-d9762c6cfd5a'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('tax accountant', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('tax analyst', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('tax partner', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('tax associate', @equivalentId)

SET @equivalentId = 'ae8fb54b-0861-4d65-bccd-a613ceb85261'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Junior Tax Accountant', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Jr. Tax Accountant', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Jr Tax Accountant', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Junior Taxation Accountant', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Jr. Taxation Accountant', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Jr Taxation Accountant', @equivalentId)

SET @equivalentId = '5e94ba61-282b-49fb-9b5d-8a30ffcddc0d'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Taxation Advisor', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Tax Advisor', @equivalentId)

SET @equivalentId = '379494cf-d42d-4b8b-9dae-3c96ee7c7f8d'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Taxation analyst', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Tax Return Preparer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Tax Agent', @equivalentId)

SET @equivalentId = '4c303f7c-ff38-4b2a-9a6f-abe4cbb1cef9'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Senior Tax Accountant', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Sr. Tax Accountant', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Sr Tax Accountant', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Senior Taxation Accountant', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Sr. Taxation Accountant', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Sr Taxation Accountant', @equivalentId)

SET @equivalentId = '6f93a5b9-18ab-40d9-a9da-2165456344ea'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Motor Claims', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Motor Claims Consultant', @equivalentId)

SET @equivalentId = '6c0f05a1-9c68-4529-b68b-5e311bde182d'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Injury Claims Consultant', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Workcover Claims', @equivalentId)

SET @equivalentId = 'a1434f0c-dae0-4acc-a3f7-aa043379238d'

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

SET @equivalentId = '1e3cfbd4-9603-495a-834d-78589845ec85'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Senior Purchasing Specialist', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Sr. Purchasing Specialist', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Sr Purchasing Specialist', @equivalentId)

SET @equivalentId = '81c9ef5a-b7b7-43be-9fc2-7b92c345d319'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Purchasing Manager', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Purchasing Mgr', @equivalentId)

SET @equivalentId = '48dccc96-41d0-481c-9402-e29817becb86'

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

SET @equivalentId = '6384545b-1bce-4a09-8e82-dca38ec81002'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Buyer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Senior  Buyer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Sr Buyer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Sr. Buyer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Regional Buyer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('National Buyer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('State Buyer', @equivalentId)

SET @equivalentId = '89be29df-3961-4735-ade2-480a75b6e2f2'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Buying Assistant', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Assistant Buyer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Junior Buyer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Jr Buyer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Jr. Buyer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Trainee Buyer', @equivalentId)

SET @equivalentId = '26196d06-e901-47f9-98e2-f798bb627a7b'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Mortgage Collections', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Mortgage Collection', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Mortgage Collections Officer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Mortgage Collection Officer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Mortgage Collections Clerk', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Mortgage Collection Clerk', @equivalentId)

SET @equivalentId = '8b845df8-0d4e-4920-bf21-9e98b0760f5e'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Banking Manager', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Bank Manager', @equivalentId)

SET @equivalentId = 'eecce1b7-ab72-4865-b1d1-8d0d398a2c52'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Business Banking Manager', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Manager Business Banking', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('banking services manager', @equivalentId)

SET @equivalentId = '377983c5-d22d-4634-8fd7-8428aab0a860'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Banking Clerk', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Banking Consultant', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Business Banker', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Banking Officer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('bank clerk', @equivalentId)

SET @equivalentId = '5769678a-2e8d-4681-9a4b-e5398b712c52'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Insurance Manager', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Insurance Mgr', @equivalentId)

SET @equivalentId = '23043650-494f-4c06-bedb-907c712cde86'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Insurance Consultant', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Insurance Assistant', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Insurance Asst', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Insurance Clerk', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Senior Insurance Administrator', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Sr. Insurance Administrator', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Sr Insurance Administrator', @equivalentId)

SET @equivalentId = '0e27738b-71c0-49bf-b33d-82b5e3566571'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Jr. Insurance Administrator', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Jr Insurance Administrator', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Junior Insurance Administrator', @equivalentId)

SET @equivalentId = 'a1a3a342-235c-42a0-a26f-bde0ed1f693b'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Insurance Broker', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('General Insurance Broker', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Insurance Solutions Consultant', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Senior Insurance Broker', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Sr. Insurance Broker', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Sr Insurance Broker', @equivalentId)

SET @equivalentId = 'eb418768-72b1-427f-bccf-0f3b52e23e6b'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Junior Insurance Broker', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Jr. Insurance Broker', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Jr Insurance Broker', @equivalentId)

SET @equivalentId = 'ade32806-59ae-405d-b2fc-d79c4b7704b6'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Chief Actuary', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('actuarial manager', @equivalentId)

SET @equivalentId = '06314691-af21-4bcc-90ae-b3c245d509e1'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Workers Compensation Officer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Workers Compensation Administrator', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('WORKERS COMPENSATION CASE MANAGER', @equivalentId)

SET @equivalentId = '09167703-1c5a-4307-aa9f-9e944d5c5d0f'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Underwriting Manager', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('National Underwriting Manager', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Underwriting', @equivalentId)

SET @equivalentId = 'e427b6b2-a271-4399-a0a1-c399e4579570'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Underwriting Executive', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Insurance Underwriter', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Assistant Underwriter', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('under writer', @equivalentId)

SET @equivalentId = '282b8908-27a2-48a2-87bc-f2f203a904d4'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Chied Financial Planner', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Chief Financial Planner', @equivalentId)

SET @equivalentId = 'a1d684d8-6c50-4bdd-9d83-2c81aae2f1ef'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Financial planner', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Financial Planner Advisor', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('certified financial planner', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Financial Services Consultant', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Personal Financial Advisor', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('CFP', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('financial advisor', @equivalentId)

SET @equivalentId = '6cb08caf-01c7-4638-9d9f-0ad6f77c5100'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Assistant Financial Planner', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Financial Planner Assistant', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Financial Planning Assistant', @equivalentId)

SET @equivalentId = '45924ad1-6fe0-4e3b-a62a-9212cb8dc77d'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Senior financial planner', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Sr. financial planner', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Sr financial planner', @equivalentId)

SET @equivalentId = '8a089d41-0808-41ef-9d86-022845781b5b'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Paraplanner', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Para planner', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Para-planner', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Para Planning', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Para-Planning', @equivalentId)

SET @equivalentId = 'f6c5440a-400c-4359-869e-14057f91b396'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Para-Financial Planner', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Para Financial Planner', @equivalentId)

SET @equivalentId = '92f7199b-86a0-4bc5-8c44-4a6eb23a65aa'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Superannuation', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Super annuation', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Super-annuation', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('super fund', @equivalentId)

SET @equivalentId = 'e017f06d-af13-4446-9680-88bea3042e8c'

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

SET @equivalentId = 'b526b986-6198-40a9-abf0-550eeb50e19d'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Recruitment Manager', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Onsite Recruitment Manager', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('National Recruitment Solutions Manager', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Regional Recruitment Manager', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('recruitment director', @equivalentId)

SET @equivalentId = '29e58e54-4b5d-4791-aa55-63b2dd8ff7ae'

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

SET @equivalentId = '4fa153ec-2890-4ac4-bff5-098da8c2c7c4'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Recruitment Administrator', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Recruitment Administration Officer', @equivalentId)

SET @equivalentId = '5cefcd70-8a4a-4d10-97e5-8192a4852dbe'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('recruiter', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Recruitment', @equivalentId)

SET @equivalentId = '1db0256a-9a74-4872-ba87-76ff7dfadba4'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('HR', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('H.R.', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Human Resources', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Human Resource', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('people and culture', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('human capital', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('personnel', @equivalentId)

SET @equivalentId = '9b95ad33-17de-495c-be51-a6f9802f1808'

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

SET @equivalentId = '1c4456b1-3570-4431-bfb1-16733f4f722f'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Senior HR Advisor', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Senior Human Resources Advisor', @equivalentId)

SET @equivalentId = 'd5dc57b2-2cd6-44d3-bea9-ebff05fc014b'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Senior Human Resources Consultant', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Senior Human Resources Officer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Senior HR Consultant', @equivalentId)

SET @equivalentId = '6e8d8d41-6c0f-4d9e-ad42-967da513407a'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('HR Coordinator', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('HR Co ordinator', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('HR Co-ordinator', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('HR Officer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('H.R. Officer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Human Resources Officer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Human Resources Officer -', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('General HR', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Personnel Officer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Personnel Services Officer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Human Resources Co-ordinator', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Human Resources Coordinator', @equivalentId)

SET @equivalentId = '876aedc0-c3c0-4f9b-bf72-8be79bf2788c'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Human Resources Assistant', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('HR Assistant', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('H.R. Assistant', @equivalentId)

SET @equivalentId = '1487117a-d9db-4fe5-87f2-b2ec433f3e8c'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Human Resources Analyst', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('HR ANALYST', @equivalentId)

SET @equivalentId = '021ab57d-6d89-4271-b696-9b51de0e563b'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('HR Executive', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Human Resources Executive', @equivalentId)

SET @equivalentId = 'fb371db0-7267-4aa5-a991-58c8540fbc4a'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('HR Payroll', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('HR/Payroll', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('H.R. Payroll', @equivalentId)

SET @equivalentId = '516bbd28-c670-4b95-ba71-aa6658ee9b2e'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Payroll Co-Ordinator', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Payroll Coordinator', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Payroll Co ordinator', @equivalentId)

SET @equivalentId = '4316ab57-ae90-483c-8793-2dc2581c85bf'

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

SET @equivalentId = '6728bfd8-ed26-4add-b121-4df9a4e40fa2'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Temporary Payroll Officer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Temp Payroll', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Payroll Temp', @equivalentId)

SET @equivalentId = '421c30d6-98ad-478f-a902-97ce0d5049ad'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Senior Payroll Officer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Payroll Supervisor', @equivalentId)

SET @equivalentId = 'e4d41396-5d02-46aa-a53d-ef895752f5bb'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Human Resources Administrator', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('HR Administrator', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('H.R. Administrator', @equivalentId)

SET @equivalentId = '9a24f97f-4171-4758-bd77-29ee42c14544'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('National Retail Manager', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Manager - Retail', @equivalentId)

SET @equivalentId = '22f239cb-e49f-4480-9396-96f3b75cd44a'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Retail Manager', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Retail Business Manager', @equivalentId)

SET @equivalentId = 'cb72a22e-8097-4e09-9fd0-47546da2d982'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Retail Operations Manager', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Retail Operations', @equivalentId)

SET @equivalentId = 'cbf9b5d8-1ab6-4500-9629-15487fefd728'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Store Manager', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Shop Manager', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Store Supervisor', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Store Manger', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Weekend Store Manager', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Retail Store Manager', @equivalentId)

SET @equivalentId = '50c3d6ce-bb35-4df9-943d-b51594b167c4'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Senior Store Manager', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Sr Store Manager', @equivalentId)

SET @equivalentId = '11e89022-33c0-4011-9bd5-98fc85bbc8dc'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Assistant Store Manager', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Asst Store Manager', @equivalentId)

SET @equivalentId = '3c9eda33-a68c-45df-82f6-36ccaa8471f8'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Retail Supervisor', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Retail Team Leader', @equivalentId)

SET @equivalentId = 'eedc3049-883b-44ae-910f-97c60faa7dd6'

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

SET @equivalentId = '2dce9b95-5a19-4cf6-a8f2-3d1ff05f8524'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('RETAIL/CUSTOMER SERVICE ASSISTANT', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Customer Service/ Retail', @equivalentId)

SET @equivalentId = '5c6adde9-d82c-4dc8-9fe2-e68da21396b5'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Casual Retail Sales assistant', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Casual Sales Consultant', @equivalentId)

SET @equivalentId = '7bb4342d-2ee1-4240-9b79-db74374be1ff'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Shop assistant', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Retail-shop assistant', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Retail Shop Assistant', @equivalentId)

SET @equivalentId = '31ae0144-9e28-4295-9b59-fc1825590b3f'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Casual Retail Assistant', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Casual Retail Sales', @equivalentId)

SET @equivalentId = 'aeb0b276-a57e-477a-8d0e-79b91555aa02'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Junior Retail Assistant', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Jr Retail Assistant', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Jr. Retail Assistant', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Retail Trainee', @equivalentId)

SET @equivalentId = '4f16cae8-1ade-4e8c-8085-9205e4823b9c'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Hotel Manager', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Hotel Duty Manager', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Hotel general manager', @equivalentId)

SET @equivalentId = 'eefca0ee-1d2f-435e-91ea-1e6994a60292'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Restaurant manager', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Restaurant General Manager', @equivalentId)

SET @equivalentId = 'cd703e43-ba0e-417d-ba82-647be954c1fb'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Bistro manager', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Restaurant Supervisor', @equivalentId)

SET @equivalentId = 'e8fbb2d8-8296-4798-befe-3a5c762da3c0'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Floor Manager', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Floor Supervisor', @equivalentId)

SET @equivalentId = 'ae8e982b-241b-4812-ae7e-655f9b0e7412'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Bar Manager', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Bar Supervisor', @equivalentId)

SET @equivalentId = 'e208a9c9-865b-48a9-866a-3e43c6dece6d'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Bar Attendant/Supervisor', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Bar Attendant/Manager', @equivalentId)

SET @equivalentId = 'a492246a-c2a0-43fe-a326-48768067f5f8'

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

SET @equivalentId = '2a05f038-07b8-4a72-a2de-ab9688328b71'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('barrista', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('barista', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('coffee maker', @equivalentId)

SET @equivalentId = 'bd03929d-0492-4f2c-90f8-f00ec99eb617'

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

SET @equivalentId = 'e76ad461-1362-44d4-945e-ddc5aac334c2'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Gaming Manager', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Gaming Mgr', @equivalentId)

SET @equivalentId = '3f07cb68-0de2-4db0-af76-c920a36b1d4a'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Gaming Attendant', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Gaming staff', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Bar/Gaming Attendant', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Gaming Bar Attendant', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Gaming/Bar Attendant', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('gaming machine attendant', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('room attendant', @equivalentId)

SET @equivalentId = '3c9a87bd-0da4-440d-930b-03527c84b89b'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Head Waiter', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Head Waitress', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Wait Supervisor', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Maitre De', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Senior Waiter', @equivalentId)

SET @equivalentId = '16a28b10-0449-4572-af63-537482c1bfd1'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Waiter', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Waitress', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Wait Staff', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Waitress Customer service', @equivalentId)

SET @equivalentId = '1e812212-5339-4036-8600-200d1d7d674b'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Kitchen Manager', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Head Cook', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Head Chef', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Chef Manager', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Head Pastry Chef', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Senior Chef', @equivalentId)

SET @equivalentId = '7c587307-c643-4019-9e73-12cece0f6b5b'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Second Chef', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('2nd Chef', @equivalentId)

SET @equivalentId = '27756aaf-32ba-48a6-928f-fc5f9413e3c2'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Chef De Partie', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Sous Chef', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Executive Chef', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Commis Chef', @equivalentId)

SET @equivalentId = '3e823b29-6d7c-4150-b1f3-181a90562fc0'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Assistant Chef', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Chefs Assistant', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Chef assistant', @equivalentId)

SET @equivalentId = '1ec0b1ad-ff24-49ed-88d8-9c9c93940b4c'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('chef', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('che', @equivalentId)

SET @equivalentId = '75db674c-3411-4f05-98a6-09dde9774031'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Waitress/ Kitchen Hand', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Waitress/kitchenhand', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Waitress / Kitchen Hand', @equivalentId)

SET @equivalentId = '8bd27f2d-876a-411f-9f79-07c8eb410763'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Executive Pastry Chef', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Pastry Chef', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('pastry cook', @equivalentId)

SET @equivalentId = 'eb57cb6b-812b-42f6-a61d-b5a31802a6c6'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Apprentice Chef', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('2nd year Apprentice Chef', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('1st Year Apprentice Chef', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('4th Year Apprentice Chef', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Trainee chef', @equivalentId)

SET @equivalentId = '967444f0-dbfb-49cf-90ab-dd97b194749e'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Cook/Chef', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Relief Chef', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Kitchen Hand/Cook', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Cook/Kitchen Hand', @equivalentId)

SET @equivalentId = '1b4939af-eb3d-4b07-a014-b5cbcd99cd3e'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('kitchen hand', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('kitchenhand', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Kitchen-hand', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Kitchen Worker', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Kitchen Hand/Waiter', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Kitchen hand/waitress', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('casual kitchen hand', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Kitchen hand Customer service', @equivalentId)

SET @equivalentId = '135c3f9a-cfc4-440b-9e51-52226230df5e'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Call Centre Manager', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Call Centre Team Manager', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Call Centre Supervisor', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Telemarketing Manager', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Telemarketing Team Leader', @equivalentId)

SET @equivalentId = '6da4075b-d513-4df6-8ee1-30628cc72d84'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Customer Service Manager', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Customer Services Manager', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Customer Service Supervisor', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Customer Service Team Leader', @equivalentId)

SET @equivalentId = 'c72449ff-e22d-48ed-aba2-9eeb5a7a717b'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Customer Service Officer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Customer Services Officer', @equivalentId)
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
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Customer Service Engineer', @equivalentId)
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

SET @equivalentId = 'd8009b62-75e5-41f7-94cb-854615713e2e'

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

SET @equivalentId = '32504840-b102-45d6-8175-5d97dda34d12'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Junior Receptionist', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Jr Receptionist', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Trainee Receptionist', @equivalentId)

SET @equivalentId = '4052d511-4604-4544-b048-413e49882383'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Junior Secretary', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Jr Secretary', @equivalentId)

SET @equivalentId = 'a90bf0fe-98b3-4d1e-bc23-72228cb9cf85'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Front Office Receptionist', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Receptionist Front Office', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Front Desk Receptionist', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Frontline Receptionist/Office All-rounder', @equivalentId)

SET @equivalentId = 'f1e43347-4c86-4d98-9f79-6e25571dbf53'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Assistant Receptionist', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Reception Assistant', @equivalentId)

SET @equivalentId = 'f4f5f030-4003-4f38-8be6-c05e5d01e254'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Front Office Manager', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Front Office Supervisor', @equivalentId)

SET @equivalentId = '7f3ce7c9-437b-4373-8a63-38c14dbcde5f'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Office manager', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Office Supervisor', @equivalentId)

SET @equivalentId = 'ff2db2cf-6e24-45a6-9aa4-13d61c0462a0'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Office Assistant', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Office Asst', @equivalentId)

SET @equivalentId = 'd35c1f9f-2b51-4ae1-b0ce-6fcf739c96e8'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Bilingual Receptionist', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('bi lingual receptionist', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('bi-lingual receptionist', @equivalentId)

SET @equivalentId = '44f2975b-97c6-4a30-bf4b-0faecf9e94b3'

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

SET @equivalentId = 'cac0d197-371b-4fa7-a261-a6d4b897b8ea'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Receptionist/Property Management', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Receptionist / Assistant Property Manager', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('property manager', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('facility manager', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('facilities manager', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('strata manager', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('portfolio property manager', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('rental manager', @equivalentId)

SET @equivalentId = 'a437c337-f56e-4b22-9428-93946602449c'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Temporary Receptionist/Secretary', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Temporary Receptionist', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Casual Receptionist', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Relief Receptionist', @equivalentId)

SET @equivalentId = 'e67f4fa0-d9a5-4cfb-8183-551d46c9c177'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Office Manager/Personal Assistant', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Office Manager/PA', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Office Manager / Executive Assistant', @equivalentId)

SET @equivalentId = '19efad11-e390-46ce-8ae1-016fb36ade68'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Administration Supervisor', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Admin Supervisor', @equivalentId)

SET @equivalentId = '6a1d3a3e-86ad-48e9-aaab-41117a0f6123'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Admin', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('administration', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Administrative', @equivalentId)
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

SET @equivalentId = '4a0a2044-3b09-4428-b9f6-a8bdac061d95'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Admin Assistant', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Admin Asst', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Administration Assistant', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Administrative Assistant', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Assistant Administrator', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Office Administration Assistant', @equivalentId)

SET @equivalentId = 'f7f945db-5ca2-43f3-97b8-a2cb4de77687'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Administrative Support Officer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Administrative Support', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Administration Support', @equivalentId)

SET @equivalentId = 'ccf1ec89-9845-4302-99e9-60e2e6e13ce3'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Office Administrator', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Office Admin', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('General office administration', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Administration Office', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Office Administration', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Office Clerk', @equivalentId)

SET @equivalentId = '44b273bd-47b1-4396-9185-055cb75fc431'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Temporary Clerical Assistant', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Temporary Administrative Officer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Relieving Administrative Officer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Temporary Admin Officer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Temp Admin Officer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Temp Administration Officer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('temporary administration assistant', @equivalentId)

SET @equivalentId = '30e5fac4-fda0-45db-a7d3-33a5532e0845'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Junior Clerical Assistant', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Clerical Trainee', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Clerical Traineeship', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Trainee Clerical Assistant', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Office Junior', @equivalentId)

SET @equivalentId = 'aa341be3-4bc2-4ff0-b22d-324be7b784e2'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Admin Trainee', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Office Administration Trainee', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Junior office administration', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Administration Trainee', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Junior Administration Assistant', @equivalentId)

SET @equivalentId = '01cc6da6-a923-4205-aed0-4cd8ca513398'

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

SET @equivalentId = '62bd6cf0-24ca-4a86-81a8-600c4850ec5c'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Data Entry Temp', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Temporary Data Entry', @equivalentId)

SET @equivalentId = 'ec3cdf56-15ed-4fc7-afdd-5395326a9118'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Engineering Manager', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Senior Engineer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Chief Engineer', @equivalentId)

SET @equivalentId = '2ae93a6f-7c97-4bd6-af25-8767e606d042'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('engineering project manager', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Senior Project Engineer', @equivalentId)

SET @equivalentId = 'ff19fc9c-3dc0-467f-b2a9-4fd22199cc93'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Site Engineer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Site Supervisor', @equivalentId)

SET @equivalentId = '202126d6-556e-492f-9ad4-30c999723d8c'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Assistant Engineer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Engineering Assistant', @equivalentId)

SET @equivalentId = '2e12f777-cc6d-4a3a-a7cf-5abd75ec0a8b'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Junior Engineer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Student Engineer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('graduate engineer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Undergraduate Engineer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('DESG Student Engineer', @equivalentId)

SET @equivalentId = '46b8cda4-a61c-4693-8acf-c973164b3917'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Field Engineer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('FIELD SERVICE ENGINEER', @equivalentId)

SET @equivalentId = '468197d3-bf94-4149-9750-72b1ca6bcbdf'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Engineering Consultant', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Consultant engineer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Consulting Engineer', @equivalentId)

SET @equivalentId = 'a3b4f068-4f3e-4330-95a3-c348048754cb'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Customer Support Engineer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Support Engineer', @equivalentId)

SET @equivalentId = '85b7d8f9-7ef6-4cd5-9834-57e2d4225796'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Aircraft Engineer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Aircraft Mechanic', @equivalentId)

SET @equivalentId = '0ec4d962-02f3-4411-8921-9312a7ea9477'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Design Engineer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('engineering designer', @equivalentId)

SET @equivalentId = '15bc8ee0-8788-48e7-b1bf-3294591bc15d'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Electrical Design Engineer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('electrical engineer', @equivalentId)

SET @equivalentId = 'c5517e20-4567-4183-9167-24b2f4d4cb01'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Mechanical Design Engineer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Mechanical Designer', @equivalentId)

SET @equivalentId = 'd4468bf4-d1f4-4355-aac7-5edcd8b0d704'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Electronics Engineer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Electronic Engineer', @equivalentId)

SET @equivalentId = '45b28429-7ccb-4821-8ec9-279ff2cf2a04'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Estimator', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('quantity surveyor', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('qs', @equivalentId)

SET @equivalentId = '784bb3c4-b82f-4530-abd9-8371c91ee812'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Sound Engineer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Audio Engineer', @equivalentId)

SET @equivalentId = 'f0a8baa6-b48a-42e4-a41a-f944fda15ac9'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Environmental Engineer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Environmental Officer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Environmental Health Officer', @equivalentId)

SET @equivalentId = '8555d039-c720-4f93-83c8-23ee5766b04a'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('mechanical engineer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('mech eng', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Maintenance Engineer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Plant Engineer', @equivalentId)

SET @equivalentId = 'b67bddf3-7aea-4576-a35f-384859b85b34'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Mining Manager', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Quarry Manager', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Quarry Operations', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('project manager mining', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('mining project manager', @equivalentId)

SET @equivalentId = '068eae32-fdad-4e41-b664-7a1b1a328aa5'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('mining engineer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Mine Geologist', @equivalentId)

SET @equivalentId = 'c9987c77-3e15-4c85-a5cb-1f0daf2e8deb'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Civil Engineer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('civilengineer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Senior Civil Engineer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Sr Civil Engineer', @equivalentId)

SET @equivalentId = '56317bd4-fbbe-4a53-b1d2-c70bb85c7ff8'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Trainee Civil Engineer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Junior Civil Engineer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Graduate Civil Engineer', @equivalentId)

SET @equivalentId = 'abb26f9a-43f9-47a5-927d-8ba2036a965f'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Process Engineer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Production Engineer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Manufacturing Engineer', @equivalentId)

SET @equivalentId = 'c358d00e-7706-4b39-a353-46375c4c276d'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Architectural Design', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Project Architect', @equivalentId)

SET @equivalentId = '1023a530-481c-48ed-95b8-1ae8b4867e08'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Senior Architect', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Senior Design Architect', @equivalentId)

SET @equivalentId = 'e7e3429f-a3cb-40e2-81b7-e415efca82df'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Assistant Architect', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Architectural Assistant', @equivalentId)

SET @equivalentId = 'd6f5854e-6c95-4d3a-8191-f52b157e6d68'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Graduate Architect', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Architectural Trainee', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Trainee Draftsman', @equivalentId)

SET @equivalentId = '6620bf65-f17c-4b3f-8b44-868eba1c9668'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Drafter', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Draftsman', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Draftsperson', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Architectural Draftsperson', @equivalentId)

SET @equivalentId = 'cb4f8178-6f12-4d72-bbbe-e6b6d946ab14'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('cad draftsman', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Autocad', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Auto CAD', @equivalentId)

SET @equivalentId = '2d3afa46-779d-44e9-bc1d-65f50be8a49c'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Senior Draftsman', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Senior Design Structural Draftsman', @equivalentId)

SET @equivalentId = '800553dd-9b92-49e4-b562-825e3dc7239a'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Trades', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Labourer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('manual labourer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('tradesman', @equivalentId)

SET @equivalentId = '9cd2b3dd-d2af-45d9-8ded-fde34a283cd8'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Trades Assistant', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Trade Assistant', @equivalentId)

SET @equivalentId = '9e5d4b6e-4c73-41fb-a8f8-c9b1ab774670'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Mechanic Manager', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Workshop Manager', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('MECHANICAL SUPERVISOR', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Mechanical Manager', @equivalentId)

SET @equivalentId = 'e156736d-9627-48e2-bb14-b6bbb68d23c5'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('boilermaker', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Boilermaker/Welder', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Welder', @equivalentId)

SET @equivalentId = '9807fe56-5dc8-42de-8cf4-65b7adbe29af'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Fitter', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Fitter/Welder', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Mechanical Fitter', @equivalentId)

SET @equivalentId = '4cf3290b-a277-4a9e-b6dd-01192ce03e3a'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('mechanic', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Service Mechanic', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Motor Mechanic', @equivalentId)

SET @equivalentId = '0dd8d327-f442-438a-a6aa-1d8cfcf4ebdd'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Assistant mechanic', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Trainee mechanic', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Apprentice Mechanic', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Apprentice Motor Mechanic', @equivalentId)

SET @equivalentId = '68465809-9b4c-4959-b49d-c886342a9cfa'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Electrical mechanic', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Maintenance Electrician', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Electrical Fitter', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('electrician', @equivalentId)

SET @equivalentId = '5882c3f2-9374-45ce-9b5c-ba76f804c258'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Electrical Supervisor', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Leading Hand Electrician', @equivalentId)

SET @equivalentId = '673cca21-9a2b-4df5-b4c4-9aed7f2d90eb'

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

SET @equivalentId = 'a2b51ad8-f766-4e65-9d15-01828a009589'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('plant manager', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('National Plant Manager', @equivalentId)

SET @equivalentId = 'e2540821-8684-4344-903b-9b1c72959974'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Operations Coordinator', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('operations co ordinator', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('operations co-ordinator', @equivalentId)

SET @equivalentId = '17265d89-05a0-4613-8432-8894a69739c8'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Operations Analyst', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Customer Operations Specialist', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Operations Support Officer', @equivalentId)

SET @equivalentId = '3fe5d7fd-16f7-4603-95fb-0369ab2880e9'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Production Supervisor', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Production Manager', @equivalentId)

SET @equivalentId = '4ae8ff5b-e98c-4782-a31d-2a14eb5c5b69'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Production Coordinator', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Production Co-Ordinator', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Production Co Ordinator', @equivalentId)

SET @equivalentId = '19180d8c-0858-47f5-9750-f10dd4fb59d3'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Production Team Leader', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Production team lead', @equivalentId)

SET @equivalentId = 'b4730ff1-68c9-4e47-94de-b518a07be98f'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('PRODUCTION SHIFT MANAGER', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('shift manager production', @equivalentId)

SET @equivalentId = '5be0026b-45dd-48e1-87cf-7d97263d67e9'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Assistant Production Manager', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Asst production manager', @equivalentId)

SET @equivalentId = 'a158e3f0-b3cd-40f8-a714-80dca7151290'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Production Planner', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Production Scheduler', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Production Analyst', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Production Controller', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Production Assistant', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Production Worker', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('mrp controller', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('supply chain planner', @equivalentId)

SET @equivalentId = '7d72fdcc-93b8-48bd-b1e0-3115abf21f38'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Production Operator', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('PRODUCTION CLERK', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Production / Machine Operator', @equivalentId)

SET @equivalentId = '016443cc-6e0f-4737-b6cf-6dbc29783e6d'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Machine Operator', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('machinist', @equivalentId)

SET @equivalentId = 'c0c8b9c7-77ef-47c1-8db6-150dc6ebfa52'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('process worker', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Process Worker/Packer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('food production worker', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Meat Packer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('process operator', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('general hand', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('generalhand', @equivalentId)

SET @equivalentId = 'c8cee03d-2101-4d8b-ab3e-db3319a2249f'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Casual Process worker', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('temp process worker', @equivalentId)

SET @equivalentId = '49dfd9a4-945c-47e6-ba91-edb3ec851c9f'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Warehouse Supervisor', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Warehouse manager', @equivalentId)

SET @equivalentId = '4797664a-60aa-4434-8554-9368568c65ae'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Warehouse Assistant', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Warehouse', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Warehouse worker', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Warehouse Clerk', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Warehouse Person', @equivalentId)

SET @equivalentId = '99eddfd8-e455-4d7e-8951-10a275fc2a25'

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

SET @equivalentId = 'dfd983a7-5d22-443c-ab1f-106ca6113596'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Maintenance Supervisor', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Maintenance manager', @equivalentId)

SET @equivalentId = 'a1431f50-13af-4c58-b94c-19c84e27351a'

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

SET @equivalentId = 'aa9f26d6-3eb9-4f96-990b-61ae3d87e489'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('IT Manager', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('I.T. Manager', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('National IT Manager', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Group IT Manager', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('IT Project Manager', @equivalentId)

SET @equivalentId = '23a36260-17ba-491b-baba-304aacf4deaf'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Infrastructure Project Manager', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Network Manager', @equivalentId)

SET @equivalentId = 'e920337c-4e3c-4456-8b63-48418352d572'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Assistant IT Manager', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Asst IT Manager', @equivalentId)

SET @equivalentId = 'eb3dad22-9c87-4ee6-aec2-5389bbc6e7ca'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('IT Operations Manager', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Computer Operations Manager', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('IT operations', @equivalentId)

SET @equivalentId = 'bd9bbec9-9ab5-4193-8359-f4589fdc6758'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Software Architect', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Systems Architect', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Senior Analyst Programmer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Senior Software Architect', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Senior Software Design Engineer', @equivalentId)

SET @equivalentId = 'dbd7eebc-4cc9-4a4c-a803-834cb9674fab'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Senior Developer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Lead Developer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Senior Programmer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Senior Software Engineer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Lead Software Engineer', @equivalentId)

SET @equivalentId = 'f3cf1d52-c514-46e0-a8d4-3a9051547280'

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

SET @equivalentId = '967400f7-f1f5-42da-82be-9b4149f069ca'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Junior Programmer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Junior Developer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Trainee Programmer', @equivalentId)

SET @equivalentId = '6ed4b215-4dfc-47b4-a133-0f8b41f20ee1'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Designer/Developer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Interface Developer', @equivalentId)

SET @equivalentId = 'f2f3fbe7-839a-49e9-8694-a5505369d1f7'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Systems Analyst/Programmer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Programmer/Analyst', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Programmer Analyst', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Analyst/Programmer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Senior Analyst/Programmer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Systems Analyst', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Senior Systems Analyst', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('System Analyst', @equivalentId)

SET @equivalentId = '3aa9df4d-04a4-49d7-92aa-669abf68a96c'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('System Engineer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Systems Engineer', @equivalentId)

SET @equivalentId = '3fdbd534-c562-42b2-80f0-e8407a8983c4'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Test Team Leader', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('TEST MANAGER', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Test Coordinator', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Testing Manager', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Senior Test Analyst', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Sr Test Analyst', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Senior Test Engineer', @equivalentId)

SET @equivalentId = 'f25fe036-7f38-4921-a836-5bcdc6cb0159'

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

SET @equivalentId = '5d7a1ef9-8c40-49ba-9918-6609a799c5d4'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('IT Administrator', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('I.T. Administrator', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('IT Admin', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('I.T. Admin', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('IT Systems Officer', @equivalentId)

SET @equivalentId = '56c0b984-249d-4169-8878-db4c06535e78'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('IT Officer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('IT Assistant', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('I.T. Officer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('I.T. Assistant', @equivalentId)

SET @equivalentId = '6ce026eb-96c9-407e-9e8d-98698dcfaba1'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('IT Specialist', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('it guru', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('it expert', @equivalentId)

SET @equivalentId = '80738543-82e7-4cba-a932-5fda37ea6e6c'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('IT Trainer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('IT Tutor', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('computer tutor', @equivalentId)

SET @equivalentId = 'bcfa0a1f-5bd9-4f5c-9f45-a1976e74882a'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Network architect', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Network Technician', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Chief Network Architect', @equivalentId)

SET @equivalentId = '6fa18df3-d1c6-4c34-b9b6-48ed56b7bc58'

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

SET @equivalentId = '75f66f87-8886-49ed-971e-f658d3f64409'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Network Engineer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Network support engineer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Network Support', @equivalentId)

SET @equivalentId = 'c32e7080-81ce-43c7-85e1-c4734e5cb20e'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Network Consultant', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Network Support Officer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Network Specialist', @equivalentId)

SET @equivalentId = 'e20da0df-3596-477a-8bc0-d0426eca5bd9'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Database Administrator', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('DBA', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('D.B.A.', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('data base administrator', @equivalentId)

SET @equivalentId = '93a85f87-f732-4255-810e-03966ab96a5b'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('IT consultant', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('I.T. Consultant', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Systems consultant', @equivalentId)

SET @equivalentId = 'aa5ffcec-5a1b-4f02-85e1-f1894901ba7c'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('java developer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('java', @equivalentId)

SET @equivalentId = 'db64a54c-b987-4a89-859a-4eb9530ca3fd'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('IT Contractor', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('IT Contracts', @equivalentId)

SET @equivalentId = '5926e6bf-3cab-4fad-9fde-47cf8768a158'

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

SET @equivalentId = 'e448dfa5-5d63-4436-9b45-ff4141185cf0'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Intranet Developer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Intranet Application Developer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Intranet Software Developer', @equivalentId)

SET @equivalentId = 'eb61ae1e-e250-4b1f-bb9e-dfa5e988937f'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Website Developer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Web Application Developer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Web Designer/Developer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Web Site Developer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Freelance Web Developer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Senior Web Developer', @equivalentId)

SET @equivalentId = '234545dd-f7be-4261-be8b-f7007cf5c4f0'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('IT Support Engineer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Computer Engineer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Desktop Support Engineer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('IT Support Technician', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Computer Technician', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('IT Technician', @equivalentId)

SET @equivalentId = 'ae7c1fa2-163b-4bf3-b33b-05290ac50d65'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('ColdFusion', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('cold fusion', @equivalentId)

SET @equivalentId = '1728cddc-e3c0-452b-9962-f8a762c316da'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('vb.net', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('visual basic.net', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('visual basic dot net', @equivalentId)

SET @equivalentId = 'deccc757-c1ee-4257-8b00-642cf53dbfb0'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('VB', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Visual basic', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('visualbasic', @equivalentId)

SET @equivalentId = '3ee88f4b-f01b-4196-9724-195007d4fd33'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Oracle Database Administrator', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Oracle DBA', @equivalentId)

SET @equivalentId = 'fbc34aa9-a066-4284-bdbc-b2d5a5328c3c'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('UNIX Systems Administrator', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Senior Unix Systems Administrator', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Unix Consultant', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Unix Consoultant', @equivalentId)

SET @equivalentId = '7b78aebd-02f0-4b3c-a827-0438e759a07e'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Telco', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Telecoms', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Telecom', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Telecomms', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Telecommunication', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Telecommunications', @equivalentId)

SET @equivalentId = '83fa347d-89a8-475d-b15c-ec8da2b59d42'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('PABX', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Private Automatic Branch eXchange.', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('pbx', @equivalentId)

SET @equivalentId = 'e837fe9e-b8ba-4d72-b503-ec0a105c68ff'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Solutions Architect', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Solution Architect', @equivalentId)

SET @equivalentId = '966e06cb-0329-4e41-b794-8db760a2b3ff'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Graphics/Multimedia', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('multimedia', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('multi media', @equivalentId)

SET @equivalentId = '687cb363-cfec-403d-b5a7-4203245051a2'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Graphic designer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Graphic Design', @equivalentId)

SET @equivalentId = 'c506b513-aa78-45f4-b17a-0970661ab736'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('freelance graphic designer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Freelance Graphic Design', @equivalentId)

SET @equivalentId = '81082953-64ff-4b4e-a6b1-5e9ff5e89147'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Junior Graphic Designer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Trainee Graphic Designer', @equivalentId)

SET @equivalentId = '64b5b73a-b3f5-4120-85e8-906e20858095'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Web / Graphic Designer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Web/Graphic Designer', @equivalentId)

SET @equivalentId = 'f7f7f1e3-ae0d-473e-b398-90d7f338a7bb'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('general counsel', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('head counsel', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('head lawyer', @equivalentId)

SET @equivalentId = 'e0fa8717-03f5-40d9-934a-1593d34ffd88'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Lawyer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Solicitor', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Legal Counsel', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('cousel', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Corporate Solicitor', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Litigation Solicitor', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Law Graduate', @equivalentId)

SET @equivalentId = 'af165a13-0c48-484f-a4a8-4d3207ec7aa0'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Legal Secretary', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Legal Secretary/Receptionist', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Legal Secretary/Personal Assistant', @equivalentId)

SET @equivalentId = '7a7c30f3-d58b-430e-a013-50c5aab7843c'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Corporate legal secretary', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Commercial legal secretary', @equivalentId)

SET @equivalentId = '454b03d2-13af-4e74-987c-a4eb37ac4a65'

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

SET @equivalentId = '8e7a49d2-c1ac-4725-8cc4-35bdf9bded1c'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Associate Nurse Unit Manager', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Nurse Unit Manager', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Charge Nurse', @equivalentId)

SET @equivalentId = '027ef202-4730-41fc-9469-b97a2462c777'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Registered Nurse', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Registered nurse - Level', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Practice Nurse', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Staff Nurse', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('State Enrolled Nurse', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('ren', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('sen', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('rn', @equivalentId)

SET @equivalentId = '7def3ce2-cc4f-43c6-b5be-6d7c6f02ab3e'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Clinical Nurse Specialist', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Clinical Nurse', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Clinical Nurse Consultant', @equivalentId)

SET @equivalentId = 'bb4fa099-27a1-4335-a468-5e148ef1016c'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Clinical Nurse Educator', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('General Nurse Training', @equivalentId)

SET @equivalentId = '6c291297-1ed2-414e-af77-5ebe87be2c24'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Nursing Assistant', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('assistant nurse', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Nurse Aid', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('ain', @equivalentId)

SET @equivalentId = '70ed74e9-59a0-4896-becc-d54a2e28c5f8'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('trainee nurse', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Student Nurse', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Trainee Enrolled Nurse', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Student Enrolled Nurse', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('enrolled nurse', @equivalentId)

SET @equivalentId = '0c92e7e9-1009-43ae-baaf-b788102b576f'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Nurseryhand', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('nursery hand', @equivalentId)

SET @equivalentId = '740a6be5-b610-4c1d-be59-72d48ddf01e5'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Medical Receptionist', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Medical Secretary', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Senior Medical Receptionist', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Casual Medical Receptionist', @equivalentId)

SET @equivalentId = '2fd65ed6-fc4f-49b7-9322-2467d6770e2e'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Dental Nurse', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Senior Dental Nurse', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Dental Assistant/Receptionist', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('dental assistant', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('dental hygienist', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('dental technician', @equivalentId)

SET @equivalentId = '73f41582-bff3-447c-949e-ed3a2b49a0df'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Junior Dental Nurse', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Trainee Dental Nurse', @equivalentId)

SET @equivalentId = 'f5a5fd0d-2d9a-4462-bd5f-59c50d84786b'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Veterinary Nurse', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Vet Nurse', @equivalentId)

SET @equivalentId = '7ac6b07d-07fa-4633-923e-0f91813db8b2'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('vet', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('vetinary', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('vetinarian', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('veterinarian', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('veterinary', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('veterinary practitioner', @equivalentId)

SET @equivalentId = '137e020b-2052-48e8-9bcf-e1fe1f2d9022'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('nanny', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('nannies', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('aupair', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('au-pair', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('au pair', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('childcare worker', @equivalentId)

SET @equivalentId = 'de88212d-ccdf-4823-aee3-e846c4d0c985'

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
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('NSW Sales Executive', @equivalentId)
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

SET @equivalentId = '27b066c3-4cd2-4aeb-b7bd-97e58f88fc77'

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

SET @equivalentId = '46189c2b-f7c5-42b8-99fe-9b077017dc9d'

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

SET @equivalentId = 'c638eb42-0962-4cbf-9de6-9af2950803f7'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Assistant Sales Manager', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Asst Sales Manager', @equivalentId)

SET @equivalentId = '6a25c735-d432-45c9-945d-172e20830746'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Channel Manager', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('channels manager', @equivalentId)

SET @equivalentId = 'c59d172d-3d7f-4ba5-abe2-6810e08fb804'

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

SET @equivalentId = 'aae9cfb2-ded4-437d-b7b7-7f1e178f5ec1'

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

SET @equivalentId = '72be2d3b-6ae0-4165-ba19-fbbe9db0b850'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Internal Sales Representative', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Internal Sales Rep', @equivalentId)

SET @equivalentId = 'b94156cb-e2fa-4bee-a051-023dc1da58db'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Sales Agent', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Sales Advisor', @equivalentId)

SET @equivalentId = 'e1409dda-81b3-4956-bbde-b14e51479ba9'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('SALES ASSOCIATE', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Sales Assoc', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Senior Sales Associate', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Sr Sales Associate', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Sr. Sales Associate', @equivalentId)

SET @equivalentId = '13e39722-c224-470d-a7e7-6742e540e884'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Sales Assistant/Manager', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Senior Sales Assistant', @equivalentId)

SET @equivalentId = '3e47a01c-8c55-472f-969c-ae6d28e33cf4'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Junior Sales Assistant', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Sales Assistant/Cashier', @equivalentId)

SET @equivalentId = '14d7eda0-520f-4ee4-ae71-b6939827ef35'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Sales Assistant/Supervisor', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('supervisor/sales assistant', @equivalentId)

SET @equivalentId = 'c776d6d6-812f-4116-ba6d-ba544a3b5c93'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Sales Assistant Customer service', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Customer Service/Sales Assistant', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Receptionist/Sales Secretary', @equivalentId)

SET @equivalentId = 'e690ec94-339f-40bd-b2dd-e198aa3db892'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Advertising Sales Manager', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Advertising Sales Representative', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Advertising Sales Rep', @equivalentId)

SET @equivalentId = 'c050e515-501e-413d-b490-06c0022a5067'

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

SET @equivalentId = '91b9cf4f-c42a-4901-b104-b2b90dfca7c8'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Marketing Manager', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('National Marketing Manager', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('GM Marketing', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Marketing GM', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Marketing general manager', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('general manager marketing', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('marketing director', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Group Marketing Manager', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Regional Marketing Manager', @equivalentId)

SET @equivalentId = 'e3ffd294-9b7d-41ec-a11f-eeb9ed77e534'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('vp marketing', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('vice president marketing', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('marketing vice president', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('marketing vp', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('marketing v.p.', @equivalentId)

SET @equivalentId = '78307762-3f8f-410e-9d85-4ef6b11f0e0a'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Marketing Communications Manager', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Communications Manager', @equivalentId)

SET @equivalentId = '9cf38e23-2a92-480c-b4b6-2af3e79aaea4'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Online Marketing Manager', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Trade Marketing Manager', @equivalentId)

SET @equivalentId = 'b8dfcbb9-07da-47bf-a3d1-51975c6dd2f3'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Assistant Marketing Manager', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Marketing Manager Assistant', @equivalentId)

SET @equivalentId = 'b589a4d9-2e05-404b-bcf1-cfe61be378c9'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Marketing Assistant', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Marketing asst', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Casual Marketing Assistant', @equivalentId)

SET @equivalentId = 'ef4207a3-7209-4559-9191-54d7948e606b'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('PR Manager', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('P.R. Manager', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Public Relations Manager', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('PR Director', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Public Relations Director', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Public relations', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('PR', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('P.R.', @equivalentId)

SET @equivalentId = '871bfb66-1d9a-470c-9c71-14d27d728a70'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Marketing Communications Specialist', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('marketing communications', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('marketing coordinator', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('marketing co ordinator', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('marketing co-ordinator', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Marketing Communications Co-ordinator', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Marketing Communications Co ordinator', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Marketing Communications Coordinator', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Marketing communications executive', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Marketing Executive', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Marketing Exec', @equivalentId)

SET @equivalentId = '48706328-d440-44fd-b766-e6698b8aca79'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Communications', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Communications Officer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Communications Consultant', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('COMMUNICATIONS COORDINATOR', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Communications Specialist', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('comms', @equivalentId)

SET @equivalentId = '8113272f-374f-4073-9481-98ece53675c7'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Market Research', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Market researcher', @equivalentId)

SET @equivalentId = '8639d077-d6c7-45d4-9fae-80f2fe16c261'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Promotions', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Promotions Officer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Promotions Co-ordinator', @equivalentId)

SET @equivalentId = '296793ed-1546-4394-b8fd-a518f3a114a9'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Senior Publicist', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Sr Publicist', @equivalentId)

SET @equivalentId = '79362b6f-e7ad-437b-a7a2-73eee06242cd'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Event Manager', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Events Manager', @equivalentId)

SET @equivalentId = 'f25e6ec0-4a9f-423d-b061-c42e9e6ae442'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Advertising Sales Executive', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Advertising Sales', @equivalentId)

SET @equivalentId = '0d3947e7-d365-4c5b-bf7f-eebec8f2fd57'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Product Manager', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Senior Product Manager', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('National Product Manager', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Assistant Product Manager', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Group product manager', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('New Product Manager', @equivalentId)

SET @equivalentId = '3fcfe326-91d9-45a3-808f-3ea1caf6ff41'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Market Research Officer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Market Research Consultant', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Market Research Analyst', @equivalentId)

SET @equivalentId = 'b9ff6c3a-e76e-4ca4-8224-4d8fbd7ca963'

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

SET @equivalentId = '4ea7ca8e-28b0-4e83-a4f1-3c903ddff425'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('IT Business Analyst', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('I.T. Business Analyst', @equivalentId)

SET @equivalentId = 'ace9274d-ea2e-41f1-878d-8916d0ff3563'

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

SET @equivalentId = 'b5a49061-8c79-4098-8759-0b53e9d3404d'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Head cleaner', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Cleaning Manager', @equivalentId)

SET @equivalentId = '8a7da3bf-d092-4b86-8929-999c1dc9cb51'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Shopping Centre Cleaner', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('centre cleaner', @equivalentId)

SET @equivalentId = 'a1ce12d9-3e60-46c7-9b41-5fe15c61251a'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Research Assistant', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Research Asst', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Senior Research Assistant', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Sr Research Assistant', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('clinical research', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('research officer', @equivalentId)

SET @equivalentId = '03784e0d-5286-40f1-8ebe-99f95811fd8c'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Principal', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Senior Teacher', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Acting Principal', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Vice Principal', @equivalentId)

SET @equivalentId = '4a10f0ea-29bf-4e8f-8565-ff3637071f68'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Teacher', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('school teacher', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Teacher/Trainer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('High School Teacher', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Middle School Teacher', @equivalentId)

SET @equivalentId = 'c0a346c9-6432-4019-beee-cb0713db86d8'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Classroom Teacher', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Primary Teacher', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Primary School Teacher', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Senior grades teacher', @equivalentId)

SET @equivalentId = '7ed48287-5ca8-4ca6-ae5f-fde039599a80'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Kindergarten Teacher', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Early Childhood Teacher', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Pre School Teacher', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Preschool Teacher', @equivalentId)

SET @equivalentId = '45685ec2-d10e-461d-9546-4f4956342e77'

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

SET @equivalentId = '83a0a504-4295-4c32-b8aa-85e0cdac3058'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Casual Teacher', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Casual Relief Teacher', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Relief Teacher', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Temporary Relief Teacher', @equivalentId)

SET @equivalentId = '0c9dde17-b168-4592-bea9-65945e32ca51'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('English Teacher', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('English Language Teacher', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('ESL Teacher', @equivalentId)

SET @equivalentId = 'edeaa759-afd3-4821-8c91-9cc82a4c2d0f'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Computer Teacher', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Information Technology Teacher', @equivalentId)

SET @equivalentId = '50dedf3f-b010-4992-af8e-265a3f266dec'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('TAFE Teacher', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('TAFE sessional teaching', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('T.A.F.E. Teacher', @equivalentId)

SET @equivalentId = '60f19ce3-18cc-4c02-a64e-eff1757fedde'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Mathematics Teacher', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Mathematics/Science Teacher', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Maths Teacher', @equivalentId)

SET @equivalentId = '056681b2-4efe-4e1b-a489-62abda18ca75'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Art Teacher', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Graphic Art Teacher', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Visual Art Teacher', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Teacher Art', @equivalentId)

SET @equivalentId = '7426fa08-c465-4a11-b932-bcfcf6e3792a'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Piano Teacher', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Music Teacher', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Instrumental Teacher', @equivalentId)

SET @equivalentId = '9028bd20-0071-4f10-9b6e-7337e80a4f33'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Dance teacher', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Belly dance teacher', @equivalentId)

SET @equivalentId = '7a49c174-e353-441f-a058-ea27bf3df9ff'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('LOTE Teacher', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('L.O.T.E. Teacher', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Foreign Language Teacher', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Chinese Teacher', @equivalentId)

SET @equivalentId = '099a9371-27f6-4cdd-832e-3fa74efeb097'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Production Technician', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Production Crew', @equivalentId)

SET @equivalentId = '322b4b68-d64f-47f5-8469-edfbbb6b4e7c'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Child Protection Worker', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Child Protection Officer', @equivalentId)

SET @equivalentId = 'e06dbeeb-66dc-417e-913a-da79f846bb6b'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Regional Survey Manager', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('State Survey Manager', @equivalentId)

SET @equivalentId = '115c22b5-cd41-4a56-a2ce-28b9f7651352'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Policy Manager', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Polices Manager', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Senior Policy Officer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Senior Policies Officer', @equivalentId)

SET @equivalentId = '34243974-30aa-4837-a132-fdea4992a6e9'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Unit Manager', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Unit Operations Manager', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Unit Director', @equivalentId)

SET @equivalentId = 'aa6466ba-79dd-4889-8c99-b9798189dc3a'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Assistant', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Asst', @equivalentId)

SET @equivalentId = 'f59c2691-816a-4068-b566-20c251203e45'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('administrator', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('ADMINISTRATIVE MANAGER', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('BUSINESS ADMINISTRATOR', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Senior Administration Officer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Business Administration', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Executive Administrator', @equivalentId)

SET @equivalentId = '2b1387e8-5465-4e92-b81a-778efc8bc2f6'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Operations Administrator', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Operations Admin', @equivalentId)

SET @equivalentId = '4361c0ba-ff60-48e9-9932-477c519c8854'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Factory Hand', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Casual Factory Hand', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('General Factory Hand', @equivalentId)

SET @equivalentId = 'e443c256-1e3d-4278-95e5-2383c2f2266e'

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

SET @equivalentId = '6758a67c-e8ca-4498-bd52-1c977a035f0d'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Proprietor', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Sole Proprietor', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Business Proprietor', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Manager/Proprietor', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Joint Proprietor', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Owner - Manager', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Business Owner/Operator', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Small business owner', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Owner-Operator', @equivalentId)

SET @equivalentId = '504f5ccd-537b-4ce6-b7ea-f6b339c6e7a3'

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

SET @equivalentId = '0ca8d851-22f2-4cb8-b549-f5c4d08c3608'

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

SET @equivalentId = '5de89672-d43e-4bc8-9b4d-d478ba83261b'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Delivery Driver', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Pizza delivery driver', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Multi-drop Delivery Driver', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Delivery Driver/Kitchen hand', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Owner Driver', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Driver', @equivalentId)

SET @equivalentId = '02660d83-f55e-4399-91c1-62c543f39e23'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Bus Driver', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Coach Driver', @equivalentId)

SET @equivalentId = '7a9aa03e-c2a6-4537-b3b0-e0a4fe529ddc'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Taxi Driver', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Cab Driver', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('HGV Driver', @equivalentId)

SET @equivalentId = 'e3a33f49-c391-4966-9a31-7cfecfcd7f48'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Interstate Truck Driver', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Truck Driver/Plant Operator', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Truck Driver', @equivalentId)

SET @equivalentId = 'ffcfa870-3c02-4a20-9eae-6b83f112b147'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('lecturer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Music Technology Lecturer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Associate Lecturer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Assistant Lecturer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Senior Lecturer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Guest Lecturer', @equivalentId)

SET @equivalentId = 'e729564d-be78-480c-8d69-decbe8f2838c'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Coordinator', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Co ordinator', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Co-ordinator', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('ordinator', @equivalentId)

SET @equivalentId = '5eac244d-7267-4ca3-810e-1667f5b3a340'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Branch Manager', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Assistant Branch Manager', @equivalentId)

SET @equivalentId = 'f046fead-62ac-48cf-9bbf-034bccc5a265'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Work Experience', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Work Experience Student', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('WORK EXPERIENCE - TAFE', @equivalentId)

SET @equivalentId = '58b07568-6ed5-432c-a380-c40c0d2ac3e4'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('PHARMACY ASSISTANT', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Pharmacy Assistant Customer Service', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('dispensary technician', @equivalentId)

SET @equivalentId = '3a202c88-280f-4342-808d-9f88939f71d6'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('pharmacist', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('pharmacy manager', @equivalentId)

SET @equivalentId = '70973554-7cf4-4754-809b-a83d2cb0de9e'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Crew Member', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Casual Crew member', @equivalentId)

SET @equivalentId = '611f09ef-1214-4e83-be67-edcda2fa6ee4'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Trainee', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Traineeship', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Student trainee', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Graduate Trainee', @equivalentId)

SET @equivalentId = 'dc22751c-319d-4f11-b4d8-07beba55f4e8'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Volunteer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Volunteer Worker', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Volunteer Member', @equivalentId)

SET @equivalentId = 'fa283a20-b322-4292-9888-0a163a173f77'

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

SET @equivalentId = '5384dc50-e689-4d5c-9b48-7c02c075d1fb'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Child Care Assistant', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Child Care Worker', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('child care', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Child Carer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Qualified Child Care Worker', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Casual child care assistant', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('childcare', @equivalentId)

SET @equivalentId = '46265450-80c4-4698-813d-f77ac838d496'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Computer Operator', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Senior Computer Operator', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Trainee Computer Operator', @equivalentId)

SET @equivalentId = 'e7f72ddc-435d-4f47-a4a8-b431a5982b56'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('FARMHAND', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Farm hand', @equivalentId)

SET @equivalentId = '5499c09b-5b96-44c7-b795-960f90e9b327'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Technical Consultant', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Senior Technical Consultant', @equivalentId)

SET @equivalentId = 'a0c575ab-0a61-4913-a8ea-ed92003af89f'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Various', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('varied', @equivalentId)

SET @equivalentId = 'ef986af8-d753-4730-b138-6d5f9b28f7d2'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Business Consultant', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Senior Business Consultant', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('New Business Consultant', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Telephone Business Consultant/Customer Service Representive', @equivalentId)

SET @equivalentId = 'ae9e04c5-cb7f-4035-a571-76fb2a1a3a29'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Laboratory Technician', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('medical laboratory technician', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('lab technician', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Laboratory Assistant', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Lab Assistant', @equivalentId)

SET @equivalentId = '689e83e5-c8e2-420a-9a62-27ce2a54dc00'

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

SET @equivalentId = 'c4b36779-532c-4e29-82eb-5b10b54e6c2a'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Delicatessen Manager', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Deli Manager', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Service Deli Manager', @equivalentId)

SET @equivalentId = '1f01444f-7131-49dc-bae2-18a5f9fd32a3'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Deli Assistant', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Service Deli Assistant', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Serviced Deli Assistant', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('DELICATESSEN ASSISTANT', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Deli', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Delicatessen', @equivalentId)

SET @equivalentId = 'd69fb090-5c72-497c-943d-af36e108cfe9'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Translator', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Interpreter', @equivalentId)

SET @equivalentId = 'ebf066c1-f11f-4217-adf2-dc1602929a37'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Stockbroker', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Stock broker', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Stock broking', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Stockbroking', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Stockbrokers', @equivalentId)

SET @equivalentId = 'ed723c27-99fe-4c1a-b4ce-5d71524607e5'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('1', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('one', @equivalentId)

SET @equivalentId = 'c101b1ec-58a2-495f-b656-3005f434fcf4'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('2', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('two', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('ii', @equivalentId)

SET @equivalentId = '091ca8c5-1b35-45f7-a550-de6a7ddb082b'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('3', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('three', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('iii', @equivalentId)

SET @equivalentId = '9070b558-4790-45a4-8300-83794713f9f1'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('4', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('four', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('iv', @equivalentId)

SET @equivalentId = 'd3a937ed-ee9d-4dd3-9714-e4af88f3bf2a'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('5', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('five', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('v', @equivalentId)

SET @equivalentId = '9e82e446-b6f1-416a-a54b-badc452ccacc'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('6', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('six', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('vi', @equivalentId)

SET @equivalentId = 'd1b6166a-c604-4c1a-8ed2-1bb1fca57b90'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('7', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('seven', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('vii', @equivalentId)

SET @equivalentId = 'ceec2828-67c0-4eae-adfd-519de2d33309'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('8', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('eight', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('viii', @equivalentId)

SET @equivalentId = '7a9a4710-36fe-43aa-91ee-b2145b5dacf9'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('9', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('nine', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('ix', @equivalentId)

SET @equivalentId = 'd2be2afd-f7d0-45e0-97a5-ae8d4c4d38bf'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('10', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('ten', @equivalentId)

SET @equivalentId = 'bd4a479d-4667-4f5a-99f1-82731486c139'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('1st', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('first', @equivalentId)

SET @equivalentId = 'f61668d1-4d95-4229-a321-8ecddccf9d6b'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('3ic', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('3.i.c.', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('3i.c.', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Third in charge', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('3 IC', @equivalentId)

SET @equivalentId = '88b66c90-5037-4ad3-a0e7-2e161db7dc48'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('2nd', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('second', @equivalentId)

SET @equivalentId = '4b6c861f-16a2-4a00-8ad0-2229b8281ee0'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('3rd', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('third', @equivalentId)

SET @equivalentId = '487bdfbc-bab3-46ef-9511-d346b9c20009'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('4th', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('forth', @equivalentId)

SET @equivalentId = '507ba1bd-bfc8-4f7f-9c82-298d09068ab2'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Aboriginal', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('indigenous', @equivalentId)

SET @equivalentId = '90191bdd-7e88-4f5b-90ee-0436ad406190'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('acfi', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Aged Care Funding Instrument', @equivalentId)

SET @equivalentId = '95643c74-30c1-4c4a-912c-2b7d21ce1e82'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('ADSL', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Asymmetric Digital Subscriber Line', @equivalentId)

SET @equivalentId = '50b6fcfb-ebbb-44f1-b64d-8efca93f5e84'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('ag', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('agriculture', @equivalentId)

SET @equivalentId = 'ff3c8d74-1277-40e5-a2c2-638718c919fe'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('A-Grade Electrical', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('A Grade Electrical', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('A Grade electrician', @equivalentId)

SET @equivalentId = '32170565-0fcd-4328-b237-f05b6fcf1cce'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('asap', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('as soon as possible', @equivalentId)

SET @equivalentId = '483b6402-f0a2-4262-bdef-b8e68bf73c9a'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('ASX', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Australian stock exchange', @equivalentId)

SET @equivalentId = '6c76fa5b-a742-44c1-9963-2b541217f7ec'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('aust', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('australia', @equivalentId)

SET @equivalentId = '631c1408-d803-4987-8af9-dcb16b3c5d26'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('bas', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('business activity statement', @equivalentId)

SET @equivalentId = 'db5cb77c-29c6-4024-8078-ded45e6abb9a'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('BHP', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('BHPBilliton', @equivalentId)

SET @equivalentId = 'ce0ae5a8-c898-4ba5-8646-f65908960dc1'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('rio tinto', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('riotinto', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('rtz', @equivalentId)

SET @equivalentId = '8964bb09-e543-4ca5-b916-7127ad67635a'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('bris', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('brisbane', @equivalentId)

SET @equivalentId = 'c0b07e47-8754-4c59-b19b-7b0a094b72ce'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('cad', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('computer aided design', @equivalentId)

SET @equivalentId = '57c8b733-a17f-48e3-9eb0-53aa02e198b9'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('cam', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('computer aided manufacturing', @equivalentId)

SET @equivalentId = 'fa353d8e-f51e-4dbc-ab51-cbc6e7197890'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('cardio', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('cardiovascular', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('cardio-vascular', @equivalentId)

SET @equivalentId = 'c6d35487-2287-4eea-9705-ea788f36b2d7'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('cashflow', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('cash flow', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('cash-flow', @equivalentId)

SET @equivalentId = '6cd3c376-fdd5-4bbc-9b15-a0897ecbd309'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('CBA', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Commonwealth Bank of Australia', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('comm bank', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('commbank', @equivalentId)

SET @equivalentId = 'ff74a96a-9547-4d14-a71a-fc6d29764b50'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('cbd', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('central business district', @equivalentId)

SET @equivalentId = '28523649-cb26-4006-a7ca-7ee0b555ec19'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('cctv', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('closed circuit tv', @equivalentId)

SET @equivalentId = 'e6fd4f1e-9272-4012-a29f-b4d6897567e7'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('cert', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('certificate', @equivalentId)

SET @equivalentId = 'e444d3ae-18fb-4a5a-b65d-1219955418f2'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('cert1', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('certificate 1', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('certi', @equivalentId)

SET @equivalentId = 'f9bfd7a6-1ddd-4da1-9623-6d7ef2e68008'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('cert2', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('certificate 2', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('certii', @equivalentId)

SET @equivalentId = '2f0fac1a-dea3-4fc4-9a3e-462cb31059c9'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('cert3', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('certificate 3', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('certiii', @equivalentId)

SET @equivalentId = 'e2b8ccb3-0830-4133-b902-12358a1ed2f1'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('comp', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('compensation', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('compo', @equivalentId)

SET @equivalentId = '696660dd-dcfe-4ee3-9ed3-acc89e95de6d'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('contract', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('agreement', @equivalentId)

SET @equivalentId = '7e119721-fff8-4e06-95b1-6e9b1d5d0665'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('CPU', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('central processing unit', @equivalentId)

SET @equivalentId = '7f28d6ce-4058-4fe4-a595-feae704f7b26'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('crm', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('customer relationship management', @equivalentId)

SET @equivalentId = 'c8fabdaf-cb4c-4d46-977b-9f8be5eb7656'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('cv', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('curriculum vitae', @equivalentId)

SET @equivalentId = '8ea305c3-423b-4993-a178-dabc2b090917'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('dispatch', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('despatch', @equivalentId)

SET @equivalentId = '2613a10b-31d0-4632-88d5-1c94b1782def'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('div', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('division', @equivalentId)

SET @equivalentId = '0dcf65a1-a4e3-4572-a8b1-1b9e0180cc2e'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('DNS', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Domain Name System', @equivalentId)

SET @equivalentId = '805ed604-da56-4147-902b-7bc0d7ec3711'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('dr', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('direct response', @equivalentId)

SET @equivalentId = '0eb36bd9-6c49-4031-aa33-03568e9cdfae'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('drp', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('disaster recovery plan', @equivalentId)

SET @equivalentId = 'c2fb7f0d-0a17-4318-af5b-f18f1a0be410'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('DSL', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Digital Subscriber Line', @equivalentId)

SET @equivalentId = '73b87398-f834-4ef6-8134-8ee77e3b3a78'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('elec', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('electrical', @equivalentId)

SET @equivalentId = '516ead2c-8e7b-45e0-bfa1-25375aa85615'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('email', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('e-mail', @equivalentId)

SET @equivalentId = '1446dfdb-665f-4dff-b5dd-b99a70725e11'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('epcm', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Engineering Procurement Construction Management', @equivalentId)

SET @equivalentId = '3c4aca3c-f6f2-4b98-8c70-5a767ebebaba'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('EPS', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('earnings per share', @equivalentId)

SET @equivalentId = '9ce8caf7-e4b2-4962-ac56-cf07cc56a0cb'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('exp', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('experience', @equivalentId)

SET @equivalentId = '1d31282a-11c5-49e0-b906-694051f1748e'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('fifo', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('first in first out', @equivalentId)

SET @equivalentId = 'd0b19ac4-104a-4e84-a7ba-d0e8a5114a1c'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('first aid', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('firstaid', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('first-aid', @equivalentId)

SET @equivalentId = '8d085ea0-278c-4399-b2c9-323eade9e580'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('fmcg', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('fast moving consumer goods', @equivalentId)

SET @equivalentId = '8c00b1d8-2ce8-451e-b4e1-324ba2384679'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Food Processor', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Food Processing', @equivalentId)

SET @equivalentId = '47dae85c-6797-4633-8f2f-8069e8001639'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('GDP', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('gross domestic product', @equivalentId)

SET @equivalentId = 'eaa5c2ae-c25d-4462-8fc4-09a911735b63'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('GNP', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('gross national product', @equivalentId)

SET @equivalentId = 'a9e2f5d6-9ba0-482c-82a9-5cf10c09c7e0'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('govt', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('government', @equivalentId)

SET @equivalentId = 'd382143c-5d1c-49be-bdb4-75e38d6bb60b'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('gp', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('general practisioner', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('general practitioner', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('medical officer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('vmo', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('registrar', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('physician', @equivalentId)

SET @equivalentId = '2fb130a6-be70-4e98-993d-5333c02fc748'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('GPS', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Global Positioning System', @equivalentId)

SET @equivalentId = 'ab889968-5e62-46bd-82a3-0907a4feddb3'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('guard', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('gaurd', @equivalentId)

SET @equivalentId = '0e383545-37d7-4c91-8503-042f2d9d3be3'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Health & Safety', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Health and Safety', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Health&Safety', @equivalentId)

SET @equivalentId = '2fc01870-d94e-4d5b-bccc-e80d499d4ed6'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Healthcare', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Health Care', @equivalentId)

SET @equivalentId = '4916d197-5023-497a-9607-a479c3302a6e'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('hp', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('hewlett packard', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('hewlettpackard', @equivalentId)

SET @equivalentId = 'b0f17f43-22ca-4a61-b175-3c47b37f5df1'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('hris', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('human resource information system', @equivalentId)

SET @equivalentId = 'e2dd61d3-845c-4707-8cc6-48d35ce4ffdc'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('ie', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('institute of engineers', @equivalentId)

SET @equivalentId = 'dd6384ad-eb78-4d7e-a7ba-97681d1c4480'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('infra structure', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('infrastructure', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('infra-structure', @equivalentId)

SET @equivalentId = '32b6a87b-85c1-49bd-9cdb-0bdcadc60b4a'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('jdedwards', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('jd edwards', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('j.d.edwards', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('jde', @equivalentId)

SET @equivalentId = '7179a471-54fc-4cd4-8cd0-156ddb1c30e9'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('jnr', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('junior', @equivalentId)

SET @equivalentId = '57cb483e-9e4c-417f-8f3a-91e1c1e0f105'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('KPI', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('key performance indicator', @equivalentId)

SET @equivalentId = '38022b32-1989-4212-b6c0-dae5219f6e13'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('l&d', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('learning and development', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('training', @equivalentId)

SET @equivalentId = '525d7add-ee6e-403f-98e0-bd38f4d4e770'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('labor', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('labour', @equivalentId)

SET @equivalentId = 'a59e4e9b-1ddf-4ada-a18a-fc5a3c3da1dc'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('lan', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('local area network', @equivalentId)

SET @equivalentId = 'a748eed7-2726-4df9-af24-bc965320b249'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('ltd', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('limited', @equivalentId)

SET @equivalentId = '0d7b8b4e-05af-4e15-95ce-09c3d1e09f60'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Mac', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Macintosh', @equivalentId)

SET @equivalentId = '184dda70-6524-4964-8c53-fd6521bb0cf2'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('macq', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('macquarie', @equivalentId)

SET @equivalentId = '0132b7e9-ccdc-4d7f-adfb-71b5aa1edc79'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('mba', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('master of business administration', @equivalentId)

SET @equivalentId = 'de5dc2ac-21e1-4565-ab79-dfd1c8a51f8f'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('mech', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('mechanical', @equivalentId)

SET @equivalentId = 'cff8f365-5b45-4e5e-80ce-2ad0bc66431c'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('med', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('medical', @equivalentId)

SET @equivalentId = '45cee93a-6740-4d65-b180-c3fe6c4b464c'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('melb', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('melbourne', @equivalentId)

SET @equivalentId = '115f357c-a5e0-4268-9d95-64336e4edec4'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('mngt', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('management', @equivalentId)

SET @equivalentId = 'dc2945b9-d23b-4a79-a8ab-d34c8c163966'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('mr', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('medium rigid', @equivalentId)

SET @equivalentId = 'ea7efea6-89e0-4e3c-9d7d-104b63cf3928'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('ms', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('microsoft', @equivalentId)

SET @equivalentId = 'ef6113e5-81ab-46e0-80b5-f6d5ec31a9d5'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('NAB', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('National Australia Bank', @equivalentId)

SET @equivalentId = '7298f7e4-a5d8-4fce-be57-3c32ffe3c49a'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('north east', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('northeast', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('north-east', @equivalentId)

SET @equivalentId = 'f69677ec-6e93-4f0d-b267-dd955c2c72de'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('neg', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('negotiable', @equivalentId)

SET @equivalentId = 'b1d44696-0daa-47d5-b2f9-883fc5da5f69'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('nsw', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('new south wales', @equivalentId)

SET @equivalentId = '8173ec4a-c3bf-4cdd-a88b-1d4be3d382b9'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('north west', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('northwest', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('north-west', @equivalentId)

SET @equivalentId = 'b6404d1c-4361-4ebc-84e9-4336618c4d0c'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('nyse', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('new york stock exchange', @equivalentId)

SET @equivalentId = 'b9d7acd2-a99a-4ada-8b91-eb998994b6b5'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('nz', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('new zealand', @equivalentId)

SET @equivalentId = 'b56508f8-8c54-4a62-a7ae-4f73bf71f734'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('OHS', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('OH&S', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Occupational Health and Safety', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Occupational Health & Safety', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('oh &s', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('ehs', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('environmental health and safety', @equivalentId)

SET @equivalentId = 'e9bc4042-94dd-4a17-80cb-844d4be21c8f'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Oil and Gas', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Oil&Gas', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Oil & Gas', @equivalentId)

SET @equivalentId = 'ff23744c-1ecc-4da1-9d67-15ed896b10d2'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('online', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('on-line', @equivalentId)

SET @equivalentId = '65628d2b-684a-40cf-a4d0-a91ecd204b07'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('os', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('operating system', @equivalentId)

SET @equivalentId = '4df221ca-3cdf-4fdd-b893-da31f2798a2d'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('ot', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('occupational therapist', @equivalentId)

SET @equivalentId = 'df5baf9e-4c5c-4ce1-acab-111c2063de33'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('p&l', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('profit and loss', @equivalentId)

SET @equivalentId = 'efd8dec9-ac61-465b-92ea-50f33c6d91b3'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('pae', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('participating administrative entity', @equivalentId)

SET @equivalentId = 'bf077eaf-4718-4833-afb8-f2c8a1794061'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('payg', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('pay as you go', @equivalentId)

SET @equivalentId = '80467c30-9f8f-48c6-9c31-9fd0c1f9a031'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('perm', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('permanent', @equivalentId)

SET @equivalentId = 'fc37212c-b3d0-42eb-b314-964e3ab38510'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('pi', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('personal injury', @equivalentId)

SET @equivalentId = '7f6a2244-df30-4a1c-afbd-503c817f6668'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('plc', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('publically listed company', @equivalentId)

SET @equivalentId = 'b9313b05-3589-4329-a2d4-59f691b5f74e'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('POS', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Point of Sale', @equivalentId)

SET @equivalentId = '3b80bc60-f6f9-4b05-91f9-9682e94e1fb2'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('proactive', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('pro active', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('pro-active', @equivalentId)

SET @equivalentId = '873783d8-ec05-41ed-a2d1-152799898472'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Psych', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('psychologist', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('psychology', @equivalentId)

SET @equivalentId = '8da68f02-18a3-4263-a899-202c52285ac4'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('pty', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('proprietary', @equivalentId)

SET @equivalentId = 'fe23a6bc-c590-44c1-bc68-c8fc6d638647'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Q&A', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Quality and Assurance', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Quality & Assurance', @equivalentId)

SET @equivalentId = '1b65ac06-cd5a-40b4-97a6-1d247f167b50'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('qa', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('quality assurance', @equivalentId)

SET @equivalentId = 'e34edd86-d5ff-4ac7-b709-262250ed68cf'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('qld', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('queensland', @equivalentId)

SET @equivalentId = '9a61c936-b452-49b8-9cf8-cde495a00ce1'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('qual', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('qualification', @equivalentId)

SET @equivalentId = 'a86a2b30-e9d0-4ec0-8fc1-6110792d3c8c'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('quick books', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('quickbooks', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('quick-books', @equivalentId)

SET @equivalentId = '5ec80b0d-6e14-4a77-80da-1a4ee6e68357'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('r and d', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('research and development', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('research & development', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('rnd', @equivalentId)

SET @equivalentId = '71a46965-3a96-44e3-b302-960c9be0b4c5'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('real estate agent', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('officer in effective control', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('realestate consultant', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('property consultant', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('buyers advocate', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('buyer s advocate', @equivalentId)

SET @equivalentId = '44e08aec-0dcc-4b20-977d-bce42d9fa26f'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('rec2rec', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('recruiter to recruiter', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('rec-to-rec', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('rec to rec', @equivalentId)

SET @equivalentId = 'b99d4f3f-3147-4eaa-be75-f0dc262d7f6b'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('reengineering', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('re engineering', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('re-engineering', @equivalentId)

SET @equivalentId = 'afb89b58-7035-49a5-93b3-a5c86e725fef'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('ref', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('reference', @equivalentId)

SET @equivalentId = '45c4543f-bea2-4cbc-ab63-4aac18cf826e'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('rehab', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('rehabilitation', @equivalentId)

SET @equivalentId = '11d842e0-2e94-455d-a1b6-4cce80e513d3'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('req', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('requirement', @equivalentId)

SET @equivalentId = '2a76aca8-9d0e-41ab-a3f6-58c71d4cb5c4'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('rf', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('radio frequency', @equivalentId)

SET @equivalentId = '6b07cfdf-c105-49a8-ab5e-a41e1a64088b'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('ROE', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('return on equity', @equivalentId)

SET @equivalentId = '4ca9ffdb-cfb8-451a-965b-f5a821b89918'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('ROI', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('return on investment', @equivalentId)

SET @equivalentId = '414db13d-de51-450c-805b-9e5465e31ee7'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('romp', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Radiation Oncology Medical Physics', @equivalentId)

SET @equivalentId = '76c8bc46-f12c-4f3a-82bd-afee5f426c33'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('rta', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('road traffic authority', @equivalentId)

SET @equivalentId = '3794b4b6-2c5d-4a58-b5a2-54fbc3107824'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('rtw', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('return to work', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('return-to-work', @equivalentId)

SET @equivalentId = 'b21e2b1b-be6a-4d3d-9666-16bc1d30e1db'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('sap', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('s.a.p.', @equivalentId)

SET @equivalentId = '5b9cf10b-1be7-4433-8109-24133e3bddc6'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('SAP EP', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Enterprise Portal', @equivalentId)

SET @equivalentId = '0fb87edf-46c6-4a62-b3c6-95e7896d6f65'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('south east', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('southeast', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('south-east', @equivalentId)

SET @equivalentId = '7bea0f86-2322-4c30-a151-fc0fb6205721'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('south west', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('southwest', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('south-west', @equivalentId)

SET @equivalentId = 'dab9e38a-129b-4a39-8d17-7997153e43f6'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('SEM', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Search Engine Marketing', @equivalentId)

SET @equivalentId = 'b4a041af-d9c5-4215-81c0-717972871ed7'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('SEO', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Search Engine Optimisation', @equivalentId)

SET @equivalentId = '7ff7f24b-60d4-487d-88bc-1ad4a6b4344e'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('share point', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('sharepoint', @equivalentId)

SET @equivalentId = '3261446e-008e-4fa8-9441-d3df0a1fce0d'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('sme', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('small and medium enterprise', @equivalentId)

SET @equivalentId = '2724f6c8-a2b9-4384-bb1e-8b98ff11f4f4'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('sml', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('small', @equivalentId)

SET @equivalentId = '8ce341a2-f60c-45ed-a500-87281654325a'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('snr', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('senior', @equivalentId)

SET @equivalentId = '4c90fc85-7386-475a-b5af-ba555bd89c6b'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('software', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('soft ware', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('soft-ware', @equivalentId)

SET @equivalentId = '80b32e80-2101-4fff-b7be-66c545e1cc3c'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('strategic', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('strategy', @equivalentId)

SET @equivalentId = 'e556a2ee-5b0c-473f-b9d6-3d2d5a50215d'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('syd', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('sydney', @equivalentId)

SET @equivalentId = '726b1c67-7903-4dcb-a820-3606ad08dab4'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('tech', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('technician', @equivalentId)

SET @equivalentId = 'cb787006-95df-440a-97b7-490f6120517a'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('temp', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('temporary', @equivalentId)

SET @equivalentId = 'f9f0166d-dea7-4087-9f12-f7fa3bc1612f'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('through', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('thru', @equivalentId)

SET @equivalentId = '985cb536-1f42-4d1a-877e-6e70ca7b6538'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('tire', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('tyre', @equivalentId)

SET @equivalentId = '2b125789-9c78-4a21-bece-813d774fd268'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('tkt', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('ticket', @equivalentId)

SET @equivalentId = '1bdb7ac2-dd4e-4a29-8178-49762eb75478'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('trim', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Total Records and Information Management', @equivalentId)

SET @equivalentId = '74cbd217-fe53-4993-87e7-8e78fe5deabb'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('tv', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('television', @equivalentId)

SET @equivalentId = 'f217d31d-60f1-408d-a3f8-b4ea50f71c96'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('uat', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('user acceptance testing', @equivalentId)

SET @equivalentId = '039275f4-182c-48e4-8a52-3a801ddfa665'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('ui', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('user interface', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('u.i.', @equivalentId)

SET @equivalentId = '7810b14d-1dbf-48a7-ad36-346881130aec'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('uni', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('university', @equivalentId)

SET @equivalentId = 'b403429d-ae87-4e48-a9f7-65b927cba45c'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('vic', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('victoria', @equivalentId)

SET @equivalentId = 'dd44e361-03c2-4ce0-97ce-931ae6bd3d57'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('act', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('australian captial territory', @equivalentId)

SET @equivalentId = '4bfc584f-9469-4c85-8b59-1a98dbb8bced'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('vpn', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('virtual private network', @equivalentId)

SET @equivalentId = '927960ef-cafe-4e2a-9155-073ea4ab934c'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('VSD', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Variable Speed Drives', @equivalentId)

SET @equivalentId = '1a86da15-ce99-4625-bd41-abd54e7db5c9'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('wa', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('western australia', @equivalentId)

SET @equivalentId = '4d813f9d-dc01-4b20-ab7b-9020c0ec97b3'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('wan', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('wide area network', @equivalentId)

SET @equivalentId = '97ffdbe9-576d-49f2-b669-f8ce2323a4a0'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('wk', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('week', @equivalentId)

SET @equivalentId = '99cdfdbd-c9a9-4309-8a53-55a74effb6f7'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Word', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Synonyms', @equivalentId)

SET @equivalentId = 'a2ad5441-487e-448c-bdc2-d78bf6e4bc15'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('xray', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('x-ray', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('x ray', @equivalentId)

SET @equivalentId = '33d63434-397e-4ac9-9973-a6988cb3e262'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('program', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('programme', @equivalentId)

SET @equivalentId = 'a737478e-217d-4fe8-91cc-2bbdd0eb8f4a'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('expert', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('guru', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('specialist', @equivalentId)

SET @equivalentId = 'ac14ba39-c95a-4301-9f66-bcd87ef056fd'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('butcher', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('butchery', @equivalentId)

SET @equivalentId = '7759ea16-8767-4cb0-bed2-a871625a8ff8'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('hvac', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('heating ventilation air conditioning', @equivalentId)

SET @equivalentId = '5e6e4199-1ebd-4ddc-adcc-9bc3d6f9570d'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('air con', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('air conditioning', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('aircon', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('airconditioning', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('air conditioner', @equivalentId)

SET @equivalentId = '400981f1-06cd-4dc2-8a69-05ad84faa4a5'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('exec', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('executive', @equivalentId)

SET @equivalentId = '397e34f2-3946-4424-8a2e-9b6fe474dfca'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('apn', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('australian provincial news', @equivalentId)

SET @equivalentId = '2c846a40-308a-4e0b-ad89-5f4bf2414d78'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('student', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('undergrad', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('under grad', @equivalentId)

SET @equivalentId = 'a758fcf3-c122-41ed-abae-5eff2f6f1d5b'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('grad', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('graduate', @equivalentId)

SET @equivalentId = '40da1791-a289-4771-b1bc-346d14f1748e'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('aqtf', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('australian quality and training frameworks', @equivalentId)

SET @equivalentId = '3f8cbdbb-13db-4916-9e8c-7b682d68062f'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('rto', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('registered training organisation', @equivalentId)

SET @equivalentId = '3e5685b8-b9d3-44fb-9a7b-3bf39018fe45'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('organisation', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('organization', @equivalentId)

SET @equivalentId = 'be6e6b39-ed38-450c-ad39-e3c49946b667'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('reg', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('regd', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('registered', @equivalentId)

SET @equivalentId = 'ef128aa8-850f-4612-abf0-5b962742d8ed'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('nlp', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('neuro linguistic programming', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('neurolinguistic programming', @equivalentId)

SET @equivalentId = '32ccb830-c31c-42c4-8deb-a6cfdb5e359e'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('bcg', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('boston consulting group', @equivalentId)

SET @equivalentId = '26c5a2e8-9c71-49cd-8bb8-ee9460ff3221'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('mobil', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('exon mobil', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('exonmobil', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('exxon', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('exxonmobil', @equivalentId)

SET @equivalentId = '97f2f5a5-7f0e-4e08-8090-893553c422bd'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('bp', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('brittish petroleum', @equivalentId)

SET @equivalentId = '0eaa996f-ba68-4f9c-8d0d-96e40cd9ab2a'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('aicd', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('austrlalian institute of company directors', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('maicd', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('faicd', @equivalentId)

SET @equivalentId = '151a52f6-0af4-4098-80c2-e18747281c8e'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('circa', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('around', @equivalentId)

SET @equivalentId = '4967da15-27b9-4d6e-bb99-3f034768406d'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('afp', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('australian federal police', @equivalentId)

SET @equivalentId = '7abfb275-494e-4c7e-8767-3ef7f16058e3'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('asic', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('australian securities and investment commission', @equivalentId)

SET @equivalentId = '494dfb6f-4cf8-4ec1-96c3-680e2b778102'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('jv', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('j v', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('joint venture', @equivalentId)

SET @equivalentId = 'd9a393a7-32ee-444f-8400-5887f3b6fc98'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('uk', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('united kingdom', @equivalentId)

SET @equivalentId = '84b69963-d6a4-45f5-867f-56a4ac762416'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('pwc', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('price waterhouse coopers', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('pricewaterhouse coopers', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('price water house coopers', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('coopers and lybrand', @equivalentId)

SET @equivalentId = 'fb2938f0-1773-4c63-b44e-51088ed83c3d'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('deloitte', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('deloitte touche', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('deloitte touche tohmatsu', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Duesburys', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('deloittetouche', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('deloittetouchetohmatsu', @equivalentId)

SET @equivalentId = 'b6f159fd-134c-4ef8-a9b6-fba03a2bc5f4'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('ey', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('e&y', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('ernst and young', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('ernst & young', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('ernst&young', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('ernst & whinney', @equivalentId)

SET @equivalentId = '5b07db4b-2cdb-4b21-8a75-60569ff92828'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('kpmg', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('hungerfords', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('peat marwick', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('peatmarwick', @equivalentId)

SET @equivalentId = '28e73adb-44e6-49c3-bd99-d32c30f80f85'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('csl', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Commonwealth Serum Laboratories', @equivalentId)

SET @equivalentId = 'ade08864-b987-410c-bdca-bf422c20b415'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('eds', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('electronic data systems', @equivalentId)

SET @equivalentId = 'c9f6a6a3-3ed7-4e53-a616-fcaa6db5ec63'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('m&a', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('mergers and acquisitions', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('mergers & acquisitions', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('mergers&acquisitions', @equivalentId)

SET @equivalentId = '3672d353-27ac-4eba-b28c-bf97b18beca8'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('arab', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('arabic', @equivalentId)

SET @equivalentId = '33e8e152-95a2-4b1c-8fd7-d5b48ef26c58'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('tac', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('transport accident commission', @equivalentId)

SET @equivalentId = '5324afb7-104a-4b3d-808c-f2394bc8b61c'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('tax', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('taxation', @equivalentId)

SET @equivalentId = '3155cc3b-9baf-497d-b274-acdbb0c44e90'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('interor design', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('interior designer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('interior decorator', @equivalentId)

SET @equivalentId = '5aa86898-5ab2-42a0-a7d6-338b3b3d137e'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('reporter', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('journalist', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('journalism', @equivalentId)

SET @equivalentId = '415f1b0c-40a9-45a0-9835-1c3b277b269c'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('actuary', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('actuarial', @equivalentId)

SET @equivalentId = '60815c60-192e-4aa6-84d1-b7a664be3359'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('tresury', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('treasury', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('treasurer', @equivalentId)

SET @equivalentId = 'f7361a80-12fa-4b92-a271-a1c262967219'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('call centre', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('call center', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('contact centre', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('callcentre', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('callcenter', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('contactcentre', @equivalentId)

SET @equivalentId = 'e69cb40c-b4b6-4775-ad24-1b1052980c16'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('cold call', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('cold calling', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('outbound', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('out bound', @equivalentId)

SET @equivalentId = '1ca05ecd-1cdf-4386-9584-ab1c28e1961b'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('worker', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('officer', @equivalentId)

SET @equivalentId = 'eacf4596-a6a2-4e93-8123-053b54ea023b'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('youth', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('adolescent', @equivalentId)

SET @equivalentId = '9a2add61-76a1-4a3c-a041-0420fef5e5a7'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('apprentice', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('apprenticeship', @equivalentId)

SET @equivalentId = 'a1ea7169-5b2e-4f23-9793-194a30cf0df8'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('kinder', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('kindergarten', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('kinda', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('childcare centre', @equivalentId)

SET @equivalentId = '901e17e5-aa79-4a95-ac5a-e5c70fc1b759'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('centre', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('center', @equivalentId)

SET @equivalentId = '34034c63-e69d-469b-b046-725015ad28ec'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('child', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('children', @equivalentId)

SET @equivalentId = '163badc3-269f-448c-8e44-cce0da6f3f5b'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('educator', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('tutor', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('trainer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('instructor', @equivalentId)

SET @equivalentId = '5c079052-9382-4cfe-9224-57872380fec9'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('waste water', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('wastewater', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('storm water', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('stormwater', @equivalentId)

SET @equivalentId = '49582552-dc2b-433b-aeda-00af1497e2d7'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('hydrology', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('hydrologist', @equivalentId)

SET @equivalentId = '28f3c5e2-2b04-4830-9221-d1419b3d1d10'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('town planner', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('town planning', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('townplanner', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('urban planner', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('urban planning', @equivalentId)

SET @equivalentId = 'dbdf3159-2427-4b64-8a3f-fedf4f003363'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('town', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('urban', @equivalentId)

SET @equivalentId = '90131a44-feeb-43f1-839d-784b8a4f3131'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('uae', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('united arab emirates', @equivalentId)

SET @equivalentId = 'f7fd545e-7750-4b2c-9fbe-8e793dc2ec6d'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('geo technical', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('geotechnical', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('geo tech', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('geotech', @equivalentId)

SET @equivalentId = '12026c05-2a66-4e73-b8f3-e7bc2d33b32a'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('lead', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('head', @equivalentId)

SET @equivalentId = '3df9d5dc-e24b-4b85-b26e-298cc8f14dc8'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('rail', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('railway', @equivalentId)

SET @equivalentId = '9ca1f9f9-4728-438c-847d-ef60680a2aee'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('nurse', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('nursing officer', @equivalentId)

SET @equivalentId = '37417070-1106-4e2a-8d05-3c755bb8d5f7'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('defence', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('defense', @equivalentId)

SET @equivalentId = '4b33cbec-480d-4d6c-874f-4437dd762fdb'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('air force', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('airforce', @equivalentId)

SET @equivalentId = 'b56813d6-42e0-47d4-a726-a9333e49a09c'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('aeroport', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('aero port', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('airport', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('air port', @equivalentId)

SET @equivalentId = '60cb6391-5191-4bd3-b0b0-1972341a785e'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('ammunition', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('munition', @equivalentId)

SET @equivalentId = 'a2f22458-0677-4501-af5d-8d9171adee13'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('cbms', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('central budget management system', @equivalentId)

SET @equivalentId = 'd4453ca8-26bf-49ee-86da-1bba1d9e42e0'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('australia post', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('auspost', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('australiapost', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('austpost', @equivalentId)

SET @equivalentId = 'fd4a664d-ae6a-4fac-a447-348428b56233'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('ict', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('information communication and technology', @equivalentId)

SET @equivalentId = 'f466fbee-7061-4207-87e6-5a6edcd769f4'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('aged care', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('agedcare', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('elderly care', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('elderlycare', @equivalentId)

SET @equivalentId = '51da75be-dec8-406d-827b-976ba1f306ff'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('disability', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('handicapped', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('handicap', @equivalentId)

SET @equivalentId = '8477869b-3dcb-44fd-b8da-fcdd7fab25f5'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('hospice', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('palliative', @equivalentId)

SET @equivalentId = '8c58fa1e-470a-4ad9-be29-8d546047cc4f'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('chemo', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('chemotherapy', @equivalentId)

SET @equivalentId = '8f63b296-6345-498f-97f4-b8ff1c6fc35e'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('een', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('endorsed enrolled nurse', @equivalentId)

SET @equivalentId = '565bba32-2b5b-417a-946c-4c2ae0161626'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('physio', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('physiotherapy', @equivalentId)

SET @equivalentId = 'e301ed5b-a83c-43bd-bdca-e7b72cab8cad'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('nutriciantist', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('dietician', @equivalentId)

SET @equivalentId = '568bf5a6-5baf-453e-8757-aa0550a466d6'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('case manager', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('case worker', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('case officer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('inury manager', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('rtw officer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('case coordinator', @equivalentId)

SET @equivalentId = 'e6b8e87b-0c39-456a-9e07-9e33b8ab2332'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('occ', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('occupational', @equivalentId)

SET @equivalentId = '814426de-3434-4c6d-9446-37bcaf2a2498'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('rec', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('recreational', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('recreation', @equivalentId)

SET @equivalentId = '8d2992a4-2051-4284-91ff-ea1469756f69'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('speech therapist', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('speech pathologist', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('language therapist', @equivalentId)

SET @equivalentId = '5d6d7f6e-64a2-4dde-850e-8cea3d610118'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('ambulance', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('paramedic', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('para medic', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('ambulance driver', @equivalentId)

SET @equivalentId = '3cef2dae-47ac-4487-a7a5-a9a00fda3205'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('scra', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('society of clinical research associates', @equivalentId)

SET @equivalentId = 'ffb08012-ddbb-40e8-b778-28c685be6a39'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('anaesthetic', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('anaesthetist', @equivalentId)

SET @equivalentId = 'cbad4253-526f-4c24-8554-e507d66a9c13'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('a&e', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('accident and emergency', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('emergency medicine', @equivalentId)

SET @equivalentId = 'e755a8fc-8fbb-4b5a-b526-4f2fe8f15227'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('midwife', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('mid wife', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('mid wifery', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('midwifery', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('mid wive', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('midwive', @equivalentId)

SET @equivalentId = 'a265cf23-5553-4d9a-81de-d306c2f18246'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('icu', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('intensive care unit', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('intensive care', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('intensivecare', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('msicu', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('critical care medicine', @equivalentId)

SET @equivalentId = '8297a3e1-81a9-46d5-816f-08c570ad482f'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('picu', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('paedeatric intesive care', @equivalentId)

SET @equivalentId = 'fb85b4e4-7b2e-45db-97f1-83233e8fd9d8'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('nicu', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('neonatal intesive care', @equivalentId)

SET @equivalentId = 'dc9f66e9-6938-4b02-870d-b8e09b55d590'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('safety officer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('safety inspector', @equivalentId)

SET @equivalentId = '85c1c3d4-d594-4951-9f19-f656d35a925f'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('concierge', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('bell attendant', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('porter', @equivalentId)

SET @equivalentId = '8a2abb76-98dd-423c-b058-418d2e7e895c'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('grounds man', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('groundsman', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('grounds keeper', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('groundskeeper', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('ground attendant', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('public area attendant', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('green keeper', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('greenkeeper', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('turf manager', @equivalentId)

SET @equivalentId = '87fe723c-5e5c-4047-9e42-3f3a3fee75af'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('dish washer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('dishwasher', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('kitchen assistant', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('sandwich hand', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('sandwichhand', @equivalentId)

SET @equivalentId = 'd6f802f8-879e-4058-be68-04730f79c1e7'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('tour guide', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('tour director', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('bus guide', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('tourguide', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('resort guide', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('tour driver', @equivalentId)

SET @equivalentId = 'c95b2030-e37b-47ca-9994-fe099f93eeec'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('japan', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('japanese', @equivalentId)

SET @equivalentId = '8aa5c942-7fc8-44ab-9d6b-978520d5e200'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('china', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('chinese', @equivalentId)

SET @equivalentId = '164248ec-1e40-46bb-be6b-cbe645827ade'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('france', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('french', @equivalentId)

SET @equivalentId = '741cd5e6-5ae4-41db-b7a6-e1d83cf71e87'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('germany', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('german', @equivalentId)

SET @equivalentId = 'e8551dad-398f-4989-a38b-b35536e30935'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('ir', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('industrial relations', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('workplace relations', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('work place relations', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('employee relations', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('er', @equivalentId)

SET @equivalentId = 'd4586d3a-895c-4f24-aa4c-380b2a43c8c8'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('job network', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('jobnetwork', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('job placement', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('jpo', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('centrelink', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('centre link', @equivalentId)

SET @equivalentId = 'd8688a04-bbe9-415c-99d8-3db334ccb3c3'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('rpo', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('recruitment process outsourcing', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('on site', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('hro', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('hr process outsourcing', @equivalentId)

SET @equivalentId = '1ebfaa7d-d0a3-4fd4-92e5-f6421bd02193'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('od', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('organisation development', @equivalentId)

SET @equivalentId = '05d6ce5f-2848-41c6-9933-e895b5326c0f'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('cqi', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('continuous quality improvement', @equivalentId)

SET @equivalentId = '3a16969a-4172-4ad2-9fd8-a8b2ecea8f76'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('webmethods', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('web methods', @equivalentId)

SET @equivalentId = 'bddaeb19-f17f-47ca-bfcc-5e0e6c17c703'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('gis', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('geographic information system', @equivalentId)

SET @equivalentId = '0bd5b9a4-e467-4d13-9d03-2c94b07532e4'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('hyper text', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('hypertext', @equivalentId)

SET @equivalentId = '1af48c19-4dae-4ef5-9dc4-5f9a89874b63'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('3d', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('3 dimensional', @equivalentId)

SET @equivalentId = 'a6bedf7c-9e85-458e-8393-91f0c1c6336e'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('mainframe', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('main frame', @equivalentId)

SET @equivalentId = '4812304a-2dba-4026-b4da-b86a995fe444'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('scrum', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('agile development', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('agile software development', @equivalentId)

SET @equivalentId = '681593b2-9e0c-4ce4-86ad-386bafa7e4b3'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('biztalk', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('biz talk', @equivalentId)

SET @equivalentId = '48183be3-0dcb-4084-bec9-6a754668c316'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('database', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('data base', @equivalentId)

SET @equivalentId = 'aef489c9-cc97-4080-8d0e-c36bde8dc4e7'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('c#', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('c sharp', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('csharp', @equivalentId)

SET @equivalentId = '0af9ca31-ca13-40e9-98e1-663afb4cd4f2'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('.net', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('dot net', @equivalentId)

SET @equivalentId = 'ede6d92d-fa1f-450d-a4b3-63a841e317c1'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('gui', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('graphic user interface', @equivalentId)

SET @equivalentId = 'ce20a863-6898-4b92-b311-acfaf74a9ea0'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('power builder', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('powerbuilder', @equivalentId)

SET @equivalentId = 'd43935ca-8609-405a-80be-50daeb1dbab9'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('cti', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('computer technology integration', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('computerised technology integration', @equivalentId)

SET @equivalentId = 'd9c66aed-c9ee-4e25-8645-76d1b47b8670'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('soa', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('service oriented architecture', @equivalentId)

SET @equivalentId = '8ad3a18f-eecd-4c96-88f2-4777676deb64'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('iso', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('international standards association', @equivalentId)

SET @equivalentId = '5c30c9f5-2977-4ffe-b27f-10e43650d336'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('rem', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('remuneration', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('salary', @equivalentId)

SET @equivalentId = 'e57816d6-6a5f-4110-8b05-1a986457a8de'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('smsf', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('self manager super fund', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('self manager superannuation fund', @equivalentId)

SET @equivalentId = '40b2f7dc-5d78-4c5c-8de0-dadee029842a'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('worksafe', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('work safe', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('work cover', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('workcover', @equivalentId)

SET @equivalentId = '9c754cec-4518-4125-a29d-1c7118db7298'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('j2ee', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('j2e', @equivalentId)

SET @equivalentId = '5168b986-e0ce-4695-85fb-0a84044c604d'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('sql', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('structured query language', @equivalentId)

SET @equivalentId = 'c60baf0e-1999-4b10-895a-78f29e510bb2'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('javascript', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('java script', @equivalentId)

SET @equivalentId = 'ba52e31f-3b63-476f-a2af-6b4378e79ab3'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('4gl', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('4th general language', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('forth general language', @equivalentId)

SET @equivalentId = '5e284fde-0fc1-4e41-a9f5-d8f28f3f6cfa'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('sqlserver', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('sql server', @equivalentId)

SET @equivalentId = '295a6e61-de79-4559-b410-82ca10f8b752'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('datawarehouse', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('data warehouse', @equivalentId)

SET @equivalentId = '09dd3bfe-a8f0-49c0-982b-5a2b8dba30ee'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('gl', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('general ledger', @equivalentId)

SET @equivalentId = '3116d8cb-2fd9-413c-a875-ba7d7ca008a5'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('ldap', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('lightweight directory access protocol', @equivalentId)

SET @equivalentId = '1574f7c0-c98d-4bd7-8522-63c0ae43efef'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('records clerk', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('records officer', @equivalentId)

SET @equivalentId = 'd46a54f6-67cb-4faf-976a-4187cc2220d5'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('inhouse', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('in house', @equivalentId)

SET @equivalentId = '1b90be30-1ba0-4cf1-858e-e67fa557c406'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('womenswear', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('womens wear', @equivalentId)

SET @equivalentId = '1ed86007-5fe2-4dad-9795-5607c2958196'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('menswear', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('mens wear', @equivalentId)

SET @equivalentId = '149ecbff-d812-493c-b22b-46c94605d212'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('cnc', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('computer numeric conrol', @equivalentId)

SET @equivalentId = '4150d32e-6ba9-47b7-a9f4-de09cc008910'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('toolmaker', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('tool maker', @equivalentId)

SET @equivalentId = '174a7af7-eb5b-4101-8eaa-edf1bcdbc74b'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('datacentre', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('data centre', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('data center', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('datacenter', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('hosting center', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('hosting centre', @equivalentId)

SET @equivalentId = '0001eeb5-74f8-47da-a978-3c06177eff11'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('mysql', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('my sql', @equivalentId)

SET @equivalentId = 'fc304a79-fbb4-4c50-84a0-81e37f774dcd'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('as400', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('as 400', @equivalentId)

SET @equivalentId = '92cb831d-825f-469e-b99e-de3d8aa9b3dd'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('ccnp', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('cisco certified network professional', @equivalentId)

SET @equivalentId = '005b6556-a993-4c3b-850e-a0d5a67ede7a'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('ccna', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('cisco certified network associate', @equivalentId)

SET @equivalentId = '3c4f2db6-4de2-400f-b0cd-6c659f00a06b'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('msce', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('microsoft certified systems engineer', @equivalentId)

SET @equivalentId = '47d52d36-75e6-4390-aacd-62930ff7c02a'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('noc', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('netwrok oprations centre', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('network operations center', @equivalentId)

SET @equivalentId = 'd4956996-e64c-4a87-a674-7880590586c2'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('san', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('storage area network', @equivalentId)

SET @equivalentId = '9a91bfb7-70e0-40bb-bdbd-f04a16e28631'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('pstn', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('public switched telephone network', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('public switched telephony network', @equivalentId)

SET @equivalentId = 'd935070c-99a1-4944-9065-3758535157dd'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('fr', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('frame relay', @equivalentId)

SET @equivalentId = '12ed8d1d-1bb9-4229-bc62-9719514b8b3e'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('level 1', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('level1', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('1st level', @equivalentId)

SET @equivalentId = 'ac9272d2-d0e5-49ed-9732-911efb95c5c2'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('level 2', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('level2', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('2nd level', @equivalentId)

SET @equivalentId = '3e603a7c-eacf-43d0-b6f8-4045950748a9'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('level 3', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('level3', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('3rd level', @equivalentId)

SET @equivalentId = '41393c28-9961-49e8-8f2e-b84b328dd9a7'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('desktop', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('desk top', @equivalentId)

SET @equivalentId = 'd0b143ac-3d5e-4553-9dfa-b77c2a346d8d'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('pc', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('personal computer', @equivalentId)

SET @equivalentId = '38bb3acc-d5ba-48b1-8956-dd3f32510aa9'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('blackbelt', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('black belt', @equivalentId)

SET @equivalentId = '1c726bf1-720a-4480-bf1f-32111c143953'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('checkpoint', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('check point', @equivalentId)

SET @equivalentId = '74a03343-91fe-453c-a0a8-d1d35b945272'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('firewall', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('fire wall', @equivalentId)

SET @equivalentId = '2c9a49e3-442a-4af3-99c3-f92b2d08bad1'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('pki', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('public key infrastructure', @equivalentId)

SET @equivalentId = 'c32b0fdb-4dec-4a53-9b3d-565782754f17'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('ipsec', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('ip sec', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('ip security', @equivalentId)

SET @equivalentId = '180e7df9-885c-4b11-a828-ca367205671c'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('coax', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('coaxial', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('co ax', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('co axial', @equivalentId)

SET @equivalentId = '2a8dd40b-c813-4cfd-b95d-cab547b2e129'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('fibre', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('fiber', @equivalentId)

SET @equivalentId = 'b7ab6893-1c3f-4edf-bf45-96441c39f4d1'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('itil', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('information technology infrastructure library', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('it infrastructure library', @equivalentId)

SET @equivalentId = '391b2055-d23a-46a1-8df0-61622675c538'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('pipeline', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('pipe line', @equivalentId)

SET @equivalentId = '732cec91-073a-429a-80b7-19d62c360c87'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('realestate', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('real estate', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('property', @equivalentId)

SET @equivalentId = 'a3d93a6a-cb61-4059-9e0f-ab9cf060870b'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('facility', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('facilities', @equivalentId)

SET @equivalentId = 'b18b7fe9-207f-49a4-b077-a291402563f4'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('stocktake', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('stock take', @equivalentId)

SET @equivalentId = '705411ec-9110-4260-af74-87a84d37ddc9'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('mobile phone', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('mobile telephony', @equivalentId)

SET @equivalentId = '9ab3a09f-0f6b-4b3c-853b-6f5ba9741f3c'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('refridgeration technician', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('refridgeration mechanic', @equivalentId)

SET @equivalentId = '9a3a7ae8-1be9-4df7-b1e6-9a17d7e2a36a'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('cabinetmaker', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('cabinet maker', @equivalentId)

SET @equivalentId = '869e8039-fc76-4bb5-9643-17e4d0f3a36e'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('hv', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('high voltage', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('highvoltage', @equivalentId)

SET @equivalentId = '61cd29a7-8f73-49a6-ba81-9ee9cf2a08f7'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('landscaper', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('landscape gardner', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('landscape architect', @equivalentId)

SET @equivalentId = 'c52ea3a8-6162-4d3c-ac48-eeac9acd284f'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('plumber', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('gas fitter', @equivalentId)

SET @equivalentId = '0891167f-f127-4bd5-bda8-19afd54c74b6'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('security installer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('security technician', @equivalentId)

SET @equivalentId = '9ed6345d-a757-43a5-9c3d-fd94e0db469d'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('supplychain', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('supply chain', @equivalentId)

SET @equivalentId = 'f56d8c54-2512-47f0-ac86-8a0833545235'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('marine', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('maritime', @equivalentId)

SET @equivalentId = 'c20da1e9-33ee-4da6-aecc-8cb8775f1ff7'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('div1', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('division 1', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('div 1', @equivalentId)

SET @equivalentId = 'fd1c49ff-0da1-4923-a7c3-7ba4f94609ad'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('div2', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('division 2', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('div 2', @equivalentId)

SET @equivalentId = '243dbdc0-5db6-44b4-adcb-ee4b0d5b09f2'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('div3', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('division 3', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('div 3', @equivalentId)

SET @equivalentId = '35eba4ee-963b-43c5-9f00-739c34bed399'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('marine engineer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('maritime engineer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('marine technician', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('maritime technician', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('tugboat engineer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('tug boat engineer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('ship engineer', @equivalentId)

SET @equivalentId = 'f361c3f4-5a4c-4ad7-ac0d-351444ee03e9'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('air traffic controller', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('airtraffic', @equivalentId)

SET @equivalentId = '0c6de88e-ecbf-48c0-84ff-63685f04ec70'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('deckhand', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('deck hand', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('integrated rating', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('able bodied seaman', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('greaser', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('ab', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('general purpose hand', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('gp hand', @equivalentId)

SET @equivalentId = '5200274f-bb29-4848-86d6-f92645d87cdb'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('skipper', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('master v', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('master 5', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('master 4', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('master4', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('master5', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('masterv', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('coxswain', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('captain', @equivalentId)

SET @equivalentId = '59106df1-3d78-480d-aa5c-ffb07abcb030'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('cabin crew', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('cabincrew', @equivalentId)

SET @equivalentId = 'e71eac49-d30f-4860-9479-c5637d00a6ac'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('steward', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('caterer', @equivalentId)

SET @equivalentId = 'de842476-95e4-46ce-a05e-d8da75509b38'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('course super', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('course superintendant', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('golfcourse superintendant', @equivalentId)

SET @equivalentId = '8edc9ab8-0730-49c3-859a-a0c359c4015f'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('golf pro', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('golf professional', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('golfpro', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('pga professional', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('golf coach', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('golfing professional', @equivalentId)

SET @equivalentId = 'ccad6970-6c58-4028-96c2-143709d653ba'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('pga', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('professional golfers association', @equivalentId)

SET @equivalentId = '4c868259-b733-4c5c-8eb5-98788626592b'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('golf shop', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('pro shop', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('proshop', @equivalentId)

SET @equivalentId = '6f0d6590-81a6-4229-9634-c623dfcf9bd0'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('golf manager', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('golfclub manager', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('golf club manager', @equivalentId)

SET @equivalentId = '1129c57d-8686-4e8c-82dc-c07c4c2623a2'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('peoplesoft', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('people soft', @equivalentId)

SET @equivalentId = '21b3c0cf-50c2-48ad-8c22-9cfec8000c69'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('ps146', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('ps 146', @equivalentId)

SET @equivalentId = 'c6e48aef-b2e0-4698-822f-45e8f1f3bcd9'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('b2b', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('b 2 b', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('business to business', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('business 2 business', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('business2busines', @equivalentId)

SET @equivalentId = 'f9c2b448-1fc4-40a6-a4ba-3bda831fdd68'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('b2c', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('b 2 c', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('business to consumer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('business 2 consumer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('business2consumer', @equivalentId)

SET @equivalentId = 'c6be14fc-6142-4429-b2fb-76879475ef8b'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('psa', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('p s a', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('preferred supplier agreement', @equivalentId)

SET @equivalentId = '540909c2-c4c1-4a4f-b07a-00a87fe78c86'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('maternal health', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('mothercraft', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('mother craft', @equivalentId)

SET @equivalentId = 'a508c9ff-b774-4647-b8bc-dfc675ab3f2d'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('hk', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('h k', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('hongkong', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('hong kong', @equivalentId)

SET @equivalentId = 'bb70c40d-f445-46e0-9ef8-9d0f607be278'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('png', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('papua new guinea', @equivalentId)

SET @equivalentId = '327a473b-45b9-47e1-a6a9-3baa18d7e7fc'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('heathfood', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('health food', @equivalentId)

SET @equivalentId = '2b3d0e9e-20e9-4858-bc0d-00570e0ce2d6'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('hifi', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('hi fi', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('high fidelity', @equivalentId)

SET @equivalentId = '1c4456e6-fbd4-42a1-8960-60f90047512c'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('line haul', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('linehaul', @equivalentId)

SET @equivalentId = 'd3dcedc0-28e0-4dde-bdc3-323f3f993617'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('primus', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('iprimus', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('primus telecom', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('primus telecommunications', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('primustelecom', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('primustel', @equivalentId)

SET @equivalentId = '1e43c50e-f811-4ffa-9ad7-02bf60561642'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('optus', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('singtel', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('singteloptus', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('singtel optus', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('singapore telecom', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('singapore telecommunications', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('sing tel', @equivalentId)

SET @equivalentId = 'a0175f05-708b-40f9-8d48-486fb45faca8'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('melbourne uni', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('melbourneuni', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('melbourne university', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('university of melbourne', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('universityofmelbourne', @equivalentId)

SET @equivalentId = 'b4225a25-6d43-4d75-96fe-c126f191be92'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('sydney uni', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('sydneyuni', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('sydney university', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('university of sydney', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('universityofsydney', @equivalentId)

SET @equivalentId = '674a4f69-a2c1-4ba8-9bae-a5ff27ed3369'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('uni of queensland', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('university of queensland', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('queensland uni', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('qld uni', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('qld university', @equivalentId)

SET @equivalentId = '5136bd80-97f5-493d-9209-6120c24ce9b2'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('acu', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('australian catholic university', @equivalentId)

SET @equivalentId = '0defa4fc-0adf-486d-a930-2144ac00dcaa'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('bond uni', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('bond university', @equivalentId)

SET @equivalentId = 'ef984579-d619-46e0-824a-73574840114a'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('cdu', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('charles darwin university', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('charles darwin uni', @equivalentId)

SET @equivalentId = 'd680ce13-b50b-484f-bdc2-c6a4b592b6fa'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('csu', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('charles sturt university', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('charles sturt un', @equivalentId)

SET @equivalentId = '26b25aca-d731-4d29-8300-74459f14a954'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('ecu', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('edith cowan university', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('edith cowan uni', @equivalentId)

SET @equivalentId = '0a21d816-e900-44df-a7d8-00462803aeda'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('jcu', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('james cook university', @equivalentId)

SET @equivalentId = 'e01d2eba-10e1-4d99-a684-3eb130fd810b'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('latrobe', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('la trobe', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('latrobe university', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('latrobe uni', @equivalentId)

SET @equivalentId = '39887cfc-49f3-4244-a0c9-a317f8bc59e5'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('scu', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('southern cross university', @equivalentId)

SET @equivalentId = 'a561f5f5-5d6d-4c52-9364-a760cc033ebd'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('swinburn uni', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('swinburn university', @equivalentId)

SET @equivalentId = '0b9230db-bd72-4698-b0e5-02d27d4f44d4'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('university of ballarat', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('ballarat uni', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('ballarat university', @equivalentId)

SET @equivalentId = '72a0050c-cb9e-4d16-a637-271dde911feb'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('university of canberra', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('canberra uni', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('canberra university', @equivalentId)

SET @equivalentId = 'd8b156c1-a70d-4f0e-a52a-9e75a7e854e9'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('une', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('university of new england', @equivalentId)

SET @equivalentId = '73b47e0d-dd82-4226-a3bb-597da88707d3'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('university of newcastle', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('newcastle uni', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('newcastle university', @equivalentId)

SET @equivalentId = '399d5bae-349b-4c07-a31f-75015f0f73c0'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('unda', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('university of notre dame australia', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('university of notredame australia', @equivalentId)

SET @equivalentId = '85aa1232-a0e4-47c7-9af2-9df51103898f'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('unisa', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('iniversity of south australia', @equivalentId)

SET @equivalentId = '16cb6ed4-8c87-4669-b214-2ba201a6a76d'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('usq', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('university of southern queensland', @equivalentId)

SET @equivalentId = 'c20b45bc-a2ae-46a1-87b5-d0be9568dc19'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('university of tasmania', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('uni of tasmania', @equivalentId)

SET @equivalentId = '804df8bd-6096-4cdc-b237-cef2317519aa'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('usc', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('university of the sunshine coast', @equivalentId)

SET @equivalentId = '299dc859-fce7-45f8-9ddc-0451daf5a0ff'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('uow', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('university of wollongong', @equivalentId)

SET @equivalentId = '77abf936-2cdf-4d9b-88b0-febdcd872ba9'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('vu', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('victoria university', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('victoria uni', @equivalentId)

SET @equivalentId = '978acb59-65f3-433f-bd31-c05fde8e1b02'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('goldcoast', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('gold coast', @equivalentId)

SET @equivalentId = '6d4447fc-8f7a-47da-9105-3d101c3c982d'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('sunshinecoast', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('sunshine coast', @equivalentId)

SET @equivalentId = '06155f72-e78b-45f7-8993-945c3fc2e57c'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('tassie', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('tasmania', @equivalentId)

SET @equivalentId = 'e080a397-ceb1-4bfe-a960-9f68e6f6e03e'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('mbs', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('melbourne business school', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('m b s', @equivalentId)

SET @equivalentId = '34fd215c-432b-4a07-90d9-b353d429abd1'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('news ltd', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('newsltd', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('news limited', @equivalentId)

SET @equivalentId = '64beea6a-0953-410f-982d-78274dc6e74d'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('qut', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('queensland university of technology', @equivalentId)

SET @equivalentId = '9471562d-c025-4b80-bb6d-503ac15b329f'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('rmit', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('royal melbourne institute of technology', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('rmit university', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('rmit uni', @equivalentId)

SET @equivalentId = '4f95b636-6879-4c4f-affe-92a95abedb8d'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('inbound', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('in bound', @equivalentId)

SET @equivalentId = '0e30aa6d-46ab-4107-b400-93f9d26ee893'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('adelaide university', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('university of adelaide', @equivalentId)

SET @equivalentId = 'a967d8bd-586b-4790-a1f0-ce49aa1323db'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('anu', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('a n u', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('australian national univeristy', @equivalentId)

SET @equivalentId = 'b1749350-bce0-420f-b667-6d3a65b6fe54'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('unsw', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('university of new south wales', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('u nsw', @equivalentId)

SET @equivalentId = '1335fd5b-b753-47dd-9357-2f2ae136255f'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('uws', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('university of western sydney', @equivalentId)

SET @equivalentId = '4be4338a-5832-4fbb-a775-8a82ee561cfc'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('monash uni', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('monash university', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('monashuni', @equivalentId)

SET @equivalentId = '15209807-7da4-438b-a315-bd9862e4ae4e'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('deakin uni', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('deakin university', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('deakinuni', @equivalentId)

SET @equivalentId = '5664be01-9295-4f00-baae-699fa3454e1a'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('officeworks', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('office works', @equivalentId)

SET @equivalentId = '88a90bab-0b20-4020-9c69-445e35eecc32'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('coles', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('coles group', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('colesmyer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('coles myer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('cml', @equivalentId)

SET @equivalentId = '776f1940-7709-40fe-bb59-1a31d146d4ec'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('wool worths', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('woolworths', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('safeway', @equivalentId)

SET @equivalentId = '0a14ca43-0d10-46a5-ab28-bb5d7297e582'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('mc donalds', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('mcdonalds', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('macdonalds', @equivalentId)

SET @equivalentId = 'c3e1a89d-e3a8-4241-92bf-417faa6b5de9'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('spotless', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('spotless catering', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('spotless group', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('spotless services', @equivalentId)

SET @equivalentId = '1f269cd2-09ef-486b-9e44-54be64c67454'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('ramsayhealth', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('ramsay health', @equivalentId)

SET @equivalentId = '5cd9fb6c-141d-4c26-8bb9-06b1b713c218'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('worley parsons', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('worleyparsons', @equivalentId)

SET @equivalentId = '31454e3a-14ab-4dd2-8590-6a20243d37a9'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('racv', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('royal automobile club of victoria', @equivalentId)

SET @equivalentId = 'eb69e3cf-d99a-4ab4-99fc-6b2432f238be'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('mcc', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('melbourne cricket club', @equivalentId)

SET @equivalentId = 'b3484f6e-d0c2-4074-ab12-45a2d97bf7d0'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('mcg', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('melbourne cricket ground', @equivalentId)

SET @equivalentId = '70f4a884-d46e-4290-aa36-374befa70cc4'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('scg', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('sydney cricket ground', @equivalentId)

SET @equivalentId = '5ac91b5a-f56d-4857-9543-769a1f5fee07'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('cocacola', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('coca cola', @equivalentId)

SET @equivalentId = '021632c7-b74a-44a4-897c-a5942f00e732'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('blue scope', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('bluescope', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('bluescope steel', @equivalentId)

SET @equivalentId = '30ca6cbb-2f40-463c-a97d-bf1a19cef900'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('salvos', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('salvation army', @equivalentId)

SET @equivalentId = '379beb66-7d9f-4eef-a4cd-6183bd024c01'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('gm holden', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('beneral motors holden', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('general motors', @equivalentId)

SET @equivalentId = 'b7ba3d44-469b-4708-b1de-c47200cb621a'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('justjeans', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('just jeans', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('just group', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('justgroup', @equivalentId)

SET @equivalentId = '0ee24ef6-9e6a-4ace-abf1-78b0687d7569'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('pbl', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('publishing and broadcasting', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('publishing & broadcasting', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('publishing&broadcasting', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('pbl ltd', @equivalentId)

SET @equivalentId = 'b2e79b04-2c04-445f-8c66-69a5853b5348'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('macq bank', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('macquarie bank', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('macquarrie bank', @equivalentId)

SET @equivalentId = 'a9d151a3-c7b7-4554-a608-f08395ccd602'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('virgin blue', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('virginblue', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('virginblue.com.au', @equivalentId)

SET @equivalentId = 'e460832c-ec5c-4d7b-8ea4-60120d5296fd'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('mitre10', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('mitre 10', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('mitre ten', @equivalentId)

SET @equivalentId = '44c20e48-5cd6-4404-88d1-fa28c79d8d25'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('starcity', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('star city', @equivalentId)

SET @equivalentId = '6a5e53fa-214e-4326-a6c1-0e2ed20ca777'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('dicksmith', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('dick smith', @equivalentId)

SET @equivalentId = '6427b296-2ed4-4931-985a-74e757da9894'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('seek', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('seek.com.au', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('seek.com', @equivalentId)

SET @equivalentId = '6e22bb30-e925-4869-a74d-cd116f6f60c0'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('yahoo', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('yahoo7', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('yahoo.com.au', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('yahoo7.com.au', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('yahoo 7', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('yahoo7!', @equivalentId)

SET @equivalentId = '46a077cf-f149-41e7-9847-45b3c8bd66e0'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('armaguard', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('arma guard', @equivalentId)

SET @equivalentId = '77ad42f8-085e-49c8-9993-c21e09d1cb82'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('ge', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('general electric', @equivalentId)

SET @equivalentId = 'dbfbdf1e-c157-44d5-a8b2-de0af3899450'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('maanz', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('marketing association of australia and new zealand', @equivalentId)

SET @equivalentId = 'b2a77336-989f-4326-8134-0ef7a8ada6e0'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('mia', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('marketing institute of austrlalia', @equivalentId)

SET @equivalentId = '46539b93-e9e0-4612-9b50-5d133cc047d3'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('aempe', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('association of marine and power engineers', @equivalentId)

SET @equivalentId = '62799344-6e2a-4a89-bda7-e714524374f2'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('vacc', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('victorian automotive chamber of commerce', @equivalentId)

SET @equivalentId = 'c8d96743-1477-4050-9dce-1ff4de504fd4'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('sae', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('society of automotive engineers', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('society of automotive engineers australasia', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('saea', @equivalentId)

SET @equivalentId = 'e267e120-e7ad-4099-b7bd-71e538d99ed6'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('ahri', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('austrlalian human resources institute', @equivalentId)

SET @equivalentId = 'fb497be2-6c7f-487b-9a15-9dd1d4beaf1a'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('rcsa', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('recruitment and consulting services association', @equivalentId)

SET @equivalentId = '75ae9147-220d-4e3f-bd3d-b3f6d653e700'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('gta', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('group training australia', @equivalentId)

SET @equivalentId = '217cb153-d686-4c46-be4e-dda87b40a12f'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('yellowpages', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('yellow pages', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('yellowpages.com.au', @equivalentId)

SET @equivalentId = '5d6da07e-a281-4d56-b3a9-36f596d3ee1e'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('whitepages', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('white pages', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('whitepages.com.au', @equivalentId)

SET @equivalentId = '0a48c18b-0338-4652-8c76-35d3bdf913dd'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Hyperbaric Technician', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Hyperbaric operator', @equivalentId)

SET @equivalentId = '9c1b86ac-5196-44a5-9ab0-885695163f49'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('firearm', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('fire arm', @equivalentId)

SET @equivalentId = '98fe348d-5fea-427c-b105-295a05403106'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('license', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('licence', @equivalentId)

SET @equivalentId = '4a6a9427-79cc-409f-9d67-0a4a8d8ae099'

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

SET @equivalentId = 'd2721f28-68e7-46c7-a006-af8cdc7211d1'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('asfa', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('association of superannuation funds of australia', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('superannuation association', @equivalentId)

SET @equivalentId = 'e3ec43eb-4986-4fd7-b950-0d77bd9a67be'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('retailer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('retail company', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('retail organisation', @equivalentId)

SET @equivalentId = 'cf29595a-11c5-452f-bfb3-ff2cfdf81f63'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('valuer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('valuation', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('property valuer', @equivalentId)

SET @equivalentId = '9e61bdd5-eeb6-4dfb-95ad-d47d53366d16'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('ara', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('australian retailers association', @equivalentId)

SET @equivalentId = 'd6343800-03d8-4595-ae3c-cbc8fda400b7'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('poolman', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('pool man', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('pool cleaner', @equivalentId)

SET @equivalentId = '41d4ac3b-1b80-4657-84a9-2b7359ee3744'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('horticulturalist', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('horti culturalist', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('horticulture', @equivalentId)

SET @equivalentId = 'e27c2ce4-3667-469d-bb15-ef4a10c70491'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('bdouble', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('b double', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('road train', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('roadtrain', @equivalentId)

SET @equivalentId = '15690744-94a7-4d21-8658-59855a549a55'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('crane driver', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('crane operator', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('crane truck driver', @equivalentId)

SET @equivalentId = 'f235ec0d-051f-4765-b624-8c4775f6a20f'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('rigger', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('dogman', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('dog man', @equivalentId)

SET @equivalentId = 'd9dcbbd7-a3fa-47d2-b2ce-ae6ffc4b7d39'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('postman', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('post man', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('postie', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('postal officer', @equivalentId)

SET @equivalentId = 'c52a134a-0e39-4098-82ab-642f345b8e57'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('carpark', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('car park', @equivalentId)

SET @equivalentId = 'c1c8b9a6-cdd8-4927-8c04-cf5f0ca533c3'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('carpark attendant', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('carpark operator', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('car park attendant', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('car park operator', @equivalentId)

SET @equivalentId = 'f239ed1a-00b2-4b61-b3ce-64fc2ee3d3a3'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('formwork', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('form work', @equivalentId)

GO
