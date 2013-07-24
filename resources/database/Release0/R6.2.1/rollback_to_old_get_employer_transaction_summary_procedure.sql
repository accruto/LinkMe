IF EXISTS (SELECT name 
	   FROM   sysobjects 
	   WHERE  name = 'GetEmployerTransactionSummaryByDateRangeAndType' 
	   AND 	  type = 'P')
DROP PROCEDURE  linkme_owner.GetEmployerTransactionSummaryByDateRangeAndType
GO

CREATE PROCEDURE linkme_owner.GetEmployerTransactionSummaryByDateRangeAndType	
	 @startDate DATETIME,
	 @endDate DATETIME,
	 @transactionType VARCHAR(50)
AS
BEGIN
	select  transactions.InvoiceNumber,
	        transactions.VerisignInvoice,
		employer.OrganisationName, 
		account.AbnNumber, 
		userProfile.FirstName, 
		userProfile.LastName,
		transactions.GstComponent GST,
		transactions.MoneyComponent DOLLARS,
		transactions.LinkMeCreditsComponent CREDITS,
		(select count(lineItems.id) 
			from linkme_owner.account_transaction_line_item as lineItems 
			where lineItems.accTransactionId = transactions.Id 			
		) RESUMES,
		employer.ContactPhoneNumber,
		employer.EmailAddress,
		userProfile.UserId,
		employer.Id
	from 
		linkme_owner.Employer_Profile as employer inner join User_Profile userProfile ON employer.Id = userProfile.ID
		inner join linkme_owner.User_Account as account on employer.Id = account.Id
		inner join linkme_owner.Account_Transaction as transactions on transactions.userAccountId = account.Id
	where	 	
		transactions.TransactionType = @transactionType
		and transactions.TransactionDate  >= @startDate
		and transactions.TransactionDate  < @endDate
	ORDER BY transactions.InvoiceNumber

END
GO