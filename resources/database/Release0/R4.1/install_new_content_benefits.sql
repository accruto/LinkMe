IF EXISTS (
	SELECT 1
	FROM information_schema.tables
	WHERE table_name = 'content'
	AND table_schema = 'linkme_owner'
	AND table_type = 'BASE TABLE'
)
BEGIN
	INSERT INTO linkme_owner.content VALUES ('6', 'networker.benefits', '<UL><LI><FONT face=Arial size=2>Create a networker fast</FONT><LI><FONT face=Arial size=2>Seek advice from your peers</FONT><LI><FONT face=Arial size=2>Make new contacts</FONT><LI><FONT face=Arial size=2>Unleash the hidden job market</FONT><LI><FONT face=Arial size=2>Rank your resume against your colleagues</FONT><LI><FONT face=Arial size=2>Endorse your friends</FONT><LI><FONT face=Arial size=2>Leverage existing relationships</FONT><LI><FONT face=Arial size=2>Develop partnerships</FONT><LI><FONT face=Arial size=2>Exchange knowledge</FONT><LI><FONT face=Arial size=2>Join for Free</FONT> </LI></UL>')
	INSERT INTO linkme_owner.content VALUES ('7', 'employer.benefits', '<UL><LI><FONT face=Arial size=2>Load a position description and locate the ideal candidate</FONT><LI><FONT face=Arial size=2>Sophisticated search and ranking formulas</FONT><LI><FONT face=Arial size=2>Increase your search capability</FONT><LI><FONT face=Arial size=2>Locate quality candidates fast</FONT> </LI></UL>')
END

GO