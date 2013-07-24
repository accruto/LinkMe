insert into resourcearticle
values ('3628DE0E-EC30-4E6B-BEC1-7F668CE9B0D9',
'22DC249E-C0CD-45B3-A194-B8EA967559F4',
'How to handle the telephone interview (and reach the face-to-face interview stage)',
'So you’ve found an ad for your dream job and submitted a thorough and thoughtful application. What’s next?
If your application makes it through the screening round, the process of securing the job is likely to involve a series of interviews, initially on the telephone, followed by a number of in-person, face-to-face meetings. Many people underestimate the importance of the initial telephone conversation: the recruiter’s goal is to determine your suitability for the role, so if you don’t make a great first impression, you’re unlikely to proceed to the next round of interviews.
Most of the time, you’ll receive a phone call from the advertiser (this could be a Recruitment Consultant or someone from the company’s HR/Recruitment team). There’s usually no warning of the call, so be prepared to shift into interview mode quickly. If you happen to miss the call, it is common courtesy to return the call promptly (which is also likely to help your application).
While the phone interview is relatively informal, this is still an interview. A few points to consider:
1.	Be proactive. You could consider contacting the advertiser proactively – either from the details in the advertisement or through your own research into the company. This leaves no doubt about how keen you are about the role. Not all advertisers encourage this approach, particularly for roles which are likely to attract a large volume of applicants. Be prepared to be told to apply online and don’t be overly pushy if this is the case.
2.	Don’t rush. You won’t be judged for taking the time to consider the question and answer it properly. Stay calm, composed and think your answers through. If you’ve reached this stage, it means the recruiter genuinely wants to understand who you are and discover why you’re suitable for the role. This means that even if you have a lot to say, the recruiter is unlikely to hang up on you and you don’t need to worry that you’re wasting their time.
3.	Be direct in your answers. Being cagey or not giving the full answer doesn’t help your cause. Remember that you are competing with other candidates and will likely to be asked the same questions as they are. Listen carefully for clues about whether your answer is on the right track. For example, if the recruiter needs more detail or is confused about your response, she may try to ask the same question in a different way.
4.	Listen. As the saying goes: “You have two ears and one mouth. Listen twice more than you speak.”

The conclusion of the call will usually be close when the questions end, and either a description of the role or being asked if you have any questions comes up. Simply enquiring about the next stage or a couple of questions about the role itself (team size, how this position fits into the team, etc.) will also be a good way for the interviewer to determine how keen you are and serve to leave them with a good impression. Both of which are key in hopefully securing your first stage interview.',
0,
getdate(),
null)
go

update resourcearticle
set text = '<p>'+replace(cast(text as varchar(8000)), char(13) + char(10),'</p>'+char(13) + char(10)+'<p>') +'</p>'
where id = '3628DE0E-EC30-4E6B-BEC1-7F668CE9B0D9'
go

update resourcearticle
set shortUrl = 'http://bit.ly/N5jT2f'
where id = '3628DE0E-EC30-4E6B-BEC1-7F668CE9B0D9'
go
