IF EXISTS (SELECT * FROM dbo.SYSOBJECTS WHERE id = object_id('linkme_owner.NonMemberSettings') AND  OBJECTPROPERTY(id, 'IsUserTable') = 1)
DROP TABLE linkme_owner.NonMemberSettings
GO

CREATE TABLE linkme_owner.NonMemberSettings
( 
	id uniqueidentifier NOT NULL,
	emailAddress nvarchar(320) NOT NULL,
	flags int NOT NULL
)
GO

CREATE UNIQUE CLUSTERED INDEX IX_NonMemberSettings_emailAddress
ON linkme_owner.NonMemberSettings (emailAddress ASC)
GO

ALTER TABLE linkme_owner.NonMemberSettings
ADD CONSTRAINT PK_NonMemberSettings PRIMARY KEY NONCLUSTERED (id)
GO
