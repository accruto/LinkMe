
IF EXISTS (SELECT * FROM dbo.SYSOBJECTS WHERE id = object_id('JobAdLocation') AND  OBJECTPROPERTY(id, 'IsUserTable') = 1)
DROP TABLE JobAdLocation
GO

CREATE TABLE JobAdLocation ( 
	jobAdId uniqueidentifier NOT NULL,
	locationReferenceId uniqueidentifier NOT NULL
)
GO

ALTER TABLE JobAdLocation ADD CONSTRAINT PK_JobAdLocation 
	PRIMARY KEY CLUSTERED (jobAdId, locationReferenceId)
GO

ALTER TABLE JobAdLocation ADD CONSTRAINT FK_JobAdLocation_LocationReference
	FOREIGN KEY (locationReferenceId) REFERENCES LocationReference (id)
GO

ALTER TABLE JobAdLocation ADD CONSTRAINT FK_JobAdLocation_JobAd 
	FOREIGN KEY (jobAdId) REFERENCES JobAd (id)
GO



