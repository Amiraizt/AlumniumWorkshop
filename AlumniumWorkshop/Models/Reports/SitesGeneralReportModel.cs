namespace AlumniumWorkshop.Models.Reports
{
    public class SitesGeneralReportModel : ReportModel
    {
        public string TotalPrice { get; set; }

        public List<SiteModel> Sites { get; set; }
        public class SiteModel
        {
            public string SiteName { get; set; }
            public string SiteOwnerName { get; set; }
            public string SiteOwnerNumber { get; set; }
            public string UsedAlumunium { get; set; }
            public string TotalPrice { get; set; }
            public string WindowsNumber { get; set; }
            public string MetersNumber { get; set; }
            public string DoorsNumber { get; set; }
        }

    }
}
