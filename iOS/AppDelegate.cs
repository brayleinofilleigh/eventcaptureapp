using System;
using System.Collections.Generic;
using System.Linq;

using Foundation;
using UIKit;
using Prism.Unity;
using Microsoft.Azure.Mobile;

namespace EventCaptureApp.iOS
{
	[Register("AppDelegate")]
	public partial class AppDelegate : global::Xamarin.Forms.Platform.iOS.FormsApplicationDelegate
	{
		public override bool FinishedLaunching(UIApplication app, NSDictionary options)
		{
			global::Xamarin.Forms.Forms.Init();
			MobileCenter.Configure(AppConstants.MobileCenterId);

			LoadApplication(new App());

			return base.FinishedLaunching(app, options);
		}
	}
}
