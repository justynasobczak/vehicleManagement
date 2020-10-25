using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using VehicleManagementModels;
using VehicleManagementWeb.Services;

namespace VehicleManagementWeb.Pages
{
    public class EditVehicleBase : ComponentBase
    {
        [Inject]
        public IVehicleService VehicleService { get; set; }

        public string PageHeaderText { get; set; }
        public Vehicle Vehicle { get; set; } = new Vehicle();

        [Inject]
        public NavigationManager NavigationManager { get; set; }

        [Parameter]
        public string Id { get; set; }

        protected async override Task OnInitializedAsync()
        {
            int.TryParse(Id, out int vehicleId);
            if (vehicleId != 0)
            {
                PageHeaderText = "Edit vehicle details";
                Vehicle = await VehicleService.GetVehicle(int.Parse(Id));
            }
            else
            {
                PageHeaderText = "Create vehicle details";
                Vehicle = new Vehicle
                {
                    Manufacturer = Manufacturer.Ferrari,
                    ManufactureYear = 2020,
                    Weight = 1.00m
                };
            }
        }

        protected async Task HandleValidSubmit()
        {
            Vehicle result = null;
            if (Vehicle.VehicleId != 0)
            {
                result = await VehicleService.UpdateVehicle(Vehicle);
            }
            else
            {
                result = await VehicleService.AddVehicle(Vehicle);
            }
            if (result != null)
            {
                NavigationManager.NavigateTo("/");
            }
        }
    }
}
