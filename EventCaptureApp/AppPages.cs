﻿using System;
using System.Collections.Generic;
using EventCaptureApp.Models;
using EventCaptureApp.Views;
using Xamarin.Forms;

namespace EventCaptureApp
{
	public class AppPages
	{
		public static readonly AppPageReference Navigation = new AppPageReference(typeof(NavigationPage));
		public static readonly AppPageReference Launch = new AppPageReference(typeof(LaunchPage));
		public static readonly AppPageReference Login = new AppPageReference(typeof(LoginPage));
		public static readonly AppPageReference CampaignList = new AppPageReference(typeof(CampaignListPage));
		public static readonly AppPageReference Update = new AppPageReference(typeof(UpdatePage));
		public static readonly AppPageReference Campaign = new AppPageReference(typeof(CampaignPage));
		public static readonly AppPageReference Document = new AppPageReference(typeof(DocumentPage));

		public static readonly List<AppPageReference> Pages = new List<AppPageReference> {
			Launch, Login, CampaignList, Update, Campaign, Document
		};
	}
}
