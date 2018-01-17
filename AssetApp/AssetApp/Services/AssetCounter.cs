namespace AssetApp.Services
{
    public partial class AssetCounter
    {
        public int AssetCounterID { get; set; }
        public int AssetType { get; set; }
        public int AssetSubType { get; set; }
        public int Count { get; set; }
        public string ClientID { get; set; }
    }
}
