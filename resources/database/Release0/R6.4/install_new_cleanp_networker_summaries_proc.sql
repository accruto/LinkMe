
IF EXISTS (SELECT name 
	   FROM   sysobjects 
	   WHERE  name = 'CleanUpNetworkerOverlookedSummaries' 
	   AND 	  type = 'P')
BEGIN
DROP PROCEDURE linkme_owner.CleanUpNetworkerOverlookedSummaries
END
GO

CREATE PROCEDURE linkme_owner.CleanUpNetworkerOverlookedSummaries
	 @DAYSTOEXPIRY INT
AS
BEGIN

	SELECT DATEDIFF(dd, SEARCHDATE, GETDATE() ) DAYSOLD , @DAYSTOEXPIRY EXPIRYDATE FROM linkme_owner.networker_overlooked_summary
	WHERE DATEDIFF(dd, SEARCHDATE, GETDATE() ) > @DAYSTOEXPIRY

	SELECT [ID] INTO  #tmpNetworkerSummaryIds
	FROM linkme_owner.networker_overlooked_summary
	WHERE DATEDIFF(dd, SEARCHDATE, GETDATE() ) > @DAYSTOEXPIRY
	

	--PRINT 'Deleting Expired Criteria'
	DELETE FROM  linkme_owner.networker_overlooked_search_criteria where ID IN (SELECT ID FROM #tmpNetworkerSummaryIds)
	--PRINT 'Deleting Expired Overlooked'
	DELETE FROM  linkme_owner.networker_overlooked_result where networkerOverlookedSummaryId IN (SELECT ID FROM #tmpNetworkerSummaryIds)
	--PRINT 'Deleting Expired purchases'
	DELETE FROM  linkme_owner.networker_purchased_result where networkerOverlookedSummaryId IN (SELECT ID FROM #tmpNetworkerSummaryIds)
	--PRINT 'Deleting Expired summaries'
	DELETE FROM  linkme_owner.networker_overlooked_summary  where ID IN (SELECT [ID] FROM #tmpNetworkerSummaryIds)
	-- dont really need to drop this as it is local-temporary, but it doesn't hurt 
	drop table #tmpNetworkerSummaryIds
END
GO