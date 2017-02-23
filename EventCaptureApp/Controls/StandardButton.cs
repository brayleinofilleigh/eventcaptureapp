using System;
using Xamarin.Forms;

namespace EventCaptureApp.Controls
{
	public class StandardButton: ButtonBase
	{
		public StandardButton()
		{
			this.TextColor = this.BorderColor = Color.Teal;
			this.BorderWidth = 1.0;
			this.Padding = new Thickness(20, 0, 20, 0);
			this.HorizontalOptions = LayoutOptions.CenterAndExpand;
		}
	}
}
