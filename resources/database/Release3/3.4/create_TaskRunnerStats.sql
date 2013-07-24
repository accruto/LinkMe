IF EXISTS (SELECT * FROM dbo.SYSOBJECTS WHERE id = object_id('[TaskRunnerStats]') AND  OBJECTPROPERTY(id, 'IsUserTable') = 1)
DROP TABLE [TaskRunnerStats]
GO

CREATE TABLE [TaskRunnerStats] ( 
	[id] uniqueidentifier NOT NULL,
	[task] varchar(50) NOT NULL,
	[date] datetime NOT NULL,
	[count] int NOT NULL,
	[counterType] varchar(50) NOT NULL
)
GO

ALTER TABLE [TaskRunnerStats] ADD CONSTRAINT [PK_TaskRunnerStats] 
	PRIMARY KEY CLUSTERED ([id])
GO
