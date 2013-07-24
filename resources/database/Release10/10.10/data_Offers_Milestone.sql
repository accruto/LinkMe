DECLARE @criteriaId UNIQUEIDENTIFIER

SET @criteriaId = '04650F3E-57C1-4622-933F-8859F52B30F5'
IF NOT EXISTS (SELECT * FROM OfferingCriteria WHERE id = @criteriaId AND name = 'Country')
	INSERT
		OfferingCriteria (id, name, value)
	VALUES
		(@criteriaId, 'Country', 1)

SET @criteriaId = '9215CFE9-8E35-4962-B3CB-81B190CF364A'
IF NOT EXISTS (SELECT * FROM OfferingCriteria WHERE id = @criteriaId AND name = 'Country')
	INSERT
		OfferingCriteria (id, name, value)
	VALUES
		(@criteriaId, 'Country', 1)

SET @criteriaId = '03223229-1CB9-4DB7-8207-57A05F865E76'
IF NOT EXISTS (SELECT * FROM OfferingCriteria WHERE id = @criteriaId AND name = 'Country')
	INSERT
		OfferingCriteria (id, name, value)
	VALUES
		(@criteriaId, 'Country', 1)

SET @criteriaId = '3FCA9961-7508-49ED-ABFC-AF63ADEDBE99'
IF NOT EXISTS (SELECT * FROM OfferingCriteria WHERE id = @criteriaId AND name = 'Country')
	INSERT
		OfferingCriteria (id, name, value)
	VALUES
		(@criteriaId, 'Country', 1)

