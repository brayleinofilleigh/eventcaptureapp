using System;

namespace EventCaptureApp.Models
{
	public class CampaignOverview
	{
		public int Id { get; set; } = 0;

		public string Title { get; set; } = string.Empty;

		public DateTime DateModified { get; set; } = DateTime.MinValue;

		public string ConfigFileName { get; set; } = string.Empty;
	}
}
