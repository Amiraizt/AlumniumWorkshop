namespace AlumniumWorkshop.Models.Reports
{
    public class SiteReportModel : ReportModel
    {
        public string SiteName { get; set; }
        public string SiteOwnerName { get; set; }
        public string SiteOwnerPhone { get; set; }
        public string Meters { get; set; }
        public string WindowsNumber { get; set; }
        public string DoorsNumber { get; set; }
        public string SiteTotalPrice { get; set; }
        public IList<ItemModel> Items { get; set; }
        public IList<AluminumModel> Aluminums { get; set; }
        public class ItemModel
        {
            public string ItemName { get; set; }
            public string UsedQuantity { get; set; }
            public string UnitPrice { get; set; }
            public string Price { get; set; }
        }
        public class AluminumModel
        {
            public string AluminumName { get; set; }
            public string MeterPrice { get; set; }
            public string TotalPrice { get; set; }

        }
    }
}
