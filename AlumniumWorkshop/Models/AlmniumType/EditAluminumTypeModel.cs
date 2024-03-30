namespace AlumniumWorkshop.Models.AlmniumType
{
    public class EditAluminumTypeModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Quantity { get; set; }

        public IList<UsedItemsModel> Items { get; set; }
    }
}
