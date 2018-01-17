using System;

namespace AssetCrossPlatformApp.Models
{
    public class Item
    {
        private string text;
        private string description;
        public string Id { get; set; }

        public string Text
        {
            get
            {
                text = this.Name + "(" + this.AssetType +")";
                return text;
            }

            set { text = value; }
        }

        public string Description
        {
            get
            {
                description = this.Status + " - Next service date : " + this.NextServiceDate;
                return description;
            }
            set { description = value; }
        }

        public int AssetID { get; set; }
        public int AssetType { get; set; }
        public int AssetSubType { get; set; }
        public string AssetTypeValue { get; set; }
        public string AssetSubTypeValue { get; set; }
        public string Name { get; set; }
        public string ClientID { get; set; }
        public string Status { get; set; }
        public DateTime? LastServiceDate { get; set; }
        public DateTime? NextServiceDate { get; set; }
    }
}