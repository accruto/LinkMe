ALTER TABLE linkme_owner.user_profile_ideal_job
ADD savedJobSearchId UNIQUEIDENTIFIER
GO

ALTER TABLE linkme_owner.user_profile_ideal_job
ADD CONSTRAINT FK_user_profile_ideal_job_SavedJobSearch
FOREIGN KEY (savedJobSearchId) REFERENCES linkme_owner.SavedJobSearch (id)
GO
