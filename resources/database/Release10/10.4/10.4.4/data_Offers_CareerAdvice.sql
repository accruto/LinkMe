DECLARE @providerId UNIQUEIDENTIFIER
SET @providerId = '{A70F4D73-4A05-480e-989D-7D8E0E8CBFAA}'
DECLARE @parentCategoryId UNIQUEIDENTIFIER

-- Provider

EXEC dbo.CreateOfferProvider @providerId, 'LinkMe'

SET @parentCategoryId = 'A0D6B83C-B185-46FA-8BD5-E76FAC01D906'
EXEC dbo.CreateOfferCategory '{D44F8915-BC7C-4115-83F6-77B0C2562064}', @parentCategoryId, 'Complimentary Career Advice'

SET @parentCategoryId = '{D44F8915-BC7C-4115-83F6-77B0C2562064}'
EXEC dbo.CreateOffering '{8E043404-66B2-4cbc-9FA9-5C8E80030FE4}', 'Would you like some basic, free career advice?', @providerId, '{6D8408FC-8035-4fcf-AF21-8A1EE3913023}', @parentcategoryId
