using System.CodeDom;
using System.CodeDom.Compiler;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Text;

namespace LinkMe.Framework.Utility.Net
{
	[System.AttributeUsage(System.AttributeTargets.Assembly)]
	public class GeneratedAssemblyAttribute
		:	System.Attribute
	{
	}

	public class NetWriteAdaptor
	{
		public NetWriteAdaptor(AssemblyInfo assemblyInfo)
		{
			m_assemblyInfo = assemblyInfo;
			m_unit = new CodeCompileUnit();

			// Attach the appropriate attributes if needed.

			if ( m_assemblyInfo.KeyFile != null && m_assemblyInfo.KeyFile.Length != 0 )
			{
				// DelaySign

				m_unit.AssemblyCustomAttributes.Add(
					new CodeAttributeDeclaration(
					typeof(AssemblyDelaySignAttribute).FullName,
					new CodeAttributeArgument(new CodePrimitiveExpression(false))));

				// KeyFile

				m_unit.AssemblyCustomAttributes.Add(
					new CodeAttributeDeclaration(
					typeof(AssemblyKeyFileAttribute).FullName,
					new CodeAttributeArgument(new CodePrimitiveExpression(m_assemblyInfo.KeyFile))));

				// KeyName

				m_unit.AssemblyCustomAttributes.Add(
					new CodeAttributeDeclaration(
					typeof(AssemblyKeyNameAttribute).FullName,
					new CodeAttributeArgument(new CodePrimitiveExpression(string.Empty))));
			}

			if ( m_assemblyInfo.Version != null )
			{
				WriteVersionAttribute();
			}

			// Indicate that this is a generated file.

			AddReference(typeof(GeneratedAssemblyAttribute));
			m_unit.AssemblyCustomAttributes.Add(
				new CodeAttributeDeclaration(typeof(GeneratedAssemblyAttribute).FullName));
		}

		private static bool ContainsType(CodeNamespace ns, string fullName)
		{
			foreach ( CodeTypeDeclaration type in ns.Types )
			{
				if ( ns.Name + "." + type.Name == fullName )
					return true;
			}

			return false;
		}

		public AssemblyInfo AssemblyInfo
		{
			get { return m_assemblyInfo.Clone(); }
		}

		public void Close()
		{
			// Write the code to a source file and then compile that source file instead of compiling the DOM.
			// This way we still have the source file (with a descriptive name) for debugging purposes.

			WriteAssembly(WriteCode());
		}

		public void SetVersion(System.Version version)
		{
			if ( version != null )
			{
				// Check to see whether this has already been set.

				if ( m_assemblyInfo.Version != null )
				{
					if ( m_assemblyInfo.Version != version )
					{
						throw new System.ApplicationException(string.Format("The version cannot be set to '{0}',"
							+ ", because it has already been set to '{1}'.", version, m_assemblyInfo.Version));
					}
				}
				else
				{
					// Update the information.

					m_assemblyInfo.Version = version;

					// Add the attribute to the assembly.

					WriteVersionAttribute();
				}
			}
		}

		public void SetCodeProvider(string fileExtension)
		{
			fileExtension = fileExtension.ToLower();

			// Check to see whether this has already been set.

			if ( m_fileExtension != null )
			{
				if ( m_fileExtension != fileExtension )
				{
					throw new System.ApplicationException(string.Format("The code provider extension cannot be"
						+ " set to '{0}', because it has already been set to '{1}'.", fileExtension, m_fileExtension));
				}
			}
			else
			{
				if ( fileExtension == "cs" || fileExtension == "vb" )
					m_fileExtension = fileExtension;
				else
					throw new System.ArgumentException(string.Format("The code provider with the '{0}' file extension does not exist.", fileExtension));
			}
		}

		public void AddReference(string assemblyPath)
		{
			if ( assemblyPath != m_assemblyInfo.Name && !m_unit.ReferencedAssemblies.Contains(assemblyPath) )
			{
				m_unit.ReferencedAssemblies.Add(assemblyPath);
			}
		}

		public void AddReference(System.Type type)
		{
			AddReference(type.Assembly.Location);
		}

		public bool ContainsType(string fullName)
		{
			foreach ( CodeNamespace ns in m_unit.Namespaces )
			{
				if ( ContainsType(ns, fullName) )
					return true;
			}

			return false;
		}

		public void WriteStartNamespace(string fullName)
		{
			m_currentNamespace = new CodeNamespace(fullName);
			m_unit.Namespaces.Add(m_currentNamespace);
		}
		
		public void WriteEndNamespace()
		{
			m_currentNamespace = null;
		}

		public void WriteStartInterface(string name)
		{
			m_currentType = new CodeTypeDeclaration(name);
			m_currentType.IsInterface = true;
			m_currentNamespace.Types.Add(m_currentType);
		}

		public void WriteEndInterface()
		{
			m_currentType = null;
		}

		public void WriteStartClass(string name)
		{
			m_currentType = new CodeTypeDeclaration(name);
			m_currentType.IsClass = true;
			m_currentNamespace.Types.Add(m_currentType);
		}

		public void WriteStartClass(string name, TypeAttributes scope)
		{
			WriteStartClass(name);
			m_currentType.TypeAttributes = scope;
		}

		public void WriteEndClass()
		{
			m_currentType = null;
		}

		public void WriteClassAttribute(System.Type attributeType)
		{
			m_currentType.CustomAttributes.Add(new CodeAttributeDeclaration(attributeType.FullName));
		}

		public void WriteClassAttribute(System.Type attributeType, params object[] arguments)
		{
			if ( arguments == null || arguments.Length == 0 )
			{
				m_currentType.CustomAttributes.Add(new CodeAttributeDeclaration(attributeType.FullName));
			}
			else
			{
				CodeAttributeArgument[] attributeArguments = new CodeAttributeArgument[arguments.Length];
				for ( int index = 0; index < arguments.Length; ++index )
					attributeArguments[index] = new CodeAttributeArgument(new CodePrimitiveExpression(arguments[index]));
				m_currentType.CustomAttributes.Add(new CodeAttributeDeclaration(attributeType.FullName, attributeArguments));
			}
		}

		public CodeMemberProperty WriteProperty(string type, string name, bool hasGet, bool hasSet)
		{
			CodeMemberProperty property = new CodeMemberProperty();
			property.Name = name;
			property.Type = new CodeTypeReference(type);
			property.HasGet = hasGet;
			property.HasSet = hasSet;

			m_currentType.Members.Add(property);
			return property;
		}

		public CodeMemberProperty WriteProperty(string type, string name, MemberAttributes attributes, bool hasGet, bool hasSet)
		{
			CodeMemberProperty property = new CodeMemberProperty();
			property.Name = name;
			property.Type = new CodeTypeReference(type);
			property.HasGet = hasGet;
			property.HasSet = hasSet;
			property.Attributes = attributes;

			m_currentType.Members.Add(property);
			return property;
		}

		public CodeMemberProperty WriteProperty(string type, string name, bool hasGet, bool hasSet, out CodeStatementCollection getStatements, out CodeStatementCollection setStatements)
		{
			CodeMemberProperty property = new CodeMemberProperty();
			property.Name = name;
			property.Type = new CodeTypeReference(type);
			property.HasGet = hasGet;
			property.HasSet = hasSet;
			property.Attributes = MemberAttributes.Public;
			getStatements = property.GetStatements;
			setStatements = property.SetStatements;

			m_currentType.Members.Add(property);
			return property;
		}

		public CodeMemberProperty WriteProperty(string type, string name, string interfaceName, bool hasGet, bool hasSet, out CodeStatementCollection getStatements, out CodeStatementCollection setStatements)
		{
			CodeMemberProperty property = new CodeMemberProperty();
			property.Name = name;
			property.Type = new CodeTypeReference(type);
			property.HasGet = hasGet;
			property.HasSet = hasSet;
			if (interfaceName != null)
				property.PrivateImplementationType = new CodeTypeReference(interfaceName);
			else
				property.Attributes = MemberAttributes.Public;
			getStatements = property.GetStatements;
			setStatements = property.SetStatements;

			m_currentType.Members.Add(property);
			return property;
		}

		public CodeMemberProperty WriteProperty(string type, string name, string interfaceName, bool hasGet, bool hasSet)
		{
			CodeMemberProperty property = new CodeMemberProperty();
			property.Name = name;
			property.Type = new CodeTypeReference(type);
			property.HasGet = hasGet;
			property.HasSet = hasSet;
			if (interfaceName != null)
				property.PrivateImplementationType = new CodeTypeReference(interfaceName);

			m_currentType.Members.Add(property);
			return property;
		}

		public void WriteImplementedInterface(string name)
		{
			m_currentType.BaseTypes.Add(name);
		}

		public void WriteBaseClass(string name)
		{
			m_currentType.BaseTypes.Add(name);
		}

		public void WriteBaseClass(System.Type type)
		{
			m_currentType.BaseTypes.Add(type);
		}

		public void WriteStaticField(string type, string name)
		{
			CodeMemberField field = new CodeMemberField(type, name);
			field.Attributes = MemberAttributes.Private | MemberAttributes.Static;
			m_currentType.Members.Add(field);
		}

		public void WriteStaticField(string type, string name, CodeExpression initExpression)
		{
			CodeMemberField field = new CodeMemberField(type, name);
			field.Attributes = MemberAttributes.Private | MemberAttributes.Static;
			if ( initExpression != null )
				field.InitExpression = initExpression;

			m_currentType.Members.Add(field);
		}

		public void WritePublicStaticField(string type, string name, string initExpression)
		{
			CodeMemberField field = new CodeMemberField(type, name);
			field.Attributes = MemberAttributes.Public | MemberAttributes.Static;
			field.InitExpression = new CodeSnippetExpression(initExpression);
			m_currentType.Members.Add(field);
		}

		public void WritePublicStaticField(string type, string name, CodeExpression initExpression)
		{
			CodeMemberField field = new CodeMemberField(type, name);
			field.Attributes = MemberAttributes.Public | MemberAttributes.Static;
			if ( initExpression != null )
				field.InitExpression = initExpression;
			m_currentType.Members.Add(field);
		}

		public void WriteInternalStaticField(string type, string name, CodeExpression initExpression)
		{
			CodeMemberField field = new CodeMemberField(type, name);
			field.Attributes = MemberAttributes.Assembly | MemberAttributes.Static;
			if ( initExpression != null )
				field.InitExpression = initExpression;
			m_currentType.Members.Add(field);
		}

		public void WriteConstantStaticField(string type, string name, object initValue)
		{
			CodeMemberField field = new CodeMemberField(type, name);
			field.Attributes = MemberAttributes.Public | MemberAttributes.Const;
			if ( initValue != null )
				field.InitExpression = new CodePrimitiveExpression(initValue);

			m_currentType.Members.Add(field);
		}

		public CodeConstructor WriteConstructor(MemberAttributes attributes)
		{
			CodeConstructor constructor = new CodeConstructor();
			constructor.Attributes = attributes;
			m_currentType.Members.Add(constructor);
			return constructor;
		}

		public CodeConstructor WriteConstructor()
		{
			return WriteConstructor(MemberAttributes.Public);
		}

		public CodeStatementCollection WriteStaticConstructor()
		{
			CodeTypeConstructor constructor = new CodeTypeConstructor();
			constructor.Attributes = MemberAttributes.Public | MemberAttributes.Static;
			m_currentType.Members.Add(constructor);
			return constructor.Statements;
		}

		public CodeMemberMethod WriteMethod(MemberAttributes attributes, string returnType, string name)
		{
			CodeMemberMethod method = new CodeMemberMethod();
			method.Attributes = attributes;
			if ( returnType != null && returnType != "void" )
				method.ReturnType = new CodeTypeReference(returnType);
			method.Name = name;
			m_currentType.Members.Add(method);
			return method;
		}

		public CodeMemberMethod WriteMethod(MemberAttributes attributes, System.Type returnType, string name)
		{
			CodeMemberMethod method = new CodeMemberMethod();
			method.Attributes = attributes;
			if ( returnType != null )
				method.ReturnType = new CodeTypeReference(returnType);
			method.Name = name;
			m_currentType.Members.Add(method);
			return method;
		}

		public CodeMemberMethod WriteMethod(MemberAttributes attributes, string name)
		{
			CodeMemberMethod method = new CodeMemberMethod();
			method.Attributes = attributes;
			method.Name = name;
			m_currentType.Members.Add(method);
			return method;
		}

		public CodeMemberMethod WritePublicMethod(string returnType, string name)
		{
			return WriteMethod(MemberAttributes.Public, returnType, name);
		}

		public CodeMemberMethod WritePublicMethod(System.Type returnType, string name)
		{
			return WriteMethod(MemberAttributes.Public, returnType, name);
		}

		public CodeMemberMethod WritePublicMethod(string name)
		{
			return WriteMethod(MemberAttributes.Public, name);
		}

		private CodeDomProvider CreateCodeProvider()
		{
			Debug.Assert(m_fileExtension == null || m_fileExtension == "cs" || m_fileExtension == "vb");

			if ( m_fileExtension == null || m_fileExtension == "cs" )
				return new Microsoft.CSharp.CSharpCodeProvider();
			else
				return new Microsoft.VisualBasic.VBCodeProvider();
		}

		private string WriteCode()
		{
			// Create the provider and writer.
			
			CodeDomProvider provider = CreateCodeProvider();

			// Create the generator.

//			ICodeGenerator generator = provider.CreateGenerator();
			CodeGeneratorOptions options = new CodeGeneratorOptions();
			options.BracingStyle = "C";
			options.IndentString = "    ";

			// Save to the file.

			string filePath = m_assemblyInfo.Name + "." + provider.FileExtension;
			using (StreamWriter writer = new StreamWriter(filePath))
			{
				provider.GenerateCodeFromCompileUnit(m_unit, writer, options);
			}

			return filePath;
		}

		private void WriteAssembly(string sourceFilePath)
		{
			// Create the provider.

			CodeDomProvider provider = CreateCodeProvider();
			
			// Create the assembly.

//			ICodeCompiler compiler = provider.CreateCompiler();
			CompilerParameters parameters = new CompilerParameters();
			parameters.GenerateExecutable = false;
			parameters.GenerateInMemory = false;
			parameters.OutputAssembly = m_assemblyInfo.Name;

			// Add all the referenced assemblies to the compiler options (since we're not passing m_unit to the compiler).

			foreach (string reference in m_unit.ReferencedAssemblies)
			{
				parameters.ReferencedAssemblies.Add(reference);
			}

			// The IncludeDebugInformation flag does more than it says - it also disables optimization and defines
			// the "DEBUG" compile time constant. Only do this in the Debug build. In Release build manually
			// add the "/debug+" compiler argument (the same for both csc.exe and vbc.exe), so that a PDB file
			// is generated, but optimization is still enabled.

#if DEBUG
			parameters.IncludeDebugInformation = true;
#else
			Trace.Assert(provider is Microsoft.CSharp.CSharpCodeProvider || provider is Microsoft.VisualBasic.VBCodeProvider,
				"Unexpected type of code provider: " + provider.GetType().FullName);
			parameters.IncludeDebugInformation = false;
			parameters.CompilerOptions = "/debug+";
#endif

			string tempPath = Path.Combine(Path.GetDirectoryName(m_assemblyInfo.Name), "Source");

			// Make sure the path is there.

			bool createDir = !Directory.Exists(tempPath);
			if ( createDir )
				Directory.CreateDirectory(tempPath);

			parameters.TempFiles = new System.CodeDom.Compiler.TempFileCollection(tempPath, true);
			parameters.TempFiles.KeepFiles = false;

			try
			{
				CompilerResults results = provider.CompileAssemblyFromFile(parameters, sourceFilePath);
				if ( results.Errors.HasErrors )
				{
					StringBuilder sb = new StringBuilder(System.Environment.NewLine);
					foreach ( CompilerError error in results.Errors )
					{
						sb.Append(error.ErrorText);
						sb.Append(System.Environment.NewLine);
					}
					throw new System.ApplicationException("An attempt to compile the '" + m_assemblyInfo.Name
						+ "' assembly has failed with the following errors: " + sb.ToString());
				}
			}
			finally
			{
				// Delete the temporary directory, if we created it.

				DirectoryInfo dirInfo = new DirectoryInfo(tempPath);
				if ( createDir && dirInfo.GetFiles().Length == 0 && dirInfo.GetDirectories().Length == 0 )
				{
					dirInfo.Delete();
				}
			}
		}

		private void WriteVersionAttribute()
		{
			m_unit.AssemblyCustomAttributes.Add(
				new CodeAttributeDeclaration(
				typeof(AssemblyVersionAttribute).FullName,
				new CodeAttributeArgument(new CodePrimitiveExpression(m_assemblyInfo.Version.ToString()))));
		}

		private AssemblyInfo m_assemblyInfo;
		private CodeCompileUnit m_unit;
		private CodeNamespace m_currentNamespace;
		private CodeTypeDeclaration m_currentType;
		private string m_fileExtension;
	}
}
