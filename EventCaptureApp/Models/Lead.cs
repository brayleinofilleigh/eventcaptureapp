using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace EventCaptureApp.Models
{
	public class Lead
	{
		public string Guid { get; set; } = string.Empty;

		public int CampaignId { get; set; } = 0;

		public bool IsSynced { get; set; } = false;

		[SQLite.Ignore]
		public List<FormInputResult> CaptureFormResults { get; set; } = new List<FormInputResult>();

		[JsonIgnore]
		public string CaptureFormResultsJson
		{
			get { return JsonConvert.SerializeObject(this.CaptureFormResults); }
			set { this.CaptureFormResults = JsonConvert.DeserializeObject<List<FormInputResult>>(value); }
		}

		[SQLite.Ignore]
		public List<int> DocumentIds { get; set; } = new List<int>();

		[JsonIgnore]
		public string DocumentIdsJson 
		{
			get { return JsonConvert.SerializeObject(this.DocumentIds); }
			set { this.DocumentIds = JsonConvert.DeserializeObject<List<int>>(value); }
		}
	}
}
