IF EXISTS (SELECT name 
	   FROM   sysobjects 
	   WHERE  name = N'clearExpiredViewStates' 
	   AND 	  type = 'P')
DROP PROCEDURE  linkme_owner.clearExpiredViewStates
GO
SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS OFF 
GO

CREATE  PROCEDURE linkme_owner.clearExpiredViewStates 
	@timeToExpireDays INT
AS
BEGIN	
	DELETE FROM linkme_owner.linkme_viewstate WITH (ROWLOCK)
	WHERE DATEADD(dd, @timeToExpireDays, UpdatedDate) <= GETDATE()
END
GO
SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO

