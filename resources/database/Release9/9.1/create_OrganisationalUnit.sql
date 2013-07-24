IF EXISTS (SELECT * FROM dbo.SYSOBJECTS WHERE id = object_id('dbo.OrganisationalUnit') AND  OBJECTPROPERTY(id, 'IsUserTable') = 1)
DROP TABLE dbo.OrganisationalUnit
GO

CREATE TABLE dbo.OrganisationalUnit
( 
	[id] uniqueidentifier NOT NULL,
	parentId uniqueidentifier,
	verifiedById uniqueidentifier NOT NULL,
	accountManagerId uniqueidentifier NOT NULL
)
GO

ALTER TABLE dbo.OrganisationalUnit
ADD CONSTRAINT PK_OrganisationalUnit 
PRIMARY KEY CLUSTERED ([id])
GO

ALTER TABLE dbo.OrganisationalUnit
ADD CONSTRAINT FK_OrganisationalUnit_Organisation
FOREIGN KEY ([id]) REFERENCES dbo.Organisation ([id])
GO

ALTER TABLE dbo.OrganisationalUnit
ADD CONSTRAINT FK_OrganisationalUnit_VerifiedByAdministrator
FOREIGN KEY (verifiedById) REFERENCES dbo.Administrator ([id])
GO

ALTER TABLE dbo.OrganisationalUnit
ADD CONSTRAINT FK_OrganisationalUnit_AccountManagerAdministrator
FOREIGN KEY (accountManagerId) REFERENCES dbo.Administrator ([id])
GO
