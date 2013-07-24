ALTER TABLE linkme_owner.resume_request_summary
DROP COLUMN industry, jobCategory
GO

ALTER TABLE linkme_owner.resume_request_summary
ALTER COLUMN industryId UNIQUEIDENTIFIER NOT NULL
GO
