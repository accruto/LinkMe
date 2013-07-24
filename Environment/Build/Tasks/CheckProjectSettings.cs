using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using Microsoft.Build.Framework;
using Microsoft.Build.Utilities;

namespace LinkMe.Environment.Build.Tasks
{
    public class CheckProjectSettings
        : Task
    {
        private const string _msBuildPrefix = "ms";
        private const string _msBuildNamespace = "http://schemas.microsoft.com/developer/msbuild/2003";
        private const string _prefix = "LinkMe.";

        private string _rootPath;
        private ITaskItem[] _projects;

        [Required]
        public string RootPath
        {
            get { return _rootPath; }
            set { _rootPath = value; }
        }

        [Required]
        public ITaskItem[] Projects
        {
            get { return _projects; }
            set { _projects = value; }
        }

        public override bool Execute()
        {
            try
            {
                foreach (var projectFile in GetProjectFiles())
                {
                    // Load each project file.

                    var xmlDocument = new XmlDocument();
                    var xmlNsMgr = new XmlNamespaceManager(xmlDocument.NameTable);
                    xmlNsMgr.AddNamespace(_msBuildPrefix, _msBuildNamespace);
                    xmlDocument.Load(projectFile);

                    CheckReferences(projectFile, xmlDocument, xmlNsMgr);
                }

                return true;
            }
            catch (Exception e)
            {
                Log.LogErrorFromException(e);
                return false;
            }
        }

        private static void CheckReferences(string projectFile, XmlDocument xmlDocument, XmlNamespaceManager xmlNsMgr)
        {
            foreach (XmlNode referenceNode in xmlDocument.SelectNodes("//" + _msBuildPrefix + ":Reference", xmlNsMgr))
            {
                var include = referenceNode.Attributes["Include"].Value;
                if (include.StartsWith(_prefix))
                {
                    // Make sure that the value only includes the file name.

                    if (include.IndexOf(",") != -1)
                        throw new ApplicationException("The '" + include + "' reference in the '" + projectFile + "' is not a simple name, but rather includes more information like Version, Culture etc.");

                    // Private should be True.

                    CheckElement(referenceNode, xmlNsMgr, projectFile, include, "Private", "True");
                }
            }
        }

        private static void CheckElement(XmlNode xmlNode, XmlNamespaceManager xmlNsMgr, string projectFile, string name, string elementName, string elementValue)
        {
            var element = xmlNode.SelectSingleNode(_msBuildPrefix + ":" + elementName, xmlNsMgr);
            if (element == null)
                throw new ApplicationException("The '" + name + "' node in the '" + projectFile + "' project does not have a " + elementName + " element.");

            if (element.InnerText != elementValue)
                throw new ApplicationException("The '" + name + "' node in the '" + projectFile + "' project has a " + name + " element that is set to '" + element.InnerText + "' when it should be set to '" + elementValue + "'.");
        }

        private IList<string> GetProjectFiles()
        {
            var projectFolder = Path.GetDirectoryName(_rootPath);

            var projectFiles = new List<string>();
            if (_projects != null)
            {
                foreach (var project in _projects)
                {
                    var path = FilePath.GetAbsolutePath(project.ItemSpec, projectFolder);
                    switch (Path.GetExtension(path))
                    {
                        case ".csproj":
                            projectFiles.Add(path);
                            break;

                        case ".sln":
                            GetProjectFiles(projectFiles, path);
                            break;
                    }
                }
            }

            return projectFiles;
        }

        private static void GetProjectFiles(IList<string> projectFiles, string solutionFilePath)
        {
            var solutionFolder = Path.GetDirectoryName(solutionFilePath);

            // Open up the solution file.

            using (var reader = new StreamReader(solutionFilePath))
            {
                var line = reader.ReadLine();
                while (line != null)
                {
                    line = line.Trim();
                    if (line.StartsWith("Project(\""))
                    {
                        var start = line.IndexOf(", \"");
                        if (start != -1)
                        {
                            start += ", \"".Length;
                            var end = line.IndexOf("\", \"", start + ", \"".Length);
                            if (end != -1)
                            {
                                var project = line.Substring(start, end - start);
                                var path = FilePath.GetAbsolutePath(project, solutionFolder);
                                switch (Path.GetExtension(path))
                                {
                                    case ".csproj":
                                        projectFiles.Add(path);
                                        break;
                                }
                            }
                        }
                    }

                    line = reader.ReadLine();
                }
            }
        }
    }
}