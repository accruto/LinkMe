
-- unique userId fix
DROP INDEX linkme_owner.User_Profile.i_user_profile_userId 
GO
CREATE UNIQUE NONCLUSTERED  INDEX i_user_profile_userId on  linkme_owner.User_Profile (userId)
GO
