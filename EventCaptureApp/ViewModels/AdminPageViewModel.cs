using System;
using System.Threading.Tasks;
using EventCaptureApp.Data;
using Prism.Commands;
using Prism.Navigation;

namespace EventCaptureApp.ViewModels
{
	public class AdminPageViewModel: ViewModelBase
	{
		private INavigationService _navigationService;
		public DelegateCommand CampaignListPageCommand { get; private set; }

		public AdminPageViewModel(INavigationService navigationService)
		{
			_navigationService = navigationService;
			this.CampaignListPageCommand = new DelegateCommand(async () => await OnCampaignListPageCommand());
		}

		public async override void OnNavigatedTo(NavigationParameters parameters)
		{
			base.OnNavigatedTo(parameters);
			//string stats = await CampaignData.Instance.GetCampaignStats(this.Campaign.Id);
		}

		public Campaign Campaign 
		{ 
			get { return CampaignData.Instance.Current; }
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
