using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
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

        [Range(typeof(decimal), "1", "99999,99", ErrorMessage = "Min value is 1 max value is 99999.99")]
        public decimal Weight { get; set; }
        [NotMapped]
        public string Icon { get; set; }
    }
}
