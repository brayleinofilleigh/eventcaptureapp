using System;
using EventCaptureApp.Data;
using EventCaptureApp.Models;
using System.Linq;
using Prism.Navigation;
using EventCaptureApp.Enums;

namespace EventCaptureApp.ViewModels
{
	public class CampaignPageViewModel: ViewModelBase
	{
		private INavigationService _navigationService;
		private CampaignCategory _selectedCategory;
		private CampaignDocument _selectedDocument;
		private int _numberSelectedDocuments = 0;

		public CampaignPageViewModel(INavigationService navigationService)
		{
			_navigationService = navigationService;
			this.SelectedCategory = this.Campaign.Categories.FirstOrDefault();
		}

		public Campaign Campaign
		{
			get { return CampaignData.Instance.Current; }
		}

		public CampaignCategory SelectedCategory
		{
			get { return _selectedCategory; }
			set { this.SetProperty(ref _selectedCategory, value); }
		}

		public CampaignDocument SelectedDocument
		{
			get { return _selectedDocument; }
			set {
				_selectedDocument = value;
				if (value != null)
				{
					NavigationParameters navParams = new NavigationParameters();
					navParams.Add(NavigationParameterKeys.Document, value);
					_navigationService.NavigateAsync(AppPages.Document.Name, navParams);
				}
			}
		}

		public int NumberSelectedDocuments
		{
			get { return _numberSelectedDocuments; }
			set { this.SetProperty(ref _numberSelectedDocuments, value); }
		}
	}
}
