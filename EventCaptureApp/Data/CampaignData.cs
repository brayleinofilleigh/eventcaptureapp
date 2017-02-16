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

namespace EventCaptureApp.Data
{
	public class CampaignData
	{
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
				CampaignOverview campaign = _campaigns.Where(x => x.Id == AppSettings.CurrentCampaignId).FirstOrDefault();
				if (campaign != null)
					await this.SetCurrent(campaign);
			}
		}

		public async Task<List<CampaignOverview>> GetCampaigns()
		{
			if (CrossConnectivity.Current.IsConnected)
			{
				//RequestModelBase request = new RequestModelBase { AuthToken = AppSettings.AuthToken };
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
			Debug.WriteLine($"Setting Campaign: {campaign.Title}");

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
				AppSettings.CurrentCampaignId = value.Id;
			}
		}

		public async Task<List<FileReference>> GetUpdateFileList(int campaignId)
		{
			List<FileReference> remoteFileList = new List<FileReference>();
			FileListRequest request = new FileListRequest() { AuthToken = AppSettings.AuthToken, CampaignId = campaignId };
			RestResponse response = await RestService.Instance.ExecRequest(AppConstants.GetCampaignFileListUrl, request);
			if (response.RequestSuccess)
				remoteFileList = JsonConvert.DeserializeObject<List<FileReference>>(response.Content);
			return AppFiles.Instance.GetFilesToUpdate(remoteFileList);
		}
	}

	public class FileListRequest : RequestModelBase
	{
		public int CampaignId { get; set; } = 0;
	}
}
