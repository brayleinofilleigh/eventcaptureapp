using System;
using System.Diagnostics;
using System.Threading.Tasks;
using EventCaptureApp.Data;
using EventCaptureApp.Helpers;
using EventCaptureApp.Models;
using Prism.Commands;
using Prism.Navigation;
using Plugin.Connectivity;

namespace EventCaptureApp.ViewModels
{
	public class LoginPageViewModel: ViewModelBase
	{
		private INavigationService _navigationService;
		private string _emailAddress = string.Empty;
		private string _password = string.Empty;
		private bool _isEmailAddressInvalid = false;
		private bool _isPasswordInvalid = false;
		public DelegateCommand SubmitCommand { get; private set; }

		public LoginPageViewModel(INavigationService navigationService)
		{
			_navigationService = navigationService;
			this.SubmitCommand = new DelegateCommand(async() => await OnSubmitCommand()).ObservesCanExecute((p) => IsNotBusy);

			this.EmailAddress = "twoollacott@brayleino.co.uk";
			this.Password = "password1234";
		}

		public string EmailAddress
		{
			get { return _emailAddress; }
			set { _emailAddress = value; }
		}

		public string Password
		{
			get { return _password; }
			set { _password = value; }
		}

		public bool IsEmailAddressInvalid
		{
			get { return _isEmailAddressInvalid; }
			set { this.SetProperty(ref _isEmailAddressInvalid, value); }
		}

		public bool IsPasswordInvalid
		{
			get { return _isPasswordInvalid; }
			set { this.SetProperty(ref _isPasswordInvalid, value); }
		}

		public bool IsValidForm
		{
			get
			{
				this.IsEmailAddressInvalid = !RegexHelper.IsValidEmail(this.EmailAddress);
				this.IsPasswordInvalid = this.Password.Length < 8;
				return !this.IsEmailAddressInvalid && !this.IsPasswordInvalid;
			}
		}

		public async Task OnSubmitCommand()
		{
			if (!this.IsInternetAvailable)
			{
				await this.DisplayAlert("Internet connection unavailable", "Please ensure you have internet connection before continuing", "OK");
				return;
			}
			if (this.IsValidForm)
			{
				await this.SubmitAuthRequest();
			}
			else
			{
				await this.DisplayAlert("Form Incomplete", "Please ensure all required fields are filled correctly", "OK");
			}
		}

		public async Task SubmitAuthRequest()
		{
			this.IsBusy = true;
			AuthRequestResponse authResponse = await AdminData.Instance.GetAuthToken(this.EmailAddress, this.Password);
			this.IsBusy = false;
			if (AdminData.Instance.IsAuthenticated)
			{
				await _navigationService.NavigateAsync(AppPages.CampaignList.Name);
			}
			else
			{
				Debug.WriteLine($"Auth response: {authResponse.ErrorCode}");
			}
		}
	}
}
