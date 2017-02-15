using System;
using System.Threading.Tasks;
using EventCaptureApp.Models;
using Newtonsoft.Json;

namespace EventCaptureApp.Services
{
	public static class AuthService
	{
		public static async Task<AuthResponse> RequestAuthToken(string emailAddress, string password)
		{
			AuthResponse authResponse = new AuthResponse();
			AuthRequest request = new AuthRequest() { EmailAddress = emailAddress, Password = password };
			RestResponse response = await RestService.Instance.ExecRequest(AppConstants.GetAuthTokenUrl, request);
			if (response.RequestSuccess)
				authResponse = JsonConvert.DeserializeObject<AuthResponse>(response.Content);
			return authResponse;
		}
	}

	public class AuthRequest
	{
		public string EmailAddress { get; set; } = string.Empty;

		public string Password { get; set; } = string.Empty;
	}

	public class AuthResponse: ResponseModelBase
	{
		public string AuthToken { get; set; } = string.Empty;
	}
}
