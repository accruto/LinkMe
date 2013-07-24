DELETE
	dbo.Product
WHERE
	id IN ('{6D28E05B-8B82-4ABB-ABFB-9F5F8728BAC9}', '{066A2F5E-9F85-4B09-AC4A-08FF15454D3B}')

INSERT
	dbo.Product (id, name, enabled, userTypes, price, currency)
VALUES
	('{6D28E05B-8B82-4ABB-ABFB-9F5F8728BAC9}', 'FeaturePack1', 1, 2, 10, 36)

INSERT
	dbo.Product (id, name, enabled, userTypes, price, currency)
VALUES
	('{066A2F5E-9F85-4B09-AC4A-08FF15454D3B}', 'FeaturePack2', 1, 2, 20, 36)
	
