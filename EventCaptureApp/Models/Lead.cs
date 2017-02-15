using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

namespace EventCaptureApp.Models
{
	public class Lead
	{
		public string Guid { get; set; } = string.Empty;

		public string FirstName { get; set; } = string.Empty;

		public string LastName { get; set; } = string.Empty;

		public string CompanyName { get; set; } = string.Empty;

		public string EmailAddress { get; set; } = string.Empty;

		public int CampaignId { get; set; } = 0;

		[JsonIgnore]
		public string DocumentsJson {
			get { return JsonConvert.SerializeObject(this.Documents); }
			set { this.Documents = JsonConvert.DeserializeObject<List<CampaignDocument>>(value); }
		}

		[SQLite.Ignore]
		public List<CampaignDocument> Documents { get; set; } = new List<CampaignDocument>();

		public bool IsSynced { get; set; } = false;
	}
}
