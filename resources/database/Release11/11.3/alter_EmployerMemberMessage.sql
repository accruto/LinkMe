BEGIN TRANSACTION
GO
ALTER TABLE dbo.EmployerMemberMessage ADD
	messageType int NOT NULL CONSTRAINT DF_EmployerMemberMessage_messageType DEFAULT 0
GO
COMMIT

ALTER TABLE dbo.EmployerMemberMessage
ADD representativeId UNIQUEIDENTIFIER NULL
