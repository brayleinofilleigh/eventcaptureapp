using System;
using EventCaptureApp.Enums;

namespace EventCaptureApp
{
	public class FormInput
	{
		public string Title { get; set; } = string.Empty;

		public string Description { get; set; } = string.Empty;

		public FormInputType Type { get; set; } = FormInputType.TextEntry;
	}
}
