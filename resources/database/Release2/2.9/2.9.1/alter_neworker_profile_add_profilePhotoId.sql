ALTER TABLE linkme_owner.networker_profile
ADD profilePhotoId UNIQUEIDENTIFIER NULL
GO

ALTER TABLE linkme_owner.networker_profile
ADD CONSTRAINT FK_networker_profile_ProfilePhotoFile
FOREIGN KEY (profilePhotoId) REFERENCES linkme_owner.[File] ([id])
GO
