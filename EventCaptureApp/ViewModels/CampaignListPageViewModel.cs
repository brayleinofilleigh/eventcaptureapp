using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using EventCaptureApp.Data;
using EventCaptureApp.Enums;
using EventCaptureApp.Models;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;

namespace EventCaptureApp.ViewModels
{
	public class CampaignListPageViewModel: ViewModelBase
	{
		private INavigationService _navigationService;
		private List<CampaignOverview> _campaigns;
		private CampaignOverview _selectedCampaign;
		public DelegateCommand UpdateCommand { get; set; }

		public CampaignListPageViewModel(INavigationService navigationService)
		{
			_navigationService = navigationService;
			this.UpdateCommand = new DelegateCommand(async () => await UpdateCampaignList());
		}

		public async override void OnNavigatedTo(NavigationParameters parameters)
		{
			await this.UpdateCampaignList();
		}

		public async Task UpdateCampaignList()
		{
			this.IsBusy = true;
			this.Campaigns = await CampaignData.Instance.GetCampaigns();
			this.IsBusy = false;
		}

		public List<CampaignOverview> Campaigns
		{
			get { return _campaigns; }
			set { this.SetProperty(ref _campaigns, value); }
		}

		public CampaignOverview SelectedCampaign
		{
			get { return _selectedCampaign; }
			set 
			{
				_selectedCampaign = value;
				if (value != null)
				{
					NavigationParameters navParams = new NavigationParameters();
					navParams.Add(NavigationParameterKeys.Campaign, value);
					_navigationService.NavigateAsync(AppPages.Update.Name, navParams);
				}
			}
		}
	}
}
