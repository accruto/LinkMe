DECLARE @productId UNIQUEIDENTIFIER
DECLARE @applicantCreditId UNIQUEIDENTIFIER
DECLARE @jobAdCreditId UNIQUEIDENTIFIER

SET @applicantCreditId = 'E63229B6-1F14-4F3A-9707-B09B375DA3A5'
SET @jobAdCreditId = 'AE51B67A-9871-4916-B38A-C9BB3B27B83D'	

-- Create a "old" disabled job ad product for testing.

SET @productId = '{43F6DEC7-7458-493A-81AD-AF7177B989D5}'

DELETE
	dbo.ProductCreditAdjustment
WHERE
	productId = @productId

DELETE
	dbo.Product
WHERE
	id = @productId

INSERT
	dbo.Product (id, name, enabled, userTypes, price, currency)
VALUES
	(@productId, '20 Job Ad Pack', 0, 2, 100, 36)

INSERT
	dbo.ProductCreditAdjustment (productId, creditId, quantity, duration)
VALUES
	(@productId, @jobAdCreditId, 20, 315360000000000)

INSERT
	dbo.ProductCreditAdjustment (productId, creditId, quantity, duration)
VALUES
	(@productId, @applicantCreditId, NULL, 315360000000000)

-- 1 Job Ad

SET @productId = '{31FE57BA-076A-4EFD-AF1F-62BC6E4180E7}'

DELETE
	dbo.ProductCreditAdjustment
WHERE
	productId = @productId

DELETE
	dbo.Product
WHERE
	id = @productId

INSERT
	dbo.Product (id, name, enabled, userTypes, price, currency)
VALUES
	(@productId, '1 Job Ad', 1, 2, 50, 36)

INSERT
	dbo.ProductCreditAdjustment (productId, creditId, quantity, duration)
VALUES
	(@productId, @jobAdCreditId, 1, 315360000000000)

INSERT
	dbo.ProductCreditAdjustment (productId, creditId, quantity, duration)
VALUES
	(@productId, @applicantCreditId, NULL, 315360000000000)

-- 5 Job Ads

SET @productId = '{F9481346-4B6A-47D6-A681-C531E12F67E7}'

DELETE
	dbo.ProductCreditAdjustment
WHERE
	productId = @productId

DELETE
	dbo.Product
WHERE
	id = @productId

INSERT
	dbo.Product (id, name, enabled, userTypes, price, currency)
VALUES
	(@productId, '5 Job Ads', 1, 2, 225, 36)

INSERT
	dbo.ProductCreditAdjustment (productId, creditId, quantity, duration)
VALUES
	(@productId, @jobAdCreditId, 5, 315360000000000)

INSERT
	dbo.ProductCreditAdjustment (productId, creditId, quantity, duration)
VALUES
	(@productId, @applicantCreditId, NULL, 315360000000000)

-- 10 Job Ads

SET @productId = '{E3909542-C381-4745-9B2E-F7B5C197C354}'

DELETE
	dbo.ProductCreditAdjustment
WHERE
	productId = @productId

DELETE
	dbo.Product
WHERE
	id = @productId

INSERT
	dbo.Product (id, name, enabled, userTypes, price, currency)
VALUES
	(@productId, '10 Job Ads', 1, 2, 425, 36)

INSERT
	dbo.ProductCreditAdjustment (productId, creditId, quantity, duration)
VALUES
	(@productId, @jobAdCreditId, 10, 315360000000000)

INSERT
	dbo.ProductCreditAdjustment (productId, creditId, quantity, duration)
VALUES
	(@productId, @applicantCreditId, NULL, 315360000000000)

-- 20 Job Ads

SET @productId = '{069AB044-18B8-48D8-B3E1-DF2105739E9A}'

DELETE
	dbo.ProductCreditAdjustment
WHERE
	productId = @productId

DELETE
	dbo.Product
WHERE
	id = @productId

INSERT
	dbo.Product (id, name, enabled, userTypes, price, currency)
VALUES
	(@productId, '20 Job Ads', 1, 2, 800, 36)

INSERT
	dbo.ProductCreditAdjustment (productId, creditId, quantity, duration)
VALUES
	(@productId, @jobAdCreditId, 20, 315360000000000)

INSERT
	dbo.ProductCreditAdjustment (productId, creditId, quantity, duration)
VALUES
	(@productId, @applicantCreditId, NULL, 315360000000000)

-- 40 Job Ads

SET @productId = '{B0BB7CEB-7421-4B97-B92C-B81B000618F8}'

DELETE
	dbo.ProductCreditAdjustment
WHERE
	productId = @productId

DELETE
	dbo.Product
WHERE
	id = @productId

INSERT
	dbo.Product (id, name, enabled, userTypes, price, currency)
VALUES
	(@productId, '40 Job Ads', 1, 2, 1400, 36)

INSERT
	dbo.ProductCreditAdjustment (productId, creditId, quantity, duration)
VALUES
	(@productId, @jobAdCreditId, 40, 315360000000000)

INSERT
	dbo.ProductCreditAdjustment (productId, creditId, quantity, duration)
VALUES
	(@productId, @applicantCreditId, NULL, 315360000000000)


