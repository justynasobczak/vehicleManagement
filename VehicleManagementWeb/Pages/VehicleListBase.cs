using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using VehicleManagementModels;
using VehicleManagementWeb.Services;

namespace VehicleManagementWeb.Pages
{
    public class VehicleListBase : ComponentBase
    {
        [Inject]
        public IVehicleService VehicleService { get; set; }
        public IEnumerable<Vehicle> Vehicles { get; set; }

        protected override async Task OnInitializedAsync()
        {
            Vehicles = (await VehicleService.GetVehicles()).ToList();
        }

        protected async Task Delete_Click(int id)
        {
            await VehicleService.DeleteVehicle(id);
        }
    }
}
