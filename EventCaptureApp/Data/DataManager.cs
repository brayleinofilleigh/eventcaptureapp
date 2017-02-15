using System;
using System.Threading.Tasks;

namespace EventCaptureApp.Data
{
	public class DataManager
	{
		private static DataManager _instance;

		public static DataManager Instance
		{
			get
			{
				if (_instance == null)
					_instance = new DataManager();
				return _instance;
			}
		}

		public async Task Init()
		{
			await AppFiles.Instance.Init();
			await LocalDatabase.Instance.Init(AppFiles.Instance.LocalStorageFolder.Path);
			await CampaignData.Instance.Init();
		}
	}
}
