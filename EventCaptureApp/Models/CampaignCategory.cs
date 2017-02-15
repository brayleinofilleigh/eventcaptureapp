using System;
using System.Collections.Generic;

namespace EventCaptureApp.Models
{
	public class CampaignCategory
	{
		public int Id { get; set; } = 0;

		public string Title { get; set; } = string.Empty;

		public string ThumbImageName { get; set; } = string.Empty;

		public List<CampaignDocument> Documents { get; set; } = new List<CampaignDocument>();
	}
}
