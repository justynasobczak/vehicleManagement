using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using VehicleManagementModels;

namespace VehicleManagementWeb.Pages
{
    public class VehicleListBase : ComponentBase
    {
        public IEnumerable<Vehicle> Vehicles { get; set; }

        protected override async Task OnInitializedAsync()
        {
            await Task.Run(LoadVehicles);
        }

        private void LoadVehicles()
        {
            Vehicle v1 = new Vehicle { Manufacturer = Manufacturer.Ferrari, ManufactureYear = 2020, OwnerName = "Owner1", VehicleId = 1, Weight = 3433.20m };
            Vehicle v2 = new Vehicle { Manufacturer = Manufacturer.Honda, ManufactureYear = 1978, OwnerName = "Owner2", VehicleId = 2, Weight = 10 };
            Vehicle v3 = new Vehicle { Manufacturer = Manufacturer.Mercedes, ManufactureYear = 2019, OwnerName = "Owner3", VehicleId = 3, Weight = 333.4m };
            Vehicle v4 = new Vehicle { Manufacturer = Manufacturer.Toyota, ManufactureYear = 2018, OwnerName = "Owner4", VehicleId = 4, Weight = 98.5m };
            Vehicles = new List<Vehicle> { v1, v2, v3, v4 };

        }
    }
}
