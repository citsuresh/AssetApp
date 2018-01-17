using System;
using AssetApp.Models;
using AssetApp.Services;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AssetApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class NewItemPage : ContentPage
    {
        public Asset Item { get; set; }

        public NewItemPage()
        {
            InitializeComponent();

            Item = new Asset();

            BindingContext = this;

            var allclients = RestServiceHelper.InvokeGetAllClientsAsync();
            foreach (var client in allclients.Result)
            {
                this.ClientIdPicker.Items.Add(client);
            }

            var allAssetTypes = RestServiceHelper.InvokeGetAllAssetTypesAsync();
            foreach (var assetType in allAssetTypes.Result)
            {
                this.AssetTypePicker.Items.Add(assetType.AssetType1);
            }

            var allAssetSubTypes = RestServiceHelper.InvokeGetAllAssetSubTypesAsync();
            foreach (var assetSubType in allAssetSubTypes.Result)
            {
                this.AssetSubTypePicker.Items.Add(assetSubType.AssetSubType1);
            }
        }

        async void Save_Clicked(object sender, EventArgs e)
        {
            MessagingCenter.Send(this, "AddItem", Item);
            await Navigation.PopModalAsync();
        }
    }
}