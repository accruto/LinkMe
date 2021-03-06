--   -------------------------------------------------- 
--   Generated by Enterprise Architect Version 6.1.792
--   Created On : Thursday, 14 September, 2006 
--   DBMS       : SQL Server 2000 
--   -------------------------------------------------- 

IF EXISTS (SELECT * FROM dbo.SYSOBJECTS WHERE id = object_id('linkme_owner.ProductDefinition') AND  OBJECTPROPERTY(id, 'IsUserTable') = 1)
DROP TABLE linkme_owner.ProductDefinition
GO

CREATE TABLE linkme_owner.ProductDefinition ( 
	id uniqueidentifier NOT NULL,
	type varchar(100) NOT NULL,    --  Name of the derived type of the product definition, eg. "ContactCreditProductDefinition" or "JobAdProductDefinition". This value should correspond to the table name. 
	displayName nvarchar(100) NOT NULL,
	duration bigint,
	isActive bit NOT NULL,
	name varchar(50) NOT NULL,    --  A unique, permanent name that identifies the product and is not visible to the user. Used in code to find a specific product. 
	priceExGst money,
	shortDescription nvarchar(200)
)
GO

ALTER TABLE linkme_owner.ProductDefinition
	ADD CONSTRAINT UQ_ProductDefinition_displayName UNIQUE (displayName)
GO

ALTER TABLE linkme_owner.ProductDefinition
	ADD CONSTRAINT UQ_ProductDefinition_name UNIQUE (name)
GO

ALTER TABLE linkme_owner.ProductDefinition ADD CONSTRAINT PK_ProductDefinition 
	PRIMARY KEY (id)
GO


EXEC sp_addextendedproperty 'MS_Description', 'Name of the derived type of the product definition, eg. "ContactCreditProductDefinition" or "JobAdProductDefinition". This value should correspond to the table name.', 'User', linkme_owner, 'table', ProductDefinition, 'column', type
GO




EXEC sp_addextendedproperty 'MS_Description', 'A unique, permanent name that identifies the product and is not visible to the user. Used in code to find a specific product.', 'User', linkme_owner, 'table', ProductDefinition, 'column', name
GO



