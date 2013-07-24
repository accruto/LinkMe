EXECUTE sp_rename N'dbo.ResourceQuestion.test', N'Tmp_text_3', 'COLUMN' 
GO
EXECUTE sp_rename N'dbo.ResourceQuestion.Tmp_text_3', N'text', 'COLUMN' 
GO
