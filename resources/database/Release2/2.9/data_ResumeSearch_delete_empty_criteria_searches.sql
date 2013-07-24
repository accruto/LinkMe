-- Delete saved resume searches referencing the empty criteria sets

DELETE linkme_owner.SavedResumeSearch
FROM linkme_owner.ResumeSearchCriteriaSet rscs
INNER JOIN linkme_owner.SavedResumeSearch srs
ON srs.criteriaSetId = rscs.id
WHERE NOT EXISTS
(
	SELECT *
	FROM linkme_owner.ResumeSearchCriteria
	WHERE setId = rscs.id
)

-- Delete resume searches referencing the empty criteria sets

DELETE linkme_owner.ResumeSearch
FROM linkme_owner.ResumeSearchCriteriaSet rscs
INNER JOIN linkme_owner.ResumeSearch rs
ON rs.criteriaSetId = rscs.id
WHERE NOT EXISTS
(
	SELECT *
	FROM linkme_owner.ResumeSearchCriteria
	WHERE setId = rscs.id
)

-- Delete the empty criteria sets themselves

DELETE linkme_owner.ResumeSearchCriteriaSet
FROM linkme_owner.ResumeSearchCriteriaSet rscs
WHERE NOT EXISTS
(
	SELECT *
	FROM linkme_owner.ResumeSearchCriteria
	WHERE setId = rscs.id
)

-- Note that data_SavedResumeSearchAlert_delete_orphaned_alerts.sql needs to be run after this
-- to delete the newly orphaned alerts.
