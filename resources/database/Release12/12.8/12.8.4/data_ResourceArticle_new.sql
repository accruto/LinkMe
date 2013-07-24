DECLARE @id UNIQUEIDENTIFIER
SET @id = 'CD6273CC-B200-459C-9D23-468FFF9A597A'

DELETE
	dbo.ResourceArticle
WHERE
	id = @id

INSERT
	dbo.ResourceArticle (id, resourceSubcategoryId, title, text, rating, createdtime, shortUrl)
VALUES
	(
		@id,
		'b173ef4b-f65d-4b16-ac01-e0d45d707061',
		'How to stand out during the job interview',
		'<p>Making the right impression in your job interview is one of the most important steps in your future career. Many people don&#39;t know this, but job interviews require careful planning and research, otherwise you will significantly reduce the chance of getting the job. In the current economic climate it is very hard to land yourself a job, thus planning your interview before is the key to success</p><p>The first (and often the most important) thing to prepare for is your attire. Don&#39;t wait until the last minute to find something to wear – you should prepare for it days in advance and get it properly cleaned and ironed. Remember that you should always dress to fit the context of a job. If you&#39;re applying for a casual gardening company, a suit might not be ideal, but if you&#39;re applying for a position as an accountant or a banker, then a full suit would be the required minimum. Despite what people say, first impressions are everything.</p><p>Conducting research about the job and company you&#39;re applying for is imperative. Consider doing a web search and learn as many facts about the company as you can. You can subtlety include these facts during the interview (when appropriate) to show the interviewer that you have done your homework on the company. Don&#39;t overdo it though – you don&#39;t want to sound like you&#39;re repeating their whole website!</p><p>Non-verbal messages are often more important than words, so make sure that you greet your interviewer with a firm handshake. Maintaining correct posture and eye contact are also two very important non-verbal messages as they make you appear more confident and presentable. These are two big qualities that hiring managers will be looking for.</p><p>You should also be able to recite your resume off by heart. As a general rule, you should not need to consult your resume. The interviewer will already have a copy of your resume, and they will ask you questions about it, so don&#39;t try to make up an answer as there is a big chance that you will get it wrong. Highlight your achievements and the value added skills you can bring to the job and back up your statements with examples.</p><p>As preparation is the key to a successful job interview, consider doing a short role-play with a friend or family member. Ask them to question you on your resume and the job to fully prepare you for the interview. The more you are prepared the greater your chances of success. Following the interview, thank the interviewer for his or her time and ask when they expect to make a final decision and don&#39;t feel discouraged to follow up with them if you haven&#39;t heard back within a few days.</p><p>Good luck!</p>',
		0,
		'13 Oct 2012',
		null
	)

UPDATE
	dbo.ResourceFeaturedArticle
SET
	resourceArticleId = @id
WHERE
	id = '8064B10A-A60B-47C6-B367-AD4C00674036'



SET @id = '{DDF35B92-8269-4D10-AB12-0FDF2EEBA710}'

DELETE
	dbo.ResourceArticle
WHERE
	id = @id

INSERT
	dbo.ResourceArticle (id, resourceSubcategoryId, title, text, rating, createdtime, shortUrl)
VALUES
	(
		@id,
		'8d8b4804-b358-440f-b364-197e39b952e3',
		'Making the transition from graduate to job seeker',
		'<p>There is a certain amount of relief when a student finally graduates from college; the years of hard work through all levels of school have finally paid off and they now stand, degree in hand, with their future laid out before them. That relief tends to be short lived when the reality strikes home that student loans now have to be paid and it&#39;s time to get a real job! Trying to land that first big job is always a daunting task, but perhaps even more so, at the moment given the tight job market. Companies are now offering fewer graduate positions and with literally hundreds of candidates applying for the same role, the interview process is even more important. With that in mind, here are some tips that can help you when making the transition from school to work.</p><p></b>Research and prepare:</b></p><p>If you have graduated from college with great grades, then that means that you have spent a great deal of time studying, which is a trait that you should carry over to your interview process. Take time to do homework on the company you are interviewing with and find a way to naturally weave that knowledge into your answers. Your pre-planning shouldn&#39;t only be limited to the actual interview, but also how to get there. Showing up late will put an immediate strike against your name and potentially eliminate you from the interview process. Plan the route you need to take to get to the location and don&#39;t be shy in giving yourself an extra 10 minutes to get there.</p><p><b>Practice makes perfect:</b></p><p>Job Interviewing can be a daunting experience which is why it&#39;s a good idea to do a few mock interviews before the big day arrives. You can ask a friend or family member to conduct the “interview”, but make sure that it is someone who is subjective and who isn&#39;t afraid to tell you that your answers were not that strong. A great tip is to write down 10 examples of achievements or skills that you would like to portray to the interviewer and integrating these examples into your answers. The worst mistake is going to the interview without preparation and stuttering your way through the interview.</p><p><b>Creating a professional image:</b></p><p>Most people are aware that they have to dress the part when going for an interview, but that extends beyond the clothing. Make sure that your hair is neat and tidy, and that you are well groomed; having a hairstyle that looks like you just stepped out of bed will quickly negate the fantastic suit you are wearing. As a recent graduate, portraying a professional image is vital to your success. Remember that a hiring manager will make an immediate impression about you the second you walk in the door. A positive first impression is vital to your success.</p><p>Being prepared, arriving on time, and looking the part is only a small part of the process. You have a limited amount of time to impress the interviewer, so use that to sell yourself as best as you can, without coming across as arrogant or pushy. Confidence is as important as your education, and it may just end up being the deciding factor between you and another candidate.</p>',
		0,
		'14 Oct 2012',
		null
	)

UPDATE
	dbo.ResourceFeaturedArticle
SET
	resourceArticleId = @id
WHERE
	id = 'F1834627-10A9-4288-ADEE-D8F55997DA01'
