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
		private Lead _current;

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
				RestResponse syncResponse = await RestService.Instance.ExecRequest(AppConstants.SaveNewLeadsUrl, leads);
				syncSuccess = syncResponse.RequestSuccess;
			}
			return syncSuccess;
		}

		public void StartNewLead()
		{
			this.Current = new Lead { Guid = Guid.NewGuid().ToString() };
		}

		public Lead Current
		{
			get { return _current; }
			private set { _current = value; }
		}
	}
}
