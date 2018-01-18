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
        public ObservableCollection<ClientAssetChartData> ClientAssetChartItems { get; set; } 
        public ObservableCollection<LabAssetChartData> LabAssetChartItems { get; set; }

        public ObservableCollection<AssetByAssetTypeChartData> AssetByAssetTypeChartItems { get; set; }
        


        public Command LoadItemsCommand { get; set; }

        public ChartsViewModel()
        {
            Title = "Charts";
            ClientAssetChartItems = new ObservableCollection<ClientAssetChartData>();
            LabAssetChartItems = new ObservableCollection<LabAssetChartData>();
            AssetByAssetTypeChartItems = new ObservableCollection<AssetByAssetTypeChartData>();

            LoadItemsCommand = new Command(async () => await ExecuteLoadItemsCommand());
        }

        async Task ExecuteLoadItemsCommand()
        {
            if (IsBusy)
                return;

            IsBusy = true;

            try
            {
                //ClientAssetChartItems.Clear();
                //var assetsGroupedByClient = RestServiceHelper.InvokeGetByClientAsync().Result;
                //foreach (var item in assetsGroupedByClient)
                //{
                //    ClientAssetChartItems.Add(new ClientAssetChartData{ClientId = item.Key , AssetCount = item.Value.Count()});
                //}

                LabAssetChartItems.Clear();
                var assetsGroupedByClient = RestServiceHelper.InvokeGetByLabAsync().Result;
                foreach (var item in assetsGroupedByClient)
                {
                    LabAssetChartItems.Add(new LabAssetChartData { LabId = item.Key, AssetCount = item.Value.Count() });
                }

                AssetByAssetTypeChartItems.Clear();
                var assetsGroupedByAssetType = RestServiceHelper.InvokeGetByAssetTypeAsync().Result;
                foreach (var item in assetsGroupedByAssetType)
                {
                    AssetByAssetTypeChartItems.Add(new AssetByAssetTypeChartData { AssetType = item.Key, AssetCount = item.Value.Count() });
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
}