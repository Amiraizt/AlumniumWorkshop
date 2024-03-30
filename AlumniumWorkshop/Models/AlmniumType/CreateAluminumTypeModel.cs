using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace AlumniumWorkshop.Models.AlmniumType
{
    public class CreateAluminumTypeModel
    {
        
        //public CreateAluminumTypeModel()
        //{
        //    Items = new List<UsedItemsModel>();
        //}
        public int Id { get; set; }
        public string Name { get; set; }
        public int Quantity { get; set; }
        public IList<UsedItemsModel> Items { get; set; }
        //public class UsedItems
        //{
        //    public int Id { get; set; }
        //    public string Name { get; set; }
        //    public int Quantity { get; set; }
        //    public bool IsSelected { get; set; }
        //}
    }
   
}
