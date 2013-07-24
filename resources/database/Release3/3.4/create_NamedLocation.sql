
IF NOT EXISTS (SELECT * FROM dbo.SYSOBJECTS WHERE id = OBJECT_ID('NamedLocation') AND OBJECTPROPERTY(id, 'IsUserTable') = 1)
BEGIN

	-- Create the table

	CREATE TABLE NamedLocation ( 
		id int NOT NULL,
		displayName LocationDisplayName NULL,
	)

	-- Create the primary key

	ALTER TABLE NamedLocation ADD CONSTRAINT PK_NamedLocation
		PRIMARY KEY CLUSTERED (id)
		
END
