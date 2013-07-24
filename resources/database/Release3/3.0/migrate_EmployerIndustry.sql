INSERT INTO dbo.EmployerIndustry(employerId, industryId)
SELECT dbo.GuidFromString(ep.[id]), industryId
FROM linkme_owner.employer_profile ep
WHERE industryId IS NOT NULL
GO
