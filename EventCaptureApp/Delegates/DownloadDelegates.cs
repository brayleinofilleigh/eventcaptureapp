using System;

namespace EventCaptureApp.Delegates
{
	public delegate void DownloadProgressDelegate(object sender, long totalBytesWritten, int percentDownloaded = 0);

	public delegate void DownloadErrorDelegate(object sender, int errorCode);
}

