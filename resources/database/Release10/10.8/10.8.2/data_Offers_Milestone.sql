DECLARE @providerId UNIQUEIDENTIFIER
DECLARE @parentCategoryId UNIQUEIDENTIFIER
DECLARE @categoryId UNIQUEIDENTIFIER

-- Employer Benefits

SET @providerId = '{A4138F25-BD3A-46cb-9A1A-D335359A22E6}'
EXEC dbo.CreateOfferProvider @providerId, 'Milestone Learning Employers'

SET @categoryId = '{CE1FD7FB-A333-44ec-904B-1CDE392943B5}'
EXEC dbo.CreateOfferCategory @categoryId, NULL, 'Employer Benefits'

SET @parentCategoryId = @categoryId

SET @categoryId = '{BD017201-64C3-45e2-93BD-826C1368B2CB}'
EXEC dbo.CreateOfferCategory @categoryId, @parentCategoryId, 'I am an employer or manager worried about staff retention in a post GFC market'

SET @categoryId = '{8B8B238F-1B8B-48e8-8CC4-1560DC6A0A0E}'
EXEC dbo.CreateOfferCategory @categoryId, @parentCategoryId, 'I am an employer or manager looking to increase my revenue and profitability'

SET @parentCategoryId = '{BD017201-64C3-45e2-93BD-826C1368B2CB}'
EXEC dbo.CreateOffering '{9F91635A-E42D-472c-BBBF-85590E0A431E}', 'I am interested in providing group sales training for my staff', @providerId, '{D5690F26-4BA8-432b-8F18-28C9111ACE52}', @parentCategoryId
EXEC dbo.CreateOffering '{27E143E3-2212-4d83-8A62-11DCC23D7E1A}', 'I am interested in providing group leadership training for my staff', @providerId, '{4F188C59-FF3A-4844-9081-AF86F6E597B5}', @parentCategoryId

SET @parentCategoryId = '{8B8B238F-1B8B-48e8-8CC4-1560DC6A0A0E}'
EXEC dbo.CreateOffering '{027A9457-0482-4079-8DE6-1596713B8E29}', 'I would like to increase the skills of my existing managers', @providerId, '{453E4986-E35E-41c3-9ADA-116A9CC29896}', @parentCategoryId
EXEC dbo.CreateOffering '{E25688EF-72C0-4139-8CE4-97A9CFAAEE43}', 'I would like to improve the performance of my sales team', @providerId, '{049FC5E0-FFB6-4e44-813D-9EDF0824B32F}', @parentCategoryId

