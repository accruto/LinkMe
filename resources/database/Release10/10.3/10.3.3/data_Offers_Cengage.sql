DECLARE @providerId UNIQUEIDENTIFIER
SET @providerId = '{5D1C4147-7C83-4fea-A86D-47A990C9AB3A}'

-- Provider

EXEC dbo.CreateOfferProvider @providerId, 'Cengage'

-- Accounting

DECLARE @parentCategoryId UNIQUEIDENTIFIER
SET @parentCategoryId = 'FAF62B20-0302-4E86-BDB5-8EE27443FF71'

EXEC dbo.CreateOffering '{E61D2F4C-546E-47ac-A9E0-EF84332BB7D9}', 'Bachelor of Accounting', @providerId, '{5FCE87A6-EBB6-4d22-86CE-2B861CE08B39}', @parentCategoryId
EXEC dbo.CreateOffering '{D4FAEA9E-5BF5-4d14-BB54-5F23F2E983C9}', 'Diploma of Accounting', @providerId, '{BA3BA4C5-1765-40dc-97FB-754EDD28B65A}', @parentCategoryId
EXEC dbo.CreateOffering '{E00EACF4-3732-4806-AD0E-89A86C1DB73E}', 'Advanced Diploma of Accounting', @providerId, '{D00F960A-67CF-4c7f-8969-9CCEC9FFBF42}', @parentCategoryId
EXEC dbo.CreateOffering '{171978A0-7EB1-453a-830D-BCB4EED31251}', 'Certificate IV in Financial Services (Accounting)', @providerId, '{6DDB86EB-0B65-4c03-A15B-DA1789B44DBD}', @parentCategoryId
EXEC dbo.CreateOffering '{596E6511-D463-44df-A896-39740544FD62}', 'Certificate III in Financial Services (Accounts Clerical)', @providerId, '{AA666416-038A-43ad-B0AC-A74B5D09EA0E}', @parentCategoryId
EXEC dbo.CreateOffering '{5B6DAAD7-920C-4b24-9B1D-E769084DB540}', 'MYOB', @providerId, '{43070EAF-F872-4a8b-8788-7FF5DEFE5DA8}', @parentCategoryId
EXEC dbo.CreateOffering '{F5878841-8BAA-4791-906D-8D3F99ADFFB4}', 'Statement of Attainment in Financial Services (Bookkeeping)', @providerId, '{A67BD690-4A72-455f-949E-DCFD9280FA0C}', @parentCategoryId

-- Arts, Humanities & Language Courses

SET @parentCategoryId = '93353773-4AFE-4833-85C1-17837DD8EA48'

EXEC dbo.CreateOffering '{F18D8734-F2E1-4fb0-98A6-89A2BAB95145}', 'Professional Editing & Proofreading', @providerId, '{0A11EC73-3F30-4421-B931-33FFB0503B7B}', @parentCategoryId
EXEC dbo.CreateOffering '{1D9941E4-56AB-4976-88FD-4F31AC2DC111}', 'Freelance Journalism', @providerId, '{94D8FBB0-339F-4185-97E2-F82ECD2E3C01}', @parentCategoryId
EXEC dbo.CreateOffering '{C9AB6897-5AD4-4444-BE5F-36A3FD953DB2}', 'Professional Children''s Writing', @providerId, '{2AC0298A-63B1-40e0-B63F-7163CAA92026}', @parentCategoryId
EXEC dbo.CreateOffering '{311B96C3-0A12-499a-BADC-1612176F5575}', 'Writing a Bestseller', @providerId, '{A11D24CB-C97C-4de3-80CC-740945BDF354}', @parentCategoryId
EXEC dbo.CreateOffering '{41C2B1A4-CEAB-49f6-B0F5-0F58F537EA22}', 'Professional Scriptwriting', @providerId, '{29BD9AFD-BE78-48da-85D1-61AC33C7A67D}', @parentCategoryId
EXEC dbo.CreateOffering '{D195C303-6B35-473c-99F4-999B8975A531}', 'Fantasy and Science Fiction Writing', @providerId, '{24F78B2B-DCA3-4488-95BF-11958A651A3E}', @parentCategoryId
EXEC dbo.CreateOffering '{A0B1AA15-EC87-4fe1-992C-9DA2B134A297}', 'Advanced Freelance Journalism', @providerId, '{7C4844DB-083C-4aa4-887B-5F8D2BA500BB}', @parentCategoryId
EXEC dbo.CreateOffering '{B5147EA7-2AD5-4232-80EE-40EAB099B837}', 'Travel Writing and Photography', @providerId, '{DFC2BFB7-CE24-46c8-8A7B-E7AFB86A42B3}', @parentCategoryId
EXEC dbo.CreateOffering '{5C2E356D-4D49-4f26-84C9-D289C4E6BD67}', 'Non-Fiction Writing and Publishing', @providerId, '{B865ACFD-24AF-4fa1-91B1-BE0573F32117}', @parentCategoryId
EXEC dbo.CreateOffering '{88B69DC9-85CB-441d-A45D-4E7127739E33}', 'Creative Writing', @providerId, '{60E29975-A47F-415c-B7ED-E13BED644A76}', @parentCategoryId

-- Art, Design & Digital Media

SET @parentCategoryId = 'AB12D51C-92CC-4717-A3B2-A50BFEE6A097'

EXEC dbo.CreateOffering '{C63D7F74-7149-49e6-9B4A-0B9211EFBE95}', 'Certificate IV in Design', @providerId, '{AF66D67A-340E-461a-BECB-90310A7309EB}', @parentCategoryId
EXEC dbo.CreateOffering '{4EAC3B33-324E-400e-B1C0-15A65D39AE0F}', 'Dressmaking & Pattern Cutting', @providerId, '{2A0B2103-00A1-4c43-ABDD-AB7499912F64}', @parentCategoryId
EXEC dbo.CreateOffering '{40CEC9C3-8786-4bbb-8BA8-6F210FE0A89C}', 'Freelance Photography', @providerId, '{B5B70EAF-AD2C-456e-AFAC-AAE0322A2DE3}', @parentCategoryId
EXEC dbo.CreateOffering '{0DC902BB-87AA-4357-9657-759C317B0BB8}', 'Graphic Design and Desktop Publishing PC', @providerId, '{7C52F56E-90D2-4117-B58B-893C21CCA408}', @parentCategoryId
EXEC dbo.CreateOffering '{3777A408-2B41-43e8-A67B-3D9D0BAA6FF2}', 'Freelance Cartooning & Illustrating', @providerId, '{035C126B-F1E5-44bd-89B9-919CEB8952AB}', @parentCategoryId
EXEC dbo.CreateOffering '{AFC50F6B-A3BF-4733-B09A-2CC1E0A16689}', 'Illustration & Drawing made easy', @providerId, '{FE3A53D8-87BE-4d28-8F28-A905C96F17B0}', @parentCategoryId
EXEC dbo.CreateOffering '{EA636F81-C3B6-4cf1-A29E-A583A3CCCE7A}', 'Floral Design', @providerId, '{E2873676-D569-47bf-8514-1AF2BD382232}', @parentCategoryId
EXEC dbo.CreateOffering '{BF82687C-2E7A-4932-AD29-0FF454D9DD22}', 'Photography', @providerId, '{5EEE52B5-96F5-4afe-BC45-9FF0890C5CA2}', @parentCategoryId
EXEC dbo.CreateOffering '{C4853A7D-F179-4921-9544-E14B96E8843E}', 'Introduction to Digital Photography', @providerId, '{0FFCFE69-E3B5-40ea-BDB3-EFA9FF8AC20D}', @parentCategoryId
EXEC dbo.CreateOffering '{357E1C17-BD03-4a06-B90F-8AE617A8DD02}', 'Statement of Attainment in Interior Design', @providerId, '{D3F8E519-A0A4-480f-B119-CDEFFB956058}', @parentCategoryId

-- Business Courses

SET @parentCategoryId = 'D328CBF4-3429-4D49-A792-442F85FC7763'

EXEC dbo.CreateOffering '{D48C8679-F47F-4986-BCC0-708894A0370E}', 'Statement of Attainment in Business (Mecical Receptionist)', @providerId, '{11561CC1-131A-4ff4-B041-0CEAC883C5A1}', @parentCategoryId
EXEC dbo.CreateOffering '{AABF90EF-EB9A-475f-A224-B8A863BFB4F2}', 'Certificate III in Business Administration', @providerId, '{8683CAA1-2A2E-4f2d-B71D-03F7B826A445}', @parentCategoryId
EXEC dbo.CreateOffering '{39E62E47-1098-47c3-9321-A17CD325D654}', 'Certificate IV in Business Administration', @providerId, '{F0A151A8-08F3-4ee9-90E4-5DB8FBDC93FF}', @parentCategoryId
EXEC dbo.CreateOffering '{8C408638-0B99-4722-860E-E6D2207C916D}', 'Certificate IV in Frontline Management', @providerId, '{18D2EEC0-E604-463d-BCFC-DED770E38E0E}', @parentCategoryId
EXEC dbo.CreateOffering '{0D720511-47BB-482d-B54C-CC45F07E1FC3}', 'Certificate IV in Small Business Management', @providerId, '{10FD5DA0-C7C4-488f-88F9-FE5FFDBBE187}', @parentCategoryId
EXEC dbo.CreateOffering '{C53E0C82-A126-48b4-8333-EFE6BB7573B4}', 'Certificate II in Business', @providerId, '{79D5EB0B-60AC-4bfa-A9CC-7669C2B298A7}', @parentCategoryId
EXEC dbo.CreateOffering '{BE232F4A-83E3-4bef-8788-A44B962DBC7D}', 'Public Relations and Events Management', @providerId, '{B90AF863-0E6F-411b-86A1-2E8BDE6AFAD2}', @parentCategoryId
EXEC dbo.CreateOffering '{12643CE4-4015-4e13-AFE2-800715423D5D}', 'Certificate III in Business', @providerId, '{18A6C82D-B624-4917-9282-D85895F24B5F}', @parentCategoryId
EXEC dbo.CreateOffering '{8C19FBBE-13AF-4a24-924A-A40955C5892A}', 'Diploma of Management', @providerId, '{AA125008-067E-400b-8C90-EF61FAF534D8}', @parentCategoryId

-- Community Services Courses

SET @parentCategoryId = '{820DBB66-48E1-46c2-9051-4ACC8E2BAFA4}'
EXEC dbo.CreateOfferCategory @parentCategoryId, 'E69254E0-F5E6-47C2-899C-8041D77EEF8B', 'Community Services Courses'

EXEC dbo.CreateOffering '{4E50787C-712B-498c-9A08-0AD56146A0E4}', 'Certificate III in Disability', @providerId, '{0F163B19-CB14-4681-844E-F5990B5B6156}', @parentCategoryId
EXEC dbo.CreateOffering '{321843C0-4455-4191-A8F4-D5A639361E6B}', 'Certificate III in Children''s Services', @providerId, '{AF9BAB29-0434-445e-9AB0-A4CE123889A4}', @parentCategoryId
EXEC dbo.CreateOffering '{7446BBC0-37A0-4c8f-97DC-E8DDA2E9FE25}', 'Certificate III in Aged Care', @providerId, '{51BC49D2-3D3D-4a82-9933-D1AC40A7BD5C}', @parentCategoryId

-- Financial Services Courses

SET @parentCategoryId = '56099C39-4C57-4464-A2D6-A9FE5F1561CE'

EXEC dbo.CreateOffering '{C8E06B12-38A9-44ef-B5F1-DCCA5EEB8F0D}', 'Certificate IV in Financial Services (Bookkeeping)', @providerId, '{23883C58-3522-4621-B56B-D475BAB17406}', @parentCategoryId

EXEC dbo.CreateOfferCategory '{6113F5B1-1D96-4b34-8108-C827237E3026}', @parentCategoryId, 'Certificate IV in Financial Services (Accounting)'
EXEC dbo.CreateCategoryOffering '{6113F5B1-1D96-4b34-8108-C827237E3026}', '{171978A0-7EB1-453a-830D-BCB4EED31251}'

EXEC dbo.CreateOfferCategory '{E8CDDA2F-1357-4878-BF5F-119BFA2C52BC}', @parentCategoryId, 'Certificate III in Financial Services (Accounts Clerical)'
EXEC dbo.CreateCategoryOffering '{E8CDDA2F-1357-4878-BF5F-119BFA2C52BC}', '{596E6511-D463-44df-A896-39740544FD62}'

-- Community Services Courses

SET @parentCategoryId = '{5CBA725B-3EDC-40fa-95C3-DC80E3DC314F}'
EXEC dbo.CreateOfferCategory @parentCategoryId, 'E69254E0-F5E6-47C2-899C-8041D77EEF8B', 'Health, Beauty & Well-Being Courses'

EXEC dbo.CreateOffering '{C0A1735C-98CB-4332-9C9B-20814CE90F94}', 'Certificate III in Nail Technology', @providerId, '{C9F7D00B-1298-40fc-8357-58D07EAA8216}', @parentCategoryId
EXEC dbo.CreateOffering '{5BF39B22-A1F0-49cd-B94A-134BD5DC5ADE}', 'Certificate II in Make-Up Services', @providerId, '{7C4EF38D-5189-4018-8896-5F24BAD883FF}', @parentCategoryId
EXEC dbo.CreateOffering '{0BED0590-1729-40da-88A6-BBDB3A2647FE}', 'Beauty Therapy', @providerId, '{521DBE34-FE8F-4980-B7F4-245B67CE17F4}', @parentCategoryId
EXEC dbo.CreateOffering '{8324BCD0-8850-4e06-B5DB-A9547B268A9D}', 'Massage', @providerId, '{B85CAFA0-AA0D-41f1-AD4E-AC5349C71C5B}', @parentCategoryId
EXEC dbo.CreateOffering '{AC360084-A260-4986-B141-3ECB9E53583F}', 'Nutrition', @providerId, '{FF9A5B9B-2BAF-4bb7-BC1F-04F893BD8118}', @parentCategoryId
EXEC dbo.CreateOffering '{1E7F1B9F-F57F-4462-9DE4-064BA329FF41}', 'Physical Therapy Aide', @providerId, '{DF54F6D1-443D-4812-A107-56D01030858B}', @parentCategoryId
EXEC dbo.CreateOffering '{86404A71-CC1D-4e0f-9011-3CFF1FEC35A4}', 'Nail Technician', @providerId, '{F313A64D-A865-4980-900E-CC4EE19AF315}', @parentCategoryId
EXEC dbo.CreateOffering '{2C38D788-D3A7-4106-B367-BEAFC91FBE3C}', 'Life Coaching', @providerId, '{9A2338B4-9BD6-4b7a-9CA6-673323828F7B}', @parentCategoryId
EXEC dbo.CreateOffering '{8CB8B02B-5A14-41a0-81A3-5E50807AC9C0}', 'Natural Therapies', @providerId, '{D0981EE3-A09E-4d62-875F-57C1A5A3C2F0}', @parentCategoryId
EXEC dbo.CreateOffering '{E149D031-BC41-42e4-BA6E-012F9DA79511}', 'Aromatherapy for Pregnancy, Labour & Infancy', @providerId, '{E2333080-96C3-44d1-8018-CF43C2D08FE2}', @parentCategoryId

-- Health Sciences, Nursing & Sport Courses

SET @parentCategoryId = '801B09FE-6730-4C97-9077-1D0F29B572F1'

EXEC dbo.CreateOffering '{764DB307-DD60-4bc2-AA76-274A4ED6FC0F}', 'Certificate III in Fitness', @providerId, '{7021D668-0F62-4891-BBB6-B28C000E680E}', @parentCategoryId
EXEC dbo.CreateOffering '{04C8585C-98D3-443b-A36E-FA5C508241F2}', 'Certificate IV in Fitness', @providerId, '{DC73CF9B-E3A5-47f5-93FC-4AD40D808046}', @parentCategoryId
EXEC dbo.CreateOffering '{1B9DEC2F-E552-46b2-9D5E-EBF8BDBCFBE1}', 'Sports Nutrition', @providerId, '{D62EE485-08A3-479c-AC93-8FEBCE21F4B1}', @parentCategoryId
EXEC dbo.CreateOffering '{12CDB629-E5F4-48d6-94A9-79526EB42251}', 'Exercise Therapy', @providerId, '{D0B36166-FED1-48da-9E79-3957E6EBA4B2}', @parentCategoryId

-- Information Technology & Computing Courses

SET @parentCategoryId = 'AE0DB911-537D-46DE-B971-4B46DC266372'

EXEC dbo.CreateOffering '{42973A23-948D-436e-AF64-482B94475B3F}', 'Certificate II in Information Technology', @providerId, '{B622ACB6-EA06-47df-BFCE-4C976063EB92}', @parentCategoryId
EXEC dbo.CreateOffering '{43027752-C354-462f-8250-CF5A56046D76}', 'PC Fundamentals', @providerId, '{B3B5192D-C143-46c4-900E-31EBE74ADBBC}', @parentCategoryId

-- Marketing Courses

SET @parentCategoryId = '{E322D095-8939-476a-B816-8798060E7F2F}'
EXEC dbo.CreateOfferCategory @parentCategoryId, 'E69254E0-F5E6-47C2-899C-8041D77EEF8B', 'Marketing Courses'

EXEC dbo.CreateOffering '{B70F613A-4A7E-4dd7-90CE-BABB14DCEF07}', 'Certificate IV in Marketing', @providerId, '{34072D42-BCE8-4f35-A734-A9FAAA1261CC}', @parentCategoryId

-- Natural & Built Environment Courses

SET @parentCategoryId = 'E818515B-479C-49B6-BF69-83E744A37C02'

EXEC dbo.CreateOffering '{BA37DE9D-484B-48e4-8EAB-C825E80E58B4}', 'Certificate II in Horticulture', @providerId, '{8D9D9C21-64FE-4885-8E6A-BABFDF991511}', @parentCategoryId

-- Retail

SET @parentCategoryId = '{536D9D2A-F671-4293-89EA-4647F18B7655}'
EXEC dbo.CreateOfferCategory @parentCategoryId, 'E69254E0-F5E6-47C2-899C-8041D77EEF8B', 'Retail Courses'

EXEC dbo.CreateOffering '{28C66123-4935-4b38-B633-F3BCE7FBE87D}', 'Retail Pharmacy Assistant', @providerId, '{50029D56-CC46-45a5-8003-E98A13D34A82}', @parentCategoryId
EXEC dbo.CreateOffering '{CC2203C8-1C1C-45fb-A921-57A6AB3848D5}', 'Certificate II in Retail', @providerId, '{D53B49F1-2646-47c0-9E57-CF44844F38F5}', @parentCategoryId

-- Short Courses

SET @parentCategoryId = '41AC5871-5463-4368-8146-00272EBEA326'

EXEC dbo.CreateOffering '{B0DDBDB5-E106-4066-852F-78D3C4851A66}', 'Pet Grooming', @providerId, '{8A4C4F74-BDFE-44b4-9094-7D3820A75B48}', @parentCategoryId
EXEC dbo.CreateOffering '{62BA7ED1-A195-4044-977C-1E8DCCFB04F7}', 'Pet Obedience Training', @providerId, '{6CDAAF69-5C60-485e-8EE6-653D9063C522}', @parentCategoryId
EXEC dbo.CreateOffering '{8904E28B-B165-4274-A290-6DA2FB1D786C}', 'Home Gardening', @providerId, '{1984DBA1-344E-48f3-959D-262F40B159E0}', @parentCategoryId

-- Tourism & Hospitality

SET @parentCategoryId = '5CFF8309-DB97-485F-A5F5-3641DF04795D'

EXEC dbo.CreateOffering '{78D5CE84-12AE-4361-9112-72952A81DD0D}', 'Certificate III in Tourism (Retail Travel Sales)', @providerId, '{07C24337-B300-42c9-A813-F7A4C7077835}', @parentCategoryId
EXEC dbo.CreateOffering '{5BA3D596-5874-4ec5-9286-277EC9032312}', 'Certificate IV in Tourism', @providerId, '{7B22D370-535C-4e4a-B648-EA8742D226B6}', @parentCategoryId
EXEC dbo.CreateOffering '{66ACA8FA-BB74-4a2a-A1B8-59D0C44CB484}', 'Galileo (e-learning)', @providerId, '{8EF29D5D-61A1-4503-A735-56F34FE52E71}', @parentCategoryId

-- Trade Skills

SET @parentCategoryId = 'B6B96130-13AD-488E-83B7-BD044C6942FB'

EXEC dbo.CreateOffering '{21C55451-6E6B-4afd-BFC6-B0C9A396E987}', 'Fundamentals of Automotive Mechanics', @providerId, '{D2122EA9-0A45-4526-928F-1C9D389ECBDF}', @parentCategoryId
EXEC dbo.CreateOffering '{C950AE82-2475-4706-B14A-0E17A570EBDE}', 'Fundamentals in Small Engine Repair', @providerId, '{CA22CA2D-7AEF-4128-A673-49014343F2E2}', @parentCategoryId
EXEC dbo.CreateOffering '{9A45EB74-B24A-42bd-B118-0EB6D5FC3453}', 'Locksmithing', @providerId, '{D92CD301-E8CC-43d1-BE84-CD6E9ECDF96E}', @parentCategoryId
EXEC dbo.CreateOffering '{EE1D23B7-0963-4b52-8276-035C1FBE4337}', 'Carpentry and Joinery (Introduction)', @providerId, '{120D827D-D622-4b7a-AD33-C6AF6222B22C}', @parentCategoryId

