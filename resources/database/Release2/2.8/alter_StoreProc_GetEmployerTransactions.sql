if exists (select * from dbo.sysobjects where id = object_id(N'[linkme_owner].[GetEmployerTransactionSummaryByDateRangeAndType]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [linkme_owner].[GetEmployerTransactionSummaryByDateRangeAndType]
GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS OFF 
GO

setuser N'linkme_owner'
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
		userProfile.FirstName, 
		userProfile.LastName,
		transactions.GstComponent GST,
		transactions.MoneyComponent DOLLARS,
		transactions.LinkMeCreditsComponent CREDITS,
		(select count(lineItems.id) 
			from linkme_owner.account_transaction_line_item as lineItems 
			where lineItems.accTransactionId = transactions.Id 	
			and lineItems.productType = 0		
		) RESUMES,
		employer.ContactPhoneNumber,
		employer.EmailAddress,
		userProfile.UserId,
		employer.Id,
		transactions.transactionDate
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
setuser
GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO

