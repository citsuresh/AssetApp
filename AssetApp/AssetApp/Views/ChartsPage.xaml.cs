using System;
using AssetApp.Models;
using AssetApp.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AssetApp.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ChartsPage : ContentPage
	{
        ChartsViewModel viewModel;

        public ChartsPage()
        {
            InitializeComponent();

            BindingContext = viewModel = new ChartsViewModel();
        }
        
        protected override void OnAppearing()
        {
            base.OnAppearing();

            if (viewModel.LabAssetChartItems.Count == 0)
            {
                viewModel.LoadItemsCommand.Execute(null);
            }
        }
    }
}