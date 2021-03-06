IF  EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[dbo].[MemberContactsByMember]'))
DROP VIEW [dbo].[MemberContactsByMember]
GO

CREATE VIEW dbo.MemberContactsByMember (memberId, reason, contacts)
WITH SCHEMABINDING
AS
	SELECT memberId, reason, COUNT_BIG(*)
	FROM dbo.MemberContact
	GROUP BY memberId, reason
GO

CREATE UNIQUE CLUSTERED INDEX IX_MemberContactsByMember
ON dbo.MemberContactsByMember (memberId, reason)
GO
