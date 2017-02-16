using System;

namespace EventCaptureApp.Interfaces
{
	public interface IFormInputControl
	{
		void SetProperties(FormInput value);
		FormInput GetProperties();
		void Highlighted(bool value);
		bool IsValid();
	}
}
