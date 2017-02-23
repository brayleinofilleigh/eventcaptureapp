using System;
using System.Globalization;
using System.Text.RegularExpressions;

namespace EventCaptureApp.Helpers
{
	public class RegexHelper
	{
		public static bool IsValidEmail (string value)
		{
			if (String.IsNullOrEmpty (value))
				return false;
			try {
				return Regex.IsMatch (value,
					@"^(?("")("".+?(?<!\\)""@)|(([0-9a-z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-z])@))" +
					@"(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-z][-\w]*[0-9a-z]*\.)+[a-z0-9][\-a-z0-9]{0,22}[a-z0-9]))$",
					RegexOptions.IgnoreCase, TimeSpan.FromMilliseconds (250));
			} catch (RegexMatchTimeoutException) {
				return false;
			}
		}

		public static string RemoveNonStandardChars (string value)
		{
			if (string.IsNullOrEmpty (value)) {
				return value;
			} else {
				return Regex.Replace (value, @"[^a-zA-Z\s+]", "");
			}
		}
	}
}

