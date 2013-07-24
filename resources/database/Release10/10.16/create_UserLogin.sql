IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'dbo.UserLogin') AND type in (N'U'))
DROP TABLE dbo.UserLogin
GO

CREATE TABLE dbo.UserLogin
(
	id UNIQUEIDENTIFIER NOT NULL,
	userId UNIQUEIDENTIFIER NOT NULL,
	time DATETIME NOT NULL,
	admin BIT NOT NULL,
	ipAddress NVARCHAR(100) NULL
)

ALTER TABLE dbo.UserLogin
ADD CONSTRAINT PK_UserLogin PRIMARY KEY NONCLUSTERED
(
	id
)

CREATE CLUSTERED INDEX [IX_UserLogin_userId] ON [dbo].[UserLogin]
(
	userId,
	time
)
GO

INSERT
	dbo.UserLogin (id, userId, time, admin, ipAddress)
SELECT
	e.id,
	e.actorId,
	e.time,
	0,
	(
		SELECT
			value
		FROM
			dbo.UserEventExtraData
		WHERE
			eventId = e.id
			AND [key] = 'IP'
	)
FROM
	dbo.UserEvent AS e
LEFT OUTER JOIN
	dbo.UserEventActionedUser AS u ON u.eventid = e.id
WHERE
	e.type = 1

INSERT
	dbo.UserLogin (id, userId, time, admin, ipAddress)
SELECT
	e.id,
	e.actorId,
	e.time,
	1,
	(
		SELECT
			value
		FROM
			dbo.UserEventExtraData
		WHERE
			eventId = e.id
			AND [key] = 'IP'
	)
FROM
	dbo.UserEvent AS e
LEFT OUTER JOIN
	dbo.UserEventActionedUser AS u ON u.eventid = e.id
WHERE
	e.type = 14