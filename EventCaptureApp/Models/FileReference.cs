using System;
using Newtonsoft.Json;
using System.Linq;

namespace EventCaptureApp.Models
{
	public class FileReference
	{
		public string Url { get; set; } = string.Empty;

		public long ByteSize { get; set; } = 0;

		public DateTime DateModified { get; set; } = DateTime.MinValue;

		[JsonIgnore]
		public string LocalFolderPath { get; set; } = string.Empty;

		[JsonIgnore]
		public string LocalPath 
		{
			get { return System.IO.Path.Combine(LocalFolderPath, Name); }
		}

		[JsonIgnore]
		public string Name
		{
			get { return System.IO.Path.GetFileName(this.Url); }
		}

		[JsonIgnore]
		public string Extension 
		{ 
			get { return System.IO.Path.GetExtension(this.Url); }
		}

		[JsonIgnore]
		public long BytesWritten { get; set; } = 0;
	}
}

