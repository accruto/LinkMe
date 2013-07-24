INSERT INTO dbo.CandidateAccessPurchase([id], purchaseTime, searcherId, candidateId)
SELECT dbo.GuidFromString([id]), purchaseDate, dbo.GuidFromString(employerId),
	dbo.GuidFromString(networkerId)
FROM linkme_owner.employer_purchases
GO
