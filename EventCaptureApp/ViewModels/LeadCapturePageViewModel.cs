using System;
using System.Collections.Generic;
using EventCaptureApp.Data;
using EventCaptureApp.Interfaces;
using EventCaptureApp.Models;
using Prism.Commands;
using System.Threading.Tasks;
using EventCaptureApp.Enums;
using Prism.Navigation;

namespace EventCaptureApp.ViewModels
{
	public class LeadCapturePageViewModel: ViewModelBase
	{
		private List<IFormInputControl> _inputControls;
		private List<string> _selectedValueList;
		private bool _isValueListShown = false;
		private IFormInputControl _selectedInputControl;
		private string _selectedValueListItem;
		private INavigationService _navigationService;
		public DelegateCommand<IFormInputControl> ShowValueListCommand { get; private set; }
		public DelegateCommand SubmitCommand { get; private set; }

		public LeadCapturePageViewModel(INavigationService navigationService)
		{
			_navigationService = navigationService;
			_inputControls = new List<IFormInputControl>();
			this.ShowValueListCommand = new DelegateCommand<IFormInputControl>(this.OnShowValueListCommand);
			this.SubmitCommand = new DelegateCommand(async () => await this.OnSubmitCommand());
			this.AddFormInputControls();
		}

		protected void AddFormInputControls()
		{
			IFormInputControl prevFormInput = null;
			foreach (FormInput formInput in this.LeadCaptureForm.FormInputs)
			{
				IFormInputControl inputControl = AppFormInputs.GetInputControlByType(formInput.Type, formInput);
				if (prevFormInput != null) prevFormInput.SetNextInputControl(inputControl);
				if (formInput.Type == FormInputType.ScrollList) inputControl.SetCommand(this.ShowValueListCommand, inputControl);
				_inputControls.Add(inputControl);
				prevFormInput = inputControl;
			}
		}

		public Campaign Campaign 
		{
			get { return CampaignData.Instance.Current; }
		}

		public LeadCaptureForm LeadCaptureForm
		{
			get { return this.Campaign.LeadCaptureForm; }
		}

		public List<IFormInputControl> InputControls
		{
			get { return _inputControls; }
		}

		public List<string> SelectedValueList
		{
			get { return _selectedValueList; }
			set { this.SetProperty(ref _selectedValueList, value); }
		}

		public string SelectedValueListItem
		{
			get { return _selectedValueListItem; }
			set 
			{
				if (value != null)
				{
					_selectedValueListItem = value;
					_selectedInputControl.GetProperties().Value = value;
					this.IsValueListShown = false;
				}
			}
		}

		public bool IsValueListShown
		{
			get { return _isValueListShown; }
			set { this.SetProperty(ref _isValueListShown, value); }
		}

		protected void OnShowValueListCommand(IFormInputControl inputControl)
		{
			_selectedInputControl = inputControl;
			this.SelectedValueList = inputControl.GetProperties().Values;
			this.IsValueListShown = true;
		}

		protected async Task OnSubmitCommand()
		{
			Tuple<bool, List<FormInputResult>> formResults = this.GetFormResults();
			if (formResults.Item1)
			{
				this.IsBusy = true;
				await LeadsData.Instance.SaveLead(this.Campaign.Id, formResults.Item2, this.Campaign.SelectedDocumentIds);
				this.IsBusy = false;
				await _navigationService.NavigateAsync(AppPages.LeadSubmitted.Name);
			}
			else
			{
				await this.DisplayAlert("Form Incomplete", "Please ensure all required fields are filled correctly", "OK");
			}
		}

		protected Tuple<bool, List<FormInputResult>> GetFormResults() {
			bool validForm = true;
			List<FormInputResult> inputResults = new List<FormInputResult>();
			foreach (IFormInputControl inputControl in this.InputControls)
			{
				inputResults.Add( new FormInputResult() { Id = inputControl.GetProperties().Id, Value = inputControl.GetProperties().Value } );
				bool validEntry = inputControl.GetIsEntryValid();
				inputControl.SetAsInvalid(!validEntry);
				if (!validEntry) validForm = false;
			}
			return new Tuple<bool, List<FormInputResult>>(validForm, inputResults);
		}
	}
}