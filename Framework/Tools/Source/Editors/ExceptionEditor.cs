using System;
using System.ComponentModel;
using System.Diagnostics;
using LinkMe.Framework.Utility.Exceptions;
using LinkMe.Framework.Tools.Controls;
using LinkMe.Framework.Tools.Net;

namespace LinkMe.Framework.Tools.Editors
{
	/// <summary>
	/// A wrapper around the ExceptionViewer control that implements IEditor.
	/// </summary>
	public class ExceptionEditor : ExceptionViewer, IRemoteEditor
	{
		#region Nested types

		private static class Worker
		{
			public static object GetExceptionInfo(GenericWrapper wrapper, object state)
			{
				if ((wrapper.AvailableFormats & WrappedValueFormats.Object) != WrappedValueFormats.Object)
					throw new ArgumentException("The GenericWrapper does not contain an object.");

				object val = wrapper.GetWrappedObject();

                var info = val as ExceptionInfo;
                if (info != null)
                    return info;

                var ex = val as Exception;
                if (ex != null)
                    return new ExceptionInfo(ex, null);

				throw new ArgumentException(string.Format("The GenericWrapper contains a '{0}' object, which"
					+ " is not derived from '{1}' or '{2}'.",
                    val.GetType().FullName, typeof(ExceptionInfo).FullName, typeof(Exception).FullName));
			}
		}

		#endregion

	    #region IEditor Members

		[Browsable(false)]
		public bool Modified
		{
			get { return false; }
		}

		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public bool ReadOnly
		{
			get { return true; }
			set { }
		}

		[Browsable(false)]
		public bool SupportsEditing
		{
			get { return false; }
		}

		public void BeginEditNew()
		{
			InitialiseFocus();
		}

		public bool CanDisplay(Type type)
		{
			if (type == null)
				throw new ArgumentNullException("type");

			return (typeof(ExceptionInfo).IsAssignableFrom(type) || typeof(Exception).IsAssignableFrom(type));
		}

		public void Clear()
		{
			DisplayException((ExceptionInfo)null);
		}

		public void DisplayValue(object value)
		{
			Debug.Assert(!(value is GenericWrapper), "GenericWrapper object passed to DisplayValue() - call"
				+ " DisplayRemoteValue() instead.");

			if (value == null)
			{
				Clear();
			}
			else if (value is ExceptionInfo)
			{
				DisplayException((ExceptionInfo)value);
			}
			else if (value is Exception)
			{
				DisplayException((Exception)value);
			}
			else
			{
				throw new ArgumentException("The '" + GetType().FullName
					+ "' control cannot display a value of type '" + value.GetType().FullName + "'.", "value");
			}
		}

		public object GetValue()
		{
			throw new NotSupportedException("The '" + GetType().FullName + "' editor is always read-only,"
				+ " so it does not support retrieving the value.");
		}

		public object GetValue(Type type)
		{
			return GetValue();
		}

		#endregion

		#region IRemoteEditor Members

		public WrappedValueFormats UsedFormats
		{
			get { return WrappedValueFormats.Object; }
		}

		public void DisplayRemoteValue(GenericWrapper wrapper)
		{
			if (wrapper == null)
				throw new ArgumentNullException("wrapper");

			DisplayException((ExceptionInfo)wrapper.RunCustomWorker(Worker.GetExceptionInfo, null));
		}

		public GenericWrapper GetRemoteValue()
		{
			throw new NotSupportedException("The '" + GetType().FullName + "' editor is always read-only,"
				+ " so it does not support retrieving the value.");
		}

		#endregion
	}
}
