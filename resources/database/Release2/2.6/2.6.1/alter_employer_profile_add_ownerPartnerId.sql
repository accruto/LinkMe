ALTER TABLE linkme_owner.employer_profile
ADD ownerPartnerId UNIQUEIDENTIFIER NULL
GO

ALTER TABLE linkme_owner.employer_profile
ADD CONSTRAINT FK_employer_profile_ServicePartner
FOREIGN KEY (ownerPartnerId)
REFERENCES linkme_owner.ServicePartner (id)
GO
