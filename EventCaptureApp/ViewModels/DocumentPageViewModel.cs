using System;
using EventCaptureApp.Enums;
using EventCaptureApp.Models;

namespace EventCaptureApp.ViewModels
{
	public class DocumentPageViewModel: ViewModelBase
	{
		private CampaignDocument _document;

		public DocumentPageViewModel()
		{
		}

		public override void OnNavigatedTo(Prism.Navigation.NavigationParameters parameters)
		{
			if (parameters.ContainsKey(NavigationParameterKeys.Document))
			{
				this.Document = (CampaignDocument)parameters[NavigationParameterKeys.Document];
			}
		}

		public CampaignDocument Document 
		{ 
			get { return _document; }
			set { this.SetProperty(ref _document, value); }
		}
	}
}
