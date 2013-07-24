-- creating the store procedure
IF EXISTS (SELECT name 
	   FROM   sysobjects 
	   WHERE  name = N'getLinkMeViewState' 
	   AND 	  type = 'P')
DROP PROCEDURE  linkme_owner.getLinkMeViewState
GO

CREATE PROCEDURE linkme_owner.getLinkMeViewState 
	@viewStateKey VARCHAR(100)
AS
BEGIN	
	SELECT PageState 
	FROM linkme_owner.linkme_viewstate (NOLOCK)
	WHERE viewStateKey = @viewStateKey
END

GO
