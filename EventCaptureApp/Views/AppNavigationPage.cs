using System;
using EventCaptureApp.Styles;
using Xamarin.Forms;

namespace EventCaptureApp.Views
{
	public class AppNavigationPage: NavigationPage
	{
		public AppNavigationPage()
		{
			this.BarBackgroundColor = AppColors.Blue;
			this.BarTextColor = AppColors.White;
		}
	}
}
