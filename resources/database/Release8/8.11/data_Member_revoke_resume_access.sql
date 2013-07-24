-- Feature 8138 - revoke all 2nd degree and public access to resumes.

UPDATE dbo.Member
SET secondDegreeAccess = secondDegreeAccess & ~8192
WHERE (secondDegreeAccess & 8192) = 8192

UPDATE dbo.Member
SET publicAccess = publicAccess & ~8192
WHERE (publicAccess & 8192) = 8192

GO
