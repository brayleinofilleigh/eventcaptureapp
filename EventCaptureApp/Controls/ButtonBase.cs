using System;
using Xamarin.Forms;

namespace EventCaptureApp.Controls
{
	public class ButtonBase: Button
	{
		public ButtonBase()
		{
			//
		}

		public int Index { get; set; } = 0;

		public Thickness Padding { get; set; } = new Thickness(0);
	}
}
