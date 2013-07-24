using System.IO;
using System.Runtime.InteropServices;
using System.Collections.Generic;
using LinkMe.Environment;
using Microsoft.VisualStudio.Project;
using Microsoft.VisualStudio.Shell.Interop;
using Microsoft.Build.BuildEngine;

namespace LinkMe.Framework.VisualStudio
{
	public abstract class LinkProjectNode
		: ProjectNode
	{
/*		/// <summary>
		/// Would like to treat files added to the project as "links".  However, the MPF does not support this.
		/// Therefore going to craft own implementation.  Unfortunately that means that these functions
		/// need to be overridden, which would not necessarily be considered because they are too "low-level", but
		/// there seems no other way to do this, i.e. there is not enough granularity or possiblity to override specifics
		/// in the base implementation.  To reduce impact when new versions of the SDK become available the methods
		/// in the base classes have been copied here with specific changes made.
		/// 
		/// If a new version of the SDK comes which still does not implement links then copy these methods again
		/// and make the changes.
		/// 
		/// If links are ever implemented in the SDK then delete the following methods completely and adjust appropriately.
		/// 
		/// Changes: The bits associated with the file system have been removed.
		/// </summary>
		public override int AddItemWithSpecific(uint itemIdLoc, VSADDITEMOPERATION op, string itemName, uint filesToOpen, string[] files, System.IntPtr dlgOwner, uint editorFlags, ref System.Guid editorType, string physicalView, ref System.Guid logicalView, VSADDRESULT[] result)
		{
			if ( files == null || result == null || files.Length == 0 || result.Length == 0 )
				return VSConstants.E_INVALIDARG;
			string[] actualFiles = new string[files.Length];

			// Locate the node to be the container node for the file(s) being added.
			HierarchyNode parentNode = NodeFromItemId(itemIdLoc);
			if ( parentNode == null )
				return VSConstants.E_INVALIDARG;

			while ( !(parentNode is ProjectNode) && !(parentNode is FolderNode) && (!CanFileNodesHaveChilds || !(parentNode is FileNode)) )
			{
				parentNode = parentNode.Parent;
			}

			// Get the directory for the node that is the parent of the item.

			string baseDirectory = GetBaseDirectoryForAddingFiles(parentNode);
			if ( string.IsNullOrEmpty(baseDirectory) )
				return VSConstants.E_FAIL;

			// This contains the path to the file as if it was directly part of the project.
			// These are required to ensure that the general project continues to recognise the files.

			List<string> projectPaths = new List<string>();
			for ( int index = 0; index < files.Length; index++ )
			{
				string projectPath = Path.Combine(baseDirectory, Path.GetFileName(files[index]));
				projectPaths.Add(projectPath);
			}

			// Ask tracker objects if we can add files.

			VSQUERYADDFILEFLAGS[] flags = GetQueryAddFileFlags(files);
			if ( !Tracker.CanAddItems(projectPaths.ToArray(), flags) )
				return VSConstants.E_FAIL;

			// Ensure that the project can be updated.

			if ( !ProjectMgr.QueryEditProjectFile(false) )
				throw Marshal.GetExceptionForHR(VSConstants.OLE_E_PROMPTSAVECANCELLED);

			// Add the files to the hierarchy.

			int actualFilesAddedIndex = 0;
			for ( int index = 0; index < projectPaths.Count; index++ )
			{
				result[0] = VSADDRESULT.ADDRESULT_Failure;
				string filePath = files[index];
				string projectPath = projectPaths[index];

				HierarchyNode child = FindChild(projectPath);
				if ( child != null )
				{
					// If the file to be added is an existing file part of the hierarchy then continue.

					if ( Microsoft.Build.BuildEngine.NativeMethods.IsSamePath(filePath, projectPath) )
					{
						result[0] = VSADDRESULT.ADDRESULT_Cancel;
						continue;
					}
					else
					{
						int canOverWriteExistingItem = CanOverwriteExistingItem(filePath, projectPath);
						if ( canOverWriteExistingItem == (int) OleConstants.OLECMDERR_E_CANCELED )
						{
							result[0] = VSADDRESULT.ADDRESULT_Cancel;
							return canOverWriteExistingItem;
						}
					}
				}

				// Add new node.

				AddNewFileNodeToHierarchy(parentNode, filePath);
				UpdateNewFileNode(FindChild(filePath), projectPath);
				result[0] = VSADDRESULT.ADDRESULT_Success;
				actualFiles[actualFilesAddedIndex++] = projectPath;
			}

			// Notify listeners that items were appended.

			if ( actualFilesAddedIndex > 0 )
				parentNode.OnItemsAppended(parentNode);

			// Open the files if required.

			if ( actualFiles.Length <= filesToOpen )
			{
				for ( int index = 0; index < filesToOpen; index++ )
				{
					string name = actualFiles[index];
					if ( !string.IsNullOrEmpty(name) )
					{
						HierarchyNode child = FindChild(name);
						if ( child != null )
						{
							IVsWindowFrame frame;
							if ( editorType == System.Guid.Empty )
							{
								System.Guid view = System.Guid.Empty;
								ErrorHandler.ThrowOnFailure(this.OpenItem(child.ID, ref view, System.IntPtr.Zero, out frame));
							}
							else
							{
								ErrorHandler.ThrowOnFailure(this.OpenItemWithSpecific(child.ID, editorFlags, ref editorType, physicalView, ref logicalView, System.IntPtr.Zero, out frame));
							}

							// Show the window frame in the UI and make it the active window.

							if ( frame != null )
								ErrorHandler.ThrowOnFailure(frame.Show());
						}
					}
				}
			}

			return VSConstants.S_OK;
		}
*/

		protected virtual void UpdateNewFileNode(HierarchyNode node, string projectPath)
		{
			// Update the properties of the build element.

			ProjectElement element = node.ItemNode;
			element.SetMetadata(Constants.Project.Item.SubType, null);
			element.SetMetadata(Constants.Project.Item.Link, FilePath.GetRelativePath(projectPath, BaseURI.AbsoluteUrl));
		}

		/// <summary>
		/// Changes: call to AddIndependentFileNode changed to AddAssembleFileNode.
		/// </summary>
		protected internal override void ProcessFiles()
		{
			List<string> subitemsKeys = new List<string>();
			Dictionary<string, BuildItem> subitems = new Dictionary<string, BuildItem>();

			// Define a set for our build items. The value does not really matter here.
			Dictionary<string, BuildItem> items = new Dictionary<string, BuildItem>();

			// Process Files
			BuildItemGroup projectFiles = BuildProject.EvaluatedItems;
			foreach ( BuildItem item in projectFiles )
			{
				// Ignore the item if it is a reference or folder
				if ( this.FilterItemTypeToBeAddedToHierarchy(item.Name) )
					continue;

				// MSBuilds tasks/targets can create items (such as object files),
				// such items are not part of the project per say, and should not be displayed.
				// so ignore those items.
				if ( !this.IsItemTypeFileType(item.Name) )
					continue;

				// If the item is already contained do nothing.
				// TODO: possibly report in the error list that the the item is already contained in the project file similar to Language projects.
				if ( items.ContainsKey(item.FinalItemSpec.ToUpperInvariant()) )
					continue;

				// Make sure that we do not want to add the item, dependent, or independent twice to the ui hierarchy
				items.Add(item.FinalItemSpec.ToUpperInvariant(), item);

				string dependentOf = item.GetMetadata(ProjectFileConstants.DependentUpon);

				if ( !this.CanFileNodesHaveChilds || string.IsNullOrEmpty(dependentOf) )
				{
					AddIndependentFileNode(item);
				}
				else
				{
					// We will process dependent items later.
					// Note that we use 2 lists as we want to remove elements from
					// the collection as we loop through it
					subitemsKeys.Add(item.FinalItemSpec);
					subitems.Add(item.FinalItemSpec, item);
				}
			}

			// Now process the dependent items.
			if ( this.CanFileNodesHaveChilds )
			{
				ProcessDependentFileNodes(subitemsKeys, subitems);
			}
		}

		/// <summary>
		/// A copy of AddIndependentFileNode with the call to GetItemParentNode changed
		/// to GetAssembleItemParentNode.
		/// </summary>
		private HierarchyNode AddIndependentFileNode(BuildItem item)
		{
			HierarchyNode currentParent = GetItemParentNode(item);
			return AddFileNodeToNode(item, currentParent);
		}

		/// <summary>
		/// A copy of GetItemParentNode where the path is determined from the Link property
		/// rather than the FinalItemSpec.
		private HierarchyNode GetItemParentNode(BuildItem item)
		{
			HierarchyNode currentParent = this;
			string strPath = item.GetMetadata("Link");

			strPath = Path.GetDirectoryName(strPath);
			if (strPath.Length > 0)
			{
				// Use the relative to verify the folders...
				currentParent = this.CreateFolderNodes(strPath);
			}
			return currentParent;
		}

		/// <summary>
		/// A direct copy of AddFileNodeToNode as that method is private in the base class.
		/// </summary>
		private HierarchyNode AddFileNodeToNode(BuildItem item, HierarchyNode parentNode)
		{
			FileNode node = this.CreateFileNode(new ProjectElement(this, item, false));
			parentNode.AddChild(node);
			return node;
		}

		/// <summary>
		/// Changes: the file may exist outside of the project folder structure so allow the
		/// full relative path creation.
		/// </summary>
		protected override ProjectElement AddFileToMsBuild(string file)
		{
			ProjectElement newItem;
			string itemPath = GetMsBuildItemPath(file);

			if ( this.IsCodeFile(itemPath) )
			{
				newItem = this.CreateMsBuildFileItem(itemPath, ProjectFileConstants.Compile);
				newItem.SetMetadata(ProjectFileConstants.SubType, ProjectFileAttributeValue.Code);
			}
			else if ( this.IsEmbeddedResource(itemPath) )
			{
				newItem = this.CreateMsBuildFileItem(itemPath, ProjectFileConstants.EmbeddedResource);
			}
			else
			{
				newItem = this.CreateMsBuildFileItem(itemPath, ProjectFileConstants.Content);
				newItem.SetMetadata(ProjectFileConstants.SubType, ProjectFileConstants.Content);
			}

			return newItem;
		}

		protected virtual string GetMsBuildItemPath(string file)
		{
			return FilePath.GetRelativePath(file, this.BaseURI.AbsoluteUrl);
		}

		protected override void InitializeProjectProperties()
		{
			string projectName = Path.GetFileNameWithoutExtension(FileName);
			if ( !string.IsNullOrEmpty(projectName) )
			{
				// Set project properties.

				if ( string.IsNullOrEmpty(GetProjectProperty(ProjectFileConstants.Name)) )
					SetProjectProperty(ProjectFileConstants.Name, projectName);
			}
		}

		public override int Save(string fileToBeSaved, int remember, uint formatIndex)
		{
			// Would like to add the targets import in InitializeProjectProperties but
			// using relative paths can cause issues when the project is created from the template
			// where the relative import path is evaluated against the template path rather
			// than the eventual project path.

			// If the file name here and the the file name on the build engine's project
			// are the same then fix up the import.

			if ( string.Compare(fileToBeSaved, BuildProject.FullFileName, true) == 0 )
			{
				string[] targetsFiles = GetProjectTargetsFiles();

				if ( targetsFiles != null )
				{
					foreach ( string targetsFile in targetsFiles )
					{
						// Look for the targets import.

						bool found = false;
						foreach ( Import import in BuildProject.Imports )
						{
							if ( string.Compare(Path.GetFileName(import.EvaluatedProjectPath), targetsFile, true) == 0 )
							{
								found = true;
								break;
							}
						}

						if ( !found )
						{
							// Add the import now.

							string projectPath = Path.GetDirectoryName(FileName);
							string targetsPath = StaticEnvironment.GetFilePath(Constants.Product.Sdk, Path.Combine(Constants.Folder.Bin, Constants.Folder.Targets), targetsFile);
							string import = FilePath.GetRelativePath(targetsPath, projectPath);
							BuildProject.AddNewImport(import, string.Empty);
						}
					}
				}
			}

			return base.Save(fileToBeSaved, remember, formatIndex);
		}

		protected virtual string[] GetProjectTargetsFiles()
		{
			return null;
		}

		protected override void SetOutputLogger(IVsOutputWindowPane output)
		{
			if ( BuildLogger == null )
			{
				// Need to get the outer IVsHierarchy because may be aggregated.

				System.IntPtr unknown = System.IntPtr.Zero;
				IVsHierarchy hierarchy = null;
				try
				{
					unknown = Marshal.GetIUnknownForObject(this);
					hierarchy = Marshal.GetTypedObjectForIUnknown(unknown, typeof(IVsHierarchy)) as IVsHierarchy;
				}
				finally
				{
					if ( unknown != System.IntPtr.Zero )
						Marshal.Release(unknown);
				}

				// Create the logger.

				BuildLogger = new VsLogger(output, TaskProvider, hierarchy);

				// To retrive the verbosity level the logger depends on the registry root.

				ILocalRegistry2 registry = GetService(typeof(SLocalRegistry)) as ILocalRegistry2;
				if ( registry != null )
				{
					string registryRoot;
					registry.GetLocalRegistryRoot(out registryRoot);
					VsLogger logger = BuildLogger as VsLogger;
					if ( !string.IsNullOrEmpty(registryRoot) && logger != null )
					{
						logger.VerbosityRegistryRoot = registryRoot;
						logger.ErrorString = ErrorString;
						logger.WarningString = WarningString;
					}
				}
			}
			else
			{
				((VsLogger) BuildLogger).OutputWindowPane = output;
			}

			if ( BuildEngine != null )
			{
				BuildEngine.UnregisterAllLoggers();
				BuildEngine.RegisterLogger(BuildLogger);
			}
		}
	}
}
