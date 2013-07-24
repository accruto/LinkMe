using System.Collections;
using System.IO;
using LinkMe.Environment;
using LinkMe.Framework.Utility;

namespace LinkMe.Domain.Users.Test.Employers.JobAds
{
    internal static class Words
    {
        private static readonly string WordListFolder = FileSystem.GetAbsolutePath("Test\\Data\\Dictionaries", RuntimeEnvironment.GetSourceFolder());

        public static readonly string[] Keywords = ReadWords("CRL-words.txt");
        public static readonly string[] GivenNames = ReadWords("Given-Names.txt");
        public static readonly string[] FamilyNames = ReadWords("Family-Names.txt");

        private static int _averageLength;

        public static int AverageLength
        {
            get
            {
                if (_averageLength == 0)
                {
                    var totalLength = 0;
                    foreach (var word in Keywords)
                        totalLength += word.Length;

                    _averageLength = totalLength / Keywords.Length;
                }

                return _averageLength;
            }
        }

        private static string[] ReadWords(string fileName)
        {
            string filePath = Path.Combine(WordListFolder, fileName);
            if (!File.Exists(filePath))
                throw new FileNotFoundException("The dictionary file '" + fileName + "' does not exist.", filePath);

            var list = new ArrayList();

            // Assume each word is on a separate line.

            using (var reader = new StreamReader(filePath))
            {
                string line = reader.ReadLine();
                while (line != null)
                {
                    line = line.Trim();

                    if (line.Length > 0)
                        list.Add(line);

                    line = reader.ReadLine();
                }
            }

            return (string[])list.ToArray(typeof(string));
        }
    }
}