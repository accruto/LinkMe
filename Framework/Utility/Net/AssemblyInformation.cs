using System.Globalization;
using System.IO;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;

namespace LinkMe.Framework.Utility.Net
{
	public class AssemblyInfo
		:	System.ICloneable,
			IInternable
	{
		public AssemblyInfo()
		{
		}

		#region IInternable Members

		void IInternable.Intern(Interner interner)
		{
			m_keyFile = interner.Intern(m_keyFile);
			m_location = interner.Intern(m_location);
		}

		#endregion

		public string FullName
		{
			get
			{
				 return m_name.FullName;
			}
			set
			{
				if (value.Length > 0)
				{
					if (m_fullNameRegex == null)
					{
						lock (this.GetType())
						{
							if (m_fullNameRegex == null)
							{
								m_fullNameRegex = new Regex(m_patternName + m_patternOptionPrefix + 
									m_patternCulture + m_patternOptionInfix +
									m_patternVersion + m_patternOptionInfix +
									m_patternStrongName + m_patternOptionInfix + 
									m_patternPublicKey + m_patternOptionSuffix, 
									m_regexOptions);
							}
						}
					}

					Match match = m_fullNameRegex.Match(value);
				
					if (! match.Success)
						throw new System.ApplicationException("Invalid assembly full name specified - unknown format");
				
					SetNameFromMatch(match);
					SetCultureFromMatch(match);
					SetVersionFromMatch(match);
					SetPublicKeyDetailsFromMatch(match);
				}
				else
				{
					m_name = new AssemblyName();
				}
			}
		}

		public string Name
		{
			get { return m_name.Name; }
			set { m_name.Name = value; }
		}

		public System.Version Version
		{
			get { return m_name.Version; }
			set { m_name.Version = value; }
		}

		public CultureInfo Culture
		{
			get { return m_name.CultureInfo; }
			set { m_name.CultureInfo = value; }
		}

		public byte[] PublicKeyToken
		{
			get { return m_name.GetPublicKeyToken(); }
			set { m_name.SetPublicKeyToken(value); m_keyFile = null; }
		}

		public byte[] PublicKey
		{
			get { return m_name.GetPublicKey(); }
			set { m_name.SetPublicKey(value); m_keyFile = null; }
		}

		public string KeyFile
		{
			get { return m_keyFile; }
			set 
			{ 
				m_keyFile = value; 
				m_name.KeyPair = null;
				m_name.SetPublicKey(null);
				m_name.SetPublicKeyToken(null);
			}
		}

		public string Location
		{
			get { return m_location; }
			set { m_location = value == null ? string.Empty : value; }
		}

		private static byte[] HexStringToByteArray(string hexString)
		{
			byte[] array = new byte[hexString.Length / 2];
			for(int index = 0; index < array.Length; ++index)
			{
				array[index] = System.Convert.ToByte(hexString.Substring(index * 2, 2), 16);
			}
			return array;
		}

		public void LoadKeyFile()
		{
			FileStream stream = new FileStream(m_keyFile, FileMode.Open, FileAccess.Read, FileShare.Read);
			try
			{
				m_name.KeyPair = new StrongNameKeyPair(stream);
			}
			finally
			{
				stream.Close();
			}
		}

		public string InitialisationString
		{
			get 
			{
				StringBuilder builder = new StringBuilder(m_name.Name);

				if (m_name.Version != null)
				{
					builder.AppendFormat(", Version={0}", m_name.Version.ToString());
				}

				if (m_name.CultureInfo != null)
				{
					builder.AppendFormat(", Culture={0}", m_name.CultureInfo.Name);
				}

				if (m_keyFile != null)
				{
					builder.AppendFormat(", KeyFile=\"{0}\"", m_keyFile.Replace(@"\", @"\\").Replace("\"", "\\\""));
				}

				return builder.ToString();
			}
			set
			{
				if (m_initStringRegex == null)
				{
					lock (this.GetType())
					{
						if (m_initStringRegex == null)
						{
							m_initStringRegex = new Regex(m_patternName + m_patternOptionPrefix + 
								m_patternCulture + m_patternOptionInfix +
								m_patternVersion + m_patternOptionInfix +
								m_patternKeyFile + m_patternOptionSuffix, 
								m_regexOptions);
						}
					}
				}

				Match match = m_initStringRegex.Match(value);
				
				if (! match.Success)
					throw new System.ApplicationException("Invalid assembly initialisation string specified - unknown format");

				SetNameFromMatch(match);
				SetCultureFromMatch(match);
				SetVersionFromMatch(match);
				SetKeyFileFromMatch(match);
			}
		}
		
		private void SetNameFromMatch(Match match)
		{
			m_name = new AssemblyName();
			m_name.Name = match.Groups["name"].Value;
		}

		private void SetVersionFromMatch(Match match)
		{
			if (match.Groups["version"].Captures.Count > 1)
				throw new System.ApplicationException("Invalid value specified - only one version may be specified");

			if (match.Groups["version"].Success)
			{
				string version = match.Groups["version"].Value;
				if (version.Length > 0 && version != "null")
					m_name.Version = new System.Version(version);
			}
		}

		private void SetCultureFromMatch(Match match)
		{
			if (match.Groups["culture"].Captures.Count > 1)
				throw new System.ApplicationException("Invalid value specified - only one culture may be specified");

			if (match.Groups["culture"].Success)
			{
				string culture = match.Groups["culture"].Value;
				if (culture.Length > 0 && culture != "null" && culture != "neutral")
					m_name.CultureInfo = new CultureInfo(culture);
			}
		}

		private void SetPublicKeyDetailsFromMatch(Match match)
		{
			if (match.Groups["strongName"].Captures.Count + match.Groups["publicKey"].Captures.Count > 1)
				throw new System.ApplicationException("Invalid value specified - only one instance of either a strong name or public key may be specified");

			if (match.Groups["strongName"].Success)
			{
				string strongName = match.Groups["strongName"].Value;
				if (strongName.Length > 0 && strongName != "null")
				{
					m_name.SetPublicKeyToken(HexStringToByteArray(strongName));
				}
			}

			if (match.Groups["publicKey"].Success)
			{
				string publicKey = match.Groups["publicKey"].Value;
				if (publicKey.Length > 0 && publicKey != "null")
				{
					m_name.SetPublicKey(HexStringToByteArray(publicKey));
				}
			}
		}

		private void SetKeyFileFromMatch(Match match)
		{
			if (match.Groups["keyFile"].Captures.Count > 1)
				throw new System.ApplicationException("Invalid value specified - only one key file may be specified");

			if (match.Groups["keyFile"].Success)
			{
				string keyFile = match.Groups["keyFile"].Value;
				if (keyFile.Length > 0 && keyFile != "null")
				{
					m_keyFile = Regex.Replace(keyFile, @"\\(.)", "$1");
				}
			}
		}

		#region ICloneable Members

		object System.ICloneable.Clone()
		{
			return this.Clone();
		}

		#endregion

		public AssemblyInfo Clone()
		{
			AssemblyInfo clone = new AssemblyInfo();
			clone.m_keyFile = m_keyFile;
			clone.m_name = (AssemblyName) m_name.Clone();
			return clone;
		}

		private AssemblyName m_name = new AssemblyName();
		private string m_location;
		private string m_keyFile;

		private static Regex m_fullNameRegex = null;
		private static Regex m_initStringRegex = null;

		private const RegexOptions m_regexOptions = RegexOptions.Compiled | RegexOptions.IgnoreCase;
		private const string m_patternName = " *(?<name>[^,]*[^, ]) *";
		private const string m_patternOptionPrefix = " *(?:, *(?:";
		private const string m_patternOptionInfix = "|";
		private const string m_patternOptionSuffix = ") *)*";
		private const string m_patternCulture = "(?:Culture *= *(?:(?<culture>[^\",]*[^, ])|\"(?<culture>[^\"]+)\"))";
		private const string m_patternVersion = "(?:Version *= *(?:(?<version>[^\",]*[^, ])|\"(?<version>[^\"]+)\"))";
		private const string m_patternStrongName = "(?:StrongName *= *(?:(?<strongName>[a-f0-9]+)|\"(?<strongName>[a-f0-9]+)\"))";
		private const string m_patternPublicKey = "(?:PublicKey *= *(?:(?<publicKey>[a-f0-9]+)|\"(?<publicKey>[a-f0-9]+)\"))";
		private const string m_patternKeyFile = "(?:KeyFile *= *(?:(?<keyFile>[^\",]*[^, ])|\"(?<keyFile>[^\"]+)\"))";
	}
}