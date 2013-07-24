-------------------------------------------------------------------------------
-- Drop

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[FiStoreParameter]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [dbo].[FiStoreParameter]
GO

-------------------------------------------------------------------------------
-- Create

CREATE TABLE dbo.FiStoreParameter
(
	messageId INT NOT NULL,
	sequence INT NOT NULL,
	name VARCHAR(128) NOT NULL,
	format INT NOT NULL,
	typeId INT NULL,
	string NTEXT NULL,
	binary IMAGE NULL
)
GO

ALTER TABLE dbo.FiStoreParameter WITH NOCHECK
ADD CONSTRAINT PK_FiStoreParameter PRIMARY KEY CLUSTERED (messageId, sequence)
GO

ALTER TABLE dbo.FiStoreParameter WITH NOCHECK
ADD CONSTRAINT FK_FiStoreParameter_FiStoreMessage FOREIGN KEY (messageId)
REFERENCES dbo.FiStoreMessage (id)
ON DELETE CASCADE
GO

ALTER TABLE dbo.FiStoreParameter WITH NOCHECK
ADD CONSTRAINT FK_FiStoreParameter_FiStoreType FOREIGN KEY (typeId)
REFERENCES dbo.FiStoreType (id)
GO
