using System.IO;
using System.Text;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using LinkMe.Environment.Build.Tasks.Assemble;

namespace LinkMe.Environment.Build.Tasks
{
    public class RegistryFile
    {
        public RegistryFile()
        {
            m_keys = new Dictionary<string, RegistryCaptureKey>();
        }

        public void Load(string fullPath)
        {
            using ( StreamReader reader = new StreamReader(fullPath) )
            {
                // Read the first line to determine the format.

                string line = reader.ReadLine();
                if ( line != "REGEDIT4" && line != "Windows Registry Editor Version 5.00" )
                    throw new System.ApplicationException("The first line of the registry file is '" + line + "', but the expected first line is 'REGEDIT4' or 'Windows Registry Editor Version 5.00'.");

                // REGEDIT version 4 saves expandable strings using ANSI encoding, but version 5 uses Unicode encoding.

                bool unicode = line == "Windows Registry Editor Version 5.00";
                RegistryCaptureKey currentKey = null;

                line = reader.ReadLine();

                for ( ; ; )
                {
                    if ( line == null )
                        break;

                    Match match;
                    if ( (match = m_keyRegex.Match(line)).Success )
                    {
                        currentKey = LoadKey(match);
                        if ( currentKey != null )
                            m_keys.Add(currentKey.Path, currentKey);
                        line = reader.ReadLine();
                    }
                    else if ( (match = m_defaultValueRegex.Match(line)).Success )
                    {
                        RegistryCaptureValue value = LoadDefaultValue(match);
                        if ( currentKey != null && value != null )
                            currentKey.Add(value);
                        line = reader.ReadLine();
                    }
                    else if ( (match = m_valueRegex.Match(line)).Success )
                    {
                        RegistryCaptureValue value = LoadValue(match, unicode, reader, ref line);
                        if ( currentKey != null && value != null )
                            currentKey.Add(value);
                    }
                    else
                    {
                        line = reader.ReadLine();
                    }
                }
            }
        }

        public ICollection<RegistryCaptureKey> Keys
        {
            get { return m_keys.Values; }
        }

        private RegistryCaptureKey LoadKey(Match match)
        {
            string root = match.Groups["Root"].Value;
            string rootRelativePath = match.Groups["RootRelativePath"].Value;

            switch ( root )
            {
                case "HKEY_CLASSES_ROOT":
                    root = Constants.Registry.Key.ClassesRoot;
                    break;

                case "HKEY_CURRENT_USER":
                    root = Constants.Registry.Key.CurrentUser;
                    break;

                case "HKEY_USERS":
                    root = Constants.Registry.Key.Users;
                    break;

                case "HKEY_LOCAL_MACHINE":
                    root = Constants.Registry.Key.LocalMachine;
                    break;

                default:
                    root = string.Empty;
                    break;
            }

            return new RegistryCaptureKey(root + "\\" + rootRelativePath);
        }

        private RegistryCaptureValue LoadDefaultValue(Match match)
        {
            return new RegistryCaptureStringValue(string.Empty, match.Groups["Value"].Value);
        }

        private RegistryCaptureValue LoadValue(Match match, bool unicode, StreamReader reader, ref string line)
        {
            RegistryCaptureValue registryValue = null;
            string name = match.Groups["Name"].Value;
            string value = match.Groups["Value"].Value;

            if ( (match = m_stringValueRegex.Match(value)).Success )
            {
                // Unescape.

                value = Regex.Replace(match.Groups["Value"].Value, @"\\(.)", "$1");
                registryValue = new RegistryCaptureStringValue(name, value);
            }
            else if ( (match = m_dwordValueRegex.Match(value)).Success )
            {
                int dwordValue = System.Convert.ToInt32(match.Groups["Value"].Value, 16);
                registryValue = new RegistryCaptureDWordValue(name, dwordValue);
            }
            else if ( (match = m_binaryValueRegex.Match(value)).Success )
            {
                byte[] bytes = null;
                GetBytes(match, ref bytes, reader, ref line);
                registryValue = new RegistryCaptureBinaryValue(name, bytes);
            }
            else if ( (match = m_expandableValueRegex.Match(value)).Success )
            {
                byte[] bytes = null;
                GetBytes(match, ref bytes, reader, ref line);
                Encoding encoding = unicode ? Encoding.Unicode : Encoding.ASCII;
                registryValue = new RegistryCaptureExpandStringValue(name, encoding.GetString(bytes));
            }
            else if ( (match = m_multiStringValueRegex.Match(value)).Success )
            {
                byte[] bytes = null;
                GetBytes(match, ref bytes, reader, ref line);
                Encoding encoding = unicode ? Encoding.Unicode : Encoding.ASCII;
                string[] strings = encoding.GetString(bytes).Split('\0');

                // Remove any empty strings from the end.

                LinkedList<string> stringList = new LinkedList<string>(strings);
                LinkedListNode<string> node = stringList.Last;
                while ( node != null )
                {
                    if ( !string.IsNullOrEmpty(node.Value) )
                        break;
                    stringList.Remove(node);
                    node = stringList.Last;
                }

                strings = new string[stringList.Count];
                stringList.CopyTo(strings, 0);

                registryValue = new RegistryCaptureMultiStringValue(name, strings);
            }

            line = reader.ReadLine();
            return registryValue;
        }

        private void GetBytes(Match match, ref byte[] currentBytes, StreamReader reader, ref string line)
        {
            // Check for the line continuation.

            byte[] lineBytes = GetBytes(match);
            currentBytes = Combine(currentBytes, lineBytes);
            if ( match.Groups["LineContinues"].Length == 0 )
                return;

            // Need to read the next line.

            line = reader.ReadLine();
            if ( !(match = m_binaryLineRegex.Match(line)).Success )
                return;

            GetBytes(match, ref currentBytes, reader, ref line);
        }

        private byte[] GetBytes(Match match)
        {
            int count = match.Groups["Byte"].Captures.Count;
            byte[] bytes = new byte[count];
            for ( int index = 0; index < count; ++index )
                bytes[index] = System.Convert.ToByte(System.Convert.ToInt32(match.Groups["Byte"].Captures[index].Value, 16));
            return bytes;
        }

        private byte[] Combine(byte[] a1, byte[] a2)
        {
            if ( a1 == null )
                return a2;
            if ( a2 == null )
                return a1;

            byte[] newBytes = new byte[a1.Length + a2.Length];
            a1.CopyTo(newBytes, 0);
            a2.CopyTo(newBytes, a1.Length);
            return newBytes;
        }

        private Dictionary<string, RegistryCaptureKey> m_keys;

        private static readonly Regex m_keyRegex = new Regex(@"^\[(?<Root>[a-zA-Z0-9_]+)\\(?<RootRelativePath>[a-zA-Z0-9_\\]+)\]$");
        private static readonly Regex m_defaultValueRegex = new Regex("^@=\"(?<Value>.*)\"$");
        private static readonly Regex m_valueRegex = new Regex("^\"(?<Name>[a-zA-Z0-9_]+)\"=(?<Value>.*)$");
        private static readonly Regex m_stringValueRegex = new Regex("^\"(?<Value>.*)\"$");
        private static readonly Regex m_dwordValueRegex = new Regex("^dword:(?<Value>.*)$");
        private static readonly Regex m_binaryValueRegex = new Regex("^hex:((?<Byte>[0-9A-Fa-f][0-9A-Fa-f]),?)*(?<LineContinues>\\\\?)$");
        private static readonly Regex m_expandableValueRegex = new Regex("^hex\\(2\\):((?<Byte>[0-9A-Fa-f][0-9A-Fa-f]),?)*(?<LineContinues>\\\\?)$");
        private static readonly Regex m_multiStringValueRegex = new Regex("^hex\\(7\\):((?<Byte>[0-9A-Fa-f][0-9A-Fa-f]),?)*(?<LineContinues>\\\\?)$");
        private static readonly Regex m_binaryLineRegex = new Regex("^ *((?<Byte>[0-9A-Fa-f][0-9A-Fa-f]),?)*(?<LineContinues>.?)$");
    }
}