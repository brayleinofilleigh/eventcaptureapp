using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using EventCaptureApp.Data;
using EventCaptureApp.Enums;
using EventCaptureApp.Models;
using System.Linq;
using Plugin.Connectivity;
using EventCaptureApp.Services;
using Prism.Commands;
using Prism.Navigation;

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
		private bool _isUpdatesSkippable = false;
		private INavigationService _navigationService;
		public DelegateCommand ContinueCommand { get; private set; }
		public DelegateCommand UpdateCommand { get; private set; }

		public UpdatePageViewModel(INavigationService navigationService)
		{
			_navigationService = navigationService;
			_updateFileList = new List<FileReference>(); 
			_fileDownloader = FileDownloader.Instance;
			this.ContinueCommand = new DelegateCommand(async () => await OnContinueCommand()).ObservesCanExecute((p) => IsNotBusy);
			this.UpdateCommand = new DelegateCommand(OnUpdateCommand).ObservesCanExecute((p) => IsNotBusy);
		}

		public async override void OnNavigatedTo(NavigationParameters parameters)
		{
			_fileDownloader.DownloadEvent += OnFileDownloaderEvent;
			if (parameters.ContainsKey(NavigationParameterKeys.Campaign))
			{
				this.Campaign = (CampaignOverview)parameters[NavigationParameterKeys.Campaign];
			}
			else if(CampaignData.Instance.Current != null) 
			{
				this.Campaign = CampaignData.Instance.Current;
			}
			if (this.Campaign != null)
				await this.FileUpdateCheck(this.Campaign.Id);
		}

		public override void OnNavigatedFrom(NavigationParameters parameters)
		{
			_fileDownloader.DownloadEvent -= OnFileDownloaderEvent;
		}

		protected async Task FileUpdateCheck(int campaignId)
		{
			this.IsBusy = true;
			this.Status = "Checking for updates...";
			if (CrossConnectivity.Current.IsConnected)
			{
				_updateFileList = await CampaignData.Instance.GetCampaignUpdateFileList(campaignId);
				_numberFilesDownload = _updateFileList.Count;
				this.IsUpdatesAvailable = _updateFileList.Any();
				this.Status = this.IsUpdatesAvailable ? "Updates available" : "You are up-to-date";
			}
			else {
				this.IsUpdatesAvailable = false;
				this.Status = "No internet connection found";
			}
			this.IsBusy = false;
		}

		protected async Task OnContinueCommand()
		{
			await _navigationService.NavigateAsync(AppPages.Campaign.Name);
		}

		protected void OnUpdateCommand()
		{
			this.IsBusy = true;
			this.UpdateDownloadStatus();
			_fileDownloader.DownloadFiles(_updateFileList);
		}

		protected void OnFileDownloaderEvent(object sender, DownloadEventType type, long totalBytesWritten, int percentDownloaded = 0, int errorCode = 0)
		{
			switch (type)
			{
				case DownloadEventType.Progess:
					this.PercentDownloaded = percentDownloaded;
					break;
				case DownloadEventType.FileDownloaded:
					_currentFileNumber++;
					this.UpdateDownloadStatus();
					break;
				case DownloadEventType.Error:
					this.Status = $"Download error - {_fileDownloader.CurrentFile.Name} : {errorCode}";
					break;
					case DownloadEventType.QueueDownloaded:
					this.FinaliseUpdate();
					break;
			}
		}

		protected async Task FinaliseUpdate()
		{
			this.IsUpdatesAvailable = this.IsBusy = false;
			this.Status = "Update complete";
			await CampaignData.Instance.SetCurrent(this.Campaign);
			await _navigationService.NavigateAsync(AppPages.Campaign.Name);
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
			private set { this.SetProperty(ref _status, value); }
		}

		public int PercentDownloaded
		{
			get { return _percentDownloaded; }
			private set { this.SetProperty(ref _percentDownloaded, value); }
		}

		public bool IsUpdatesAvailable
		{
			get { return _isUpdatesAvailable; }
			private set 
			{ 
				this.SetProperty(ref _isUpdatesAvailable, value);
				this.IsUpdatesSkippable = !value;
			}
		}

		public bool IsUpdatesSkippable
		{
			get { return _isUpdatesSkippable; }
			private set { this.SetProperty(ref _isUpdatesSkippable, value); }
		}
	}
}
