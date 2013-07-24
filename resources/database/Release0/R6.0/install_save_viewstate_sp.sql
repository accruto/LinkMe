
-- creating the store procedure
IF EXISTS (SELECT name 
	   FROM   sysobjects 
	   WHERE  name = N'saveLinkMeViewState' 
	   AND 	  type = 'P')
DROP PROCEDURE  linkme_owner.saveLinkMeViewState
GO

CREATE PROCEDURE linkme_owner.saveLinkMeViewState 
	@viewStateKey VARCHAR(100),
	@pageState	NTEXT,
	@updatedDate DATETIME
AS
BEGIN	

	DECLARE @VSKEYCOUNT SMALLINT
	SET @VSKEYCOUNT = 0

	SELECT @VSKEYCOUNT = count( viewStateKey ) 
	FROM linkme_owner.linkme_viewstate (NOLOCK)
	WHERE viewStateKey = @viewStateKey
	
	IF (@VSKEYCOUNT > 0)
	BEGIN
		
		UPDATE linkme_owner.linkme_viewstate WITH (ROWLOCK) SET
			PageState  = @pageState,
			UpdatedDate = @updatedDate
		WHERE
			ViewStateKey = @viewStateKey
	END
	ELSE
	BEGIN
	
		INSERT INTO linkme_owner.linkme_viewstate WITH (ROWLOCK)
		(
			ViewStateKey,
			PageState,
			UpdatedDate
		)
		VALUES
		(
			@viewStateKey,
			@pageState,
			@updatedDate
		)
	END
END

GO
