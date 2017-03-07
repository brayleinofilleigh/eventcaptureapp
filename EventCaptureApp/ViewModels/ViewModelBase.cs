using System;
using System.Threading.Tasks;
using Plugin.Connectivity;
using Prism.Mvvm;
using Prism.Navigation;

namespace EventCaptureApp.ViewModels
{
	public class ViewModelBase: BindableBase, INavigationAware
	{
		private bool _isBusy = false;
		private bool _isNotBusy = true;

		public ViewModelBase()
		{
		}

		public virtual void OnNavigatedFrom(NavigationParameters parameters)
		{
			//
		}

		public virtual void OnNavigatingTo(NavigationParameters parameters)
		{
			//
		}

		public virtual void OnNavigatedTo(NavigationParameters parameters)
		{
			//
		}

		public async Task DisplayAlert(string title, string message, string cancel)
		{
			await App.Current.MainPage.DisplayAlert(title, message, cancel);
		}

		public bool IsInternetAvailable
		{
			get { return CrossConnectivity.Current.IsConnected; }
		}

		public bool IsBusy 
		{ 
			get { return _isBusy; }
			set 
			{
				this.SetProperty(ref _isBusy, value);
				this.SetProperty(ref _isNotBusy, !value);
			}
		}

		public bool IsNotBusy
		{
			get { return _isNotBusy; }
		}
	}
}
