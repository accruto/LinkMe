IF EXISTS (SELECT name 
	   FROM   sysobjects 
	   WHERE  name = 'GetAccountTransactionSummaryByDateRange' 
	   AND 	  type = 'P')
DROP PROCEDURE linkme_owner.GetAccountTransactionSummaryByDateRange
GO


