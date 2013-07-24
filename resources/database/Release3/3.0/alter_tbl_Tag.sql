EXEC sp_changeobjectowner 'linkme_owner.tbl_Tag', dbo
GO

-- Change id column from bigint to int

ALTER TABLE dbo.tbl_Tag
DROP CONSTRAINT PK_tbl_Tag
GO

ALTER TABLE dbo.tbl_Tag
ALTER COLUMN [Id] INT NOT NULL
GO

ALTER TABLE dbo.tbl_Tag
ADD CONSTRAINT PK_tbl_Tag
PRIMARY KEY CLUSTERED ([Id])
GO
