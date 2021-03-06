IF  EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[Address]') AND name = N'IX_Address_locationReferenceId')
DROP INDEX [IX_Address_locationReferenceId] ON [dbo].[Address]
GO

CREATE UNIQUE NONCLUSTERED INDEX [IX_Address_locationReferenceId] ON [dbo].[Address]
(
	[locationReferenceId]
)
GO

