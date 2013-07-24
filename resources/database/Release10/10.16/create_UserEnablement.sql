IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'dbo.UserEnablement') AND type in (N'U'))
DROP TABLE dbo.UserEnablement
GO

CREATE TABLE dbo.UserEnablement
(
	id UNIQUEIDENTIFIER NOT NULL,
	userId UNIQUEIDENTIFIER NOT NULL,
	time DATETIME NOT NULL,
	enabledById UNIQUEIDENTIFIER NOT NULL
)

ALTER TABLE dbo.UserEnablement
ADD CONSTRAINT PK_UserEnablement PRIMARY KEY NONCLUSTERED
(
	id
)

CREATE CLUSTERED INDEX [IX_UserEnablement_userId] ON [dbo].[UserEnablement]
(
	userId,
	time
)
GO

INSERT
	dbo.UserEnablement (id, userId, time, enabledById)
SELECT
	e.id, u.userId, e.time, e.actorId
FROM
	dbo.UserEvent AS e
LEFT OUTER JOIN
	dbo.UserEventActionedUser AS u ON u.eventid = e.id
WHERE
	e.type = 7