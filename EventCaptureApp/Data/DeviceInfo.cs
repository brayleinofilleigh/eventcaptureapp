using System;
using Plugin.DeviceInfo;

namespace EventCaptureApp.Data
{
	public class DeviceInfo
	{
		private static DeviceInfo _instance;

		public static DeviceInfo Instance
		{
			get
			{
				if (_instance == null)
					_instance = new DeviceInfo();
				return _instance;
			}
		}

		public string Id
		{
			get { return CrossDeviceInfo.Current.Id; }
		}

		public string Model
		{
			get { return CrossDeviceInfo.Current.Model; }
		}

		public string OSPlatform
		{
			get { return CrossDeviceInfo.Current.Platform.ToString(); }
		}

		public string OSVersion
		{
			get { return CrossDeviceInfo.Current.Version; }
		}
	}
}
