DECLARE @providerId UNIQUEIDENTIFIER
SET @providerId = '{02522B6F-8678-4ff1-96B7-D54617939228}'

-- Provider

EXEC dbo.CreateOfferProvider @providerId, 'Swinburne University of Technology - Short Courses'

-- Arts, Humanities & Language Courses

DECLARE @parentCategoryId UNIQUEIDENTIFIER
SET @parentCategoryId = '93353773-4AFE-4833-85C1-17837DD8EA48'

EXEC dbo.CreateOffering '{66A11F40-E0FF-4cef-A3FA-7CF67A3CE111}', 'Short Courses - Performance', @providerId, '{B7EC3754-0648-4343-8A61-3EA82A0FAE05}', @parentCategoryId
EXEC dbo.CreateOfferingLocation '{66A11F40-E0FF-4cef-A3FA-7CF67A3CE111}', '{8DC87E42-2FDC-4e54-A5F0-973EE09A5C55}', 'VIC', 18
EXEC dbo.CreateOffering '{39E67B42-32A3-49bd-85C5-969BD387A9B0}', 'Short Courses - Writing', @providerId, '{A974C517-2C81-4ec6-B230-58381364D6BE}', @parentCategoryId
EXEC dbo.CreateOfferingLocation '{39E67B42-32A3-49bd-85C5-969BD387A9B0}', '{F9F7F0C1-0E11-42c6-AC19-A4A93FFE79F5}', 'VIC', 18

-- Art, Design & Digital Media

SET @parentCategoryId = 'E69254E0-F5E6-47C2-899C-8041D77EEF8B'
EXEC CreateOfferCategory 'AB12D51C-92CC-4717-A3B2-A50BFEE6A097', @parentcategoryId, 'Art, Design & Digital Media'

SET @parentCategoryId = 'AB12D51C-92CC-4717-A3B2-A50BFEE6A097'

EXEC dbo.CreateOffering '{035B7E1D-D830-428b-BA1A-20C63D39649B}', 'Short Courses - Fashion', @providerId, '{8B6FBFF3-81A3-4f4a-947B-92C69C801B66}', @parentCategoryId
EXEC dbo.CreateOfferingLocation '{035B7E1D-D830-428b-BA1A-20C63D39649B}', '{E1407AC9-99E4-495c-BD31-5BAB51BC9E59}', 'VIC', 18
EXEC dbo.CreateOffering '{6DBC6C0C-A0CD-4393-BD41-E996702C8D2E}', 'Short Courses - Folio Development', @providerId, '{CCE1FCEB-D05B-4a10-A435-059838A4FC51}', @parentCategoryId
EXEC dbo.CreateOfferingLocation '{6DBC6C0C-A0CD-4393-BD41-E996702C8D2E}', '{B7695B2B-4CFF-4955-A759-E10A9ED84E89}', 'VIC', 18
EXEC dbo.CreateOffering '{00D6C7CC-F235-4586-89E4-38C2D576E1DB}', 'Short Courses - Interior Design', @providerId, '{328FF479-C46E-407a-982C-8E3513CCA4D2}', @parentCategoryId
EXEC dbo.CreateOfferingLocation '{00D6C7CC-F235-4586-89E4-38C2D576E1DB}', '{1D841548-98B4-45da-872C-19CD30D114F7}', 'VIC', 18
EXEC dbo.CreateOffering '{364D493A-34F4-4729-B007-8B040303E50E}', 'Short Courses - Makeup', @providerId, '{D701CAF9-7618-453e-8111-45EB92CFD599}', @parentCategoryId
EXEC dbo.CreateOfferingLocation '{364D493A-34F4-4729-B007-8B040303E50E}', '{740E01A1-027A-442f-BDD2-185D90DF03D6}', 'VIC', 18
EXEC dbo.CreateOffering '{0D3FEC68-A514-4a3e-A852-A6005E652AB8}', 'Short Courses - Multimedia', @providerId, '{307E89F9-6F1E-4476-BBFD-73A7929FAF66}', @parentCategoryId
EXEC dbo.CreateOfferingLocation '{0D3FEC68-A514-4a3e-A852-A6005E652AB8}', '{552D090A-2E24-4b3b-BACD-C33E658B3BB8}', 'VIC', 18
EXEC dbo.CreateOffering '{0087E9EE-1B49-4b4e-8768-31C03A0591B4}', 'Short Courses - Photography', @providerId, '{37A0A1C4-FA7E-4eea-B34C-7FD872DDDF49}', @parentCategoryId
EXEC dbo.CreateOfferingLocation '{0087E9EE-1B49-4b4e-8768-31C03A0591B4}', '{61FA9093-114D-437f-B6F5-7591E4D96BC5}', 'VIC', 18
EXEC dbo.CreateOffering '{AF849AED-AA00-429f-9897-D7414512511D}', 'Short Courses - Visual Arts', @providerId, '{27C0F27A-6D21-4e67-80B4-93E45EBED7B1}', @parentCategoryId
EXEC dbo.CreateOfferingLocation '{AF849AED-AA00-429f-9897-D7414512511D}', '{E7436907-8882-4236-8D46-09B46430D5D1}', 'VIC', 18
EXEC dbo.CreateOffering '{821BAD6F-1A7B-4f22-801C-B512298BFCB8}', 'Short Courses - Visual Merchandising', @providerId, '{ECE89AEC-D6EC-48e1-BC0B-7B507623161C}', @parentCategoryId
EXEC dbo.CreateOfferingLocation '{821BAD6F-1A7B-4f22-801C-B512298BFCB8}', '{885BFA2C-F01C-4ab0-8484-42C4DECB11AF}', 'VIC', 18

-- Business Courses

SET @parentCategoryId = 'D328CBF4-3429-4D49-A792-442F85FC7763'

EXEC dbo.CreateOffering '{9CF9F1B8-67F7-41a0-B233-BC916FAFFFD3}', 'Short Courses - Business Communication', @providerId, '{6A53D2C9-4950-47e8-ACEE-B9186063008A}', @parentCategoryId
EXEC dbo.CreateOfferingLocation '{9CF9F1B8-67F7-41a0-B233-BC916FAFFFD3}', '{A39FD519-20DC-482a-8E9E-772BDB26CD85}', 'VIC', 18
EXEC dbo.CreateOffering '{288FF511-A232-46d4-B88C-34697D442943}', 'Short Courses - Business Essentials', @providerId, '{91B02F7D-C003-4168-9B30-A366856F7A0C}', @parentCategoryId
EXEC dbo.CreateOfferingLocation '{288FF511-A232-46d4-B88C-34697D442943}', '{B7BB38E3-741A-493d-8D20-93264F950E40}', 'VIC', 18
EXEC dbo.CreateOffering '{0EDB4E04-8931-4422-A9F7-F47C3DBB7E17}', 'Short Courses - Career Change', @providerId, '{7A6D165B-2A46-49da-92B9-7E0E05EC5CFA}', @parentCategoryId
EXEC dbo.CreateOfferingLocation '{0EDB4E04-8931-4422-A9F7-F47C3DBB7E17}', '{C5B53C38-40CC-4eda-8D45-DD938845274B}', 'VIC', 18
EXEC dbo.CreateOffering '{6760B3B6-C692-4f30-AD9F-6B31325970EA}', 'Short Courses - Finance & Investment', @providerId, '{77BDFBA7-6041-4288-B3F9-99BEA3C098A1}', @parentCategoryId
EXEC dbo.CreateOfferingLocation '{6760B3B6-C692-4f30-AD9F-6B31325970EA}', '{9138C42C-34BA-43f2-91C6-BD6E7661995C}', 'VIC', 18
EXEC dbo.CreateOffering '{E3C62103-A293-4ad1-A64B-A74697CAC4DB}', 'Short Courses - Leadership', @providerId, '{7D57285D-52AA-4cdf-A797-7825222DC3EB}', @parentCategoryId
EXEC dbo.CreateOfferingLocation '{E3C62103-A293-4ad1-A64B-A74697CAC4DB}', '{42F4A634-257D-431c-BEDE-5B853673E434}', 'VIC', 18
EXEC dbo.CreateOffering '{CEC3F086-97F5-4746-B8A0-AA1890E76FD5}', 'Short Courses - Project Management', @providerId, '{7DEE74C9-8465-4f46-988A-AA35AA7D0A12}', @parentCategoryId
EXEC dbo.CreateOfferingLocation '{CEC3F086-97F5-4746-B8A0-AA1890E76FD5}', '{EA797EE5-917E-401a-84AD-39FE1103677C}', 'VIC', 18
EXEC dbo.CreateOffering '{FC48D0BB-0322-4a3f-AA5D-C938D4E3E0F1}', 'Short Courses - Sales & Marketing', @providerId, '{C54FFFD1-3471-4331-89A6-00FC32B5E521}', @parentCategoryId
EXEC dbo.CreateOfferingLocation '{FC48D0BB-0322-4a3f-AA5D-C938D4E3E0F1}', '{ACEADFC2-C0B6-49e2-A4E5-16F0AC0A003A}', 'VIC', 18
EXEC dbo.CreateOffering '{C715FC10-48D8-4112-B3B2-36FE92DB090B}', 'Short Courses - Small Business', @providerId, '{8B3DDEDD-FB9F-4ee1-9B29-2E34C54B795A}', @parentCategoryId
EXEC dbo.CreateOfferingLocation '{C715FC10-48D8-4112-B3B2-36FE92DB090B}', '{1F729172-D0D0-4df0-A3D2-61D9BBCF2A63}', 'VIC', 18

-- HR & Recruitment Courses

SET @parentCategoryId = '97151BFF-CE44-4D57-8DFD-031E4FBFE7D1'
UPDATE
	OfferCategory
SET
	enabled = 1
WHERE
	id = @parentCategoryId

EXEC dbo.CreateOffering '{D45FC235-55FC-408c-A46E-2EEA4373E206}', 'Short Courses - Human Resources', @providerId, '{E6B06F2F-EB69-4101-89B0-59C80FB24C09}', @parentCategoryId
EXEC dbo.CreateOfferingLocation '{D45FC235-55FC-408c-A46E-2EEA4373E206}', '{5E96FCDE-6378-400b-B07D-D1F82350577B}', 'VIC', 18

-- Project Management

SET @parentCategoryId = 'E69254E0-F5E6-47C2-899C-8041D77EEF8B'
EXEC CreateOfferCategory '{5DA7AEF5-CEBF-40b5-971E-EAC6180FACB5}', @parentcategoryId, 'Project Management'
EXEC CreateOfferCategory '6D7C1AF0-B2D2-4C63-A06F-95BB08F4F195', '{5DA7AEF5-CEBF-40b5-971E-EAC6180FACB5}', 'Short Courses - Project Management'
EXEC CreateCategoryOffering '6D7C1AF0-B2D2-4C63-A06F-95BB08F4F195', '{CEC3F086-97F5-4746-B8A0-AA1890E76FD5}'

-- Short Courses

SET @parentCategoryId = 'E69254E0-F5E6-47C2-899C-8041D77EEF8B'
EXEC CreateOfferCategory '{41AC5871-5463-4368-8146-00272EBEA326}', @parentCategoryId, 'Short Courses'
SET @parentCategoryId = '{41AC5871-5463-4368-8146-00272EBEA326}'

EXEC dbo.CreateOffering '{C889992C-F812-4231-AF59-191307D70CE4}', 'Art & Design - Fashion', @providerId, '{34620DC0-2C4A-4591-8478-4EB1426251F8}', @parentCategoryId
EXEC dbo.CreateOfferingLocation '{C889992C-F812-4231-AF59-191307D70CE4}', '{65D1B5F0-AC92-422c-AA4D-A580E928AE42}', 'VIC', 18
EXEC dbo.CreateOffering '{1363F4BD-92AA-45ac-BA6D-E0EE8CA86046}', 'Art & Design - Folio Development', @providerId, '{20E1C439-0ADF-474b-832B-FE593CC6711A}', @parentCategoryId
EXEC dbo.CreateOfferingLocation '{1363F4BD-92AA-45ac-BA6D-E0EE8CA86046}', '{89AA0C63-C2FC-4d27-A172-DCBD1C2E7A29}', 'VIC', 18
EXEC dbo.CreateOffering '{69EEB37C-78D2-4abf-B7F1-309E14BC45C2}', 'Art & Design - Interior Design', @providerId, '{F6E568ED-493A-472a-9F59-6F5550D791D6}', @parentCategoryId
EXEC dbo.CreateOfferingLocation '{69EEB37C-78D2-4abf-B7F1-309E14BC45C2}', '{37164278-4B7B-4f6a-B951-F88DAB8F2A0E}', 'VIC', 18
EXEC dbo.CreateOffering '{D5E5B331-1D7B-427e-B330-FC28A8EE2891}', 'Art & Design - Makeup', @providerId, '{941BD52A-CC89-4460-8C32-E66BA28527AA}', @parentCategoryId
EXEC dbo.CreateOfferingLocation '{D5E5B331-1D7B-427e-B330-FC28A8EE2891}', '{3951A05F-232D-4b44-B53D-D668AC19173F}', 'VIC', 18
EXEC dbo.CreateOffering '{14EC3251-3770-48b9-8605-FE1979BBE7F3}', 'Art & Design - Multimedia', @providerId, '{108099A7-276B-479f-AE21-F134842FD0E2}', @parentCategoryId
EXEC dbo.CreateOfferingLocation '{14EC3251-3770-48b9-8605-FE1979BBE7F3}', '{801FB789-517B-437a-9B4F-5B16A92280FB}', 'VIC', 18
EXEC dbo.CreateOffering '{7B3A5083-D9B8-4e80-9D58-C75944CABCF6}', 'Art & Design - Performance', @providerId, '{991D22D1-3D7A-4aec-8E4C-388DACAEC0C0}', @parentCategoryId
EXEC dbo.CreateOfferingLocation '{7B3A5083-D9B8-4e80-9D58-C75944CABCF6}', '{264031DC-7FD2-425f-9F47-C024BE88E698}', 'VIC', 18
EXEC dbo.CreateOffering '{FAA016FC-AD68-4de8-8F07-C4AA9EE1977C}', 'Art & Design - Photography', @providerId, '{CC539D8D-BE7E-4890-9B43-F969D7408AEA}', @parentCategoryId
EXEC dbo.CreateOfferingLocation '{FAA016FC-AD68-4de8-8F07-C4AA9EE1977C}', '{5E8B7F1A-1C8F-44e4-BBE6-0FE4669E4428}', 'VIC', 18
EXEC dbo.CreateOffering '{F960BED4-3778-4c64-B28E-3F4A97CB4A3F}', 'Art & Design - Visual Arts', @providerId, '{77A0EB34-E6A5-4247-8EBA-FA4ABE0BD5E2}', @parentCategoryId
EXEC dbo.CreateOfferingLocation '{F960BED4-3778-4c64-B28E-3F4A97CB4A3F}', '{3370DFCE-8451-4c4a-B706-3975683103D8}', 'VIC', 18
EXEC dbo.CreateOffering '{CA465E5F-77A0-4eea-A0A5-E9B2FEA7F030}', 'Art & Design - Visual Merchandising', @providerId, '{C57328AF-F7F4-4979-BE76-3F2696595126}', @parentCategoryId
EXEC dbo.CreateOfferingLocation '{CA465E5F-77A0-4eea-A0A5-E9B2FEA7F030}', '{7C474BF2-2E2D-440b-A9C3-DC74B95A9403}', 'VIC', 18
EXEC dbo.CreateOffering '{64D12371-AD3B-4cff-A9DA-4CE59A58A25C}', 'Business - Business Communication', @providerId, '{EBE54475-F621-4468-9B85-9DA1F16C33E3}', @parentCategoryId
EXEC dbo.CreateOfferingLocation '{64D12371-AD3B-4cff-A9DA-4CE59A58A25C}', '{A4945D14-FB43-406f-90E3-B86CFA079EEB}', 'VIC', 18
EXEC dbo.CreateOffering '{A1CC9AC9-8A4C-46a9-8852-BB04827FFA7C}', 'Business - Business Essentials', @providerId, '{C8CE4514-7FFB-43cc-91B7-4C8649CAEA7C}', @parentCategoryId
EXEC dbo.CreateOfferingLocation '{A1CC9AC9-8A4C-46a9-8852-BB04827FFA7C}', '{B8CA203F-198C-45d1-B159-C79BAB8C9803}', 'VIC', 18
EXEC dbo.CreateOffering '{3AEF0B3F-512C-4722-B0D0-24BDF68AC45C}', 'Business - Career Change', @providerId, '{A7730A1A-7379-4c4e-AB9F-9653BEE4B5D6}', @parentCategoryId
EXEC dbo.CreateOfferingLocation '{3AEF0B3F-512C-4722-B0D0-24BDF68AC45C}', '{DED53B0A-0D50-472c-92DD-E23D5AB8F0FF}', 'VIC', 18
EXEC dbo.CreateOffering '{145A407E-1394-40cb-A39E-C832DEB13414}', 'Business - Finance & Investment', @providerId, '{A54E613F-1588-4930-B502-80C4FE5B2996}', @parentCategoryId
EXEC dbo.CreateOfferingLocation '{145A407E-1394-40cb-A39E-C832DEB13414}', '{6B8AF84C-8BD0-4dbb-AF8B-E20897347556}', 'VIC', 18
EXEC dbo.CreateOffering '{09BBA61E-6511-4df2-A413-49FCF7B23E91}', 'Business - Human Resources', @providerId, '{4A04108B-3E6C-4778-A4A1-3258CCC485EC}', @parentCategoryId
EXEC dbo.CreateOfferingLocation '{09BBA61E-6511-4df2-A413-49FCF7B23E91}', '{FF7CE963-41B1-412e-AC46-676A38C93812}', 'VIC', 18
EXEC dbo.CreateOffering '{67C5F285-242A-4aaf-AAFA-459CBAACDEFE}', 'Business - Leadership', @providerId, '{E1D06671-D7F6-4ce8-9B8D-7BA5D3103770}', @parentCategoryId
EXEC dbo.CreateOfferingLocation '{67C5F285-242A-4aaf-AAFA-459CBAACDEFE}', '{AA90EE44-64E6-4c57-AD42-E2642BF057FB}', 'VIC', 18
EXEC dbo.CreateOffering '{B8EEF6C2-8AEF-468b-8A40-3E3373C694B7}', 'Business - Massage', @providerId, '{ED4CD552-91A0-42cb-8001-74C8B4E7F131}', @parentCategoryId
EXEC dbo.CreateOfferingLocation '{B8EEF6C2-8AEF-468b-8A40-3E3373C694B7}', '{A71F722F-68D5-4e98-BA81-C83B505D6B68}', 'VIC', 18
EXEC dbo.CreateOffering '{3D691122-EC28-48d4-A743-0D7AF7BFB731}', 'Business - Project Management', @providerId, '{0D26F8B3-799D-46be-A7FD-E3C995D829CF}', @parentCategoryId
EXEC dbo.CreateOfferingLocation '{3D691122-EC28-48d4-A743-0D7AF7BFB731}', '{E98EC557-BEAB-46f7-B505-6AE62D8F018E}', 'VIC', 18
EXEC dbo.CreateOffering '{CBB7097D-9A3A-4eac-BA2A-A11C03AC78DB}', 'Business - Sales & Marketing', @providerId, '{9EB85DB9-CE17-4378-A6D8-A8C85533C2B1}', @parentCategoryId
EXEC dbo.CreateOfferingLocation '{CBB7097D-9A3A-4eac-BA2A-A11C03AC78DB}', '{30C1E0C2-6F06-44c2-A392-3C396668C010}', 'VIC', 18
EXEC dbo.CreateOffering '{2FA91345-FA94-44a9-A822-3AE38BCBB439}', 'Business - Small Business', @providerId, '{9F7F9206-0849-410c-825F-F3949ADFD269}', @parentCategoryId
EXEC dbo.CreateOfferingLocation '{2FA91345-FA94-44a9-A822-3AE38BCBB439}', '{824B97F9-80A9-4a28-B134-22E05B7D83D1}', 'VIC', 18
EXEC dbo.CreateOffering '{03B9B6D4-2224-44e9-860E-BAAFEC4D42BB}', 'Business - Writing', @providerId, '{ED17C566-B448-4fe5-A990-472C08454090}', @parentCategoryId
EXEC dbo.CreateOfferingLocation '{03B9B6D4-2224-44e9-860E-BAAFEC4D42BB}', '{F042603D-710A-42a1-9529-9C39BDE7BDED}', 'VIC', 18
EXEC dbo.CreateOffering '{74D33335-24C5-4dc1-A8ED-21759308A20A}', 'Home & Garden - Floral Art', @providerId, '{C87AC0E9-6A4E-4aee-AB4A-1973C85EA84E}', @parentCategoryId
EXEC dbo.CreateOfferingLocation '{74D33335-24C5-4dc1-A8ED-21759308A20A}', '{A42FDA22-3247-4888-AA2A-514580092467}', 'VIC', 18
EXEC dbo.CreateOffering '{12ADFEEA-3695-48ca-81D2-18AB43C2F958}', 'Home & Garden - Home Improvements', @providerId, '{C704F325-4001-4175-8BDB-38D23147E972}', @parentCategoryId
EXEC dbo.CreateOfferingLocation '{12ADFEEA-3695-48ca-81D2-18AB43C2F958}', '{7C8314D0-674D-48c4-9B71-3ED9946B8C08}', 'VIC', 18
EXEC dbo.CreateOffering '{388DCE25-40B5-4977-BFDC-4555D0F823E9}', 'Home & Garden - Horticulture', @providerId, '{FAA156AF-9D9E-4721-904C-48469FD27CE9}', @parentCategoryId
EXEC dbo.CreateOfferingLocation '{388DCE25-40B5-4977-BFDC-4555D0F823E9}', '{5BE29DEB-C5C9-4ef2-B808-587925C100CB}', 'VIC', 18
EXEC dbo.CreateOffering '{D9701F05-A5E7-4ef9-B77B-53A91F95F043}', 'Hospitality - Cooking', @providerId, '{108BD9D9-70D3-43ce-985E-C0BBAAB78804}', @parentCategoryId
EXEC dbo.CreateOfferingLocation '{D9701F05-A5E7-4ef9-B77B-53A91F95F043}', '{40044E5E-B79D-43eb-89F4-03B79920EFF3}', 'VIC', 18
EXEC dbo.CreateOffering '{B285631B-5D6C-404f-A24E-7195F51B908D}', 'Hospitality - Food Safety Training', @providerId, '{1C66D8BB-6722-4a4a-934C-445883920F28}', @parentCategoryId
EXEC dbo.CreateOfferingLocation '{B285631B-5D6C-404f-A24E-7195F51B908D}', '{A4A56D0F-6659-41d1-8B18-C719835F6C47}', 'VIC', 18
EXEC dbo.CreateOffering '{B3B2A61E-EA7B-4a13-8592-4CDC62F68179}', 'Hospitality - Hospitality Training', @providerId, '{E867B6D8-D45D-48b7-B68F-9A50C199F476}', @parentCategoryId
EXEC dbo.CreateOfferingLocation '{B3B2A61E-EA7B-4a13-8592-4CDC62F68179}', '{959BE40E-60DB-480a-96B4-2236EC82EC4A}', 'VIC', 18
EXEC dbo.CreateOffering '{FC06B4D1-62FF-4bf1-92B5-A272CBD09DFE}', 'I.T. - Bookkeeping', @providerId, '{3AC62648-F469-4be9-A182-C77882C9BAF9}', @parentCategoryId
EXEC dbo.CreateOfferingLocation '{FC06B4D1-62FF-4bf1-92B5-A272CBD09DFE}', '{3AFC0C6A-452B-4530-9BEC-AC1CE1FABE5A}', 'VIC', 18
EXEC dbo.CreateOffering '{EF329691-E4DD-4c85-838F-C74692D27C6D}', 'I.T. - Computing Essentials', @providerId, '{CED27351-C542-40c3-A55F-567415FFBAE9}', @parentCategoryId
EXEC dbo.CreateOfferingLocation '{EF329691-E4DD-4c85-838F-C74692D27C6D}', '{A155D99B-E044-46a4-BFFD-20D064BCB1BC}', 'VIC', 18
EXEC dbo.CreateOffering '{9A64562A-2B65-4143-B521-0367ECF33D48}', 'I.T. - Computer Aided Design', @providerId, '{87072896-03FA-4fc7-AE5E-27ED2D597FC7}', @parentCategoryId
EXEC dbo.CreateOfferingLocation '{9A64562A-2B65-4143-B521-0367ECF33D48}', '{58BFD049-CD0C-44b2-B7D4-5CAC6E79C818}', 'VIC', 18
EXEC dbo.CreateOffering '{0B615673-DD46-4e1c-B573-14D4C3E9C201}', 'I.T. - MS Office', @providerId, '{AD95EF90-83F6-4abb-B850-930B2CF65CB0}', @parentCategoryId
EXEC dbo.CreateOfferingLocation '{0B615673-DD46-4e1c-B573-14D4C3E9C201}', '{5BCCAE1C-28D1-40e5-9521-BC7E7731D6E1}', 'VIC', 18

-- Sustainability

SET @parentCategoryId = 'E69254E0-F5E6-47C2-899C-8041D77EEF8B'
EXEC CreateOfferCategory '{D4F502B2-011D-4bcb-BF94-AF96EAFF5038}', @parentCategoryId, 'Sustainability'
SET @parentCategoryId = '{D4F502B2-011D-4bcb-BF94-AF96EAFF5038}'

EXEC dbo.CreateOffering '{E49A01C7-EA23-46ac-A110-A84AD6868B36}', 'Short Courses - Carbon Accounting', @providerId, '{6AFC6A04-A9C0-4299-8F12-AE263998FA08}', @parentCategoryId
EXEC dbo.CreateOfferingLocation '{E49A01C7-EA23-46ac-A110-A84AD6868B36}', '{2AFE8B6F-D57B-4f9e-93EB-9C831953163C}', 'VIC', 18
EXEC dbo.CreateOffering '{83EDA1F3-5265-419e-A2AD-FC764838CC4A}', 'Short Courses - Site Environment', @providerId, '{E529D4D1-0233-488d-AF5F-6189FDAB7498}', @parentCategoryId
EXEC dbo.CreateOfferingLocation '{83EDA1F3-5265-419e-A2AD-FC764838CC4A}', '{4A164200-F734-425d-9E60-CFD4FC937C71}', 'VIC', 18
EXEC dbo.CreateOffering '{E959A3BB-789E-4efc-A0E4-613382299458}', 'Short Courses - Management', @providerId, '{D457ABD4-CE95-4bea-88B4-A405E79541BA}', @parentCategoryId
EXEC dbo.CreateOfferingLocation '{E959A3BB-789E-4efc-A0E4-613382299458}', '{DCB5A4ED-A0F0-4277-BA60-EFD6387EC7FB}', 'VIC', 18
EXEC dbo.CreateOffering '{048944ED-0EAA-4ed2-8C54-9F6207735131}', 'Short Courses - Solar Grid Connect', @providerId, '{A1162611-3CB6-4899-B7DF-FE1423ED74FF}', @parentCategoryId
EXEC dbo.CreateOfferingLocation '{048944ED-0EAA-4ed2-8C54-9F6207735131}', '{ADC2F181-BE2B-4b50-8F53-E3E0402362DB}', 'VIC', 18

-- Trade Skills

SET @parentCategoryId = 'E69254E0-F5E6-47C2-899C-8041D77EEF8B'
EXEC CreateOfferCategory '{B6B96130-13AD-488e-83B7-BD044C6942FB}', @parentCategoryId, 'Trade Skills'
SET @parentCategoryId = '{B6B96130-13AD-488e-83B7-BD044C6942FB}'

EXEC dbo.CreateOffering '{76AE0F8A-70C6-47ca-B662-CD9EF523F8D2}', 'Short Courses - Electronics', @providerId, '{15C38B1C-C6CE-4210-8435-A1B5DD855AB9}', @parentCategoryId
EXEC dbo.CreateOfferingLocation '{76AE0F8A-70C6-47ca-B662-CD9EF523F8D2}', '{95AE8F5C-6431-4365-9FBD-3ADC6D3BE9EA}', 'VIC', 18
EXEC dbo.CreateOffering '{3B78F581-7EB7-4038-9FB2-0124C0AF7F0F}', 'Short Courses - Welding', @providerId, '{4C9F0140-0EB2-42b2-875C-38E4ED44B835}', @parentCategoryId
EXEC dbo.CreateOfferingLocation '{3B78F581-7EB7-4038-9FB2-0124C0AF7F0F}', '{42820A3B-002B-4d52-8418-96BC621E8F9A}', 'VIC', 18

-- Provider

SET @providerId = '{06F350F9-36F9-4785-BEF0-0D8D745849CC}'
EXEC dbo.CreateOfferProvider @providerId, 'Swinburne University of Technology - Postgraduate'

-- Business Courses

SET @parentCategoryId = 'D328CBF4-3429-4D49-A792-442F85FC7763'

EXEC dbo.CreateOffering '{D500182B-E301-4a3a-ACC0-E3286AAC3FDA}', 'Grad Cert - Business Excellence (Quality Mngt)', @providerId, '{62A116AB-0834-4a80-B031-A4F08AFF2BBD}', @parentCategoryId
EXEC dbo.CreateOfferingLocation '{D500182B-E301-4a3a-ACC0-E3286AAC3FDA}', '{4C14135E-D2DA-4bf7-A45B-435351B47AC7}', 'VIC', 18
EXEC dbo.CreateOffering '{684692EC-7356-48cd-859A-CB140062531B}', 'Grad Cert - Corporate Governance', @providerId, '{53A66688-7DB9-4ec5-9B03-16AB6DA4B7D3}', @parentCategoryId
EXEC dbo.CreateOfferingLocation '{684692EC-7356-48cd-859A-CB140062531B}', '{A5BF71D7-55A7-4beb-A686-397A5F776D67}', 'VIC', 18
EXEC dbo.CreateOffering '{8AC7FD88-01D7-43cc-AD7E-43E0028CBB59}', 'Grad Cert - Executive Administration', @providerId, '{AC2A7A98-D4E0-4801-848F-EBEEDE2EBD45}', @parentCategoryId
EXEC dbo.CreateOfferingLocation '{8AC7FD88-01D7-43cc-AD7E-43E0028CBB59}', '{4A48EEAC-36D5-4fd5-8616-25E01442A3B2}', 'VIC', 18
EXEC dbo.CreateOffering '{B822D95A-F954-4178-A62B-7862A1CF9F27}', 'Grad Cert - Financial Services', @providerId, '{D78AD006-0906-4233-A0ED-8DD4AFDF88B3}', @parentCategoryId
EXEC dbo.CreateOfferingLocation '{B822D95A-F954-4178-A62B-7862A1CF9F27}', '{7DAEC3ED-8BFE-466a-AD2C-456E9C340F02}', 'VIC', 18
EXEC dbo.CreateOffering '{E5902B35-6440-4ce7-9D67-B2BD0638E68E}', 'Grad Cert - Project Management', @providerId, '{CB1A78FB-420D-4208-BCA2-1C8083054313}', @parentCategoryId
EXEC dbo.CreateOfferingLocation '{E5902B35-6440-4ce7-9D67-B2BD0638E68E}', '{5E43D2D6-8D51-4607-A105-733EDA498B72}', 'VIC', 18
EXEC dbo.CreateOffering '{F5A3461B-7AD4-467e-950B-F818F57FCF58}', 'Grad Cert - Public Relations', @providerId, '{1C8E678D-8059-4e4d-9A39-EE8DBEAFE6FF}', @parentCategoryId
EXEC dbo.CreateOfferingLocation '{F5A3461B-7AD4-467e-950B-F818F57FCF58}', '{CBE889C6-6726-4c42-A9A6-6983DB98AE3F}', 'VIC', 18
EXEC dbo.CreateOffering '{635E3564-FDD9-443c-ABDE-1618322CEB2D}', 'Grad Cert - Retail Management', @providerId, '{4B04EF32-BBC5-46c1-B920-11DB8E503471}', @parentCategoryId
EXEC dbo.CreateOfferingLocation '{635E3564-FDD9-443c-ABDE-1618322CEB2D}', '{A652E874-E696-4d44-AF02-B6A522744E0D}', 'VIC', 18
EXEC dbo.CreateOffering '{928FB84D-1504-41e1-911F-7EFCD6BEF4D0}', 'Grad Cert - Supply Chain', @providerId, '{6DC35A95-13C5-4e44-983C-FA2B6BE0E953}', @parentCategoryId
EXEC dbo.CreateOfferingLocation '{928FB84D-1504-41e1-911F-7EFCD6BEF4D0}', '{49E502A9-2533-4520-9909-FC885ADB2266}', 'VIC', 18

-- Masters

IF NOT EXISTS (SELECT * FROM Offering WHERE id = '{26F7E52F-B191-41fe-A86F-E6779487AE8A}')
	INSERT
		Offering (id, providerId, name, enabled)
	VALUES
		('{26F7E52F-B191-41fe-A86F-E6779487AE8A}', @providerId, 'Masters', 1)
ELSE
	UPDATE
		Offering
	SET
		providerId = @providerId,
		name = 'Masters',
		enabled = 1
	WHERE
		id = '{26F7E52F-B191-41fe-A86F-E6779487AE8A}'

IF NOT EXISTS (SELECT * FROM OfferCategoryOffering WHERE categoryId = '7216177F-BEEF-410A-A86C-61A1A6D5AA6C' AND offeringId = '{26F7E52F-B191-41fe-A86F-E6779487AE8A}')
	INSERT
		OfferCategoryOffering (categoryId, offeringId)
	VALUES
		('7216177F-BEEF-410A-A86C-61A1A6D5AA6C', '{26F7E52F-B191-41fe-A86F-E6779487AE8A}')

EXEC dbo.CreateOfferingLocation '{26F7E52F-B191-41fe-A86F-E6779487AE8A}', '{36948829-27B0-4928-9124-7EB281F784C1}', 'VIC', 18

-- HR & Recruitment Courses

SET @parentCategoryId = '97151BFF-CE44-4D57-8DFD-031E4FBFE7D1'
EXEC dbo.CreateOffering '{00490FF9-41CA-41f9-8D34-867AB8CD2C55}', 'Grad Cert - HR Management', @providerId, '{BDC46A34-686D-47a1-9AFD-81EA5C053D3F}', @parentCategoryId
EXEC dbo.CreateOfferingLocation '{00490FF9-41CA-41f9-8D34-867AB8CD2C55}', '{BD1F15CC-878C-49ff-8756-D0EDE9FB1331}', 'VIC', 18

-- Project Management

EXEC CreateOfferCategory '{0AEDA0D0-2360-4c7a-8CE8-8E098CABD2B0}', '{5DA7AEF5-CEBF-40b5-971E-EAC6180FACB5}', 'Grad Cert - Corporate Governance'
EXEC CreateCategoryOffering '{0AEDA0D0-2360-4c7a-8CE8-8E098CABD2B0}', '{684692EC-7356-48cd-859A-CB140062531B}'

EXEC CreateOfferCategory '{9CE80421-5B1E-4cba-AFE2-0BAA67CCEDB3}', '{5DA7AEF5-CEBF-40b5-971E-EAC6180FACB5}', 'Grad Cert - Project Management'
EXEC CreateCategoryOffering '{9CE80421-5B1E-4cba-AFE2-0BAA67CCEDB3}', '{E5902B35-6440-4ce7-9D67-B2BD0638E68E}'

