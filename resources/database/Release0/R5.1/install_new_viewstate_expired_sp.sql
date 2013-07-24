
-- creating the store procedure
IF EXISTS (SELECT name 
	   FROM   sysobjects 
	   WHERE  name = N'clearExpiredViewStates' 
	   AND 	  type = 'P')
DROP PROCEDURE  linkme_owner.clearExpiredViewStates
GO

CREATE PROCEDURE linkme_owner.clearExpiredViewStates 
	@timeToExpireMins INT
AS
BEGIN	
	DELETE FROM linkme_owner.linkme_viewstate WITH (ROWLOCK)
	WHERE DATEADD(mi, @timeToExpireMins, UpdatedDate) <= GETDATE()
END
GO