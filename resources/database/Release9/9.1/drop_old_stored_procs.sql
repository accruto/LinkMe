IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[CreateAdminUser]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[CreateAdminUser]

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[DeleteOldViewState]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[DeleteOldViewState]

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[GetLinkMeViewState]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[GetLinkMeViewState]

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SaveLinkMeViewState]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[SaveLinkMeViewState]

GO
