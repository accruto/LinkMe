-- Create the indexed view for use in resume filtering. The realisation of this view is stored permanently in the database.
SET ARITHABORT ON

GO

CREATE VIEW dbo.ResumeFilter (id, lastEditedTime, flags, employerAccess, desiredJobTypes, desiredSalaryLower, desiredSalaryUpper, localityId, countrySubdivisionId)
WITH SCHEMABINDING
AS
SELECT r.id, r.lastEditedTime, ru.flags, m.employerAccess, c.desiredJobTypes, c.desiredSalaryLower, c.desiredSalaryUpper, a.localityId, a.countrySubdivisionId
FROM dbo.Resume r
INNER JOIN dbo.Member m ON r.candidateId = m.id
INNER JOIN dbo.Candidate c ON r.candidateId = c.[id]
INNER JOIN dbo.Address a ON m.addressId = a.[id]
INNER JOIN dbo.RegisteredUser ru ON r.candidateId = ru.id
--WHERE r.lensXml IS NOT NULL -- uncomment this when lensXml becomes varchar(max)

GO

CREATE UNIQUE CLUSTERED INDEX [UQ_ResumeFilter] ON [dbo].[ResumeFilter] 
(
	[id] ASC
)

GO