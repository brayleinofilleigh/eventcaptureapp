using System;
using System.Diagnostics;
using System.Threading.Tasks;
using EventCaptureApp.Data;
using EventCaptureApp.Services;
using EventCaptureApp.ViewModels;
using Prism.Commands;
using Prism.Navigation;
using Xamarin.Forms;

namespace EventCaptureApp.ViewModels
{
	public class LoginPageViewModel: ViewModelBase
	{
		private INavigationService _navigationService;
		private bool _isFormValid = false;
		private string _emailAddress = string.Empty;
		private string _password = string.Empty;
		public DelegateCommand SubmitCommand { get; private set; }

		public LoginPageViewModel(INavigationService navigationService)
		{
			_navigationService = navigationService;
			this.SubmitCommand = new DelegateCommand(async() => await OnSubmitCommand()).ObservesCanExecute((p) => IsFormValid);
		}

		public string EmailAddress
		{
			get { return _emailAddress; }
			set
			{
				_emailAddress = value;
				this.CheckFormValidation();
			}
		}

		public string Password
		{
			get { return _password; }
			set
			{
				_password = value;
				this.CheckFormValidation();
			}
		}

		public bool IsFormValid
		{
			get { return _isFormValid; }
			set { this.SetProperty(ref _isFormValid, value); }
		}

		public void CheckFormValidation()
		{
			this.IsFormValid = this.EmailAddress.Length > 2 && this.Password.Length > 2;
		}

		public async Task OnSubmitCommand()
		{
			this.IsBusy = true;
			AuthResponse response = await AuthService.RequestAuthToken(EmailAddress, Password);
			this.IsBusy = false;
			if (!string.IsNullOrEmpty(response.AuthToken))
			{
				AppSettings.AuthToken = response.AuthToken;
				await _navigationService.NavigateAsync(AppPages.CampaignList.Name);
			}
			else {
				Debug.WriteLine(response.ErrorCode);
			}
			//await _navigationService.NavigateAsync(AppPages.Home.Name);
		}
	}
}
