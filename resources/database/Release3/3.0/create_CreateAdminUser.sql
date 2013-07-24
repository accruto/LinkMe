if exists (select * from dbo.sysobjects where id = object_id(N'[linkme_owner].[CreateAdminUser]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [linkme_owner].[CreateAdminUser]
GO
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[CreateAdminUser]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[CreateAdminUser]
GO

CREATE PROCEDURE dbo.CreateAdminUser(@id UNIQUEIDENTIFIER, @loginId LoginId,
	@passwordHash PasswordHash, @firstName PersonName, @lastName PersonName,
	@emailAddress EmailAddress)
AS
BEGIN
	BEGIN TRANSACTION

	INSERT INTO dbo.RegisteredUser([id], loginId, createdTime, emailAddress, firstName, lastName,
		flags, passwordHash)
	VALUES (@id, @loginId, GETDATE(), @emailAddress, @firstName, @lastName, 35, @passwordHash)

	IF (@@ERROR <> 0)
	BEGIN
		ROLLBACK TRANSACTION
		RETURN
	END

	INSERT INTO dbo.Administrator([id])
	VALUES (@id)

	IF (@@ERROR <> 0)
	BEGIN
		ROLLBACK TRANSACTION
		RETURN
	END

	COMMIT TRANSACTION
END

GO
