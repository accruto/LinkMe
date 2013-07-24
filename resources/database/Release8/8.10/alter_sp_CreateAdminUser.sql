/****** Object:  StoredProcedure [dbo].[CreateAdminUser]    Script Date: 10/20/2008 16:01:16 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER PROCEDURE [dbo].[CreateAdminUser](@id UNIQUEIDENTIFIER, @loginId LoginId,
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

	INSERT INTO dbo.Contributor([id])
	VALUES (@id)

	IF (@@ERROR <> 0)
	BEGIN
		ROLLBACK TRANSACTION
		RETURN
	END

	COMMIT TRANSACTION
END