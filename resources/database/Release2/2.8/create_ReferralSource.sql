CREATE TABLE linkme_owner.ReferralSource
(
	id UNIQUEIDENTIFIER NOT NULL,
	displayName VARCHAR(50) NOT NULL
)
GO

ALTER TABLE linkme_owner.ReferralSource ADD CONSTRAINT PK_ReferralSource
	PRIMARY KEY (id)
GO