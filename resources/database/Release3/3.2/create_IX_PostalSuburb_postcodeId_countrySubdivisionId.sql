CREATE NONCLUSTERED INDEX [IX_PostalSuburb_postcodeId_countrySubdivisionId] ON [dbo].[PostalSuburb] 
(
	[postcodeId] ASC,
	[countrySubdivisionId] ASC
)
