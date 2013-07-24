if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[GetJobAdLastClosedTime]') and xtype in (N'FN', N'IF', N'TF'))
DROP FUNCTION [dbo].[GetJobAdLastClosedTime]
GO

CREATE FUNCTION dbo.GetJobAdLastClosedTime (@jobAdId UNIQUEIDENTIFIER)
RETURNS DATETIME
AS
BEGIN

	DECLARE @time DATETIME

	-- The job ad must still be closed.

	SELECT
		@time = MAX(s.time)
	FROM
		dbo.JobAdStatus AS s
	INNER JOIN
		dbo.JobAd AS a ON a.id = s.jobAdId
	WHERE
		a.id = @jobAdId
		AND a.status = 3
		AND s.previousStatus <> 3
		AND s.newStatus = 3

	RETURN @time
END
GO

