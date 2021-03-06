IF  EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[Employer]') AND name = N'IX_Employer_organisation')
DROP INDEX [IX_Employer_organisation] ON [dbo].[Employer] WITH ( ONLINE = OFF )
GO

CREATE INDEX IX_Employer_organisation ON dbo.Employer
(
	organisationId,
	[id]
)
GO
