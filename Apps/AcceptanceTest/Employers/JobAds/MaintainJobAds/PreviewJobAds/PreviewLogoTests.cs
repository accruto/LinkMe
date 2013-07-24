using System;
using System.IO;
using LinkMe.Domain.Files;
using LinkMe.Domain.Files.Commands;
using LinkMe.Domain.Roles.JobAds;
using LinkMe.Environment;
using LinkMe.Framework.Utility;
using LinkMe.Framework.Utility.Urls;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.AcceptanceTest.Employers.JobAds.MaintainJobAds.PreviewJobAds
{
    [TestClass]
    public class PreviewLogoTests
        : PreviewJobAdTests
    {
        private readonly IFilesCommand _filesCommand = Resolve<IFilesCommand>();

        [TestMethod]
        public void TestNoLogo()
        {
            var employer = CreateEmployer(null);
            var jobAd = CreateJobAd(employer, JobAdStatus.Draft);

            LogIn(employer);
            Get(GetPreviewUrl(jobAd.Id));

            AssertNoLogo();
        }

        [TestMethod]
        public void TestLogo()
        {
            var employer = CreateEmployer(null);
            var jobAd = CreateJobAd(employer, JobAdStatus.Draft);
            AddLogo(jobAd);

            LogIn(employer);
            Get(GetPreviewUrl(jobAd.Id));

            Assert.IsNotNull(jobAd.LogoId);
            AssertLogo(jobAd.LogoId.Value);
        }

        private void AddLogo(JobAd jobAd)
        {
            var filePath = FileSystem.GetAbsolutePath(@"Test\Data\Photos\ProfilePhoto.jpg", RuntimeEnvironment.GetSourceFolder());

            using (var stream = new MemoryStream())
            {
                using (var writer = new BinaryWriter(stream))
                {
                    // Load the contents from the file.

                    using (var reader = new BinaryReader(File.OpenRead(filePath)))
                    {
                        var count = 4096;
                        var buffer = new byte[count];
                        while ((count = reader.Read(buffer, 0, count)) > 0)
                            writer.Write(buffer, 0, count);
                    }

                    stream.Position = 0;
                    var fileReference = _filesCommand.SaveFile(FileType.CompanyLogo, new StreamFileContents(stream), Path.GetFileName(filePath));

                    jobAd.LogoId = fileReference.Id;
                    _jobAdsCommand.UpdateJobAd(jobAd);
                }
            }
        }

        private void AssertNoLogo()
        {
            Assert.IsNull(GetLogoUrl());
        }

        private void AssertLogo(Guid fileId)
        {
            var logoUrl = GetLogoUrl();
            Assert.IsNotNull(logoUrl);
            Assert.AreEqual(logoUrl.Path.ToLower(), GetLogoUrl(fileId).Path.ToLower());
        }

        private static ReadOnlyUrl GetLogoUrl(Guid fileId)
        {
            return new ReadOnlyApplicationUrl(true, "~/employers/jobads/logos/" + fileId);
        }

        private ReadOnlyUrl GetLogoUrl()
        {
            var start = Browser.CurrentPageText.IndexOf("logoUrl: '", StringComparison.Ordinal);
            if (start == -1)
                return null;

            var end = Browser.CurrentPageText.IndexOf("'", start + "logoUrl: '".Length, StringComparison.Ordinal);
            if (end == -1)
                return null;

            var url = Browser.CurrentPageText.Substring(start + "logoUrl: '".Length, end - start - "logoUrl: '".Length);
            if (string.IsNullOrEmpty(url))
                return null;

            return new ReadOnlyApplicationUrl(url);
        }
    }
}
