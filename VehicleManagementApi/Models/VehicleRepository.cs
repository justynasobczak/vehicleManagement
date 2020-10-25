﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
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
            return await _appDbContext.Vehicles.ToListAsync();
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
    }
}
