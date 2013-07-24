using System;
using System.Linq;
using LinkMe.Domain.Resources;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.Domain.Test.Resources
{
    [TestClass]
    public class VideosTests
        : ResourcesTests
    {
        private const string Category1 = "Job interviewing";
        private const string Subcategory1 = "How to ace the interview";
        private const string Title1 = "Interview tips #2";
        private const string Text1 = @"<p>I think the second thing, that I find is really important, is &#39;Ok, you&#39;re a recruitement consultant, but what are you going to do for me?&#39;, &#39;Why are you different?&#39; and &#39;What are you going to do for my organisation that other people are not going to do?&#39;.</p>  <p>So, think about yourself. Think about your strengths and talk to those things about why you are different, and what you are going to do for me.</p>  <p>A lot of people don&#39;t realise they&#39;ve got to have a reason to buy. An employer is the buyer, the candidate (in an interview situation) is the seller; although in very clever organisations they sell themselves to the candidate as well.</p>  <p>But &#39;Why should I buy?&#39; should be another perspective, looking at it from the candidate&#39;s point of view.</p>";
        private const string ShortUrl1 = "http://bit.ly/AbXpLF";
        private const string ExternalVideoId1 = "ul2fTRiAGoI";

        private const string Category2 = "Career management";
        private const string Subcategory2 = "Manage your career";
        private const string Title2 = "Changing your career";
        private const string Text2 = @"<p>Well, I think WHY people want to change is a really important question.</p>  <p>Because a lot of people work at things that they actually don&#39;t love. They just sort of ... they live from Friday night until Sunday night, and then they work from Monday morning until Friday night.</p>  <p></p>  <p>And one of the things that impresses me about people these days is, firstly, you don&#39;t actually have to have a degree in the area you want to work in. You can study what you like, what a passion is, and you can work at something else.</p>  <p>Conversely, you can study at something that you think gives you a really good foundation. You might do Economics, but you might want to be in Human Resources or you might want to be in Real Estate Sales, or something totally different.</p>  <p>So, these days, I think it&#39;s important to try and learn, for a foundation for your work, and realise that the first qualification might only be the basis and you might go on to two or three different qualifications, and the same goes for jobs.</p>  <p>So, the people I see, who are best at doing what they do at work, are the people who are passionately and emotionally involved in what they doing. I mean, you&#39;re in the IT area, and people who are in IT - they speak a different language. You know, they talk code to each other and no-one else can understand what they&#39;re talking about. If they&#39;re in the public service, they can talk about a whole range of things by using SD4 and KR1 and no-one else knows what they&#39;re talking about, because they&#39;re so involved emotionally, they love what they&#39;re doing. [And] I think that&#39;s the best job you can have, ever, is one you&#39;re just busting to get out of bed, because you&#39;ve been thinking about your job, really excited by it and motivated by it, those people don&#39;t get tired by going to work. They get pumped by going to work.</p>  <p>So, if I think you are looking to change your career, totally, change to something that you really love.</p>  <p>And change to something that you feel you&#39;d be really good at.</p>";
        private const string ShortUrl2 = "http://bit.ly/xgCVjR";
        private const string ExternalVideoId2 = "-kWensZKYig";

        [TestMethod]
        public void TestVideos()
        {
            var categories = _resourcesQuery.GetCategories();
            var videos = (from v in _resourcesQuery.GetVideos() orderby v.CreatedTime select v).ToList();
            Assert.AreEqual(16, videos.Count);

            var video1 = videos[12];
            var subcategory1 = categories.Single(c => c.Name == Category1).Subcategories.Single(s => s.Name == Subcategory1);
            AssertVideo(Title1, Text1, subcategory1.Id, ShortUrl1, ExternalVideoId1, video1);
            AssertVideo(Title1, Text1, subcategory1.Id, ShortUrl1, ExternalVideoId1, _resourcesQuery.GetVideo(video1.Id));

            var video2 = videos[15];
            var subcategory2 = categories.Single(c => c.Name == Category2).Subcategories.Single(s => s.Name == Subcategory2);
            AssertVideo(Title2, Text2, subcategory2.Id, ShortUrl2, ExternalVideoId2, video2);
            AssertVideo(Title2, Text2, subcategory2.Id, ShortUrl2, ExternalVideoId2, _resourcesQuery.GetVideo(video2.Id));

            videos = _resourcesQuery.GetVideos(new[] { video1.Id, video2.Id }).ToList();
            Assert.AreEqual(2, videos.Count);
            AssertVideo(Title1, Text1, subcategory1.Id, ShortUrl1, ExternalVideoId1, (from a in videos where a.Id == video1.Id select a).Single());
            AssertVideo(Title2, Text2, subcategory2.Id, ShortUrl2, ExternalVideoId2, (from a in videos where a.Id == video2.Id select a).Single());
        }

        private static void AssertVideo(string title, string text, Guid subcategoryId, string shortUrl, string externalVideoId, Video video)
        {
            Assert.AreEqual(title, video.Title);
            Assert.AreEqual(text, video.Text);
            Assert.AreEqual(subcategoryId, video.SubcategoryId);
            Assert.AreEqual(shortUrl, video.ShortUrl);
            Assert.AreEqual(externalVideoId, video.ExternalVideoId);
        }
    }
}
