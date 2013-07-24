CREATE PROCEDURE dbo.GetLinkMeViewState
	@viewStateKey VARCHAR(100)
AS
BEGIN	
	SELECT PageState 
	FROM dbo.linkme_viewstate (NOLOCK)
	WHERE viewStateKey = @viewStateKey
END

GO

if exists (select * from dbo.sysobjects where id = object_id(N'[linkme_owner].[getLinkMeViewState]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [linkme_owner].[getLinkMeViewState]
GO
