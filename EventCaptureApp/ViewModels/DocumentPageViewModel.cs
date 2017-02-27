using System;
using System.Diagnostics;
using EventCaptureApp.Data;
using EventCaptureApp.Enums;
using EventCaptureApp.Models;

namespace EventCaptureApp.ViewModels
{
	public class DocumentPageViewModel: ViewModelBase
	{
		private CampaignDocument _document;
		private string _documentFilePath = string.Empty;

		public DocumentPageViewModel()
		{
		}

		public override void OnNavigatedTo(Prism.Navigation.NavigationParameters parameters)
		{
			if (parameters.ContainsKey(NavigationParameterKeys.Document))
			{
				this.Document = (CampaignDocument)parameters[NavigationParameterKeys.Document];
				this.DocumentFilePath = AppFiles.Instance.GetDownloadedFilePath(this.Document.FileName);
			}
		}

		public CampaignDocument Document 
		{ 
			get { return _document; }
			set { this.SetProperty(ref _document, value); }
		}

		public string DocumentFilePath
		{ 
			get { return _documentFilePath; }
			private set { this.SetProperty(ref _documentFilePath, value); }
		}
	}
}
