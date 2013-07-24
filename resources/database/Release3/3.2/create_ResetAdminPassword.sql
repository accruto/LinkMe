if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[ResetAdminPassword]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[ResetAdminPassword]
GO

CREATE PROCEDURE dbo.ResetAdminPassword(@loginId LoginId, @passwordHash PasswordHash = '4C+YKJIYzi7OVxqXoB3TfA==')
AS
BEGIN
	UPDATE dbo.RegisteredUser
	SET passwordHash = @passwordHash, flags = flags | 2
	FROM dbo.RegisteredUser ru
	INNER JOIN dbo.Administrator a
	ON ru.[id] = a.[id]
	WHERE ru.loginId = @loginId
END

GO
