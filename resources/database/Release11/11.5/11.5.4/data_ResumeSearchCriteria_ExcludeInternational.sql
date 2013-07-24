UPDATE
	ResumeSearchCriteria
SET
	name = 'IncludeInternational',
	value = 'False'
WHERE
	name = 'ExcludeInternational'
	AND value = 'True'

UPDATE
	ResumeSearchCriteria
SET
	name = 'IncludeInternational',
	value = 'True'
WHERE
	name = 'ExcludeInternational'
	AND value = 'False'