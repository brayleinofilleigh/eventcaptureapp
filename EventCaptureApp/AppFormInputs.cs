using System;
using System.Collections.Generic;
using EventCaptureApp.Controls;
using EventCaptureApp.Enums;
using EventCaptureApp.Interfaces;
using Xamarin.Forms;

namespace EventCaptureApp
{
	public class AppFormInputs
	{
		public static readonly Dictionary<FormInputType, Type> Inputs = new Dictionary<FormInputType, Type>()
		{
			{FormInputType.EmailField, typeof(TextEntry)},
			{FormInputType.NumericField, typeof(TextEntry)},
			{FormInputType.ValueList, typeof(FormValueListButton)},
			{FormInputType.TextField, typeof(TextEntry)},
			{FormInputType.TickBox, typeof(FormSwitch)}
		};

		public static IFormInputControl GetInputControlByType(FormInputType type, FormInput properties)
		{
			IFormInputControl inputControl = (IFormInputControl)Activator.CreateInstance(Inputs[type]);
			inputControl.SetProperties(properties);
			return inputControl;
		}
	}
}
