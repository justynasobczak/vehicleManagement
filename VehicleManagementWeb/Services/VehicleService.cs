using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using VehicleManagementModels;

namespace VehicleManagementWeb.Services
{
    public class VehicleService : IVehicleService
    {
        private readonly HttpClient _httpClient;
        public VehicleService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<IEnumerable<Vehicle>> GetVehicles()
        {
            return await _httpClient.GetJsonAsync<Vehicle[]>("api/vehicle");
        }
    }
}
