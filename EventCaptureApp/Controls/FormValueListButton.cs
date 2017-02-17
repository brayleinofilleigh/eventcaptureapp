using System;
using System.Windows.Input;
using EventCaptureApp.Interfaces;
using Xamarin.Forms;

namespace EventCaptureApp.Controls
{
	public class FormValueListButton: Button, IFormInputControl
	{
		public FormValueListButton()
		{
			this.SetBinding(Button.TextProperty, "Title");
		}

		public void SetProperties(FormInput properties)
		{
			this.BindingContext = properties;
		}

		public void SetCommand(ICommand command, object parameter = null)
		{
			this.Command = command;
			this.CommandParameter = parameter;
		}

		public FormInput GetProperties()
		{
			return (FormInput)this.BindingContext;
		}

		public void Highlighted(bool value) { }

		public bool IsValid()
		{
			return true;
		}
	}
}
