using System;
using Xamarin.Forms;

namespace EventCaptureApp.Controls
{
	public class ContentViewBase: ContentView
	{
		public static readonly BindableProperty IsHighlightedProperty = BindableProperty.Create("IsHighlighted", typeof(bool), typeof(ContentViewBase), false, propertyChanged: OnHighlightChanged);

		private Color _defaultBgColor;

		public ContentViewBase()
		{
			_defaultBgColor = this.BackgroundColor;
		}

		public int Index { get; set; } = 0;

		public Color HighlightColor { get; set; } = Color.Silver;

		public bool IsHighlighted
		{
			get { return (bool)GetValue(IsHighlightedProperty); }
			set { SetValue(IsHighlightedProperty, value); }
		}

		private static void OnHighlightChanged(BindableObject bindable, object oldValue, object newValue)
		{
			if (newValue != null)
			{
				ContentViewBase contentView = (ContentViewBase)bindable;
				contentView.BackgroundColor = (bool)newValue ? contentView.HighlightColor : contentView._defaultBgColor;
			}
		}
	}
}
