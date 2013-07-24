IF EXISTS (SELECT * FROM dbo.SYSOBJECTS WHERE id = object_id('EmailLinkStats') AND  OBJECTPROPERTY(id, 'IsUserTable') = 1)
DROP TABLE EmailLinkStats
GO

CREATE TABLE EmailLinkStats ( 
	[file] varchar(50) NOT NULL,
	emailStatsId uniqueidentifier NOT NULL,
	hits int NOT NULL
)
GO

ALTER TABLE EmailLinkStats ADD CONSTRAINT PK_EmailLinkStats 
	PRIMARY KEY CLUSTERED ([file], emailStatsId)
GO




