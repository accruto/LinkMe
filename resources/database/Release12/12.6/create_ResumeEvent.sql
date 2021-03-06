IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'dbo.ResumeEvent') AND type in (N'U'))
DROP TABLE dbo.ResumeEvent
GO

CREATE TABLE dbo.ResumeEvent
(
	id UNIQUEIDENTIFIER NOT NULL,
	time DATETIME NOT NULL,
	eventType INT NOT NULL,
	candidateId UNIQUEIDENTIFIER NOT NULL,
	resumeId UNIQUEIDENTIFIER NOT NULL,
	resumeCreated BIT NOT NULL
)

ALTER TABLE dbo.ResumeEvent
ADD CONSTRAINT PK_ResumeEvent PRIMARY KEY NONCLUSTERED
(
	id
)

CREATE CLUSTERED INDEX [IX_ResumeEvent_time] ON [dbo].[ResumeEvent] 
(
	time ASC
)

GO
