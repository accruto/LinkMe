CREATE PROCEDURE dbo.SaveLinkMeViewState 
	@viewStateKey VARCHAR(100),
	@pageState NTEXT,
	@updatedDate DATETIME
AS
BEGIN	

	DECLARE @VSKEYCOUNT SMALLINT
	SET @VSKEYCOUNT = 0

	SELECT @VSKEYCOUNT = count( viewStateKey ) 
	FROM dbo.linkme_viewstate (NOLOCK)
	WHERE viewStateKey = @viewStateKey
	
	IF (@VSKEYCOUNT > 0)
	BEGIN
		
		UPDATE dbo.linkme_viewstate WITH (ROWLOCK) SET
			PageState  = @pageState,
			UpdatedDate = @updatedDate
		WHERE
			ViewStateKey = @viewStateKey
	END
	ELSE
	BEGIN
	
		INSERT INTO dbo.linkme_viewstate
		WITH (ROWLOCK)
		(ViewStateKey, PageState, UpdatedDate)
		VALUES (@viewStateKey, @pageState, @updatedDate)
	END
END

GO

if exists (select * from dbo.sysobjects where id = object_id(N'[linkme_owner].[saveLinkMeViewState]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [linkme_owner].[saveLinkMeViewState]
GO

