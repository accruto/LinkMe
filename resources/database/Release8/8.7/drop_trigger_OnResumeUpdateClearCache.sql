IF  EXISTS (SELECT * FROM sys.triggers WHERE object_id = OBJECT_ID(N'dbo.OnResumeUpdateClearHash'))
    DROP TRIGGER dbo.OnResumeUpdateClearHash
