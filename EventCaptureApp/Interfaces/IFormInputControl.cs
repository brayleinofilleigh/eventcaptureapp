using System;
using System.Windows.Input;

namespace EventCaptureApp.Interfaces
{
	public interface IFormInputControl
	{
		void SetProperties(FormInput properties);
		void SetCommand(ICommand command, object parameter = null);
		FormInput GetProperties();
		void SetAsInvalid(bool value);
		bool GetIsEntryValid();
		void SetNextInputControl(IFormInputControl inputControl);
		void SetFocus();
		IFormInputControl GetNextInputControl();
	}
}
