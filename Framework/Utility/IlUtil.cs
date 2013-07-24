using System.Collections;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Threading;

namespace LinkMe.Framework.Utility
{
	/// <summary>
	/// Provides static methods for working with MSIL code.
	/// </summary>
	public sealed class IlUtil
	{
		#region Nested types

		private class TextReaderHelper
		{
			private TextReader m_reader;
			private string m_value;

			internal TextReaderHelper(TextReader reader)
			{
				m_reader = reader;
			}

			internal string Value
			{
				get { return m_value; }
			}

			internal void ReadToEnd()
			{
				m_value = m_reader.ReadToEnd();
			}
		}

		#endregion

		private static readonly string m_ildasmPath;
		private static readonly string m_peVerifyPath;
		private static readonly string m_ilasmPath;

		static IlUtil()
		{
			// Try to find all the tools, but don't immediately fail if they are not found.

			try
			{
				string sdkRoot = BuildUtil.SdkInstallRoot;
				m_ildasmPath = Path.Combine(sdkRoot, "Bin\\ildasm.exe");
				m_peVerifyPath = Path.Combine(sdkRoot, "Bin\\peverify.exe");

				if (!File.Exists(m_ildasmPath))
				{
					m_ildasmPath = null;
				}
				if (!File.Exists(m_peVerifyPath))
				{
					m_peVerifyPath = null;
				}
			}
			catch
			{
				m_ildasmPath = null;
				m_peVerifyPath = null;
			}

			try
			{
				m_ilasmPath = Path.Combine(System.Runtime.InteropServices.RuntimeEnvironment.GetRuntimeDirectory(),
					"ilasm.exe");

				if (!File.Exists(m_ilasmPath))
				{
					m_ilasmPath = null;
				}
			}
			catch
			{
				m_ilasmPath = null;
			}
		}

		private IlUtil()
		{
		}

		/// <summary>
		/// Diassembles the specified .NET assembly to MSIL source code.
		/// </summary>
		/// <param name="assemblyPath">Path of the assembly to diassemble.</param>
		/// <param name="win32ResourceFile">On output, the path of the Win32 resource file extracted
		/// from the assembly -or- null if the assembly did not contain any Win32 resources.</param>
		/// <param name="managedResourceFiles">On output, the paths of all managed resource files extracted
		/// from the assembly -or- null if the assembly did not contain any managed resources.</param>
		/// <returns>Path of the MSIL source file containg the diassembly.</returns>
		public static string Disassemble(string assemblyPath, out string win32ResourceFile,
			out string[] managedResourceFiles)
		{
			const string win32ResFilePrefix = "// WARNING: Created Win32 resource file ";
			const string managedResFilePrefix = "// WARNING: managed resource file ";
			const int maxIldasmTime = 60000; // 1 minute should be enough.

			if (m_ildasmPath == null)
			{
				throw new System.ApplicationException("The ILDASM tool could not be found. This may mean"
					+ " the .NET Framework 1.1 SDK is not installed properly.");
			}

			if (assemblyPath == null)
				throw new System.ArgumentNullException("assemblyPath");
			if (!File.Exists(assemblyPath))
			{
				throw new FileNotFoundException("The assembly to be disassembled, '" + assemblyPath
					+ "', does not exist.", assemblyPath);
			}

			win32ResourceFile = null;

			// Build the command line.

			string ilPath = Path.ChangeExtension(assemblyPath, ".il");
			string arguments = string.Format("/out=\"{0}\" /source /linenum /quoteallnames /nobar \"{1}\"",
				ilPath, assemblyPath);

			// Run the ILDASM process.

			ProcessStartInfo startInfo = new ProcessStartInfo(m_ildasmPath, arguments);
			startInfo.CreateNoWindow = true;
			startInfo.UseShellExecute = false;
			startInfo.RedirectStandardError = true;

			try
			{
				Process ildasm = Process.Start(startInfo);

				// Parse the output for the names of resource files. For some reason these are sent to standard
				// error, not standard output. There should be at most one Win32 resource file, but there may be
				// any number of managed resource files.

				ArrayList managedResources = new ArrayList();

				string line;
				while ((line = ildasm.StandardError.ReadLine()) != null)
				{
					int index = line.IndexOf(managedResFilePrefix);
					if (index != -1)
					{
						// The resource file name should be quoted (/quoteallnames switch).

						Debug.Assert(line[index + managedResFilePrefix.Length] == '\'',
							"line[index + managedResFilePrefix.Length] == '\''");
						int startIndex = index + managedResFilePrefix.Length + 1;
						int endIndex = line.IndexOf('\'', startIndex);
						Debug.Assert(endIndex != -1, "endIndex != -1");

						managedResources.Add(line.Substring(startIndex, endIndex - startIndex));
					}
					else
					{
						index = line.IndexOf(win32ResFilePrefix);
						if (index != -1)
						{
							Debug.Assert(win32ResourceFile == null, "More than one Win32 resource file created.");

							// The name of the Win32 resource file extends to the end of the line
							// (spaces and all).

							win32ResourceFile = line.Substring(index + win32ResFilePrefix.Length);
						}
					}
				}

				managedResourceFiles = (managedResources.Count == 0 ? null : (string[])managedResources.ToArray(typeof(string)));

				// Wait for the process to exist - this must be after the standard error is read to avoid
				// a deadlock.

				if (!ildasm.WaitForExit(maxIldasmTime))
				{
					throw new System.ApplicationException("The ILDASM process failed to exit within " +
						maxIldasmTime.ToString() + " ms.");
				}

				if (ildasm.ExitCode != 0)
				{
					string stderr = ildasm.StandardError.ReadToEnd();
					throw new System.ApplicationException(string.Format("ILDASM returned exit code {0}."
						+ ". Standard error output:{1}{2}", ildasm.ExitCode, System.Environment.NewLine, stderr));
				}
			}
			catch (System.Exception ex)
			{
				throw new System.ApplicationException("Failed to disassemble file '" + assemblyPath + "' to IL.", ex);
			}

			return ilPath;
		}

		/// <summary>
		/// Assembles the specified MSIL source file to a .NET assembly.
		/// </summary>
		/// <param name="ilPath">Path of the MSIL source file to assemble.</param>
		/// <param name="assemblyPath">Path of the target assembly.</param>
		/// <param name="isExe">True to make the assembly an executable (EXE), false to make it a library (DLL).</param>
		/// <param name="win32ResourceFile">Path of the Win32 resource file to include in the assembly. May be null.</param>
		/// <param name="keyFile">The key file to be used to sign the assembly. May be null.</param>
		/// <remarks>The names of managed resource files to be included in the assembly do not need to be specified,
		/// because they are listed in the MSIL source file.</remarks>
		public static void Assemble(string ilPath, string assemblyPath, bool isExe, string win32ResourceFile,
			string keyFile)
		{
			const int maxIlasmTime = 60000; // 1 minute should be enough.

			if (m_ilasmPath == null)
				throw new System.ApplicationException("The ILASM tool could not be found.");

			if (ilPath == null)
				throw new System.ArgumentNullException("ilPath");
			if (assemblyPath == null)
				throw new System.ArgumentNullException("assemblyPath");
			if (!File.Exists(ilPath))
			{
				throw new FileNotFoundException("The IL source file to be assembled, '" + ilPath
					+ "', does not exist.", ilPath);
			}

			string ilDir = Path.GetDirectoryName(ilPath);

			string outputSwitch = (isExe ? "/exe" : "/dll");
			string resourceSwitch = string.Empty;
			if (win32ResourceFile != null && win32ResourceFile.Length > 0)
			{
				if (!FileSystem.IsAbsolutePath(win32ResourceFile))
				{
					win32ResourceFile = FileSystem.GetAbsolutePath(win32ResourceFile, ilDir);
				}
				if (!File.Exists(win32ResourceFile))
				{
					throw new FileNotFoundException("The Win32 resource file, '" + win32ResourceFile
						+ "', does not exist.", win32ResourceFile);
				}

				resourceSwitch = "/resource=\"" + win32ResourceFile + "\"";
			}

			string keyFileSwitch = string.Empty;
			if (keyFile != null && keyFile.Length > 0)
			{
				string fullKeyPath;
				if (FileSystem.IsAbsolutePath(keyFile))
				{
					fullKeyPath = keyFile;
				}
				else
				{
					// Try to find the key file relative to the assembly directory.
					
					fullKeyPath = FileSystem.GetAbsolutePath(keyFile, ilDir);
					if (!File.Exists(fullKeyPath))
					{
						// Bit of a hack: go up one level less than the key path specifies. This may work if
						// the output directory was one level higher than the temporary object directory when the
						// assembly was originally built (which is often true).

						fullKeyPath = FileSystem.GetAbsolutePath(keyFile, ilPath);
					}
				}

				if (!File.Exists(fullKeyPath))
				{
					throw new FileNotFoundException("The key file, '" + keyFile
						+ "', does not exist.", win32ResourceFile);
				}

				keyFileSwitch = "/key=\"" + fullKeyPath + "\"";
			}

			string arguments = string.Format("{0} /nologo /quiet /debug {1} {2} /output=\"{3}\" \"{4}\"",
				outputSwitch, resourceSwitch, keyFileSwitch, assemblyPath, ilPath);

			ProcessStartInfo startInfo = new ProcessStartInfo(m_ilasmPath, arguments);
			startInfo.CreateNoWindow = true;
			startInfo.UseShellExecute = false;
			startInfo.RedirectStandardOutput = true;
			startInfo.RedirectStandardError = true;

			try
			{
				Process ilasm = Process.Start(startInfo);

				// ILAsm reports some errors in stdout and others in stderr! Include both in the exception
				// message.

				// Read standard error on a separate thread from standard output, otherwise a deadlock
				// sometimes occurs. See ms-help://MS.VSCC.2003/MS.MSDNQTR.2005JUL.1033/cpref/html/frlrfSystemDiagnosticsProcessStartInfoClassRedirectStandardOutputTopic.htm

				TextReaderHelper helper = new TextReaderHelper(ilasm.StandardError);
				Thread helperThread = new Thread(new ThreadStart(helper.ReadToEnd));
				helperThread.Start();

				string stdout = ilasm.StandardOutput.ReadToEnd();

				if (!helperThread.Join(maxIlasmTime))
				{
					helperThread.Abort();
				}

				string stderr = helper.Value;

				// Wait for the process to exist - this must be after the standard error is read to avoid
				// a deadlock.

				if (!ilasm.WaitForExit(maxIlasmTime))
				{
					throw new System.ApplicationException("The ILASM process failed to exit within " +
						maxIlasmTime.ToString() + " ms.");
				}

				if (ilasm.ExitCode != 0)
				{
					throw new System.ApplicationException(string.Format("ILASM returned exit code {0}:{1}{2}{1}{3}",
						ilasm.ExitCode, System.Environment.NewLine, stdout, stderr));
				}
			}
			catch (System.Exception ex)
			{
				throw new System.ApplicationException(string.Format("Failed to assemble IL file '{0}' to assembly '{1}'.",
					ilPath, assemblyPath), ex);
			}
		}

		/// <summary>
		/// Calls the PEVerify tool to verify the specified assembly.
		/// </summary>
		/// <param name="assemblyPath">Path of the assembly to verify.</param>
		/// <param name="ignoreErrors">Array of PEVerify error codes to ignore. May be null.</param>
		/// <param name="errors">The PEverify output if verification fails, null if it succeeds.</param>
		/// <returns>True if verification succeeds, false if it fails.</returns>
		public static bool VerifyAssembly(string assemblyPath, uint[] ignoreErrors, out string errors)
		{
			const int maxErrors = 10;
			const int maxPeVerifyTime = 30000; // 30 seconds - this tool seems to be quite fast.

			if (m_peVerifyPath == null)
			{
				throw new System.ApplicationException("The PEVerify tool could not be found. This may mean"
					+ " the .NET Framework 1.1 SDK is not installed properly.");
			}

			if (assemblyPath == null)
				throw new System.ArgumentNullException("assemblyPath");
			if (!File.Exists(assemblyPath))
			{
				throw new FileNotFoundException("The assembly to be verified, '" + assemblyPath
					+ "', does not exist.", assemblyPath);
			}

			string arguments = string.Format("\"{0}\" /unique /hresult /break={1}", assemblyPath, maxErrors);

			// Append the error codes to ignore, if any.

			if (ignoreErrors != null && ignoreErrors.Length > 0)
			{
				StringBuilder sb = new StringBuilder(arguments);
				sb.Append(" /ignore=");
				sb.Append(ignoreErrors[0].ToString("x"));

				for (int index = 1; index < ignoreErrors.Length; index++)
				{
					sb.Append(',');
					sb.Append(ignoreErrors[index].ToString("x"));
				}

				arguments = sb.ToString();
			}

			// Run PEVerify.exe.

			ProcessStartInfo startInfo = new ProcessStartInfo(m_peVerifyPath, arguments);
			startInfo.CreateNoWindow = true;
			startInfo.UseShellExecute = false;
			startInfo.RedirectStandardOutput = true;

			Process peverify = Process.Start(startInfo);

			string output = peverify.StandardOutput.ReadToEnd();

			// Wait for the process to exist - this must be after the standard error is read to avoid
			// a deadlock.

			if (!peverify.WaitForExit(maxPeVerifyTime))
			{
				throw new System.ApplicationException("The PEVerify process failed to exit within " +
					maxPeVerifyTime.ToString() + " ms.");
			}

			if (peverify.ExitCode == 0)
			{
				errors = null;
				return true;
			}
			else
			{
				errors = output;
				return false;
			}
		}
	}
}
