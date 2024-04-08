using Alumnium.Core;
using AlumniumWorkshop.Models.AlmniumType;

namespace AlumniumWorkshop.Models.SiteRequest
{
    public class RootSiteUsedItemModel
    {
        public int SiteId { get; set; }
        public IList<UseItemModel> Items { get; set; }
    }
}
