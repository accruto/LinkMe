DECLARE Admins CURSOR FOR 
SELECT * FROM Administrator;

OPEN Admins

DECLARE @adminId UNIQUEIDENTIFIER

FETCH NEXT FROM Admins INTO @adminId

WHILE (@@FETCH_STATUS <> -1)
BEGIN
	IF(NOT EXISTS(SELECT id FROM [dbo].Contributor WHERE id = @adminId))   
		INSERT INTO [dbo].Contributor (id) VALUES (@adminId)
	FETCH NEXT FROM Admins INTO @adminId
END

CLOSE Admins
DEALLOCATE Admins