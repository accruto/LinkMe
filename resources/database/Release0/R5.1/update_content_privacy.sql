
-- UPDATES THE PRIVACY STATEMENT


IF NOT EXISTS (SELECT 1 FROM linkme_owner.Content WHERE contentKey = 'aboutus.privacy')
	BEGIN
		BEGIN TRAN
			INSERT INTO linkme_owner.Content VALUES('aed6c63fd9ec4f7c85440006b1017a20', 'aboutus.privacy','')
		COMMIT TRAN

		declare @objFSys int
		declare @objFile int
		declare @blnEndOfFile int
		declare @strLine varchar(8000)
		exec sp_OACreate 'Scripting.FileSystemObject', @objFSys out
		
		-- Change the file path to the one that is passed to your stored procedure
		exec sp_OAMethod @objFSys, 'OpenTextFile', @objFile out, 'C:\Projects\LinkMe\resources\database\R5.1\privacy_statement_content.txt', 1
		exec sp_OAMethod @objFile, 'AtEndOfStream', @blnEndOfFile out
		
		DECLARE @OFFSET INT 
		SET @OFFSET = 0
		-- get each line in turn
		while @blnEndOfFile=0 begin
			exec sp_OAMethod @objFile, 'ReadLine', @strLine out
			-- Here you got one line from the file
			
			DECLARE @LEN INT
			SET @LEN = LEN(@strLine)  
			
			
			DECLARE @ptrval binary(16)
			SELECT @ptrval = TEXTPTR(contentValue) FROM linkme_owner.Content WHERE contentKey = 'aboutus.privacy'
			UPDATETEXT linkme_owner.Content.ContentValue  @ptrval @OFFSET NULL @strLine
			
			SET @OFFSET = @OFFSET + @LEN
			
			exec sp_OAMethod @objFile, 'AtEndOfStream', @blnEndOfFile out
		end
		-- clean up
		exec sp_OADestroy @objFile
		exec sp_OADestroy @objFSys
	END
ELSE
	BEGIN
		PRINT ' Privacy Statement Already Exists'
	END
GO
IF  EXISTS (SELECT 1 FROM linkme_owner.Content WHERE contentKey = 'aboutus.privacy')
	BEGIN
	 	SELECT 'Privacy Statement Created!'
	END
ELSE
	BEGIN
		SELECT 'Privacy Statement Not Created!'
	END
GO