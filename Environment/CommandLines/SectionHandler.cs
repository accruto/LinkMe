using System;
using System.Configuration;
using System.Xml;

namespace LinkMe.Environment.CommandLines
{
    /// <summary>
    /// Summary description for SectionHandler.
    /// </summary>
    public class SectionHandler
        :	IConfigurationSectionHandler
    {
        public SectionHandler()
        {
        }
	
        #region IConfigurationSectionHandler Members

        public object Create(object parent, object configContext, XmlNode section)
        {
            var manager = new CommandManager();

            // Read all options.

            var options = section.SelectSingleNode("options");
            if (options != null)
            {
                foreach (var option in options.ChildNodes)
                {
                    if (option is XmlElement)
                        manager.Add(ReadOption((XmlElement) option));
                }
            }

            // Read all commands.

            var commands = section.SelectSingleNode("commands");
            if (commands != null)
            {
                foreach (var command in commands.ChildNodes)
                {
                    if (command is XmlElement)
                        manager.Add(ReadCommand((XmlElement) command, manager));
                }
            }

            return manager;
        }

        #endregion

        private static CommandConfiguration ReadCommand(XmlElement xmlCommand, CommandManager manager)
        {
            // Read the flag.

            var flag = xmlCommand.Name;
            var className = GetAttribute(xmlCommand, "class");
            var description = GetElementString(xmlCommand, "description", string.Empty);
            var command = new CommandConfiguration(flag, className, description);

            // Read the options.

            var xmlOptions = xmlCommand.SelectSingleNode("options");
            if (xmlOptions != null)
            {
                foreach (var xmlOption in xmlOptions.ChildNodes)
                {
                    if (xmlOption is XmlElement)
                    {
                        var option = manager.Options[((XmlElement)xmlOption).Name];
                        if (option == null)
                            throw new ApplicationException("The '" + ((XmlElement)xmlOption).Name + "' option for the '" + flag + "' command does not have a corresponding definition.");
                        command.Add(option);
                    }
                }
            }

            return command;
        }

        private OptionConfiguration ReadOption(XmlElement xmlOption)
        {
            var name = xmlOption.Name;
            var type = GetAttributeString(xmlOption, "type", "flag");
            var required = GetAttributeBoolean(xmlOption, "required", false);

            switch (type)
            {
                case "or":
                    var orOption = new OrOptionConfiguration(name, required);
                    foreach (var xmlOrOption in xmlOption.ChildNodes)
                    {
                        if (xmlOrOption is XmlElement)
                            orOption.Add(ReadOption((XmlElement)xmlOrOption));
                    }

                    return orOption;

                case "and":
                    var andOption = new AndOptionConfiguration(name, required);
                    foreach (var xmlAndOption in xmlOption.ChildNodes)
                    {
                        if (xmlAndOption is XmlElement)
                            andOption.Add(ReadOption((XmlElement)xmlAndOption));
                    }

                    return andOption;

                default:
                    var value = GetAttributeBoolean(xmlOption, "value", false);
                    var multiple = GetAttributeBoolean(xmlOption, "multiple", false);
                    var description = GetElementString(xmlOption, "description", string.Empty);
                    var flagOption = new FlagOptionConfiguration(name, required, value, multiple, description);

                    var xmlValueNames = xmlOption.SelectSingleNode("valuenames");
                    if (xmlValueNames != null)
                    {
                        foreach (XmlNode xmlValueName in xmlValueNames.SelectNodes("valuename"))
                            flagOption.AddValueName(xmlValueName.InnerText);
                    }

                    return flagOption;
            }
        }

        private static string GetAttribute(XmlElement xmlElement, string name)
        {
            var xmlAttribute = xmlElement.Attributes[name];
            if (xmlAttribute == null)
                throw new ApplicationException("Cannot find the required '" + name + "' attribute on the '" + xmlElement.Name + "' element.");
            return xmlAttribute.Value;
        }

        private static string GetAttributeString(XmlElement xmlElement, string name, string defaultValue)
        {
            var xmlAttribute = xmlElement.Attributes[name];
            return xmlAttribute == null ? defaultValue : xmlAttribute.Value;
        }

        private static bool GetAttributeBoolean(XmlElement xmlElement, string name, bool defaultValue)
        {
            var xmlAttribute = xmlElement.Attributes[name];
            return xmlAttribute == null ? defaultValue : XmlConvert.ToBoolean(xmlAttribute.Value);
        }

        private static string GetElementString(XmlElement xmlElement, string name, string defaultValue)
        {
            var xmlChild = xmlElement.SelectSingleNode(name);
            return xmlChild == null ? defaultValue : xmlChild.InnerText;
        }
    }
}