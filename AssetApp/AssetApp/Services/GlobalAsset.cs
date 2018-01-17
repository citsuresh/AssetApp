    using System;

namespace AssetAndroidApp
{
    public partial class GlobalAsset
    {
        public int AssetID { get; set; }
        public int AssetType { get; set; }
        public int AssetSubType { get; set; }
        public string SerialNumber { get; set; }
        public string ClientID { get; set; }
        public string Status { get; set; }
        public Nullable<System.DateTime> LastServiceDate { get; set; }
        public Nullable<System.DateTime> NextServiceDate { get; set; }
    }
}
