SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS OFF 
GO

IF EXISTS (SELECT name 
	   FROM   sysobjects 
	   WHERE  name = 'GetABNTransactionSummaryByDateRange' 
	   AND 	  type = 'P')
DROP PROCEDURE linkme_owner.GetABNTransactionSummaryByDateRange
GO


CREATE PROCEDURE linkme_owner.GetABNTransactionSummaryByDateRange	
	 @startDate DATETIME,
	 @endDate DATETIME,
	 @abnNumber VARCHAR(11)
AS
BEGIN
	select  SUM(CASE
			WHEN transactions.TransactionType = 2 THEN  transactions.gstComponent
			WHEN transactions.TransactionType = 3 THEN transactions.gstComponent
			ELSE 0 
			END) AS GST,
		sum(CASE
			WHEN transactions.TransactionType = 2 THEN  transactions.MoneyComponent
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
			from linkme_owner.user_account as acct INNER JOIN linkme_owner.Account_Transaction as trans
			ON acct.Id = trans.userAccountId INNER JOIN 
			linkme_owner.account_transaction_line_item as lineItems on lineItems.accTransactionId = trans.Id
			where trans.TransactionDate  >= @startDate
			and trans.TransactionDate  < @endDate
			and acct.abnNumber = @abnNumber
			and lineItems.productType = 0		
		) AS Resumes
		
	from linkme_owner.user_account as account INNER JOIN linkme_owner.Account_Transaction as transactions
		ON account.Id = transactions.userAccountId
	where	account.abnNumber = @abnNumber
		and transactions.TransactionDate  >= @startDate
		and transactions.TransactionDate  < @endDate
END

GO
SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO