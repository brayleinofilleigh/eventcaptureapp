using System;
using System.Windows.Input;
using EventCaptureApp.Interfaces;
using Xamarin.Forms;

namespace EventCaptureApp.Controls
{
	public class FormSwitch: Switch, IFormInputControl
	{
		public FormSwitch()
		{
			this.SetBinding(Switch.IsToggledProperty, "Value");
		}

		public void SetProperties(FormInput properties)
		{
			this.BindingContext = properties;
		}

		public void SetCommand(ICommand command, object parameter = null) { }

		public FormInput GetProperties()
		{
			return (FormInput)this.BindingContext;
		}

		public void InvalidHighlight(bool value) { }

		public bool IsValid()
		{
			return true;
		}

		public void SetNextInputControl(IFormInputControl inputControl) { }

		public IFormInputControl GetNextInputControl()
		{
			return null;
		}

		public void SetFocus() { }
	}
}
