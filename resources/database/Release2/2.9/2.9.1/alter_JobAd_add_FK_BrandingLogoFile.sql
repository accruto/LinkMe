ALTER TABLE linkme_owner.JobAd
ADD CONSTRAINT FK_JobAd_BrandingLogoFile
FOREIGN KEY (brandingLogoImageId) REFERENCES linkme_owner.[File] ([id])
GO
