-- Remove verification on these companies, as they wish to reshuffle all their users
UPDATE Company SET verifiedById = NULL WHERE name LIKE 'Alliance%'