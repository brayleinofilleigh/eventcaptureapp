using System;

namespace EventCaptureApp.Interfaces
{
	public interface IAppFiles
	{
		bool FileExists(string filePath);

		DateTime GetFileModifiedDate(string filePath);
	}
}
