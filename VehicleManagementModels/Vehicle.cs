using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace VehicleManagementModels
{
    public class Vehicle
    {
        public int VehicleId { get; set; }
        public Manufacturer Manufacturer { get; set; }
        [Required(ErrorMessage = "Please provide owner's name")]
        public string OwnerName { get; set; }
        [Range(1900, 2100, ErrorMessage = "Min value is 1900 max value is 2100")]
        public int ManufactureYear { get; set; }

        [Range(typeof(decimal), "1", "1000000", ErrorMessage = "Min value is 1 max value is 999999.99")]
        public decimal Weight { get; set; }
    }
}
