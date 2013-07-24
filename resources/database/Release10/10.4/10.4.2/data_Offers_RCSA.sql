DECLARE @providerId UNIQUEIDENTIFIER
SET @providerId = '{A290F6ED-77E6-4db0-8834-5552024027B7}'
DECLARE @parentCategoryId UNIQUEIDENTIFIER

-- Provider

EXEC dbo.CreateOfferProvider @providerId, 'RCSA Australia & New Zealand'

-- HR & Recruitment Courses

SET @parentCategoryId = '97151BFF-CE44-4D57-8DFD-031E4FBFE7D1'
EXEC dbo.CreateOffering '{3FA94E5D-201F-4346-93D4-0EF21038EC2C}', 'Certificate IV in Human Resources', @providerId, '{60C56B6E-6BD5-4f25-AC0A-BBA9289DC227}', @parentCategoryId
EXEC dbo.CreateOffering '{9280C231-9D41-40b1-BB02-E776E86D255E}', 'Seminars, Webinars and Workshops', @providerId, '{B74C9476-3E37-4637-B3B5-E9306B2E22EC}', @parentCategoryId
EXEC dbo.CreateOffering '{26F7259E-4E41-432c-A403-8BF925AD53C9}', 'Certificate in Recruitment & Selection', @providerId, '{AFEB46D8-EE62-4c23-9410-C0F8652A83FE}', @parentCategoryId
EXEC dbo.CreateOffering '{90933795-7732-411b-AB99-F513011B3415}', 'Certificate in Talent Management', @providerId, '{883ACD4A-5108-45bb-BDBE-FC2C5F87F980}', @parentCategoryId
EXEC dbo.CreateOffering '{8435DDA3-920C-4c5f-8CEF-68EA145CE60F}', 'Certificate in OHS  Risk  Management  (Onhire Worker Services)', @providerId, '{F75F330B-C99F-44d6-8BE5-2BB6CDFAC1B6}', @parentCategoryId
EXEC dbo.CreateOffering '{BB34D7D2-A27F-4757-918B-05803906F098}', 'Diploma of Human Resources Management', @providerId, '{E3D5FBEF-264E-451c-A8CE-1F187F930C31}', @parentCategoryId
EXEC dbo.CreateOffering '{57F8D265-63B2-4ad3-BBCC-C45985F829F9}', 'Graduate  Certificate in Recruitment, Placement & Career Development', @providerId, '{596A3FC7-21A6-4e3a-AE97-9E0475EE31C6}', @parentCategoryId
EXEC dbo.CreateOffering '{3B9BCEC6-06F8-4c45-8173-E9926C737822}', 'RCSA Professional Diploma in Human Resource Consulting', @providerId, '{AAEC3FAB-F61A-4a54-888B-FDE10D7B66EC}', @parentCategoryId
  
-- HR & Recruitment

SET @parentCategoryId = 'F9D6868D-5EF4-4DE5-94BC-7474DBD700E2'
EXEC dbo.CreateOffering '{F9C45237-7B80-4dcb-8F0F-5C154BCF40DE}', 'Are you a Recruitment Professional seeking  recognised  industry accreditation?', @providerId, '{AB88D0B9-6465-43ff-8EE3-D7517E0FE6AC}', @parentCategoryId
EXEC dbo.CreateOffering '{F9C45237-7B80-4dcb-8F0F-5C154BCF40DE}', 'Are you a  representative of a company seeking industry accreditation?', @providerId, '{D62EF252-C973-4afc-8831-ADFBB5EA5AB2}', @parentCategoryId
 
