using LinkMe.Apps.Services.JobAds;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.Apps.Services.Test.JobAds
{
    [TestClass]
    public class JobAdExtensionsTests
        : TestClass
    {
        private const string RealJobAdText = @"This is the real job ad text. This is the real job ad text.
            This is the real job ad text. This is the real job ad text. This is the real job ad text. 
            This is the real job ad text. This is the real job ad text. This is the real job ad text. 
            This is the real job ad text. This is the real job ad text.";
        private const string ExtraMarquees1 = @"<center><span style=""color: red; font-size: 15px; font-family: Arial; text-decoration: blink;"">
            ---NOW RECRUITING---</span><br><marquee width=""100%"" direction=""left"" scrolldelay=""65""
            style=""font-family: Verdana; font-size: 15px; color: black;""><b>
            <br>...........www. Structural jobs.com.au......................Structural Drafting Manager
            $115-100K Post Review? Still not happy?.................
            www. Structural jobs.com.au.......................Structural Designer $75-68K Juicy Large
            Scale Projects................</b></marquee></center><br><br><p>";
        private const string ExtraMarquees2 = @"<marquee width=""100%"" direction=""left"" scrolldelay=""70"" style=""font-family: Verdana; font-size: 15px; color: black;"">Other Mechanical Design Jobs:<br>Principal Mechanical Design Engineer Minerals Processing $183-171<br>Principal Mechanical Design Engineer Materials Handling $185-175K</marquee><br><center><span style=""color: red; font-size: 15px; font-family: Arial; text-decoration: blink;"">---NOW RECRUITING---</span><br><marquee width=""100%"" direction=""left"" scrolldelay=""65"" style=""font-family: Verdana; font-size: 15px; color: black;""><b> ...........Principal Mechanical Engineer $175-155K.................Mechanical Design Engineer $108-92K Consultancy. Mining.......................www.Mechanicaljobs.com.au.................Commissioning Engineer $98-95K.................Mechanical Design Engineer $145-115K Mining ................. www.Engineerjobs.com.au.................Mechanical Engineer $138-112K Multi $Billion Mining Project ..................... Senior Project Engineer, Mechanical - Mining and Resources $155-125K...................Design Engineers - Mechanical $125-90K Mineral Processing Specialisation .....................www.Engineerjobs.com.au.................Lead Mechanical Project Engineer $175-150K..........................Principal Mechanical Engineer $167 - 148K Perth Based.................Principal Mechanical Engineer Perth $148 - 167K Strategy and Long Term Direction.................Senior Mechanical Design Engineer $127-108K.................Mechanical Design Engineer $108-92K Consultancy. Mining.................Mechanical Design Engineer $138-112K Multi $Billion Mining Project.................Mechanical Engineer $129-103K Got the Goods?.................Senior Mechanical Engineer $142-129K.................Principal Mechanical Engineer $186-179K.................Lead Mechanical Engineer $168-149K.................Mechanical Engineer $138-126K GO and get some.................Mechanical Design Engineer $115-95K Step to Senior..................Mechanical Design Engineer $110 -95K Materials Handling.................Senior Mechanical Maintenance Engineer $144-132K.................Mechanical Engineering Roles $180-85K Relocating?.................Senior Mechanical Engineer Power Generation $153-138K ...........Principal Mechanical Engineer $175-155K.................Mechanical Design Engineer $108-92K Consultancy. Mining.......................www.Mechanicaljobs.com.au.................Commissioning Engineer $98-95K.................Mechanical Design Engineer $145-115K Mining ................. www.Engineerjobs.com.au.................Mechanical Engineer $138-112K Multi $Billion Mining Project ..................... Senior Project Engineer, Mechanical - Mining and Resources $155-125K...................Design Engineers - Mechanical $125-90K Mineral Processing Specialisation .....................www.Engineerjobs.com.au.................Lead Mechanical Project Engineer $175-150K..........................Principal Mechanical Engineer $167 - 148K </b><p></p><b>	
		
		</b>";
        private const string ExtraMarquees3 = @"<marquee width=""100%"" direction=""left"" scrolldelay=""70"" style=""font-family: Verdana; font-size: 15px; color: black;"">Environmental Manager $192-167K + Bonuses…… Senior Environmental Engineer/Hydrogeologist $121-106K Water Resources…… Senior Environmental Engineer $128-116K…… Senior Contamination Consultant/Project Manager $118 - 108K…… Associate Environmental Project Manager $128 - 115K…… Senior Environmental Engineers $122 - 83K Perth offers Choice…… Principal Environmental Engineer $180-164K…… Principal Environmental &amp; Sustainability Consultant $127-114K…… Senior Contamination Consultant $118 - 108K…… Wastewater Engineer $105 – 192K…… Water Engineer $110 – 98K……</marquee><br><br>";
        private const string ExtraMarquees4 = @"<marquee width=""100%"" direction=""left"" scrolldelay=""70"" style=""font-family: Verdana; font-size: 15px; color: black;"">Other Mechanical Design Jobs:<br>Principal Mechanical Design Engineer Minerals Processing $183-171<br>Principal Mechanical Design Engineer Materials Handling $185-175K</marquee><br><center><span style=""color: red; font-size: 15px; font-family: Arial; text-decoration: blink;"">---NOW RECRUITING---</span><br><marquee width=""100%"" direction=""left"" scrolldelay=""65"" style=""font-family: Verdana; font-size: 15px; color: black;""><b> ...........Principal Mechanical Engineer $175-155K.................Mechanical Design Engineer $108-92K Consultancy. Mining.......................www.Mechanicaljobs.com.au.................Commissioning Engineer $98-95K.................Mechanical Design Engineer $145-115K Mining ................. www.Engineerjobs.com.au.................Mechanical Engineer $138-112K Multi $Billion Mining Project ..................... Senior Project Engineer, Mechanical - Mining and Resources $155-125K...................Design Engineers - Mechanical $125-90K Mineral Processing Specialisation .....................www.Engineerjobs.com.au.................Lead Mechanical Project Engineer $175-150K..........................Principal Mechanical Engineer $167 - 148K Perth Based.................Principal Mechanical Engineer Perth $148 - 167K Strategy and Long Term Direction.................Senior Mechanical Design Engineer $127-108K.................Mechanical Design Engineer $108-92K Consultancy. Mining.................Mechanical Design Engineer $138-112K Multi $Billion Mining Project.................Mechanical Engineer $129-103K Got the Goods?.................Senior Mechanical Engineer $142-129K.................Principal Mechanical Engineer $186-179K.................Lead Mechanical Engineer $168-149K.................Mechanical Engineer $138-126K GO and get some.................Mechanical Design Engineer $115-95K Step to Senior..................Mechanical Design Engineer $110 -95K Materials Handling.................Senior Mechanical Maintenance Engineer $144-132K.................Mechanical Engineering Roles $180-85K Relocating?.................Senior Mechanical Engineer Power Generation $153-138K ...........Principal Mechanical Engineer $175-155K.................Mechanical Design Engineer $108-92K Consultancy. Mining.......................www.Mechanicaljobs.com.au.................Commissioning Engineer $98-95K.................Mechanical Design Engineer $145-115K Mining ................. www.Engineerjobs.com.au.................Mechanical Engineer $138-112K Multi $Billion Mining Project ..................... Senior Project Engineer, Mechanical - Mining and Resources $155-125K...................Design Engineers - Mechanical $125-90K Mineral Processing Specialisation .....................www.Engineerjobs.com.au.................Lead Mechanical Project Engineer $175-150K..........................Principal Mechanical Engineer $167 - 148K </b><p></p>";
        private const string ExtraHtml = @"<font size=""80%"">Not the job for you? Here are some more options:<br><br></font><li><font size=""80%"">
            <b>Electrical Engineer $148-129K + site bonus- based in Sydney</b><br></font></li><li>
            <font size=""80%"">Electrical Engineer Mining & Power Gen Specialist - $152-137K. West Perth
            <br></font></li></li>";

        private const string IrrelevantStyleStart = "<span class='extraJobAdText'>";
        private const string IrrelevantStyleEnd = "</span>";

        [TestMethod]
        public void TestFixExtraText()
        {
            Assert.IsNull(((string)null).FixExtraTextAndTrim(false, false));
            Assert.AreEqual("", "".FixExtraTextAndTrim(false, false));

            TestStripExtraInternal(ExtraHtml, "", true);

            // EP 03/07/07: When the extra text is at the start it doesn't remove it perfectly - there are
            // some empty tags left. These could be stripped, too, but they're not causing any problems
            // at the moment, so I haven't got around to it.

            TestStripExtraInternal(ExtraMarquees1, "<br><br><p>", false);
            TestStripExtraInternal(ExtraMarquees2, "<p></p><b>\t\r\n\t\t\r\n\t\t</b>", false);
            TestStripExtraInternal(ExtraMarquees3, "<br><br>", false);
            TestStripExtraInternal(ExtraMarquees4, "<p></p>", false);
        }

        private static void TestStripExtraInternal(string extra, string expectedAtStart, bool testSetStyle)
        {
            // Try stripping the text.

            var stripped = (RealJobAdText + extra).FixExtraTextAndTrim(false, false);
            Assert.AreEqual(RealJobAdText, stripped);

            if (testSetStyle)
            {
                // Try enclosing it in a special style.

                var styled = (RealJobAdText + extra).FixExtraTextAndTrim(false, true);
                Assert.AreEqual(RealJobAdText + IrrelevantStyleStart + extra + IrrelevantStyleEnd, styled);
            }
            else
            {
                stripped = (extra + RealJobAdText).FixExtraTextAndTrim(false, false);
                Assert.AreEqual(expectedAtStart + RealJobAdText, stripped);
            }
        }
    }
}