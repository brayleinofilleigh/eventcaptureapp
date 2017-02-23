﻿using System;
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
			this.Campaign.ClearSelectedDocuments();
		}

		protected async Task OnHomeCommand()
		{
			await _navigationService.NavigateAsync(AppPages.Campaign.Name);
		}

		public Campaign Campaign
		{
			get { return CampaignData.Instance.Current; }
		}
	}
}