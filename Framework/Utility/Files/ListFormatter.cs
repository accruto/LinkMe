using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;

namespace LinkMe.Framework.Utility.Files
{
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property)]
    public class FileItemOrderAttribute
        : Attribute
    {
        private readonly int _order;

        public FileItemOrderAttribute(int order)
        {
            _order = order;
        }

        public int Order
        {
            get { return _order; }
        }
    }

	public interface IFileFormatProvider
	{
		string FormatHeaderItem(FileItem item, int rowNumber, int totalRows);
		string FormatItem(FileItem item, int rowNumber, int totalRows);
		string GetLineBreak();
	}

    public class FileItem
        : IComparable
    {
        private readonly int _order;
        private readonly string _value;

        public FileItem(int order, string value)
        {
            _order = order;
            _value = value;
        }

        public int Order
        {
            get { return _order; }
        }

        public string Value
        {
            get { return _value; }
        }

        int IComparable.CompareTo(object obj)
        {
            if (obj == null || !(obj is FileItem))
                throw new ArgumentException("Can only compare to another OrderWrapper");

            var item = (FileItem)obj;
            return Order.CompareTo(item.Order);
        }
    }

    /// <summary>
    /// Could there not be some way with a serializer that this could be done?
    /// </summary>
    public class FileFormatter
    {
        private readonly IFileFormatProvider _formatProvider;

        public FileFormatter(IFileFormatProvider formatProvider)
        {
            _formatProvider = formatProvider;
        }

        public void Format<T>(IList<T> list, Stream stream)
            where T : class
        {
            var writer = new StreamWriter(stream);

            var headerRow = GetRow<T>(null);
            WriteRowToStream(headerRow, writer, RowType.Header);
            writer.Write(_formatProvider.GetLineBreak());

            for (var index = 0; index < list.Count; index++)
            {
                var row = GetRow(list[index]);
                WriteRowToStream(row, writer, RowType.Item);
                if (index < list.Count - 1)
                    writer.Write(_formatProvider.GetLineBreak());
            }

            writer.Flush();
        }

        private void WriteRowToStream(IList<FileItem> row, TextWriter writer, RowType rowType)
        {
            for (var index = 0; index < row.Count; index++)
            {
                var item = row[index];
                if (rowType == RowType.Item)
                    writer.Write(_formatProvider.FormatItem(item, index, row.Count));
                else if (rowType == RowType.Header)
                    writer.Write(_formatProvider.FormatHeaderItem(item, index, row.Count));
            }
        }

        private static IList<FileItem> GetRow<T>(T instance)
            where T : class
        {
            var members = typeof(T).GetMembers(BindingFlags.Public | BindingFlags.Instance);
            var items = new List<FileItem>();

            foreach (var member in members)
            {
                if (member.MemberType == MemberTypes.Property)
                {
                    var value = instance != null ? GetMemberValue(member, instance) : GetMemberName(member);

                    var attributes = member.GetCustomAttributes(typeof (FileItemOrderAttribute), true) as FileItemOrderAttribute[];
                    if (attributes != null && attributes.Length > 0)
                    {
                        var attribute = attributes[0];
                        items.Add(new FileItem(attribute.Order, value));
                    }
                }
            }

            items.Sort();
            return items;
        }

        private static string GetMemberName(MemberInfo member)
        {
            return member.Name;
        }

        private static string GetMemberValue(MemberInfo member, object instance)
        {
            if (member.MemberType == MemberTypes.Property)
            {
                var property = (PropertyInfo)member;
                var value = property.GetValue(instance, new object[0]);
                return value != null ? value.ToString() : string.Empty;
            }

            if (member.MemberType == MemberTypes.Field)
            {
                var field = (FieldInfo)member;
                var value = field.GetValue(instance);
                return value != null ? value.ToString() : string.Empty;
            }

            return string.Empty;
        }

        public enum RowType
        {
            Header,
            Item
        }
    }
}