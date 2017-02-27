using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using EventCaptureApp.Data;
using EventCaptureApp.Models;
using EventCaptureApp.Services;
using Newtonsoft.Json;
using System.Linq;

namespace EventCaptureApp.Data
{
	public class LeadsData
	{
		private static LeadsData _instance;

		public static LeadsData Instance
		{
			get
			{
				if (_instance == null)
					_instance = new LeadsData();
				return _instance;
			}
		}

		public async Task<List<Lead>> GetUnsyncedLeads()
		{
			return await LocalDatabase.Instance.LeadsTable.Where(x => x.IsSynced == false).ToListAsync();
		}

		public async Task<bool> SyncLeads()
		{
			bool syncSuccess = true;
			List<Lead> leads = await this.GetUnsyncedLeads();
			if (leads.Any())
			{
				LeadSyncRequest leadSycnRequest = new LeadSyncRequest() { AuthToken = AdminData.Instance.AuthToken, Leads = leads };
				RestResponse syncResponse = await RestService.Instance.ExecRequest(AppConstants.SaveNewLeadsUrl, leadSycnRequest);
				syncSuccess = syncResponse.RequestSuccess;
			}
			leads.Clear();
			return syncSuccess;
		}

		public async Task<int> SaveLead(int campaignId, List<FormInputResult> captureFormResults, List<int> documentIds)
		{
			Lead lead = new Lead()
			{
				Guid = Guid.NewGuid().ToString(),
				CampaignId = campaignId,
				CaptureFormResults = captureFormResults,
				DocumentIds = documentIds
			};
			int insertId = await LocalDatabase.Instance.Connection.InsertAsync(lead);
			return insertId;
		}
	}

	public class LeadSyncRequest: RestRequestBase
	{
		public List<Lead> Leads { get; set; } = new List<Lead>();
	}
}
