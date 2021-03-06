IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'dbo.Product') AND type in (N'U'))
DROP TABLE dbo.Product
GO

CREATE TABLE dbo.Product
(
	id UNIQUEIDENTIFIER NOT NULL,
	name NVARCHAR(100) NOT NULL,
	enabled BIT NOT NULL,
	userTypes INT NOT NULL,
	price DECIMAL NOT NULL,
	currency INT NOT NULL
)

ALTER TABLE dbo.Product
ADD CONSTRAINT PK_Product PRIMARY KEY NONCLUSTERED
(
	id
)

CREATE UNIQUE INDEX [IX_Product_name] ON [dbo].[Product]
(
	[name]
)

