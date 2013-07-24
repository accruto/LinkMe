IF EXISTS (
	SELECT 1
	FROM information_schema.tables
	WHERE table_name = 'content'
	AND table_schema = 'linkme_owner'
	AND table_type = 'BASE TABLE'
)
BEGIN
	DROP TABLE linkme_owner.content
END

GO

IF NOT EXISTS (
	SELECT 1
	FROM information_schema.tables
	WHERE table_name = 'content'
	AND table_schema = 'linkme_owner'
	AND table_type = 'BASE TABLE'
)
BEGIN
	CREATE TABLE linkme_owner.content (
		id VARCHAR(50),
		contentKey VARCHAR(255) NULL,
		contentValue VARCHAR(8000) NULL
		
		CONSTRAINT pk_content PRIMARY KEY (id)
	)
END

GO

IF EXISTS (
	SELECT 1
	FROM information_schema.tables
	WHERE table_name = 'content'
	AND table_schema = 'linkme_owner'
	AND table_type = 'BASE TABLE'
)
BEGIN
	INSERT INTO linkme_owner.content VALUES ('1', 'homepage.notice', '')
	INSERT INTO linkme_owner.content VALUES ('2', 'employer.notice', 'You can upload a resume of someone and LinkMe will return a list of Contacts ranked according to how well they match the provided resume. This is perfect for when you need to find a replacement for someone who is leaving their job!')
	INSERT INTO linkme_owner.content VALUES ('3', 'networker.notice', 'You can rank your resume against other networkers on LINKME and gain some insight into how well you might go applying for jobs and what gaps might exist in your resume - Try the customer search in the Rank My Resume tool and see how you go!')
	INSERT INTO linkme_owner.content VALUES ('4', 'networker.faq.text', '<P><B>Q:</B> Why can I see some contacts when I do a search and others have their identities masked? <BR><B>A:</B> This is because you can are only allowed to view the identity of networkers who are from your first to third degrees of separation. You may contact these networkers at no charge. To contact networkers beyond your third degree or out of your network you must pay a small fee <BR><BR><B>Q:</B> I typed in a few keywords and got either very few matches or matches with very low scores.<BR><B>A:</B> While Keyword searching is enabled it is not the best method to search - Resumes represent a large chunk of data with a complex meaning. Our search technology is more meaning focused - this means it can match on terms but will provide a much better return if you give it more information. Try writing out exactly what you want: (see example) <BR><BR><I>I am looking for someone with 5 years of experience working in sales promotion and advertising. Experience in the FMCG sector is most important. They have worked on large campaigns and primarily work in the Sydney Market (This is only 184 characters - you can use 400 characters) </I><BR><BR><B>Q:</B> How does scoring work?<BR><B>A:</B> Scoring is in a range from 0 - 1000. The networker search is limited so scores will always be much lower than 1000 however, scores should be at least 200 for a decent match and even higher for a good match. In the very near future we will be releasing a new version of LINKME which will improve networker searching and we may also offer networkers a full search product as well <BR><BR><STRONG>Scores are percentages of match (The higher the score the higher the quality of match):</STRONG></P>
<LI><STRONG>A score of 10 is a 1% match - Bad Match / No Match </STRONG>
<LI><STRONG>A score of 500 is 50% match - Better Match </STRONG>
<LI><STRONG>A score of 998 is 99.8% match - High Quality Match <BR><BR></STRONG><I><STRONG>How the LINKME engine actually works</STRONG> <BR><BR>A traditional match engine is computing the percentage overlap between two documents -- for example, the percentage of words or concepts in the job description that appear in the resume. The more references a resume includes to those words or concepts, the higher it will score. <BR><BR>LINKME''s technology score differs in so far as it is not a measure of overlap. It is a prediction of the probability that this job could be the next job on the candidate''s resume. <BR><BR>LINKME renders that prediction based on a combination of a matching of concepts, as generalized based on correlations from a large number of real resumes, and on our analysis of the actual employment behavior patterns displayed across millions of actual career transitions. This last point is particularly key: across thousands or millions of actual career transitions observed, what career paths landed people this kind of job in the past and then, importantly, how does this candidate''s career path compare with theirs? <BR><BR>This means that LINKME is able to deliver results that are far more intuitively relevant because LINKME gives the highest scores to those who most closely resemble the candidates who have been placed into this kind of job in the past. <BR><BR></I><B>Q:</B> How do years of experience work?<BR><B>A:</B> If someone is a waiter for 6 years and a business analyst for 1 year and puts both jobs on their resume, the system will show 7 years of experience. (It is probably better in this case to remove the experience which does not relate to your desired career progression) <BR></LI>')
	INSERT INTO linkme_owner.content VALUES ('5', 'employer.faq.text', '<STRONG>Q:</STRONG> What should I use to search for candidates?<BR><B>A:</B> You can use either a job description or a resume of an "ideal candidate." If you are using a job / position description it is best to use the entire job description. The more data you put into your search, the higher quality search results you will get. If the position description does not contain the full attributes for the candidate that you wish to find then add these attributes before submitting your search. You can also submit a resume which you have made up, the resume of the person leaving the position or the resume of someone who fits the position but might not be available. <BR><BR><B>Q:</B> How does scoring work?<BR><B>A:</B> Scoring is a range from 0-1000 - For Recruiter / Employer search you want a score of at least 500 to buy a resume. Scores above 500 are much more desirable. Scores of 750 or greater will be high quality matches. So if your search does not result in scores of at least 500 then you have either not submitted enough information from which to make a quality match or a candidate which matches your query does not exist in the LINKME database at the time of your search. <I>Always submit as much information as you can about the position or attributes of the candidate, which you require to ensure a quality match.</I><BR><BR><B>Scores are percentages of match (The higher the score the higher the quality of match): <BR><BR>
<LI>A score of 10 is a 1% match - Bad Match / No Match 
<LI>A score of 500 is 50% match - Better Match 
<LI>A score of 998 is 99.8% match - High Quality Match <BR><BR><I>How the LINKME engine actually works</I></B><BR><BR>A traditional match engine is computing the percentage overlap between two documents -- for example, the percentage of words or concepts in the job description that appear in the resume. The more references a resume includes to those words or concepts, the higher it will score. <BR><BR>LINKME''s technology score differs in so far as it is not a measure of overlap. It is a prediction of the probability that this job could be the next job on the candidate''s resume. <BR><BR>LINKME renders that prediction based on a combination of a matching of concepts, as generalized based on correlations from a large number of real resumes, and on our analysis of the actual employment behavior patterns displayed across millions of actual career transitions. This last point is particularly key: across thousands or millions of actual career transitions observed, what career paths landed people this kind of job in the past and then, importantly, how does this candidate''s career path compare with theirs? <BR><BR>This means that LINKME is able to deliver results that are far more intuitively relevant because LINKME gives the highest scores to those who most closely resemble the candidates who have been placed into this kind of job in the past.</LI>')
END

GO