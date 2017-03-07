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
		public static readonly BindableProperty HighlightAsInvalidProperty = BindableProperty.Create(nameof(HighlightAsInvalid), typeof(bool), typeof(TextEntry), false, propertyChanged: OnHighlightAsInvalidChanged);
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

		public bool HighlightAsInvalid
		{
			get { return (bool)GetValue(HighlightAsInvalidProperty); }
			set { SetValue(HighlightAsInvalidProperty, value); }
		}

		private static void OnHighlightAsInvalidChanged(BindableObject bindable, object oldValue, object newValue)
		{
			if (newValue != null)
			{
				TextEntry textEntry = (TextEntry)bindable;
				textEntry.BackgroundColor = (bool)newValue ? Color.Aqua : AppColors.White;
			}
		}

		public void SetProperties(FormInput properties)
		{
			this.BindingContext = properties;
			this.IsRequired = properties.IsRequired;
			this.MinCharLength = properties.MinCharLength;
			this.MaxCharLength = properties.MaxCharLength;
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

		public void SetAsInvalid(bool value)
		{
			this.HighlightAsInvalid = value;
		}

		public bool GetIsEntryValid()
		{
			bool validEntry = true;
			if (this.IsRequired)
			{
				if (this.Keyboard == Keyboard.Email)
				{
					validEntry = RegexHelper.IsValidEmail(this.Text);
				}
				else
				{
					validEntry = this.Text.Length >= this.GetProperties().MinCharLength && this.Text.Length <= this.GetProperties().MaxCharLength;
				}
			}
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

		public int MinCharLength { get; set; } = 1;

		public int MaxCharLength { get; set; } = int.MaxValue;

		public bool IsRequired { get; set; } = false;

		public void Dispose()
		{
			this.Completed -= OnCompleteEvent;
		}
	}
}
