ALTER TABLE linkme_owner.resume_request_summary
ADD industryId UNIQUEIDENTIFIER NULL
GO

ALTER TABLE linkme_owner.resume_request_summary
ADD CONSTRAINT FK_resume_request_summary_Industry
FOREIGN KEY(industryId) REFERENCES linkme_owner.Industry(id)
GO
