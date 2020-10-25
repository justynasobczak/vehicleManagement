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
    }
}
