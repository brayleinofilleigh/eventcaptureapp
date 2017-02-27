using System;
namespace EventCaptureApp.Models
{
	public class RestRequestBase
	{
		public string AuthToken { get; set; } = string.Empty;
	}

	public class RestResponseBase
	{
		public int ErrorCode { get; set; } = 0;
	}
}
