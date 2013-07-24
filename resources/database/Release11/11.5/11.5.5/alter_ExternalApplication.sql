UPDATE
	dbo.ExternalApplication
SET
	applicantId = NEWID()
WHERE
	applicantId IS NULL
GO

DROP INDEX dbo.ExternalApplication.IX_ExternalApplication_applicantId
GO

ALTER TABLE dbo.ExternalApplication
ALTER COLUMN applicantId UNIQUEIDENTIFIER NOT NULL
GO

CREATE NONCLUSTERED INDEX IX_ExternalApplication_applicantId ON dbo.ExternalApplication
(
	applicantId
)
GO


