DECLARE @providerId UNIQUEIDENTIFIER
SET @providerId = '{B8791540-BEC7-4994-AE3A-4183200BA8B4}'

DECLARE @categoryId UNIQUEIDENTIFIER
DECLARE @parentCategoryId UNIQUEIDENTIFIER
DECLARE @offeringId UNIQUEIDENTIFIER

-- Provider

EXEC dbo.CreateOfferProvider @providerId, 'HR National'

-- Help me start my own recruitment business

SET @parentCategoryId = '385188B4-D205-46B5-AA18-6170B9623407'
SET @categoryId = '{2D081A08-5F63-445d-AAC1-F5A5E989B294}'
EXEC dbo.CreateOfferCategory @categoryId, @parentCategoryId, 'Help me start my own recruitment business'

SET @parentCategoryId = '{2D081A08-5F63-445d-AAC1-F5A5E989B294}'
SET @offeringId = '{C0A1738B-F857-40a0-B167-53AF4163AC3F}'
EXEC dbo.CreateOffering @offeringId, 'I am a Recruitment Professional looking to start my own business', @providerId, '{7CA13286-941F-487a-A632-435236AAF803}', @parentCategoryId

DECLARE @value1 DECIMAL
DECLARE @value2 DECIMAL

SET @value1 = 75000
SET @value2 = 150000
EXEC dbo.CreateOfferingCriteria '{D10DC73A-A004-4f9d-82AB-60D74114FD35}', @offeringId, 'JobTitle', 'Recruitment Consultant', 'Country', 1, 'SalaryLowerBound', @value1, 'SalaryUpperBound', @value2

SET @offeringId = '{5D26D39A-8854-4d38-BF66-B89974979DCF}'
EXEC dbo.CreateOffering @offeringId, 'I am an experienced people manager looking to use my skills in a new area', @providerId, '{6845262B-11F8-4a87-AE5A-E4E9A30019A8}', @parentCategoryId

SET @value1 = 80000
SET @value2 = 200000
EXEC dbo.CreateOfferingCriteria '{01A1821F-D1C7-4384-A46A-D7A014FC037C}', @offeringId, 'Keywords', '("HR Manager" OR "Operations Manager" OR "Sales Manager"")', 'SalaryLowerBound', @value1, 'SalaryUpperBound', @value2, NULL, NULL

SET @offeringId = '{D96675BE-3A31-4b0a-BD42-9BDC16E6E14C}'
EXEC dbo.CreateOffering @offeringId, 'I am an experienced sales professional looking to use my skills in a new area', @providerId, '{D948209D-762F-4690-9A5B-7FB335275946}', @parentCategoryId

EXEC dbo.CreateOfferingCriteria '{FCC6480A-233E-4cd9-BE32-18C704FB37C9}', @offeringId, 'Keywords', '(BDM OR "Sales Manager"")', 'SalaryLowerBound', @value1, 'SalaryUpperBound', @value2, NULL, NULL

-- Professionals & managers! Seeking a change? A career in recruitment awaits

SET @parentCategoryId = '385188B4-D205-46B5-AA18-6170B9623407'
SET @categoryId = '{00145E35-5E5B-4b54-AF83-5E0A235BE670}'
EXEC dbo.CreateOfferCategory @categoryId, @parentCategoryId, 'Professionals & managers! Seeking a change? A career in recruitment awaits'

SET @parentCategoryId = '{00145E35-5E5B-4b54-AF83-5E0A235BE670}'
SET @categoryId = '{16A03AF1-38B3-4165-B760-02B798ED1C17}'
SET @offeringId = '{5D26D39A-8854-4d38-BF66-B89974979DCF}'
EXEC dbo.CreateOfferCategory @categoryId, @parentCategoryId, 'I am an experienced people manager looking to use my skills in a new area'
EXEC dbo.CreateCategoryOffering @categoryId, @offeringId

SET @categoryId = '{C5F076D1-7EDF-4d09-9603-2F989477DE1F}'
SET @offeringId = '{D96675BE-3A31-4b0a-BD42-9BDC16E6E14C}'
EXEC dbo.CreateOfferCategory @categoryId, @parentCategoryId, 'I am an experienced sales professional looking to use my skills in a new area'
EXEC dbo.CreateCategoryOffering @categoryId, @offeringId

-- Sales professionals! Seeking a change? A recruitment career awaits!

SET @parentCategoryId = '385188B4-D205-46B5-AA18-6170B9623407'
SET @categoryId = '{81FD9B48-A758-4d9f-8DCC-2E5E5AE6F76A}'
EXEC dbo.CreateOfferCategory @categoryId, @parentCategoryId, 'Sales professionals! Seeking a change? A recruitment career awaits!'

SET @parentCategoryId = '{81FD9B48-A758-4d9f-8DCC-2E5E5AE6F76A}'
SET @categoryId = '{CC440914-A16E-4eaf-8F8E-6B9F27C357B1}'
SET @offeringId = '{D96675BE-3A31-4b0a-BD42-9BDC16E6E14C}'
EXEC dbo.CreateOfferCategory @categoryId, @parentCategoryId, 'I am an experienced sales professional looking to use my skills in a new area'
EXEC dbo.CreateCategoryOffering @categoryId, @offeringId
