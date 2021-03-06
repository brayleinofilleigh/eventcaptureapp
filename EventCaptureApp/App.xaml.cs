﻿using Prism.Unity;
using Xamarin.Forms;
using EventCaptureApp.Views;
using System.Diagnostics;
using System.Threading.Tasks;
using EventCaptureApp.Data;
using Microsoft.Azure.Mobile;
using Microsoft.Azure.Mobile.Analytics;
using Microsoft.Azure.Mobile.Crashes;
using Prism.Navigation;

namespace EventCaptureApp
{
	public partial class App : PrismApplication
	{
		public const int LaunchDelayTime = 500;
		public static INavigationService RootNavigationService;

		public App()
		{
			RootNavigationService = this.NavigationService;
		}

		protected async override void OnInitialized()
		{
			InitializeComponent();
			string launchUri = System.IO.Path.Combine(AppPages.Navigation.Name, AppPages.Launch.Name);
			await this.NavigationService.NavigateAsync(launchUri);
			await this.Init();
		}

		protected async Task Init()
		{
			await DataManager.Instance.Init();
			await Task.Delay(LaunchDelayTime);
			Debug.WriteLine(AppFiles.Instance.LocalStorageFolder.Path);
			Debug.WriteLine($"Auth Token: {AdminData.Instance.AuthToken}");

			/*if (AdminData.Instance.IsAuthenticated)
			{
				if (CampaignData.Instance.Current == null)
				{
					await this.NavigationService.NavigateAsync(AppPages.CampaignList.Name);
				}
				else {
					await this.NavigationService.NavigateAsync(AppPages.Update.Name);
				}
			}
			else {
				await this.NavigationService.NavigateAsync(AppPages.Login.Name);
			}*/
			await this.NavigationService.NavigateAsync(AppPages.Login.Name);
		}

		protected override void RegisterTypes()
		{
			this.Container.RegisterTypeForNavigation<AppNavigationPage>();
			this.Container.RegisterTypeForNavigation<LaunchPage>();
			this.Container.RegisterTypeForNavigation<LoginPage>();
			this.Container.RegisterTypeForNavigation<CampaignListPage>();
			this.Container.RegisterTypeForNavigation<UpdatePage>();
			this.Container.RegisterTypeForNavigation<AdminPage>();
			this.Container.RegisterTypeForNavigation<CampaignPage>();
			this.Container.RegisterTypeForNavigation<DocumentPage>();
			this.Container.RegisterTypeForNavigation<LeadCapturePage>();
			this.Container.RegisterTypeForNavigation<LeadSubmittedPage>();
		}

		protected override void OnStart()
		{
			MobileCenter.Start(typeof(Analytics), typeof(Crashes));
		}
		/*
		protected override void OnSleep()
		{
			// Handle when your app sleeps
		}

		protected override void OnResume()
		{
			// Handle when your app resumes
		}
		*/
	}
}
