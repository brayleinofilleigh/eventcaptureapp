using System;
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

		public virtual void OnNavigatedTo(NavigationParameters parameters)
		{
			//
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
