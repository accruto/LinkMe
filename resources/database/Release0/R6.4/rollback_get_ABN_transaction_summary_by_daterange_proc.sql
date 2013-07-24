IF EXISTS (SELECT name 
	   FROM   sysobjects 
	   WHERE  name = 'GetABNTransactionSummaryByDateRange' 
	   AND 	  type = 'P')
DROP PROCEDURE  linkme_owner.GetABNTransactionSummaryByDateRange
GO