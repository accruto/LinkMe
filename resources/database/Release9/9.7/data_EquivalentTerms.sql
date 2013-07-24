DECLARE @equivalentId UNIQUEIDENTIFIER

DELETE FROM dbo.EquivalentTerms

SET @equivalentId = 'ea04ae0a-6896-4982-af1f-3a978d39644d'

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

SET @equivalentId = '8d7666e5-7ddc-4279-ba2e-f6219fd65498'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Executive Chairman', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Exec Chairman', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('E.C.', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('EC', @equivalentId)

SET @equivalentId = '99816a58-42c9-4b11-bcf5-0828e4b9d746'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('General Manager', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('GM', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('G.M.', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Group General Manager', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('GGM', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('G.G.M.', @equivalentId)

SET @equivalentId = 'e33e1f5b-70be-4414-aaba-83fe894eeabf'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Chief Operating Officer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('COO', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('C.O.O.', @equivalentId)

SET @equivalentId = '9c24e85d-8943-47c8-aac7-9f3b2cb4518d'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('vp', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('v.p.', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('vice president', @equivalentId)

SET @equivalentId = 'eaaaae95-1233-4a5a-8df4-156f1c18683d'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('senior vp', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('senior v.p.', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Senior Vice President', @equivalentId)

SET @equivalentId = '8eadc688-e78b-40fa-bd0d-65754362ff9e'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('2ic', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('2.i.c.', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('2i.c.', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Second in charge', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('2 IC', @equivalentId)

SET @equivalentId = '26e0d377-d874-4028-b8bb-684df1fbe913'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Company Director', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Dir', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Dir.', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('director', @equivalentId)

SET @equivalentId = 'de8efb91-978e-4c86-8588-7fe5beb74bd7'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('non executive director', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('non exec director', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('non-exec director', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('non exec dir.', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('non-exec dir.', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('ned', @equivalentId)

SET @equivalentId = '21dcd3fd-4a59-4b8d-9763-263ca2139a8b'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('company secretary', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('company sec.', @equivalentId)

SET @equivalentId = '3b68f717-60b9-4748-aafd-338e8cd61111'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Chairman', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('chairperson', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('chairwoman', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('chair man', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('chair person', @equivalentId)

SET @equivalentId = '05612974-7bf2-4fec-8dcb-43e7f8a77799'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('CIO', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('C.I.O.', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Chief Information Officer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('general manager it', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('it director', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('director it', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('director information technology', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('head of it', @equivalentId)

SET @equivalentId = 'd8015c63-edac-4af6-94bb-963032ac2347'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('manager', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('mgr', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('manger', @equivalentId)

SET @equivalentId = '20c8bd8e-451b-44c1-b5e9-2f06c5d183f0'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('State Manager', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('statemanager', @equivalentId)

SET @equivalentId = '80024bdc-78e9-487b-a5ee-763197c5a131'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Manager Governance', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Manager Accountability', @equivalentId)

SET @equivalentId = '63776dd2-4536-4cda-9b6b-0089aae412a6'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Program Director', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Director program', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('program manager', @equivalentId)

SET @equivalentId = '19dea3e5-e8ca-42f3-b4a9-b0049681a7ab'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Assistant Manager', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('asst mgr', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('asst manager', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Assistant Manger', @equivalentId)

SET @equivalentId = 'e599534b-4397-441d-8765-21b29b8950a8'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Senior Supervisor', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Snr Surpervisor', @equivalentId)

SET @equivalentId = '973b1cfe-aa63-45e8-8fc3-3235b43dbd6c'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Assistant Supervisor', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Asst Supervisor', @equivalentId)

SET @equivalentId = '4f2faf06-f9b3-4205-a1c7-03bcc09ee8c4'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Team Leader', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Team lead', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Teamleader', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('supervisor', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('foreman', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('fore man', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('foreperson', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('fore person', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('team manager', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('teamlead', @equivalentId)

SET @equivalentId = 'db1c3c90-7222-47a6-a1f0-8ff60e53ca53'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Project Manager', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Project Mgr', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('PM', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Project Manger', @equivalentId)

SET @equivalentId = '577e980a-3537-4e2d-8f99-249e0a981ea7'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('General Manager Project', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Project Director', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('project coordinator', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Senior Project Manager', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Senior Project director', @equivalentId)

SET @equivalentId = 'ecc2cd08-2e43-4e07-bc91-3ecc51a3cf24'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('BPM', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('B.P.M.', @equivalentId)

SET @equivalentId = '7bb5d4ec-0f25-403a-8b22-6f73fa7dc224'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Assistant Project Manager', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Asst Project Manager', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Asst Project Mgr', @equivalentId)

SET @equivalentId = 'f8bae1dc-e940-4294-9ab4-a0f228d2a632'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Junior Project Manager', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Trainee Project Manager', @equivalentId)

SET @equivalentId = 'c06a13a6-c853-4f07-96b9-e01cf107ecbc'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('inhouse consultant', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('in-house consultant', @equivalentId)

SET @equivalentId = '74054e89-211c-4f66-acfc-cf846d67aee7'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('BPR consultant', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('business process re-engineering', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('bpr', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('business process reengineering', @equivalentId)

SET @equivalentId = '31d45d8c-c301-474e-8120-c764e0c49dc7'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('DM', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('D.M.', @equivalentId)

SET @equivalentId = '12095bce-fb36-4216-9779-c129eb887ab3'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Manager Planning', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Manager- Planning', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('planning manager', @equivalentId)

SET @equivalentId = 'c12bfc35-f274-48b9-bcae-0dca6fd4ea8a'

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

SET @equivalentId = '7e5f8c06-2262-4a5e-bcc5-e56793504251'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Assistant C.F.O.', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Assistant CFO', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Asst C.F.O.', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Asst CFO', @equivalentId)

SET @equivalentId = '410ddb79-53fa-4a77-9bcf-bd1ecc1b7bb2'

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

SET @equivalentId = '9a285a5b-409b-4eca-8962-c29f65897b60'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Senior Financial Controller', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Sr. Financial Controller', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Sr Fin Controller', @equivalentId)

SET @equivalentId = 'd894cc76-0ff4-4504-88a1-21f06b2dee10'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Assistant Financial Controller', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Asst Financial Controller', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Asst Fin Controller', @equivalentId)

SET @equivalentId = 'a63b1db2-8d31-4d4b-9d93-7fa3e8298a3c'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Regional Financial Controller', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Regional Finance Controller', @equivalentId)

SET @equivalentId = '0599363f-67ca-4f31-b02f-5089efbed82a'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('National Financial Controller', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('National Finance Controller', @equivalentId)

SET @equivalentId = '31931cf1-688c-4971-a92e-26820fdfa8e4'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Accounting Manager', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Accounts Mgr', @equivalentId)

SET @equivalentId = '758fe71c-fc16-400e-8cd2-6f5cb6a20871'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Senior Finance Accountant', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Sr Finance Accountant', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Sr. Finance Accountant', @equivalentId)

SET @equivalentId = 'ddc4d305-13e5-44e3-92cc-7c9c22393213'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Senior Financial Accountant', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Sr Financial Accountant', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Sr. Financial Accountant', @equivalentId)

SET @equivalentId = '91083fa4-823d-4a82-b078-bb8e9af0d80e'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Administration Manager', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Admin Manager', @equivalentId)

SET @equivalentId = '2c90ac40-46cd-4635-94eb-1058e5721043'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('National Administration Manager', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Assistant Administration Manager', @equivalentId)

SET @equivalentId = 'd4852e65-f770-419c-bcf2-431ae5ee7dc1'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Senior Consultant', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Sr Consultant', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Sr. Consultant', @equivalentId)

SET @equivalentId = 'fc6cb32a-2b2e-4886-8709-c5c735e13419'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Principal Consultant', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Chief Consultant', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Managing Consultant', @equivalentId)

SET @equivalentId = '7028b360-d013-4eb4-b3ae-30b513e4afd6'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Accountant', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Acc', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Accountan', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('acct', @equivalentId)

SET @equivalentId = '6456926f-1232-4300-a8f8-4218811ab128'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Financial accountant', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Fin Acc', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Finance Accountant', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Finance Acc', @equivalentId)

SET @equivalentId = '1e88f4a0-e11b-4383-97b9-8337f50e9120'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Corporate Accountant', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Corp Accountant', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Sr. Mg Accountant', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Contract Accountant', @equivalentId)

SET @equivalentId = '9d05a12b-7ffd-4c7d-a8bd-0052831ee40c'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Business Accountant', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Bus Acct', @equivalentId)

SET @equivalentId = '8fe2a294-272e-47ac-a690-35b24ba237e1'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Management accountant', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Management Acct', @equivalentId)

SET @equivalentId = '28acfedd-9963-4ab2-be58-099f69122772'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Senior Management Accountant', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Sr Management Accountant', @equivalentId)

SET @equivalentId = '91fde280-22af-4766-b2e3-90771aeb189c'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Accounting', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Accountin', @equivalentId)

SET @equivalentId = '7314c3f0-a384-40f1-b3a0-329b8a16ef1f'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('cpa', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('c.p.a.', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('certified practising accountant', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('certified accountant', @equivalentId)

SET @equivalentId = 'c7637057-8443-473f-ab0e-46a4cbc2e74d'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('ca', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('c.a.', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('chartered accountant', @equivalentId)

SET @equivalentId = '5bb418b2-30b6-4b19-8aae-cf96a596092a'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Temporary Accountant', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Temp Accountant', @equivalentId)

SET @equivalentId = 'd0fbcf40-7a7a-4384-8002-f30d83018eee'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Sr. Accountant', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Sr Accountant', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Senior Accountant', @equivalentId)

SET @equivalentId = '932780fa-0529-40da-be26-656f6a00a0ac'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Financial/Management Accountant', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Assistant Management Accountant', @equivalentId)

SET @equivalentId = '2db93804-cb96-4289-906a-0cf47253bc4f'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Junior Accountant', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Jr Accountant', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Jr. Accounts', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Junior Accounts Clerk', @equivalentId)

SET @equivalentId = 'b807e7f9-ef32-4cf0-a338-5856ba1c8a5c'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Assistant Accountant', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Asst Accountant', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Accounts Assistant', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Acct Asst', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Accounting Assistant', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Account Assistant', @equivalentId)

SET @equivalentId = 'd9e67836-0b68-4e57-b3dd-a915c4876466'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Accounts Clerk', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Accounts Officer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Accounts Staff', @equivalentId)

SET @equivalentId = '4ccb01db-40b6-40a1-8a95-e73d0d59b4a4'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Project Accountant', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Proj Acct', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Project Management Accountant', @equivalentId)

SET @equivalentId = '2ebaa9c8-6f96-4b8e-9c7c-801ae7968e1f'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Auditor', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('auditing', @equivalentId)

SET @equivalentId = '351199c2-49cc-41fe-8c3f-13eb83e651f8'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Audit Manager', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Audit Mgr', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('head of audit', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('audit director', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('audit partner', @equivalentId)

SET @equivalentId = '43f1148f-2139-4f9a-a036-d2a446b010a2'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Senior Auditor', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Sr Auditor', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Sr. Auditor', @equivalentId)

SET @equivalentId = '961ec8c1-6478-4be7-bdf7-080d77a69363'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Internal Auditor', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('In house Auditor', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('In-house Auditor', @equivalentId)

SET @equivalentId = '22373d99-4457-4169-b6a0-6e3e057f07c4'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('External auditor', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Contract Auditor', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('financial auditor', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Revenue Auditor', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('State Auditor', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Statutory Accountant', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Senior Internal Auditor', @equivalentId)

SET @equivalentId = 'e6d6fda2-82d0-4da8-b433-0862423b7c80'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Junior Auditor', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Jr Auditor', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Jr. Auditor', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Apprentice Auditor', @equivalentId)

SET @equivalentId = '824f4486-0001-4ccc-88c8-80fe2eb9aaf8'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Quality Auditor', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('QA Auditor', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Quality Assurance Auditor', @equivalentId)

SET @equivalentId = 'cce0e523-9582-4203-bf88-e8addb2d5b6b'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Operations Auditor', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Process Auditor', @equivalentId)

SET @equivalentId = 'adc6eb89-c09e-4c95-b589-41af9e7e011c'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Stock Auditor', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Night Auditor', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('stock counter', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('stocktake auditor', @equivalentId)

SET @equivalentId = 'a55aceff-f5ee-400d-b668-a30b1b305657'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('stock controller', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('inventory controller', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('stockcontroller', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('stock supervisor', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('stock control', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('stock manager', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('inventory manager', @equivalentId)

SET @equivalentId = '4554dfc5-fb63-4f96-9122-bba6d5cae35c'

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

SET @equivalentId = '00f37891-630b-405d-9d88-29a37c74a16d'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Accounts Payable Manager', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Accounts Payable Supervisor', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('head of accounts payable', @equivalentId)

SET @equivalentId = '85fec85e-4a02-4dda-985d-adec9d1a3bf2'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Accounts Payable', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Accts Payable', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('AP', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Sales Ledger', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Accounts Payable Officer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Accounts Payable Clerk', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Accounts Payable Coordinator', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Accounts Payable Co-ordinator', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Accounts Payable Co ordinator', @equivalentId)

SET @equivalentId = 'ebf660df-b6c8-4d6e-9c5b-f4e8565ef358'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Accounts Payable Assistant', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Assistant Accounts Payable', @equivalentId)

SET @equivalentId = '205b84c7-4469-4dc4-91c2-68e16a855a5f'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Accounts Payable/Receivable', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Accounts payable specialist', @equivalentId)

SET @equivalentId = '400efe7b-dcaa-4527-a77a-ac8627d7e8ae'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Reconciliation Clerk', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Reconciliation Officer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Reconciliation Assistant', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Reconciliation Asst', @equivalentId)

SET @equivalentId = '12caa374-da96-416c-8bee-17a44b369d89'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Accounts Receivable Manager', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Accounts Receivable Supervisor', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Collections Supervisor', @equivalentId)

SET @equivalentId = 'f197e958-d71e-4f6b-af7e-7dd0c137d61c'

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

SET @equivalentId = '16904f93-6ddb-4cf4-914d-47ad80de5926'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Mgr Credit Control', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Credit Control Manager', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Credit Control Mgr', @equivalentId)

SET @equivalentId = 'b3f3ff34-8f4a-4900-9b99-5a687f557bd5'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Senior Credit Controller', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Sr Credit Controller', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Sr. Credit Controller', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Manager Credit Control', @equivalentId)

SET @equivalentId = '4cb0d916-7fce-4f66-98fb-9f37fc1600ef'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Junior Credit Controller', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Jr Credit Controller', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Jr. Credit Controller', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Jnr Credit Officer', @equivalentId)

SET @equivalentId = '5e3b05b7-e40f-4bdf-8cff-592544605406'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Finance Analyst', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Financial Analyst', @equivalentId)

SET @equivalentId = 'fca0fe2f-bc07-426a-bdd0-be622f33e4fd'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Credit Analyst', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Credit Risk Analyst', @equivalentId)

SET @equivalentId = '5f9f4890-b9d7-4754-b9e9-af9e94d5635e'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Senior Finance Analyst', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Sr Finance Analyst', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Sr. Finance Analyst', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Senior Financial Analyst', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Sr Financial Analyst', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Sr. Financial Analyst', @equivalentId)

SET @equivalentId = 'ccdfad3c-ba54-4d41-b5a1-2bf1fa1646ec'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Senior Credit Analyst', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Sr Credit Analyst', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Sr. Credit Analyst', @equivalentId)

SET @equivalentId = '6c98e49e-3213-47b9-a5d4-5cba31330c10'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Junior Finance Analyst', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Jr Finance Analyst', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Jr. Finance Analyst', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Junior Financial Analyst', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Jr Financial Analyst', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Jr. Financial Analyst', @equivalentId)

SET @equivalentId = 'bcb4f057-f49b-45a0-9f71-400b7e2a1652'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Junior Credit Analyst', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Jr Credit Analyst', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Jr. Credit Analyst', @equivalentId)

SET @equivalentId = '767d2fde-1ec3-4417-bf6f-c39b606821a4'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Taxation Manager', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Taxation Mgr', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Tax Manager', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Tax Mgr', @equivalentId)

SET @equivalentId = '0a93e635-62c3-424a-846a-ffcd4f043c07'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('taxatiojn accountant', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Taxation Accountant', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('tax associate', @equivalentId)

SET @equivalentId = '71632392-fb50-46f4-9b1e-550f29515538'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('tax accountant', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('tax analyst', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('tax partner', @equivalentId)

SET @equivalentId = '761bf91f-d6a0-470f-9e11-19475b513e10'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Junior Tax Accountant', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Jr. Tax Accountant', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Jr Tax Accountant', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Junior Taxation Accountant', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Jr. Taxation Accountant', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Jr Taxation Accountant', @equivalentId)

SET @equivalentId = '0fdf02f4-b627-427c-b0de-3ab83b9a17c7'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Taxation Advisor', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Tax Advisor', @equivalentId)

SET @equivalentId = 'a319f5d4-71ec-4e08-8088-efdeab8c88ec'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Taxation analyst', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Tax Return Preparer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Tax Agent', @equivalentId)

SET @equivalentId = 'd6c53ecb-e573-464a-b9b1-abbf2ff7c3b3'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Senior Tax Accountant', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Sr. Tax Accountant', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Sr Tax Accountant', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Senior Taxation Accountant', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Sr. Taxation Accountant', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Sr Taxation Accountant', @equivalentId)

SET @equivalentId = '30c71330-6f3f-41d9-aa88-c75e2f35ffb9'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Motor Claims', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Motor Claims Consultant', @equivalentId)

SET @equivalentId = 'd9ce0e51-b061-4f48-ae82-7cd968dc1eae'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Injury Claims Consultant', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Workcover Claims', @equivalentId)

SET @equivalentId = '840ba089-2fae-45f6-af60-8ee7344565b4'

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

SET @equivalentId = '64b09fd1-413e-4c10-b3e6-8b7a464be612'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Senior Purchasing Specialist', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Sr. Purchasing Specialist', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Sr Purchasing Specialist', @equivalentId)

SET @equivalentId = '0ce788d4-2a5c-4ffb-97dc-8ba9551da3b0'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Purchasing Manager', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Purchasing Mgr', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('procurement manager', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('head of procurement', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('procurement director', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('buying manager', @equivalentId)

SET @equivalentId = '618cc77e-8280-447c-a61a-f96e54da70cd'

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

SET @equivalentId = '376272fb-db1a-49e6-a363-a3cb2b60dcb2'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Buyer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Senior  Buyer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Sr Buyer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Sr. Buyer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Regional Buyer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('National Buyer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('State Buyer', @equivalentId)

SET @equivalentId = '38864730-f800-46b3-a05e-a7a98af65838'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Buying Assistant', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Assistant Buyer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Junior Buyer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Jr Buyer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Jr. Buyer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Trainee Buyer', @equivalentId)

SET @equivalentId = '7f94dbc0-6968-4bb7-9715-4abf7cc58303'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Mortgage Collections', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Mortgage Collection', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Mortgage Collections Officer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Mortgage Collections Clerk', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Mortgage Collection Clerk', @equivalentId)

SET @equivalentId = '01b9652b-0567-4459-ac87-d06d3bf027bc'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Banking Manager', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Bank Manager', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('bank branch manager', @equivalentId)

SET @equivalentId = '726e3029-c71e-49a2-ad96-6959985c969f'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Branch Manager', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('branch head', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('branchmanager', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('branch supervisor', @equivalentId)

SET @equivalentId = 'af6e4679-03d0-49c3-a974-1ed3f64d8992'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Business Banking Manager', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Manager Business Banking', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('banking services manager', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('business banker', @equivalentId)

SET @equivalentId = '1ee3e119-593d-4373-a753-d6dd7c84a048'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Banking Clerk', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Banking Consultant', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Banking Officer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('bank clerk', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('teller', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('bank teller', @equivalentId)

SET @equivalentId = '59125755-31dd-4530-ba3d-d0458e0336db'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Insurance Manager', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Insurance Mgr', @equivalentId)

SET @equivalentId = '4907669d-f13c-4546-aba6-78dd4c7bd678'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Insurance Consultant', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Insurance Assistant', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Insurance Asst', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Insurance Clerk', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Senior Insurance Administrator', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Sr. Insurance Administrator', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Sr Insurance Administrator', @equivalentId)

SET @equivalentId = 'a2c38f7a-150a-4703-8269-1d0be7ef8ccf'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Jr. Insurance Administrator', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Jr Insurance Administrator', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Junior Insurance Administrator', @equivalentId)

SET @equivalentId = 'e91eb05e-2227-438f-8ef6-23a29f053ef3'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Insurance Broker', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('General Insurance Broker', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Insurance Solutions Consultant', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Senior Insurance Broker', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Sr. Insurance Broker', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Sr Insurance Broker', @equivalentId)

SET @equivalentId = '6f625cf1-65ff-4d95-883b-083b215f0ad8'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Junior Insurance Broker', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Jr. Insurance Broker', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Jr Insurance Broker', @equivalentId)

SET @equivalentId = '64114f95-9e61-4690-9af3-f216dd433fe0'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Chief Actuary', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('actuarial manager', @equivalentId)

SET @equivalentId = 'ece3638c-af3d-4449-b601-f8033421bd61'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Workers Compensation Officer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Workers Compensation Administrator', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('WORKERS COMPENSATION CASE MANAGER', @equivalentId)

SET @equivalentId = '9e5a33a0-3632-462e-972b-062c6ae77b85'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Underwriting Manager', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('National Underwriting Manager', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Underwriting', @equivalentId)

SET @equivalentId = 'a0e5027a-4d34-464e-9af4-17320e8288b1'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Underwriting Executive', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Insurance Underwriter', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Assistant Underwriter', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('under writer', @equivalentId)

SET @equivalentId = '0cc14e7b-4e2d-4470-8e56-591974173776'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Chief Financial Planner', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('head of financial planning', @equivalentId)

SET @equivalentId = '41eacc99-7abc-4cb3-8d0f-46a589bbd1a9'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Financial planner', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Financial Planner Advisor', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('certified financial planner', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Financial Services Consultant', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Personal Financial Advisor', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('CFP', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('financial advisor', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('c f p', @equivalentId)

SET @equivalentId = 'f8c745b9-f0fd-4c4e-83eb-90846a07ead2'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Assistant Financial Planner', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Financial Planner Assistant', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Financial Planning Assistant', @equivalentId)

SET @equivalentId = '8be91d48-276c-45d6-a948-db08a43fe529'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Senior financial planner', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Sr. financial planner', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Sr financial planner', @equivalentId)

SET @equivalentId = '0350dbab-c101-4413-a0a3-2723e472d930'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Paraplanner', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Para planner', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Para-planner', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Para Planning', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Para-Planning', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Para-Financial Planner', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Para Financial Planner', @equivalentId)

SET @equivalentId = 'b740791b-e6d3-424b-a827-768904a08a1e'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Superannuation', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Super annuation', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Super-annuation', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('super fund', @equivalentId)

SET @equivalentId = '8fbd6250-57c0-4570-b6d2-c2fda27c5678'

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

SET @equivalentId = 'cca80921-8757-4b95-a62b-7d4676183a90'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('recruitment', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('onboarding', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('on boarding', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('on-boarding', @equivalentId)

SET @equivalentId = '233b1b2c-064a-4e9d-8d66-0613259eb5f7'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Recruitment Manager', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Onsite Recruitment Manager', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('National Recruitment Solutions Manager', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Regional Recruitment Manager', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('recruitment director', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('head of recruitment', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('recruitment dierctor', @equivalentId)

SET @equivalentId = '97724203-3cad-49f1-b3dd-5bb09b196261'

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

SET @equivalentId = 'ea8a298e-e10c-4b0a-be99-2d2016a5102a'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Recruitment Administrator', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Recruitment Administration Officer', @equivalentId)

SET @equivalentId = 'c7a87056-07db-40d6-9525-ff04fef67edf'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('HR', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('H.R.', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Human Resources', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Human Resource', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('people and culture', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('human capital', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('personnel', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('h r', @equivalentId)

SET @equivalentId = 'bf688a9c-eb9f-4e38-8639-ce97faa65114'

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
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('hrd', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('director human resources', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('hr director', @equivalentId)

SET @equivalentId = '532c9315-eb6e-4d0d-ac68-28f08bd16640'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Senior HR Advisor', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Senior Human Resources Advisor', @equivalentId)

SET @equivalentId = 'c9d77b20-69c4-4e4c-b474-52e518b3f53b'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Senior Human Resources Consultant', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Senior Human Resources Officer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Senior HR Consultant', @equivalentId)

SET @equivalentId = 'e275259a-bcb7-49b3-a879-f8345f3d7606'

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

SET @equivalentId = 'e85834b9-b83c-4c3a-a00c-473e9bba96ed'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Human Resources Analyst', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('HR ANALYST', @equivalentId)

SET @equivalentId = 'f9f1ac62-083f-4d15-8fe3-2d86a251ef32'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('HR Executive', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Human Resources Executive', @equivalentId)

SET @equivalentId = '63a30bec-0bd9-44e0-ac5a-4222a5da63f7'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('HR Payroll', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('HR/Payroll', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('H.R. Payroll', @equivalentId)

SET @equivalentId = '068856cd-97f5-410d-bb60-9c1cc7ab9286'

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

SET @equivalentId = 'dae4fcc2-4c2b-47c9-ba40-0d1b4e529bf6'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Temporary Payroll Officer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Temp Payroll', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Payroll Temp', @equivalentId)

SET @equivalentId = 'f5f851a2-db79-4950-81c8-bcaeeda66378'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Senior Payroll Officer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Payroll Supervisor', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('payroll manager', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('head of payroll', @equivalentId)

SET @equivalentId = '1d0070cf-c459-4618-822e-949c4efd57a0'

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

SET @equivalentId = '0801bbc6-2f88-4a92-add4-59759d11e76d'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Retail Supervisor', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Retail Team Leader', @equivalentId)

SET @equivalentId = 'f390add7-a152-455b-a869-05dd185c2f08'

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

SET @equivalentId = '0d8bd4ee-448b-4774-9ca8-1d5b4ef3e7f9'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('LinkMe', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('owner of this intellectual property', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('link me', @equivalentId)

SET @equivalentId = '6c89bb97-e149-4bee-9754-2bdcc9d0e5b6'

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

SET @equivalentId = '29be2cd6-83fb-43ac-80db-7e83d8323ba8'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Restaurant manager', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Restaurant General Manager', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Bistro manager', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Restaurant Supervisor', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('restaurant duty manager', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('restaurant and bar manager', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('restaurant owner', @equivalentId)

SET @equivalentId = 'd9ef25f1-3ab4-4482-8f71-1eabfef279a9'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('duty free', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('dutyfree', @equivalentId)

SET @equivalentId = 'bd2c0e24-f829-40e7-8154-7e876d75748b'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Floor Manager', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Floor Supervisor', @equivalentId)

SET @equivalentId = 'f68092d4-bcce-434f-ab78-920913cd7fbb'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Bar Manager', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Bar Supervisor', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('bar owner', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('head barman', @equivalentId)

SET @equivalentId = 'c69c7fcf-1a94-4999-93de-9ccce97016ce'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('food and beverage', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('foodandbeverage', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('food & beverage', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('food&beverage', @equivalentId)

SET @equivalentId = '6590b9fc-8ef4-4dc7-b1a8-bc72d3066f7b'

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

SET @equivalentId = '01856b2c-3850-4818-90b9-38d9bd08cc45'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('barrista', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('barista', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('coffee maker', @equivalentId)

SET @equivalentId = '006deee7-7d9a-48ef-8bf7-b243b4a3b373'

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

SET @equivalentId = '2770ec26-86df-4aa0-af13-5821a30ffc70'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Gaming Manager', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Gaming Mgr', @equivalentId)

SET @equivalentId = 'b802731f-4da4-4899-ba44-8ca1a9f4fee1'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Gaming Attendant', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Gaming staff', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Bar/Gaming Attendant', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Gaming Bar Attendant', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Gaming/Bar Attendant', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('gaming machine attendant', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('room attendant', @equivalentId)

SET @equivalentId = 'dac9fe73-52b8-4b15-8bef-f22094f6caa4'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Head Waiter', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Head Waitress', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Wait Supervisor', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Maitre De', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Senior Waiter', @equivalentId)

SET @equivalentId = 'a55eb9c8-0ddc-4c6a-adc1-3c8b285a328f'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Waiter', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Waitress', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Wait Staff', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Waitress Customer service', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Waitress/ Kitchen Hand', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Waitress/kitchenhand', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Waitress / Kitchen Hand', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Waiter/kitchen hand', @equivalentId)

SET @equivalentId = 'cbf0bfdd-3047-459b-926c-a1cc656ee6ab'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Kitchen Manager', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Head Cook', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Head Chef', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Chef Manager', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Head Pastry Chef', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Senior Chef', @equivalentId)

SET @equivalentId = 'e464bfaf-cc33-422a-8eba-20dd3759260f'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Second Chef', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('2nd Chef', @equivalentId)

SET @equivalentId = '4fcfde6c-e119-4d3d-a28c-f4952178967f'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Chef De Partie', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Sous Chef', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Executive Chef', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Commis Chef', @equivalentId)

SET @equivalentId = '6fbae95a-6a7a-4f9f-b5a7-3b15502f17e9'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Assistant Chef', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Chefs Assistant', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Chef assistant', @equivalentId)

SET @equivalentId = 'f5aeaaa2-0c62-43f7-ae1a-524ae74897c5'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('chef', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('che', @equivalentId)

SET @equivalentId = 'babae7c6-4d72-49b5-b473-7a180ddbdfb5'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Executive Pastry Chef', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Pastry Chef', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('pastry cook', @equivalentId)

SET @equivalentId = 'd791fc9e-dabc-473c-b9b7-d6b0d431f30b'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Apprentice Chef', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('2nd year Apprentice Chef', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('1st Year Apprentice Chef', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('4th Year Apprentice Chef', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Trainee chef', @equivalentId)

SET @equivalentId = '065662f7-fec4-45e2-b6b9-551ad45b66e8'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Cook/Chef', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Relief Chef', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Kitchen Hand/Cook', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Cook/Kitchen Hand', @equivalentId)

SET @equivalentId = '004eb9c8-42fd-4f44-8e15-3b74d8049b8f'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('kitchen hand', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('kitchenhand', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Kitchen-hand', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Kitchen Worker', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Kitchen Hand/Waiter', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Kitchen hand/waitress', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('casual kitchen hand', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Kitchen hand Customer service', @equivalentId)

SET @equivalentId = '61e12390-2f1d-4a0b-b869-b6dc53436d7b'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Call Centre Manager', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Call Centre Team Manager', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Call Centre Supervisor', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Telemarketing Manager', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Telemarketing Team Leader', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('contact centre manager', @equivalentId)

SET @equivalentId = '07671d12-5225-413e-b1aa-9b8ea59c78b5'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Customer Service Manager', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Customer Services Manager', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Customer Service Supervisor', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Customer Service Team Leader', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('customer care manager', @equivalentId)

SET @equivalentId = '72a93f19-76e9-40ff-8d0d-14bf1644bdee'

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

SET @equivalentId = '89f06e93-d42f-4532-8ef7-5d422c4fcf6e'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('customer service', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('customer care', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('client service', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('client care', @equivalentId)

SET @equivalentId = 'ec2c3308-1fa4-4339-a4e5-828db7a5977d'

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

SET @equivalentId = '1e514b43-a820-4f68-842c-f974342b2676'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Junior Receptionist', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Jr Receptionist', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Trainee Receptionist', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Assistant Receptionist', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Reception Assistant', @equivalentId)

SET @equivalentId = '53530156-b306-4398-bfd9-3f82453ec683'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Junior Secretary', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Jr Secretary', @equivalentId)

SET @equivalentId = 'd4413fc7-f3e1-4989-b958-9f8c00c19a63'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Front Office Receptionist', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Receptionist Front Office', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Front Desk Receptionist', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Frontline Receptionist/Office All-rounder', @equivalentId)

SET @equivalentId = '704e1acc-563a-478d-99c1-24770bb6ec75'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Front Office Manager', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Front Office Supervisor', @equivalentId)

SET @equivalentId = '3dec0046-6a38-48c1-bad2-2d8161227bd5'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Office manager', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Office Supervisor', @equivalentId)

SET @equivalentId = 'bbf024ef-a005-4eec-9d3d-0ac3eaef5b8f'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Office Assistant', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Office Asst', @equivalentId)

SET @equivalentId = '4e05f53c-ebe9-4e66-8943-172d2fe800a7'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Bilingual Receptionist', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('bi lingual receptionist', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('bi-lingual receptionist', @equivalentId)

SET @equivalentId = '8f1a4ab5-5679-4456-ab80-8f440c08eabe'

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

SET @equivalentId = '5d59e1e9-9f01-41c1-97ca-fbbf3d8b6af6'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Receptionist/Property Management', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Receptionist / Assistant Property Manager', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('property manager', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('facility manager', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('facilities manager', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('strata manager', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('portfolio property manager', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('rental manager', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('realestate manager', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('real estate manager', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('asset manager', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('caretaker', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('care taker', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('caretaker/manager', @equivalentId)

SET @equivalentId = '4d650318-9e99-4632-a125-1dd7b22ea343'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Temporary Receptionist/Secretary', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Temporary Receptionist', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Casual Receptionist', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Relief Receptionist', @equivalentId)

SET @equivalentId = '253a4eb7-a698-4bf0-b074-d0a5765de07d'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Office Manager/Personal Assistant', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Office Manager/PA', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Office Manager / Executive Assistant', @equivalentId)

SET @equivalentId = 'd28a9e26-7bcc-4948-8a43-e7dadc95023f'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Administration Supervisor', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Admin Supervisor', @equivalentId)

SET @equivalentId = '1e91edc8-d264-42a1-9ba9-27e8e64875e9'

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

SET @equivalentId = '29d882d9-f4aa-4ac1-ae5f-f0efa6d3d8bb'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('admin', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('administration', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('administrative', @equivalentId)

SET @equivalentId = '251b8f0e-ac6b-4fb7-8a3a-8f59c0a600d1'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Administrative Support Officer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Administrative Support', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Administration Support', @equivalentId)

SET @equivalentId = '5641d5a4-0162-4849-a614-f58a0d9f4a2c'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Temporary Clerical Assistant', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Temporary Administrative Officer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Relieving Administrative Officer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Temporary Admin Officer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Temp Admin Officer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Temp Administration Officer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('temporary administration assistant', @equivalentId)

SET @equivalentId = '96af95d7-6644-4e25-8426-ac4de2903f47'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Junior Clerical Assistant', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Clerical Trainee', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Clerical Traineeship', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Trainee Clerical Assistant', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Office Junior', @equivalentId)

SET @equivalentId = 'c3f0776a-db0d-4fde-b8d4-9bfaee604eb7'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Admin Trainee', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Office Administration Trainee', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Junior office administration', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Administration Trainee', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Junior Administration Assistant', @equivalentId)

SET @equivalentId = '04479f91-9344-433f-bddf-cc27e7e8d717'

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

SET @equivalentId = 'afdad2db-baba-4f73-a086-66f74d9b27af'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('engineer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('eng', @equivalentId)

SET @equivalentId = 'd4decb2f-b31c-456f-a142-5bbe92dd21d4'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Engineering Manager', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Senior Engineer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Chief Engineer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('director of engineering', @equivalentId)

SET @equivalentId = 'e6d7091b-c61f-4105-b8d2-f15a8a4899b1'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('engineering project manager', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Senior Project Engineer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Project Engineer', @equivalentId)

SET @equivalentId = '1d0520b5-622d-4e10-bc62-8c137e9de52b'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Site Engineer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Site Supervisor', @equivalentId)

SET @equivalentId = '7bd393c8-a810-4582-8189-3bad2a6236b5'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Assistant Engineer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Engineering Assistant', @equivalentId)

SET @equivalentId = '0f665183-87f4-4021-aa99-d86b11543564'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Junior Engineer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Student Engineer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('graduate engineer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Undergraduate Engineer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('DESG Student Engineer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('trainee engineer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('engineering trainee', @equivalentId)

SET @equivalentId = '6b11065f-2831-4e15-9346-49129621a5cf'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Field Engineer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('FIELD SERVICE ENGINEER', @equivalentId)

SET @equivalentId = '10cae09f-6770-46a0-8598-cd288d6f84df'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Customer Engineer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('customer service engineer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('service engineer', @equivalentId)

SET @equivalentId = '6b4646fe-1fc3-41bf-94f6-67cbc51b07b5'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Engineering Consultant', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Consultant engineer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Consulting Engineer', @equivalentId)

SET @equivalentId = '9343626a-dcbf-4bd5-8e83-64fe27a1a8ca'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Customer Support Engineer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Support Engineer', @equivalentId)

SET @equivalentId = 'dd2a684c-06da-47cf-af1f-a081f5518327'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Aircraft Engineer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Aircraft Mechanic', @equivalentId)

SET @equivalentId = '5d0d1b93-d00a-42e9-9af8-2d2d4d9d7ae1'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Design Engineer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('engineering designer', @equivalentId)

SET @equivalentId = 'a8552e01-4eb6-4706-bf21-da8ec90118c6'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Electrical Design Engineer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('electrical engineer', @equivalentId)

SET @equivalentId = 'dbc073a6-7685-4e1c-9ce6-1f4b19412e20'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Mechanical Design Engineer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Mechanical Designer', @equivalentId)

SET @equivalentId = '923b2b83-1cfe-451c-ae72-e9b31a3cfa01'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Planning Engineer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('project planning engineer', @equivalentId)

SET @equivalentId = 'd79d919e-484e-474c-9fa9-4d282c37d209'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Electronics Engineer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Electronic Engineer', @equivalentId)

SET @equivalentId = '77deeada-3e1d-472d-920b-6115e9c1064d'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Estimator', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('quantity surveyor', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('qs', @equivalentId)

SET @equivalentId = 'af8df45b-450f-474d-bba7-40d6326b9a3a'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Sound Engineer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Audio Engineer', @equivalentId)

SET @equivalentId = 'fd0cbbac-9e74-4869-9ded-890c503a857e'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Quality Assurance Engineer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('qa engineer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('quality engineer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('reliability engineer', @equivalentId)

SET @equivalentId = '6e9db557-2bdf-45bc-9557-26d2eea8e99c'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Environmental Engineer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Environmental Officer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('sustainability engineer', @equivalentId)

SET @equivalentId = 'be3d9947-054f-4467-a212-c6f5da676a5b'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('hydraulic fitter', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('hydraulic mechanic', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('hydraulic fittersales', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('hydraulic technician', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('hydraulic valve technican', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('hydraulic hose fitter', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('hose technican', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('industrial hydraulic technican', @equivalentId)

SET @equivalentId = '28a9436d-2f26-4397-ae97-059c012827bf'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('mechanical engineer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('mech eng', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Maintenance Engineer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Plant Engineer', @equivalentId)

SET @equivalentId = '5b7ba4b9-634b-4902-a029-808e6a83ae5b'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Mining Manager', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Quarry Manager', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Quarry Operations', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('project manager mining', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('mining project manager', @equivalentId)

SET @equivalentId = '060e872f-11d4-4215-a282-5c2e82d5707a'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('mining engineer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Mine Geologist', @equivalentId)

SET @equivalentId = '1e5ec6b7-4e03-435f-b1a5-46c463944373'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('underground', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('under ground', @equivalentId)

SET @equivalentId = 'f463142d-2e63-48f5-810e-3f3202e7790d'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('aboveground', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('above ground', @equivalentId)

SET @equivalentId = 'e99aa5b8-d152-4a98-aba3-cd28ae10c965'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Civil Engineer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('civilengineer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Senior Civil Engineer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Sr Civil Engineer', @equivalentId)

SET @equivalentId = '7ab64759-310c-48e4-8752-aa99a6bd04a6'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Trainee Civil Engineer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Junior Civil Engineer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Graduate Civil Engineer', @equivalentId)

SET @equivalentId = 'dccf04f0-0d20-4163-9b5b-d98ec61824ca'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Process Engineer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Production Engineer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Manufacturing Engineer', @equivalentId)

SET @equivalentId = 'ff016cd5-d959-408e-8755-66c25c62ffc0'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Architectural Design', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Project Architect', @equivalentId)

SET @equivalentId = '2e55c9ad-ab6e-483a-80d3-29cc6f3946fb'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Senior Architect', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Senior Design Architect', @equivalentId)

SET @equivalentId = '83982632-8cd3-4148-8182-c0aaf35efb5f'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Assistant Architect', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Architectural Assistant', @equivalentId)

SET @equivalentId = '53ac0697-d8c2-49ec-b7c0-c029647d5a13'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Graduate Architect', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Architectural Trainee', @equivalentId)

SET @equivalentId = 'a1891f42-5ac8-4359-96de-af2bbfd8c675'

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

SET @equivalentId = 'b9b5ca9d-ca3c-4a46-8898-0548c74aa053'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('computer aided design', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Autocad', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Auto CAD', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('cad', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('autocad2000', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('autocad 2000', @equivalentId)

SET @equivalentId = 'fae6b344-fae9-44a0-a2cd-b8349c733426'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Senior Draftsman', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Senior Design Structural Draftsman', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('lead draftsman', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('drafting manager', @equivalentId)

SET @equivalentId = '76dfa7a6-dfac-412c-99a7-9cb2562f8dd9'

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

SET @equivalentId = '2178b459-131f-4412-ad36-621abf2e17ef'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('tradesman', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('tradesperson', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('trades man', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('trades person', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('tradie', @equivalentId)

SET @equivalentId = 'd2ac5f85-212a-468f-962b-1c54e22094ef'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('plasterer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('solid plasterer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('fibrous plasterer', @equivalentId)

SET @equivalentId = '56fc9f41-e82d-41c6-94b5-52fb343bcfbc'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('scaffolder', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('scaffolding coordinator', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('scaffolding labourer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('scaffolding', @equivalentId)

SET @equivalentId = '211782ef-1d47-4f02-9bc7-4c2a73c11a89'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Mechanic Manager', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Workshop Manager', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('MECHANICAL SUPERVISOR', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Mechanical Manager', @equivalentId)

SET @equivalentId = '35d8a7d9-d378-4006-a2b5-a5e6a0d451c5'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('boilermaker', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Boilermaker/Welder', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Welder', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('boiler maker', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('welding', @equivalentId)

SET @equivalentId = 'cb31b117-3c65-4741-9108-12ef85aa19ce'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Fitter', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Fitter/Welder', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Mechanical Fitter', @equivalentId)

SET @equivalentId = '4cb471ac-403b-4c34-b583-e080013c233e'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('hdpe', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('High-density polyethylene', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Hi density polyethylene', @equivalentId)

SET @equivalentId = 'c4503f3d-aea6-4b75-ab36-98d0c22d0fe0'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('pvc', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Polyvinylechloride', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Poly vinyle chloride', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Poly vinyl chloride', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Polyvinylchloride', @equivalentId)

SET @equivalentId = '69ffa57e-07fa-4e92-ba42-7ec50d1e47f2'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('mechanic', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Service Mechanic', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Motor Mechanic', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('automotive mechanic', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('auto mechanic', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('car mechanic', @equivalentId)

SET @equivalentId = 'dfc8e201-eb10-4896-a299-03503fdfe13e'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('windscreen', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('wind screen', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('auto glass', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('automotive glass', @equivalentId)

SET @equivalentId = '1350e3b3-aadf-4578-8357-a9a15f6c1ca4'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Assistant mechanic', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Trainee mechanic', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Apprentice Mechanic', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Apprentice Motor Mechanic', @equivalentId)

SET @equivalentId = '179481be-57a9-4a73-8cad-ea6235462036'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Electrical mechanic', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Maintenance Electrician', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Electrical Fitter', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('electrician', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('electrical contractor', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('sparky', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('sparkie', @equivalentId)

SET @equivalentId = 'c461f58c-07db-4897-a773-1e5263dc2d58'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Apprentice Electrical Mechanic', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('trainee Electrical Mechanic', @equivalentId)

SET @equivalentId = '87d83ddb-54e6-47f6-a5b2-a23b5ce6a28f'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Diesel Mechanic', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('diesel motor mechanic', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('diesel fitter', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('diesel engine mechanic', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('diesel mechanic/fitter', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('diesel plant fitter', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('truck mechanic', @equivalentId)

SET @equivalentId = '358e4088-5ec4-40cb-9a45-8128691f502c'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Electrical Supervisor', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Leading Hand Electrician', @equivalentId)

SET @equivalentId = 'c28c0ca6-0276-4e59-8d30-c05f3a12e197'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Apprentice Electrician', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('trainee electrician', @equivalentId)

SET @equivalentId = '2acb4b98-4f83-4cdb-8742-01a7cf916930'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('tiler', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('roof tiler', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('roofer', @equivalentId)

SET @equivalentId = '12a99a2a-5b01-422e-947f-0b1bdbf0b9df'

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

SET @equivalentId = '88a72e4d-2472-4aeb-96fc-b2763d90d3cf'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('plant manager', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('National Plant Manager', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('manufacturing manager', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Production Supervisor', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Production Manager', @equivalentId)

SET @equivalentId = '98305fa7-177f-45ea-9b95-e55af3fddf95'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Operations Coordinator', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('operations co ordinator', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('operations co-ordinator', @equivalentId)

SET @equivalentId = '1cfb8bc4-fc04-42f2-9fb7-f6e8066199a1'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Operations Analyst', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Customer Operations Specialist', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Operations Support Officer', @equivalentId)

SET @equivalentId = '9c58bfed-438f-4b6d-9ed1-0d0e8de44aae'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Production Coordinator', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Production Co-Ordinator', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Production Co Ordinator', @equivalentId)

SET @equivalentId = 'c7185eb0-cc19-45d4-a7c8-169cfac150fd'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Production Team Leader', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Production team lead', @equivalentId)

SET @equivalentId = '0fc7f3a8-98b6-4040-9d63-cf0a7721658c'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('PRODUCTION SHIFT MANAGER', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('shift manager production', @equivalentId)

SET @equivalentId = 'e1cdb424-b68f-4155-87cb-9bc46bf09b03'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('crew member', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('crewmember', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('team member', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('teammember', @equivalentId)

SET @equivalentId = 'ebd7d7b4-745b-47e0-a7c0-c14da3da5a5e'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Assistant Production Manager', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Asst production manager', @equivalentId)

SET @equivalentId = '8ceb9aa0-f60e-4c7a-820c-d02dd30e3729'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('under manager', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('undermanager', @equivalentId)

SET @equivalentId = 'd7682f86-71c5-463d-897c-c1f244ab301e'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Production Planner', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Production Scheduler', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Production Analyst', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Production Controller', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Production Assistant', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('PRODUCTION CLERK', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('mrp controller', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('supply chain planner', @equivalentId)

SET @equivalentId = '7688b57d-37da-4e1d-87ab-9f8af7aaab78'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Production Operator', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Production / Machine Operator', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Machine Operator', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('machinist', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Production Worker', @equivalentId)

SET @equivalentId = 'e6457546-bd17-4c89-a651-9dfba1079d8c'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('process worker', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Process Worker/Packer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('food production worker', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Meat Packer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('process operator', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('general hand', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('generalhand', @equivalentId)

SET @equivalentId = '7c36a1b1-bf4f-42c6-9619-9746cf417ad0'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Casual Process worker', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('temp process worker', @equivalentId)

SET @equivalentId = '7cf4b38e-9749-44be-9409-e082eba46582'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Warehouse Supervisor', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Warehouse manager', @equivalentId)

SET @equivalentId = '0a1507e6-c470-44b0-ace6-1c9e2d98d8f1'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Warehouse Assistant', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Warehouse', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Warehouse worker', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Warehouse Clerk', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Warehouse Person', @equivalentId)

SET @equivalentId = '5a673d11-0c5a-46ff-9dbb-bebde55ab4ac'

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

SET @equivalentId = '86f01b83-2f6d-44b4-a7f9-4a2474973ed1'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Maintenance Supervisor', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Maintenance manager', @equivalentId)

SET @equivalentId = 'fdd59711-dade-4754-9790-5321f2892f17'

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

SET @equivalentId = 'a808dc6c-50ae-4c28-8835-31798bc8ba4a'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('jit', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('just in time', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('justintime', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('j I t', @equivalentId)

SET @equivalentId = '64525e0b-dd3d-4059-aea1-9daf23474812'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('tqc', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('total quality control', @equivalentId)

SET @equivalentId = '66313e44-d1cf-474a-9309-63dd05100591'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('IT Manager', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('I.T. Manager', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('National IT Manager', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Group IT Manager', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('IT Project Manager', @equivalentId)

SET @equivalentId = '3073a3ec-0a7c-4f7e-9fd1-22214498375a'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Infrastructure Project Manager', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Network Manager', @equivalentId)

SET @equivalentId = '4788674f-5138-4422-b405-24a0d60aca4b'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Assistant IT Manager', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Asst IT Manager', @equivalentId)

SET @equivalentId = 'd53575d1-f31a-42a9-9da6-fb19564b0b12'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('IT Operations Manager', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Computer Operations Manager', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('IT operations', @equivalentId)

SET @equivalentId = '26d8f0ca-2a97-4831-b6fd-8d72269bef27'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Software Architect', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Systems Architect', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Senior Analyst Programmer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Senior Software Architect', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Senior Software Design Engineer', @equivalentId)

SET @equivalentId = '7c10664e-77fa-4e2b-ac87-87acf22fb8e8'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Senior Developer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Lead Developer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Senior Programmer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Senior Software Engineer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Lead Software Engineer', @equivalentId)

SET @equivalentId = '5cae9efb-624e-46f2-9237-a1cd32d2afbc'

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

SET @equivalentId = '6e3ee414-c61c-423a-aba4-4b18da4bc139'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Junior Programmer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Junior Developer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Trainee Programmer', @equivalentId)

SET @equivalentId = 'fd6781b0-2ffc-4931-ad35-475d1c0283cc'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Designer/Developer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Interface Developer', @equivalentId)

SET @equivalentId = '86b8aed1-34ef-42cd-9cb2-ebb642807324'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Systems Analyst/Programmer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Programmer/Analyst', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Programmer Analyst', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Analyst/Programmer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Senior Analyst/Programmer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Systems Analyst', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Senior Systems Analyst', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('System Analyst', @equivalentId)

SET @equivalentId = '41b70279-58d2-4963-8b8b-501e128406a8'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('System Engineer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Systems Engineer', @equivalentId)

SET @equivalentId = 'c82a925d-ba77-4205-b706-6c24b6764d63'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Test Team Leader', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('TEST MANAGER', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Test Coordinator', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Testing Manager', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Senior Test Analyst', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Sr Test Analyst', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Senior Test Engineer', @equivalentId)

SET @equivalentId = '0ad398ef-582b-48ac-8cef-3e4ecd972faf'

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

SET @equivalentId = '519d92cb-4737-4217-aed3-35804da5f093'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('IT Administrator', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('I.T. Administrator', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('IT Admin', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('I.T. Admin', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('IT Systems Officer', @equivalentId)

SET @equivalentId = 'ada2434c-a0fc-47b2-b513-94d6d232754c'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('IT Officer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('IT Assistant', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('I.T. Officer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('I.T. Assistant', @equivalentId)

SET @equivalentId = 'ff0ea6b0-59b2-47d6-8f55-303dfaeeaa32'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('IT Specialist', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('it guru', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('it expert', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('information technology specialist', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('information technology expert', @equivalentId)

SET @equivalentId = '1e55a111-7732-4c82-a94b-414b810272b9'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('IT Trainer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('IT Tutor', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('computer tutor', @equivalentId)

SET @equivalentId = 'f7df6d1d-178a-49ac-9777-28a1067e8bb5'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Network architect', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Network Technician', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Chief Network Architect', @equivalentId)

SET @equivalentId = '86d1ce83-638b-4299-9871-a2d8ecc07fd6'

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

SET @equivalentId = '569498b9-1e85-4e08-a7e2-9bce1b0b84d4'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Network Engineer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Network support engineer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Network Support', @equivalentId)

SET @equivalentId = '1450e8b6-6e70-429c-8d13-9e8b8d175043'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Network Consultant', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Network Support Officer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Network Specialist', @equivalentId)

SET @equivalentId = 'd6df2107-d153-4b2b-854e-911fb576db03'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Database Administrator', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('DBA', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('D.B.A.', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('data base administrator', @equivalentId)

SET @equivalentId = '7d1cc6ed-df1e-438c-a069-c9b3e32967a3'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('IT consultant', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('I.T. Consultant', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Systems consultant', @equivalentId)

SET @equivalentId = 'c90f94aa-92c2-42e8-9b39-31fff1fa65be'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('java developer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('java', @equivalentId)

SET @equivalentId = '457c7f76-19c4-46e0-b1e3-30aa9ba17074'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('idn', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('identity driven networking', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('identity driven network', @equivalentId)

SET @equivalentId = '56ac1059-d37c-4e6a-81b3-c06c957d69b2'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('IT Contractor', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('IT Contracts', @equivalentId)

SET @equivalentId = '7bf184c3-6bc9-4335-8075-4e87ad73251d'

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

SET @equivalentId = '9b6d0e7b-b5a7-4e67-8b2d-3ffce4b66b8e'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Intranet Developer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Intranet Application Developer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Intranet Software Developer', @equivalentId)

SET @equivalentId = '2cc90a11-873f-444e-a6d1-27a93e53c1fc'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Website Developer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Web Application Developer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Web Designer/Developer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Web Site Developer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Freelance Web Developer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Senior Web Developer', @equivalentId)

SET @equivalentId = '33ff2104-c9ab-4a42-9631-c4e9dbee2a7e'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('IT Support Engineer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Computer Engineer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Desktop Support Engineer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('IT Support Technician', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Computer Technician', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('IT Technician', @equivalentId)

SET @equivalentId = 'cef1eabe-e2f8-4627-bb9e-76e718b41346'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('cms', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('content management system', @equivalentId)

SET @equivalentId = '51c1aaa0-8795-4301-a0a2-2e7150fcf067'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('ColdFusion', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('cold fusion', @equivalentId)

SET @equivalentId = '8cdc1ce2-ebca-477f-89b3-ecb7e730ae9a'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('vb.net', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('visual basic.net', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('visual basic dot net', @equivalentId)

SET @equivalentId = 'e90731f3-ac08-4300-b48d-a20478176df6'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('VB', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Visual basic', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('visualbasic', @equivalentId)

SET @equivalentId = 'd59ccde9-da91-4716-8499-f0679c2cdfab'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Oracle Database Administrator', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Oracle DBA', @equivalentId)

SET @equivalentId = '64c3f471-848e-46a6-a3ce-7b0ba6d504ff'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('UNIX Systems Administrator', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Senior Unix Systems Administrator', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Unix Consultant', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Unix Consoultant', @equivalentId)

SET @equivalentId = 'ae0056f6-40bf-46b8-beb3-a3ccef3cb496'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Telco', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Telecoms', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Telecom', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Telecomms', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Telecommunication', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Telecommunications', @equivalentId)

SET @equivalentId = '60bac3be-8acc-4488-ac1d-8e48a82ff074'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('PABX', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Private Automatic Branch eXchange.', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('pbx', @equivalentId)

SET @equivalentId = '8134e551-cf3c-4753-85a2-d6e99f940e25'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Solutions Architect', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Solution Architect', @equivalentId)

SET @equivalentId = 'b41ed5d8-9e7e-4303-97fe-101fcc51428d'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Graphics/Multimedia', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('multimedia', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('multi media', @equivalentId)

SET @equivalentId = '23da5052-43cd-452c-a703-9cf4a0cae570'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Graphic designer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Graphic Design', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('graphic artist', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('freelance designer', @equivalentId)

SET @equivalentId = '9b5825d0-71dd-45c3-a831-324f18843dd7'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('freelance graphic designer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Freelance Graphic Design', @equivalentId)

SET @equivalentId = 'd322b080-689c-46d0-92de-066d097d3aab'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Junior Graphic Designer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Trainee Graphic Designer', @equivalentId)

SET @equivalentId = '46e52aa2-efc4-4b9a-b5c5-7e24d78854b2'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Web / Graphic Designer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Web/Graphic Designer', @equivalentId)

SET @equivalentId = '32e139c6-9b05-4879-b0aa-b3768554c080'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('general counsel', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('head counsel', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('head lawyer', @equivalentId)

SET @equivalentId = '28cbe402-fd78-448f-9782-fdce118bcba1'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Lawyer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Solicitor', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Legal Counsel', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('cousel', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Corporate Solicitor', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Litigation Solicitor', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Law Graduate', @equivalentId)

SET @equivalentId = '551c1208-0ac2-47a2-9796-029673c9bc95'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Legal Secretary', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Legal Secretary/Receptionist', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Legal Secretary/Personal Assistant', @equivalentId)

SET @equivalentId = '95ba2cfd-c0cf-4b2a-ab43-77f4c434e776'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Corporate legal secretary', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Commercial legal secretary', @equivalentId)

SET @equivalentId = 'dc39b0cb-a26e-4f6f-a979-3c9445c3d002'

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

SET @equivalentId = '167da1e8-b917-4af4-bcb7-fe60a808d06e'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('jp', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('justice of the peace', @equivalentId)

SET @equivalentId = '1dc23688-87af-4d29-ad0f-4f4959b0ad5a'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Associate Nurse Unit Manager', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Nurse Unit Manager', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Charge Nurse', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('head of nursing', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('nursing manager', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('director of nursing', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('nursing director', @equivalentId)

SET @equivalentId = 'cb9b23b0-b046-4698-a9d2-a0fa619dc4a6'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Registered Nurse', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Registered nurse - Level', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Practice Nurse', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Staff Nurse', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('State Enrolled Nurse', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('ren', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('sen', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('rn', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('registered enrolled nurse', @equivalentId)

SET @equivalentId = 'eca13169-4ab0-4f90-8bc4-f90c474b4591'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Clinical Nurse Specialist', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Clinical Nurse', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Clinical Nurse Consultant', @equivalentId)

SET @equivalentId = '589e5b51-9eb9-4605-8b51-0bd43d7f3248'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('orderly', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('nursing attendant', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('personal service assistant', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('hospital orderly', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('medical orderly', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('shift orderly', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('psa', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('p s a', @equivalentId)

SET @equivalentId = '19a11822-a6e2-4ed7-943f-43f157f57c9d'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Clinical Nurse Educator', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('General Nurse Training', @equivalentId)

SET @equivalentId = 'dbbe4245-bad2-420e-b3d6-a2dc3cce7b31'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Nursing Assistant', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('assistant nurse', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Nurse Aid', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('ain', @equivalentId)

SET @equivalentId = '8a9dce4e-df86-4e65-94b3-a19759988eda'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('trainee nurse', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Student Nurse', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Trainee Enrolled Nurse', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Student Enrolled Nurse', @equivalentId)

SET @equivalentId = 'e0305746-b1a7-4374-87e4-2061e74d5973'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Nurseryhand', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('nursery hand', @equivalentId)

SET @equivalentId = '75bb5c7a-9aa6-4a83-ab3f-b7cee527fe38'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Medical Receptionist', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Medical Secretary', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Senior Medical Receptionist', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Casual Medical Receptionist', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('medical centre receptionist', @equivalentId)

SET @equivalentId = '31bd7687-3c12-4f59-a8f8-31f08a7e4b16'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('medical records clerk', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('medical records officer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('medical information officer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('medical information clerk', @equivalentId)

SET @equivalentId = '5837df47-c6fe-4ef7-9a0f-05c4b8ac7e28'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('dentist', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('dental surgeon', @equivalentId)

SET @equivalentId = '992c00fa-dca6-4b39-88e6-51d996d90720'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Dental Nurse', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Dental Assistant/Receptionist', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Dental Assistant/Reception', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('dental assistant', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('dental assisting', @equivalentId)

SET @equivalentId = '0df6dbe7-c474-4bee-8120-6ddb7f20ce4d'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('dental hygienist', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('dental technician', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('dental therapist', @equivalentId)

SET @equivalentId = 'c82c46ed-1cda-426e-ba00-abace9251a83'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Junior Dental Nurse', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Trainee Dental Nurse', @equivalentId)

SET @equivalentId = '69713c22-480a-4470-881c-f39201b2a721'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Veterinary Nurse', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Vet Nurse', @equivalentId)

SET @equivalentId = '06a9ccfe-7a30-41e4-b51a-5a5b45578103'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('vet', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('vetinary', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('vetinarian', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('veterinarian', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('veterinary', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('veterinary practitioner', @equivalentId)

SET @equivalentId = '9613b365-3f99-4a96-9ce4-486b84084ca3'

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

SET @equivalentId = 'ce24311e-143a-4901-816c-d18a49c79918'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('obstetric', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('gynaecology', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('gynaecologist', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('obstetrician', @equivalentId)

SET @equivalentId = 'c1fcdaeb-aa9a-47b3-ba44-4ce45db6d1a2'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('ward clerk', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('ward officer', @equivalentId)

SET @equivalentId = '474ecc07-a9ce-4609-a1f4-239fac135fa4'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('nanny', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('nannies', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('aupair', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('au-pair', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('au pair', @equivalentId)

SET @equivalentId = '52c8b4cb-da0d-429a-abad-ef3f5d964782'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('osteopath', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('osteo', @equivalentId)

SET @equivalentId = '8d23b342-0262-4748-846b-fb14b119e7be'

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

SET @equivalentId = '2bdc8b29-dff9-47fb-b3cb-e8ed40244a84'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('pbac', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Pre-qualifications Bids and Awards Committee', @equivalentId)

SET @equivalentId = '0d99bba4-9b8f-4001-8b49-ff7b116ac2c5'

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
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('sales people', @equivalentId)

SET @equivalentId = '83335fc2-c380-44e5-acd4-f276a4e3ad26'

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

SET @equivalentId = 'fb78e02b-cc83-4cd7-9bff-7588b4bcbed7'

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

SET @equivalentId = 'b70773c2-1698-4265-84d4-65e1197be691'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Assistant Sales Manager', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Asst Sales Manager', @equivalentId)

SET @equivalentId = '79246e65-e345-4ebd-8604-4551d2f4a2ec'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Channel Manager', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('channels manager', @equivalentId)

SET @equivalentId = 'd872b359-2898-42c7-92c8-f6a718a0d183'

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

SET @equivalentId = 'bfffdb66-93b2-4439-b4b2-6539936e7de1'

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

SET @equivalentId = '1ea4459a-fab2-4189-a537-ad12fc62dc15'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Internal Sales Representative', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Internal Sales Rep', @equivalentId)

SET @equivalentId = '41675d35-8b57-44a0-9d64-d350145f2e93'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Sales Agent', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Sales Advisor', @equivalentId)

SET @equivalentId = 'aba17ea6-0cdf-4869-8436-28d87c13be9f'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('SALES ASSOCIATE', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Sales Assoc', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Senior Sales Associate', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Sr Sales Associate', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Sr. Sales Associate', @equivalentId)

SET @equivalentId = 'ce78499c-8219-488a-9b8c-848ae8611380'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Sales Assistant/Manager', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Senior Sales Assistant', @equivalentId)

SET @equivalentId = '9cf05a1a-dffd-4f4d-8e65-72fa9682c0aa'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Junior Sales Assistant', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Sales Assistant/Cashier', @equivalentId)

SET @equivalentId = '0ea13180-3310-41ea-874b-2c54b3369f2b'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Sales Assistant/Supervisor', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('supervisor/sales assistant', @equivalentId)

SET @equivalentId = '21886a19-482f-4e2a-a853-484bcdc120d9'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Sales Assistant Customer service', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Customer Service/Sales Assistant', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Receptionist/Sales Secretary', @equivalentId)

SET @equivalentId = '969778df-61e0-4f31-a72c-ad5e2804d2dc'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Advertising Sales Manager', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Advertising Sales Representative', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Advertising Sales Rep', @equivalentId)

SET @equivalentId = '6aae0225-c2d0-4d28-aba7-0f9592c41e00'

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

SET @equivalentId = '4fe7ac77-bf7e-4652-b782-2bb001b85254'

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

SET @equivalentId = '6f9bac5b-d35e-40e0-b2db-6e02f822bc00'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('vp marketing', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('vice president marketing', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('marketing vice president', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('marketing vp', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('marketing v.p.', @equivalentId)

SET @equivalentId = 'ec0f99c4-92e0-4672-aaaa-535e7edb7ba1'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('membership manager', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('membership development manager', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('manager membership services', @equivalentId)

SET @equivalentId = '261a8eed-3277-4aa5-b296-3cb9aa17b45d'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('membership consultant', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('membership officer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('membership services officer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('membership administrator', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('membership salees consultant', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('membership development consultant', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('membership development offcier', @equivalentId)

SET @equivalentId = '7b6d3642-72c7-41bf-a6fe-b924c8723634'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Marketing Assistant', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Marketing asst', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Casual Marketing Assistant', @equivalentId)

SET @equivalentId = '3368b128-b682-4534-a9ba-2603402cc862'

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

SET @equivalentId = 'fe9a80f7-c827-421c-b03a-9e297e8267ae'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('PR', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('p r', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('pblic relations', @equivalentId)

SET @equivalentId = '0355bc34-36ff-4bcf-abee-7a5754017734'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Marketing Communications Specialist', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('marketing communications', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('marketing coordinator', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('marketing co ordinator', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('marketing co-ordinator', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Marketing Communications Co-ordinator', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Marketing Communications Co ordinator', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Marketing Communications Coordinator', @equivalentId)

SET @equivalentId = '629a8a0c-c7ff-4e80-9b54-31f852ce707a'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Communications', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Communications Consultant', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('COMMUNICATIONS COORDINATOR', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Communications Specialist', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('comms', @equivalentId)

SET @equivalentId = 'b3a13e2d-d089-4341-b88c-f0d86b954781'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Market Research', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Market researcher', @equivalentId)

SET @equivalentId = 'b0fdb41d-0f09-4d80-b369-2311ff853164'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('fundraising', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('fund raising', @equivalentId)

SET @equivalentId = 'e42363b0-ff3c-49a4-90ab-f3da8d6b5c11'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Promotions', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Promotions Officer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Promotions Co-ordinator', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Promotions Manager', @equivalentId)

SET @equivalentId = 'b74f6ede-0601-41f5-918a-675f8d097b24'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Senior Publicist', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Sr Publicist', @equivalentId)

SET @equivalentId = '6bd92d81-d7b9-46c8-8933-7d845ec59dc3'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Event Manager', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Events Manager', @equivalentId)

SET @equivalentId = 'f26ded0b-e4da-4b6d-ad62-6dfe40efaa8c'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Advertising Sales Executive', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Advertising Sales', @equivalentId)

SET @equivalentId = '811c28f0-52af-4ce0-95e4-9dea7a3dfd62'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Product Manager', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Senior Product Manager', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('National Product Manager', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Assistant Product Manager', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Group product manager', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('New Product Manager', @equivalentId)

SET @equivalentId = 'd4e50883-fdbe-4f02-8a93-f7f28649a5a1'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Market Research Officer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Market Research Consultant', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Market Research Analyst', @equivalentId)

SET @equivalentId = '6f4452e3-1ea7-4d57-ab86-ae7bd922f645'

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

SET @equivalentId = '70389ca8-9e6b-4797-ae3e-270983a4d0b4'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('IT Business Analyst', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('I.T. Business Analyst', @equivalentId)

SET @equivalentId = '019e79e8-f903-4909-81cf-c1682835c1bb'

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

SET @equivalentId = 'c8416c00-3cd2-42b1-89cf-41b2ce7b3615'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Head cleaner', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Cleaning Manager', @equivalentId)

SET @equivalentId = '53683a2d-4a58-4e7a-9cd8-40520e4b50d0'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Shopping Centre Cleaner', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('centre cleaner', @equivalentId)

SET @equivalentId = '3a8e2401-3d9a-42f2-a718-757a6d1d2432'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Research Assistant', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Research Asst', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Senior Research Assistant', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Sr Research Assistant', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('clinical research', @equivalentId)

SET @equivalentId = 'cc407940-200e-45f4-aaa5-b65c99cc34ef'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Principal', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Senior Teacher', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Acting Principal', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Vice Principal', @equivalentId)

SET @equivalentId = '11bb6ed3-ea2b-4639-82d6-ac3524adbdd0'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Teacher', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('school teacher', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Teacher/Trainer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('High School Teacher', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Middle School Teacher', @equivalentId)

SET @equivalentId = 'f832e3b9-83ac-4f98-b458-488529ec4172'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Classroom Teacher', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Primary Teacher', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Primary School Teacher', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Senior grades teacher', @equivalentId)

SET @equivalentId = 'ecaf3085-9512-451d-951b-2bb58786ff55'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('librarian', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('library teacher', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('teacher librarian', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('campus librarian', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('branch librarian', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('knowledge centre manager', @equivalentId)

SET @equivalentId = '60c25a24-5480-44ce-9c0a-cd680f6253ac'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('library technician', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('library assistant', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('library attendant', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('libraries officer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('library officer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('library worker', @equivalentId)

SET @equivalentId = 'b457e221-e99d-4494-9194-3db60b990e35'

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

SET @equivalentId = '07fde081-0ff1-4590-ac98-0ec32def3787'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Casual Teacher', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Casual Relief Teacher', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Relief Teacher', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Temporary Relief Teacher', @equivalentId)

SET @equivalentId = '61584816-8f92-4c00-9449-4a5104992d15'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('English Teacher', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('English Language Teacher', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('ESL Teacher', @equivalentId)

SET @equivalentId = '28207402-3e2e-4dc1-943d-cdbbb85da243'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Computer Teacher', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Information Technology Teacher', @equivalentId)

SET @equivalentId = '9ba7428f-b5d7-453c-8130-e7494f9be83d'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('TAFE Teacher', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('TAFE sessional teaching', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('T.A.F.E. Teacher', @equivalentId)

SET @equivalentId = 'b8cdeaf2-b722-4b7c-a73d-048f989ba390'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Mathematics Teacher', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Mathematics/Science Teacher', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Maths Teacher', @equivalentId)

SET @equivalentId = '0aa5f847-c721-4c55-9184-b518c6574199'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('math', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('mathmatics', @equivalentId)

SET @equivalentId = '74126ac7-ec3c-45bd-bc1c-0af2d4d55c6b'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Art Teacher', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Graphic Art Teacher', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Visual Art Teacher', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Teacher Art', @equivalentId)

SET @equivalentId = '0f10f023-d97a-4fd6-a3ef-a954aeec6515'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Piano Teacher', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Music Teacher', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Instrumental Teacher', @equivalentId)

SET @equivalentId = '71a727da-7d98-474b-894b-93e1bceee2e6'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Dance teacher', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Belly dance teacher', @equivalentId)

SET @equivalentId = 'fc7f394e-5e86-4cf7-8857-4be4c4aa8b4d'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('LOTE Teacher', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('L.O.T.E. Teacher', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Foreign Language Teacher', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Chinese Teacher', @equivalentId)

SET @equivalentId = '85ef8bff-d643-40b6-a827-cf9dbae73a79'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Production Technician', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Production Crew', @equivalentId)

SET @equivalentId = '48a1ef47-e35d-49f1-9371-544f74186b2d'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Child Protection Worker', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Child Protection Officer', @equivalentId)

SET @equivalentId = '5e1db14f-b32d-4d3d-a896-8e156c543e06'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Regional Survey Manager', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('State Survey Manager', @equivalentId)

SET @equivalentId = 'ace108c4-e665-4c66-a34b-c62f1f61711a'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Policy Manager', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Polices Manager', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Senior Policy Officer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Senior Policies Officer', @equivalentId)

SET @equivalentId = 'f5d5f24f-b7e1-4c19-8b0d-857ce5d2b26d'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Unit Manager', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Unit Operations Manager', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Unit Director', @equivalentId)

SET @equivalentId = 'a8a24a3e-e8ba-41fd-848f-8a86800e9ae7'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Assistant', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Asst', @equivalentId)

SET @equivalentId = 'dae14ad6-d383-40c4-b6b4-24da495cddf1'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('administrator', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('ADMINISTRATIVE MANAGER', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('BUSINESS ADMINISTRATOR', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Senior Administration Officer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Business Administration', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Executive Administrator', @equivalentId)

SET @equivalentId = '76bc76f5-de9e-4b19-affb-7080b8d9c63a'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Operations Administrator', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Operations Admin', @equivalentId)

SET @equivalentId = '76ebed28-ba08-4d0d-9aeb-cd854cac46a1'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Factory Hand', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Casual Factory Hand', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('General Factory Hand', @equivalentId)

SET @equivalentId = 'd39d4272-3474-4cc0-a96e-b6cbb6e89231'

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

SET @equivalentId = '71fe8828-6d6a-47dd-91c7-a4ae90b2d57b'

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

SET @equivalentId = 'c1aaa982-e6e0-4fc6-a961-6608d2361e78'

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

SET @equivalentId = '5ccc704a-f204-44c2-b02e-6a594416057a'

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

SET @equivalentId = '1203a9a0-6056-4b8e-9c46-cb75c7cebd8d'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Delivery Driver', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Pizza delivery driver', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Multi-drop Delivery Driver', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Delivery Driver/Kitchen hand', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Owner Driver', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Driver', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('courier', @equivalentId)

SET @equivalentId = 'cb75d5b1-ec4d-49e4-aa5a-af610bf268f7'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Bus Driver', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Coach Driver', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('school bus driver', @equivalentId)

SET @equivalentId = 'ce06110a-4f3a-4229-b354-2e759acc180c'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Taxi Driver', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Cab Driver', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('HGV Driver', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('cabbie', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('taxidriver', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('taxi operator', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('cabdriver', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('chauffer', @equivalentId)

SET @equivalentId = 'ba732847-a115-420a-8115-2a876280ce0c'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Interstate Truck Driver', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Truck Driver/Plant Operator', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Truck Driver', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('truckdriver', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('truckie', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('dump truck operator', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('truck driving', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('line haul', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('linehaul', @equivalentId)

SET @equivalentId = 'd0ba34be-b13a-4148-bab0-16048944ae1d'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('cold store', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('coldstore', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('coolroom', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('cool room', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('cold storage', @equivalentId)

SET @equivalentId = 'ae5f8234-eb18-4273-82b3-729327d25093'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('lecturer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Music Technology Lecturer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Associate Lecturer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Assistant Lecturer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Senior Lecturer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Guest Lecturer', @equivalentId)

SET @equivalentId = '34f12a6e-eeab-45cc-8c7b-ca91869c543e'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Coordinator', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Co ordinator', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Co-ordinator', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('ordinator', @equivalentId)

SET @equivalentId = '28b214cf-6652-4529-82e8-cca6e35c6c01'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('cabling technician', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('cabler', @equivalentId)

SET @equivalentId = '1b62274c-6bd1-46e3-8c09-ec21fc25c3b4'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Work Experience', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Work Experience Student', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('WORK EXPERIENCE - TAFE', @equivalentId)

SET @equivalentId = '0a638735-197c-4903-804a-118b0e302347'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('PHARMACY ASSISTANT', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Pharmacy Assistant Customer Service', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('dispensary technician', @equivalentId)

SET @equivalentId = '4e66d69d-00e6-4ace-a96e-9c08fec70ed1'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('pharmacist', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('pharmacy manager', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('retail pharmacist', @equivalentId)

SET @equivalentId = '3b542732-e570-4dac-8202-34eb43256863'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('pharmaceutical', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('pharmaceutics', @equivalentId)

SET @equivalentId = 'f9286b83-5943-4719-b160-9542fe03f4f1'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Trainee', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Traineeship', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Student trainee', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Graduate Trainee', @equivalentId)

SET @equivalentId = '902698ba-a084-48cb-a181-7298a6e9e718'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Volunteer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Volunteer Worker', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Volunteer Member', @equivalentId)

SET @equivalentId = '2cda853c-ce8e-42c1-ba73-ebdb2600db64'

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

SET @equivalentId = '3022310f-76ca-4773-89dc-2646f448d310'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Child Care Assistant', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Child Care Worker', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('child care', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Child Carer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Qualified Child Care Worker', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Casual child care assistant', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('childcare worker', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('childcare professional', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('kindergarten teacher', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('creche attendant', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('preschool teacher', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('pre school teacher', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Early Childhood Teacher', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('creche leader', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('creche team leader', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('creche worker', @equivalentId)

SET @equivalentId = 'f9052385-8a9b-4457-bbb8-f00518f5f4f1'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Computer Operator', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Senior Computer Operator', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Trainee Computer Operator', @equivalentId)

SET @equivalentId = 'f7ac17ed-e962-42ea-8eba-7245d0cf342a'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('FARMHAND', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Farm hand', @equivalentId)

SET @equivalentId = '11e89e83-a858-4f9d-be79-79a8beac18b7'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Technical Consultant', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Senior Technical Consultant', @equivalentId)

SET @equivalentId = 'b509997b-7cb9-4555-ae0a-ecd546242fc7'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Various', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('varied', @equivalentId)

SET @equivalentId = '047b461a-0bbe-4aa1-97fa-80d333b9f1c6'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Business Consultant', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Senior Business Consultant', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('New Business Consultant', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Telephone Business Consultant/Customer Service Representive', @equivalentId)

SET @equivalentId = '05e04e43-b78b-4700-8e7f-bf5fac78c273'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Laboratory Technician', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('medical laboratory technician', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('lab technician', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Laboratory Assistant', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Lab Assistant', @equivalentId)

SET @equivalentId = 'ebe64dee-f41f-4049-a199-0e149770afa9'

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

SET @equivalentId = '51bc096e-1c51-452a-a04d-9d9a9bf41cd1'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Delicatessen Manager', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Deli Manager', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Service Deli Manager', @equivalentId)

SET @equivalentId = 'f7bc8645-444d-4783-b21a-b47d46157152'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Deli Assistant', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Service Deli Assistant', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Serviced Deli Assistant', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('DELICATESSEN ASSISTANT', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Deli', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Delicatessen', @equivalentId)

SET @equivalentId = '63c6f1fa-83f1-45b4-990a-aaa4b4901981'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('dialler', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('power dialler', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('predictive dialler', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('automated dialler', @equivalentId)

SET @equivalentId = 'f07f55c5-51e2-4fa8-b155-31074c09d16a'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Translator', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Interpreter', @equivalentId)

SET @equivalentId = 'd854791a-10a9-432e-b246-c46707542e84'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Stockbroker', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Stock broker', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Stock broking', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Stockbroking', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Stockbrokers', @equivalentId)

SET @equivalentId = 'f6b52348-b2ca-4933-90f6-f2f14a3624d7'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('1', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('one', @equivalentId)

SET @equivalentId = '13d6d82a-35d6-420e-9e13-0fac213fc789'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('2', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('two', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('ii', @equivalentId)

SET @equivalentId = '1db3fedb-99fd-4b21-8ef5-40b747348bd1'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('3', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('three', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('iii', @equivalentId)

SET @equivalentId = 'e5148041-2bd3-42f0-b03c-450819954910'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('4', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('four', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('iv', @equivalentId)

SET @equivalentId = '06674866-636f-49f0-8753-2151930d9b18'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('5', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('five', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('v', @equivalentId)

SET @equivalentId = 'c3c0f9ae-14a7-41a3-b436-cd6f6aa56617'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('6', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('six', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('vi', @equivalentId)

SET @equivalentId = '62e3516c-b75f-4f92-b2a0-fc5cef36118e'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('7', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('seven', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('vii', @equivalentId)

SET @equivalentId = '72d79468-95bb-415b-a3e3-314b06be7647'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('8', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('eight', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('viii', @equivalentId)

SET @equivalentId = 'a132ea9e-559c-458c-b968-0e2c596ab56a'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('9', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('nine', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('ix', @equivalentId)

SET @equivalentId = '4b54afe6-16f3-438f-bc71-af03d4e09a25'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('10', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('ten', @equivalentId)

SET @equivalentId = '8d3e1ccc-3e99-40fa-a649-562d57e7a6bd'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('1st', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('first', @equivalentId)

SET @equivalentId = 'b2952d22-9532-4532-a37e-cbec8cdb4c13'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('3ic', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('3.i.c.', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('3i.c.', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Third in charge', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('3 IC', @equivalentId)

SET @equivalentId = '11831c32-fd40-4d53-ac10-e8223661fcee'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('2nd', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('second', @equivalentId)

SET @equivalentId = 'de37b15c-5695-4bfc-8d84-eb5971375cb5'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('3rd', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('third', @equivalentId)

SET @equivalentId = '2c598902-ace7-4301-b6bd-1d0803ef8ef7'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('4th', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('forth', @equivalentId)

SET @equivalentId = '40b3b8ef-46e6-421d-91e8-494bc7f764c7'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('class 1', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('class1', @equivalentId)

SET @equivalentId = '6822c5eb-4a76-4c23-9458-f66ed9b437b4'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('class 2', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('class2', @equivalentId)

SET @equivalentId = '8fc0fc0f-b964-4554-a2f3-a5a886366207'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('class 3', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('class3', @equivalentId)

SET @equivalentId = '054d5232-35dd-4868-b4a7-4ef400577916'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('class 4', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('class4', @equivalentId)

SET @equivalentId = '23572319-71c4-4dfa-97af-2eccc829a40e'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('class 5', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('class5', @equivalentId)

SET @equivalentId = 'b2336275-379f-48c7-8aa3-b8a0d030a4a2'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('phase 1', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('phase1', @equivalentId)

SET @equivalentId = '69ad39bb-eb51-4612-97ea-00c5aa4e2770'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('phase 2', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('phase2', @equivalentId)

SET @equivalentId = 'f2633c85-7b6a-4002-b78d-1537543ccca2'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('phase 3', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('phase3', @equivalentId)

SET @equivalentId = '691e92a5-841c-4edc-9cdd-9825d632c3e9'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('phase 4', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('phase4', @equivalentId)

SET @equivalentId = 'be0b429f-a5f5-4d44-bd6a-cf2238bfd9ac'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('phase 5', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('phase5', @equivalentId)

SET @equivalentId = 'f7d825d8-baf2-4da8-b2e3-b8bba704fd3b'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('after sales', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('aftersales', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('post sales', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('postsales', @equivalentId)

SET @equivalentId = '78e90f62-8adf-412a-b9ec-83278f3a7e53'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('parts interpreter', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('spare parts interpreter', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('parts cataloguer', @equivalentId)

SET @equivalentId = 'e67a07f4-5967-443b-b8d4-f7945444606f'

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

SET @equivalentId = '4094480c-227f-4c2d-a508-32dee92cbff2'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Aboriginal', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('indigenous', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('torres straight islander', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('torres straight islands', @equivalentId)

SET @equivalentId = '8ea8eb6f-749e-450d-a59c-f2939c121e0c'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('acfi', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Aged Care Funding Instrument', @equivalentId)

SET @equivalentId = 'fa7c8620-6b5b-43b2-91c9-8497c3b20ffa'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('ADSL', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Asymmetric Digital Subscriber Line', @equivalentId)

SET @equivalentId = 'f0bfde36-a76e-4818-86c0-0509f49b5a9d'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('ag', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('agriculture', @equivalentId)

SET @equivalentId = 'd4474ec3-7346-4b55-b81e-89007de9f454'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('A-Grade Electrical', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('A Grade Electrical', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('A Grade electrician', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('agrade electrician', @equivalentId)

SET @equivalentId = 'dcadd518-a427-4838-b15a-dfb3020e3363'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('asap', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('as soon as possible', @equivalentId)

SET @equivalentId = 'da5878e2-06fc-43de-81dc-a4416d6463ff'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('ASX', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Australian stock exchange', @equivalentId)

SET @equivalentId = '84813866-5110-4bc1-b24f-087e417189b1'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('aust', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('australia', @equivalentId)

SET @equivalentId = 'e85d77e2-81b0-4bba-a23d-aa512ba87909'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('bas', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('business activity statement', @equivalentId)

SET @equivalentId = 'baef8969-1b1a-4a9a-9fde-71ea5ff2fde0'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('BHP', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('BHPBilliton', @equivalentId)

SET @equivalentId = '0561c35d-d26d-48fc-bb75-17a5b1b2b3b6'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('rio tinto', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('riotinto', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('rtz', @equivalentId)

SET @equivalentId = '6283df57-4908-4c82-9631-faeecf69acf5'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('bris', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('brisbane', @equivalentId)

SET @equivalentId = '28c90899-a6ff-4038-91aa-0fbead0d19c6'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('cam', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('computer aided manufacturing', @equivalentId)

SET @equivalentId = '04231e4c-b278-4e7c-85f0-b608a20618d7'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('cardio', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('cardiovascular', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('cardio-vascular', @equivalentId)

SET @equivalentId = '22297a98-1bfd-4aac-8003-2153fc609b61'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('cashflow', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('cash flow', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('cash-flow', @equivalentId)

SET @equivalentId = 'c658c2cd-e3eb-4a37-9016-0ec3d90000e8'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('CBA', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Commonwealth Bank of Australia', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('comm bank', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('commbank', @equivalentId)

SET @equivalentId = 'ed37ec6f-4302-4172-a0f8-7d25860e2938'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('cbd', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('central business district', @equivalentId)

SET @equivalentId = 'f4561d09-46ae-4e16-a027-c82a6ff14853'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('cctv', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('closed circuit tv', @equivalentId)

SET @equivalentId = 'f809bbe1-3ee5-43e3-a9fc-87ab9582caa8'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('cert', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('certificate', @equivalentId)

SET @equivalentId = '9917c7d4-8266-4310-a4fe-a85a8fc4d5e1'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('cert1', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('certificate 1', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('certi', @equivalentId)

SET @equivalentId = '62946672-582e-497d-84bc-b64be21957c5'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('cert2', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('certificate 2', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('certii', @equivalentId)

SET @equivalentId = 'c88a84e7-d82c-4157-97e0-932dc58b8988'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('cert3', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('certificate 3', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('certiii', @equivalentId)

SET @equivalentId = 'd6cfd050-f5bb-44d3-9c6d-6976e5f80ccb'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('comp', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('compensation', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('compo', @equivalentId)

SET @equivalentId = '261177da-671e-40af-ba99-d98b9072982f'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('contract', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('agreement', @equivalentId)

SET @equivalentId = '87517871-f44b-48d1-aadf-61421d1e5a8d'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('CPU', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('central processing unit', @equivalentId)

SET @equivalentId = 'd9a092cc-3680-4cb1-860f-01d46cac8da2'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('crm', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('customer relationship management', @equivalentId)

SET @equivalentId = '33584f3a-b942-4f1e-a2cc-ef7774e7dfc2'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('cv', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('curriculum vitae', @equivalentId)

SET @equivalentId = '43c7bb77-8621-4d4d-8e5d-7aaea2094ab4'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('dispatch', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('despatch', @equivalentId)

SET @equivalentId = '51d8db70-535e-48a3-9493-2634172a5323'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('div', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('division', @equivalentId)

SET @equivalentId = '81042bde-db78-4ccb-ae60-0e39907083cc'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('DNS', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Domain Name System', @equivalentId)

SET @equivalentId = '7816799b-20af-4480-9272-5ff01308852a'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('dr', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('direct response', @equivalentId)

SET @equivalentId = '747a69ce-f01b-4226-bf87-154036c36d7d'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('drp', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('disaster recovery plan', @equivalentId)

SET @equivalentId = '195a95e5-9585-4478-8f3e-245bbed5407a'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('DSL', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Digital Subscriber Line', @equivalentId)

SET @equivalentId = '907f957f-64e3-47e9-bba5-164ade20d1ec'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('elec', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('electrical', @equivalentId)

SET @equivalentId = '81f614c0-f6b9-4aa5-81b3-ff5330eafddf'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('email', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('e-mail', @equivalentId)

SET @equivalentId = '90207d6e-f128-47d1-80c5-5e65b4428d02'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('epcm', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Engineering Procurement Construction Management', @equivalentId)

SET @equivalentId = '665a7dcf-57b4-4bb5-a41a-3eed71d476fb'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('EPS', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('earnings per share', @equivalentId)

SET @equivalentId = '581b13ab-048d-44a8-a413-2a586253473c'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('exp', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('experience', @equivalentId)

SET @equivalentId = '57d053c3-9bfe-418a-b854-0fc17d084edd'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('fifo', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('first in first out', @equivalentId)

SET @equivalentId = '3df1da6c-94e2-4f12-8721-2c496f70a53d'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('first aid', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('firstaid', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('first-aid', @equivalentId)

SET @equivalentId = 'd5a94b54-2edf-4158-a646-ef4af23710e1'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('fmcg', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('fast moving consumer goods', @equivalentId)

SET @equivalentId = '14ebb0dc-1d6e-4889-9839-f38c7c06fe1a'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Food Processor', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Food Processing', @equivalentId)

SET @equivalentId = '52145976-7892-45d1-ac23-380e2c951465'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('GDP', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('gross domestic product', @equivalentId)

SET @equivalentId = '4259303b-c134-4961-8c1d-db022c0b5a57'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('GNP', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('gross national product', @equivalentId)

SET @equivalentId = '3532ecc5-bce3-4ea4-879f-e05e263fe5ff'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('govt', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('government', @equivalentId)

SET @equivalentId = 'e9287f50-0c95-48b0-8c17-685d81b2f6e2'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('GPS', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Global Positioning System', @equivalentId)

SET @equivalentId = '9bfcabf1-f3c6-4d52-ace4-d197eae4a10d'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('guard', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('gaurd', @equivalentId)

SET @equivalentId = '99043650-c03f-4d91-86cb-3bf3641bbfc4'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Health & Safety', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Health and Safety', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Health&Safety', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('healthandsafety', @equivalentId)

SET @equivalentId = 'eaf4966e-20fc-4db2-b6ef-52ba0387d0c9'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Healthcare', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Health Care', @equivalentId)

SET @equivalentId = '94bcbfa5-16f6-4369-846a-bfcd3bfc5529'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('hp', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('hewlett packard', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('hewlettpackard', @equivalentId)

SET @equivalentId = '88125d66-d9b8-4060-ae60-14c1af11c84c'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('hris', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('human resource information system', @equivalentId)

SET @equivalentId = 'fed789df-3403-416c-b452-01a9ae1426dd'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('ie', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('institute of engineers', @equivalentId)

SET @equivalentId = '1889f65d-ceeb-4a9a-a10d-9e1a1a07f3b2'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('infra structure', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('infrastructure', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('infra-structure', @equivalentId)

SET @equivalentId = '5cad7d69-e9d3-44d7-b745-401c34a0c141'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('jdedwards', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('jd edwards', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('j.d.edwards', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('jde', @equivalentId)

SET @equivalentId = '07cbcce1-9104-4d52-85fd-7229ac8c5488'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('jnr', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('junior', @equivalentId)

SET @equivalentId = '7b7414be-55f3-4bd6-893c-1c2e8f4863aa'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('KPI', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('key performance indicator', @equivalentId)

SET @equivalentId = 'aeee5c73-339c-445a-ab5b-e07622071d9e'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('l&d', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('learning and development', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('training', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('l and d', @equivalentId)

SET @equivalentId = 'c4fc150b-d846-463f-ac68-08a2114aca0c'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('labor', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('labour', @equivalentId)

SET @equivalentId = 'd7413c56-6d9e-4d8b-b1fe-4d12a05d9a70'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('lan', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('local area network', @equivalentId)

SET @equivalentId = 'db31f54c-63df-4fd0-9b7f-2fbea7f80508'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('ltd', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('limited', @equivalentId)

SET @equivalentId = '9ad56df9-6e5f-41de-9b76-f71d1261651c'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Mac', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Macintosh', @equivalentId)

SET @equivalentId = '26e0ea41-6699-4ba3-a74b-a22df3969ded'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('macq', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('macquarie', @equivalentId)

SET @equivalentId = '2f703c89-8fa3-4207-9fa9-eed94bba5581'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('mba', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('master of business administration', @equivalentId)

SET @equivalentId = '2c6ad012-724e-4fda-a4af-2adf11673e2f'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('mech', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('mechanical', @equivalentId)

SET @equivalentId = 'd6059634-6f04-4d26-8530-4ae08a974830'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('med', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('medical', @equivalentId)

SET @equivalentId = 'fc5d73eb-8c34-4225-968a-7f2ad413e3c5'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('melb', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('melbourne', @equivalentId)

SET @equivalentId = '12d3a259-56d1-4996-a9a2-6e5152ea2f8a'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('mngt', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('management', @equivalentId)

SET @equivalentId = 'bd3fe923-a00b-4736-bba6-e58357037de5'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('mr', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('medium rigid', @equivalentId)

SET @equivalentId = '4d7346b2-cee3-45d0-80c8-59d474ecd8b4'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('ms', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('microsoft', @equivalentId)

SET @equivalentId = 'ae016cf2-3aed-4e86-8b02-70d1a12be4c9'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('NAB', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('National Australia Bank', @equivalentId)

SET @equivalentId = '196f7901-8824-49d0-8425-a5f77de75408'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('north east', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('northeast', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('north-east', @equivalentId)

SET @equivalentId = 'b36f16ff-bc24-4890-b8ac-3727c99a09d0'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('neg', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('negotiable', @equivalentId)

SET @equivalentId = 'a4bda5de-017b-4f0a-b346-b2e91bb34228'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('nsw', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('new south wales', @equivalentId)

SET @equivalentId = '4c4fcede-f241-4eaf-b78c-7686103fd8ab'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('north west', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('northwest', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('north-west', @equivalentId)

SET @equivalentId = 'cf6c9d10-dd2e-4d3f-a065-0ee63ebd5a56'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('nyse', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('new york stock exchange', @equivalentId)

SET @equivalentId = '4f5cfb68-cf42-4a60-a065-3d95179ed92f'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('nz', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('new zealand', @equivalentId)

SET @equivalentId = 'da1f6127-4a79-4208-bc55-4c49a43ebc73'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('OHS', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('OH&S', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Occupational Health and Safety', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Occupational Health & Safety', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('oh &s', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('ehs', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('environmental health and safety', @equivalentId)

SET @equivalentId = '6d39c4cb-95fa-43cd-b196-2a790b059d55'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Oil and Gas', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Oil&Gas', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Oil & Gas', @equivalentId)

SET @equivalentId = '27766274-5d74-46ce-bf85-aa9ede081a0e'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('online', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('on-line', @equivalentId)

SET @equivalentId = 'ac151556-74ea-452f-b743-186a756210ec'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('os', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('operating system', @equivalentId)

SET @equivalentId = '695d90ea-16e3-405a-86a6-401ed2ef6e8b'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('ot', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('occupational therapist', @equivalentId)

SET @equivalentId = '414e49d1-1b57-4171-b983-8753c584599b'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('p&l', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('profit and loss', @equivalentId)

SET @equivalentId = '1c6c86f9-c541-4789-85c9-6352f8b5a7fa'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('pae', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('participating administrative entity', @equivalentId)

SET @equivalentId = 'af38797c-6e6b-4e64-9f9f-d30243997068'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('payg', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('pay as you go', @equivalentId)

SET @equivalentId = '7d9c2483-18eb-4822-86f3-3cd362187ba5'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('perm', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('permanent', @equivalentId)

SET @equivalentId = '5144d00f-7aab-4f59-a1d4-86af7bbc791f'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('pi', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('personal injury', @equivalentId)

SET @equivalentId = '758f65cb-1d07-44ba-9452-f979fab370f6'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('plc', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('publically listed company', @equivalentId)

SET @equivalentId = 'a972ab72-18c0-443d-bcab-d3daa73ea391'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('POS', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Point of Sale', @equivalentId)

SET @equivalentId = '88efaa12-9aa7-42cd-996e-d7edf27d5f72'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('proactive', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('pro active', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('pro-active', @equivalentId)

SET @equivalentId = 'd20d8595-ceb4-4eba-8385-8cc2db689641'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Psych', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('psychologist', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('psychology', @equivalentId)

SET @equivalentId = 'eb974321-e60c-4cc1-ac02-197626eba887'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('pty', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('proprietary', @equivalentId)

SET @equivalentId = 'd5cc76fc-fd57-4afe-a001-75aae51db013'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Q&A', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Quality and Assurance', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Quality & Assurance', @equivalentId)

SET @equivalentId = '79a3c2d3-5c4c-4417-8639-908450e812a3'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('qa', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('quality assurance', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('qc', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('quality control', @equivalentId)

SET @equivalentId = 'c22aeb3f-0847-49ce-b4cd-fa570bfa4c78'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('qld', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('queensland', @equivalentId)

SET @equivalentId = '961bab6f-a50c-4b88-98ab-e4cc69902af0'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('qual', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('qualification', @equivalentId)

SET @equivalentId = '31b06096-af97-4e81-8f0e-f06807ef99be'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('quick books', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('quickbooks', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('quick-books', @equivalentId)

SET @equivalentId = 'd7489310-e479-497c-ad33-b4a5947bd2d5'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('r and d', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('research and development', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('research & development', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('rnd', @equivalentId)

SET @equivalentId = '033dd087-135d-4a33-8659-d7026cd90616'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('real estate agent', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('officer in effective control', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('realestate consultant', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('property consultant', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('buyers advocate', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('buyer s advocate', @equivalentId)

SET @equivalentId = '78c7c668-9c63-4d31-8d88-8d5a71e8a5ee'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('rec2rec', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('recruiter to recruiter', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('rec-to-rec', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('rec to rec', @equivalentId)

SET @equivalentId = '0c74b389-d4fb-415a-9d2b-d4c39a542711'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('reengineering', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('re engineering', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('re-engineering', @equivalentId)

SET @equivalentId = 'cf1b6f18-340a-4d22-909b-72433c333a38'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('ref', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('reference', @equivalentId)

SET @equivalentId = '7356c853-b7be-42f2-8caf-4f4a0d15655d'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('rehab', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('rehabilitation', @equivalentId)

SET @equivalentId = '3c87c3d3-9fb3-4f3b-8856-4b2e12736aff'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('req', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('requirement', @equivalentId)

SET @equivalentId = '99a124e1-10c8-4c86-8685-7172e5107cd9'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('rf', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('radio frequency', @equivalentId)

SET @equivalentId = '77782d44-37ea-493c-8704-67c4235a8d27'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('ROE', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('return on equity', @equivalentId)

SET @equivalentId = 'bd987a12-fb90-42dc-a579-cc2cd07a6951'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('ROI', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('return on investment', @equivalentId)

SET @equivalentId = '313b313f-33bf-4412-9933-86974a99ccd2'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('romp', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Radiation Oncology Medical Physics', @equivalentId)

SET @equivalentId = 'ac2dd55f-c80d-48ea-9dac-a5684562403b'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('rta', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('road traffic authority', @equivalentId)

SET @equivalentId = 'ed38f77a-48fd-4dad-b6b4-1313d35904c3'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('rtw', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('return to work', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('return-to-work', @equivalentId)

SET @equivalentId = '9c1110e1-e8dd-4582-8da0-5e2fc9877bf0'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('sap', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('s.a.p.', @equivalentId)

SET @equivalentId = '34fc83a9-13dc-4301-b838-260aca060ba4'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('SAP EP', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Enterprise Portal', @equivalentId)

SET @equivalentId = '8fadd7c6-38f7-4644-b45e-3b66c0363288'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('south east', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('southeast', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('south-east', @equivalentId)

SET @equivalentId = '9aa2b33d-b186-43b2-a84f-4b586a2d7fcb'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('south west', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('southwest', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('south-west', @equivalentId)

SET @equivalentId = '8da9eb9d-3542-4977-a4e1-db8213b0cb6f'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('SEM', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Search Engine Marketing', @equivalentId)

SET @equivalentId = '0cfa166a-8f84-4d6d-9510-f346281397a1'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('SEO', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Search Engine Optimisation', @equivalentId)

SET @equivalentId = 'a7942c6a-7e2e-46bc-975c-473acf916102'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('share point', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('sharepoint', @equivalentId)

SET @equivalentId = '64c5622d-8e53-452b-9dd2-0e56be7abc64'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('sme', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('small and medium enterprise', @equivalentId)

SET @equivalentId = '1d68a145-b9df-44d8-a271-940ffd6a4e34'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('sml', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('small', @equivalentId)

SET @equivalentId = '9d81f66c-3249-4401-8f23-c8b2bfb2eee2'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('snr', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('senior', @equivalentId)

SET @equivalentId = 'd51ac83c-e811-44a2-aab8-36621ad17c2a'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('software', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('soft ware', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('soft-ware', @equivalentId)

SET @equivalentId = '72956ee2-567c-42ad-9060-fe1682f2bd18'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('strategic', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('strategy', @equivalentId)

SET @equivalentId = 'aad5568a-75db-4ec8-8556-e1099b3d9a7f'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('syd', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('sydney', @equivalentId)

SET @equivalentId = 'ee9771fc-4ab5-4989-9d7a-fb54cc61ef3d'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('tech', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('technician', @equivalentId)

SET @equivalentId = '4710d261-e8af-4d1e-a190-9be78dd55b07'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('temp', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('temporary', @equivalentId)

SET @equivalentId = '336f389c-c5db-4600-88e3-f6df8ba6cc3e'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('through', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('thru', @equivalentId)

SET @equivalentId = 'e98cc20c-015f-4a1a-94e2-831ae0f8fd5c'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('tire', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('tyre', @equivalentId)

SET @equivalentId = '98621477-5269-438b-ae1c-b3173c47857d'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('tyre fitter', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('tyrefitter', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('wheel fitter', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('tyre technician', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('wheel aligner', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('wheel balancer', @equivalentId)

SET @equivalentId = '44554572-7814-485a-b597-fb77ef840ec6'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('tkt', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('ticket', @equivalentId)

SET @equivalentId = 'eed51ff3-23cc-4417-be5f-b837c6a06411'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('trim', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Total Records and Information Management', @equivalentId)

SET @equivalentId = 'c916f1fa-8968-4c68-9933-aae926c46649'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('tv', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('television', @equivalentId)

SET @equivalentId = 'f90cb11a-d852-4f5a-832b-616f29ad704f'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('uat', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('user acceptance testing', @equivalentId)

SET @equivalentId = '73d4238c-5271-4b80-9a24-038db764f892'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('ui', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('user interface', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('u.i.', @equivalentId)

SET @equivalentId = '3c5fd57b-12a0-4bd8-b59c-d0c1c09a0752'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('uni', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('university', @equivalentId)

SET @equivalentId = '93a04954-169a-488e-9750-6539c415ee6e'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('vic', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('victoria', @equivalentId)

SET @equivalentId = '2ce833e9-9a60-40e4-9110-187c667192f0'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('act', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('australian captial territory', @equivalentId)

SET @equivalentId = '20f12b0d-686d-4ba6-ab6b-75399ba9ad44'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('vpn', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('virtual private network', @equivalentId)

SET @equivalentId = '314aeed4-ee3c-47e7-9bf3-d9747598aab3'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('VSD', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Variable Speed Drives', @equivalentId)

SET @equivalentId = 'f7c96045-315e-44d6-af31-9ab40f7630d2'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('wa', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('western australia', @equivalentId)

SET @equivalentId = '8653ecac-5511-4060-b72a-ca571819362a'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('wan', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('wide area network', @equivalentId)

SET @equivalentId = '308b6a98-4207-45b2-af50-984ca87a2156'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('wk', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('week', @equivalentId)

SET @equivalentId = 'c2eaee82-e8e5-4333-98f1-c3114e9329f4'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Word', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Synonyms', @equivalentId)

SET @equivalentId = 'fa2990eb-3997-4990-b1a3-78c37d4ec63c'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('xray', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('x-ray', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('x ray', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('mri', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('medical resonance imaging', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('imaging', @equivalentId)

SET @equivalentId = '8a7c07b9-b90f-434d-b9af-33faf2d0c73f'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('builder', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('framer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('carpenter', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('charpentier', @equivalentId)

SET @equivalentId = '46eaef47-71c7-4b47-8fe7-1b5356f95b4a'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('bricklayer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('brick layer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('bricky', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('brickie', @equivalentId)

SET @equivalentId = '47d743f6-4658-4fcb-bd3b-2ff2eb50b7dc'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('pe', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('physical education', @equivalentId)

SET @equivalentId = 'aa02242d-0804-4d01-a2dc-ec0f51e23d09'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('program', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('programme', @equivalentId)

SET @equivalentId = 'fec08ccd-9780-494d-8827-3d39a0027e57'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('expert', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('guru', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('specialist', @equivalentId)

SET @equivalentId = '6e1663a7-97a2-4259-be3c-34dd843ed801'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('butcher', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('butchery', @equivalentId)

SET @equivalentId = 'f0eb1cd1-1583-4027-aa93-e2a184bfbe58'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('hvac', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('heating ventilation air conditioning', @equivalentId)

SET @equivalentId = '32ff4129-26d9-4ddc-997e-99b9dfc2e5af'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('air con', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('air conditioning', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('aircon', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('airconditioning', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('air conditioner', @equivalentId)

SET @equivalentId = '92c488e1-63b2-49e3-87b8-160d921deaca'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('exec', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('executive', @equivalentId)

SET @equivalentId = '4058aaf7-e151-4519-913b-2c2c9f3296c8'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('apn', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('australian provincial news', @equivalentId)

SET @equivalentId = 'a9f7a17d-0730-4d30-a3a5-dbe823269ab1'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('student', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('undergrad', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('under grad', @equivalentId)

SET @equivalentId = '1dae70bd-26ee-458f-967b-933e8273a372'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('grad', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('graduate', @equivalentId)

SET @equivalentId = '1cc2c3a0-0a23-4dba-9b17-74f731aed024'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('aqtf', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('australian quality and training frameworks', @equivalentId)

SET @equivalentId = '0ac3cca7-1152-4cf0-b384-1c84abc7f982'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('rto', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('registered training organisation', @equivalentId)

SET @equivalentId = '884f17c4-c194-405b-acdb-9f7483135786'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('organisation', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('organization', @equivalentId)

SET @equivalentId = '9fb3a460-e498-4f84-a291-f554134ef9d2'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('reg', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('regd', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('registered', @equivalentId)

SET @equivalentId = '0fb242c7-433f-40bd-9eb4-d6861da24656'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('nlp', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('neuro linguistic programming', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('neurolinguistic programming', @equivalentId)

SET @equivalentId = 'e35fedae-be51-42f8-a0d8-6a6496cc35fc'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('bcg', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('boston consulting group', @equivalentId)

SET @equivalentId = 'd4acfda4-bc42-4a39-9717-60126827d1ef'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('mobil', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('exon mobil', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('exonmobil', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('exxon', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('exxonmobil', @equivalentId)

SET @equivalentId = '6cb31cfb-0ed0-4de4-8b5b-4e302853d6a8'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('bp', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('brittish petroleum', @equivalentId)

SET @equivalentId = '58ea128c-32b7-4915-b650-6599cca819b3'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('aicd', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('austrlalian institute of company directors', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('maicd', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('faicd', @equivalentId)

SET @equivalentId = '711b77fb-9a3c-48c6-bdcd-f707a89dffbc'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('circa', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('around', @equivalentId)

SET @equivalentId = '17555d11-1885-41eb-84b9-35790467066b'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('afp', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('australian federal police', @equivalentId)

SET @equivalentId = 'a2fdcab4-c125-4c20-9b3d-99a7181831bf'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('police officer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('policeofficer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('policeman', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('police man', @equivalentId)

SET @equivalentId = '1fa59d1e-9f67-4425-9a54-ebf0ffd9fe8f'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('asic', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('australian securities and investment commission', @equivalentId)

SET @equivalentId = '13440c86-5fe6-4ecd-a8c3-ca9dba0c7d09'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('jv', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('j v', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('joint venture', @equivalentId)

SET @equivalentId = '24605cee-03f7-4dc4-b4e4-103757788efe'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('uk', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('united kingdom', @equivalentId)

SET @equivalentId = '1bb53dda-3a81-4ba1-8bee-a56a12f441b1'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('pwc', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('price waterhouse coopers', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('pricewaterhouse coopers', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('price water house coopers', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('coopers and lybrand', @equivalentId)

SET @equivalentId = 'b662bf87-2fdd-48d7-a8a1-f2f998fcb2b2'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('deloitte', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('deloitte touche', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('deloitte touche tohmatsu', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Duesburys', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('deloittetouche', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('deloittetouchetohmatsu', @equivalentId)

SET @equivalentId = 'ce03f881-5ba9-43a0-903e-c3d2d43d7453'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('ey', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('e&y', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('ernst and young', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('ernst & young', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('ernst&young', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('ernst & whinney', @equivalentId)

SET @equivalentId = 'dc467e71-98f2-42f6-8fde-616b4a1be879'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('kpmg', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('hungerfords', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('peat marwick', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('peatmarwick', @equivalentId)

SET @equivalentId = 'e35c572f-e061-4b41-9d15-b33ff811a29d'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('csl', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Commonwealth Serum Laboratories', @equivalentId)

SET @equivalentId = '93358793-1dad-4132-8fc3-b780fa46f778'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('eds', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('electronic data systems', @equivalentId)

SET @equivalentId = '489059a1-4a0c-4c91-8842-d9387f48181e'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('gsk', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('glaxo smith kline', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('glaxosmithkline', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('glaxo', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('glaxo smithkline', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('glaxo & smithkline', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('glaxo and smithkline', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('glaxo wellcome', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('glaxowellcome', @equivalentId)

SET @equivalentId = '1360fcf2-001c-4f13-9578-7be5ec6cd052'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('astrazeneca', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('astra zeneca', @equivalentId)

SET @equivalentId = '5674aaaa-6dac-4082-b7b6-19414d04a720'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('julia ross', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('juliaross', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('ross human directions', @equivalentId)

SET @equivalentId = '2c0c3e96-ace4-4300-a74e-3869cee82b71'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('ray white', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('raywhite', @equivalentId)

SET @equivalentId = '960f9e33-7dd5-4beb-83dc-462f17c2d623'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('hocking stuart', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('hockingstuart', @equivalentId)

SET @equivalentId = '38c60527-49b2-4f32-a456-f90874c40ac6'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('barry plant', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('barryplant', @equivalentId)

SET @equivalentId = '38f42f40-0038-4f00-b6c4-ebebc336fa6c'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('tradelink', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('trade link', @equivalentId)

SET @equivalentId = '26586190-9c36-4aaf-a27f-0a091cd423c6'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('lockwood', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('lock wood', @equivalentId)

SET @equivalentId = 'a13ef5cd-7ac5-4e74-88a6-c862c2beb242'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('procter and gamble', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('p&g', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('procter & gamble', @equivalentId)

SET @equivalentId = 'd09020ce-fad7-4948-a3e8-8b138a6433c9'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('m&a', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('mergers and acquisitions', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('mergers & acquisitions', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('mergers&acquisitions', @equivalentId)

SET @equivalentId = '2bb96516-4b2d-466d-8d91-9b152a8bfa95'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('arab', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('arabic', @equivalentId)

SET @equivalentId = '5d5e4dc4-d968-4050-86ed-072a319ea13e'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('tac', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('transport accident commission', @equivalentId)

SET @equivalentId = '1e57c0f7-9167-40e8-bfb4-21c47e235b8a'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('tax', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('taxation', @equivalentId)

SET @equivalentId = '9a972d44-2375-4474-a689-10c708f441a2'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('interor design', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('interior designer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('interior decorator', @equivalentId)

SET @equivalentId = 'd811adba-e8eb-4dfc-900c-dafd1b0a2fe6'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('reporter', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('journalist', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('journalism', @equivalentId)

SET @equivalentId = 'c4addfe4-95e9-43a4-ab73-143e394a9d6f'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('actuary', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('actuarial', @equivalentId)

SET @equivalentId = '99178e08-5ea3-4df7-8be4-997930b3ea05'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('tresury', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('treasury', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('treasurer', @equivalentId)

SET @equivalentId = '67ccc14b-ba69-4d9a-af94-f95dc7678f6e'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('call centre', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('call center', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('contact centre', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('callcentre', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('callcenter', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('contactcentre', @equivalentId)

SET @equivalentId = '8d2fec4d-6989-44bc-bf94-605a50b95efe'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('cold call', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('cold calling', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('outbound', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('out bound', @equivalentId)

SET @equivalentId = '15e1c7a4-d5e4-4b6a-8bbf-6900f4e651cc'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('worker', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('officer', @equivalentId)

SET @equivalentId = '8e476108-0de2-4c4e-afe4-653bbf7422ab'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('youth', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('adolescent', @equivalentId)

SET @equivalentId = '5719a433-e2be-44c4-b8ba-1f633809c0a4'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('family services', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('community services', @equivalentId)

SET @equivalentId = '06ebb9d6-479b-4e4e-9d53-7269a1ce437f'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('apprentice', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('apprenticeship', @equivalentId)

SET @equivalentId = '101cc191-2f67-4789-b927-fa2cd4947772'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('kinder', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('kindergarten', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('kinda', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('childcare centre', @equivalentId)

SET @equivalentId = 'dac9c6fc-7a43-4f85-8c51-27fc110c9a45'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('centre', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('center', @equivalentId)

SET @equivalentId = 'd4728f6f-6060-43b7-82a9-617f3acf7903'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('child', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('children', @equivalentId)

SET @equivalentId = '087716f7-7d4c-42ab-aee8-992ccb103146'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('educator', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('tutor', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('trainer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('instructor', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('trainor', @equivalentId)

SET @equivalentId = '617cdd2b-b771-4719-9b34-7affad2a005a'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('waste water', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('wastewater', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('storm water', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('stormwater', @equivalentId)

SET @equivalentId = '114b3df5-20a8-4af3-91eb-40e058728c89'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('hydrology', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('hydrologist', @equivalentId)

SET @equivalentId = 'f17b92c8-02af-4078-87b1-08c576f8dd08'

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

SET @equivalentId = '31afe3cb-8e46-4a4b-8533-70339aec6af6'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('planning clerk', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('planning assistant', @equivalentId)

SET @equivalentId = '4d75fc88-9ed8-497f-ab79-7430ac20aea8'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('building inspector', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('asset inspector', @equivalentId)

SET @equivalentId = 'fb7a388c-8369-41c5-90a1-3f328dbe55f2'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('town', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('urban', @equivalentId)

SET @equivalentId = '9f10c85c-6a93-4f1f-b912-d6391fc12719'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('hairdressor', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('hair stylist', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('hairdresser', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('hair and makeup', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('hair dresser', @equivalentId)

SET @equivalentId = '460cb600-44e0-4f16-bbe9-381051e55ade'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('footware', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('foot ware', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('footwear', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('foot wear', @equivalentId)

SET @equivalentId = '869f442c-1bff-4bcd-af02-096a3be8bef3'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('make up', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('makeup', @equivalentId)

SET @equivalentId = '0e85b036-9e4e-4f0a-8ad0-e318d1cae5e4'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('uae', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('united arab emirates', @equivalentId)

SET @equivalentId = 'ddb2151e-f481-4348-b456-657fa9d24b6b'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('geo technical', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('geotechnical', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('geo tech', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('geotech', @equivalentId)

SET @equivalentId = '3efdce5a-cedf-418f-9dc8-d83847c7eee3'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('lead', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('head', @equivalentId)

SET @equivalentId = '5e71d6ba-b276-4822-921e-53eadf91f6be'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('rail', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('railway', @equivalentId)

SET @equivalentId = '06a8e62b-c689-442a-b23d-459801796726'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('nurse', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('nursing officer', @equivalentId)

SET @equivalentId = '6c84e6c2-edb8-4006-bddc-0d3cedd3b315'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('defence', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('defense', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('armed forces', @equivalentId)

SET @equivalentId = 'd8c693f8-ecad-48bc-aa17-91f2d7311887'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('air force', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('airforce', @equivalentId)

SET @equivalentId = '69f9f883-05bb-48a4-b794-9249c573a59b'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('aeroport', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('aero port', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('airport', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('air port', @equivalentId)

SET @equivalentId = '86a53454-06f1-425f-9ef2-e5b8eb1b5c4d'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('ammunition', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('munition', @equivalentId)

SET @equivalentId = '70434ed8-4264-4b69-9cb6-a8a58b102ffa'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('cbms', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('central budget management system', @equivalentId)

SET @equivalentId = '7b347e9d-56fe-4672-b727-ea5a536888ea'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('australia post', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('auspost', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('australiapost', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('austpost', @equivalentId)

SET @equivalentId = '41ade71b-d333-4ace-9ced-639109d87b93'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('ict', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('information communication and technology', @equivalentId)

SET @equivalentId = 'ded0c3e7-a3a0-4097-9fce-962896bb99ae'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('aged care', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('agedcare', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('elderly care', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('elderlycare', @equivalentId)

SET @equivalentId = '9c281dd5-a6a8-4586-9b68-bdb653cac1e4'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('disability', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('handicapped', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('handicap', @equivalentId)

SET @equivalentId = '02c98ab2-4368-4be5-bb8b-455acdf327d9'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('hospice', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('palliative', @equivalentId)

SET @equivalentId = 'c6c1deae-7f5f-4401-8680-6ee2fd7ea888'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('chemo', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('chemotherapy', @equivalentId)

SET @equivalentId = '2b8764a2-2f02-4154-b646-4fe32e0b6573'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('en', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('enrolled nurse', @equivalentId)

SET @equivalentId = '6d2f0999-ef62-498f-b15b-b0af2db3761f'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('een', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('endorsed enrolled nurse', @equivalentId)

SET @equivalentId = '94285541-dca1-4d10-b953-f30f4e984784'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('physio', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('physiotherapy', @equivalentId)

SET @equivalentId = 'ac72629d-91a8-42ad-b2d8-56f992c87ea4'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('nutriciantist', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('dietician', @equivalentId)

SET @equivalentId = 'f11930a3-4770-4f8f-8745-40a2934220b2'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('case manager', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('case worker', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('case officer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('inury manager', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('rtw officer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('case coordinator', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('elo', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('employment liaison officer', @equivalentId)

SET @equivalentId = 'cce8733e-d017-4595-92f4-01411c6e2c78'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('occ', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('occupational', @equivalentId)

SET @equivalentId = 'dfb7a2ac-ddee-4f48-9727-532871875695'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('rec', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('recreational', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('recreation', @equivalentId)

SET @equivalentId = '79b035a4-8ea4-4cf9-8ced-81c8ac654368'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('speech therapist', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('speech pathologist', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('language therapist', @equivalentId)

SET @equivalentId = '379d8de6-86ef-426b-acdc-6aa1f01d3aae'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('ambulance', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('paramedic', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('para medic', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('ambulance driver', @equivalentId)

SET @equivalentId = '6e6a3ba2-2908-42c3-bdfd-ccb5ddbfa27f'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('scra', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('society of clinical research associates', @equivalentId)

SET @equivalentId = '0b5b1ae8-5700-49d5-b5a4-f147d5de79b8'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('anaesthetic', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('anaesthetist', @equivalentId)

SET @equivalentId = '34716a27-8760-4d34-bc1b-d1729712fd5f'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('a&e', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('accident and emergency', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('emergency medicine', @equivalentId)

SET @equivalentId = 'dcf516f2-a1b8-4a6b-9490-223fbba8b700'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('midwife', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('mid wife', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('mid wifery', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('midwifery', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('mid wive', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('midwive', @equivalentId)

SET @equivalentId = 'd33d2696-a1f4-48f4-841c-bd657544d0da'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('icu', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('intensive care unit', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('intensive care', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('intensivecare', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('msicu', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('critical care medicine', @equivalentId)

SET @equivalentId = 'fb6896ed-5308-427a-9ba0-15c9c8681b6a'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('picu', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('paedeatric intesive care', @equivalentId)

SET @equivalentId = '2ed7c03a-762a-41af-8223-f8fc7f126f1d'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('nicu', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('neonatal intesive care', @equivalentId)

SET @equivalentId = '7afd5c36-702f-4b99-95af-fdf728a70db7'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('safety officer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('safety inspector', @equivalentId)

SET @equivalentId = 'c7c9b888-a1e2-44a6-a8e3-3e48aa2862e3'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('concierge', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('bell attendant', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('porter', @equivalentId)

SET @equivalentId = 'abbaa078-a03b-4c32-84c6-dda7760fc033'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('grounds man', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('groundsman', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('grounds keeper', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('groundskeeper', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('ground attendant', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('public area attendant', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('green keeper', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('greenkeeper', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('turf manager', @equivalentId)

SET @equivalentId = '25e0e626-0f96-488b-a625-a2ed1c99c2db'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('dish washer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('dishwasher', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('kitchen assistant', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('sandwich hand', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('sandwichhand', @equivalentId)

SET @equivalentId = 'babea598-b7f1-4072-b2ec-496ef91fe549'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('tour guide', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('tour director', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('bus guide', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('tourguide', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('resort guide', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('tour driver', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('tour driver guide', @equivalentId)

SET @equivalentId = '41d5c6f1-1475-4b70-a931-d26a7c0397d1'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('japan', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('japanese', @equivalentId)

SET @equivalentId = 'ca4bb2a6-07fd-41a9-92c5-f761aa50f0c4'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('china', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('chinese', @equivalentId)

SET @equivalentId = 'c8dfb2c3-bbf6-4a69-a78e-f4889a061bfc'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('france', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('french', @equivalentId)

SET @equivalentId = 'b3d42a67-694b-4f10-8167-215e650aca7a'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('germany', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('german', @equivalentId)

SET @equivalentId = '31caf55b-10bd-4233-a39a-5f98b4fd9d8b'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('ir', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('industrial relations', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('workplace relations', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('work place relations', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('employee relations', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('er', @equivalentId)

SET @equivalentId = '0016d59d-6ab6-4605-9817-a46dc633c3e5'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('workplace', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('work place', @equivalentId)

SET @equivalentId = '5ae7de6a-c0b2-4f2d-b1d2-358795584b0f'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('job network', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('jobnetwork', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('job placement', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('jpo', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('centrelink', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('centre link', @equivalentId)

SET @equivalentId = 'bb0a110e-aad1-4f9b-96be-7a02af4f94fe'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('rpo', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('recruitment process outsourcing', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('onsite recruitment', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('hro', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('hr process outsourcing', @equivalentId)

SET @equivalentId = 'b173ef5e-ae1f-4e13-8492-84d66fbcd70e'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('od', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('organisation development', @equivalentId)

SET @equivalentId = 'e785f509-8f9c-450d-8e1d-5fce67bee34f'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('cqi', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('continuous quality improvement', @equivalentId)

SET @equivalentId = '7839470a-c840-4e24-add4-52487d10ab36'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('webmethods', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('web methods', @equivalentId)

SET @equivalentId = 'cf3ee370-c2b2-43d1-971b-6eff72db52b7'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('gis', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('geographic information system', @equivalentId)

SET @equivalentId = 'aadec0fe-9da1-43fe-a6a5-c5f4f9b1b619'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('hyper text', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('hypertext', @equivalentId)

SET @equivalentId = 'dfbe9c2d-4e43-4a8a-8b03-7847bfe668ee'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('3d', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('3 dimensional', @equivalentId)

SET @equivalentId = 'd9e07cb1-cc6f-49a6-9e60-d6a861bbdd81'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('mainframe', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('main frame', @equivalentId)

SET @equivalentId = 'b05b5ffb-8eb5-4c03-9a08-1fc2a666d3f8'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('scrum', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('agile development', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('agile software development', @equivalentId)

SET @equivalentId = 'a2a83c1c-206c-4fe5-920c-64f8557a043c'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('biztalk', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('biz talk', @equivalentId)

SET @equivalentId = 'd86faf27-25ae-4c81-81f1-c9b76ec0efe1'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('database', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('data base', @equivalentId)

SET @equivalentId = '1e1f4eae-d73e-4fd8-bf7b-f44059dc8d5a'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('c#', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('c sharp', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('csharp', @equivalentId)

SET @equivalentId = 'be012216-795e-449f-9867-f772c2061b6e'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('.net', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('dot net', @equivalentId)

SET @equivalentId = 'f0368b2a-5cb0-4350-b435-ab35b3e979ba'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('gui', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('graphic user interface', @equivalentId)

SET @equivalentId = 'a1a6e475-bec9-4110-b5b0-9ff5018d6e9c'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('power builder', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('powerbuilder', @equivalentId)

SET @equivalentId = 'ed0b0e98-bd04-4a30-a2fa-4e7bd1e1c91a'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('cti', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('computer technology integration', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('computerised technology integration', @equivalentId)

SET @equivalentId = '34e87b6c-c2df-4c79-89e1-f10817815fa6'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('soa', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('service oriented architecture', @equivalentId)

SET @equivalentId = '5ae5dfa1-abb6-4db3-a957-394c5550804d'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('iso', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('international standards association', @equivalentId)

SET @equivalentId = 'b3417fe4-e49f-4b34-9b1a-11e0163b99af'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('rem', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('remuneration', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('salary', @equivalentId)

SET @equivalentId = '9436abe0-396b-479a-b2ba-2939159de31f'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('smsf', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('self manager super fund', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('self manager superannuation fund', @equivalentId)

SET @equivalentId = '4a8ef75a-e0fa-4e3f-81e7-6079bd97c758'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('worksafe', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('work safe', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('work cover', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('workcover', @equivalentId)

SET @equivalentId = 'b279ee42-8702-4bd4-b437-f82a4cb5c91e'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('j2ee', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('j2e', @equivalentId)

SET @equivalentId = 'd32a9174-293f-4a97-9f56-8e9bdd0225a0'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('sql', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('structured query language', @equivalentId)

SET @equivalentId = 'b2667944-3d6f-4439-9652-d5fc36e9a78b'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('javascript', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('java script', @equivalentId)

SET @equivalentId = 'c952f0a7-75a4-48dc-9469-186efaae18fc'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('4gl', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('4th general language', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('forth general language', @equivalentId)

SET @equivalentId = 'fcac847f-9cdf-47b4-bc18-dd1759613ed1'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('sqlserver', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('sql server', @equivalentId)

SET @equivalentId = 'f5f6e1d2-2c23-4263-a5f4-a5e8db757435'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('datawarehouse', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('data warehouse', @equivalentId)

SET @equivalentId = '0880d7ce-34f5-4338-8f16-f664f2705773'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('gl', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('general ledger', @equivalentId)

SET @equivalentId = '4a97bc69-e635-4be2-8c75-b4c897e84790'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('ldap', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('lightweight directory access protocol', @equivalentId)

SET @equivalentId = 'acd5a8b9-88c8-4546-89f4-f729502921fb'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('records clerk', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('records officer', @equivalentId)

SET @equivalentId = 'd6e6bc9e-4ff0-4adc-baf4-22787191f0eb'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('inhouse', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('in house', @equivalentId)

SET @equivalentId = '224043c9-7a25-4847-815d-c62659279f26'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('cnc', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('computer numeric conrol', @equivalentId)

SET @equivalentId = '4a869d73-d085-46a9-b3e5-c2719c811e6e'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('toolmaker', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('tool maker', @equivalentId)

SET @equivalentId = '8ddd64ff-d68a-4cac-aa16-e628fd1fe4e2'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('datacentre', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('data centre', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('data center', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('datacenter', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('hosting center', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('hosting centre', @equivalentId)

SET @equivalentId = '2087ed95-f31f-4f09-b8b1-0680a32fa203'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('mysql', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('my sql', @equivalentId)

SET @equivalentId = '1d0a4865-0d9b-4f78-a996-6a6201f3f3bc'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('as400', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('as 400', @equivalentId)

SET @equivalentId = '1851ac71-e2ef-45a6-b02d-aa0538c87810'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('ccnp', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('cisco certified network professional', @equivalentId)

SET @equivalentId = '1c4bdd17-89b1-4070-bf15-df860f2c86d7'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('ccna', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('cisco certified network associate', @equivalentId)

SET @equivalentId = 'aa6a6066-53b6-4eee-bf4b-5ce70b28011b'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('msce', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('microsoft certified systems engineer', @equivalentId)

SET @equivalentId = '69fcefb1-dedd-4752-bad5-3239d4e97883'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('noc', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('netwrok oprations centre', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('network operations center', @equivalentId)

SET @equivalentId = '15bd269c-b820-4a76-9fe5-580a1e195b48'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('san', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('storage area network', @equivalentId)

SET @equivalentId = '166dd354-7237-4f5a-9318-f292a981db97'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('pstn', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('public switched telephone network', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('public switched telephony network', @equivalentId)

SET @equivalentId = 'd86604a6-bb15-452f-8c25-2302b5bc06d6'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('fr', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('frame relay', @equivalentId)

SET @equivalentId = 'dbd7f859-64b5-473d-baf0-0e7843a4fe9a'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('level 1', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('level1', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('1st level', @equivalentId)

SET @equivalentId = '4d4208c4-65a6-47b1-8bae-870fdb224475'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('level 2', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('level2', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('2nd level', @equivalentId)

SET @equivalentId = '178ea5e5-4ac7-4053-aa6f-5582fa60fcc9'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('level 3', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('level3', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('3rd level', @equivalentId)

SET @equivalentId = '491b091e-f04c-421a-9523-a61e0b5531fe'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('desktop', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('desk top', @equivalentId)

SET @equivalentId = 'de82f3ff-6972-434f-ad34-b011d730f703'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('pc', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('personal computer', @equivalentId)

SET @equivalentId = '2c34c7d4-7878-4b7c-af94-231aae0b351a'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('blackbelt', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('black belt', @equivalentId)

SET @equivalentId = '96389e24-1e10-45a9-a244-10688d5796df'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('checkpoint', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('check point', @equivalentId)

SET @equivalentId = 'd32b9e4b-024f-431b-9b0f-76292e5741da'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('firewall', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('fire wall', @equivalentId)

SET @equivalentId = 'eb63c467-d37e-46c9-beb4-116b6e61138e'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('pki', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('public key infrastructure', @equivalentId)

SET @equivalentId = '50efeca3-62b0-4b84-be28-598df0087db2'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('ipsec', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('ip sec', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('ip security', @equivalentId)

SET @equivalentId = '95f99b37-cdf5-4029-821e-1a5fdc43e911'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('coax', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('coaxial', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('co ax', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('co axial', @equivalentId)

SET @equivalentId = 'd13adf90-ba55-4c07-b042-821d43c58e11'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('fibre', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('fiber', @equivalentId)

SET @equivalentId = '08517f8f-1312-444e-a317-bccf2660972f'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('itil', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('information technology infrastructure library', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('it infrastructure library', @equivalentId)

SET @equivalentId = 'e439648a-451d-429b-a1fd-20b0a77ae83a'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('pipeline', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('pipe line', @equivalentId)

SET @equivalentId = '59fd462d-e4bd-4e04-bd43-7a4281948c7d'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('realestate', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('real estate', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('property', @equivalentId)

SET @equivalentId = 'b85f2659-4ac3-4523-beea-3ac5cd123a63'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('facility', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('facilities', @equivalentId)

SET @equivalentId = '5976556c-2eaa-4fd6-a34e-388bd0862999'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('stocktake', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('stock take', @equivalentId)

SET @equivalentId = '2f0df8de-1e3e-4c43-b8b0-b951da29e032'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('mobile phone', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('mobile telephony', @equivalentId)

SET @equivalentId = '8996d28b-99be-4f7f-9f51-8aebbf9948be'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('refridgeration technician', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('refridgeration mechanic', @equivalentId)

SET @equivalentId = 'a5228058-8b60-45f2-ae87-62418e754a86'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('cabinetmaker', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('cabinet maker', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('furniture maker', @equivalentId)

SET @equivalentId = 'daa1bfe1-82a7-455f-94f3-77373fa7bfdd'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('hv', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('high voltage', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('highvoltage', @equivalentId)

SET @equivalentId = '8a9300f6-5fce-443d-b8eb-eaba18e12e2b'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('landscaper', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('landscape gardner', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('landscape architect', @equivalentId)

SET @equivalentId = '2c91a916-5f5d-4070-b5fb-a4e3158916c4'

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

SET @equivalentId = 'c48edee9-2c7d-4515-98e6-728329663976'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('plumber', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('gas fitter', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('plombier', @equivalentId)

SET @equivalentId = '01825c8e-fcc4-41ad-b0e4-867ef127ec71'

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

SET @equivalentId = '9d0df6a3-e80e-465b-b7f5-1a7b0a97af8f'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('painter', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('house painter', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('painting worker', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('painting and decorating', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('painting & decorating', @equivalentId)

SET @equivalentId = 'd970e2fa-4375-4de2-8c0b-515f76fc43da'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('spray painter', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('spraypainter', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('auto painter', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('car painter', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('paint line operator', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('paint shop operator', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('paintshop operator', @equivalentId)

SET @equivalentId = 'd4da3e46-94d9-4059-9c25-0bfa043c416c'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('panel beater', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('panelbeater', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('automotive repairer', @equivalentId)

SET @equivalentId = 'd19de5fe-a308-4566-93eb-5bdd559b6a3d'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('dry cleaner', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('drycleaner', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('drycleaning', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('dry cleaning', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('dri cleaner', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('dricleaner', @equivalentId)

SET @equivalentId = 'c94244a9-7e34-42f4-ab7e-550cfce088b1'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('security installer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('security technician', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('signwriter/painter', @equivalentId)

SET @equivalentId = '76747f1f-1d12-4d2b-ae70-b98376e1d697'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('supplychain', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('supply chain', @equivalentId)

SET @equivalentId = '56076f46-0570-441a-b247-df2845ff587c'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('marine', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('maritime', @equivalentId)

SET @equivalentId = '0582aea6-869b-464d-816e-0b371861525e'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('div1', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('division 1', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('div 1', @equivalentId)

SET @equivalentId = '7aea998c-37c8-436d-a9a7-64933b5b0931'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('div2', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('division 2', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('div 2', @equivalentId)

SET @equivalentId = '6bb210ff-e68d-4611-9c22-c009c1e42419'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('div3', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('division 3', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('div 3', @equivalentId)

SET @equivalentId = 'b3d18724-7d67-4e9c-be42-2b6a4f0d46c8'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('marine engineer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('maritime engineer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('marine technician', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('maritime technician', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('tugboat engineer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('tug boat engineer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('ship engineer', @equivalentId)

SET @equivalentId = 'b71ca7fe-538b-4026-b418-70ca7774026b'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('airtraffic controller', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('air traffic controller', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('air traffic services officer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('air traffic', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('airtraffic', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('air traffic scheduler', @equivalentId)

SET @equivalentId = '5eed057b-9b9c-4c9b-86f2-1ff93f5dd148'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('baggage handler', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('baggagehandler', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('luggage handler', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('baggage coordinator', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('ground handler', @equivalentId)

SET @equivalentId = 'b3174890-f625-4d39-94fd-b838e2e4c230'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('customs officer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('quarantine officer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('customs clerk', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('customs controller', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('customs supervisor', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('customs inspector', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('quarantine inspector', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('quarantine inspection officer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('quarantine clerk', @equivalentId)

SET @equivalentId = 'e1115882-c2ad-49d0-b066-d5ea80fd2950'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('deckhand', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('deck hand', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('integrated rating', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('able bodied seaman', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('greaser', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('ab', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('general purpose hand', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('gp hand', @equivalentId)

SET @equivalentId = '7fc6ba13-e103-4a5a-8ad1-a8cb189bcbe8'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('skipper', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('master v', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('master 5', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('master 4', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('master4', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('master5', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('masterv', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('coxswain', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('ship captain', @equivalentId)

SET @equivalentId = 'cccc7211-235d-444d-8b4e-88103c1694d3'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('cabin crew', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('cabincrew', @equivalentId)

SET @equivalentId = '50f3e20e-8277-4711-8dd0-672b5895437a'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('steward', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('caterer', @equivalentId)

SET @equivalentId = '83967f74-f796-49ed-9588-a0b7bdc9ecba'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('course super', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('course superintendant', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('golfcourse superintendant', @equivalentId)

SET @equivalentId = 'f2d54814-6ec3-4fee-97c5-524af5f1c793'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('golf pro', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('golf professional', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('golfpro', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('pga professional', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('golf coach', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('golfing professional', @equivalentId)

SET @equivalentId = '9029975d-536a-41e1-8855-0890500b7b8c'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('pga', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('professional golfers association', @equivalentId)

SET @equivalentId = '3c2ad9d2-eb1f-46ac-81b6-93e7ab36ad2a'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('golf shop', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('pro shop', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('proshop', @equivalentId)

SET @equivalentId = '6a0fdd3f-b1a3-4973-bf12-25d59f253a2c'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('golf manager', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('golfclub manager', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('golf club manager', @equivalentId)

SET @equivalentId = '5c788c22-7a1f-4e77-83fa-0bc6e022e16e'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('peoplesoft', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('people soft', @equivalentId)

SET @equivalentId = '475b7bcd-081f-4265-982d-c8a3407ca1ec'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('ps146', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('ps 146', @equivalentId)

SET @equivalentId = '3dc9a1b1-978f-4d63-890d-bc4c2ffbd440'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('b2b', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('b 2 b', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('business to business', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('business 2 business', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('business2busines', @equivalentId)

SET @equivalentId = 'd58eed89-07d4-451d-b909-c32f75196e4a'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('b2c', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('b 2 c', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('business to consumer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('business 2 consumer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('business2consumer', @equivalentId)

SET @equivalentId = '86fe3545-35ed-4b66-a163-54c1c3c5d3d5'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('maternal health', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('mothercraft', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('mother craft', @equivalentId)

SET @equivalentId = '1ddd0067-7487-4ccf-be11-f80bd1d2d8a8'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('hk', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('h k', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('hongkong', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('hong kong', @equivalentId)

SET @equivalentId = '09aeaa50-96ba-45b9-8d16-206af4144f23'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('png', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('papua new guinea', @equivalentId)

SET @equivalentId = '96c2b838-c610-4987-aa19-c1860cc93610'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('heathfood', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('health food', @equivalentId)

SET @equivalentId = 'f3dbf73f-ea26-4c9d-abfd-58adea15a4cf'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('hifi', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('hi fi', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('high fidelity', @equivalentId)

SET @equivalentId = 'a0c19f50-543d-4f99-ad43-2bc6b33567f2'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('primus', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('iprimus', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('primus telecom', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('primus telecommunications', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('primustelecom', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('primustel', @equivalentId)

SET @equivalentId = '0cdfb718-eccd-4c9a-ac41-f0243c19dd72'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('optus', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('singtel', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('singteloptus', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('singtel optus', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('singapore telecom', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('singapore telecommunications', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('sing tel', @equivalentId)

SET @equivalentId = '18b4a6fa-10dd-4412-9d41-d4fb37a41de4'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('melbourne uni', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('melbourneuni', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('melbourne university', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('university of melbourne', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('universityofmelbourne', @equivalentId)

SET @equivalentId = '880a2765-4a92-4b85-bee1-e08e1b717466'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('sydney uni', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('sydneyuni', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('sydney university', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('university of sydney', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('universityofsydney', @equivalentId)

SET @equivalentId = 'fcc740b6-6075-4b90-877f-9f70d0536768'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('uni of queensland', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('university of queensland', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('queensland uni', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('qld uni', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('qld university', @equivalentId)

SET @equivalentId = '9034f223-08ba-4a8d-bee0-bbb3015ae22f'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('acu', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('australian catholic university', @equivalentId)

SET @equivalentId = 'b679b6fa-2ad1-406a-b36d-2559352fc3de'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('bond uni', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('bond university', @equivalentId)

SET @equivalentId = '862f834d-cbb1-4fd1-bde6-abde279fd8ec'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('cdu', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('charles darwin university', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('charles darwin uni', @equivalentId)

SET @equivalentId = '5d2d153e-96f8-4ee9-883f-5241418920d0'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('csu', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('charles sturt university', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('charles sturt un', @equivalentId)

SET @equivalentId = '1cd3fd81-3aa0-4517-97bf-1f91662ae6bf'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('ecu', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('edith cowan university', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('edith cowan uni', @equivalentId)

SET @equivalentId = 'a704ff54-3e72-4b0a-a0b0-0ad2497d4dc9'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('jcu', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('james cook university', @equivalentId)

SET @equivalentId = 'c6960fa4-f740-44bc-98b4-169863c4ad1c'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('latrobe', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('la trobe', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('latrobe university', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('latrobe uni', @equivalentId)

SET @equivalentId = '2bc677ce-e8da-4241-b77d-ba2465d1c54c'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('scu', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('southern cross university', @equivalentId)

SET @equivalentId = 'e56d8a04-9cef-4e57-ba0c-88dcfa02a4f9'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('swinburn uni', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('swinburn university', @equivalentId)

SET @equivalentId = '154cf42b-847e-4e69-a8e1-8b481194a67b'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('university of ballarat', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('ballarat uni', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('ballarat university', @equivalentId)

SET @equivalentId = '89281cf4-5a02-4e2a-9991-937aa23185ef'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('university of canberra', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('canberra uni', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('canberra university', @equivalentId)

SET @equivalentId = '5081b355-e616-4faa-aea4-ceeadf5eb0c9'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('une', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('university of new england', @equivalentId)

SET @equivalentId = 'e27b5918-88c9-4505-a157-82723fd72714'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('university of newcastle', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('newcastle uni', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('newcastle university', @equivalentId)

SET @equivalentId = '72b2f453-e99a-4c53-adb9-a5b6b879eacd'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('unda', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('university of notre dame australia', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('university of notredame australia', @equivalentId)

SET @equivalentId = '7b20871f-d301-4df7-9f39-f02409a6b7fe'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('unisa', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('iniversity of south australia', @equivalentId)

SET @equivalentId = 'f62dbab5-ca58-43ac-9991-7524863e6c92'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('usq', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('university of southern queensland', @equivalentId)

SET @equivalentId = '9e8dc6b0-c55c-4ed1-8762-787774078450'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('university of tasmania', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('uni of tasmania', @equivalentId)

SET @equivalentId = '8461a40a-44e2-4d5a-8ac0-714fe5ac826c'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('usc', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('university of the sunshine coast', @equivalentId)

SET @equivalentId = 'f003f7db-3ea0-4cd6-9036-11afd1c05362'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('uow', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('university of wollongong', @equivalentId)

SET @equivalentId = '151c3414-b206-4f01-8a82-8861abb1fa43'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('vu', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('victoria university', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('victoria uni', @equivalentId)

SET @equivalentId = '74fb3b52-225f-4255-8edf-013b90b68571'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('goldcoast', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('gold coast', @equivalentId)

SET @equivalentId = 'f7758f4c-c71c-4601-ae3e-7cf1eabbd457'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('sunshinecoast', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('sunshine coast', @equivalentId)

SET @equivalentId = '5a6fa870-603c-41be-b0b3-16bfca9d6455'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('tassie', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('tasmania', @equivalentId)

SET @equivalentId = '6caeda92-1728-4bbc-9ad4-290d3e908a51'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('mbs', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('melbourne business school', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('m b s', @equivalentId)

SET @equivalentId = 'ec2d9875-9cc3-4abc-9060-b99513ddd828'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('news ltd', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('newsltd', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('news limited', @equivalentId)

SET @equivalentId = '51199d0a-56f0-47e1-9529-72f7dc9f0ec9'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('qut', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('queensland university of technology', @equivalentId)

SET @equivalentId = 'e1a0201b-6092-47f7-8963-2779f2af7ae8'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('rmit', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('royal melbourne institute of technology', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('rmit university', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('rmit uni', @equivalentId)

SET @equivalentId = '1491844e-87b0-4df9-88d9-6e8c607ad919'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('inbound', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('in bound', @equivalentId)

SET @equivalentId = 'de6bb9da-6f7d-424a-8ff3-0c2e6b4a90b4'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('adelaide university', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('university of adelaide', @equivalentId)

SET @equivalentId = 'f6c8561e-062d-4539-8b92-ba195bf850c5'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('anu', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('a n u', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('australian national univeristy', @equivalentId)

SET @equivalentId = '6dec48fe-894d-4229-b5cd-5c5f242b0f74'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('unsw', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('university of new south wales', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('u nsw', @equivalentId)

SET @equivalentId = 'ba81d536-340d-43d4-b9a5-62eec2551ac4'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('uws', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('university of western sydney', @equivalentId)

SET @equivalentId = '0c30e85e-3b06-4fb8-bd1e-36bf264b9f34'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('monash uni', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('monash university', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('monashuni', @equivalentId)

SET @equivalentId = 'bebf41cd-0c61-4b8f-b17c-a15a70dc9c29'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('deakin uni', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('deakin university', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('deakinuni', @equivalentId)

SET @equivalentId = 'a7886e1b-bef8-4241-ba02-31c6c38d863e'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('college', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('school', @equivalentId)

SET @equivalentId = 'ecd5215a-dc85-4865-bb44-3d949a386d61'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('baccalaureate', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('international baccalaureate', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('ib', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('bac', @equivalentId)

SET @equivalentId = 'aff2636c-99b0-4753-8f9b-921f3c1c6b5f'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('special education', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('special ed', @equivalentId)

SET @equivalentId = 'e0038a73-3684-4d9c-aedd-21faa4315a44'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('vce', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('victorian certificate of education', @equivalentId)

SET @equivalentId = '12b01660-01c8-4e4a-bba8-010c6dde01e0'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('hsc', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('higher school certificate', @equivalentId)

SET @equivalentId = '5128b2de-9824-48ea-b6bb-e7782b0ade1d'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('young and rubicam', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Y&R', @equivalentId)

SET @equivalentId = 'fbe1079a-b6af-4fa8-a537-dfebef428001'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('officeworks', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('office works', @equivalentId)

SET @equivalentId = '510ca7b5-1674-45d5-87e9-fc48f626e640'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('coles', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('coles group', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('colesmyer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('coles myer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('cml', @equivalentId)

SET @equivalentId = '5b17c22a-5140-45c6-a5a2-6b3fe34e9086'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('wool worths', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('woolworths', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('safeway', @equivalentId)

SET @equivalentId = '47b50b50-23d2-4bc8-b3b5-d90c6b308a43'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('mc donalds', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('mcdonalds', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('macdonalds', @equivalentId)

SET @equivalentId = 'f2bb119f-8752-4522-81e8-255103ce9446'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('spotless', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('spotless catering', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('spotless group', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('spotless services', @equivalentId)

SET @equivalentId = 'bba06fd7-a8b1-4006-9a41-859194312629'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('ramsayhealth', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('ramsay health', @equivalentId)

SET @equivalentId = 'f7eaaace-644f-4d1d-b81f-51da36c65342'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('worley parsons', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('worleyparsons', @equivalentId)

SET @equivalentId = '241106cb-87b6-4f3c-a494-f889e43a1a1d'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('racv', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('royal automobile club of victoria', @equivalentId)

SET @equivalentId = '6fe4dc69-374e-478a-a29c-3c9b982495cf'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('mcc', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('melbourne cricket club', @equivalentId)

SET @equivalentId = '4ebbed1b-9c1a-4ec8-b1b9-a54cc278295d'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('mcg', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('melbourne cricket ground', @equivalentId)

SET @equivalentId = 'de642b59-fe09-4516-b1f0-81035af683af'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('scg', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('sydney cricket ground', @equivalentId)

SET @equivalentId = 'c4f8df4a-b8a8-4b1e-a46c-888d8b62b6db'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('cocacola', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('coca cola', @equivalentId)

SET @equivalentId = 'e61266cb-e75d-4653-957f-c23050982bbb'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('blue scope', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('bluescope', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('bluescope steel', @equivalentId)

SET @equivalentId = '6bf177b3-9596-4a65-9268-1796328afaf7'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('salvos', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('salvation army', @equivalentId)

SET @equivalentId = '01b48d70-2a94-44d5-9d54-e1fb31598499'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('gm holden', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('beneral motors holden', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('general motors', @equivalentId)

SET @equivalentId = '18f9bf07-87d8-4ade-855a-3d45902730c0'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('justjeans', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('just jeans', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('just group', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('justgroup', @equivalentId)

SET @equivalentId = 'b9d37bc9-cc0b-4b2b-ac84-34235930ce9a'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('pbl', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('publishing and broadcasting', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('publishing & broadcasting', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('publishing&broadcasting', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('pbl ltd', @equivalentId)

SET @equivalentId = 'cdfcf87f-b1c0-4c91-b4ff-c2958dc386ab'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('macq bank', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('macquarie bank', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('macquarrie bank', @equivalentId)

SET @equivalentId = '66cb9bef-8482-4851-8766-5ea3c6f52dbb'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('virgin blue', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('virginblue', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('virginblue.com.au', @equivalentId)

SET @equivalentId = '39d34ea4-d0e9-41d4-a32d-3d394d253dba'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('mitre10', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('mitre 10', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('mitre ten', @equivalentId)

SET @equivalentId = '6eccba4e-de14-4a4d-bad5-a15068d1e11b'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('starcity', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('star city', @equivalentId)

SET @equivalentId = '875266ed-8a26-4b57-90ff-bbb0b8e1130b'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('dicksmith', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('dick smith', @equivalentId)

SET @equivalentId = 'd2a30591-1f48-4675-b159-f6d6cf177046'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('seek', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('seek.com.au', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('seek.com', @equivalentId)

SET @equivalentId = 'a2c5e56d-c7b7-4277-a0a4-32ad3f1d963f'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('yahoo', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('yahoo7', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('yahoo.com.au', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('yahoo7.com.au', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('yahoo 7', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('yahoo7!', @equivalentId)

SET @equivalentId = 'c43c009d-6bc2-4309-948f-1cca75d0101f'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('armaguard', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('arma guard', @equivalentId)

SET @equivalentId = 'ae9f6e9f-5f09-4c32-b390-4942db01fcd2'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('ge', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('general electric', @equivalentId)

SET @equivalentId = 'd1336797-1c8a-408a-b2da-69f139ff15ff'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('talent2', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('talent 2', @equivalentId)

SET @equivalentId = '85b3ba3d-d4a1-42fd-94df-29e284a1d8e2'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('chandler macleod', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('chandlermacleod', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('cmg', @equivalentId)

SET @equivalentId = '583ce03e-d6ca-4455-abbb-93711797fdf7'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('lloyd morgan', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('lloydmorgan', @equivalentId)

SET @equivalentId = '9e534607-0d70-43b3-be7e-1eb9884c1270'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('m&b', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('morgan and banks', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('morgan&banks', @equivalentId)

SET @equivalentId = 'ce71b3dd-cdf3-42e3-a590-e71094687967'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('maanz', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('marketing association of australia and new zealand', @equivalentId)

SET @equivalentId = 'afeee09a-6956-4124-8968-9e1d006d3bc2'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('mia', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('marketing institute of austrlalia', @equivalentId)

SET @equivalentId = 'ef1903d6-14e6-49e8-9c54-e3f3089d2e36'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('aempe', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('association of marine and power engineers', @equivalentId)

SET @equivalentId = 'c807c145-8626-469c-9877-d5a2cfe0c07f'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('vacc', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('victorian automotive chamber of commerce', @equivalentId)

SET @equivalentId = '4dc60ea2-173b-4b51-9064-77d15075f61c'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('sae', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('society of automotive engineers', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('society of automotive engineers australasia', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('saea', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('sae-a', @equivalentId)

SET @equivalentId = 'f2b8752b-7d04-49ee-8794-7098c4b8f868'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('ahri', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('austrlalian human resources institute', @equivalentId)

SET @equivalentId = 'c7ab1fb0-3a03-4069-a0b4-bb5a7acf0457'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('rcsa', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('recruitment and consulting services association', @equivalentId)

SET @equivalentId = '4a807550-9cdc-4852-b0f9-be090bca848b'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('gta', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('group training australia', @equivalentId)

SET @equivalentId = '20c0638f-7b48-4a63-99c0-81002771ef48'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('finsia', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('financial services institute of australiasia', @equivalentId)

SET @equivalentId = '0084aa5b-88d7-4d46-9b2e-886858b68cdb'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('rdns', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('royal district nursing service', @equivalentId)

SET @equivalentId = 'f8b4dce8-9e9b-4a9b-b49c-4108a592c5f7'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('pia', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('planning institute of australia', @equivalentId)

SET @equivalentId = '7cff84fe-1b49-4c1a-a1ad-a26441d54e3d'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('yellowpages', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('yellow pages', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('yellowpages.com.au', @equivalentId)

SET @equivalentId = '7d07e095-68cb-4358-a8ad-85998affe3a4'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('whitepages', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('white pages', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('whitepages.com.au', @equivalentId)

SET @equivalentId = 'b1dc3e07-f2a8-4655-8d7f-0f0191c2d3bb'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Hyperbaric Technician', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('Hyperbaric operator', @equivalentId)

SET @equivalentId = '04d4935d-fa6d-4cd2-97df-e83629d1e753'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('license', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('licence', @equivalentId)

SET @equivalentId = '5231f73a-e1fe-4a8a-9f88-839133bfe579'

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

SET @equivalentId = '8552f485-5d76-4128-84ee-1711577d5811'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('asfa', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('association of superannuation funds of australia', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('superannuation association', @equivalentId)

SET @equivalentId = '1cd89ebd-f065-425f-ac72-1aa704e25d29'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('retailer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('retail company', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('retail organisation', @equivalentId)

SET @equivalentId = 'c799520d-5def-4a20-b366-2f7ec17c61f7'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('valuer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('valuation', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('property valuer', @equivalentId)

SET @equivalentId = '3d77c814-1d38-476a-9e1e-95555555ad61'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('ara', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('australian retailers association', @equivalentId)

SET @equivalentId = '50f9efc4-d01c-4b28-a24c-24505bfcedd6'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('horticulturalist', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('horti culturalist', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('horticulture', @equivalentId)

SET @equivalentId = 'ba730bb1-1a50-4581-b5e7-bd089deac7f9'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('bdouble', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('b double', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('road train', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('roadtrain', @equivalentId)

SET @equivalentId = '7f5f1779-cc6f-46ed-8a3b-1cd28d765b3f'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('crane driver', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('crane operator', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('crane truck driver', @equivalentId)

SET @equivalentId = '1449ea48-7b89-4c24-9017-a00d81cc6e1f'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('rigger', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('dogman', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('dog man', @equivalentId)

SET @equivalentId = '73892769-4f39-4381-8cb8-2aeab86116ca'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('postman', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('post man', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('postie', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('postal officer', @equivalentId)

SET @equivalentId = 'd0dccbfb-6c35-4011-8870-e073944b2121'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('carpark', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('car park', @equivalentId)

SET @equivalentId = '7f58ddd5-0a85-4641-93fa-2cd250da12af'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('carpark attendant', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('carpark operator', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('car park attendant', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('car park operator', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('parking attendant', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('parking staff', @equivalentId)

SET @equivalentId = '14b83717-12d2-45ee-af25-a9b230912d04'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('formwork', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('form work', @equivalentId)

SET @equivalentId = '5ca47576-ec8f-4fee-83db-5487130c6cf9'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('agribusiness', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('agri business', @equivalentId)

SET @equivalentId = '36d55e67-ca37-4d0d-9109-c065016ceb62'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('freelance', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('free lance', @equivalentId)

SET @equivalentId = '3a780502-2a82-4220-86fd-98f50259a6c8'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('voip', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('voice over internet protocol', @equivalentId)

SET @equivalentId = '9689e043-7948-4dea-baf0-02b043d67852'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('phd', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('doctorate', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('doctor of philosophy', @equivalentId)

SET @equivalentId = '014016bc-6d58-4f29-ad77-91ac7a5c0a74'

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
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('aquatic co ordinator', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('aquatic teacher', @equivalentId)

SET @equivalentId = 'fc6f20d7-3936-4469-81a1-24a6108c7791'

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

SET @equivalentId = 'a13cae28-273b-4a0a-8a3b-9fad5a435c5f'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('aquatic coordinator', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('aquatic facility assistant', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('pool operations manager', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('pool operations co ordinator', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('pool operations coordinator', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('pool superintendant', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('pool operations attendant', @equivalentId)

SET @equivalentId = 'f2f73e4e-c84a-4025-a394-161bf7bbe8dc'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('pool builder', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('pool installer', @equivalentId)

SET @equivalentId = '93b5c2a3-d482-413d-96ee-4bbd9381a64e'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('poolman', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('pool man', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('pool cleaner', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('pool technician', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('pool maintenance', @equivalentId)

SET @equivalentId = 'd8021a09-6a97-445d-92b1-72e2da41014f'

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

SET @equivalentId = 'b311fc0a-bca2-4172-ba60-703d50f6e49e'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('recreation centre manager', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('fitness centre manager', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('swim centre manager', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('rec centre manager', @equivalentId)

SET @equivalentId = '55050eeb-127b-4776-b8cb-a11a29486797'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('masseuse', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('massage therapist', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('massage specialist', @equivalentId)

SET @equivalentId = '02600f5a-7c1a-4340-88c0-8fb2f5e668a1'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('cpr', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('cardio pulmonary resuscitation', @equivalentId)

SET @equivalentId = '52195445-9d6a-48cb-bfd7-c45bc767c563'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('environmental', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('sustainability', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('environment', @equivalentId)

SET @equivalentId = 'c245bf33-64e8-4d62-95f3-de9e1750da19'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('vacation', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('holiday', @equivalentId)

SET @equivalentId = '1b03cfe2-8626-47c2-a1a6-ad1b28d00e5c'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('afl', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('australian football league', @equivalentId)

SET @equivalentId = 'fd5b0ae0-9b68-4d85-8d46-a4a05d90a295'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('football', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('footy', @equivalentId)

SET @equivalentId = '5a7a31ad-9032-413a-b7c3-845c01e1f874'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('nrl', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('national rugby league', @equivalentId)

SET @equivalentId = 'bae665a8-caa0-4f0f-a8da-81673445f51d'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('motor sport', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('motorsport', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('car racing', @equivalentId)

SET @equivalentId = '2990040e-3184-471c-ace7-0edd3454bb02'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('ais', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('australian institute of sport', @equivalentId)

SET @equivalentId = '37bf1635-b281-46a0-b69f-54384f4557c6'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('nbl', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('national basketball league', @equivalentId)

SET @equivalentId = '0640ab17-47df-4209-af78-50bdf7e0eeaa'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('nba', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('national basketball association', @equivalentId)

SET @equivalentId = '10e1805c-6a10-4678-9022-8522914da74e'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('aoc', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('australian olympic committee', @equivalentId)

SET @equivalentId = 'd82d1328-1070-4041-93e5-046fc2e11892'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('winter sport', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('wintersport', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('skiing', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('snow skiing', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('snowboarding', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('snow boarding', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('snow sport', @equivalentId)

SET @equivalentId = '9a160ff0-1299-4a40-b019-4045b6edbae5'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('train driver', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('rail driver', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('traindriver', @equivalentId)

SET @equivalentId = '755d5d21-f8be-4b97-ae4c-2e365aef59e8'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('tram driver', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('tramdriver', @equivalentId)

SET @equivalentId = 'a39e60a0-56ca-40e0-b866-1bcb1d0f0fcc'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('cyclist', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('bike rider', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('bicycle rider', @equivalentId)

SET @equivalentId = 'a6ab788f-b645-4a1c-8b8c-72ece73cd9d9'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('parking inspector', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('traffic officer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('council parketing inspector', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('infringements officer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('parking enforcement officer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('car parking officer', @equivalentId)

SET @equivalentId = 'a4ad6800-5c69-4295-a88e-6e6144f229bb'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('carpark manager', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('carpark supervisor', @equivalentId)

SET @equivalentId = '0a2e2de6-b9f5-46c8-b131-f3ba619045df'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('catering supervisor', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('catering manager', @equivalentId)

SET @equivalentId = '6e5694b4-34ab-4a11-8331-7b3bd51ca646'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('catering assistant', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('catering service assistant', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('catering officer', @equivalentId)

SET @equivalentId = '275875ff-648c-47be-9d4e-11bdb02bf189'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('cellarhand', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('cellar hand', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('cellarman', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('store and cellar', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('cellar man', @equivalentId)

SET @equivalentId = '343e1cc3-9efe-4a1a-868a-936529c9af87'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('aquaculture', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('aqua cluture', @equivalentId)

SET @equivalentId = '126bfa42-0922-4130-87ba-2ea31aaabe5c'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('rifle', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('gun', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('firearm', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('fire arm', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('shotgun', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('pistol', @equivalentId)

SET @equivalentId = '9270c9dc-7876-4c5d-a24a-0948a32fa47e'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('bid', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('proposal', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('tender', @equivalentId)

SET @equivalentId = 'd2395479-f8bb-422f-b0be-53d31ab8cbc3'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('dress maker', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('dressmaker', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('couture', @equivalentId)

SET @equivalentId = '770a2263-3c8f-4c6f-a25f-f7ff728e935f'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('eho', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('environmental health officer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('health officer', @equivalentId)

SET @equivalentId = '4c69c844-504f-41d9-81b7-73f85b617fa8'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('fitness first', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('fitnessfirst', @equivalentId)

SET @equivalentId = 'ca9fa7cb-6734-4418-ac30-01a37193b986'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('project development officer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('project development coordinator', @equivalentId)

SET @equivalentId = 'b66c771a-f9fb-4039-b1ba-eea4670aca3f'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('fernwood', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('fern wood', @equivalentId)

SET @equivalentId = 'ff508e0b-7cf0-499c-8ffa-690351e9f18d'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('ymca', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('the y', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('young men''s christian association', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('y m c a', @equivalentId)

SET @equivalentId = '8c0b84fa-06ec-41f3-963a-a52a22af92db'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('arborist', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('tree doctor', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('tree worker', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('tree climber', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('tree lopper', @equivalentId)

SET @equivalentId = 'b8f7a4e4-6862-4010-8d8d-8c0168bdaa91'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('statutory', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('stat', @equivalentId)

SET @equivalentId = 'b52ba44e-f724-42c0-8fda-af11abc49647'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('data analyst', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('information analyst', @equivalentId)

SET @equivalentId = '4efed39e-4012-46b3-93c6-b3d01dc8bfc0'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('housing officer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('housing services officer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('housing support worker', @equivalentId)

SET @equivalentId = 'c44befab-f44a-4618-9376-9afc72d230c3'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('researcher', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('research officer', @equivalentId)

SET @equivalentId = '2375f134-07ae-474f-ac6d-f43b939fc231'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('dhs', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('department of human services', @equivalentId)

SET @equivalentId = 'a92ba1fa-c432-436d-9e41-b218aac51c18'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('wellness', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('fitness', @equivalentId)

SET @equivalentId = '17453562-a264-41e1-80cd-031d8d1c6663'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('ddon', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('deputy director of nursing', @equivalentId)

SET @equivalentId = 'c15dc6e0-da00-4b1b-b670-3ce2e16185a1'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('crisis response officer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('crisis support officer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('crisis assistance officer', @equivalentId)

SET @equivalentId = '50a8d170-d633-4db0-9d0e-3138e5ba5b6d'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('career advisor', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('career cousellor', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('career coach', @equivalentId)

SET @equivalentId = 'e2d556a4-d22f-44a4-97c7-8af2418ad734'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('ultrasound', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('ultra sound', @equivalentId)

SET @equivalentId = '17f970f9-0535-4932-ab70-10f749ba2ad1'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('outplacement', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('out placement', @equivalentId)

SET @equivalentId = '7c345292-864f-48c6-a8b5-b85e7ade83fc'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('radiotherapy', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('radio therapy', @equivalentId)

SET @equivalentId = 'b33b0312-1d98-4eb7-aa66-95c88d472722'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('outplacement clerk', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('outplacement officer', @equivalentId)

SET @equivalentId = '5ab03c33-60c8-4c7a-a13f-d68b9d803ad6'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('aod', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('alcohol and other drugs', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('alcohol & other drugs', @equivalentId)

SET @equivalentId = '5aa3aa24-878f-481c-aa16-08106ba92983'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('neurology', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('neuro', @equivalentId)

SET @equivalentId = '73b8caf6-d6e2-4141-8919-d8ebd9a9b521'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('forex', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('foreign exchange', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('fx', @equivalentId)

SET @equivalentId = 'af0e08a3-e672-4337-bc0d-bc3d493708a2'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('expat', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('ex pat', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('ex patriate', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('expatriate', @equivalentId)

SET @equivalentId = 'b37c8d68-ea59-4f71-9514-b17ffc53e383'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('six sigma', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('sixsigma', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('6sigma', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('6 sigma', @equivalentId)

SET @equivalentId = '090c5329-4890-45d3-b6ab-2beaa7b1ad1f'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('streetwear', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('street wear', @equivalentId)

SET @equivalentId = '4ea6159f-1ce7-42cd-8d39-6e8f8c1382d4'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('menswear', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('mens wear', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('mens clothing', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('mens fashion', @equivalentId)

SET @equivalentId = '38c99311-356e-4d0a-a786-d8770ba24a09'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('childrenswear', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('childrens wear', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('childrens clothing', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('childrens fashion', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('kids clothing', @equivalentId)

SET @equivalentId = '310db31b-b670-49a1-b259-8aa3cc64f468'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('womenswear', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('womens wear', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('womens clothing', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('womens fashion', @equivalentId)

SET @equivalentId = 'f3046f67-183e-4b09-a3f5-567c29b14411'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('lingerie', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('intimate apparel', @equivalentId)

SET @equivalentId = '0fa815ef-d70a-42f5-8fe1-038fc8eb2999'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('dj', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('disc jokey', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('radio announcer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('disc jockey', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('voice over announcer', @equivalentId)

SET @equivalentId = 'c022713c-38c7-4693-bd81-3d6c98e519aa'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('lcms', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('learning content management system', @equivalentId)

SET @equivalentId = '404174cd-4da0-4f47-90d8-579a7cfad63c'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('super market', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('supermarket', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('hyper market', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('hypermarket', @equivalentId)

SET @equivalentId = '1bd3d917-29c6-4a37-aac6-d2d6f083074a'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('typesetter', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('type setter', @equivalentId)

SET @equivalentId = 'e275c9ce-ec51-4f31-9288-24d8f4c16381'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('priest', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('vicar', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('minister of religon', @equivalentId)

SET @equivalentId = 'a4aadbe5-fd48-48cc-a64e-8d08e25e863a'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('mower man', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('mowerman', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('lawn mower', @equivalentId)

SET @equivalentId = '747eaf44-b8f5-449b-951e-440d6e074234'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('signwriter', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('sign writer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('sign designer', @equivalentId)

SET @equivalentId = '04d0fcc9-bd77-497e-b7e6-883be7c6072c'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('funeral director', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('funeral assistant', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('funeral coordinator', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('funeral consultant', @equivalentId)

SET @equivalentId = 'e14cfc60-14d9-494e-9cf8-b9cbc0e9e465'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('vigneron', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('winemaker', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('wine maker', @equivalentId)

SET @equivalentId = 'd5ab5fe2-906a-44fb-a800-ac51c0e792c0'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('flooring', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('floor covering', @equivalentId)

SET @equivalentId = '8af96c8a-177a-4ba2-829c-9952000a5989'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('garbage collector', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('garbo', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('garbage truck driver', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('garbage supervisor', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('refuse collector', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('refuse truck driver', @equivalentId)

SET @equivalentId = '95bc10e7-ae60-498f-826b-7eef75593ac9'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('alumni', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('alumnus', @equivalentId)

SET @equivalentId = 'c844f5a6-7797-4bd6-ac5e-594747e330ea'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('outpatient clerk', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('outpatient officer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('out patient officer', @equivalentId)

SET @equivalentId = 'c8250175-93a8-425b-beb3-35c104d53ae9'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('diesetter', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('die setter', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('die operator', @equivalentId)

SET @equivalentId = 'c727f0d6-2576-4001-aeb4-c635bf39933e'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('diecast', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('die cast', @equivalentId)

SET @equivalentId = '7ad8c884-a9d0-4d10-badd-2bdd3307cfaf'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('zookeeper', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('zoo keeper', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('animal keeper', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('animal handler', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('animal carer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('animal technician', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('animal management officer', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('animal attendant', @equivalentId)

SET @equivalentId = '0d9e9eed-2a87-47d0-8742-d9b4b8b8ee68'

INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('4wd', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('4 wheel drive', @equivalentId)
INSERT INTO dbo.EquivalentTerms(searchTerm, equivalentGroupId) VALUES ('four wheel drive', @equivalentId)

GO
