-- Both are NOT NULL.

UPDATE
	dbo.SavedResumeSearch
SET
	name = LEFT(k.value + ', ' + l.value, 100)
FROM
	dbo.SavedResumeSearch AS s
	INNER JOIN dbo.ResumeSearchCriteriaSet AS cs ON cs.id = s.criteriaSetId
	LEFT OUTER JOIN dbo.ResumeSearchCriteria AS k ON k.setId = cs.id
	LEFT OUTER JOIN dbo.ResumeSearchCriteria AS l ON l.setId = cs.id
WHERE
	s.name IS NULL
	AND k.name = 'Keywords'
	AND l.name = 'Location'

UPDATE
	dbo.SavedResumeSearch
SET
	name = LEFT(k.value + ', ' + l.value, 100)
FROM
	dbo.SavedResumeSearch AS s
	INNER JOIN dbo.ResumeSearchCriteriaSet AS cs ON cs.id = s.criteriaSetId
	LEFT OUTER JOIN dbo.ResumeSearchCriteria AS k ON k.setId = cs.id
	LEFT OUTER JOIN dbo.ResumeSearchCriteria AS l ON l.setId = cs.id
WHERE
	s.name IS NULL
	AND k.name = 'JobTitle'
	AND l.name = 'Location'

UPDATE
	dbo.SavedResumeSearch
SET
	name = LEFT(k.value, 100)
FROM
	dbo.savedresumesearch AS s
	INNER JOIN dbo.ResumeSearchCriteriaSet AS cs ON cs.id = s.criteriaSetId
	INNER JOIN dbo.ResumeSearchCriteria AS k ON k.setId = cs.id
WHERE
	s.name IS NULL
	AND k.name = 'Keywords'

UPDATE
	dbo.SavedResumeSearch
SET
	name = LEFT(k.value, 100)
FROM
	dbo.savedresumesearch AS s
	INNER JOIN dbo.ResumeSearchCriteriaSet AS cs ON cs.id = s.criteriaSetId
	INNER JOIN dbo.ResumeSearchCriteria AS k ON k.setId = cs.id
WHERE
	s.name IS NULL
	AND k.name = 'JobTitle'

UPDATE
	dbo.SavedResumeSearch
SET
	name = LEFT(k.value, 100)
FROM
	dbo.savedresumesearch AS s
	INNER JOIN dbo.ResumeSearchCriteriaSet AS cs ON cs.id = s.criteriaSetId
	INNER JOIN dbo.ResumeSearchCriteria AS k ON k.setId = cs.id
WHERE
	s.name IS NULL
	AND k.name = 'CompanyKeywords'

UPDATE
	dbo.SavedResumeSearch
SET
	name = LEFT(k.value, 100)
FROM
	dbo.savedresumesearch AS s
	INNER JOIN dbo.ResumeSearchCriteriaSet AS cs ON cs.id = s.criteriaSetId
	INNER JOIN dbo.ResumeSearchCriteria AS k ON k.setId = cs.id
WHERE
	s.name IS NULL
	AND k.name = 'DesiredJobTitle'

UPDATE
	dbo.SavedResumeSearch
SET
	name = LEFT(k.value, 100)
FROM
	dbo.savedresumesearch AS s
	INNER JOIN dbo.ResumeSearchCriteriaSet AS cs ON cs.id = s.criteriaSetId
	INNER JOIN dbo.ResumeSearchCriteria AS k ON k.setId = cs.id
WHERE
	s.name IS NULL
	AND k.name = 'Location'

-- Catch all at the end.

UPDATE
	dbo.SavedResumeSearch
SET
	name = 'Saved search'
FROM
	dbo.savedresumesearch AS s
WHERE
	s.name IS NULL

SELECT
	count(*)
FROM
	dbo.SavedResumeSearch AS s
WHERE
	s.name IS NULL

-- Look for duplicate names by generating an identity column which is simply used to rename.

ALTER TABLE dbo.SavedResumeSearch
ADD rank INT NOT NULL IDENTITY
GO

UPDATE
	dbo.SavedResumeSearch
SET
	name = name + ' (2)'
FROM
	dbo.SavedResumeSearch AS s
WHERE
	EXISTS
	(
		SELECT
			*
		FROM
			dbo.SavedResumeSearch AS t
		WHERE
			t.name = s.name
			AND t.ownerId = s.ownerId
			AND t.rank < s.rank
	)

UPDATE
	dbo.SavedResumeSearch
SET
	name = LEFT(name, LEN(name) - 4) + ' (3)'
FROM
	dbo.SavedResumeSearch AS s
WHERE
	EXISTS
	(
		SELECT
			*
		FROM
			dbo.SavedResumeSearch AS t
		WHERE
			t.name = s.name
			AND t.ownerId = s.ownerId
			AND t.rank < s.rank
	)

UPDATE
	dbo.SavedResumeSearch
SET
	name = LEFT(name, LEN(name) - 4) + ' (4)'
FROM
	dbo.SavedResumeSearch AS s
WHERE
	EXISTS
	(
		SELECT
			*
		FROM
			dbo.SavedResumeSearch AS t
		WHERE
			t.name = s.name
			AND t.ownerId = s.ownerId
			AND t.rank < s.rank
	)

UPDATE
	dbo.SavedResumeSearch
SET
	name = LEFT(name, LEN(name) - 4) + ' (5)'
FROM
	dbo.SavedResumeSearch AS s
WHERE
	EXISTS
	(
		SELECT
			*
		FROM
			dbo.SavedResumeSearch AS t
		WHERE
			t.name = s.name
			AND t.ownerId = s.ownerId
			AND t.rank < s.rank
	)

UPDATE
	dbo.SavedResumeSearch
SET
	name = LEFT(name, LEN(name) - 4) + ' (6)'
FROM
	dbo.SavedResumeSearch AS s
WHERE
	EXISTS
	(
		SELECT
			*
		FROM
			dbo.SavedResumeSearch AS t
		WHERE
			t.name = s.name
			AND t.ownerId = s.ownerId
			AND t.rank < s.rank
	)

UPDATE
	dbo.SavedResumeSearch
SET
	name = LEFT(name, LEN(name) - 4) + ' (7)'
FROM
	dbo.SavedResumeSearch AS s
WHERE
	EXISTS
	(
		SELECT
			*
		FROM
			dbo.SavedResumeSearch AS t
		WHERE
			t.name = s.name
			AND t.ownerId = s.ownerId
			AND t.rank < s.rank
	)

UPDATE
	dbo.SavedResumeSearch
SET
	name = LEFT(name, LEN(name) - 4) + ' (8)'
FROM
	dbo.SavedResumeSearch AS s
WHERE
	EXISTS
	(
		SELECT
			*
		FROM
			dbo.SavedResumeSearch AS t
		WHERE
			t.name = s.name
			AND t.ownerId = s.ownerId
			AND t.rank < s.rank
	)

UPDATE
	dbo.SavedResumeSearch
SET
	name = LEFT(name, LEN(name) - 4) + ' (9)'
FROM
	dbo.SavedResumeSearch AS s
WHERE
	EXISTS
	(
		SELECT
			*
		FROM
			dbo.SavedResumeSearch AS t
		WHERE
			t.name = s.name
			AND t.ownerId = s.ownerId
			AND t.rank < s.rank
	)

UPDATE
	dbo.SavedResumeSearch
SET
	name = LEFT(name, LEN(name) - 4) + ' (10)'
FROM
	dbo.SavedResumeSearch AS s
WHERE
	EXISTS
	(
		SELECT
			*
		FROM
			dbo.SavedResumeSearch AS t
		WHERE
			t.name = s.name
			AND t.ownerId = s.ownerId
			AND t.rank < s.rank
	)

UPDATE
	dbo.SavedResumeSearch
SET
	name = LEFT(name, LEN(name) - 5) + ' (11)'
FROM
	dbo.SavedResumeSearch AS s
WHERE
	EXISTS
	(
		SELECT
			*
		FROM
			dbo.SavedResumeSearch AS t
		WHERE
			t.name = s.name
			AND t.ownerId = s.ownerId
			AND t.rank < s.rank
	)

UPDATE
	dbo.SavedResumeSearch
SET
	name = LEFT(name, LEN(name) - 5) + ' (12)'
FROM
	dbo.SavedResumeSearch AS s
WHERE
	EXISTS
	(
		SELECT
			*
		FROM
			dbo.SavedResumeSearch AS t
		WHERE
			t.name = s.name
			AND t.ownerId = s.ownerId
			AND t.rank < s.rank
	)

UPDATE
	dbo.SavedResumeSearch
SET
	name = LEFT(name, LEN(name) - 5) + ' (13)'
FROM
	dbo.SavedResumeSearch AS s
WHERE
	EXISTS
	(
		SELECT
			*
		FROM
			dbo.SavedResumeSearch AS t
		WHERE
			t.name = s.name
			AND t.ownerId = s.ownerId
			AND t.rank < s.rank
	)

UPDATE
	dbo.SavedResumeSearch
SET
	name = LEFT(name, LEN(name) - 5) + ' (14)'
FROM
	dbo.SavedResumeSearch AS s
WHERE
	EXISTS
	(
		SELECT
			*
		FROM
			dbo.SavedResumeSearch AS t
		WHERE
			t.name = s.name
			AND t.ownerId = s.ownerId
			AND t.rank < s.rank
	)

UPDATE
	dbo.SavedResumeSearch
SET
	name = LEFT(name, LEN(name) - 5) + ' (15)'
FROM
	dbo.SavedResumeSearch AS s
WHERE
	EXISTS
	(
		SELECT
			*
		FROM
			dbo.SavedResumeSearch AS t
		WHERE
			t.name = s.name
			AND t.ownerId = s.ownerId
			AND t.rank < s.rank
	)

UPDATE
	dbo.SavedResumeSearch
SET
	name = LEFT(name, LEN(name) - 5) + ' (16)'
FROM
	dbo.SavedResumeSearch AS s
WHERE
	EXISTS
	(
		SELECT
			*
		FROM
			dbo.SavedResumeSearch AS t
		WHERE
			t.name = s.name
			AND t.ownerId = s.ownerId
			AND t.rank < s.rank
	)

UPDATE
	dbo.SavedResumeSearch
SET
	name = LEFT(name, LEN(name) - 5) + ' (17)'
FROM
	dbo.SavedResumeSearch AS s
WHERE
	EXISTS
	(
		SELECT
			*
		FROM
			dbo.SavedResumeSearch AS t
		WHERE
			t.name = s.name
			AND t.ownerId = s.ownerId
			AND t.rank < s.rank
	)

UPDATE
	dbo.SavedResumeSearch
SET
	name = LEFT(name, LEN(name) - 5) + ' (18)'
FROM
	dbo.SavedResumeSearch AS s
WHERE
	EXISTS
	(
		SELECT
			*
		FROM
			dbo.SavedResumeSearch AS t
		WHERE
			t.name = s.name
			AND t.ownerId = s.ownerId
			AND t.rank < s.rank
	)

UPDATE
	dbo.SavedResumeSearch
SET
	name = LEFT(name, LEN(name) - 5) + ' (20)'
FROM
	dbo.SavedResumeSearch AS s
WHERE
	EXISTS
	(
		SELECT
			*
		FROM
			dbo.SavedResumeSearch AS t
		WHERE
			t.name = s.name
			AND t.ownerId = s.ownerId
			AND t.rank < s.rank
	)

UPDATE
	dbo.SavedResumeSearch
SET
	name = LEFT(name, LEN(name) - 5) + ' (21)'
FROM
	dbo.SavedResumeSearch AS s
WHERE
	EXISTS
	(
		SELECT
			*
		FROM
			dbo.SavedResumeSearch AS t
		WHERE
			t.name = s.name
			AND t.ownerId = s.ownerId
			AND t.rank < s.rank
	)

UPDATE
	dbo.SavedResumeSearch
SET
	name = LEFT(name, LEN(name) - 5) + ' (22)'
FROM
	dbo.SavedResumeSearch AS s
WHERE
	EXISTS
	(
		SELECT
			*
		FROM
			dbo.SavedResumeSearch AS t
		WHERE
			t.name = s.name
			AND t.ownerId = s.ownerId
			AND t.rank < s.rank
	)

UPDATE
	dbo.SavedResumeSearch
SET
	name = LEFT(name, LEN(name) - 5) + ' (23)'
FROM
	dbo.SavedResumeSearch AS s
WHERE
	EXISTS
	(
		SELECT
			*
		FROM
			dbo.SavedResumeSearch AS t
		WHERE
			t.name = s.name
			AND t.ownerId = s.ownerId
			AND t.rank < s.rank
	)

UPDATE
	dbo.SavedResumeSearch
SET
	name = LEFT(name, LEN(name) - 5) + ' (24)'
FROM
	dbo.SavedResumeSearch AS s
WHERE
	EXISTS
	(
		SELECT
			*
		FROM
			dbo.SavedResumeSearch AS t
		WHERE
			t.name = s.name
			AND t.ownerId = s.ownerId
			AND t.rank < s.rank
	)

DELETE
	dbo.SavedResumeSearch
FROM
	dbo.SavedResumeSearch AS s
WHERE
	EXISTS
	(
		SELECT
			*
		FROM
			dbo.SavedResumeSearch AS t
		WHERE
			t.name = s.name
			AND t.ownerId = s.ownerId
			AND t.rank < s.rank
	)

ALTER TABLE dbo.SavedResumeSearch
DROP COLUMN rank
GO

