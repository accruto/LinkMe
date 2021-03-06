IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[GetUserActivity]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[GetUserActivity]
GO

CREATE PROCEDURE [dbo].[GetUserActivity]
(
	@loginId NVARCHAR(320)
)
AS
BEGIN

	SET NOCOUNT ON

	DECLARE @userId UNIQUEIDENTIFIER
	SELECT @userId = id FROM dbo.RegisteredUser WHERE loginId = @loginId

	DECLARE @activity TABLE
	(
		time DATETIME,
		userId UNIQUEIDENTIFIER,
		action NVARCHAR(100),
		actorId UNIQUEIDENTIFIER
	)

	-- Activation

	INSERT
		@activity (time, userId, action, actorId)
	SELECT
		time, userId, 'Activated', activatedById
	FROM
		dbo.UserActivation
	WHERE
		userId = @userId

	-- Deactivation

	INSERT
		@activity (time, userId, action, actorId)
	SELECT
		time, userId, 'Deactivated', deactivatedById
	FROM
		dbo.UserDeactivation
	WHERE
		userId = @userId

	-- UserDisablement

	INSERT
		@activity (time, userId, action, actorId)
	SELECT
		time, userId, 'Disabled', disabledById
	FROM
		dbo.UserDisablement
	WHERE
		userId = @userId

	-- UserEnablement

	INSERT
		@activity (time, userId, action, actorId)
	SELECT
		time, userId, 'Enabled', enabledById
	FROM
		dbo.UserEnablement
	WHERE
		userId = @userId

	-- UserLogin

	INSERT
		@activity (time, userId, action, actorId)
	SELECT
		time, userId, 'Login', userId
	FROM
		dbo.UserLogin
	WHERE
		userId = @userId

	SELECT
		a.time AS Time,
		u.firstName + ' ' + u.lastName AS [User],
		a.action AS Action,
		au.firstName + ' ' + au.lastName AS Actor
	FROM
		@activity AS a
	INNER JOIN
		dbo.RegisteredUser AS u ON u.id = a.userId
	INNER JOIN
		dbo.RegisteredUser AS au ON au.id = a.actorId
	ORDER BY
		time DESC

END