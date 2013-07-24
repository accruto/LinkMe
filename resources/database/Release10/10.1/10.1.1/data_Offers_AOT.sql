DECLARE @providerId UNIQUEIDENTIFIER
SET @providerId = '{69C3C1DA-061D-4cb6-B8B4-87560CF8D44A}'

-- Provider

EXEC dbo.CreateOfferProvider @providerId, 'Accredited Online Training'

-- Certificate courses

DECLARE @categoryId UNIQUEIDENTIFIER
SET @categoryId = '{DE7D1128-EFD0-4622-A25D-B88E9812DD6C}'

EXEC dbo.CreateOfferCategory @categoryId, 'E69254E0-F5E6-47C2-899C-8041D77EEF8B' /*Further Education & Development*/, 'Certificate Courses'

DECLARE @parentCategoryId UNIQUEIDENTIFIER
SET @parentCategoryId = @categoryId

EXEC dbo.CreateOffering '{3E9D2473-141E-447e-8D0D-F04CB8255DB3}', 'Certificate IV in Training & Assessment', @providerId, '{9D6C11BF-A965-43f2-ABCF-C48D2B64E3BB}', @parentCategoryId
EXEC dbo.CreateOffering '{5A248E65-DA94-4949-91A5-40719778D0BF}', 'Certificate IV in Frontline Management', @providerId, '{0BBF1745-59B5-4bb5-8EBE-CDEE434C94DE}', @parentCategoryId
EXEC dbo.CreateOffering '{9E67F624-98B8-48bf-8E05-E0EBAE98B8EB}', 'Certificate IV in Project Management', @providerId, '{105EBEC3-9D13-4fdc-BFE2-6AB2257A2653}', @parentCategoryId
EXEC dbo.CreateOffering '{E2319308-D9CF-4619-AA10-8CD908FEFC38}', 'Certificate IV in Business', @providerId, '{A799B43C-2FA3-42a7-BD2D-66748D2620D9}', @parentCategoryId
EXEC dbo.CreateOffering '{F2F8B037-5BF3-49b4-8CDE-8CD89AEAABF9}', 'Certificate IV in Small Business Management', @providerId, '{8E4ACB1D-3582-4470-9403-31D8153D47FC}', @parentCategoryId
EXEC dbo.CreateOffering '{E35F2E7D-7A9F-4abb-8A40-B42C65F7FA41}', 'Certificate IV in Business Administration', @providerId, '{1E3B7471-0D5D-4857-BBA0-C56A6E13C8A1}', @parentCategoryId
EXEC dbo.CreateOffering '{0EF948DF-23D1-48ff-8435-B283DAE78D8A}', 'Certificate IV in Human Resources', @providerId, '{29CF1BCB-323D-4d82-990D-3391F0EE1B1B}', @parentCategoryId
EXEC dbo.CreateOffering '{C3D3C157-21F3-47b8-88D0-593365C97A1C}', 'Certificate IV in Occupational Health & Safety', @providerId, '{980E9EF5-0168-4e61-8522-A40F540F54FD}', @parentCategoryId
EXEC dbo.CreateOffering '{52FABD2A-DF35-4931-9640-0797DF153862}', 'Certificate III in Financial Services (Accounts Clerical)', @providerId, '{3CCD3C82-794B-43fa-AE1F-648AD212F988}', @parentCategoryId
EXEC dbo.CreateOffering '{F4FDF033-ABA9-49ee-9193-28A6A4962B6B}', 'Certificate III in Financial Services (Insurance Services)', @providerId, '{2F9469AE-D9ED-4257-9FED-C4326B64D6CB}', @parentCategoryId
EXEC dbo.CreateOffering '{FA5BCFF2-42F6-435c-B803-605A1E8CF9AD}', 'Certificate III in Business Administration', @providerId, '{F6B85C26-DCBF-4406-A11C-A05938EFB9BF}', @parentCategoryId
EXEC dbo.CreateOffering '{B3653D1F-57AF-4e9b-AF68-5FB4A0EEFA48}', 'Certificate II in Business', @providerId, '{6C0594C3-8C02-4d7d-8763-F5735346AB1C}', @parentCategoryId

-- Business Courses

SET @parentCategoryId = 'D328CBF4-3429-4D49-A792-442F85FC7763'

EXEC dbo.CreateOffering '{5A248E65-DA94-4949-91A5-40719778D0BF}', 'Certificate IV in Frontline Management', @providerId, '{42BD9F3C-395A-4b2b-9D81-B5335F659532}', @parentCategoryId
EXEC dbo.CreateOffering '{9E67F624-98B8-48bf-8E05-E0EBAE98B8EB}', 'Certificate IV in Project Management', @providerId, '{F62A4EA1-D2AA-4b29-A53C-B0E9F9A62180}', @parentCategoryId
EXEC dbo.CreateOffering '{E2319308-D9CF-4619-AA10-8CD908FEFC38}', 'Certificate IV in Business', @providerId, '{4FA1FC80-478B-4bf0-A7E4-F57861280135}', @parentCategoryId
EXEC dbo.CreateOffering '{F2F8B037-5BF3-49b4-8CDE-8CD89AEAABF9}', 'Certificate IV in Small Business Management', @providerId, '{143A908E-061B-4677-A0D0-395EA14ECC3C}', @parentCategoryId
EXEC dbo.CreateOffering '{E35F2E7D-7A9F-4abb-8A40-B42C65F7FA41}', 'Certificate IV in Business Administration', @providerId, '{8D9AB246-C6E8-49de-943A-9EE845961428}', @parentCategoryId
EXEC dbo.CreateOffering '{FA5BCFF2-42F6-435c-B803-605A1E8CF9AD}', 'Certificate III in Business Administration', @providerId, '{7FA9823D-E26B-4462-B351-5D9F345FBDA0}', @parentCategoryId
EXEC dbo.CreateOffering '{B3653D1F-57AF-4e9b-AF68-5FB4A0EEFA48}', 'Certificate II in Business', @providerId, '{22E4D7B2-A41B-422c-B255-E83CC6157E05}', @parentCategoryId
EXEC dbo.CreateOffering '{15E18C5B-FFD0-49de-B5EE-A20B3647744B}', 'Diploma of Business', @providerId, '{F9351740-24E9-442e-B4BF-1805D7DE7073}', @parentCategoryId
EXEC dbo.CreateOffering '{641700BC-E862-4b0a-AB7D-DD22A19437B4}', 'Diploma of Business Administration', @providerId, '{BF23EFB9-0E43-41f2-83E4-E2BC7A15CB3B}', @parentCategoryId
EXEC dbo.CreateOffering '{F206F153-FC45-4296-9AFD-10F6C041D0E4}', 'Diploma of Management', @providerId, '{5466E3FB-1905-4cb5-9CA7-087212FF9BBB}', @parentCategoryId
EXEC dbo.CreateOffering '{B1DA7941-02C2-46e7-BAAC-78D42AAF0C88}', 'Diploma of Project Management', @providerId, '{389BEE64-F475-44a2-BADE-EB238D37F030}', @parentCategoryId

-- Financial Services Courses

SET @parentCategoryId = '56099C39-4C57-4464-A2D6-A9FE5F1561CE'

EXEC dbo.CreateOffering '{52FABD2A-DF35-4931-9640-0797DF153862}', 'Certificate III in Financial Services (Accounts Clerical)', @providerId, '{DEFEA56D-E028-47af-A1E9-134C7E6EFD8A}', @parentCategoryId
EXEC dbo.CreateOffering '{F4FDF033-ABA9-49ee-9193-28A6A4962B6B}', 'Certificate III in Financial Services (Insurance Services)', @providerId, '{24820601-FEB4-4e73-9A06-6AEA9632229D}', @parentCategoryId

-- HR & Recruitment Courses

SET @categoryId = '{97151BFF-CE44-4d57-8DFD-031E4FBFE7D1}'
EXEC dbo.CreateOfferCategory @categoryId, 'E69254E0-F5E6-47C2-899C-8041D77EEF8B' /*Further Education & Development*/, 'HR & Recruitment Courses'

SET @parentCategoryId = @categoryId

EXEC dbo.CreateOffering '{0A0FBCA7-3099-48fc-983F-08984D13E203}', 'Diploma of Human Resources', @providerId, '{5E337558-AA1C-4abd-9C7F-248608AF7C97}', @parentCategoryId
EXEC dbo.CreateOffering '{0EF948DF-23D1-48ff-8435-B283DAE78D8A}', 'Certificate IV in Human Resources', @providerId, '{9C62D349-12EC-48e6-AE55-4DEF2B54E48A}', @parentCategoryId
EXEC dbo.CreateOffering '{C3D3C157-21F3-47b8-88D0-593365C97A1C}', 'Certificate IV in Occupational Health & Safety', @providerId, '{7F70CA7E-F931-450a-9CA5-AF88E61E49FE}', @parentCategoryId

-- Tourism & Hospitality Courses

SET @parentCategoryId = '5CFF8309-DB97-485F-A5F5-3641DF04795D'

EXEC dbo.CreateOffering '{DB2E42D5-6316-4229-8C13-0DCF29F4506E}', 'Responsible Service of Alcohol (QLD)', @providerId, '{49F5195C-5472-4301-93D6-339B0A8F71F1}', @parentCategoryId
EXEC dbo.CreateOffering '{CBEB6579-1D03-4f31-BBD9-A78D73E19A26}', 'Responsible Service of Gambling', @providerId, '{75C962F5-33BE-4d7f-B83D-81600C945055}', @parentCategoryId
EXEC dbo.CreateOffering '{F722A254-6F99-49e3-AB1E-899496742FEC}', 'Follow workplace hygiene procedures', @providerId, '{356C28A8-B4C9-4e3c-BF6F-2E1EF586E1D3}', @parentCategoryId

-- Alternative Careers

SET @parentCategoryId = '385188B4-D205-46B5-AA18-6170B9623407'

EXEC dbo.CreateOffering '{823B487A-A7D0-45e4-8AF1-77E22FE6D09D}', 'Help me start my career in Recruitment or HR', @providerId, '{586AF2B1-7DF6-4cfb-8E1A-FD4497BE820F}', @parentCategoryId
EXEC dbo.CreateOffering '{9A1FD4D7-B687-4e62-BDA5-9ECE943195A7}', 'Help me start my career in Business', @providerId, '{5A0CF57D-791A-4b84-94FD-0E8E77F554CF}', @parentCategoryId
EXEC dbo.CreateOffering '{9F1F4343-89B3-4749-BD38-EDC87B62554D}', 'Help me start my career in Project Management', @providerId, '{84DFA14D-BDA9-4f90-AE96-2E90B5539C50}', @parentCategoryId
EXEC dbo.CreateOffering '{FDD15A78-764D-4b61-98F8-603AB54B5AED}', 'Help me start my career in the Financial Services', @providerId, '{76542735-17CE-4a5d-BF2B-DEB28A00C844}', @parentCategoryId
EXEC dbo.CreateOffering '{888C8D3F-73DA-463c-BD33-DA2ACF5540D4}', 'Help me start my career in Hospitality', @providerId, '{BA29E183-F913-4da3-9C3E-64A79B0BB102}', @parentCategoryId
