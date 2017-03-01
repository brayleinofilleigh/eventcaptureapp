using System;
using System.Threading.Tasks;
using EventCaptureApp.Data;
using EventCaptureApp.ViewModels;
using Prism.Commands;
using Prism.Navigation;

namespace EventCaptureApp.ViewModels
{
	public class LeadSubmittedPageViewModel: ViewModelBase
	{
		private INavigationService _navigationService;
		public DelegateCommand HomeCommand { get; private set; }

		public LeadSubmittedPageViewModel(INavigationService navigationService)
		{
			_navigationService = navigationService;
			this.HomeCommand = new DelegateCommand(async () => await this.OnHomeCommand());
		}

		protected async Task OnHomeCommand()
		{
			this.Campaign.ClearSelectedDocuments();
			await App.RootNavigationService.NavigateAsync(AppPages.CampaignList.Name);
		}

		public Campaign Campaign
		{
			get { return CampaignData.Instance.Current; }
		}
	}
}
