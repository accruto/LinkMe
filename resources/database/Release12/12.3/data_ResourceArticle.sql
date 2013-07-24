insert into resourcearticle
values ('070E4632-0113-4EAA-813D-B665CBDC6530',
'68C6D880-73B2-44C2-BE89-4D07C85E190F',
'Five Myths About Job Searching',
'#1: The smartest person always gets the job
Definitely not true – companies these days are more interested in the complete worker. Having brains is always an advantage, but it''s not the only thing that hiring managers are looking for. In today’s economy, an employer wants to know that, if required, you are able to complete a wide range of jobs. Having transferrable skills, a can do attitude and a willingness to learn and be involved in all aspects of the business is key to nailing the job interview.
#2: Direct experience is most important
Transferable skills are key. In certain industries the job specification may require direct experience, but in many circumstances being able to show that you have the skills to succeed is just as important. Do not get discouraged if you feel that you lack the right skills to get a new job. Concentrate on the value added skills that you have and highlight these skills on your resume and in the job interview.
#3: Dating a co-worker will lead to career doom
An urban myth. I have even heard of stories where dating the boss has resulted in career success (not recommended!). Always remember to perform your role to the highest quality and it does not matter who you decide to date! (Note – public displays of affection are a big no no! – this type of behaviour is best saved for non-work hours).
#4: Applying for jobs online is the only way to find a new job
Job searching online is one of many different approaches you should take. Before you even begin to apply for jobs, ensure that you have a professionally written resume. No matter how many jobs you apply for, it doesn''t matter if your resume is not selling your skills. With the growth of social networking online, sites such as LinkedIn can be a fantastic way to approach people who you typically could not just pick up the phone and call.
#5: Writing a cover letter is a waste of time
Every time you apply for a job you should accompany your resume with a targeted cover letter. The only exception is when the job specification clearly states not to send a cover letter. Most times a hiring manager will read your cover letter before opening your resume. If your cover letter does not shine, there is a good chance your resume won’t even be opened. You may have the greatest resume written by a professional resume writer, but it means nothing if your cover letter is letting you down.',
0,
getdate(),
null)
go

update resourcearticle
set text = '<p>'+replace(cast(text as varchar(8000)), char(13) + char(10),'</p>'+char(13) + char(10)+'<p>') +'</p>'
where id = '070E4632-0113-4EAA-813D-B665CBDC6530'
go

update resourcearticle
set shortUrl = 'http://bit.ly/IyFf9l'
where id = '070E4632-0113-4EAA-813D-B665CBDC6530'
go
