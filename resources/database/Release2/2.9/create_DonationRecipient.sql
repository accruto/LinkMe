IF EXISTS (SELECT * FROM dbo.SYSOBJECTS WHERE id = object_id('linkme_owner.DonationRecipient') AND  OBJECTPROPERTY(id, 'IsUserTable') = 1)
DROP TABLE linkme_owner.DonationRecipient
GO

CREATE TABLE linkme_owner.DonationRecipient ( 
	displayName nvarchar(200) NOT NULL,
	isActive bit NOT NULL,
	id uniqueidentifier NOT NULL
)
GO

ALTER TABLE linkme_owner.DonationRecipient ADD CONSTRAINT PK_DonationRecipient 
	PRIMARY KEY CLUSTERED (id)
GO




