ALTER TABLE linkme_owner.user_profile_ideal_job
ADD currentJobIndustryId UNIQUEIDENTIFIER NULL
GO

ALTER TABLE linkme_owner.user_profile_ideal_job
ADD CONSTRAINT FK_user_profile_ideal_job_Industry
FOREIGN KEY(currentJobIndustryId) REFERENCES linkme_owner.Industry(id)
GO
