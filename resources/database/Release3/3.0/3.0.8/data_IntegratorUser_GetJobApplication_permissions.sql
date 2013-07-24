-- Grant PostJobAds and GetJobApplication to IPA and Personal Concept

UPDATE dbo.IntegratorUser
SET [permissions] = 3
WHERE username = 'ipa_jobs' OR username = 'PersonalConcept'
