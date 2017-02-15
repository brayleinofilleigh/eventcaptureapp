using System;
using EventCaptureApp.Delegates;
using EventCaptureApp.DependencyService.iOS;
using EventCaptureApp.Interfaces;
using Foundation;
using Xamarin.Forms;

[assembly: Dependency(typeof(FileDownloader_iOS))]

namespace EventCaptureApp.DependencyService.iOS
{
	public class FileDownloader_iOS : NSUrlSessionDownloadDelegate, IFileDownloader
	{
		public event DownloadErrorDelegate DownloadError;
		public event DownloadProgressDelegate DownloadProgress;
		public event DownloadProgressDelegate DownloadComplete;

		private NSUrlSessionDownloadTask _downloadTask;
		private NSUrlSession _session;
		private NSFileManager _fileManager;
		private string _localFilePath;

		public FileDownloader_iOS()
		{
			_fileManager = new NSFileManager();
			NSUrlSessionConfiguration sessionConfig = NSUrlSessionConfiguration.CreateBackgroundSessionConfiguration(Guid.NewGuid().ToString());
			_session = NSUrlSession.FromConfiguration(sessionConfig, this, new NSOperationQueue());
		}

		public void DownloadFile(string remoteUrl, string localPath)
		{
			_localFilePath = localPath;
			_downloadTask = _session.CreateDownloadTask(NSUrlRequest.FromUrl(new NSUrl(remoteUrl)));
			_downloadTask.Resume();
		}

		public override void DidWriteData(NSUrlSession session, NSUrlSessionDownloadTask downloadTask, long bytesWritten, long totalBytesWritten, long totalBytesExpectedToWrite)
		{
			if (this.DownloadProgress != null)
				this.DownloadProgress(this, totalBytesWritten);
		}

		public override void DidCompleteWithError(NSUrlSession session, NSUrlSessionTask task, NSError error)
		{
			if (error != null)
			{
				if (this.DownloadError != null)
					this.DownloadError(this, Convert.ToInt32(error.Code));
			}
		}

		public override void DidFinishDownloading(NSUrlSession session, NSUrlSessionDownloadTask downloadTask, NSUrl location)
		{
			long bytesReceived = downloadTask.BytesReceived;
			NSError nsError = new NSError();
			_fileManager.Remove(_localFilePath, out nsError);
			_fileManager.Move(location.Path, _localFilePath, out nsError);
			_downloadTask.Dispose();
			if (this.DownloadComplete != null) 
				this.DownloadComplete(this, bytesReceived);
		}
	}
}