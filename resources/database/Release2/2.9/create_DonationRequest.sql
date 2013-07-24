IF EXISTS (SELECT * FROM dbo.SYSOBJECTS WHERE id = object_id('linkme_owner.DonationRequest') AND  OBJECTPROPERTY(id, 'IsUserTable') = 1)
DROP TABLE linkme_owner.DonationRequest
GO

CREATE TABLE linkme_owner.DonationRequest ( 
	amount money NOT NULL,
	donationRecipientId uniqueidentifier NOT NULL,
	id uniqueidentifier NOT NULL
	UNIQUE(donationRecipientId, amount)
)
GO

ALTER TABLE linkme_owner.DonationRequest ADD CONSTRAINT PK_DonationRequest 
	PRIMARY KEY CLUSTERED (id)
GO

ALTER TABLE linkme_owner.DonationRequest ADD CONSTRAINT FK_DonationRequest_DonationRecipient 
	FOREIGN KEY (donationRecipientId) REFERENCES linkme_owner.DonationRecipient (id)
GO


ALTER TABLE linkme_owner.DonationRequest ADD 
	CONSTRAINT UQ_recipient_amount UNIQUE  NONCLUSTERED 
	(
		donationRecipientId,
		amount
	)  ON [PRIMARY] 
GO


