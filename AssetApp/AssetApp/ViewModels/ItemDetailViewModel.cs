using AssetApp.Models;

namespace AssetApp.ViewModels
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
