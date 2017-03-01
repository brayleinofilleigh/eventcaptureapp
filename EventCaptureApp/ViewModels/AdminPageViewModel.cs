using System;
using System.Threading.Tasks;
using EventCaptureApp.Data;
using EventCaptureApp.Models;
using Prism.Commands;
using Prism.Navigation;

namespace EventCaptureApp.ViewModels
{
	public class AdminPageViewModel: ViewModelBase
	{
		private INavigationService _navigationService;
		private CampaignStats _stats;
		public DelegateCommand CampaignListPageCommand { get; private set; }

		public AdminPageViewModel(INavigationService navigationService)
		{
			_navigationService = navigationService;
			this.CampaignListPageCommand = new DelegateCommand(async () => await OnCampaignListPageCommand());
		}

		public async override void OnNavigatedTo(NavigationParameters parameters)
		{
			base.OnNavigatedTo(parameters);
			this.IsBusy = true;
			this.Stats = await CampaignData.Instance.GetCampaignStats(this.Campaign.Id);
			this.IsBusy = false;
		}

		public Campaign Campaign 
		{ 
			get { return CampaignData.Instance.Current; }
		}

		public CampaignStats Stats
		{
			get { return _stats; }
			set { this.SetProperty(ref _stats, value); }
		}

		public DeviceInfo DeviceInfo
		{
			get { return DeviceInfo.Instance; }
		}

		protected async Task OnCampaignListPageCommand()
		{
			await _navigationService.NavigateAsync(AppPages.CampaignList.Name);
		}
	}
}
