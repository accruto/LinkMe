using System;
using System.Collections;
using System.IO;
using System.Text;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

namespace LinkMe.Framework.Tools.Mmc
{
	/// <summary>
	/// Stores snap-in state. Use the indexer to read and write property values.
	/// </summary>
	public class SnapinState
	{
		public SnapinState()
		{
		}

		public object this[string key]
		{
			get
			{
				return m_data[key];
			}
			set
			{
				m_data[key] = value;
				m_isDirty = true;
			}
		}

		internal virtual void ReadState(ComStream stream)
		{
			if ( stream.Length > 0 )
			{
				BinaryFormatter formatter = new BinaryFormatter(null, new StreamingContext(StreamingContextStates.Persistence));
				m_data = (Hashtable) formatter.Deserialize(stream);
			}
		}

		internal virtual void WriteState(Stream stream)
		{
			BinaryFormatter formatter = new BinaryFormatter(null, new StreamingContext(StreamingContextStates.Persistence));
			formatter.Serialize(stream, m_data);
		}

 		/// <summary>
		/// Set this to true when the state has changed (is dirty).
		/// </summary>
		public bool IsDirty 
		{
			get { return m_isDirty; }
			set { m_isDirty = value; }
		}

		private Hashtable m_data = new Hashtable();
		private bool m_isDirty = false;
	}
}
