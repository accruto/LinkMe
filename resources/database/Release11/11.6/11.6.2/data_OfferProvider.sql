UPDATE
	dbo.OfferProvider
SET
	enabled = 0
WHERE
	name IN ('InterviewGOLD', 'AIG')
	