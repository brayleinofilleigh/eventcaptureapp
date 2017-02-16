using System;
namespace EventCaptureApp
{
	public static class AppConstants
	{
		public static readonly bool IsLiveBuild = false;

		private const string DevWebserviceUrl = "https://ios-dev.proofing5.brayleino.co.uk/blvisitorapp/";
		private const string LiveWebserviceUrl = "https://eventcaptureapp.brayleino.co.uk/";

		public static readonly string MobileCenterId = "53ebc8a0-2f9c-4f00-973d-fe0f1bb8ae94";

		public static readonly string LocalDatabaseName = "eventcaptureapp.sqlite";

		private static string WebserviceUrl
		{
			get { return IsLiveBuild ? LiveWebserviceUrl : DevWebserviceUrl; }
		}

		public static string GetAuthTokenUrl
		{
			get { return GetWebserviceCallUrl("GetAuthToken"); }
		}

		public static string GetCampaignsUrl
		{
			//get { return GetWebserviceCallUrl("GetCampaigns"); }
			get { return GetWebserviceCallUrl("campaigns.json"); }
		}

		public static string GetCampaignFileListUrl
		{
			get { return GetWebserviceCallUrl("GetCampaignFileList"); }
		}

		public static string SaveNewLeadsUrl
		{
			get { return GetWebserviceCallUrl("SaveNewLeads"); }
		}

		private static string GetWebserviceCallUrl(string callName)
		{
			return System.IO.Path.Combine(WebserviceUrl, callName);
		}
	}
}
