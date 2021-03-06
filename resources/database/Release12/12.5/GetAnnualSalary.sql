IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[GetAnnualSalary]') AND type in (N'FN', N'IF', N'TF', N'FS', N'FT'))
DROP FUNCTION [dbo].[GetAnnualSalary]
GO

Create FUNCTION [dbo].[GetAnnualSalary] (@salary DECIMAL(18, 8), @salaryRate TINYINT)
RETURNS DECIMAL(18, 8)
AS
BEGIN

	-- Mirrors Salary.ToRate method.

	IF (@salaryRate = 0 OR @salaryRate = 1)
		RETURN @salary

	DECLARE @annualSalary DECIMAL(18, 8)
	DECLARE @monthToYear DECIMAL(18, 8)
	DECLARE @weekToYear DECIMAL(18, 8)
	DECLARE @dayToYear DECIMAL(18, 8)
	DECLARE @hourToYear DECIMAL(18, 8)

	SET @monthToYear = 12
	SET @weekToYear = 52
	SET @dayToYear = 250
	SET @hourToYear = 2000

	SELECT @annualSalary = 
		@salary *
		CASE 
		WHEN @salaryRate = 2 THEN @monthToYear
		WHEN @salaryRate = 3 THEN @weekToYear
		WHEN @salaryRate = 4 THEN @dayToYear
		WHEN @salaryRate = 5 THEN @hourToYear
		ELSE 1
		END

	RETURN @annualSalary
END