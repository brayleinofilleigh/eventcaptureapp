﻿using System;
namespace EventCaptureApp.Models
{
	public class CampaignStats: CampaignOverview
	{
		public int NumberInstalledDevices { get; set; } = 0;

		public int NumberSubmittedLeads { get; set; } = 0;
	}
}
