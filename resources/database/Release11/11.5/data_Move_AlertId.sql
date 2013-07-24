UPDATE SavedResumeSearchAlert
SET SavedResumeSearchId = 
(SELECT id
FROM SavedResumeSearch
WHERE SavedResumeSearchAlert.id = SavedResumeSearch.alertId)