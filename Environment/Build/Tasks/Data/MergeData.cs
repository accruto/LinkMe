using System;
using System.IO;
using System.Text;
using Microsoft.Build.Framework;
using Microsoft.Build.Utilities;

namespace LinkMe.Environment.Build.Tasks.Data
{
    public sealed class MergeData
        : Task
    {
        private ITaskItem[] m_mergeItems;
        private readonly MergeDataOptions m_options;

        public MergeData()
        {
            m_options = new MergeDataOptions();
        }

        private MergeDataOptions Options
        {
            get { return m_options; }
        }

        #region Task

        public override bool Execute()
        {
            try
            {
                // OutputFolder should be absolute and exist.

                m_options.OutputFolder = FilePath.GetAbsolutePath(m_options.OutputFolder, Path.GetDirectoryName(m_options.ProjectFullPath));
                if (!Directory.Exists(m_options.OutputFolder))
                    Directory.CreateDirectory(m_options.OutputFolder);

                // Simply iterate.

                foreach (ITaskItem item in m_mergeItems)
                    Merge(item);
                return true;
            }
            catch (Exception e)
            {
                Log.LogErrorFromException(e);
                return false;
            }
        }

        #endregion

        #region Properties

        [Required]
        public string ProjectFullPath
        {
            get { return Options.ProjectFullPath; }
            set { Options.ProjectFullPath = value; }
        }

        [Required]
        public string OutputPath
        {
            get { return Options.OutputFolder; }
            set { Options.OutputFolder = value; }
        }

        public string Configuration
        {
            get { return Options.Configuration; }
            set { Options.Configuration = value; }
        }

        public ITaskItem[] Merges
        {
            get { return m_mergeItems; }
            set { m_mergeItems = value; }
        }

        #endregion

        private void Merge(ITaskItem item)
        {
            string sourceFullPath = FilePath.GetAbsolutePath(item.ItemSpec, Path.GetDirectoryName(Options.ProjectFullPath));
            string sourceFolder = Path.GetDirectoryName(sourceFullPath);

            // Open the source file.

            using (StreamReader reader = new StreamReader(sourceFullPath))
            {
                StringBuilder dropText = new StringBuilder();
                StringBuilder createText = new StringBuilder();

                // Iterate through each file in the source file.

                string file = reader.ReadLine();
                while (file != null)
                {
                    file = file.Trim();
                    if (file != string.Empty)
                    {
                        string path = FilePath.GetAbsolutePath(file, sourceFolder);
                        Merge(path, dropText, createText);
                    }

                    file = reader.ReadLine();
                }

                // Write out the results.

                string destinationFullPathStem = FilePath.GetAbsolutePath(Path.GetFileNameWithoutExtension(item.ItemSpec), Options.OutputFolder);
                string dropFullPath = destinationFullPathStem + ".Drop.sql";
                using (StreamWriter writer = new StreamWriter(dropFullPath))
                {
                    writer.Write(dropText);
                }

                string createFullPath = destinationFullPathStem + ".Create.sql";
                using (StreamWriter writer = new StreamWriter(createFullPath))
                {
                    writer.Write(createText);
                }
            }

            Log.LogMessage(MessageImportance.High, "'{0}' has been merged.", sourceFullPath);
        }

        private static void Merge(string path, StringBuilder dropText, StringBuilder createText)
        {
            // Open the file to read.

            using (StreamReader fileReader = new StreamReader(path))
            {
                bool inDrop = false;
                bool inCreate = false;

                StringBuilder fileDropText = new StringBuilder();
                fileDropText.AppendLine("--===============================================================================");
                fileDropText.Append("-- ").AppendLine(Path.GetFileName(path));
                fileDropText.AppendLine("--===============================================================================");
                fileDropText.AppendLine();

                StringBuilder fileCreateText = new StringBuilder();
                fileCreateText.AppendLine("--===============================================================================");
                fileCreateText.Append("-- ").AppendLine(Path.GetFileName(path));
                fileCreateText.AppendLine("--===============================================================================");
                fileCreateText.AppendLine();

                string previousLine = string.Empty;
                string line = fileReader.ReadLine();
                while (line != null)
                {
                    if (inDrop)
                    {
                        if (line != "-- Create")
                        {
                            fileDropText.AppendLine(previousLine);
                        }
                        else
                        {
                            fileCreateText.AppendLine(previousLine);
                            inDrop = false;
                            inCreate = true;
                        }
                    }
                    else if (inCreate)
                    {
                        fileCreateText.AppendLine(previousLine);
                    }
                    else
                    {
                        if (line == "-- Drop")
                        {
                            fileDropText.AppendLine(previousLine);
                            inDrop = true;
                        }
                        else
                        {
                            // Write to both.

                            fileDropText.AppendLine(previousLine);
                            fileCreateText.AppendLine(previousLine);
                        }
                    }

                    previousLine = line;
                    line = fileReader.ReadLine();
                }

                fileCreateText.AppendLine(previousLine);

                dropText.Insert(0, fileDropText);
                createText.Append(fileCreateText);
            }
        }
    }
}