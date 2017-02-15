using System;
using Foundation;

namespace EventCaptureApp.Helpers.iOS
{
	public class DateUtils
	{
		public static DateTime NSDateToDateTime(NSDate nsDate)
		{
			DateTime dateTime = new DateTime(2001, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
			return dateTime.AddSeconds(nsDate.SecondsSinceReferenceDate).ToLocalTime();
		}
	}
}

