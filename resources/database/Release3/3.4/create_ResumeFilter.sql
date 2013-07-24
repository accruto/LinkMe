if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[ResumeFilter]') and OBJECTPROPERTY(id, N'IsView') = 1)
drop view [dbo].[ResumeFilter]
GO

SET ARITHABORT ON
GO

CREATE VIEW dbo.ResumeFilter ([id], candidateId, lastEditedTime, flags, employerAccess, desiredJobTypes,
	desiredSalaryLower, desiredSalaryUpper, localityId, countrySubdivisionId, relocationPreference)
WITH SCHEMABINDING
AS
SELECT r.id, r.candidateId, r.lastEditedTime, ru.flags, m.employerAccess, c.desiredJobTypes, c.desiredSalaryLower,
	c.desiredSalaryUpper, lr.localityId, lr.countrySubdivisionId, c.relocationPreference
FROM dbo.Resume r
INNER JOIN dbo.Member m
ON r.candidateId = m.[id]
INNER JOIN dbo.Candidate c
ON r.candidateId = c.[id]
INNER JOIN dbo.Address a
ON m.addressId = a.[id]
INNER JOIN dbo.LocationReference lr
ON a.locationReferenceId = lr.[id]
INNER JOIN dbo.RegisteredUser ru
ON r.candidateId = ru.[id]

GO

CREATE UNIQUE CLUSTERED INDEX UQ_ResumeFilter
ON dbo.ResumeFilter ([id])
GO
