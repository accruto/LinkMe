-- Reset all non-yearly rates

UPDATE
	dbo.Candidate
SET
	desiredSalaryLower = NULL,
	desiredSalaryUpper = NULL,
	desiredSalaryRateType = 0
WHERE
	desiredSalaryRateType IN (2, 3, 4, 5, 6)

-- If the rate is None then nullify the bounds.

UPDATE
	dbo.Candidate
SET
	desiredSalaryLower = NULL,
	desiredSalaryUpper = NULL
WHERE
	desiredSalaryRateType = 0

-- If the bounds are both 0 then set to None.

UPDATE
	dbo.Candidate
SET
	desiredSalaryLower = NULL,
	desiredSalaryUpper = NULL,
	desiredSalaryRateType = 0
WHERE
	desiredSalaryLower = 0
	AND desiredSalaryUpper = 0

-- Turn low salaries into hourly.

UPDATE
	dbo.Candidate
SET
	desiredSalaryLower = CAST(desiredSalaryLower / 5 AS INT) * 5,
	desiredSalaryUpper = CAST(desiredSalaryUpper / 5 AS INT) * 5,
	desiredSalaryRateType = 5
WHERE
	desiredSalaryLower < 1000
	AND desiredSalaryUpper < 1000

UPDATE
	dbo.Candidate
SET
	desiredSalaryLower = 125,
	desiredSalaryUpper = NULL
WHERE
	desiredSalaryRateType = 5
	AND desiredSalaryLower > 125

-- Large salary range.

UPDATE
	dbo.Candidate
SET
	desiredSalaryLower = NULL,
	desiredSalaryUpper = NULL,
	desiredSalaryRateType = 0
WHERE
	desiredSalaryUpper - desiredSalaryLower > 100000

-- Large bounds.

UPDATE
	dbo.Candidate
SET
	desiredSalaryLower = 250000,
	desiredSalaryUpper = NULL,
	desiredSalaryRateType = 1
WHERE
	desiredSalaryUpper > 250000
	AND desiredSalaryLower > 250000

