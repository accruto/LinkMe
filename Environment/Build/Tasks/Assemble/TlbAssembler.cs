/*using System.IO;
using Microsoft.Build.Utilities;

namespace LinkMe.Framework.Sdk.Tasks
{
	internal class TlbAssembler
		: ArtifactAssembler
	{
		public TlbAssembler(Artifact artifact, string sourceFullPath, string destinationFullPath, AssembleOptions options, TaskLoggingHelper log)
			: base(artifact, sourceFullPath, destinationFullPath, options, log)
		{
		}

		public override void Assemble(Action action)
		{
			// Copy the source itself.

			Copy(SourceFullPath);

			// Do the registration.

			RegisterTypeLib(action, DestinationFullPath);
		}

		public override void Clean(Action action)
		{
			if ( File.Exists(DestinationFullPath) )
			{
				// Do the unregistration.

				UnregisterTypeLib(action, DestinationFullPath);

				// Delete the file itself.

				Delete(DestinationFullPath);
			}
		}

		private void RegisterTypeLib(Action action, string fullPath)
		{
			// Check whether this can be done.

			if ( RegisterUtil.CanRegisterTypeLibrary(fullPath) )
			{
				// Perform the action only if required.

				if ( action == Action.Assemble )
					RegisterUtil.RegisterTypeLibrary(fullPath);

				Artifact.SetMetadata(Constants.Catalogue.Artifact.Register, true);
				LogMessage(Resources.Messages.RegisteredTypeLibrary, fullPath);
			}
		}

		private void UnregisterTypeLib(Action action, string fullPath)
		{
			// Check whether this can be done.

			if ( RegisterUtil.CanRegisterTypeLibrary(fullPath) )
			{
				// Perform the action only if required.

				if ( action == Action.Assemble )
					RegisterUtil.UnregisterTypeLibrary(fullPath);

				LogMessage(Resources.Messages.UnregisteredTypeLibrary, fullPath);
			}
		}
	}
}
*/