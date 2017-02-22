using System;
using System.Collections.Generic;
using EventCaptureApp.Delegates;
using EventCaptureApp.Interfaces;
using EventCaptureApp.Models;
using Xamarin.Forms;
using System.Linq;
using EventCaptureApp.Enums;

namespace EventCaptureApp.Services
{
	public class FileDownloader
	{
		public event DownloadEventDelegate DownloadEvent;
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
				_downloader.DownloadEvent += OnFileDownloadEvent;
				_downloader.DownloadFile(this.CurrentFile.Url, this.CurrentFile.LocalPath);
			}
			else {
				this.DispatchEvent(DownloadEventType.QueueDownloaded);
			}
		}

		private void RemoveEventListeners()
		{
			_downloader.DownloadEvent -= OnFileDownloadEvent;
		}

		protected void OnFileDownloadEvent(object sender, DownloadEventType type, long totalBytesWritten, int percentDownloaded = 0, int errorCode = 0)
		{
			switch (type)
			{
				case DownloadEventType.Progess:
					this.UpdateDownloadProgress(totalBytesWritten);
					break;
				case DownloadEventType.Error:
					this.RemoveEventListeners();
					this.DispatchEvent(DownloadEventType.Error, errorCode);
					break;
				case DownloadEventType.FileDownloaded:
					this.RemoveEventListeners();
					this.UpdateDownloadProgress(totalBytesWritten);
					_queuedFiles.Remove(this.CurrentFile);
					this.DispatchEvent(DownloadEventType.FileDownloaded);
					this.StartNextFileDownload();
					break;
			}
		}

		protected void UpdateDownloadProgress(long totalFileBytesWritten)
		{
			this.CurrentFile.BytesWritten = totalFileBytesWritten;
			this.BytesDownloaded = _bytesWritten + totalFileBytesWritten;
			this.DispatchEvent(DownloadEventType.Progess);
		}

		protected void DispatchEvent(DownloadEventType type, int errorCode = 0)
		{
			if (this.DownloadEvent != null)
				this.DownloadEvent(this, type, this.BytesDownloaded, this.PercentDownloaded, errorCode);
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

