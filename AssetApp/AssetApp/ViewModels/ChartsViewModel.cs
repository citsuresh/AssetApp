using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using AssetApp.Models;
using AssetApp.Services;
using AssetApp.Views;
using Xamarin.Forms;

namespace AssetApp.ViewModels
{
    public class ChartsViewModel : BaseViewModel
    {
        public ObservableCollection<ClientAsset> Items { get; set; }

        public Command LoadItemsCommand { get; set; }

        public ChartsViewModel()
        {
            Title = "Charts";
            Items = new ObservableCollection<ClientAsset>();
            LoadItemsCommand = new Command(async () => await ExecuteLoadItemsCommand());
        }

        async Task ExecuteLoadItemsCommand()
        {
            if (IsBusy)
                return;

            IsBusy = true;

            try
            {
                Items.Clear();
                var assetsGroupedByClient = RestServiceHelper.InvokeGetByClientAsync().Result;
                
                foreach (var item in assetsGroupedByClient)
                {
                    Items.Add(new ClientAsset{ClientId = item.Key , AssetCount = item.Value.Count()});
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            finally
            {
                IsBusy = false;
            }
        }
    }

    public class ClientAsset
    {
        public string ClientId { get; set; }
        public int AssetCount { get; set; }
    }
}