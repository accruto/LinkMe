



ALTER TABLE linkme_owner.User_Profile ADD emailVerified BIT NULL
GO

-- Migrate existing User Profile data into the email verified column
update linkme_owner.user_profile set 
emailVerified = 1
where active = 1
GO

update linkme_owner.user_profile set 
emailVerified = 0
where active = 0
GO