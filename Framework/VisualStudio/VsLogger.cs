using System.Diagnostics;
using System.Globalization;
using System.CodeDom.Compiler;
using System.Runtime.InteropServices;
using System.Text;
using Microsoft.Build.Framework;
using Microsoft.Build.Utilities;
using Microsoft.VisualStudio;
using Microsoft.VisualStudio.Shell.Interop;
using Microsoft.VisualStudio.Shell;
using Microsoft.VisualStudio.TextManager.Interop;
using Microsoft.Win32;

using IOleServiceProvider = Microsoft.VisualStudio.OLE.Interop.IServiceProvider;

namespace LinkMe.Framework.VisualStudio
{
	/// <summary>
	/// The supplied logger in the SDK, IDEBuildLogger, has some bugs in formatting errors which are
	/// not easily fixed because that class is sealed.  This class has been written to fix those bugs.
	/// </summary>
	public sealed class VsLogger
		: Logger
	{
		#region Fields

		private string m_verbosityRegistryRoot = Constants.Registry.VisualStudio.RootKeyPath;
		private string m_errorString = string.Empty;
		private string m_warningString = string.Empty;
		private int m_currentIndentation = 0;
		private IVsOutputWindowPane m_outputWindowPane;
		private TaskProvider m_taskProvider;
		private IVsHierarchy m_hierarchy;
		private System.IServiceProvider m_serviceProvider;

		#endregion

		#region Properties

		public string WarningString
        {
            get { return m_warningString; }
            set { m_warningString = value; }
        }

        public string ErrorString
        {
            get { return m_errorString; }
            set { m_errorString = value; }
        }

		public string VerbosityRegistryRoot
		{
			get { return m_verbosityRegistryRoot; }
			set { m_verbosityRegistryRoot = value; }
		}

		public IVsOutputWindowPane OutputWindowPane
		{
			get { return m_outputWindowPane; }
			set { m_outputWindowPane = value; }
		}

		#endregion

		public VsLogger(IVsOutputWindowPane output, TaskProvider taskProvider, IVsHierarchy hierarchy)
		{
			if ( taskProvider == null )
				throw new System.ArgumentNullException("taskProvider");
			if ( hierarchy == null )
				throw new System.ArgumentNullException("hierarchy");

			m_taskProvider = taskProvider;
			m_outputWindowPane = output;
			m_hierarchy = hierarchy;
			IOleServiceProvider site;
			Microsoft.VisualStudio.ErrorHandler.ThrowOnFailure(hierarchy.GetSite(out site));
			m_serviceProvider = new ServiceProvider(site);
		}

		public override void Initialize(IEventSource eventSource)
		{
			if ( null == eventSource )
				throw new System.ArgumentNullException("eventSource");
			eventSource.BuildStarted += new BuildStartedEventHandler(BuildStartedHandler);
			eventSource.BuildFinished += new BuildFinishedEventHandler(BuildFinishedHandler);
			eventSource.ProjectStarted += new ProjectStartedEventHandler(ProjectStartedHandler);
			eventSource.ProjectFinished += new ProjectFinishedEventHandler(ProjectFinishedHandler);
			eventSource.TargetStarted += new TargetStartedEventHandler(TargetStartedHandler);
			eventSource.TargetFinished += new TargetFinishedEventHandler(TargetFinishedHandler);
			eventSource.TaskStarted += new TaskStartedEventHandler(TaskStartedHandler);
			eventSource.TaskFinished += new TaskFinishedEventHandler(TaskFinishedHandler);
			eventSource.CustomEventRaised += new CustomBuildEventHandler(CustomHandler);
			eventSource.ErrorRaised += new BuildErrorEventHandler(ErrorHandler);
			eventSource.WarningRaised += new BuildWarningEventHandler(WarningHandler);
			eventSource.MessageRaised += new BuildMessageEventHandler(MessageHandler);
		}

		#region Delegates

		private void ErrorHandler(object sender, BuildErrorEventArgs errorEvent)
		{
			AddToErrorList(errorEvent, errorEvent.Code, errorEvent.File, errorEvent.LineNumber, errorEvent.ColumnNumber);
		}

		private void WarningHandler(object sender, BuildWarningEventArgs errorEvent)
		{
			AddToErrorList(errorEvent, errorEvent.Code, errorEvent.File, errorEvent.LineNumber, errorEvent.ColumnNumber);
		}

		private void MessageHandler(object sender, BuildMessageEventArgs messageEvent)
		{
			if ( IfLog(messageEvent.Importance) )
				Log(messageEvent);
		}

		private void BuildStartedHandler(object sender, BuildStartedEventArgs buildEvent)
		{
			if ( IfLog(MessageImportance.Low) )
				Log(buildEvent);

			// Remove all tasks at the start of a build and determine the verbosity.

			m_taskProvider.Tasks.Clear();
			SetVerbosity();
		}

		private void BuildFinishedHandler(object sender, BuildFinishedEventArgs buildEvent)
		{
			if ( IfLog(buildEvent.Succeeded ? MessageImportance.Low : MessageImportance.High) )
			{
				if ( m_outputWindowPane != null )
					m_outputWindowPane.OutputStringThreadSafe(System.Environment.NewLine);
				Log(buildEvent);
			}
		}

		private void ProjectStartedHandler(object sender, ProjectStartedEventArgs buildEvent)
		{
			if ( IfLog(MessageImportance.Low) )
				Log(buildEvent);
		}

		private void ProjectFinishedHandler(object sender, ProjectFinishedEventArgs buildEvent)
		{
			if ( IfLog(buildEvent.Succeeded ? MessageImportance.Low : MessageImportance.High) )
				Log(buildEvent);
		}

		private void TargetStartedHandler(object sender, TargetStartedEventArgs buildEvent)
		{
			if ( IfLog(MessageImportance.Normal) )
				Log(buildEvent);
			++m_currentIndentation;
		}

		private void TargetFinishedHandler(object sender, TargetFinishedEventArgs buildEvent)
		{
			--m_currentIndentation;
			if ( IfLog(buildEvent.Succeeded ? MessageImportance.Low : MessageImportance.High) )
				Log(buildEvent);
		}

		private void TaskStartedHandler(object sender, TaskStartedEventArgs buildEvent)
		{
			if ( IfLog(MessageImportance.Normal) )
				Log(buildEvent);
			++m_currentIndentation;
		}

		private void TaskFinishedHandler(object sender, TaskFinishedEventArgs buildEvent)
		{
			--m_currentIndentation;
			if ( IfLog(buildEvent.Succeeded ? MessageImportance.Normal : MessageImportance.High) )
				Log(buildEvent);
		}

		private void CustomHandler(object sender, CustomBuildEventArgs buildEvent)
		{
			Log(buildEvent);
		}

		#endregion

		#region Helpers

		private void AddToErrorList(BuildEventArgs errorEvent, string errorCode, string file, int line, int column)
		{
			TaskPriority priority = errorEvent is BuildErrorEventArgs ? TaskPriority.High : TaskPriority.Normal;
			if ( m_outputWindowPane != null && (Verbosity != LoggerVerbosity.Quiet || errorEvent is BuildErrorEventArgs) )
			{
				// Format error and output it to the output window

				CompilerError error = new CompilerError(file, line, column, errorCode, errorEvent.Message);
				error.IsWarning = errorEvent is BuildWarningEventArgs;
				Microsoft.VisualStudio.ErrorHandler.ThrowOnFailure(m_outputWindowPane.OutputStringThreadSafe(GetErrorMessage(error)));
			}

			// Add error to task list

			ErrorTask task = new ErrorTask();
			task.Document = file;
			task.Line = line - 1; // The task list does +1 before showing this number.
			task.Column = column;
			task.Text = errorEvent.Message;
			task.Priority = priority;
			task.Category = TaskCategory.BuildCompile;
			task.HierarchyItem = m_hierarchy;
			task.Navigate += new System.EventHandler(NavigateToHandler);
			if ( errorEvent is BuildWarningEventArgs )
				task.ErrorCategory = TaskErrorCategory.Warning;
			m_taskProvider.Tasks.Add(task);
		}

		private void NavigateToHandler(object sender, System.EventArgs args)
		{
			Microsoft.VisualStudio.Shell.Task task = sender as Microsoft.VisualStudio.Shell.Task;
			if ( task == null )
				throw new System.ArgumentException("sender");

			// Open the document.

			if ( string.IsNullOrEmpty(task.Document) )
				return;
			IVsUIShellOpenDocument openDocument = m_serviceProvider.GetService(typeof(IVsUIShellOpenDocument)) as IVsUIShellOpenDocument;
			if ( openDocument == null )
				return;

			IVsWindowFrame frame;
			IOleServiceProvider serviceProvider;
			IVsUIHierarchy hierarchy;
			uint itemId;
			System.Guid logicalView = VSConstants.LOGVIEWID_Code;
			if ( Failed(openDocument.OpenDocumentViaProject(task.Document, ref logicalView, out serviceProvider, out hierarchy, out itemId, out frame)) || frame == null )
				return;

			// Get the text buffer.

			object docData;
			frame.GetProperty((int) __VSFPROPID.VSFPROPID_DocData, out docData);
			VsTextBuffer buffer = docData as VsTextBuffer;
			if ( buffer == null )
			{
				IVsTextBufferProvider bufferProvider = docData as IVsTextBufferProvider;
				if ( bufferProvider != null )
				{
					IVsTextLines lines;
					ThrowOnFailure(bufferProvider.GetTextBuffer(out lines));
					buffer = lines as VsTextBuffer;
					if ( buffer == null )
						return;
				}
			}

			// Perform the navigation.

			IVsTextManager manager = m_serviceProvider.GetService(typeof(VsTextManagerClass)) as IVsTextManager;
			if ( manager == null )
				return;
			manager.NavigateToLineAndColumn(buffer, ref logicalView, task.Line, task.Column, task.Line, task.Column);
		}

		private string GetErrorMessage(CompilerError error)
		{
			if ( error == null )
				return string.Empty;

			StringBuilder sb = new StringBuilder();
			if ( !string.IsNullOrEmpty(error.FileName) )
				sb.AppendFormat(CultureInfo.CurrentUICulture, "{0}({1},{2}): ", error.FileName, error.Line, error.Column);
			sb.AppendFormat(CultureInfo.CurrentUICulture, "{0} {1}: {2}", error.IsWarning ? m_warningString : m_errorString, error.ErrorNumber, (error.ErrorText == null ? string.Empty : error.ErrorText) + System.Environment.NewLine);
			return sb.ToString();
		}

		private void SetVerbosity()
		{
			string keyPath = string.Format(@"{0}\{1}", m_verbosityRegistryRoot, Constants.Registry.VisualStudio.GeneralKeyPath);
			using ( RegistryKey key = Registry.CurrentUser.OpenSubKey(keyPath) )
			{
				if ( key != null )
				{
					object value = key.GetValue(Constants.Registry.VisualStudio.Verbosity);
					if ( value != null && value is int )
						Verbosity = (LoggerVerbosity) (int) value;
				}
			}
		}

		private bool IfLog(MessageImportance importance)
		{
			switch ( Verbosity )
			{
				case LoggerVerbosity.Quiet:
					return false;

				case LoggerVerbosity.Minimal:
					return importance == MessageImportance.High;

				case LoggerVerbosity.Normal:
				case LoggerVerbosity.Detailed:
					return importance != MessageImportance.Low;

				case LoggerVerbosity.Diagnostic:
					return true;

				default:
					return true;
			}
		}

		private void Log(BuildEventArgs buildEvent)
		{
			if ( m_outputWindowPane != null && !string.IsNullOrEmpty(buildEvent.Message) )
			{
				StringBuilder message = new StringBuilder();
				if ( m_currentIndentation > 0 )
					message.Append('\t', m_currentIndentation);
				message.AppendLine(buildEvent.Message);
				m_outputWindowPane.OutputStringThreadSafe(message.ToString());
			}
		}

		public bool Failed(int hr)
		{
			return hr < 0;
		}

		public int ThrowOnFailure(int hr)
		{
			if ( Failed(hr) )
				Marshal.ThrowExceptionForHR(hr);
			return hr;
		}

		#endregion
	}
}
