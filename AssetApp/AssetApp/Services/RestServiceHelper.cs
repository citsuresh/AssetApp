using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Runtime.CompilerServices;
using AssetApp.Models;
using Newtonsoft.Json;

namespace AssetApp.Services
{
    /// <summary>
    /// Summary description for RestServiceHelper
    /// </summary>
    public static partial class RestServiceHelper
    {
        public static List<AssetType> AssetTypes = new List<AssetType>();
        public static List<AssetSubType> AssetSubTypes = new List<AssetSubType>();

        public static async System.Threading.Tasks.Task PopulateAssetTypesSubTypesAsync()
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(GetAssetWebApiRestServiceAddress());

            // Add an Accept header for JSON format.
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

            var requestUri = "assettypes";
            HttpResponseMessage response = client.GetAsync(requestUri).Result;

            if (response.IsSuccessStatusCode)
            {
                string json = await response.Content.ReadAsStringAsync();
                var assetTypes = JsonConvert.DeserializeObject<IEnumerable<AssetType>>(json);
                AssetTypes.AddRange(assetTypes);
            }

            requestUri = "assetsubtypes";
            response = client.GetAsync(requestUri).Result;

            if (response.IsSuccessStatusCode)
            {
                string json = await response.Content.ReadAsStringAsync();
                var assetSubTypes = JsonConvert.DeserializeObject<IEnumerable<AssetSubType>>(json);
                AssetSubTypes.AddRange(assetSubTypes);
            }
        }

        public static async System.Threading.Tasks.Task<IEnumerable<string>> InvokeGetAllClientsAsync()
        {
            List<string> clientIds = new List<string>();
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(GetAssetWebApiRestServiceAddress());

            // Add an Accept header for JSON format.
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

            var requestUri = "clients";

            HttpResponseMessage response = client.GetAsync(requestUri).Result;

            if (response.IsSuccessStatusCode)
            {
                string json = await response.Content.ReadAsStringAsync();
                clientIds.AddRange(JsonConvert.DeserializeObject<IEnumerable<string>>(json));
            }

            return clientIds;
        }

        public static async System.Threading.Tasks.Task<IEnumerable<AssetType>> InvokeGetAllAssetTypesAsync()
        {
            List<AssetType> assetTypes = new List<AssetType>();
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(GetAssetWebApiRestServiceAddress());

            // Add an Accept header for JSON format.
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

            var requestUri = "assettypes";

            HttpResponseMessage response = client.GetAsync(requestUri).Result;

            if (response.IsSuccessStatusCode)
            {
                string json = await response.Content.ReadAsStringAsync();
                assetTypes.AddRange(JsonConvert.DeserializeObject<IEnumerable<AssetType>>(json));
            }

            return assetTypes;
        }

        public static async System.Threading.Tasks.Task<IEnumerable<AssetSubType>> InvokeGetAllAssetSubTypesAsync()
        {
            List<AssetSubType> assetSubTypes = new List<AssetSubType>();
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(GetAssetWebApiRestServiceAddress());

            // Add an Accept header for JSON format.
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

            var requestUri = "assetsubtypes";

            HttpResponseMessage response = client.GetAsync(requestUri).Result;

            if (response.IsSuccessStatusCode)
            {
                string json = await response.Content.ReadAsStringAsync();
                assetSubTypes.AddRange(JsonConvert.DeserializeObject<IEnumerable<AssetSubType>>(json));
            }

            return assetSubTypes;
        }
        
        public static async System.Threading.Tasks.Task<Dictionary<string, IEnumerable<Asset>>> InvokeGetAsync()
        {
            Dictionary<string, IEnumerable<Asset>> assetCountersByClient = new Dictionary<string, IEnumerable<Asset>>();
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(GetGlobalRestServiceBaseAddress());

            // Add an Accept header for JSON format.
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

            HttpResponseMessage response = client.GetAsync("values").Result;

            if (response.IsSuccessStatusCode)
            {
                string json = await response.Content.ReadAsStringAsync();
                var assetCounters = JsonConvert.DeserializeObject<IEnumerable<Asset>>(json).ToList();
                PopulateAssetType(assetCounters);
                var clientIDs = assetCounters.Select(counter => counter.ClientID).Distinct();
                foreach (var clientID in clientIDs)
                {
                    var assetCounterforClient = assetCounters.Where(counter => counter.ClientID == clientID);
                    assetCountersByClient.Add(clientID, assetCounterforClient);
                }
            }

            return assetCountersByClient;
        }

        public static bool InvokePost(string ClientID, int assetType, int subAssetType, int count = 1)
        {
            try
            {
                //HttpClient client = new HttpClient();
                //client.BaseAddress = new Uri(GetGlobalRestServiceBaseAddress());

                //client.DefaultRequestHeaders.Accept.Add(
                //    new MediaTypeWithQualityHeaderValue("application/json"));

                //var assetCounter = new AssetCounter()
                //{
                //    AssetType = assetType,
                //    AssetSubType = subAssetType,
                //    Count = count,
                //    ClientID = ClientID
                //};

                //var response = client.PostAsJsonAsync("Values", assetCounter).Result;

                //if (response.IsSuccessStatusCode)
                //{
                //    return true;
                //}
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return false;
        }

        public static bool InvokePostGlobalAsset(GlobalAsset globalAsset)
        {
            if (globalAsset == null)
            {
                return false;
            }

            try
            {
                //HttpClient client = new HttpClient();

                //client.BaseAddress = new Uri(GetGlobalRestServiceBaseAddress());

                //client.DefaultRequestHeaders.Accept.Add(
                //    new MediaTypeWithQualityHeaderValue("application/json"));

                //var response = client.PostAsJsonAsync("globalassetsapi", globalAsset).Result;

                //if (response.IsSuccessStatusCode)
                //{
                //    return true;
                //}
                //else
                //{
                //    throw new Exception(response.ReasonPhrase + " : " + response.Content.ReadAsStringAsync().Result);
                //}
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return false;
        }


        public static bool InvokePut(GlobalAsset globalAsset)
        {
            try
            {
                //HttpClient client = new HttpClient();

                //client.BaseAddress = new Uri(GetGlobalRestServiceBaseAddress());

                //client.DefaultRequestHeaders.Accept.Add(
                //    new MediaTypeWithQualityHeaderValue("application/json"));

                //var response = client.PutAsJsonAsync("globalassetsapi", globalAsset).Result;

                //if (response.IsSuccessStatusCode)
                //{
                //    return true;
                //}
            }
            catch (Exception)
            {
            }

            return false;
        }

        private static string GetGlobalRestServiceBaseAddress()
        {
            //client.BaseAddress = new Uri("http://globalassetrestservicesample.azurewebsites.net/api/");

            var restServiceBaseAddress = "http://globalassetrestservicesample.azurewebsites.net/api/";

            if (string.IsNullOrEmpty(restServiceBaseAddress))
            {
                restServiceBaseAddress = "http://localhost:11964/api/";
            }
            return restServiceBaseAddress;
        }

        private static string GetAssetWebApiRestServiceAddress()
        {

            var restServiceAddress = "https://assetdbwebapi.azurewebsites.net/api/";

            if (string.IsNullOrEmpty(restServiceAddress))
            {
                restServiceAddress = "http://localhost:22460/api/";
            }
            return restServiceAddress;
        }

        public static AssetType GetAssetType(int assetTypeId)
        {
            if (!RestServiceHelper.AssetTypes.Any())
            {
                RestServiceHelper.PopulateAssetTypesSubTypesAsync().Wait();
            }

            return RestServiceHelper.AssetTypes.Where(type => type.AssetTypeID == assetTypeId).FirstOrDefault();
        }

        public static AssetSubType GetAssetSubType(int assetTypeId, int assetSubTypeId)
        {
           return RestServiceHelper.AssetSubTypes.Where(type => type.AssetType == assetTypeId && type.AssetSubTypeID == assetSubTypeId).FirstOrDefault();
        }

        public static async System.Threading.Tasks.Task<IEnumerable<Asset>> InvokeGetAllAssets(string clientId = "")
        {
            List<Asset> assets = new List<Asset>();
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(GetAssetWebApiRestServiceAddress());

            // Add an Accept header for JSON format.
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

            var requestUri = "assets";
            if (!string.IsNullOrWhiteSpace(clientId))
            {
                requestUri += "/" + clientId;
            }

            HttpResponseMessage response = client.GetAsync(requestUri).Result;

            if (response.IsSuccessStatusCode)
            {
                string json = await response.Content.ReadAsStringAsync();
                var assetCounters = JsonConvert.DeserializeObject<IEnumerable<Asset>>(json).ToList();
                PopulateAssetType(assetCounters);
                assets.AddRange(assetCounters);
            }

            return assets;
        }

    public static async System.Threading.Tasks.Task<Dictionary<string, IEnumerable<Asset>>> InvokeGetByClientAsync(string clientId = "")
        {
            Dictionary<string, IEnumerable<Asset>> assetCountersByClient = new Dictionary<string, IEnumerable<Asset>>();
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(GetAssetWebApiRestServiceAddress());

            // Add an Accept header for JSON format.
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

            var requestUri = "assetsbyclient";
            if (!string.IsNullOrWhiteSpace(clientId))
            {
                requestUri += "/" + clientId;
            }

            HttpResponseMessage response = client.GetAsync(requestUri).Result;

            if (response.IsSuccessStatusCode)
            {
                string json = await response.Content.ReadAsStringAsync();
                var assetCounters = JsonConvert.DeserializeObject<IEnumerable<Asset>>(json).ToList();
                PopulateAssetType(assetCounters);
                var clientIDs = assetCounters.Select(counter => counter.ClientID).Distinct();
                foreach (var clientID in clientIDs)
                {
                    var assetCounterforClient = assetCounters.Where(counter => counter.ClientID == clientID);
                    assetCountersByClient.Add(clientID, assetCounterforClient);
                }
            }

            return assetCountersByClient;
        }

        private static void PopulateAssetType(IEnumerable<Asset> resultValues)
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
        }

        public static async System.Threading.Tasks.Task<Dictionary<string, IEnumerable<Asset>>> InvokeGetByLabAsync(string clientId = "")
        {
            Dictionary<string, IEnumerable<Asset>> assetCountersByLab = new Dictionary<string, IEnumerable<Asset>>();

            var assets = InvokeGetAllAssets(clientId).Result.ToList();

                var labIDs = assets.Select(counter => counter.LabID).Distinct();
                foreach (var labId in labIDs)
                {
                    var assetforLab = assets.Where(counter => counter.LabID == labId);
                    assetCountersByLab.Add(labId, assetforLab);
                }

            return assetCountersByLab;
        }

        public static async System.Threading.Tasks.Task<Dictionary<string, IEnumerable<Asset>>> InvokeGetByAssetTypeAsync(string clientId = "")
        {
            Dictionary<string, IEnumerable<Asset>> assetCountersByLab = new Dictionary<string, IEnumerable<Asset>>();

            var assets = InvokeGetAllAssets(clientId).Result.ToList();

            var assetTypes = assets.Select(counter => counter.AssetTypeValue).Distinct();
            foreach (var assetType in assetTypes)
            {
                var assetforassetType = assets.Where(counter => counter.AssetTypeValue == assetType);
                assetCountersByLab.Add(assetType, assetforassetType);
            }

            return assetCountersByLab;
        }
    }
}