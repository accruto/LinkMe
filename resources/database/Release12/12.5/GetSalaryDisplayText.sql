IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[GetSalaryDisplayText]') AND type in (N'FN', N'IF', N'TF', N'FS', N'FT'))
DROP FUNCTION [dbo].[GetSalaryDisplayText]
GO

CREATE FUNCTION [dbo].[GetSalaryDisplayText](@lowerBound AS DECIMAL(18, 8), @upperBound AS DECIMAL(18, 8), @rate AS TINYINT)
RETURNS NVARCHAR(100)
AS
BEGIN

	-- Converts the Salary into a string using the same logic as the SalaryExtensions.GetDisplayText method.
	-- Note that this function does convert to an annual salary first.

	DECLARE @displayText NVARCHAR(100)

	IF (@rate = 0)
		RETURN @displayText

	SET @lowerBound = dbo.GetAnnualSalary(@lowerBound, @rate)
	SET @upperBound = dbo.GetAnnualSalary(@upperBound, @rate)

	IF (NOT @lowerBound IS NULL)
	BEGIN
		-- Lower bound

		IF (NOT @upperBound IS NULL)
		BEGIN
			-- Upper bound

			IF (@lowerBound = @upperBound)
				SET @displayText = '$' + CAST(CAST(@lowerBound AS INT) AS NVARCHAR(100))
			ELSE
				SET @displayText = '$' + CAST(CAST(@lowerBound AS INT) AS NVARCHAR(100)) + ' to $' + CAST(CAST(@upperBound AS INT) AS NVARCHAR(100))
		END
		ELSE
		BEGIN
			-- No upper bound

			SET @displayText = '$' + CAST(CAST(@lowerBound AS INT) AS NVARCHAR(100)) + '+'
		END
	END
	ELSE
	BEGIN
		-- No lower bound.

		IF (NOT @upperBound IS NULL)
			SET @displayText = '$' + CAST(CAST(@upperBound AS INT) AS NVARCHAR(100))
	
	END


	RETURN @displayText
END
