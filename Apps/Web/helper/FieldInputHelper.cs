using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Diagnostics;
using System.Web.UI;
using System.Web.UI.WebControls;
using LinkMe.Framework.Utility;
using LinkMe.WebControls;

namespace LinkMe.Web.Helper
{
	internal static class FieldInputHelper
	{
        internal static string GetRelativeId(Control control, Control relativeTo, char idSeparator)
        {
            if (control == null)
                throw new ArgumentNullException("control");

            return GetRelativeId(control.UniqueID, relativeTo, idSeparator);
        }
        
        internal static string GetRelativeId(string uniqueId, Control relativeTo, char idSeparator)
	    {
            if (string.IsNullOrEmpty(uniqueId))
                throw new ArgumentException("The control unique ID must be specified.", "uniqueId");
            if (relativeTo == null)
                throw new ArgumentNullException("relativeTo");

            string parentIdPrefix = relativeTo.NamingContainer.UniqueID + idSeparator;

            return (uniqueId.StartsWith(parentIdPrefix) ? uniqueId.Substring(parentIdPrefix.Length) : uniqueId);
        }

	    internal static IEnumerable<ListItem> CreateListItems<T>(IEnumerable<T> items, Func<T, string> getText, bool selected)
            where T : struct, IConvertible
		{
            Debug.Assert(typeof(T).IsEnum, "typeof(T).IsEnum");

            // Use integer values instead of just ToString(). This allows them to be parsed in JavaScript.

            foreach (var item in items)
			{
                var listItem = new ListItem
                {
                    Text = getText(item),
                    Value = item.ToInt32(null).ToString(),
                    Selected = selected
                };

			    yield return listItem;
			}
		}

        internal static T GetEnum<T>(this ListItemCollection items)
            where T : struct, IConvertible
        {
            return (T)Enum.ToObject(typeof(T), ControlUtils.SelectedListItemValuesToInt(items));
        }

	    internal static void SetFlags<T>(this ListItemCollection items, T value)
            where T : struct, IConvertible
		{
			Debug.Assert(items != null, "items != null");
            Debug.Assert(typeof(T).IsEnum, "The type must be an enum.");
            Debug.Assert(typeof(T).GetCustomAttributes(typeof(FlagsAttribute), false).Length > 0,
                "The type must be an enum decorated with [Flags]. Use SetEnum() instead if it's not.");

            int intValue = value.ToInt32(null);

			foreach (ListItem item in items)
			{
				Debug.Assert(item.Value != null, "item.Value != null");

				// A simple cast to int doesn't work here for some reason.
				int itemValue = ((IConvertible)Enum.Parse(typeof(T), item.Value, false)).ToInt32(null);

                if (intValue == 0)
                {
                    item.Selected = (itemValue == 0);
                }
                else
                {
                    item.Selected = ((intValue & itemValue) != 0);
                }
			}
		}

	    internal static void SetFlags<T>(this ListItemCollection items, T? value, bool selectIfNull)
            where T : struct, IConvertible
	    {
	        if (value.HasValue)
	        {
	            SetFlags(items, value.Value);
	        }
	        else
	        {
	            foreach (ListItem item in items)
	                item.Selected = selectIfNull;
	        }
	    }

        internal static void SetEnum<T>(this ListItemCollection items, T value)
            where T : struct, IConvertible
        {
            Debug.Assert(items != null, "items != null");
            Debug.Assert(typeof(T).IsEnum, "The type must be an enum.");
            Debug.Assert(typeof(T).GetCustomAttributes(typeof(FlagsAttribute), false).Length == 0,
                "The type must not be decorated with [Flags]. Use SetFlags() if it is.");

            int intValue = value.ToInt32(null);

            foreach (ListItem item in items)
            {
                Debug.Assert(item.Value != null, "item.Value != null");
                int itemValue = ((IConvertible)Enum.Parse(typeof(T), item.Value, false)).ToInt32(null);
                item.Selected = (itemValue == intValue);
            }
        }

		internal static IDictionary ValidateNameValueCollection(NameValueCollection nvc)
		{
			var errors = new HybridDictionary();

			int count = nvc.Count;

			for (int i = 0; i < count; i++)
			{
				string key = nvc.GetKey(i);
				if (key != "__VIEWSTATE")
				{
					string values = nvc.Get(i);
					if (!string.IsNullOrEmpty(values))
					{
						string message = ValidateString(values);

						if (message != null)
						{
							if (key == null)
							{
								key = "(null)";
							}

							errors.Add(key, message);
						}
					}
				}
			}

			return errors;
		}

		private static string ValidateString(string s)
		{
			int num1;
			if (HtmlUtil.ContainsHtml(s, out num1))
			{
				string text1 = "";
				int num2 = num1 - 10;
				if (num2 <= 0)
				{
					num2 = 0;
				}
				else
				{
					text1 = text1 + "...";
				}
				int num3 = num1 + 20;
				if (num3 >= s.Length)
				{
					num3 = s.Length;
					text1 = text1 + s.Substring(num2, num3 - num2);
				}
				else
				{
					text1 = text1 + s.Substring(num2, num3 - num2) + "...";
				}

				return text1;
			}
			return null;
		}
	}
}