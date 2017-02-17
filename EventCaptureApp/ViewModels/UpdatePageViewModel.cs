using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using EventCaptureApp.Data;
using EventCaptureApp.Enums;
using EventCaptureApp.Models;
using System.Linq;
using Plugin.Connectivity;
using EventCaptureApp.Services;
using Prism.Commands;
using Newtonsoft.Json;

namespace EventCaptureApp.ViewModels
{
	public class UpdatePageViewModel: ViewModelBase
	{
		private CampaignOverview _campaign;
		private string _status = string.Empty;
		private FileDownloader _fileDownloader;
		private List<FileReference> _updateFileList;
		private int _numberFilesDownload = 0;
		private int _currentFileNumber = 1;
		private int _percentDownloaded = 0;
		private bool _isUpdatesAvailable = false;
		public DelegateCommand UpdateCommand { get; private set; }

		public UpdatePageViewModel()
		{
			_fileDownloader = FileDownloader.Instance;
			this.UpdateCommand = new DelegateCommand(StartFileUpdate).ObservesCanExecute((p) => IsUpdatesAvailable);
		}

		public async override void OnNavigatedTo(Prism.Navigation.NavigationParameters parameters)
		{
			await Task.Delay(500);
			if (parameters.ContainsKey(NavigationParameterKeys.Campaign))
			{
				this.Campaign = (CampaignOverview)parameters[NavigationParameterKeys.Campaign];
			}
			else if(CampaignData.Instance.Current != null) 
			{
				this.Campaign = CampaignData.Instance.Current;
			}
			if (this.Campaign != null)
			{
				await this.FileUpdateCheck(this.Campaign.Id);
			}
			_fileDownloader.DownloadProgress += OnDownloadProgress;
			_fileDownloader.DownloadError += OnDownloadError;
			_fileDownloader.FileDownloadComplete += OnFileDownloadComplete;
			_fileDownloader.QueueDownloadComplete += OnFileQueueDownloadComplete;
		}

		public override void OnNavigatedFrom(Prism.Navigation.NavigationParameters parameters)
		{
			_fileDownloader.DownloadProgress -= OnDownloadProgress;
			_fileDownloader.DownloadError -= OnDownloadError;
			_fileDownloader.FileDownloadComplete -= OnFileDownloadComplete;
			_fileDownloader.QueueDownloadComplete -= OnFileQueueDownloadComplete;
		}

		protected async Task FileUpdateCheck(int campaignId)
		{
			this.IsBusy = true;
			this.Status = "Checking for updates...";
			if (CrossConnectivity.Current.IsConnected)
			{
				_updateFileList = await CampaignData.Instance.GetUpdateFileList(campaignId);
				_numberFilesDownload = _updateFileList.Count;
				this.IsUpdatesAvailable = _updateFileList.Any();

				Debug.WriteLine(JsonConvert.SerializeObject(_updateFileList));

				if (!this.IsUpdatesAvailable)
					this.Status = "You are up-to-date";
			}
			else {
				this.Status = "No internet connection found";
			}
			this.IsBusy = false;
		}

		/*private void GetFilesToUpdate(List<FileReference> fileList)
		{
			foreach (FileReference file in fileList)
			{
				DateTime localFileDateModed = AppFiles.Instance.GetFileModifiedDate(AppFiles.Instance.GetDownloadedFilePath(file.Name));
				if (DateTime.Compare(localFileDateModed, file.DateModified) < 0)
				{
					file.LocalFolderPath = AppFiles.Instance.DownloadsFolder.Path;
					_fileUpdateList.Add(remoteFile);
				}
			}
			fileList = fileList.OrderBy(x => x.Extension == ".sqlite" || x.Extension == ".json").ToList();
		}*/

		protected void StartFileUpdate()
		{
			this.UpdateDownloadStatus();
			_fileDownloader.DownloadFiles(_updateFileList);
		}

		protected void OnDownloadProgress(object sender, long totalBytesWritten, int percentDownloaded)
		{
			this.PercentDownloaded = percentDownloaded;
		}

		protected void OnDownloadError(object sender, int errorCode)
		{
			this.Status = $"Download error - {_fileDownloader.CurrentFile.Name} : {errorCode}";
		}

		protected void OnFileDownloadComplete(object sender, long totalBytesWritten, int percentDownloaded)
		{
			_currentFileNumber++;
			this.UpdateDownloadStatus();
		}

		protected void OnFileQueueDownloadComplete(object sender, long totalBytesWritten, int percentDownloaded)
		{
			this.Status = "Update complete";
		}

		protected void UpdateDownloadStatus()
		{
			this.Status = $"Downloading file {_currentFileNumber} of {_numberFilesDownload}";
		}

		public CampaignOverview Campaign
		{
			get { return _campaign; }
			private set { this.SetProperty(ref _campaign, value); }
		}

		public string Status
		{
			get { return _status; }
			set { this.SetProperty(ref _status, value); }
		}

		public int PercentDownloaded
		{
			get { return _percentDownloaded; }
			set { this.SetProperty(ref _percentDownloaded, value); }
		}

		public bool IsUpdatesAvailable
		{
			get { return _isUpdatesAvailable; }
			set { this.SetProperty(ref _isUpdatesAvailable, value); }
		}
	}
}
