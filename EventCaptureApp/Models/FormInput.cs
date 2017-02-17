using System;
using System.Collections.Generic;
using EventCaptureApp.Enums;
using Prism.Mvvm;

namespace EventCaptureApp
{
	public class FormInput: BindableBase
	{
		private string _value = string.Empty;

		public int Id { get; set; } = 0;

		public string Title { get; set; } = string.Empty;

		public FormInputType Type { get; set; } = FormInputType.TextField;

		public List<string> Values { get; set; } = new List<string>();

		public string Value
		{
			get { return _value; }
			set { this.SetProperty(ref _value, value); }
		}

		public int MinCharLength { get; set; } = 1;

		public int MaxCharLength { get; set; } = int.MaxValue;

		public bool IsRequired { get; set; } = false;
	}
}
