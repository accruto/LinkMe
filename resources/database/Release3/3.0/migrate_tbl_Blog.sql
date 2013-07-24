EXEC sp_changeobjectowner 'linkme_owner.tbl_Blog', dbo
GO

-- Change UserId to UNIQUEIDENTIFIER and add a foreign key to Member.
-- Don't rename the column to minimise code changes required to blogs.

EXEC sp_rename 'dbo.tbl_Blog.UserId', '_UserId', 'COLUMN'

ALTER TABLE dbo.tbl_Blog
ADD UserId UNIQUEIDENTIFIER NULL

GO

UPDATE dbo.tbl_Blog
SET UserId = dbo.GuidFromString(_UserId)

GO

ALTER TABLE dbo.tbl_Blog
ALTER COLUMN UserId UNIQUEIDENTIFIER NOT NULL

ALTER TABLE dbo.tbl_Blog
DROP COLUMN _UserId

GO

ALTER TABLE dbo.tbl_Blog
ADD CONSTRAINT FK_tbl_Blog_Member
FOREIGN KEY (UserId) REFERENCES dbo.Member ([id])

GO
