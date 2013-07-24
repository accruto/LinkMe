if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[OnResumeInsertUpdate]') and OBJECTPROPERTY(id, N'IsTrigger') = 1)
drop trigger [dbo].[OnResumeInsertUpdate]
GO

CREATE TRIGGER OnResumeInsertUpdate
ON Resume
FOR INSERT, UPDATE

AS

-- Only run if the XML was actually updated - ignore changes to sha1sum, etc.

IF UPDATE(lensXml)
BEGIN
	-- Need to set this for NHibernate.
	
	SET NOCOUNT ON
	
	INSERT
		dbo.NetworkerEvent ( id, [time], type, actorId )
	SELECT
		NEWID(), GETDATE(), 100, i.candidateId
	FROM
		inserted AS i
	
	SET NOCOUNT OFF
END