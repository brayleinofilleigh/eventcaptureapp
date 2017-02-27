using System;
using System.Threading.Tasks;
using EventCaptureApp.Models;
using EventCaptureApp.Services;
using Newtonsoft.Json;
using Plugin.Settings;

namespace EventCaptureApp.Data
{
	public class AdminData
	{
		private static readonly string AuthTokenKey = "authToken";
		private static AdminData _instance;

		public static AdminData Instance
		{
			get
			{
				if (_instance == null)
					_instance = new AdminData();
				return _instance;
			}
		}

		public async Task<AuthRequestResponse> GetAuthToken(string emailAddress, string password)
		{
			AuthRequestResponse authResponse = new AuthRequestResponse();
			AuthRequest request = new AuthRequest() { EmailAddress = emailAddress, Password = password };
			//RestResponse response = await RestService.Instance.ExecRequest(AppConstants.GetAuthTokenUrl, request);
			RestResponse response = await RestService.Instance.ExecRequest(AppConstants.GetAuthTokenUrl);
			if (response.RequestSuccess)
			{
				authResponse = JsonConvert.DeserializeObject<AuthRequestResponse>(response.Content);
				this.AuthToken = authResponse.AuthToken;
			}
			return authResponse;
		}

		public string AuthToken
		{
			get { return CrossSettings.Current.GetValueOrDefault<string>(AuthTokenKey, string.Empty); }
			private set { CrossSettings.Current.AddOrUpdateValue<string>(AuthTokenKey, value); }
		}

		public bool IsAuthenticated
		{
			get { return !string.IsNullOrEmpty(this.AuthToken); }
		}
	}
}
