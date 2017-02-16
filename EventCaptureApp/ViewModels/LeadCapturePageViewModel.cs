using System;
using System.Collections.Generic;
using System.Diagnostics;
using EventCaptureApp.Data;
using EventCaptureApp.Interfaces;
using EventCaptureApp.Models;
using Prism.Commands;

namespace EventCaptureApp.ViewModels
{
	public class LeadCapturePageViewModel: ViewModelBase
	{
		private List<IFormInputControl> _inputControls;
		private List<string> _selectedValueList;
		public DelegateCommand SubmitCommand { get; set; }

		public LeadCapturePageViewModel()
		{
			_inputControls = new List<IFormInputControl>();
			foreach (FormInput formInput in this.LeadCaptureForm.FormInputs)
				_inputControls.Add( AppFormInputs.GetInputControlByType(formInput.Type, formInput) );
			
			this.SubmitCommand = new DelegateCommand(this.OnSubmitCommand);
		}

		public LeadCaptureForm LeadCaptureForm
		{
			get { return CampaignData.Instance.Current.LeadCaptureForm; }
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

		protected void OnSubmitCommand()
		{
			foreach (IFormInputControl inputControl in this.InputControls)
			{
				//inputControl.Highlighted(!inputControl.IsValid());
				Debug.WriteLine(inputControl.GetProperties().Title);
				Debug.WriteLine(inputControl.GetProperties().Value);
				Debug.WriteLine("//////////");
			}
		}
	}
}
