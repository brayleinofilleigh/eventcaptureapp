using System;
using System.Windows.Input;

namespace EventCaptureApp.Interfaces
{
	public interface IFormInputControl
	{
		void SetProperties(FormInput properties);
		void SetCommand(ICommand command, object parameter = null);
		FormInput GetProperties();
		void Highlighted(bool value);
		bool IsValid();
	}
}
