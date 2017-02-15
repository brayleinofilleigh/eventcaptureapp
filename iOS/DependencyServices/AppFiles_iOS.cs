using System;
using EventCaptureApp.DependencyService.iOS;
using EventCaptureApp.Helpers.iOS;
using EventCaptureApp.Interfaces;
using Foundation;
using Xamarin.Forms;

[assembly: Dependency(typeof(AppFiles_iOS))]

namespace EventCaptureApp.DependencyService.iOS
{
	public class AppFiles_iOS: IAppFiles
	{
		public AppFiles_iOS()
		{
		}

		public bool FileExists(string filePath)
		{
			return NSFileManager.DefaultManager.FileExists(filePath);
		}

		public DateTime GetFileModifiedDate(string filePath)
		{
			DateTime fileDate = DateTime.MinValue;
			NSError nsError = new NSError();
			if (this.FileExists(filePath))
			{
				NSDate nsDate = NSFileManager.DefaultManager.GetAttributes(filePath, out nsError).ModificationDate;
				fileDate = DateUtils.NSDateToDateTime(nsDate);
			}
			return fileDate;
		}
	}
}
