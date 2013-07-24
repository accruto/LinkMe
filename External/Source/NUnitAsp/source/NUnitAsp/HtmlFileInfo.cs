using System;
using System.IO;

namespace NUnit.Extensions.Asp
{
	/// <summary>
	/// Summary description for HtmlFileInfo.
	/// </summary>
	public class HtmlFileInfo
	{
		private string name;
		private string filename;
		//private byte[] contents;

		public HtmlFileInfo(string name, string filename)
		{
			//
			// TODO: Add constructor logic here
			//
			this.name= name;
			this.filename = filename;
		}

		public string Filename {
			get { return filename; }
			set { this.filename = value; }
		}

		public string Name {
			get {return name;}
		}

		public void WriteContents( BinaryWriter writer ) {
			//Load the contents from the filename and store in byte.
		
			FileStream stream = File.OpenRead(filename);
			int count = 4096;
			using (BinaryReader reader = new BinaryReader(stream)) {
				// do it in groups of 4096
				

				byte[] buffer = new byte[4096];
				while ((count=reader.Read(buffer,0,4096))>0) {
					writer.Write(buffer,0,count);
				}
				
				
			}
			
		
		}
	}
}
