using System;
using System.Collections.Generic;

namespace EventCaptureApp.Models
{
	public class LeadCaptureForm
	{
		public string Title { get; set; } = string.Empty;

		public string BodyText { get; set; } = string.Empty;

		public List<FormInput> FormInputs { get; set; } = new List<FormInput>();
	}
}
