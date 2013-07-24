using System.Data;
using System.Data.OleDb;
using System.Diagnostics;

using LinkMe.Framework.Type;
using LinkMe.Framework.Instrumentation.Message;

namespace LinkMe.Framework.Instrumentation.MessageComponents
{
/*	internal class MssqlUtility
	{
		private string m_connectionString;

		internal MssqlUtility(string connectionString)
		{
			Debug.Assert(connectionString != null && connectionString.Length != 0,
				"connectionString != null && connectionString.Length != 0");

			m_connectionString = connectionString;
		}

		public void SaveLogMessage(EventMessage logMessage)
		{
			// Create the connection and command objects
			using (OleDbConnection connection = new OleDbConnection( m_connectionString ) )
			{
				OleDbCommand cmd = new OleDbCommand("usp_UpdateLogMessage",
					connection);
				cmd.CommandType = CommandType.StoredProcedure;

				// Add the parameters
				AddParametersToCommand( logMessage, cmd.Parameters );

				// Execute
				connection.Open();

				cmd.ExecuteNonQuery();
			}
		}


		public void SaveLogMessages(EventMessages eventMessages)
		{
			if ( eventMessages != null)
			{
				foreach ( EventMessage eventMessage in eventMessages)
				{
					this.SaveLogMessage( eventMessage );
				}
			}
		}

		private void AddParametersToCommand(EventMessage logMessage, OleDbParameterCollection parameters)
		{
			bool contextLogging = false;
			bool diagnosticLogging = false;
			bool securityLogging = false;
			bool processLogging = false;
			string parametersList = string.Empty;

			// Add Standard logging fields.
			parameters.Add(Constants.FieldEventType, logMessage.Event);
			parameters.Add(Constants.FieldSourceName, logMessage.Source);
			parameters.Add(Constants.FieldMethodName, logMessage.Method);
			parameters.Add(Constants.FieldMessage, logMessage.Message );
			parameters.Add(Constants.FieldDateTime, TypeXmlConvert.ToString(logMessage.Time));
			
			if ( logMessage.Parameters != null )
			{
				for (int index = 0; index < logMessage.Parameters.Count; ++ index)
				{
					parametersList += logMessage.Parameters[index].Name
						+ " = " + logMessage.Parameters[index].ToString() 
						+ ";";
				}
				if (parametersList.Trim().Length != 0)
					parametersList = parametersList.Remove( parametersList.Length -1, 1);
				parameters.Add(Constants.FieldParameters, parametersList);
			}
			else
			{
				parameters.Add(Constants.FieldParameters, parametersList);
			}
			

/*			// Add context logging fields, if exist
			if ( logMessage.EventDetails.Contains( Constants.EventDetails.Context ) )
			{
				ContextEventDetail ctxEventDetail = (ContextEventDetail)
					(logMessage.EventDetails.GetEventDetail( Constants.EventDetails.Context ));
				contextLogging = true;
				parameters.Add( Constants.FieldUserID, Constants.DefaultString);
				parameters.Add( Constants.FieldUserName, Constants.DefaultString);
				parameters.Add( Constants.FieldContextID, Guid.Empty );
				
			}
			else
			{
				parameters.Add( Constants.FieldUserID, Constants.DefaultString);
				parameters.Add( Constants.FieldUserName, Constants.DefaultString);
				parameters.Add( Constants.FieldContextID, Guid.Empty);
				contextLogging = false;
			}

			// Add Diagnostics logging fields, if exist
			if (logMessage.EventDetails.Contains(Constants.EventDetails.Diagnostic))
			{
				DiagnosticEventDetail diagEventDetail = ( (DiagnosticEventDetail)
					(logMessage.EventDetails.GetEventDetail(Constants.EventDetails.Diagnostic) ) );
				parameters.Add( Constants.FieldStackTrace, diagEventDetail.StackTrace );
				diagnosticLogging = true;
			}
			else
			{
				parameters.Add( Constants.FieldStackTrace, Constants.DefaultString );
				diagnosticLogging = false;
			}

			// Add Diagnostics logging fields, if exist
			if ( logMessage.EventDetails.Contains(Constants.EventDetails.Process) )
			{
				ProcessEventDetail processEventDetail = ((ProcessEventDetail)
					(logMessage.EventDetails.GetEventDetail(Constants.EventDetails.Process)));
				parameters.Add(Constants.FieldProcessID, processEventDetail.ProcessId);
				parameters.Add(Constants.FieldThreadID, processEventDetail.ThreadId);
				parameters.Add(Constants.FieldThreadSequence, processEventDetail.ThreadSequence);
				parameters.Add(Constants.FieldProcessName, processEventDetail.ProcessName);
				parameters.Add(Constants.FieldMachineName, processEventDetail.MachineName);
				processLogging = true;
			}
			else
			{
				parameters.Add(Constants.FieldProcessID, Constants.DefaultInt);
				parameters.Add(Constants.FieldThreadID, Constants.DefaultInt);
				parameters.Add(Constants.FieldThreadSequence, Constants.DefaultInt);
				parameters.Add(Constants.FieldProcessName, Constants.DefaultString);
				parameters.Add(Constants.FieldMachineName, Constants.DefaultString);
				processLogging = false;
			}

			// Add security logging fields, if exist
			if ( logMessage.EventDetails.Contains( Constants.EventDetails.Security ))
			{
				SecurityEventDetail securityEventDetail = ( (SecurityEventDetail)
					( logMessage.EventDetails.GetEventDetail(Constants.EventDetails.Security) ));
				
				parameters.Add(Constants.FieldThreadUser, securityEventDetail.UserName );
				parameters.Add(Constants.FieldProcessUser, securityEventDetail.ProcessUserName );
				parameters.Add(Constants.FieldAuthenticationType, securityEventDetail.AuthenticationType);
				parameters.Add(Constants.FieldIsAuthenticated, securityEventDetail.IsAuthenticated);
				securityLogging = true;
			}
			else
			{
				parameters.Add(Constants.FieldThreadUser, Constants.DefaultString);
				parameters.Add(Constants.FieldProcessUser, Constants.DefaultString);
				parameters.Add(Constants.FieldAuthenticationType, Constants.DefaultString);
				parameters.Add(Constants.FieldIsAuthenticated, Constants.DefaultBool);
				securityLogging = false;
			}
			
			parameters.Add( Constants.FieldBitContextIndicator, contextLogging );
			parameters.Add( Constants.FieldBitDiagnosticIndicator, diagnosticLogging );
			parameters.Add( Constants.FieldBitSecurityIndicator, securityLogging );
			parameters.Add( Constants.FieldBitSecurityProcess, processLogging );
		}
	}
*/
}
