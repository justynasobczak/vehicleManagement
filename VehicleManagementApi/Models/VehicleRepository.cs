using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;
using VehicleManagementModels;

namespace VehicleManagementApi.Models
{
    public class VehicleRepository : IVehicleRepository
    {
        private readonly AppDbContext _appDbContext;

        public VehicleRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task<Vehicle> AddVehicle(Vehicle vehicle)
        {
            var result = await _appDbContext.Vehicles.AddAsync(vehicle);
            await _appDbContext.SaveChangesAsync();
            return result.Entity;
        }

        public async Task<Vehicle> DeleteVehicle(int vehicleId)
        {
            var dbEntry = await _appDbContext.Vehicles.FirstOrDefaultAsync(v => v.VehicleId == vehicleId);
            if (dbEntry != null)
            {
                _appDbContext.Vehicles.Remove(dbEntry);
                await _appDbContext.SaveChangesAsync();
                return dbEntry;
            }

            return null;
        }

        public async Task<Vehicle> GetVehicle(int vehicleId)
        {
            return await _appDbContext.Vehicles.FirstOrDefaultAsync(v => v.VehicleId == vehicleId);
        }

        public async Task<IEnumerable<Vehicle>> GetVehicles()
        {
            var query = from vehicle in _appDbContext.Vehicles
                let category = (
                    from category in _appDbContext.Categories
                    where category.WeightFrom <= vehicle.Weight && category.WeightUpTo > vehicle.Weight
                    select category).FirstOrDefault()
                select new {vehicle, category};
            var result = await query.ToListAsync();
            
            return
                result.Select(pair =>
                {
                    var vehicle = pair.vehicle;
                    vehicle.Icon = pair.category?.Icon;
                    return vehicle;
                });
        }

        public async Task<Vehicle> UpdateVehicle(Vehicle vehicle)
        {
            var dbEntry = await _appDbContext.Vehicles.FirstOrDefaultAsync(v => v.VehicleId == vehicle.VehicleId);
            if (dbEntry != null)
            {
                dbEntry.OwnerName = vehicle.OwnerName;
                dbEntry.Manufacturer = vehicle.Manufacturer;
                dbEntry.ManufactureYear = vehicle.ManufactureYear;
                dbEntry.Weight = vehicle.Weight;

                await _appDbContext.SaveChangesAsync();
                return dbEntry;
            }

            return null;
        }

        private string GetIconForVehicle(Vehicle vehicle)
        {
            var result = _appDbContext.Categories
                .Where(c => (c.WeightFrom <= vehicle.Weight) && (c.WeightUpTo > vehicle.Weight))
                .Select(c => c.Icon)
                .FirstOrDefault();
            if (result != null)
            {
                return result;
            }

            return "out of range";
        }
    }
}