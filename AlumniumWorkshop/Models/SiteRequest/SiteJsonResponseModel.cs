using AlumniumWorkshop.Models.AlmniumType;

namespace AlumniumWorkshop.Models.SiteRequest
{
    public class SiteJsonResponseModel
    {
        public double TotalPrice { get; set; }  
        public  List<JsonAluminumDetailsModel> aluminums { get; set; }
    }
}
