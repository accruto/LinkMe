REM -------------- ExecuteScript -------------------

Function ExecuteScript (scriptFile)
	Set WshShell = WScript.CreateObject("WScript.Shell")
	
	CommandLine = "isql -U " & DatabaseUserId & " -P " & DatabasePassword & " -d " & DatabaseName & " -i " & scriptFile
	LogFile.WriteLine("CommandLine: '" & CommandLine & "'")
	
	Proceed = MsgBox ("CommandLine: '" & CommandLine & "'" & (Chr(13)& Chr(10)) & "Proceed?", VBYesNo, "Confirm") 
	If Proceed <> VBYes Then
		LogFile.WriteLine("Execution cancelled")
		Exit Function
	End If 

	LogFile.WriteLine("Execution proceeding")
	
	Set objExecObject = WshShell.Exec(CommandLine)
	Do While Not objExecObject.StdOut.AtEndOfStream
	    CommandOutput = CommandOutput & objExecObject.StdOut.Read(1)
	Loop
	
	message = "Command output: " & CommandOutput
	WScript.Echo message
	LogFile.WriteLine("+++ Begin Command Output +++")
	LogFile.WriteLine(CommandOutput)
	LogFile.WriteLine("+++ End Command Output +++")
	
	ExecuteScript = objExecObject.ExitCode
End Function

REM -------------- ExitScript -------------------

Sub ExitScript
	LogFile.Close
	WScript.Quit
End Sub

REM -------------- Main -------------------

Set args = Wscript.Arguments
If args.Named.Exists("d") Then
	DatabaseName = args.Named("d")
End If
If args.Named.Exists("U") Then
	DatabaseUserId = args.Named("U")
End If
If args.Named.Exists("P") Then
	DatabasePassword = args.Named("P")
End If
If args.Named.Exists("f") Then
    Dim arList, iCounter, strSentence
    strSentence = args.Named("f")
    scriptFiles = Split(strSentence, ",")
Else
	WScript.Echo("No Input files specified")
	ExitScript
End If
If args.Named.Exists("l") Then
	logfileName = args.Named("l")
Else
	logfileName = "install.log"
End If

Set LogFile = CreateObject("Scripting.FileSystemObject").CreateTextFile(logfileName, True)

DatabaseName = InputBox("Enter database name", "Database name", DatabaseName)
LogFile.WriteLine("Database name: '" & DatabaseName & "'")

DatabaseUserId = InputBox("Enter database user id", "Database user id", DatabaseUserId)
LogFile.WriteLine("Database user id: '" & DatabaseUserId & "'")

DatabasePassword = InputBox("Enter database password", "Database password", DatabasePassword)
LogFile.WriteLine("Database password: '" & DatabasePassword & "'")

For Each scriptFile in scriptFiles
	LogFile.WriteLine("Processing script: '" & scriptFile & "'")
	returnCode = ExecuteScript(scriptFile)
	If returnCode <> 0 Then
		message = "Script " & scriptFile & " failed with code " & returnCode & (Chr(13)& Chr(10)) & "Proceed?"
		LogFile.WriteLine(message)
		Proceed = MsgBox (message, VBYesNo, "Confirm") 
		If Proceed <> VBYes Then
			LogFile.WriteLine("Execution cancelled")
			ExitScript
		End If 
	End If
Next

ExitScript

