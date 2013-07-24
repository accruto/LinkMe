namespace LinkMe.Framework.Utility.Files
{
    public enum Delimiter
    {
        Comma,
        Pipe
    }

    public class DelimitedFormatProvider
        : IFileFormatProvider
    {
        private readonly string _delimiter;

        public DelimitedFormatProvider(Delimiter delimiter)
        {
            _delimiter = delimiter == Delimiter.Pipe ? "|" : ",";
        }

        string IFileFormatProvider.FormatHeaderItem(FileItem item, int rowNumber, int totalRows)
        {
            return FormatItem(item, rowNumber, totalRows);
        }

        string IFileFormatProvider.FormatItem(FileItem item, int rowNumber, int totalRows)
        {
            return FormatItem(item, rowNumber, totalRows);
        }

        string IFileFormatProvider.GetLineBreak()
        {
            return System.Environment.NewLine;
        }

        private string FormatItem(FileItem item, int rowNumber, int totalRows)
        {
            return string.Format("\"{0}\"{1}", item.Value.Replace("\"", ""), (rowNumber < totalRows - 1 ? _delimiter : ""));
        }
    }
}