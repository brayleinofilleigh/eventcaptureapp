using System;
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

		public void SetProperties(FormInput value)
		{
			this.BindingContext = value;
		}

		public FormInput GetProperties()
		{
			return (FormInput)this.BindingContext;
		}

		public void Highlighted(bool value)
		{
			//
		}

		public bool IsValid()
		{
			return true;
		}
	}
}
