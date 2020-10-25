using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VehicleManagementModels;

namespace VehicleManagementApi.Models
{
    public interface IVehicleRepository
    {
        Task<IEnumerable<Vehicle>> GetVehicles();
        Task<Vehicle> GetVehicle(int vehicleId);
        Task<Vehicle> AddVehicle(Vehicle vehicle);
        Task<Vehicle> UpdateVehicle(Vehicle vehicle);
        void DeleteVehicle(int vehicleId);

    }
}
