using System;
using System.Collections.Generic;
using EventCaptureApp.Enums;

namespace EventCaptureApp
{
	public class FormInput
	{
		public int Id { get; set; } = 0;

		public string Title { get; set; } = string.Empty;

		public FormInputType Type { get; set; } = FormInputType.TextField;

		public List<string> Values { get; set; } = new List<string>();

		public string Value { get; set; } = string.Empty;

		public int MinCharLength { get; set; } = int.MinValue;

		public int MaxCharLength { get; set; } = int.MaxValue;

		public bool IsRequired { get; set; } = false;
	}
}
