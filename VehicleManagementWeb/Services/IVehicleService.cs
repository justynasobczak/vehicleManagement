using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VehicleManagementModels;

namespace VehicleManagementWeb.Services
{
    public interface IVehicleService
    {
        Task<IEnumerable<Vehicle>> GetVehicles();
        Task<Vehicle> GetVehicle(int id);
        Task<Vehicle> UpdateVehicle(Vehicle updatedVehicle);
        Task<Vehicle> AddVehicle(Vehicle newVehicle);
        Task DeleteVehicle(int id);

    }
}
