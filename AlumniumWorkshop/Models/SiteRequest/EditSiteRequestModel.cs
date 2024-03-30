namespace AlumniumWorkshop.Models.SiteRequest
{
    public class EditSiteRequestModel
    {
        public int Id { get; set; }
        public string SiteName { get; set; }
        public string SiteOwnerName { get; set; }
        public string SiteOwnerPhone { get; set; }
        public decimal MetersNumber { get; set; }
        public int WindowsNumber { get; set; }
        public int DoorsNumber { get; set; }
        public decimal TotalPrice { get; set; }
        public List<UsedAluminumModel> UsedAluminumList { get; set; }
    }
}
