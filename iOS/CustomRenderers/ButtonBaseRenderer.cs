using System;
using System.Diagnostics;
using EventCaptureApp.Controls;
using EventCaptureApp.iOS;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(ButtonBase), typeof(ButtonBaseRenderer))]

namespace EventCaptureApp.iOS
{
	public class ButtonBaseRenderer: ButtonRenderer
	{
		public ButtonBaseRenderer()
		{
		}

		protected override void OnElementChanged(ElementChangedEventArgs<Xamarin.Forms.Button> e)
		{
			base.OnElementChanged(e);
			if (this.Element != null)
			{
				ButtonBase buttonBase = (ButtonBase)this.Element;
				Thickness padding = buttonBase.Padding;
				this.Control.ContentEdgeInsets = new UIKit.UIEdgeInsets(new nfloat(padding.Top), new nfloat(padding.Left), new nfloat(padding.Bottom), new nfloat(padding.Right));
			}
		}
	}
}
