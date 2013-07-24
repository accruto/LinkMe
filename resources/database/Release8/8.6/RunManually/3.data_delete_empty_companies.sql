DELETE Company
FROM Company c
WHERE NOT EXISTS
(
	SELECT *
	FROM Employer e
	WHERE e.companyId = c.[id]
)
