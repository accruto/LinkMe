IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[GetCandidateStatusDisplayText]') AND type in (N'FN', N'IF', N'TF', N'FS', N'FT'))
DROP FUNCTION [dbo].[GetCandidateStatusDisplayText]
GO

CREATE FUNCTION [dbo].[GetCandidateStatusDisplayText](@status AS TINYINT)
RETURNS NVARCHAR(100)
AS
BEGIN

	-- Converts the CandidateStatus enum into a string using the same logic as the CandidateStatusDisplay class.

	DECLARE @displayText NVARCHAR(100)

	SELECT
		@displayText = CASE @status
		WHEN 1 THEN 'Not looking'
		WHEN 2 THEN 'Happy to talk'
		WHEN 3 THEN 'Actively looking'
		WHEN 4 THEN 'Immediately available'
		ELSE 'Unspecified'
		END

	RETURN @displayText
END
