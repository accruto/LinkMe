using System.ComponentModel;
using System.Globalization;
using LinkMe.Environment;
using Microsoft.VisualStudio.Project;

namespace LinkMe.Framework.VisualStudio.Assemble
{
	[PropertyPageTypeConverter(typeof(BuildActionConverter))]
	public enum BuildAction { None, Assemble, CopyOnBuild }

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
				if ( str == StringResourceManager.GetString(typeof(Resources.StringResources), Constants.BuildAction.Assemble, culture) )
					return BuildAction.Assemble;

				if ( str == StringResourceManager.GetString(typeof(Resources.StringResources), Constants.BuildAction.CopyOnBuild, culture) )
					return BuildAction.CopyOnBuild;

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

		public override bool GetStandardValuesSupported(System.ComponentModel.ITypeDescriptorContext context)
		{
			return true;
		}

		public override StandardValuesCollection GetStandardValues(ITypeDescriptorContext context)
		{
			return new StandardValuesCollection(new BuildAction[] { BuildAction.Assemble, BuildAction.CopyOnBuild, BuildAction.None });
		}
	}
}
