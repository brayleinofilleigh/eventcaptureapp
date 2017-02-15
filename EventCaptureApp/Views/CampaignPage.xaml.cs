using System;
using EventCaptureApp.ViewModels;
using Xamarin.Forms;

namespace EventCaptureApp.Views
{
	public partial class CampaignPage : ContentPage
	{
		private CampaignPageViewModel _viewModel;

		public CampaignPage()
		{
			InitializeComponent();
			NavigationPage.SetHasNavigationBar(this, false);
		}
	}
}
