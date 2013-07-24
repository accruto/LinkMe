-- There is no existing date of birth data.

INSERT INTO dbo.Networker([id])
SELECT dbo.GuidFromString([id])
FROM linkme_owner.networker_profile
GO
