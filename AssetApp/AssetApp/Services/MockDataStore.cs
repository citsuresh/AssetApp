using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AssetApp.Models;
using AssetApp.Services;

[assembly: Xamarin.Forms.Dependency(typeof(MockDataStore))]
namespace AssetApp.Services
{
    public class MockDataStore : IDataStore<Asset>
    {
        List<Asset> items;

        public MockDataStore()
        {
            //Acr.UserDialogs.UserDialogs.Instance.ShowLoading("Loading text here...");
            var assets = GetAssetsByClient(string.Empty);
            //ThreadPool.QueueUserWorkItem(o => GetAssetsByClient(string.Empty));

            items = new List<Asset>();

            items.AddRange(assets);
            //Acr.UserDialogs.UserDialogs.Instance.HideLoading();
            //var mockItems = new List<Item>
            //{
            //    new Item { Id = Guid.NewGuid().ToString(), Text = "First item", Description="This is an item description." },
            //    new Item { Id = Guid.NewGuid().ToString(), Text = "Second item", Description="This is an item description." },
            //    new Item { Id = Guid.NewGuid().ToString(), Text = "Third item", Description="This is an item description." },
            //    new Item { Id = Guid.NewGuid().ToString(), Text = "Fourth item", Description="This is an item description." },
            //    new Item { Id = Guid.NewGuid().ToString(), Text = "Fifth item", Description="This is an item description." },
            //    new Item { Id = Guid.NewGuid().ToString(), Text = "Sixth item", Description="This is an item description." },
            //};

            //foreach (var item in mockItems)
            //{
            //    items.Add(item);
            //}
        }

        private IEnumerable<Asset> GetAssetsByClient(string clientId)
        {
            List<Asset> assetlist = new List<Asset>();
            if (string.IsNullOrWhiteSpace(clientId))
            {
                //ShowOnUIThread("Getting assets for All clients...");

                Dictionary<string, IEnumerable<Asset>> result = RestServiceHelper.InvokeGetByClientAsync().Result;
                foreach (var resultValue in result.Values)
                {
                    assetlist.AddRange(PopulateAssetList(resultValue));
                }

                //ShowOnUIThread("Number of assets for all clients is " + assets.Count);
            }
            else
            {
                //ShowOnUIThread("Getting assets for client '" + clientId + "'...");
                var result = RestServiceHelper.InvokeGetByClientAsync(clientId).Result;
                if (result.ContainsKey(clientId))
                {
                    assetlist.AddRange(PopulateAssetList(result[clientId]));
                }

                //ShowOnUIThread("Number of assets for client '" + clientId + "' is " + assetlist.Count);
            }

            return assetlist;
        }

        private static List<Asset> PopulateAssetList(IEnumerable<Asset> resultValues)
        {
            List<Asset> assetlist = new List<Asset>();
            foreach (var item in resultValues)
            {
                var assettypeValue = RestServiceHelper.GetAssetType(item.AssetType);

                item.AssetTypeValue = string.Empty;
                if (assettypeValue != null)
                {
                    item.AssetTypeValue = assettypeValue.AssetType1;
                }

                var assetsubtypeValue = RestServiceHelper.GetAssetSubType(item.AssetType, item.AssetSubType);

                item.AssetSubTypeValue = string.Empty;
                if (assetsubtypeValue != null)
                {
                    item.AssetSubTypeValue = assetsubtypeValue.AssetSubType1;
                }

                assetlist.Add(item);
            }

            return assetlist;
        }


        public async Task<bool> AddItemAsync(Asset item)
        {
            items.Add(item);

            return await Task.FromResult(true);
        }

        public async Task<bool> UpdateItemAsync(Asset item)
        {
            var _item = items.Where((Asset arg) => arg.Id == item.Id).FirstOrDefault();
            items.Remove(_item);
            items.Add(item);

            return await Task.FromResult(true);
        }

        public async Task<bool> DeleteItemAsync(Asset item)
        {
            var _item = items.Where((Asset arg) => arg.Id == item.Id).FirstOrDefault();
            items.Remove(_item);

            return await Task.FromResult(true);
        }

        public async Task<Asset> GetItemAsync(string id)
        {
            return await Task.FromResult(items.FirstOrDefault(s => s.Id == id));
        }

        public async Task<IEnumerable<Asset>> GetItemsAsync(bool forceRefresh = false)
        {
            items.Clear();
            var assets = GetAssetsByClient(string.Empty);
            items = new List<Asset>();
            items.AddRange(assets);

            return await Task.FromResult(items);
        }
    }
}