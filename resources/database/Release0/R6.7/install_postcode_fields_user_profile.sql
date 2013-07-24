


-- drop columns (may error)
ALTER TABLE linkme_owner.user_profile
DROP COLUMN postcode 
GO

ALTER TABLE linkme_owner.user_profile
DROP COLUMN country 
GO

alter table linkme_owner.user_profile add postcode varchar(10) NULL
GO

alter table linkme_owner.user_profile add country varchar(100) NULL
GO

-- Migrate existing Networker data into the base table columns
update linkme_owner.user_profile set 
postcode = (select np.postcode FROM linkme_owner.networker_profile np where np.id = linkme_owner.user_profile.id),
country = (select np.country FROM linkme_owner.networker_profile np where np.id = linkme_owner.user_profile.id)
GO


