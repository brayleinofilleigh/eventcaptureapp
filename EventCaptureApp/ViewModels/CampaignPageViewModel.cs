using System;
using EventCaptureApp.Data;
using EventCaptureApp.Models;
using System.Linq;
using Prism.Navigation;
using EventCaptureApp.Enums;
using Prism.Commands;
using System.Threading.Tasks;

namespace EventCaptureApp.ViewModels
{
	public class CampaignPageViewModel: ViewModelBase
	{
		private INavigationService _navigationService;
		private CampaignCategory _selectedCategory;
		private CampaignDocument _selectedDocument;
		private int _numberSelectedDocuments = 0;
		public DelegateCommand CapturePageCommand { get; private set; }
		public DelegateCommand AdminPageCommand { get; private set; }

		public CampaignPageViewModel(INavigationService navigationService)
		{
			_navigationService = navigationService;
			this.SelectedCategory = this.Campaign.Categories.FirstOrDefault();
			this.CapturePageCommand = new DelegateCommand(async () => await this.OnCapturePageCommand());
			this.AdminPageCommand = new DelegateCommand(async () =>  await this.OnAdminPageCommand());
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

		protected async Task OnCapturePageCommand()
		{
			await _navigationService.NavigateAsync(AppPages.LeadCapture.Name);
		}

		protected async Task OnAdminPageCommand()
		{
			await _navigationService.NavigateAsync(AppPages.Admin.Name);
		}

		public int NumberSelectedDocuments
		{
			get { return _numberSelectedDocuments; }
			set { this.SetProperty(ref _numberSelectedDocuments, value); }
		}
	}
}
