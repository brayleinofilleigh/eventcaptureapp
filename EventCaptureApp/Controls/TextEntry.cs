using System;
using System.Diagnostics;
using System.Windows.Input;
using EventCaptureApp.Enums;
using EventCaptureApp.Helpers;
using EventCaptureApp.Interfaces;
using EventCaptureApp.Styles;
using Xamarin.Forms;

namespace EventCaptureApp.Controls
{
	public class TextEntry: Entry, IFormInputControl, IDisposable
	{
		private IFormInputControl _nextInputControl;

		public TextEntry()
		{
			this.SetBinding(Entry.PlaceholderProperty, "Title");
			this.SetBinding(Entry.TextProperty, "Value");
			this.Completed += OnCompleteEvent;
		}

		protected void OnCompleteEvent(object sender, EventArgs e)
		{
			if (this.GetNextInputControl() != null)
				this.GetNextInputControl().SetFocus();
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

		public void InvalidHighlight(bool value)
		{
			this.BackgroundColor = value ? AppColors.White : Color.Aqua;
		}

		public bool IsValid()
		{
			bool validEntry = true;
			if (this.GetProperties().IsRequired)
			{
				switch (this.GetProperties().Type)
				{
					case FormInputType.EmailField:
						validEntry = RegexHelper.IsValidEmail(this.Text);
						break;
					default:
						validEntry = this.Text.Length >= this.GetProperties().MinCharLength && this.Text.Length <= this.GetProperties().MaxCharLength;
						break;
				}
			}
			this.InvalidHighlight(validEntry);
			return validEntry;
		}

		public void SetNextInputControl(IFormInputControl inputControl)
		{
			_nextInputControl = inputControl;
		}

		public IFormInputControl GetNextInputControl()
		{
			return _nextInputControl;
		}

		public void SetFocus()
		{
			this.Focus();
		}

		public IFormInputControl NextInputControl
		{
			get { return this.GetNextInputControl(); }
			set { this.SetNextInputControl(value); }
		}

		public void Dispose()
		{
			this.Completed -= OnCompleteEvent;
		}
	}
}
