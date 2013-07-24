IF EXISTS (SELECT * FROM dbo.SYSOBJECTS WHERE id = object_id('[EmailLinkStats]') AND  OBJECTPROPERTY(id, 'IsUserTable') = 1)
DROP TABLE [EmailLinkStats]
GO

CREATE TABLE [EmailLinkStats] ( 
	[id] uniqueidentifier NOT NULL,
	[file] varchar(50) NOT NULL,
	[emailStatsId] uniqueidentifier NOT NULL,
	[time] datetime NOT NULL
)
GO

ALTER TABLE [EmailLinkStats] ADD CONSTRAINT [PK_EmailLinkStats] 
	PRIMARY KEY CLUSTERED ([id])
GO

ALTER TABLE [EmailLinkStats] ADD CONSTRAINT [FK_EmailLinkStats_EmailStats] 
	FOREIGN KEY ([emailStatsId]) REFERENCES [EmailStats] ([id])
GO





