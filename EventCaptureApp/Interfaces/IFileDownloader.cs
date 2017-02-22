using System;
using EventCaptureApp.Delegates;

namespace EventCaptureApp.Interfaces
{
	public interface IFileDownloader
	{
		event DownloadEventDelegate DownloadEvent;

		void DownloadFile(string remoteUrl, string localPath);
	}
}

