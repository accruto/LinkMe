-- WARNING: This script deletes all resume search data!

DELETE ResumeSearch
GO
DELETE ResumeSearchCriteria
GO
DELETE ResumeSearchCriteriaSet
GO
DELETE ResumeSearchResult
GO
DELETE ResumeSearchResultSet
GO

-- Drop some constraints temporarily.

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('FK_ResumeSearch_ResumeSearchCriteriaSet') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE ResumeSearch DROP CONSTRAINT FK_ResumeSearch_ResumeSearchCriteriaSet
GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('FK_ResumeSearch_ResumeSearchResultSet') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE ResumeSearch DROP CONSTRAINT FK_ResumeSearch_ResumeSearchResultSet
GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('UQ_ResumeSearch_resultSetId') AND OBJECTPROPERTY(id, 'IsUniqueCnst') = 1)
ALTER TABLE ResumeSearch DROP CONSTRAINT UQ_ResumeSearch_resultSetId
GO

-- Use a bit of a hack here: insert the original ID into the duration column so we can join on it for the criteria and results.

INSERT INTO ResumeSearch(id, startTime, duration, context, searcherId, criteriaSetId, resultSetId)
SELECT NEWID(), runDate, id, context, profileId, NEWID(), NEWID()
FROM linkme_owner.search_log
GO

INSERT INTO ResumeSearchCriteriaSet (id)
SELECT criteriaSetId
FROM ResumeSearch
GO

INSERT INTO ResumeSearchCriteria(setId, name, value)
SELECT rs.criteriaSetId, slc.criteriaType, slc.criteriaValue
FROM linkme_owner.search_log_criteria slc
INNER JOIN ResumeSearch rs
ON slc.searchLogId = rs.duration -- The hack.
GO

INSERT INTO ResumeSearchResultSet (id, count)
SELECT rs.resultSetId, sl.resultCount
FROM ResumeSearch rs
INNER JOIN linkme_owner.search_log sl
ON sl.id = rs.duration
GO

INSERT INTO ResumeSearchResult(setId, rank, resumeId)
SELECT rs.resultSetId, slr.rank, slr.networkerId
FROM linkme_owner.search_log_result slr
INNER JOIN ResumeSearch rs
ON slr.searchLogId = rs.duration -- The same hack.
GO

UPDATE ResumeSearch
SET duration = NULL -- Remove the hack.
GO

-- Restore the constraints.

ALTER TABLE ResumeSearch WITH CHECK
	ADD CONSTRAINT FK_ResumeSearch_ResumeSearchCriteriaSet FOREIGN KEY (criteriaSetId) REFERENCES ResumeSearchCriteriaSet (id)
GO

ALTER TABLE ResumeSearch WITH CHECK
	ADD CONSTRAINT FK_ResumeSearch_ResumeSearchResultSet FOREIGN KEY (resultSetId) REFERENCES ResumeSearchResultSet (id)
GO

ALTER TABLE ResumeSearch WITH CHECK
	ADD CONSTRAINT UQ_ResumeSearch_resultSetId UNIQUE (resultSetId)
GO
