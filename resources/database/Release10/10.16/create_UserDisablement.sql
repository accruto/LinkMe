IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'dbo.UserDisablement') AND type in (N'U'))
DROP TABLE dbo.UserDisablement
GO

CREATE TABLE dbo.UserDisablement
(
	id UNIQUEIDENTIFIER NOT NULL,
	userId UNIQUEIDENTIFIER NOT NULL,
	time DATETIME NOT NULL,
	disabledById UNIQUEIDENTIFIER NOT NULL
)

ALTER TABLE dbo.UserDisablement
ADD CONSTRAINT PK_UserDisablement PRIMARY KEY NONCLUSTERED
(
	id
)

CREATE CLUSTERED INDEX [IX_UserDisablement_userId] ON [dbo].[UserDisablement]
(
	userId,
	time
)
GO

INSERT
	dbo.UserDisablement (id, userId, time, disabledById)
SELECT
	e.id, u.userId, e.time, e.actorId
FROM
	dbo.UserEvent AS e
LEFT OUTER JOIN
	dbo.UserEventActionedUser AS u ON u.eventid = e.id
WHERE
	e.type = 6