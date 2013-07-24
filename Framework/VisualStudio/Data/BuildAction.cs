using System.ComponentModel;
using System.Globalization;
using LinkMe.Environment;
using Microsoft.VisualStudio.Project;

namespace LinkMe.Framework.VisualStudio.Data
{
	[PropertyPageTypeConverter(typeof(BuildActionConverter))]
	public enum BuildAction { None, Merge }

	public class BuildActionConverter
		:	EnumConverter
	{
		public BuildActionConverter()
			:	base(typeof(BuildAction))
		{
		}

		public override bool CanConvertFrom(ITypeDescriptorContext context, System.Type sourceType)
		{
			if ( sourceType == typeof(string) )
				return true;
			return base.CanConvertFrom(context, sourceType);
		}

		public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
		{
			string str = value as string;
			if ( str != null )
			{
				if ( str == StringResourceManager.GetString(typeof(Resources.StringResources), Constants.BuildAction.Merge, culture) )
					return BuildAction.Merge;

				if ( str == StringResourceManager.GetString(typeof(Resources.StringResources), Constants.BuildAction.None, culture) )
					return BuildAction.None;
			}

			return base.ConvertFrom(context, culture, value);
		}

		public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, System.Type destinationType)
		{
			if ( destinationType == typeof(string) )
			{
				string result = null;
				if ( value != null )
					result = StringResourceManager.GetString(typeof(Resources.StringResources), ((BuildAction) value).ToString(), culture);
				else
					result = StringResourceManager.GetString(typeof(Resources.StringResources), BuildAction.None.ToString(), culture);

				if ( result != null )
					return result;
			}

			return base.ConvertTo(context, culture, value, destinationType);
		}

		public override bool GetStandardValuesSupported(ITypeDescriptorContext context)
		{
			return true;
		}

		public override StandardValuesCollection GetStandardValues(ITypeDescriptorContext context)
		{
			return new StandardValuesCollection(new BuildAction[] { BuildAction.Merge, BuildAction.None });
		}
	}
}
