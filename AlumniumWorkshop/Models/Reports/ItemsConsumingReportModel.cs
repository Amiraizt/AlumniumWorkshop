namespace AlumniumWorkshop.Models.Reports
{
    public class ItemsConsumingReportModel : ReportModel
    {
        public IList<ConsumedItemsModel> ConsumedItems { get; set; }
        public decimal TotalPrice { get; set; }
    }
}
