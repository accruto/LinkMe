DELETE linkme_owner.JobApplication
FROM linkme_owner.JobApplication ja
WHERE NOT EXISTS
(
	SELECT *
	FROM linkme_owner.CandidateListEntry cle
	WHERE cle.jobApplicationId = ja.id
)