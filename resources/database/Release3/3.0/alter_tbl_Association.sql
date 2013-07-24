EXEC sp_changeobjectowner 'linkme_owner.tbl_Association', dbo
GO

-- Change all the columns from bigint to int

ALTER TABLE dbo.tbl_Association
DROP CONSTRAINT PK_tbl_Association

ALTER TABLE dbo.tbl_Association
DROP CONSTRAINT IX_tbl_Association

GO

ALTER TABLE dbo.tbl_Association
ALTER COLUMN [Id] INT NOT NULL

ALTER TABLE dbo.tbl_Association
ALTER COLUMN InstanceId INT NOT NULL

ALTER TABLE dbo.tbl_Association
ALTER COLUMN TagId INT NOT NULL

GO

ALTER TABLE dbo.tbl_Association
ADD CONSTRAINT PK_tbl_Association
PRIMARY KEY CLUSTERED ([Id])

ALTER TABLE dbo.tbl_Association
ADD CONSTRAINT IX_tbl_Association
UNIQUE NONCLUSTERED (InstanceId, TagId, Class)

GO

-- Add a foreign key to tbl_Tag, while we're at it, deleting all the orphaned associations.

DELETE dbo.tbl_Association
FROM dbo.tbl_Association a
WHERE NOT EXISTS
(
	SELECT *
	FROM dbo.tbl_Tag t
	WHERE t.[Id] = a.TagId
)

ALTER TABLE dbo.tbl_Association
ADD CONSTRAINT FK_tbl_Association_tbl_Tag
FOREIGN KEY (TagId) REFERENCES dbo.tbl_Tag([Id])

GO
