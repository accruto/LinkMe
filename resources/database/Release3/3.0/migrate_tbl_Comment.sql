EXEC sp_changeobjectowner 'linkme_owner.tbl_Comment', dbo
GO

-- Try to fix a major WTF here: the UserId column may contain EITHER a user ID (GUID) as a string
-- OR the name entered by an anonymous comment poster. The Anonymous column specifies which of these
-- it is. Split it into two columns: AnonymousName for an anonymous user and UserId (GUID) for a
-- registered user. The Anonymous column is then no longer needed.

EXEC sp_rename 'dbo.tbl_Comment.UserId', '_UserId', 'COLUMN'

ALTER TABLE dbo.tbl_Comment
ADD UserId UNIQUEIDENTIFIER NULL

ALTER TABLE dbo.tbl_Comment
ADD AnonymousName NVARCHAR(100) NULL

GO

UPDATE dbo.tbl_Comment
SET UserId = dbo.GuidFromString(_UserId)
WHERE Anonymous = 0

UPDATE dbo.tbl_Comment
SET AnonymousName = _UserId
WHERE Anonymous = 1

GO

ALTER TABLE [dbo].[tbl_Comment] DROP CONSTRAINT [DF_tbl_Comment_Annoymose]

ALTER TABLE dbo.tbl_Comment
DROP COLUMN _UserId

ALTER TABLE dbo.tbl_Comment
DROP COLUMN Anonymous

GO

ALTER TABLE dbo.tbl_Comment
ADD CONSTRAINT FK_tbl_Comment_RegisteredUser
FOREIGN KEY (UserId) REFERENCES dbo.RegisteredUser ([id])

-- For good measure add a foreign key to the Post ("Parent").

ALTER TABLE dbo.tbl_Comment
ADD CONSTRAINT FK_tbl_Comment_tbl_Post
FOREIGN KEY (Parent) REFERENCES dbo.tbl_Post ([id])

GO
