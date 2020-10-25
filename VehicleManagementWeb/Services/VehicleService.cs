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

        public async Task<Vehicle> GetVehicle(int id)
        {
            return await _httpClient.GetJsonAsync<Vehicle>($"api/vehicle/{id}");
        }

        public async Task<Vehicle> UpdateVehicle(Vehicle updatedVehicle)
        {
            return await _httpClient.PutJsonAsync<Vehicle>($"api/vehicle", updatedVehicle);
        }

        public async Task<Vehicle> AddVehicle(Vehicle newVehicle)
        {
            return await _httpClient.PostJsonAsync<Vehicle>($"api/vehicle", newVehicle);
        }

        public async Task DeleteVehicle(int id)
        {
            await _httpClient.DeleteAsync($"api/vehicle/{id}");
        }
    }
}
