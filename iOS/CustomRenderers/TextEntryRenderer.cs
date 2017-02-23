using System;
using System.Diagnostics;
using EventCaptureApp.Controls;
using EventCaptureApp.iOS;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(TextEntry), typeof(TextEntryRenderer))]

namespace EventCaptureApp.iOS
{
	public class TextEntryRenderer: EntryRenderer
	{
		public TextEntryRenderer()
		{
			//
		}

		protected override void OnElementChanged(ElementChangedEventArgs<Xamarin.Forms.Entry> e)
		{
			base.OnElementChanged(e);
			if (this.Element != null)
			{
				TextEntry textEntry = (TextEntry)this.Element;
				if (textEntry.GetNextInputControl() == null)
				{
					this.Control.ReturnKeyType = UIKit.UIReturnKeyType.Done;
				}
				else {
					this.Control.ReturnKeyType = UIKit.UIReturnKeyType.Next;
				}
			}
		}
	}
}
