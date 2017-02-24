using System;
namespace EventCaptureApp.Data
{
	public class AdminData
	{
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

		public bool IsAuthenticated
		{
			get { return !string.IsNullOrEmpty(AppSettings.AuthToken); }
		}
	}
}
