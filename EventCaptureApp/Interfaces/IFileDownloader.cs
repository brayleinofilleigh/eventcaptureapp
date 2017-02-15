using System;
using EventCaptureApp.Delegates;

namespace EventCaptureApp.Interfaces
{
	public interface IFileDownloader
	{
		event DownloadProgressDelegate DownloadProgress;
		event DownloadProgressDelegate DownloadComplete;
		event DownloadErrorDelegate DownloadError;

		void DownloadFile(string remoteUrl, string localPath);
	}
}

