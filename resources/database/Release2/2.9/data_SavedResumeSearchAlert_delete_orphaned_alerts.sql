DELETE	linkme_owner.SavedResumeSearchAlert
FROM	linkme_owner.SavedResumeSearchAlert alert
WHERE	NOT EXISTS
(
	SELECT	*
	FROM linkme_owner.SavedResumeSearch search
	where search.alertId = alert.id
)