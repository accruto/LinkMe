IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'dbo.UserDeactivation') AND type in (N'U'))
DROP TABLE dbo.UserDeactivation
GO

CREATE TABLE dbo.UserDeactivation
(
	id UNIQUEIDENTIFIER NOT NULL,
	userId UNIQUEIDENTIFIER NOT NULL,
	time DATETIME NOT NULL,
	deactivatedById UNIQUEIDENTIFIER NOT NULL,
	reason NVARCHAR(100) NULL,
	comments NVARCHAR(1000) NULL
)

ALTER TABLE dbo.UserDeactivation
ADD CONSTRAINT PK_UserDeactivation PRIMARY KEY NONCLUSTERED
(
	id
)

CREATE CLUSTERED INDEX [IX_UserDeactivation_userId] ON [dbo].[UserDeactivation]
(
	userId,
	time
)
GO

INSERT
	dbo.UserDeactivation (id, userId, time, deactivatedById, reason, comments)
SELECT
	e.id,
	u.userId,
	e.time,
	e.actorId,
	(
		SELECT
			value
		FROM
			dbo.UserEventExtraData
		WHERE
			eventid = e.id
			AND [key] = 'reason'
	),
	(
		SELECT
			value
		FROM
			dbo.UserEventExtraData
		WHERE
			eventid = e.id
			AND [key] = 'comments'
	)
FROM
	dbo.UserEvent AS e
LEFT OUTER JOIN
	dbo.UserEventActionedUser AS u ON u.eventid = e.id
WHERE
	e.type = 15