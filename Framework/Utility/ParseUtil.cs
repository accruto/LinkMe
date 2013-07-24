using System;
using System.Diagnostics;
using LinkMe.Framework.Utility.Exceptions;

namespace LinkMe.Framework.Utility
{
    public static class ParseUtil
    {
        public static bool ParseUserInputBoolean(string text, string description)
        {
            if (string.IsNullOrEmpty(description))
                throw new ArgumentException("The description must be specified.", "description");

            if (TextUtil.TrimAndCheckEmpty(ref text))
                throw new UserException("No " + description + " was specified.");

            return ParseUserInputBooleanInternal(text, description);
        }

        public static bool ParseUserInputBooleanOptional(string text, string description, bool defaultValue)
        {
            if (string.IsNullOrEmpty(description))
                throw new ArgumentException("The description must be specified.", "description");

            if (TextUtil.TrimAndCheckEmpty(ref text))
                return defaultValue;

            return ParseUserInputBooleanInternal(text, description);
        }

        public static int ParseUserInputInt32(string text, string description)
        {
            if (string.IsNullOrEmpty(description))
                throw new ArgumentException("The description must be specified.", "description");

            if (TextUtil.TrimAndCheckEmpty(ref text))
                throw new UserException("No " + description + " was specified.");

            return ParseUserInputInt32Internal(text, description);
        }

        public static int? ParseUserInputInt32Optional(string text, string description)
        {
            if (string.IsNullOrEmpty(description))
                throw new ArgumentException("The description must be specified.", "description");

            if (TextUtil.TrimAndCheckEmpty(ref text))
                return null;

            return ParseUserInputInt32Internal(text, description);
        }

        public static int? ParseUserInputInt32Optional(string text, string description, int? defaultValue)
        {
            if (string.IsNullOrEmpty(description))
                throw new ArgumentException("The description must be specified.", "description");

            if (TextUtil.TrimAndCheckEmpty(ref text))
                return defaultValue;

            return ParseUserInputInt32Internal(text, description);
        }

        public static int ParseUserInputInt32Optional(string text, string description, int defaultValue)
        {
            if (string.IsNullOrEmpty(description))
                throw new ArgumentException("The description must be specified.", "description");

            if (TextUtil.TrimAndCheckEmpty(ref text))
                return defaultValue;

            return ParseUserInputInt32Internal(text, description);
        }

        public static Guid ParseUserInputGuid(string text, string description)
        {
            if (string.IsNullOrEmpty(description))
                throw new ArgumentException("The description must be specified.", "description");

            if (TextUtil.TrimAndCheckEmpty(ref text))
                throw new UserException("No " + description + " was specified.");

            return ParseUserInputGuidInternal(text, description);
        }

        public static Guid? ParseUserInputGuidOptional(string text, string description)
        {
            if (string.IsNullOrEmpty(description))
                throw new ArgumentException("The description must be specified.", "description");

            if (TextUtil.TrimAndCheckEmpty(ref text))
                return null;

            try
            {
                return new Guid(text);
            }
            catch (FormatException ex)
            {
                throw new UserException("The specified " + description + ", '" + HtmlUtil.StrippedIfContainsHtml(text)
                                           + "', is not a valid GUID.", ex);
            }
        }

        public static Guid[] ParseUserInputGuidList(string text, char separator, string itemDescription)
        {
            if (string.IsNullOrEmpty(itemDescription))
                throw new ArgumentException("The item description must be specified.", "itemDescription");

            if (TextUtil.TrimAndCheckEmpty(ref text))
                return null;

            string[] strings = text.Split(separator);
            if (strings.Length == 0)
                return null;

            var values = new Guid[strings.Length];

            for (int i = 0; i < strings.Length; i++)
            {
                string itemText = strings[i];
                if (TextUtil.TrimAndCheckEmpty(ref itemText))
                    throw new UserException("At least one " + itemDescription + " in the list is empty.");

                values[i] = ParseUserInputGuidInternal(itemText, itemDescription);
            }

            return values;
        }

        public static DateTime ParseUserInputDateTime(string text, string description)
        {
            if (string.IsNullOrEmpty(description))
                throw new ArgumentException("The description must be specified.", "description");

            if (TextUtil.TrimAndCheckEmpty(ref text))
                throw new UserException("No " + description + " was specified.");

            try
            {
                return DateTime.Parse(text);
            }
            catch (FormatException ex)
            {
                throw new UserException("The specified " + description + ", '" + HtmlUtil.StrippedIfContainsHtml(text)
                                           + "', is not a valid date/time.", ex);
            }
        }

        public static T ParseUserInputEnumOptional<T>(string text, string description, T defaultValue)
            where T : struct
        {
            T? parsed = ParseUserInputEnumOptional<T>(text, description);
            return (parsed.HasValue ? parsed.Value : defaultValue);
        }

        public static T? ParseUserInputEnumOptional<T>(string text, string description)
            where T : struct
        {
            if (string.IsNullOrEmpty(description))
                throw new ArgumentException("The description must be specified.", "description");

            if (TextUtil.TrimAndCheckEmpty(ref text))
                return null;

            return ParseUserInputEnumInternal<T>(text, description);
        }

        public static T ParseUserInputEnum<T>(string text, string description)
            where T : struct
        {
            if (string.IsNullOrEmpty(description))
                throw new ArgumentException("The description must be specified.", "description");

            if (TextUtil.TrimAndCheckEmpty(ref text))
                throw new UserException("No " + description + " was specified.");

            return ParseUserInputEnumInternal<T>(text, description);
        }

        public static decimal ParseUserInputDecimal(string text, string description)
        {
            if (string.IsNullOrEmpty(description))
                throw new ArgumentException("The description must be specified.", "description");

            if (TextUtil.TrimAndCheckEmpty(ref text))
                throw new UserException("No " + description + " was specified.");

            return ParseUserInputDecimalInternal(text, description);
        }

        public static int? ParseUserInputDistanceKmOptional(string text, string description, int? defaultValue)
        {
            // Remove "km" from the string, case-insensitive.

            if (text != null)
            {
                int index = text.IndexOf("km", StringComparison.InvariantCultureIgnoreCase);
                if (index != -1)
                {
                    text = text.Remove(index, 2);
                }
            }

            return ParseUserInputInt32Optional(text, description, defaultValue);
        }

        private static bool ParseUserInputBooleanInternal(string text, string description)
        {
            try
            {
                return bool.Parse(text);
            }
            catch (FormatException ex)
            {
                throw new UserException(string.Format("The specified {0}, '{1}', is not a valid boolean.",
                                                         description, HtmlUtil.StrippedIfContainsHtml(text)), ex);
            }
        }

        private static int ParseUserInputInt32Internal(string text, string description)
        {
            try
            {
                return int.Parse(text);
            }
            catch (FormatException ex)
            {
                throw new UserException(string.Format("The specified {0}, '{1}', is not a valid number.",
                                                         description, HtmlUtil.StrippedIfContainsHtml(text)), ex);
            }
            catch (OverflowException)
            {
                throw new UserException(string.Format("The specified {0}, '{1}', is too large"
                                                         + " or too small.", description, HtmlUtil.StrippedIfContainsHtml(text)));
            }
        }

        private static decimal ParseUserInputDecimalInternal(string text, string description)
        {
            try
            {
                return decimal.Parse(text);
            }
            catch (FormatException ex)
            {
                throw new UserException(string.Format("The specified {0}, '{1}', is not a valid decimal"
                                                         + " number.", description, HtmlUtil.StrippedIfContainsHtml(text)), ex);
            }
            catch (OverflowException)
            {
                throw new UserException(string.Format("The specified {0}, '{1}', is too large"
                                                         + " or too small.", description, HtmlUtil.StrippedIfContainsHtml(text)));
            }
        }

        private static T ParseUserInputEnumInternal<T>(string text, string description)
            where T : struct
        {
            Debug.Assert(typeof(T).IsEnum, "typeof(T).IsEnum");
            Debug.Assert(!string.IsNullOrEmpty(text), "!string.IsNullOrEmpty(text)");

            if (TextUtil.IsDigits(text))
            {
                int intValue = ParseUserInputInt32Internal(text, description);
                return (T)Enum.ToObject(typeof(T), intValue);
            }

            try
            {
                return (T)Enum.Parse(typeof(T), text, true);
            }
            catch (ArgumentException ex)
            {
                // Don't include the enum name here - it makes no sense to the end user.
                throw new UserException(string.Format("'{0}' is not a valid value for {1}.",
                                                         HtmlUtil.StrippedIfContainsHtml(text), description), ex);
            }
        }

        private static Guid ParseUserInputGuidInternal(string text, string description)
        {
            try
            {
                return new Guid(text);
            }
            catch (FormatException ex)
            {
                throw new UserException("The specified " + description + ", '"
                                           + HtmlUtil.StrippedIfContainsHtml(text) + "', is not a valid GUID.", ex);
            }
        }

        public static Guid? TryParseGuid(string text)
        {
            if (text == null || text.Length < 32 || text.Length > 38)
                return null;

            try
            {
                return new Guid(text);
            }
            catch
            {
                return null;
            }
        }
    }
}