UPDATE linkme_owner.JobAd
SET location = l.locality + ', ' + l.state
FROM linkme_owner.JobAd ja
INNER JOIN linkme_owner.locality l
ON l.postcode = ja.postcode
WHERE ja.postcode != '' AND (ja.location = ja.postcode OR ja.location = ' ' + ja.postcode
	OR ja.location = 'QLD ' + ja.postcode OR ja.location = 'NSW ' + ja.postcode OR ja.location = 'WA ' + ja.postcode
	OR ja.location = 'VIC ' + ja.postcode OR ja.location = 'ACT ' + ja.postcode)
	AND ja.postcode IN
(
	SELECT postcode
	FROM linkme_owner.locality
	WHERE locality != ''
	GROUP BY postcode
	HAVING COUNT(postcode) = 1
)
