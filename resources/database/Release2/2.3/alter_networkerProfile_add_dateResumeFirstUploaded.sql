ALTER TABLE	linkme_owner.networker_profile
ADD		resumeFirstUploadedDate DATETIME
GO

-- Default existing data to their join date if they have a resume.

UPDATE	np 
SET	np.resumeFirstUploadedDate = [up].[joinDate]
FROM	linkme_owner.networker_profile as np INNER JOIN
	linkme_owner.user_profile as up ON np.id = up.id
WHERE	np.resumeComplete = 1

GO


-- Otherwise SQL mindate???? AsP.NET doesn't let us make it null

declare @mindate as Datetime
Set @mindate=-53690

UPDATE	linkme_owner.networker_profile 
SET	linkme_owner.networker_profile.resumeFirstUploadedDate = @mindate
WHERE	linkme_owner.networker_profile.resumeComplete = 0

GO