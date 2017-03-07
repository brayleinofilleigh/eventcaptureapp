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
		public DelegateCommand HomeCommand { get; private set; }

		public LeadSubmittedPageViewModel()
		{
			this.HomeCommand = new DelegateCommand(async () => await this.OnHomeCommand());
		}

		protected async Task OnHomeCommand()
		{
			this.Campaign.ResetSelectedDocuments();
			this.Campaign.ResetFormInputValues();
			await App.RootNavigationService.NavigateAsync(AppPages.Campaign.Name);
		}

		public Campaign Campaign
		{
			get { return CampaignData.Instance.Current; }
		}
	}
}
