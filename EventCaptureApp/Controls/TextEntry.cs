using System;
using System.Diagnostics;
using System.Windows.Input;
using EventCaptureApp.Enums;
using EventCaptureApp.Interfaces;
using EventCaptureApp.Styles;
using Xamarin.Forms;

namespace EventCaptureApp.Controls
{
	public class TextEntry: Entry, IFormInputControl
	{
		public TextEntry()
		{
			this.SetBinding(Entry.PlaceholderProperty, "Title");
			this.SetBinding(Entry.TextProperty, "Value");
		}

		public void SetProperties(FormInput properties)
		{
			this.BindingContext = properties;
			switch (properties.Type)
			{
				case FormInputType.TextField:
					this.Keyboard = Keyboard.Default;
					break;
				case FormInputType.EmailField:
					this.Keyboard = Keyboard.Email;
					break;
				case FormInputType.NumericField:
					this.Keyboard = Keyboard.Numeric;
					break;
			}
		}

		public void SetCommand(ICommand command, object parameter = null) { }

		public FormInput GetProperties()
		{
			return (FormInput)this.BindingContext;
		}

		public void Highlighted(bool value)
		{
			this.BackgroundColor = value ? AppColors.Blue : AppColors.White;
		}

		public bool IsValid()
		{
			bool validEntry = true;
			if (this.GetProperties().IsRequired)
				validEntry = this.Text.Length >= this.GetProperties().MinCharLength && this.Text.Length <= this.GetProperties().MaxCharLength;
			
			//if (this.GetProperties().Type == FormInputType.EmailField)
			return validEntry;
		}
	}
}
