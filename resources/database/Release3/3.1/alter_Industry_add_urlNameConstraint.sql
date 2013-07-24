-- Add the urlName constraint

ALTER TABLE dbo.Industry
ADD CONSTRAINT UQ_Industry_urlName UNIQUE (urlName)

GO