using System;
using Newtonsoft.Json;
using Prism.Mvvm;

namespace EventCaptureApp.Models
{
	public class CampaignDocument: BindableBase
	{
		private bool _isSelected = false;

		public int Id { get; set; } = 0;

		public string Title { get; set; } = string.Empty;

		public string FileName { get; set; } = string.Empty;

		[JsonIgnore]
		public bool IsSelected
		{
			get { return _isSelected; }
			set { this.SetProperty(ref _isSelected, value); }
		}
	}
}
