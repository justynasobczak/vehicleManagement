using System;
using System.Collections.Generic;
using System.ComponentModel;
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

        [Inject]
        public NavigationManager NavigationManager { get; set; }
        public IEnumerable<Vehicle> Vehicles { get; set; }

        private bool _isSortedAscending;
        private string _currentSortColumn;

        protected override async Task OnInitializedAsync()
        {
            Vehicles = (await VehicleService.GetVehicles()).ToList();
        }

        protected async Task OnDeleteVehicle(int vehicleId)
        {
            await VehicleService.DeleteVehicle(vehicleId);
            NavigationManager.NavigateTo("/", true);
        }
        
        protected void OnAddVehicle()
        {
            NavigationManager.NavigateTo("/editvehicle", true);
        }

        protected void OnEditVehicle(int vehicleId)
        {
            NavigationManager.NavigateTo($"/editvehicle/{vehicleId}", true);
        }

        protected void SortTable(string columnName)
        {

            if (columnName != _currentSortColumn)
            {
                Vehicles = Vehicles.OrderBy(x => x.GetType().GetProperty(columnName)?.GetValue(x, null)).ToList();
                _currentSortColumn = columnName;
                _isSortedAscending = true;

            }
            else
            {
                if (_isSortedAscending)
                {
                    Vehicles = Vehicles.OrderByDescending(x => x.GetType().GetProperty(columnName)?.GetValue(x, null)).ToList();
                }
                else
                {
                    Vehicles = Vehicles.OrderBy(x => x.GetType().GetProperty(columnName)?.GetValue(x, null)).ToList();
                }

                _isSortedAscending = !_isSortedAscending;
            }
        }
    }
}
