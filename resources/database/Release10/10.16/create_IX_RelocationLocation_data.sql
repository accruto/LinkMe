IF  EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[RelocationLocation]') AND name = N'IX_RelocationLocation_data')
DROP INDEX [IX_RelocationLocation_data] ON [dbo].[RelocationLocation] WITH ( ONLINE = OFF )
GO

CREATE NONCLUSTERED INDEX IX_RelocationLocation_data ON dbo.RelocationLocation 
(
	candidateId ASC,
	locationReferenceId ASC
)
