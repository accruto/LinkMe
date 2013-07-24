if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[SaveLinkMeViewState]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[SaveLinkMeViewState]
GO

CREATE PROCEDURE dbo.SaveLinkMeViewState(
	@key UNIQUEIDENTIFIER,
	@pageState TEXT,
	@updatedDate DATETIME
)
AS
BEGIN	
	INSERT INTO dbo.linkme_viewstate
	WITH (ROWLOCK)
	([key], pageState, updatedDate)
	VALUES (@key, @pageState, @updatedDate)
END
GO
