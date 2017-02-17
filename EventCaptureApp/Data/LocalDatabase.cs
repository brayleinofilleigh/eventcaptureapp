using System;
using System.Threading.Tasks;
using EventCaptureApp.Models;
using SQLite;

namespace EventCaptureApp.Data
{
	public class LocalDatabase
	{
		private static LocalDatabase _instance;
		private SQLiteAsyncConnection _dbConn;

		public static LocalDatabase Instance
		{
			get
			{
				if (_instance == null)
					_instance = new LocalDatabase();
				return _instance;
			}
		}

		public async Task Init(string localStoragePath)
		{
			string dbPath = System.IO.Path.Combine(localStoragePath, AppConstants.LocalDatabaseName);
			_dbConn = new SQLiteAsyncConnection(dbPath);
			await _dbConn.CreateTableAsync<Lead>();
		}

		public AsyncTableQuery<Lead> LeadsTable
		{
			get { return _dbConn.Table<Lead>(); }
		}

		public SQLiteAsyncConnection Connection
		{
			get { return _dbConn; }
		}
	}
}
