IF  EXISTS (SELECT * FROM sys.triggers WHERE object_id = OBJECT_ID(N'[dbo].[OnRegisteredUserUpdate]'))
DROP TRIGGER [dbo].[OnRegisteredUserUpdate]

IF  EXISTS (SELECT * FROM sys.triggers WHERE object_id = OBJECT_ID(N'[dbo].[OnMemberUpdate]'))
DROP TRIGGER [dbo].[OnMemberUpdate]

IF  EXISTS (SELECT * FROM sys.triggers WHERE object_id = OBJECT_ID(N'[dbo].[OnLocationReferenceUpdate]'))
DROP TRIGGER [dbo].[OnLocationReferenceUpdate]

IF  EXISTS (SELECT * FROM sys.triggers WHERE object_id = OBJECT_ID(N'[dbo].[OnCandidateIndustryInsertDelete]'))
DROP TRIGGER [dbo].[OnCandidateIndustryInsertDelete]

IF  EXISTS (SELECT * FROM sys.triggers WHERE object_id = OBJECT_ID(N'[dbo].[OnNetworkLinkInsert]'))
DROP TRIGGER [dbo].[OnNetworkLinkInsert]

IF  EXISTS (SELECT * FROM sys.triggers WHERE object_id = OBJECT_ID(N'[dbo].[OnCandidateUpdate]'))
DROP TRIGGER [dbo].[OnCandidateUpdate]

IF  EXISTS (SELECT * FROM sys.triggers WHERE object_id = OBJECT_ID(N'[dbo].[OnBlogPostInsert]'))
DROP TRIGGER [dbo].[OnBlogPostInsert]

IF  EXISTS (SELECT * FROM sys.triggers WHERE object_id = OBJECT_ID(N'[dbo].[OnNetworkerUpdate]'))
DROP TRIGGER [dbo].[OnNetworkerUpdate]

IF  EXISTS (SELECT * FROM sys.triggers WHERE object_id = OBJECT_ID(N'[dbo].[OnWhiteboardMessageInsert]'))
DROP TRIGGER [dbo].[OnWhiteboardMessageInsert]

IF  EXISTS (SELECT * FROM sys.triggers WHERE object_id = OBJECT_ID(N'[dbo].[OnCachedMemberDataUpdate]'))
DROP TRIGGER [dbo].[OnCachedMemberDataUpdate]