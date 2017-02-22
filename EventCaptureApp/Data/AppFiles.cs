using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using EventCaptureApp.Interfaces;
using EventCaptureApp.Models;
using PCLStorage;
using Xamarin.Forms;
using System.Linq;

namespace EventCaptureApp.Data
{
	public class AppFiles
	{
		private const string DownloadsFolderName = "downloads";
		private const string TempFolderName = "temp";
		private const string CampaignListFileName = "campaigns.json";
		private static AppFiles _instance;
		private IFolder _downloadsFolder;
		private IFolder _tempFolder;
		private IFile _campaignListFile;

		public static AppFiles Instance
		{
			get
			{
				if (_instance == null)
					_instance = new AppFiles();
				return _instance;
			}
		}

		public async Task Init()
		{
			_downloadsFolder = await this.LocalStorageFolder.CreateFolderAsync(DownloadsFolderName, CreationCollisionOption.OpenIfExists);
			_tempFolder = await this.LocalStorageFolder.CreateFolderAsync(TempFolderName, CreationCollisionOption.OpenIfExists);
			_campaignListFile = await this.LocalStorageFolder.CreateFileAsync(CampaignListFileName, CreationCollisionOption.OpenIfExists);
		}

		public IFolder LocalStorageFolder
		{
			get { return FileSystem.Current.LocalStorage; }
		}

		public IFolder DownloadsFolder
		{
			get { return _downloadsFolder; }
		}

		public IFolder TempFolder
		{
			get { return _tempFolder; }
		}

		public IFile CampaignListFile
		{
			get { return _campaignListFile; }
		}

		public string GetDownloadedFilePath(string fileName)
		{
			return System.IO.Path.Combine(this.DownloadsFolder.Path, fileName);
		}

		public async Task<bool> FileExists(string filePath)
		{
			IFile file = await FileSystem.Current.GetFileFromPathAsync(filePath);
			return file != null;
		}

		public DateTime GetFileModifiedDate(string filePath)
		{
			return DependencyService.Get<IAppFiles>().GetFileModifiedDate(filePath);;
		}

		public bool IsFileOutOfDate(FileReference file)
		{
			DateTime localFileDate = this.GetFileModifiedDate(file.LocalPath);
			return DateTime.Compare(localFileDate, file.DateModified) < 0;
		}
	}
}
