IF EXISTS (SELECT name 
	   FROM   sysobjects 
	   WHERE  name = 'getAlertsCount' 
	   AND 	  type = 'P')
DROP PROCEDURE  linkme_owner.getAlertsCount
GO
CREATE PROCEDURE linkme_owner.getAlertsCount
	@userProfileId VARCHAR(50)
AS
BEGIN	
	SELECT Count(id)  as AlertsCount
	FROM linkme_owner.user_alert_data (NOLOCK)
	WHERE userProfileId = @userProfileId
END
GO



