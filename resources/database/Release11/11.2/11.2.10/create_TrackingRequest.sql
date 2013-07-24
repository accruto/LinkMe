-- TrackingCommunication

IF EXISTS (SELECT * FROM dbo.SYSOBJECTS WHERE id = object_id('dbo.TrackingRequest') AND OBJECTPROPERTY(id, 'IsUserTable') = 1)
DROP TABLE dbo.TrackingRequest
GO

CREATE TABLE dbo.TrackingRequest
(
	id UNIQUEIDENTIFIER NOT NULL,

	time DATETIME NOT NULL,
	userId UNIQUEIDENTIFIER NULL,
	sessionId NVARCHAR(128) NULL,

	url NVARCHAR(1024) NOT NULL,
	queryString NVARCHAR(1024) NULL,
	referrerUrl NVARCHAR(1024) NULL,
	referrerQueryString NVARCHAR(1024) NULL,

	clientHostName NVARCHAR(128) NULL,
	clientHostAddress NVARCHAR(128) NULL,
	clientAgent NVARCHAR(128) NULL,

	serverHostName NVARCHAR(128) NULL,
	serverProcessId INT NULL
)
GO

ALTER TABLE dbo.TrackingRequest ADD CONSTRAINT PK_TrackingRequest
	PRIMARY KEY NONCLUSTERED (id)
GO

CREATE CLUSTERED INDEX IX_TrackingRequest ON dbo.TrackingRequest
(
	time,
	userId,
	sessionId
)