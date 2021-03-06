IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[IsResumeEmpty]') AND type in (N'FN', N'IF', N'TF', N'FS', N'FT'))
DROP FUNCTION [dbo].[IsResumeEmpty]
GO

CREATE FUNCTION [dbo].[IsResumeEmpty](@id AS UNIQUEIDENTIFIER)
RETURNS INT
AS
BEGIN

	IF EXISTS
	(
		SELECT
			id
		FROM
			dbo.Resume WITH (NOLOCK)
		WHERE
			id = @id
			AND
			(
				isEmpty = 0
				OR EXISTS (SELECT resumeId FROM dbo.ResumeJob WITH (NOLOCK) WHERE resumeId = id AND isEmpty = 0)
				OR EXISTS (SELECT resumeId FROM dbo.ResumeSchool WITH (NOLOCK) WHERE resumeId = id AND isEmpty = 0)
			)
	)
		RETURN 0

	RETURN 1
END
