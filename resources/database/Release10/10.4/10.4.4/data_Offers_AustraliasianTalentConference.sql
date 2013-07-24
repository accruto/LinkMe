DECLARE @providerId UNIQUEIDENTIFIER
SET @providerId = '{C6D25CCE-836A-48d8-901F-D9B8FCCFE50F}'
DECLARE @parentCategoryId UNIQUEIDENTIFIER

-- Provider

EXEC dbo.CreateOfferProvider @providerId, 'Australasian Talent Conference'

SET @parentCategoryId = NULL
EXEC dbo.CreateOfferCategory '{EE9CC5B7-CB67-4c94-8538-A599AA909BC0}', @parentCategoryId, 'Conferences & Events (Paid)'

SET @parentCategoryId = '{EE9CC5B7-CB67-4c94-8538-A599AA909BC0}'
EXEC dbo.CreateOffering '{AC1BA9A1-8FD6-4070-AEFE-04261387BE39}', 'Talent sourcing and acquisition', @providerId, '{7460567E-3DF3-4c4e-819E-2C042F5A13C8}', @parentcategoryId
