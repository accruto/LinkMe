using System.IO;

namespace LinkMe.Framework.Utility
{
	/// <summary>
	/// Binary serialization interfaces
	/// </summary>
	public interface IBinarySerializable
	{
		void Read(BinaryReader reader);
		void Write(BinaryWriter writer);
	}
}
