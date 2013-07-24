DECLARE @id UNIQUEIDENTIFIER
SET @id = '{2AD69B6D-7CD3-4EF3-B51C-B26BCC001001}'

DELETE
	dbo.ResourceAnsweredQuestion
WHERE
	id = @id

INSERT
	dbo.ResourceAnsweredQuestion (id, resourceSubcategoryId, title, text, createdTime, shortUrl)
VALUES
	(
		@id,
		'8f508e2c-c7bb-46fe-8bd6-aa812054dc03',
		'Should I have an objective statement on my resume? Are there other ways to make my resume stand out from the crowd?',
		'<p><b>Replacing the Objective Statement with a Qualifications Profile on your resume</b></p><p>Does your current resume begin with a generic objective statement telling the reader what type of jobs you are looking for? Let me guess that it reads something similar to this:</p><p><i>&quot;I am seeking the opportunity to expand my skills, knowledge and experience in a challenging professional environment. I am honest, reliable, eager to learn and open to tackling a range of tasks. I am a strong and empathetic team player and always complete tasks to a high degree of quality and to deadlines&quot;</i></p><p>If this is how your resume begins, it&#39;s time to make changes. In the competitive job environment where hiring managers may receive upwards of 500 applications for a single position, an objective statement is more likely going to lead to your resume being deleted.  From a hiring manager&#39;s perspective, they are not interested in a non-specific, all-purpose statement that adds no value to the resume and provides them with no reason to want to hire you. You may have the best skills and be the perfect fit for the job however, you may never get this opportunity because your resume has already been deleted.</p><p><b>What is a Qualifications Profile?</b></p><p>A great way to introduce yourself on your resume is by creating a qualifications summary or career summary.  Rather than telling the reader you are seeking an opportunity to expand your skills, rather promote what skills you actually can bring to this specific role. A targeted resume including a targeted profile will encourage the reader to continue reading the resume as opposed to pressing the delete button. For example, if you are applying for an IT job that requires programming skills, list you&#39;re programming skills within your introductory profile. That way, the reader will straight away be interested to read on as they know that you have skills that are required for this position.</p><p><b>How long should my Qualifications Profile be?</b></p><p>The last thing you want to do is turn your qualifications profile into an essay! Statistically, a hiring manager will only spend between 15 to 20 seconds when initially reading your resume. If they open your resume and see a half page profile they are more likely to be turned off as they won&#39;t be bothered to read all this information. A well written profile should be no longer than 2-4 sentences. It needs to be targeted and present value.</p><p><b>Final thought:</b></p><p>When you begin to write your new resume, don&#39;t forget the number one rule. Your resume is a marketing document. The more you can showcase your skills and achievements the greater chance you will have of being selected for the interview stage.</p>',
		'14 Oct 2012',
		NULL		
	)

UPDATE
	dbo.ResourceFeaturedArticle
SET
	resourceArticleId = @id
WHERE
	id = '877E72B3-0B55-4222-B94E-19BC744D957F'
