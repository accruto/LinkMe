if exists (select * from dbo.sysobjects where id = object_id(N'[linkme_owner].[CreateAdminUser]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [linkme_owner].[CreateAdminUser]
GO

CREATE PROCEDURE linkme_owner.CreateAdminUser(@id UNIQUEIDENTIFIER, @userId VARCHAR(50),
	@passwordHash VARCHAR(50), @firstName VARCHAR(50), @lastName VARCHAR(50),
	@emailAddress VARCHAR(255))
AS
BEGIN
	BEGIN TRANSACTION

	INSERT INTO linkme_owner.user_profile (id, userId, password, firstName, lastName, active, joinDate,
		preferredHtmlEmailFormat, OptOutOfNewsletters, newUserMustChangePassword, forcePasswordChange,
		subRole, postcode, country, emailVerified, optIntoNewslettersDate)
	VALUES (@id, @userId, @passwordHash, @firstName, @lastName, 1, GETDATE(), 1, 1, 1, 1, NULL, NULL,
		NULL, 1, '1753-01-01')

	IF (@@ERROR <> 0)
	BEGIN
		ROLLBACK TRANSACTION
		RETURN
	END

	INSERT INTO linkme_owner.administrator_profile (id, emailAddress)
	VALUES (@id, @emailAddress)

	IF (@@ERROR <> 0)
	BEGIN
		ROLLBACK TRANSACTION
		RETURN
	END

	COMMIT TRANSACTION
END
GO
