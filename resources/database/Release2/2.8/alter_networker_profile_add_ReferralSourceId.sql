ALTER TABLE linkme_owner.networker_profile
ADD referralSourceId UNIQUEIDENTIFIER
GO 

ALTER TABLE linkme_owner.networker_profile
ADD CONSTRAINT FK_referralSourceId FOREIGN KEY (referralSourceId) REFERENCES linkme_owner.ReferralSource (id)
GO
