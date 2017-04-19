#region usings

using System;
using System.Collections.Generic;
using System.Linq;
using BrazilianCompany.Model.Interface;

#endregion

namespace BrazilianCompany.DataAccess
{
    public class DataRepository : IDataRepository
    {
        private readonly Dictionary<string, IVehicle> _sectorPlaceToLicensePlate;

        public DataRepository()
        {
            _sectorPlaceToLicensePlate = new Dictionary<string, IVehicle>();
        }

        public void AddVehicle(int sector, int place, IVehicle vehicle)
        {
            var key = GetKey(sector, place);
            if (_sectorPlaceToLicensePlate.ContainsKey(key))
                throw new InvalidOperationException($"There is already a vehicle in sector {sector} place {place}");

            _sectorPlaceToLicensePlate.Add(key, vehicle);
        }

        public void RemoveVehicle(int sector, int place)
        {
            var key = GetKey(sector, place);
            if (!_sectorPlaceToLicensePlate.ContainsKey(key))
                throw new InvalidOperationException($"Sector {sector} place {place} is free");

            _sectorPlaceToLicensePlate.Remove(key);
        }

        public bool IsPlaceOccupied(int sector, int place)
        {
            var key = GetKey(sector, place);
            return _sectorPlaceToLicensePlate.ContainsKey(key);
        }

        public bool HasVehicle(string licensePlate)
        {
            return _sectorPlaceToLicensePlate.Values.Any(
                v => string.Equals(v.LicensePlate, licensePlate, StringComparison.OrdinalIgnoreCase));
        }

        public IVehicle FindVehicle(string licensePlate)
        {
            return _sectorPlaceToLicensePlate.Values.FirstOrDefault(
                v => string.Equals(v.LicensePlate, licensePlate, StringComparison.OrdinalIgnoreCase));
        }

        public IList<IVehicle> FindVehiclesByOwner(string owner)
        {
            return _sectorPlaceToLicensePlate
                .Values
                .Where(v => string.Equals(v.Owner, owner, StringComparison.OrdinalIgnoreCase))
                .OrderBy(v => v.EnterTime)
                .ThenBy(v => v.LicensePlate)
                .ToList();
        }

        private static string GetKey(int sector, int place)
        {
            return $"{sector}_{place}";
        }
    }
}