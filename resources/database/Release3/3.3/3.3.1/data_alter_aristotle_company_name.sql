-- Change the company name of Aristotle (and all variants) to Eurolink...
UPDATE Company
SET name = 'Eurolink Consulting Australia (trading as Aristotle)'
WHERE name LIKE 'Aristotle%'