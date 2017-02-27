using System;

namespace EventCaptureApp.Models
{
	public class AuthRequest
	{
		public string EmailAddress { get; set; } = string.Empty;

		public string Password { get; set; } = string.Empty;
	}

	public class AuthRequestResponse: RestResponseBase
	{
		public string AuthToken { get; set; } = string.Empty;
	}
}
