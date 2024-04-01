namespace AlumniumWorkshop.Models.AlmniumType
{
    public class JsonAluminumDetailsModel
    {
        public string AluminumName { get; set; }
        public List<ItemModel> Items { get; set; }
        public class ItemModel
        {
            public string ItemName { get; set; }
            public int Qty { get; set; }
            public double UnitPrice { get; set; }
            public double TotalPrice { get; set; }
        }
    }
}
