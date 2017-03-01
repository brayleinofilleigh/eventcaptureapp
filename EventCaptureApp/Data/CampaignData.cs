using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using EventCaptureApp.Models;
using EventCaptureApp.Services;
using Newtonsoft.Json;
using PCLStorage;
using System.Linq;
using System.Diagnostics;
using Plugin.Connectivity;
using Plugin.Settings;

namespace EventCaptureApp.Data
{
	public class CampaignData
	{
		private static readonly string CurrentCampaignIdKey = "currentCampaignId";
		private static CampaignData _instance;
		private List<CampaignOverview> _campaigns = new List<CampaignOverview>();
		private Campaign _current;

		public static CampaignData Instance
		{
			get
			{
				if (_instance == null)
					_instance = new CampaignData();
				return _instance;
			}
		}

		public async Task Init()
		{
			string content = await AppFiles.Instance.CampaignListFile.ReadAllTextAsync();
			if (!string.IsNullOrEmpty(content))
			{
				_campaigns = JsonConvert.DeserializeObject<List<CampaignOverview>>(content);
				CampaignOverview campaign = _campaigns.Where(x => x.Id == this.CurrentCampaignId).FirstOrDefault();
				if (campaign != null)
					await this.SetCurrent(campaign);
			}
		}

		public async Task<List<CampaignOverview>> GetCampaigns()
		{
			if (CrossConnectivity.Current.IsConnected)
			{
				//RequestModelBase request = new RequestModelBase { AuthToken = AdminData.Instance.AuthToken };
				//RestResponse response = await RestService.Instance.ExecRequest(AppConstants.GetCampaignsUrl, request);
				RestResponse response = await RestService.Instance.ExecRequest(AppConstants.GetCampaignsUrl);

				if (response.RequestSuccess)
				{
					_campaigns = JsonConvert.DeserializeObject<List<CampaignOverview>>(response.Content);
					await AppFiles.Instance.CampaignListFile.WriteAllTextAsync(response.Content);
				}
			}
			return _campaigns;
		}

		public async Task<bool> SetCurrent(CampaignOverview campaign)
		{
			string configFilePath = AppFiles.Instance.GetDownloadedFilePath(campaign.ConfigFileName); 
			bool configFileExists = await AppFiles.Instance.FileExists(configFilePath);
			if (configFileExists)
			{
				IFile configFile = await FileSystem.Current.GetFileFromPathAsync(configFilePath);
				string content = await configFile.ReadAllTextAsync();
				this.Current = JsonConvert.DeserializeObject<Campaign>(content);
			}
			return configFileExists;
		}

		public Campaign Current
		{
			get { return _current; }
			private set 
			{
				_current = value;
				this.CurrentCampaignId = value.Id;
			}
		}

		public async Task<List<FileReference>> GetCampaignUpdateFileList(int campaignId)
		{
			List<FileReference> fileList = new List<FileReference>();
			//FileListRequest request = new FileListRequest() { AuthToken = AdminData.Instance.AuthToken, CampaignId = campaignId };
			//RestResponse response = await RestService.Instance.ExecRequest(AppConstants.GetCampaignFileListUrl, request);
			RestResponse response = await RestService.Instance.ExecRequest(AppConstants.GetCampaignFileListUrl);

			if (response.RequestSuccess)
			{
				fileList = JsonConvert.DeserializeObject<List<FileReference>>(response.Content);
				foreach(FileReference file in fileList)
				{
					file.LocalFolderPath = AppFiles.Instance.DownloadsFolder.Path;
					file.IsOutOfDate = AppFiles.Instance.IsFileOutOfDate(file);
				}
			}
			return fileList.Where(x => x.IsOutOfDate == true).OrderBy(x => x.Extension == ".sqlite" || x.Extension == ".json").ToList();
		}

		public async Task<CampaignStats> GetCampaignStats(int campaignId)
		{
			CampaignStats stats = new CampaignStats();
			CampaignStatsRequest request = new CampaignStatsRequest() { AuthToken = AdminData.Instance.AuthToken, CampaignId = campaignId };
			RestResponse response = await RestService.Instance.ExecRequest(AppConstants.GetCampaignStatsUrl, request);
			if (response.RequestSuccess)
				stats = JsonConvert.DeserializeObject<CampaignStats>(response.Content);
			return stats;
		}

		private int CurrentCampaignId
		{
			get { return CrossSettings.Current.GetValueOrDefault<int>(CurrentCampaignIdKey, 0); }
			set { CrossSettings.Current.AddOrUpdateValue<int>(CurrentCampaignIdKey, value); }
		}
	}

	public class FileListRequest : RestRequestBase
	{
		public int CampaignId { get; set; } = 0;
	}

	public class CampaignStatsRequest: RestRequestBase
	{
		public int CampaignId { get; set; } = 0;
	}
}