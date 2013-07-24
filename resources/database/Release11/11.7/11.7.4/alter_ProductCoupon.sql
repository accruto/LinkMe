ALTER TABLE dbo.ProductCoupon
ADD percentageDiscount DECIMAL(18, 18) NULL

ALTER TABLE dbo.ProductCoupon
ADD fixedDiscount DECIMAL(18, 2) NULL

ALTER TABLE dbo.ProductCoupon
DROP COLUMN discount