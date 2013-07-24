ALTER TABLE linkme_owner.JobApplication
ADD resumeId varchar(100), resumeAttachmentFileId uniqueidentifier
GO

ALTER TABLE linkme_owner.JobApplication ADD CONSTRAINT FK_JobApplication_File 
	FOREIGN KEY (resumeAttachmentFileId) REFERENCES linkme_owner.[File] (id)
GO

ALTER TABLE linkme_owner.JobApplication ADD CONSTRAINT FK_JobApplication_networker_resume_data 
	FOREIGN KEY (resumeId) REFERENCES linkme_owner.networker_resume_data (id)
GO
