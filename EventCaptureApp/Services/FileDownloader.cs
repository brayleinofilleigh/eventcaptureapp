using System;
using System.Collections.Generic;
using EventCaptureApp.Delegates;
using EventCaptureApp.Interfaces;
using EventCaptureApp.Models;
using Xamarin.Forms;
using System.Linq;

namespace EventCaptureApp.Services
{
	public class FileDownloader
	{
		public event DownloadErrorDelegate DownloadError;
		public event DownloadProgressDelegate DownloadProgress;
		public event DownloadProgressDelegate FileDownloadComplete;
		public event DownloadProgressDelegate QueueDownloadComplete;

		private static FileDownloader _instance;
		private IFileDownloader _downloader;
		private List<FileReference> _queuedFiles = new List<FileReference>();
		private long _bytesWritten = 0;

		public static FileDownloader Instance
		{
			get
			{
				if (_instance == null)
				{
					_instance = new FileDownloader();
					_instance._downloader = DependencyService.Get<IFileDownloader>();
				}
				return _instance;
			}
		}

		public void DownloadFiles(List<FileReference> fileList)
		{
			_queuedFiles = fileList;
			this.TotalBytesToDownload = this.BytesDownloaded = _bytesWritten = 0;
			foreach (FileReference file in fileList)
				this.TotalBytesToDownload += file.ByteSize;
			this.StartNextFileDownload();
		}

		private void StartNextFileDownload()
		{
			_bytesWritten = this.BytesDownloaded;
			if (_queuedFiles.Any())
			{
				this.CurrentFile = _queuedFiles.First();
				_downloader.DownloadProgress += FileDownloadProgressHandler;
				_downloader.DownloadError += FileDownloadErrorHandler;
				_downloader.DownloadComplete += FileDownloadCompleteHandler;
				_downloader.DownloadFile(this.CurrentFile.Url, this.CurrentFile.LocalPath);
			}
			else {
				if (this.QueueDownloadComplete != null)
					this.QueueDownloadComplete(this, this.BytesDownloaded, this.PercentDownloaded);
			}
		}

		private void RemoveEventListeners()
		{
			_downloader.DownloadProgress -= FileDownloadProgressHandler;
			_downloader.DownloadError -= FileDownloadErrorHandler;
			_downloader.DownloadComplete -= FileDownloadCompleteHandler;
		}

		protected void FileDownloadProgressHandler(object sender, long totalBytesWritten, int percentDownloaded)
		{
			this.UpdateDownloadProgress(totalBytesWritten);
		}

		protected void FileDownloadErrorHandler(object sender, int errorCode)
		{
			this.RemoveEventListeners();
			if (this.DownloadError != null)
				this.DownloadError(this, errorCode);
		}

		protected void FileDownloadCompleteHandler(object sender, long totalBytesWritten, int percentDownloaded)
		{
			this.RemoveEventListeners();
			this.UpdateDownloadProgress(totalBytesWritten);
			_queuedFiles.Remove(this.CurrentFile);
			if (this.FileDownloadComplete != null) 
				this.FileDownloadComplete(this, this.BytesDownloaded, this.PercentDownloaded);
			this.StartNextFileDownload();
		}

		private void UpdateDownloadProgress(long totalFileBytesWritten)
		{
			this.CurrentFile.BytesWritten = totalFileBytesWritten;
			this.BytesDownloaded = _bytesWritten + totalFileBytesWritten;
			if (this.DownloadProgress != null)
				this.DownloadProgress(this, this.BytesDownloaded, this.PercentDownloaded);
		}

		public FileReference CurrentFile { get; private set; }

		public long TotalBytesToDownload { get; private set; } = 0;

		public long BytesDownloaded { get; private set; } = 0;

		public int PercentDownloaded { 
			get
			{
				decimal percent = this.BytesDownloaded * Decimal.Divide(100, this.TotalBytesToDownload);
				return Convert.ToInt32(percent);
			}
		}
	}
}

