﻿namespace AlumniumWorkshop.Models.Reports
{
    public class ConsumedItemsModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Quantity { get; set; }
        public int WareHouseQuantity { get; set; }
        public decimal TotalPrice { get; set; }
        public decimal UnitPrice { get; set; }
        public string AdditionDate { get; set; }
    }
}
