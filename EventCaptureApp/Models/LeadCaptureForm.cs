using System;
using System.Collections.Generic;

namespace EventCaptureApp.Models
{
	public class LeadCaptureForm
	{
		public string Title { get; set; } = string.Empty;

		public List<FormInput> Inputs { get; set; } = new List<FormInput>();
	}
}
