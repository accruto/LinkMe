ALTER TABLE linkme_owner.employer_profile
ADD industryId UNIQUEIDENTIFIER NULL
GO

ALTER TABLE linkme_owner.employer_profile
ADD CONSTRAINT FK_employer_profile_Industry
FOREIGN KEY(industryId) REFERENCES linkme_owner.Industry(id)
GO
