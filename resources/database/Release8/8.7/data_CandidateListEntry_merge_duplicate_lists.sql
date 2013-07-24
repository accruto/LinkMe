-- Where there are multiple default lists for the same owner move all the entries to the one
-- with the lowest ID. The rest will be deleted as empty.

-- First delete the entries duplicated between the lists, except the one to be kept.

DELETE cleRemove
FROM dbo.CandidateListEntry cleRemove
INNER JOIN dbo.CandidateList clRemove
ON cleRemove.candidateListId = clRemove.[id]
INNER JOIN dbo.CandidateList clKeep
ON clRemove.ownerId = clKeep.ownerId AND clRemove.[id] > clKeep.[id]
INNER JOIN dbo.CandidateListEntry cleKeep
ON cleKeep.candidateListId = clKeep.[id] AND cleRemove.candidateId = cleKeep.candidateId
WHERE clKeep.[name] IS NULL AND clRemove.[name] IS NULL
GO

-- Now move the remaining entries to the list with the lowest ID.

UPDATE dbo.CandidateListEntry
SET candidateListId = clKeep.[id]
FROM dbo.CandidateListEntry cle
INNER JOIN dbo.CandidateList clRemove
ON cle.candidateListId = clRemove.[id]
INNER JOIN dbo.CandidateList clKeep
ON clRemove.ownerId = clKeep.ownerId AND clRemove.[id] > clKeep.[id]
WHERE clKeep.[name] IS NULL AND clRemove.[name] IS NULL
GO
