-- Create a view to maintain the name uniqueness

IF  EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'dbo.Product') AND name = N'IX_Product_name')
DROP INDEX IX_Product_name ON dbo.Product
GO

CREATE VIEW ProductName WITH SCHEMABINDING
AS
SELECT name FROM dbo.Product
WHERE enabled = 1
GO

CREATE UNIQUE CLUSTERED INDEX UQ_Product_name
ON ProductName (name)
GO

