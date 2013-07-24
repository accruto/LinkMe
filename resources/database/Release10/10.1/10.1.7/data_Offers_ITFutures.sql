DECLARE @providerId UNIQUEIDENTIFIER
SET @providerId = '20D4488A-9852-40AB-ABF8-B5FE1F3A5057'

-- Alternative Careers

DECLARE @parentCategoryId UNIQUEIDENTIFIER
SET @parentCategoryId = '385188B4-D205-46B5-AA18-6170B9623407'

EXEC dbo.CreateOffering '{C2DE11F9-043A-4f05-A881-6C7D73C475B6}', 'Help me further my IT career', @providerId, '{2C9632B1-A7DF-4a40-8ED3-41BD5F28C3E4}', @parentCategoryId
