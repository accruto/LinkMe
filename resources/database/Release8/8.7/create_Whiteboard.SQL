IF EXISTS (SELECT * FROM dbo.SYSOBJECTS WHERE id = object_id('[Whiteboard]') AND  OBJECTPROPERTY(id, 'IsUserTable') = 1)
DROP TABLE [Whiteboard]
GO

CREATE TABLE [Whiteboard] ( 
	[id] uniqueidentifier NOT NULL
)
GO

ALTER TABLE [Whiteboard] ADD CONSTRAINT [PK_Whiteboard] 
	PRIMARY KEY CLUSTERED ([id])
GO


