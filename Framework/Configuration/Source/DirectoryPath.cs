using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Xml;

using LinkMe.Framework.Type;
using LinkMe.Framework.Utility;
using LinkMe.Framework.Utility.Exceptions;
using LinkMe.Framework.Utility.Xml;

namespace LinkMe.Framework.Configuration
{
    [System.Serializable]
    public sealed class DirectoryPath
        : System.ICloneable,
            System.IComparable,
            IInternable,
            IBinarySerializable
    {
        public DirectoryPath(string path)
        {
            const string method = ".ctor";

            if (path == null)
                throw new NullParameterException(typeof(DirectoryPath), method, "path");

            // Use a capturing regex to extract the parent and name.

            EnsureRegexes();

            Match match = m_capturingPathRegex.Match(path);
            if (!match.Success)
                throw new InvalidParameterFormatException(typeof(DirectoryPath), method, "reference", path, Constants.Validation.CompleteQualifiedReferencePattern);

            m_name = match.Groups["Name"].Value;
            m_parent = match.Groups["Parent"].Value;
        }

        public DirectoryPath(string parent, string name)
        {
            const string method = ".ctor";

            // Check parent.

            if (parent == null)
                parent = string.Empty;
            else if (parent.Length != 0 && !IsPath(parent))
                throw new InvalidParameterFormatException(typeof(DirectoryPath), method, "parent", parent, Constants.Validation.CompletePathPattern);

            // Check name.

            if (name == null)
                throw new NullParameterException(typeof(DirectoryPath), method, "name");
            if (!IsName(name))
                throw new InvalidParameterFormatException(typeof(DirectoryPath), method, "name", name, Constants.Validation.CompleteNamePattern);

            // Assign.

            m_parent = parent;
            m_name = name;
        }

        /// <summary>
        /// Private constructor for cloning - performs no checks.
        /// </summary>
        private DirectoryPath(bool ignore, string name, string parent)
        {
            m_parent = parent;
            m_name = name;
        }

        #region ICloneable Members

        public DirectoryPath Clone()
        {
            return new DirectoryPath(false, m_name, m_parent);
        }

        object System.ICloneable.Clone()
        {
            return Clone();
        }

        #endregion

        #region IComparable Members

        public int CompareTo(DirectoryPath other)
        {
            int result = m_parent.CompareTo(other.m_parent);
            if (result != 0)
                return result;
            return m_name.CompareTo(other.m_name);
        }

        int System.IComparable.CompareTo(object other)
        {
            const string method = "CompareTo";
            if (!(other is DirectoryPath))
                throw new InvalidParameterTypeException(typeof(DirectoryPath), method, "other", typeof(DirectoryPath), other);
            return CompareTo((DirectoryPath)other);
        }

        #endregion

        #region IInternable Members

        void IInternable.Intern(Interner interner)
        {
            m_name = interner.Intern(m_name);
            m_parent = interner.Intern(m_parent);
        }

        #endregion

        #region IBinarySerializable Members

        public void Write(BinaryWriter writer)
        {
            writer.Write(m_parent);
            writer.Write(m_name);
        }

        public void Read(BinaryReader reader)
        {
            m_parent = reader.ReadString();
            m_name = reader.ReadString();
        }

        #endregion

        #region System.Object Members

        public override string ToString()
        {
            return Path;
        }

        public override bool Equals(object other)
        {
            DirectoryPath otherName = other as DirectoryPath;
            if (otherName == null)
                return false;

            return m_parent == otherName.m_parent
                && m_name == otherName.m_name;
        }

        public static bool operator ==(DirectoryPath name1, DirectoryPath name2)
        {
            return object.Equals(name1, name2);
        }

        public static bool operator !=(DirectoryPath name1, DirectoryPath name2)
        {
            return !object.Equals(name1, name2);
        }

        public override int GetHashCode()
        {
            return m_parent.GetHashCode()
                ^ m_name.GetHashCode();
        }

        #endregion

        #region Properties

        public string Parent
        {
            get { return m_parent; }
            set
            {
                const string method = "set_Parent";

                if (value == null)
                    value = string.Empty;
                else if (value.Length != 0 && !IsPath(value))
                    throw new InvalidParameterFormatException(typeof(DirectoryPath), method, "value", value, Constants.Validation.CompletePathPattern);

                m_parent = value;
            }
        }

        public string Name
        {
            get { return m_name; }
            set
            {
                const string method = "set_Name";

                if (value == null)
                    throw new NullParameterException(typeof(DirectoryPath), method, "name");
                if (!IsName(value))
                    throw new InvalidParameterFormatException(typeof(DirectoryPath), method, "name", value, Constants.Validation.CompleteNamePattern);

                m_name = value;
            }
        }

        public string Path
        {
            get { return GetPathUnchecked(m_parent, m_name); }
        }

        #endregion

        public static string Combine(string path, string relativePath)
        {
            const string method = "Combine";

            // Check.

            if (path == null)
                throw new NullParameterException(typeof(DirectoryPath), method, "path");
            if (!IsPath(path))
                throw new InvalidParameterFormatException(typeof(DirectoryPath), method, "path", path, Constants.Validation.CompletePathPattern);

            if (relativePath == null)
                throw new NullParameterException(typeof(DirectoryPath), method, "relativePath");
            if (!IsRelativePath(relativePath))
                throw new InvalidParameterFormatException(typeof(DirectoryPath), method, "relativePath", relativePath, Constants.Validation.CompletePathPattern);

            if (path.EndsWith("/"))
                return path + relativePath;
            else
                return path + "/" + relativePath;
        }

        public static void SplitRootPath(string path, out string rootPath, out string relativePath)
        {
            const string method = "SplitRootPath";

            if (path == null)
                throw new NullParameterException(typeof(DirectoryPath), method, "path");
            if (!IsPath(path))
                throw new InvalidParameterFormatException(typeof(DirectoryPath), method, "path", path, Constants.Validation.CompletePathPattern);

            int pos = path.IndexOf('/', 1);
            if (pos == -1)
            {
                rootPath = path;
                relativePath = string.Empty;
            }
            else
            {
                rootPath = path.Substring(0, pos);
                relativePath = path.Substring(pos + 1);
            }
        }

        public static void SplitRootRelativePath(string path, out string rootPath, out string relativePath)
        {
            const string method = "SplitRootRelativePath";

            if (path == null)
                throw new NullParameterException(typeof(DirectoryPath), method, "path");
            if (!IsRelativePath(path))
                throw new InvalidParameterFormatException(typeof(DirectoryPath), method, "path", path, Constants.Validation.CompleteRelativePathPattern);

            int pos = path.IndexOf('/');
            if (pos == -1)
            {
                rootPath = path;
                relativePath = string.Empty;
            }
            else
            {
                rootPath = path.Substring(0, pos);
                relativePath = path.Substring(pos + 1);
            }
        }

        /// <summary>
        /// Create an instance of the class from a fully qualified reference without checking that it is valid.
        /// </summary>
        /// <remarks>This method does not validate the input values. You can use it to improve performance when
        /// the values are known to be valid.</remarks>
        public static DirectoryPath CreateUnchecked(string parent, string name)
        {
            Debug.Assert(parent != null && name != null, "parent != null && name != null");
            return new DirectoryPath(false, name, parent);
        }

        public static string GetPathUnchecked(string parent, string name)
        {
            return (parent.Length == 0 ? name : parent + "/" + name);
        }

        public static string CreatePathName(string name, System.Version version)
        {
            const string method = "CreatePathName";
            if (name == null)
                throw new NullParameterException(typeof(DirectoryPath), method, "name");
            if (!IsName(name))
                throw new InvalidParameterFormatException(typeof(DirectoryPath), method, "name", name, Constants.Validation.CompleteNamePattern);

            return version == null ? name : name + "-" + version.ToString().Replace(".", "-");
        }

        #region Validation static methods

        // IsName() is by far the most commonly called validation method, so it's implemented directly, which
        // is about 10 times faster than the equivalent regular expression.
        public static bool IsName(string text)
        {
            const string method = "IsName";

            if (text == null)
                throw new NullParameterException(typeof(DirectoryPath), method, "text");

            if (text.Length == 0)
                return false;

            char first = text[0];
            if (!((first >= 'A' && first <= 'Z') || (first >= 'a' && first <= 'z')))
                return false;

            for (int index = 1; index < text.Length; index++)
            {
                char c = text[index];

                if (!((c >= 'A' && c <= 'Z') || (c >= 'a' && c <= 'z') || (c >= '0' && c <= '9') || c == '_' || c == '-'))
                    return false;
            }

            return true;
        }

        public static bool IsPath(string text)
        {
            const string method = "IsPath";

            if (text == null)
                throw new NullParameterException(typeof(DirectoryPath), method, "text");

            EnsureRegexes();
            return m_pathRegex.IsMatch(text);
        }

        public static bool IsRelativePath(string text)
        {
            const string method = "IsRelativePath";

            if (text == null)
                throw new NullParameterException(typeof(DirectoryPath), method, "text");

            EnsureRegexes();
            return m_relativePathRegex.IsMatch(text);
        }

        #endregion

        private static void EnsureRegexes()
        {
            if (m_pathRegex == null)
            {
                lock (typeof(DirectoryPath))
                {
                    if (m_pathRegex == null)
                    {
                        m_pathRegex = new Regex(Constants.Validation.CompletePathPattern, RegexOptions.Compiled);
                        m_relativePathRegex = new Regex(Constants.Validation.CompleteRelativePathPattern, RegexOptions.Compiled);
                        m_capturingPathRegex = new Regex(Constants.Validation.CapturingPathPattern, RegexOptions.Compiled);
                    }
                }
            }
        }

        private static Regex m_pathRegex;
        private static Regex m_relativePathRegex;
        private static Regex m_capturingPathRegex;

        private string m_parent;
        private string m_name;
    }
}
