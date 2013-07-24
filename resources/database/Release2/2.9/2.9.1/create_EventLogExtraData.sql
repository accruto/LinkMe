IF EXISTS (SELECT * FROM dbo.SYSOBJECTS WHERE id = object_id('linkme_owner.EventLogExtraData') AND  OBJECTPROPERTY(id, 'IsUserTable') = 1)
DROP TABLE linkme_owner.EventLogExtraData
GO

CREATE TABLE linkme_owner.EventLogExtraData ( 
	dataKey varchar(80) NOT NULL,
	dataValue ntext NOT NULL,
	eventLogId bigint NOT NULL
)
GO

ALTER TABLE linkme_owner.EventLogExtraData ADD CONSTRAINT PK_EventLogExtraData
	PRIMARY KEY CLUSTERED (eventLogId, dataKey)
GO

ALTER TABLE linkme_owner.EventLogExtraData ADD CONSTRAINT FK_EventLogExtraData_event_log 
	FOREIGN KEY (eventLogId) REFERENCES linkme_owner.event_log (id)
GO
