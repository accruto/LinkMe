


-- drop columns (may error)
ALTER TABLE linkme_owner.networker_profile
ADD postcode varchar(10) NULL
GO

ALTER TABLE linkme_owner.networker_profile
ADD country varchar(100) NULL
GO


-- Migrate existing Networker data into the base table columns
update linkme_owner.networker_profile set 
postcode = (select up.postcode FROM linkme_owner.user_profile up where up.id = linkme_owner.networker_profile.id),
country = (select up.country FROM linkme_owner.user_profile up where up.id = linkme_owner.networker_profile.id)
GO


