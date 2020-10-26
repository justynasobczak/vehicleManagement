using System;
using System.ComponentModel.DataAnnotations;

namespace VehicleManagementModels
{
    public class Category
    {
        public const decimal MaxWeight = 100000M;
        public int CategoryId { get; set; }
        
        [Required]
        [StringLength(30)]
        public string CategoryName { get; set; }
        
        [Required]
        [Range(typeof(decimal), "0", "100000", ErrorMessage = "Weight should be between 1, 100000")]
        public decimal WeightFrom { get; set; }
        
        [Required]
        public decimal WeightUpTo { get; set; }
        
        [Required]
        public string Icon { get; set; }

    }
}
