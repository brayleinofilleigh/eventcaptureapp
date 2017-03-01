using System;

namespace EventCaptureApp.Models
{
	public class AuthRequest
	{
		public string EmailAddress { get; set; } = string.Empty;

		public string Password { get; set; } = string.Empty;

		public string DeviceId { get; set; } = string.Empty;

		public string DeviceModel { get; set; } = string.Empty;

		public string DeviceOSPlatform { get; set; } = string.Empty;

		public string DeviceOSVersion { get; set; } = string.Empty;
	}

	public class AuthRequestResponse: RestResponseBase
	{
		public string AuthToken { get; set; } = string.Empty;
	}
}
