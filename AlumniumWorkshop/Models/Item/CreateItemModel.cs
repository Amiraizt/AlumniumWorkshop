namespace AlumniumWorkshop.Models.Item
{
    public class CreateItemModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public List<ItemsList> Items { get; set; }
    }
    public class ItemsList
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsSelected { get; set; }
    }
}
