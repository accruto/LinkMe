using System;
using System.Linq;
using LinkMe.Domain.Resources;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.Domain.Test.Resources
{
    [TestClass]
    public class ArticlesTests
        : ResourcesTests
    {
        private const string Category1 = "Job search";
        private const string Subcategory1 = "Job seeking tips";
        private const string Title1 = "Switching up your job seeking tactics";
        private const string Text1 = "<p>Many times, job applications go entirely unnoticed. A friend of mine recently tried something new because she realized she wasn&#39;t getting any interview requests. Like most people, she would search through popular websites and apply for jobs straight away, always including a personalized cover letter and resume. Then she&#39;d wait and &quot;hope for the best&quot;. And nothing happened. It was at this point that she started re-applying for the same positions. And guess what? She received a couple of invitations to interview.</p>  <p>This is not the first time I&#39;ve heard of this happening. In this age of emailed applications and very little personal contact, it&#39;s hard to figure out why the interview requests are not coming. Of course, this has a lot to do with the quality of your resume and cover letter, but sometimes it can also be just because of plain bad luck. In some cases, your application could have just gone unnoticed through no fault of your own. Below are some of the reasons for this:</p>  <p></p>  <p><b>Bad timing</b></p>  <p>You could have simply applied at the wrong time, literally. A hiring manager could have opened your email while in the middle of something and then simply forgotten about it a minute later. It does happen.</p>  <p></p>  <p><b>Your application was accidentally deleted</b></p>  <p>Do you ever quickly go through your email list and hold your finger down on the delete button? You&#39;re not the only one. If a hiring manager is posting advertisements on job websites, the probability of their receiving spam emails is quite high. They could have accidentally deleted your application.</p>  <p></p>  <p><b>Too many applications</b></p>  <p>A lot of times, hiring managers will have a stopping point for accepting applications. They either received too many or they&#39;ve already narrowed the list down to just a few candidates. Whatever the reason, they won&#39;t bother to delete the advertisement, and they might automatically delete your application. It is not uncommon, however, for them to become dissatisfied with their applicant pool. When this happens, they&#39;re not likely to review the same applicant pool from before - they&#39;ll focus on the new applicants.</p>  <p>My advice is to always try again because you never know what may happen. I am not suggesting that these scenarios happen all the time, but they do happen somewhat frequently, so give yourself the benefit of the doubt. </p>";
        private const string ShortUrl1 = "http://bit.ly/wQLTEE";

        private const string Category2 = "Resume writing";
        private const string Subcategory2 = "Resume writing tips";
        private const string Title2 = "10 resume writing questions to ask a professional resume writer";
        private const string Text2 = "<p>1)  What do human resources professionals / employers want in a resume?</p>  <p>The most important part of a resume is the same no matter if you are a student or recent graduate or a CEO or executive. Recruiters and employers are looking for achievements and the value added skills you can bring to the job. Too many times a candidate will focus on the duties and responsibilities. While this is important it will not make a candidate stand out from the competition. Emphasising achievements backed up with examples is the most important part of resume writing.</p>  <p></p>  <p>2) Can you give us examples about why some resumes are never read past the first sentence?</p>  <p>There is no set rule why a particular resume may be deleted however there are many factors that can contribute to a hiring manager pressing the delete button. What every candidate needs to remember is that for every job there is potentially another 100, 200 or even 300 other candidates applying for the job. Your resume is the first impression which a hiring manger will make about a particular candidate. Using fancy fonts, long sentences, bad spelling and grammar are just a few reasons why a resume will be deleted before the hiring manager has even completed the first sentences</p>  <p></p>  <p>3) How can you make your resume stand out?</p>  <p>Professionalism is the key and targeting the resume for the job you are applying for. Remember your resume has a purpose and that is to get you an interview. It is not a pierce of artwork that will be hung on the wall. If the job you are applying for requires leadership abilities than provide examples about ways you have performed as a leader. Do not make the reader have to guess!</p>  <p></p>  <p>4) What is the number one tip to create a resume that gets the interview?</p>  <p>Achievement based resume writing</p>  <p></p>  <p>5) In your experience, what are some of the mistakes that appear in resumes?</p>  <p>The top 5 resume mistakes I see on a daily basis are as follows:</p>  <p>&bull; The use of &quot;text Messaging&quot; abbreviations (&quot;Going 2 c clients&quot;)</p>  <p>&bull; No dates</p>  <p>&bull; Lack of achievements or highlights</p>  <p>&bull; Irrelevant information (long winded)</p>  <p>&bull; Spelling mistakes</p>  <p></p>  <p>6) What do you think about including a &quot;career objective&quot; statement in your resume?</p>  <p>Career objective or career summary when written well adds great value to your resume. However when written badly or &quot;generically&quot; can have a negative effect on the resume. In my opinion I like to include a career summary to introduce the candidate to the reader. It is however very important to include value added information in the career objective rather than generic information such as &quot;hard working individual who is very loyal and solve problems&quot;</p>  <p></p>  <p>7) Can you give us an example of how a candidate can address gaps in their work continuity?</p>  <p>Always one of the hardest aspects of writing a resume is dealing with working gaps. A cover letter goes hand in hand with a resume and a well constructed cover letter can explain to the reader why there is a gap. I recently worked with a senior executive who took 2 years off to travel and perform community work. We included the community work on his resume to show the reader that he had been actively doing something over that certain time period and then was able to explain in the cover letter that after working nonstop for 20 years he took a 2 year break from his professional career in order to perform the community work which he had not been able to do due to his work commitments.</p>  <p></p>  <p>8) Should you include your hobbies or interests in a resume?</p>  <p>If the hobbies and interest add value to the resume than I recommend including them. If not leave them out. (Remember to target everything on your resume to the position you are going for)</p>  <p></p>  <p>9) How long does a resume have to be?</p>  <p>Resume writing is not an exact science and there is no exact answer. A standard resume will be between 2-3 pages. This is also dependent on the stage of the career a candidate is currently at. For example a graduate or young professional will typically want to have a 2 page resume. A more senior candidate may need 3-4 pages to include all of their achievements and work history.</p>  <p></p>  <p>10) Do you need to include your whole employment history or should you list only the positions relevant to the role advertised?</p>  <p>This answer is different for every candidate. Obviously a student or graduate will have fewer positions to include than a senior executive and therefore although a certain job may not be relevant to the position which they are applying for it does show the reader that they have work experience. A senior executive can afford to be more targeted and include positions related to the role.</p>";
        private const string ShortUrl2 = "http://bit.ly/A3Gwjk";

        [TestMethod]
        public void TestArticles()
        {
            var categories = _resourcesQuery.GetCategories();
            var articles = (from a in _resourcesQuery.GetArticles() orderby a.CreatedTime select a).ToList();
            Assert.AreEqual(83, articles.Count);

            var article1 = articles[23];
            var subcategory1 = categories.Single(c => c.Name == Category1).Subcategories.Single(s => s.Name == Subcategory1);
            AssertArticle(Title1, Text1, subcategory1.Id, ShortUrl1, article1);
            AssertArticle(Title1, Text1, subcategory1.Id, ShortUrl1, _resourcesQuery.GetArticle(article1.Id));

            var article2 = articles[45];
            var subcategory2 = categories.Single(c => c.Name == Category2).Subcategories.Single(s => s.Name == Subcategory2);
            AssertArticle(Title2, Text2, subcategory2.Id, ShortUrl2, article2);
            AssertArticle(Title2, Text2, subcategory2.Id, ShortUrl2, _resourcesQuery.GetArticle(article2.Id));

            articles = _resourcesQuery.GetArticles(new[] { article1.Id, article2.Id }).ToList();
            Assert.AreEqual(2, articles.Count);
            AssertArticle(Title1, Text1, subcategory1.Id, ShortUrl1, (from a in articles where a.Id == article1.Id select a).Single());
            AssertArticle(Title2, Text2, subcategory2.Id, ShortUrl2, (from a in articles where a.Id == article2.Id select a).Single());
        }

        private static void AssertArticle(string title, string text, Guid subcategoryId, string shortUrl, Resource article)
        {
            Assert.AreEqual(title, article.Title);
            Assert.AreEqual(text, article.Text);
            Assert.AreEqual(subcategoryId, article.SubcategoryId);
            Assert.AreEqual(shortUrl, article.ShortUrl);
        }
    }
}
