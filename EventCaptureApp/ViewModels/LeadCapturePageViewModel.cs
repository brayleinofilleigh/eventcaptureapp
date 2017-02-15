using System;
using EventCaptureApp.Data;
using EventCaptureApp.Models;

namespace EventCaptureApp.ViewModels
{
	public class LeadCapturePageViewModel: ViewModelBase
	{
		public LeadCapturePageViewModel()
		{
		}

		public LeadCaptureForm LeadCaptureForm
		{
			get { return CampaignData.Instance.Current.LeadCaptureForm; }
		}
	}
}
