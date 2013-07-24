ALTER TABLE linkme_owner.employer_profile
ADD flags INT NULL
GO

-- Initially enable ShowSuggestedCandidatesPage and ReceiveSuggestedCandidatesEmails for all active users.

UPDATE linkme_owner.employer_profile
SET flags = 0

UPDATE linkme_owner.employer_profile
SET flags = 3
FROM linkme_owner.employer_profile ep
INNER JOIN linkme_owner.user_profile up
ON ep.id = up.id
WHERE NOT (up.firstName = 'Linking' AND up.lastName = 'Node')
GO

ALTER TABLE linkme_owner.employer_profile
ALTER COLUMN flags INT NOT NULL
GO
