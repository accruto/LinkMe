SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS OFF 
GO

IF EXISTS (SELECT name 
	   FROM   sysobjects 
	   WHERE  name = 'GetAccountTransactionSummaryByDateRange' 
	   AND 	  type = 'P')
DROP PROCEDURE linkme_owner.GetAccountTransactionSummaryByDateRange
GO

CREATE PROCEDURE linkme_owner.GetAccountTransactionSummaryByDateRange	
	 @startDate DATETIME,
	 @endDate DATETIME,
	 @employerID VARCHAR(50)
AS
BEGIN
	select  SUM(CASE
			WHEN transactions.TransactionType = 2 Then transactions.gstComponent
			WHEN transactions.TransactionType = 3 Then transactions.gstComponent
			ELSE 0
			END) AS GST,
		sum(CASE
			WHEN transactions.TransactionType = 2 THEN transactions.MoneyComponent
			WHEN transactions.TransactionType = 3 THEN transactions.MoneyComponent
			ELSE 0
			END) as Dollars,
		sum(CASE
			WHEN transactions.TransactionType = 0 THEN  transactions.LinkMeCreditsComponent
			ELSE 0 
			END) as CreditAllocation,
		sum(CASE
			WHEN transactions.TransactionType = 1 THEN  transactions.LinkMeCreditsComponent
			ELSE 0 
			END) as CreditPurchase,
		(select sum(1) 
			from linkme_owner.account_transaction_line_item as lineItems INNER JOIN
			linkme_owner.Account_Transaction as trans on lineItems.accTransactionId = trans.Id
			where trans.TransactionDate  >= @startDate
			and trans.TransactionDate  < @endDate
			and trans.userAccountId = @employerID
			and lineItems.productType = 0		
		) AS Resumes
		
	from linkme_owner.Account_Transaction as transactions
	where	transactions.userAccountId = @employerID
		and transactions.TransactionDate  >= @startDate
		and transactions.TransactionDate  < @endDate
END

GO
SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO
