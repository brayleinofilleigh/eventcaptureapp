using System;
using EventCaptureApp.Enums;

namespace EventCaptureApp.Delegates
{
	public delegate void DownloadEventDelegate(object sender, DownloadEventType type, long totalBytesWritten, int percentDownloaded = 0, int errorCode = 0);
}

