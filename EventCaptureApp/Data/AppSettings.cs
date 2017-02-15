using System;
using Plugin.Settings;

namespace EventCaptureApp.Data
{
	public static class AppSettings
	{
		private static readonly string AuthTokenKey = "authToken";
		private static readonly string CurrentCampaignIdKey = "currentCampaignId";

		public static string AuthToken
		{
			get { return CrossSettings.Current.GetValueOrDefault<string>(AuthTokenKey, string.Empty); }
			set { CrossSettings.Current.AddOrUpdateValue<string>(AuthTokenKey, value); }
		}

		public static int CurrentCampaignId
		{
			get { return CrossSettings.Current.GetValueOrDefault<int>(CurrentCampaignIdKey, 0); }
			set { CrossSettings.Current.AddOrUpdateValue<int>(CurrentCampaignIdKey, value); }
		}
	}
}
