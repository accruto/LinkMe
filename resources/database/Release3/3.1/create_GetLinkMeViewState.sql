if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[GetLinkMeViewState]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[GetLinkMeViewState]
GO

CREATE PROCEDURE dbo.GetLinkMeViewState(
	@key UNIQUEIDENTIFIER
)
AS
BEGIN	
	SELECT pageState
	FROM dbo.linkme_viewstate (NOLOCK)
	WHERE [key] = @key
END
GO
