namespace AlumniumWorkshop.Models.AlmniumType
{
    public class AlmuniumTypeModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Quantity { get; set; }
        public string Status { get; set; }
        public List<UsedItemsModel> Items { get; set; }
    }
}
