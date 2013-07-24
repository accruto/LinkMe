using System;
using System.Runtime;
using System.Reflection;
using System.Collections;
using System.Runtime.InteropServices;
using Microsoft.Win32;

namespace LinkMe.Framework.Tools.Mmc
{
	class SnapinRegistrar
	{
		public static void RegisterAll()
		{
			foreach ( Assembly assembly in AppDomain.CurrentDomain.GetAssemblies() )
				RegisterAssembly(assembly);
		}

		public static void RegisterAssembly(Assembly assembly)
		{
			SnapinRegistrar registrar = new SnapinRegistrar();
			registrar.Register(assembly);
		}
			
		public static void UnRegisterAll()
		{
			foreach ( Assembly assembly in AppDomain.CurrentDomain.GetAssemblies() )
				UnRegisterAssembly(assembly);
		}

		public static void UnRegisterAssembly(Assembly assembly)
		{
			SnapinRegistrar registrar = new SnapinRegistrar();
			registrar.UnRegister(assembly);
		}

		private void Register(Assembly assembly)
		{
			foreach ( System.Type type in assembly.GetTypes() )
			{
				if ( Marshal.IsTypeVisibleFromCom(type) && type.IsClass && (type.IsPublic || type.IsNestedPublic) && !type.IsImport )
					FindAttributes(type);
			}

			PrepareAttributes();
			Register();
		}

		private void UnRegister(Assembly assembly)
		{
			foreach ( System.Type type in assembly.GetTypes() )
			{
				if ( Marshal.IsTypeVisibleFromCom(type) && type.IsClass && (type.IsPublic || type.IsNestedPublic) && !type.IsImport )
					FindAttributes(type);
			}

			PrepareAttributes();
			UnRegister();
		}

		void FindAttributes(System.Type type)
		{
			// Get Guid and ProgId.

			string clsid = "{" + Marshal.GenerateGuidForType(type).ToString().ToUpper() + "}";

			// Look for a Snapin.

			object [] attributes = type.GetCustomAttributes(typeof(SnapinInfoAttribute), true);
			if ( attributes.Length != 0 )
			{
				SnapinInfoAttribute attribute = attributes[0] as SnapinInfoAttribute;
				if ( attribute != null )
				{
					SnapinRegistrationInfo info = new SnapinRegistrationInfo();
					info.Attribute = attribute;
					info.Guid = clsid.ToUpper();
					info.SnapinType = type;
					m_infos.Add(info);
				}
			}

			// Look for a Snapin About.

			attributes = type.GetCustomAttributes(typeof(AboutSnapinAttribute), true);
			if ( attributes.Length != 0 )
			{
				AboutSnapinAttribute attribute = attributes[0] as AboutSnapinAttribute;
				if ( attribute != null )
				{
					SnapinAboutRegistrationInfo info = new SnapinAboutRegistrationInfo();
					info.Attribute = attribute;
					info.Guid = clsid.ToUpper();
					m_aboutInfos.Add(attribute.SnapinType.ToString(), info);
				}
			}
		}

		void PrepareAttributes()
		{
			// Match up the snapin with the snapin abouts.

			foreach ( SnapinRegistrationInfo info in m_infos )
			{            
				SnapinAboutRegistrationInfo aboutInfo = m_aboutInfos[info.SnapinType.ToString()] as SnapinAboutRegistrationInfo;
				if ( aboutInfo == null )
					throw new ApplicationException("Failed to locate a Snapin About object for the snapin: " + info.SnapinType.ToString());
				info.AboutRegistrationInfo = aboutInfo;
			}
		}

		void Register()
		{
			foreach ( SnapinRegistrationInfo info in m_infos )
			{
				// Create the entries in the registry.

				RegistryKey key = Registry.LocalMachine.CreateSubKey("Software\\Microsoft\\MMC\\Snapins");
				key = key.CreateSubKey(info.Guid);
				key.SetValue("NameString", info.Attribute.Name);
				key.SetValue("About", info.AboutRegistrationInfo.Guid);
				if ( info.Attribute.StandAlone )
					key.CreateSubKey("StandAlone");
			}
		}

		void UnRegister()
		{
			foreach ( SnapinRegistrationInfo info in m_infos )
			{        
				try
				{
					// Delete the registry key.

					RegistryKey key = Registry.LocalMachine.OpenSubKey("Software\\Microsoft\\MMC\\Snapins", true);
					key.DeleteSubKeyTree(info.Guid);
				}
				catch ( System.Exception )
				{
				}
			}
		}

		private ArrayList m_infos = new ArrayList();
		private Hashtable m_aboutInfos = new Hashtable();
	}

    internal class SnapinRegistrationInfo
    {
        public Type SnapinType;
        public string Guid;
        public SnapinInfoAttribute Attribute;
        public SnapinAboutRegistrationInfo AboutRegistrationInfo;
    }

    internal class SnapinAboutRegistrationInfo
    {
        public string Guid;
        public AboutSnapinAttribute Attribute;
    }
   
}
