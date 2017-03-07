using System;
using System.Collections.Generic;
using System.Diagnostics;
using EventCaptureApp.Models;
using Xamarin.Forms;
using System.Linq;

namespace EventCaptureApp.Controls
{
	public class CategoryMenuBar: StackLayout
	{
		public static readonly BindableProperty CategoriesProperty = BindableProperty.Create(nameof(Categories), typeof(List<CampaignCategory>), typeof(CategoryMenuBar), null, propertyChanged: OnCategoriesChanged);
		public static readonly BindableProperty SelectedCategoryProperty = BindableProperty.Create(nameof(SelectedCategory), typeof(CampaignCategory), typeof(CategoryMenuBar), null, propertyChanged: OnSelectedCategoryChanged);
		private List<CategoryMenuBarButton> _categoryButtons;
		private TapGestureRecognizer _categoryTapRecognizer;

		public CategoryMenuBar()
		{
			_categoryTapRecognizer = new TapGestureRecognizer();
			_categoryButtons = new List<CategoryMenuBarButton>();
			this.Orientation = StackOrientation.Horizontal;
			this.Spacing = 0;
			_categoryTapRecognizer.Tapped += OnCategoryBtnTapped;
		}

		public List<CampaignCategory> Categories 
		{
			get { return (List<CampaignCategory>)GetValue(CategoriesProperty); }
			set { SetValue(CategoriesProperty, value); }
		}

		public CampaignCategory SelectedCategory
		{
			get { return (CampaignCategory)GetValue(SelectedCategoryProperty); }
			set { SetValue(SelectedCategoryProperty, value); }
		}

		private static void OnCategoriesChanged(BindableObject bindable, object oldValue, object newValue) 		{ 			if (newValue != null) 			{ 				CategoryMenuBar menuBar = (CategoryMenuBar)bindable;
				menuBar.SetCategories((List<CampaignCategory>)newValue); 			} 		}

		private static void OnSelectedCategoryChanged(BindableObject bindable, object oldValue, object newValue)
		{
			if (newValue != null)
			{
				CategoryMenuBar menuBar = (CategoryMenuBar)bindable;
				menuBar.SetSelectedCategory((CampaignCategory)newValue);
			}
		}

		protected void SetCategories(List<CampaignCategory> categories)
		{
			this.Children.Clear();
			_categoryButtons.Clear();
			foreach (CampaignCategory category in categories)
			{
				CategoryMenuBarButton categoryBtn = new CategoryMenuBarButton() { BindingContext = category };
				this.Children.Add(categoryBtn);
				_categoryButtons.Add(categoryBtn);
				categoryBtn.GestureRecognizers.Add(_categoryTapRecognizer);
			}
		}

		protected void SetSelectedCategory(CampaignCategory selectedCategory)
		{
			foreach (CategoryMenuBarButton categoryBtn in _categoryButtons)
			{
				categoryBtn.IsHighlighted = (CampaignCategory)categoryBtn.BindingContext == selectedCategory;
			}
		}

		protected void OnCategoryBtnTapped(object sender, EventArgs e)
		{
			CategoryMenuBarButton categoryBtn = (CategoryMenuBarButton)sender;
			this.SelectedCategory = (CampaignCategory)categoryBtn.BindingContext;
		}
	}
}
