using System;
using System.Collections.Generic;
using EventCaptureApp.Models;
using System.Linq;

namespace EventCaptureApp
{
	public class Campaign: CampaignOverview
	{
		public List<CampaignCategory> Categories { get; set; } = new List<CampaignCategory>();

		public LeadCaptureForm LeadCaptureForm { get; set; } = new LeadCaptureForm();

		///////////////////////////////////

		[Newtonsoft.Json.JsonIgnore]
		public List<CampaignDocument> Documents
		{
			get { return this.Categories.SelectMany(x => x.Documents).ToList(); }
		}

		[Newtonsoft.Json.JsonIgnore]
		public List<CampaignDocument> SelectedDocuments
		{
			get { return this.Documents.Where(x => x.IsSelected == true).ToList(); }
		}
	}
}
