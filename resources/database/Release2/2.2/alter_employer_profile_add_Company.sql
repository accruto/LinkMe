ALTER TABLE linkme_owner.employer_profile
ADD companyId UNIQUEIDENTIFIER
GO

ALTER TABLE linkme_owner.employer_profile ADD CONSTRAINT FK_employer_profile_Company
	FOREIGN KEY (companyId) REFERENCES linkme_owner.Company (id)
GO
