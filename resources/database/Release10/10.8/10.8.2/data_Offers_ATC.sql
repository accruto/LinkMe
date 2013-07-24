DECLARE @providerId UNIQUEIDENTIFIER
DECLARE @parentCategoryId UNIQUEIDENTIFIER
DECLARE @categoryId UNIQUEIDENTIFIER

-- Employer Benefits

SET @providerId = '{C6D25CCE-836A-48D8-901F-D9B8FCCFE50F}'
SET @parentCategoryId = '{EE9CC5B7-CB67-4C94-8538-A599AA909BC0}'

SET @categoryId = '{05FA625D-BA35-44a3-834B-0E32CB4512B3}'
EXEC dbo.CreateOfferCategory @categoryId, @parentCategoryId, 'The Australasian Talent Conference Presents'

SET @parentCategoryId = @categoryId
EXEC dbo.CreateOffering '{CF3A720C-B99D-443f-BF9D-5C1AC6C4A73B}', 'Social Media 101: A hands-on guide to Social Media for Recruitment', @providerId, '{7D13C0F6-676D-4aaa-96AF-4AC500E293ED}', @parentCategoryId
EXEC dbo.CreateOfferingCriteria '{F0BAED9B-245B-47da-9EB7-BD464965AE50}', '{CF3A720C-B99D-443f-BF9D-5C1AC6C4A73B}', 'Country', 1, 'Keywords', 'Recruitment OR HR OR Manager OR Executive OR CEO', NULL, NULL, NULL, NULL