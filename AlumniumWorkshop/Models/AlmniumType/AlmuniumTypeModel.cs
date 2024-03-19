namespace AlumniumWorkshop.Models.AlmniumType
{
    public class AlmuniumTypeModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string UsedItemName { set; get; }
        public int UsedItemId { set; get; }
        public decimal Quantity { get; set; }
    }
}
