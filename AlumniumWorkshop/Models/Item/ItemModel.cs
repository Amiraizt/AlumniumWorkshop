namespace AlumniumWorkshop.Models.Item
{
    public class ItemModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public string Status { get; set; }
        public string CreationDate { get; set; }
    }
}
