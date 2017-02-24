using System;
using System.Collections.Generic;
using EventCaptureApp.Models;
using System.Linq;

namespace EventCaptureApp
{
	public class Campaign: CampaignOverview
	{
		public string LogoFileName { get; set; } = string.Empty;

		public string HeaderBgColor { get; set; } = "#ffffff";

		public string HeaderTextColor { get; set; } = "#000000";

		public string ButtonBorderColor { get; set; } = "#000000";

		public string ButtonTextColor { get; set; } = "#000000";

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

		[Newtonsoft.Json.JsonIgnore]
		public List<int> SelectedDocumentIds
		{
			get { return this.SelectedDocuments.Select(x => x.Id).ToList(); }
		}

		public void ClearSelectedDocuments()
		{
			foreach (CampaignDocument document in this.SelectedDocuments)
				document.IsSelected = false;
		}
	}
}
