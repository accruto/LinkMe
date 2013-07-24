insert into resourcearticle
values ('96EFF759-5E0F-4CBD-8481-395827FB6A6A',
'C25792C1-4098-443A-8775-EB689154DB30',
'Job Interview First Impressions',
'Are first impressions really that important? The answer is... YES!

When the decision comes to hire a new employee, the candidates that are chosen almost always will share similar educational backgrounds, skills and experience. Because of this, it can be the small things that make the difference between getting the job or not. A lousy handshake or dirty shoes can be all it takes to lose out on getting the job.

Top tips to make sure that your first impression counts!

<b>Dressing appropriately</b>
A person who looks professional portrays the image of being professional. A person who looks sloppy portrays an image of being sloppy. If two people walk into an office and candidate one is perfectly dressed with clean shoes a shirt tucked in and brushed hair, and candidate 2 walks in looking like they just woke up, it goes without saying which candidate will more likely get the job. Before even discussing their skills, the hiring manager''s first impression about professionalism has already been made.

<b>Hygiene</b>
As a hiring manager, I can tell you there is nothing more off putting than interviewing a candidate with bad hygiene. No matter what job you are applying for, bad breath or lack of hygiene is not going to help you to get ahead. There is a fine balance between wearing the right amount of perfume/aftershave or too much. If the interviewer can smell your perfume from across the table you are probably wearing too much!

<b>Addressing the interviewer properly</b>
Showing respect toward the interviewer is paramount. Remember the interviewer is not your best friend who you have known for many years. Using slang or shortening their name (“Wassup Dave”) is not the correct way to make a good first impression.

<b>Listening</b>
A great mistake you can make in the interview is to speak too much and not answer questions. Interviewing is a 2-way process. Not only do you need to directly answer the interview questions, but you need to listen to what the interviewer is saying. Unless it is a direct yes or no answer, always provide examples and evidence to support what you are saying. Make sure you leave the interviewer with no doubts that you are the right person for the job.

<b>Handshake and Smile</b>
When you walk into the interview, a solid handshake and smile will go a long way to building rapport with the interviewer and will also leave a positive memory in their minds after the interview has finished. Typically the person who is interviewing you will often be your boss, and therefore they will want to know that not only do you have the skills required to do the job, but that they are going to want to work with you on a daily basis.',
0,
getdate(),
null)
go

insert into resourcearticle
values ('E6093E28-4CEF-4147-8594-31E22BF9272C',
'22374AB3-7AEB-4C4B-95EE-EC89C3DC9944',
'Asking For a Raise? Try to Avoid These Red Flags',
'There comes a point in every worker’s career when he or she feels deserving of a pay raise. If you’re like most people, it will probably be on you to ask for one – a raise won’t just be granted automatically. What you need to do is give your employer some concrete reasons for why you deserve one, and try to make it hard for them to say no. One thing to keep in mind, however, is that no matter how deserving you feel of one, employers will be reluctant to give you one if you possess one of the following traits:
<b>You routinely arrive to work late</b>
There is no better way to kill your chance at a raise. No matter how much you may excel at your job, it won’t matter if you are habitually late to work, even by a couple of minutes.
<b>You’ve had some issues with co-workers</b>
Someone once described a constant disagreement between two co-workers as a “cancer in the workplace” because it spreads and eventually affects everyone else in the office. You don’t need to be best friends with everyone at work, but you definitely should avoid personal disputes with your colleagues. Problems like these will label you as a liability rather than an asset – no boss wants to deal with workers like this.
<b>You take a lot of sick days</b>
Sure, everyone gets sick sometimes, but have you ever noticed the people who always seem to be out of the office for one reason or another? Chances are that you’re not the only one who’s noticed. Employees like this are seen as unreliable and as ones who routinely take advantage of the company. This is definitely a label you don’t want on your back – especially if you’re about to ask for a raise.
<b>Your boss asks you to do things more than once – repeatedly</b>
It’s not the end of the world when you forget to do something at work, but it does become a problem with this is somewhat of a habit. No boss likes to ask for things twice, especially on a regular basis.',
0,
getdate(),
null)
go

update resourcearticle
set text = '<p>'+replace(cast(text as varchar(8000)), char(13) + char(10),'</p>'+char(13) + char(10)+'<p>') +'</p>'
where id = '96EFF759-5E0F-4CBD-8481-395827FB6A6A'
or id = 'E6093E28-4CEF-4147-8594-31E22BF9272C'
go

update resourcearticle
set shortUrl = 'http://bit.ly/KelH51'
where id = '96EFF759-5E0F-4CBD-8481-395827FB6A6A'
go

update resourcearticle
set shortUrl = 'http://bit.ly/P8dd3o'
where id = 'E6093E28-4CEF-4147-8594-31E22BF9272C'
go
