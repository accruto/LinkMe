IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Group_SmallImageFile]') AND parent_object_id = OBJECT_ID(N'[dbo].[Group]'))
ALTER TABLE [dbo].[Group] DROP CONSTRAINT [FK_Group_SmallImageFile]

IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Group_ImageFile]') AND parent_object_id = OBJECT_ID(N'[dbo].[Group]'))
ALTER TABLE [dbo].[Group] DROP CONSTRAINT [FK_Group_ImageFile]

IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_GroupEvent_FileReference]') AND parent_object_id = OBJECT_ID(N'[dbo].[GroupEvent]'))
ALTER TABLE [dbo].[GroupEvent] DROP CONSTRAINT [FK_GroupEvent_FileReference]

IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_JobApplication_ResumeAttachmentFile]') AND parent_object_id = OBJECT_ID(N'[dbo].[JobApplication]'))
ALTER TABLE [dbo].[JobApplication] DROP CONSTRAINT [FK_JobApplication_ResumeAttachmentFile]

IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_JobAd_BrandingLogoFile]') AND parent_object_id = OBJECT_ID(N'[dbo].[JobAd]'))
ALTER TABLE [dbo].[JobAd] DROP CONSTRAINT [FK_JobAd_BrandingLogoFile]

IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_UserMessageAttachment_FileReference]') AND parent_object_id = OBJECT_ID(N'[dbo].[UserMessageAttachment]'))
ALTER TABLE [dbo].[UserMessageAttachment] DROP CONSTRAINT [FK_UserMessageAttachment_FileReference]

IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Member_ProfilePhotoFile]') AND parent_object_id = OBJECT_ID(N'[dbo].[Member]'))
ALTER TABLE [dbo].[Member] DROP CONSTRAINT [FK_Member_ProfilePhotoFile]

IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_CandidateResumeFile_FileReference]') AND parent_object_id = OBJECT_ID(N'[dbo].[CandidateResumeFile]'))
ALTER TABLE [dbo].[CandidateResumeFile] DROP CONSTRAINT [FK_CandidateResumeFile_FileReference]

-- Turn the primary key into a non-clustered index.

IF  EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[FileReference]') AND name = N'PK_FileReference')
ALTER TABLE [dbo].[FileReference] DROP CONSTRAINT [PK_FileReference]
GO

ALTER TABLE [dbo].[FileReference]
ADD CONSTRAINT [PK_FileReference] PRIMARY KEY NONCLUSTERED
(
	[id]
)
GO

-- Add index for mimetype and name

IF  EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[FileReference]') AND name = N'IX_FileReference_name_mimeType')
DROP INDEX [IX_FileReference_name_mimeType] ON [dbo].[FileReference]
GO

CREATE CLUSTERED INDEX [IX_FileReference_name_mimeType] ON [dbo].[FileReference]
(
	[name],
	[mimeType]
)
GO

ALTER TABLE dbo.[Group]
ADD CONSTRAINT [FK_Group_SmallImageFile] FOREIGN KEY (smallImageFileReferenceId)
REFERENCES dbo.FileReference (id)

ALTER TABLE dbo.[Group]
ADD CONSTRAINT [FK_Group_ImageFile] FOREIGN KEY (imageFileReferenceId)
REFERENCES dbo.FileReference (id)

ALTER TABLE dbo.[GroupEvent]
ADD CONSTRAINT [FK_GroupEvent_FileReference] FOREIGN KEY (imageFileReferenceId)
REFERENCES dbo.FileReference (id)

ALTER TABLE dbo.[JobApplication]
ADD CONSTRAINT [FK_JobApplication_ResumeAttachmentFile] FOREIGN KEY (resumeAttachmentFileId)
REFERENCES dbo.FileReference (id)

ALTER TABLE dbo.[JobAd]
ADD CONSTRAINT [FK_JobAd_BrandingLogoFile] FOREIGN KEY (brandingLogoImageId)
REFERENCES dbo.FileReference (id)

ALTER TABLE dbo.[UserMessageAttachment]
ADD CONSTRAINT [FK_UserMessageAttachment_FileReference] FOREIGN KEY (fileId)
REFERENCES dbo.FileReference (id)

ALTER TABLE dbo.[Member]
ADD CONSTRAINT [FK_Member_ProfilePhotoFile] FOREIGN KEY (profilePhotoId)
REFERENCES dbo.FileReference (id)

ALTER TABLE dbo.[CandidateResumeFile]
ADD CONSTRAINT [FK_CandidateResumeFile_FileReference] FOREIGN KEY (fileId)
REFERENCES dbo.FileReference (id)

