using System;
using System.Collections.Generic;
using EventCaptureApp.Models;
using EventCaptureApp.Views;
using Xamarin.Forms;

namespace EventCaptureApp
{
	public class AppPages
	{
		public static readonly AppPageReference Navigation = new AppPageReference(typeof(AppNavigationPage));
		public static readonly AppPageReference Launch = new AppPageReference(typeof(LaunchPage));
		public static readonly AppPageReference Login = new AppPageReference(typeof(LoginPage));
		public static readonly AppPageReference CampaignList = new AppPageReference(typeof(CampaignListPage));
		public static readonly AppPageReference Update = new AppPageReference(typeof(UpdatePage));
		public static readonly AppPageReference Admin = new AppPageReference(typeof(AdminPage));
		public static readonly AppPageReference Campaign = new AppPageReference(typeof(CampaignPage));
		public static readonly AppPageReference Document = new AppPageReference(typeof(DocumentPage));
		public static readonly AppPageReference LeadCapture = new AppPageReference(typeof(LeadCapturePage));
		public static readonly AppPageReference LeadSubmitted = new AppPageReference(typeof(LeadSubmittedPage));

		public static readonly List<AppPageReference> Pages = new List<AppPageReference> {
			Launch, Login, CampaignList, Update, Admin, Campaign, Document, LeadCapture, LeadSubmitted
		};
	}
}
