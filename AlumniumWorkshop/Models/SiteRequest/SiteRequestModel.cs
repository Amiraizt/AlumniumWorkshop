namespace AlumniumWorkshop.Models.SiteRequest
{
    public class SiteRequestModel
    {
        public int Id { get; set; }
        public string SiteName { get; set; }
        public string SiteOwnerName { get; set; }
        public string SiteOwnerPhone { get; set; }
        public string AluminumTypeName { get; set; }
        public int AluminumTypeId { get; set; }
        public decimal MetersNumber { get; set; }
        public decimal TotalPrice { get; set; }
    }
}
