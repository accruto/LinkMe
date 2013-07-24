using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Linq;
using LinkMe.Apps.Agents.Domain.Roles.Affiliations.Communities;
using LinkMe.Domain.Roles.Affiliations.Verticals;
using LinkMe.Domain.Roles.Affiliations.Verticals.Commands;
using LinkMe.Environment;
using LinkMe.Framework.Content;
using LinkMe.Framework.Content.ContentItems;

namespace LinkMe.Apps.Agents.Test.Verticals
{
    public enum TestVertical
    {
        Default,
        Autopeople,
        NewZealand,
    }

    public class VerticalTestData
    {
        private readonly IDictionary<string, string> _properties = new Dictionary<string, string>();

        public Guid? Id { get; set; }
        public string Name { get; set; }

        public string Url
        {
            get { return GetValue("Url", null); }
        }

        public string Host
        {
            get { return GetValue("Host", null); }
        }

        public string SecondaryHost
        {
            get { return GetValue("SecondaryHost", null); }
        }

        public string TertiaryHost
        {
            get { return GetValue("TertiaryHost", null); }
        }

        public int? CountryId
        {
            get { return GetIntValue("CountryId", null); }
        }

        public bool RequiresExternalLogin
        {
            get { return GetBoolValue("RequiresExternalLogin", false); }
        }

        public string ExternalLoginUrl
        {
            get { return GetValue("ExternalLoginUrl", null); }
        }

        public string ExternalCookieDomain
        {
            get { return GetValue("ExternalCookieDomain", null); }
        }

        public string EmailDisplayName
        {
            get { return GetValue("EmailDisplayName", null); }
        }

        public string ReturnEmailAddress
        {
            get { return GetValue("ReturnEmailAddress", null); }
        }

        public string MemberServicesEmailAddress
        {
            get { return GetValue("MemberServicesEmailAddress", null); }
        }

        public string EmployerServicesEmailAddress
        {
            get { return GetValue("EmployerServicesEmailAddress", null); }
        }

        public string Header
        {
            get { return GetValue("Header", null); }
        }

        public string Footer
        {
            get { return GetValue("Footer", null); }
        }

        public string HomePageTitle
        {
            get { return GetValue("HomePageTitle", null); }
        }

        public string ImageRootFolder
        {
            get { return GetValue("ImageRootFolder", null); }
        }

        public string CandidateImageRelativePath
        {
            get { return GetValue("CandidateImageRelativePath", null); }
        }

        public string FaviconRelativePath
        {
            get { return GetValue("FaviconRelativePath", null); }
        }

        public string HeaderSnippet
        {
            get { return GetValue("HeaderSnippet", null); }
        }

        public string ActivationEmailSnippet
        {
            get { return GetValue("ActivationEmailSnippet", null); }
        }

        public string CandidateImageUrl
        {
            get
            {
                if (ImageRootFolder == null || CandidateImageRelativePath == null)
                    return null;

                if (ImageRootFolder.EndsWith("/"))
                    return ImageRootFolder + CandidateImageRelativePath;
                return ImageRootFolder + "/" + CandidateImageRelativePath;
            }
        }

        protected string GetValue(string name, string defaultValue)
        {
            string value;
            return _properties.TryGetValue(name, out value) ? value : defaultValue;
        }

        protected int? GetIntValue(string name, int? defaultValue)
        {
            string value;
            return _properties.TryGetValue(name, out value) ? int.Parse(value) : defaultValue;
        }

        protected bool GetBoolValue(string name, bool defaultValue)
        {
            string value;
            return _properties.TryGetValue(name, out value) ? bool.Parse(value) : defaultValue;
        }

        internal void Add(string name, string value)
        {
            _properties[name] = value;
        }
    }

    public static class VerticalsTestExtensions
    {
        private static readonly IDictionary<string, VerticalTestData> Datas = new Dictionary<string, VerticalTestData>();
        private static readonly object DataLock = new object();

        public static Vertical CreateTestVertical(this TestVertical vertical, IVerticalsCommand verticalsCommand, IContentEngine contentEngine)
        {
            return vertical.GetVerticalTestData().CreateTestVertical(verticalsCommand, contentEngine);
        }

        public static Vertical CreateTestVertical(this VerticalTestData data, IVerticalsCommand verticalsCommand, IContentEngine contentEngine)
        {
            if (data.Id != null)
            {
                var vertical = new Vertical
                {
                    Id = data.Id.Value,
                    Name = data.Name,
                    Url = data.Url,
                    Host = data.Host,
                    SecondaryHost = data.SecondaryHost,
                    TertiaryHost = data.TertiaryHost,
                    CountryId = data.CountryId,
                    RequiresExternalLogin = data.RequiresExternalLogin,
                    ExternalLoginUrl = data.ExternalLoginUrl,
                    ExternalCookieDomain = data.ExternalCookieDomain,
                    EmailDisplayName = data.EmailDisplayName,
                    ReturnEmailAddress = data.ReturnEmailAddress,
                    MemberServicesEmailAddress = data.MemberServicesEmailAddress,
                    EmployerServicesEmailAddress = data.EmployerServicesEmailAddress
                };
                verticalsCommand.CreateVertical(vertical);

                if (contentEngine != null)
                    contentEngine.CreateContent(vertical.Id, data.Header, data.Footer, data.HomePageTitle, data.ImageRootFolder, data.CandidateImageRelativePath, data.FaviconRelativePath);
                return vertical;
            }

            // Default, so just create the content.

            if (contentEngine != null)
                contentEngine.CreateContent(null, data.Header, data.Footer, data.HomePageTitle, data.ImageRootFolder, data.CandidateImageRelativePath, data.FaviconRelativePath);
            return null;
        }

        private static void CreateContent(this IContentEngine contentEngine, Guid? verticalId, string headerHtml, string footerHtml, string homePageTitle, string imageRootFolder, string candidateImageRelativePath, string faviconRelativePath)
        {
            // Create some content.

            if (!string.IsNullOrEmpty(headerHtml))
            {
                contentEngine.CreateContentItem(new CommunityHeaderContentItem
                {
                    Name = "Page header",
                    VerticalId = verticalId,
                    Content = new HtmlContentItem { Text = headerHtml },
                });
            }

            if (!string.IsNullOrEmpty(footerHtml))
            {
                contentEngine.CreateContentItem(new CommunityFooterContentItem
                {
                    Name = "Page footer",
                    VerticalId = verticalId,
                    Content = new HtmlContentItem { Text = footerHtml },
                });
            }

            if (!string.IsNullOrEmpty(homePageTitle))
                AddTextContent(contentEngine, verticalId, "Home page title", homePageTitle);

            if (!string.IsNullOrEmpty(imageRootFolder) && candidateImageRelativePath != null)
                AddImageContent(contentEngine, verticalId, "Candidate logo", imageRootFolder, candidateImageRelativePath);

            if (!string.IsNullOrEmpty(imageRootFolder) && faviconRelativePath != null)
                AddImageContent(contentEngine, verticalId, "Favicon", imageRootFolder, faviconRelativePath);
        }

        public static void AddHtmlContent(this IContentEngine contentEngine, Vertical vertical, string name, string text)
        {
            contentEngine.CreateContentItem(new HtmlContentItem
                                                {
                                                    Name = name,
                                                    VerticalId = vertical == null ? (Guid?)null : vertical.Id,
                                                    Text = text
                                                });
        }

        public static void AddSectionContent(this IContentEngine contentEngine, Vertical vertical, bool enabled, string sectionName, string sectionTitle, string sectionContent)
        {
            var contentItem = new SectionContentItem
                                  {
                                      Name = sectionName,
                                      IsEnabled = enabled,
                                      VerticalId = vertical == null ? (Guid?)null : vertical.Id,
                                      SectionTitle = sectionTitle,
                                      SectionContent = new HtmlContentItem
                                                           {
                                                               Text = sectionContent
                                                           }
                                  };
            contentEngine.CreateContentItem(contentItem);
        }

        public static void DisableSectionContent(this IContentEngine contentEngine, Vertical vertical, string sectionName)
        {
            var contentItem = contentEngine.GetContentItem<SectionContentItem>(sectionName, vertical == null ? (Guid?)null : vertical.Id, true);
            contentEngine.DisableContentItem(contentItem.Id);
        }

        public static void UpdateSectionContent(this IContentEngine contentEngine, Vertical vertical, bool enabled, string sectionName, string sectionTitle, string sectionContent)
        {
            var contentItem = contentEngine.GetContentItem<SectionContentItem>(sectionName, vertical == null ? (Guid?)null : vertical.Id, true);

            contentItem.IsEnabled = enabled;
            contentItem.SectionTitle = sectionTitle;
            contentItem.SectionContent = new HtmlContentItem { Text = sectionContent };

            contentEngine.UpdateContentItem(contentItem);
        }

        private static void AddTextContent(this IContentEngine contentEngine, Guid? verticalId, string name, string text)
        {
            contentEngine.CreateContentItem(new TextContentItem
                                                {
                                                    Name = name,
                                                    VerticalId = verticalId,
                                                    Text = text
                                                });
        }

        private static void AddImageContent(this IContentEngine contentEngine, Guid? verticalId, string name, string rootFolder, string relativePath)
        {
            contentEngine.CreateContentItem(new ImageContentItem
                                                {
                                                    Name = name,
                                                    VerticalId = verticalId,
                                                    RootFolder = rootFolder,
                                                    RelativePath = relativePath,
                                                });
        }

        public static VerticalTestData GetVerticalTestData(this TestVertical vertical)
        {
            var name = vertical.ToString();
            VerticalTestData data;

            lock (DataLock)
            {
                if (Datas.TryGetValue(name, out data))
                    return data;
            }

            // Not found so load it.

            data = LoadData<VerticalTestData>(name);
            if (data == null)
                return null;

            lock (DataLock)
            {
                if (!Datas.ContainsKey(name))
                    Datas[name] = data;
            }

            return data;
        }

        public static T LoadData<T>(string name)
            where T : VerticalTestData, new()
        {
            var data = new T();
            var folder = Path.Combine(Path.Combine(RuntimeEnvironment.GetSourceFolder(), @"Test\Data\Verticals"), name);
            var dataFile = Path.Combine(folder, "VerticalData.xml");
            LoadData(dataFile, data);
            return data;
        }

        private static void LoadData(string dataFile, VerticalTestData data)
        {
            // Load the data file.

            var element = XElement.Load(dataFile);
            data.Id = GetGuidValue(element.Element("Id"));
            data.Name = GetValue(element.Element("Name"));

            foreach (var childElement in element.Elements())
            {
                if (childElement.Name != "Id" && childElement.Name != "Name")
                    data.Add(childElement.Name.LocalName, childElement.Value);
            }

            // Load other files.

            foreach (var file in Directory.GetFiles(Path.GetDirectoryName(dataFile)))
            {
                var fileName = Path.GetFileNameWithoutExtension(file);
                if (fileName != "VerticalData")
                    data.Add(fileName, File.ReadAllText(file));
            }
        }

        private static string GetValue(XElement element)
        {
            return element.Value;
        }

        private static Guid? GetGuidValue(XElement element)
        {
            if (element == null)
                return null;
            return new Guid(element.Value);
        }
    }
}