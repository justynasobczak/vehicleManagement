using System;

namespace VehicleManagementModels
{
    public class Category
    {
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
        public decimal WeightFrom { get; set; }
        public decimal WeightUpTo { get; set; }
        public string Icon { get; set; }

    }
}
