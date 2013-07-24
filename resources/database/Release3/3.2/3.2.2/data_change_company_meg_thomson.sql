-- Change Meg Thomson account to company TAD
UPDATE
	Employer
SET
	companyId = 

	(SELECT
		id
	FROM
		Company
	WHERE
		name = 'TAD')

FROM
	Employer AS emp
INNER JOIN
	RegisteredUser AS ru
ON	ru.id = emp.id
WHERE
	ru.loginId = 'Meg Thomson'