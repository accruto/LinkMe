if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[GetJobAdLastClosedTime]') and xtype in (N'FN', N'IF', N'TF'))
DROP FUNCTION [dbo].[GetJobAdLastClosedTime]
GO

CREATE FUNCTION dbo.GetJobAdLastClosedTime (@jobAdId UNIQUEIDENTIFIER)
RETURNS DATETIME
AS
BEGIN

	DECLARE @time DATETIME

	SELECT
		@time = MAX(s.time)
	FROM
		dbo.JobAdStatus AS s
	WHERE
		s.jobAdId = @jobAdId
		AND s.previousStatus <> 3
		AND s.newStatus = 3

	RETURN @time
END
GO

