CREATE NONCLUSTERED INDEX [IX_NetworkLink_toNetworkerId_fromNetworkerId_addedTime] ON [dbo].[NetworkLink] 
(
	[toNetworkerId] ASC,
	[fromNetworkerId] ASC,
	[addedTime] ASC
)
