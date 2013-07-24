IF EXISTS (SELECT * FROM dbo.SYSOBJECTS WHERE id = object_id('[EmailSendStats]') AND  OBJECTPROPERTY(id, 'IsUserTable') = 1)
DROP TABLE [EmailSendStats]
GO

CREATE TABLE [EmailSendStats] ( 
	[id] uniqueidentifier NOT NULL,
	[emailStatsId] uniqueidentifier NOT NULL,
	[time] datetime NOT NULL
)
GO

ALTER TABLE [EmailSendStats] ADD CONSTRAINT [PK_EmailSendStats] 
	PRIMARY KEY CLUSTERED ([id])
GO

ALTER TABLE [EmailSendStats] ADD CONSTRAINT [FK_EmailSendStats_EmailStats] 
	FOREIGN KEY ([emailStatsId]) REFERENCES [EmailStats] ([id])
GO




