IF EXISTS (SELECT * FROM dbo.SYSOBJECTS WHERE id = object_id('dbo.IgnoredNetworkMatch') AND  OBJECTPROPERTY(id, 'IsUserTable') = 1)
DROP TABLE IgnoredNetworkMatch
GO

CREATE TABLE dbo.IgnoredNetworkMatch ( 
	id uniqueidentifier NOT NULL,
	category NetworkMatchCategory NOT NULL,
	[time] datetime NOT NULL,
	ignorerId uniqueidentifier NOT NULL,
	ignoredId uniqueidentifier NOT NULL
)
GO

ALTER TABLE IgnoredNetworkMatch
	ADD CONSTRAINT UQ_IgnoredNetworkMatch UNIQUE (category, ignorerId, ignoredId)
GO

ALTER TABLE IgnoredNetworkMatch ADD CONSTRAINT PK_IgnoredNetworkMatch 
	PRIMARY KEY CLUSTERED (id)
GO

ALTER TABLE IgnoredNetworkMatch ADD CONSTRAINT FK_IgnoredNetworkMatch_Ignored 
	FOREIGN KEY (ignoredId) REFERENCES Networker (id)
GO

ALTER TABLE IgnoredNetworkMatch ADD CONSTRAINT FK_IgnoredNetworkMatch_Ignorer 
	FOREIGN KEY (ignorerId) REFERENCES Networker (id)
GO
