-- First remove salaries that are too large to be converted - they're completely meaningless anyway.

DECLARE @maxSalary DECIMAL(18,8)
SET @maxSalary = 1000000

UPDATE linkme_owner.user_profile_ideal_job
SET currentSalaryMinRate = 0, currentSalaryMaxRate = -1
WHERE currentSalaryMinRate > @maxSalary OR currentSalaryMaxRate > @maxSalary

UPDATE linkme_owner.user_profile_ideal_job
SET idealSalaryMinRate = 0, idealSalaryMaxRate = -1
WHERE idealSalaryMinRate > @maxSalary OR idealSalaryMaxRate > @maxSalary

-- Convert current and ideal salaries from hourly to yearly. Note that currentSalaryRateType and
-- idealSalaryRateType do not need to change as they only specify how the user wishes to PRESENT their
-- salary - it's always stored as yearly.

UPDATE linkme_owner.user_profile_ideal_job
SET currentSalaryMinRate = CASE currentSalaryMinRate WHEN -1 THEN -1 ELSE ROUND(currentSalaryMinRate * 2080, 0) END,
	currentSalaryMaxRate = CASE currentSalaryMaxRate WHEN -1 THEN -1 ELSE ROUND(currentSalaryMaxRate * 2080, 0) END,
	idealSalaryMinRate = CASE idealSalaryMinRate WHEN -1 THEN -1 ELSE ROUND(idealSalaryMinRate * 2080, 0) END,
	idealSalaryMaxRate = CASE idealSalaryMaxRate WHEN -1 THEN -1 ELSE ROUND(idealSalaryMaxRate * 2080, 0) END

GO