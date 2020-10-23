using System;
using System.Collections.Generic;
using System.Text;

namespace VehicleManagementModels
{
    public class Vehicle
    {
        public int VehicleId { get; set; }
        public Manufacturer Manufacturer { get; set; }
        public string OwnerName { get; set; }
        public int ManufactureYear { get; set; }
        public decimal Weight { get; set; }
    }
}
