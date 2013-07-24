UPDATE linkme_owner.ResumeSearchCriteriaSet
SET type = 'au.com.venturelogic.linkme.common.managers.search.SimpleResumeSearchCriteria'
WHERE type = 'au.com.venturelogic.linkme.common.managers.search.BasicSearchCriteria' OR type = 'SimpleSearch_1.8' OR type = 'SuggestedCandidates_2.1'

UPDATE linkme_owner.ResumeSearchCriteriaSet
SET type = 'au.com.venturelogic.linkme.common.managers.search.AdvancedResumeSearchCriteria'
WHERE type = 'au.com.venturelogic.linkme.common.managers.search.AdvancedSearchCriteria' OR type = 'AdvancedSearch_1.8'

GO
