using System;
using System.Collections.Generic;
using System.Diagnostics;
using EventCaptureApp.Interfaces;
using EventCaptureApp.ViewModels;
using Xamarin.Forms;

namespace EventCaptureApp.Views
{
	public partial class LeadCapturePage : ContentPage
	{
		public LeadCapturePage()
		{
			InitializeComponent();
			LeadCapturePageViewModel viewModel = (LeadCapturePageViewModel)this.BindingContext;
			foreach (IFormInputControl inputControl in viewModel.InputControls)
				this.inputControlsLayout.Children.Add((View)inputControl);
		}
	}
}
