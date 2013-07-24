-------------------------------------------------------------------------------
-- Drop

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[FiStoreMessage]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [dbo].[FiStoreMessage]
GO

-------------------------------------------------------------------------------
-- Create

CREATE TABLE dbo.FiStoreMessage
(
	id INT IDENTITY (1, 1) NOT NULL,
	[time] BIGINT NOT NULL,
	sequence INT NOT NULL,
	sourceId INT NOT NULL,
	[event] VARCHAR(256) NOT NULL,
	[type] VARCHAR(256) NOT NULL,
	method VARCHAR(256) NOT NULL,
	[message] NTEXT NOT NULL,
	exception NTEXT NULL,
	details NTEXT NULL,
)
GO

ALTER TABLE dbo.FiStoreMessage WITH NOCHECK
ADD CONSTRAINT PK_FiStoreMessage PRIMARY KEY NONCLUSTERED (id)
GO

ALTER TABLE dbo.FiStoreMessage WITH NOCHECK
ADD CONSTRAINT FK_FiStoreMessage_FiStoreSource FOREIGN KEY (sourceId)
REFERENCES dbo.FiStoreSource (id)
GO

CREATE CLUSTERED INDEX IX_FiStoreMessage_Order
ON dbo.FiStoreMessage ([time], sequence)
GO

CREATE INDEX IX_FiStoreMessage_SourceId
ON dbo.FiStoreMessage (sourceId)
GO

CREATE INDEX IX_FiStoreMessage_Event
ON dbo.FiStoreMessage ([event])
GO
