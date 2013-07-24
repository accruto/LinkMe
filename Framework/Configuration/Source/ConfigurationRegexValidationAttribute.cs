namespace LinkMe.Framework.Configuration
{
    [System.AttributeUsage(System.AttributeTargets.Property)]
    public class ConfigurationRegexValidationAttribute
        : System.Attribute
    {
        public ConfigurationRegexValidationAttribute(string pattern)
        {
            m_pattern = pattern ?? string.Empty;
        }

        public string Pattern
        {
            get { return m_pattern; }
        }

        private string m_pattern;
    }
}
