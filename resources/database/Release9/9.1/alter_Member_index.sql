IF  EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[Member]') AND name = N'IX_Member_addressId')
DROP INDEX [IX_Member_addressId] ON [dbo].[Member]
GO

CREATE UNIQUE NONCLUSTERED INDEX [IX_Member_addressId] ON [dbo].[Member]
(
	[addressId]
)
GO

