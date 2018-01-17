using System;

using AssetCrossPlatformApp.Models;

namespace AssetCrossPlatformApp.ViewModels
{
    public class ItemDetailViewModel : BaseViewModel
    {
        public Asset Item { get; set; }
        public ItemDetailViewModel(Asset item = null)
        {
            Title = item?.Text;
            Item = item;
        }
    }
}
