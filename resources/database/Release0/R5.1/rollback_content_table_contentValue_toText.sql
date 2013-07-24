IF EXISTS (
	SELECT 1
	FROM information_schema.tables
	WHERE table_name = 'Content'
	AND table_schema = 'linkme_owner'
	AND table_type = 'BASE TABLE'
)
BEGIN
	BEGIN TRAN
	DECLARE @ErrorSave INT
	SET @ErrorSave = 0

	SELECT 
	*
	INTO #tmpContent
	FROM linkme_owner.content
	
IF (@@ERROR <> 0) SET @ErrorSave = @@ERROR

	DELETE FROM linkme_owner.Content
IF (@@ERROR <> 0) SET @ErrorSave = @@ERROR

	ALTER TABLE linkme_owner.Content
		DROP COLUMN contentValue
IF (@@ERROR <> 0) SET @ErrorSave = @@ERROR

	ALTER TABLE linkme_owner.Content
		ADD contentValue varchar(8000) NULL
IF (@@ERROR <> 0) SET @ErrorSave = @@ERROR

	IF @ErrorSave <> 0 
	BEGIN 
		ROLLBACK TRAN 
	END
	ELSE 
	BEGIN 
		COMMIT TRAN
	END
END
GO
BEGIN
	BEGIN TRAN

	INSERT INTO linkme_owner.Content SELECT [id], contentKey, CONVERT(varchar(8000), contentValue) FROM #tmpContent
	DROP TABLE #tmpContent
	IF @@ERROR <> 0 
	BEGIN 
		ROLLBACK TRAN 
	END
	ELSE 
	BEGIN 
		COMMIT TRAN
	END

	SELECT 'IMPORTANT: PLEASE CHECK DB FOR OPEN TRANSACTIONS'
END
GO
