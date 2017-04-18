#region usings

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using BrazilianCompany.DataAccess;
using BrazilianCompany.Model.Interface;
using BrazilianCompany.Model.Model;

#endregion

namespace BrazilianCompany.Logic.Implementation
{
    public class VehiclePark : IVehiclePark
    {
        protected readonly IDataRepository DataRepository;
        private readonly Layout _layout;

        public VehiclePark(int numberOfSectors, int placesPerSector, IDataRepository dataRepository)
        {
            DataRepository = dataRepository;
            _layout = new Layout(numberOfSectors, placesPerSector);
        }

        public string Park(IVehicle vehicle, int sector, int placeNumber, DateTime startTime)
        {
            Validate(sector, placeNumber, vehicle, startTime);

            if (DataRepository.IsPlaceOccupied(sector, placeNumber))
                return $"The place ({sector},{placeNumber}) is occupied";

            if (DataRepository.HasVehicle(vehicle.LicensePlate))
                return $"There is already a vehicle with license plate {vehicle.LicensePlate} in the park";

            DataRepository.AddVehicle(sector, placeNumber, vehicle);

            return $"{vehicle.GetType().Name} parked successfully at place ({sector},{placeNumber})";
        }

        public Ticket ExitVehicle(string licensePlate, DateTime exitTime, decimal paid)
        {
            if (paid <= 0)
                throw new ArgumentException("Invalid paid");

            if (!DataRepository.HasVehicle(licensePlate))
                throw new ArgumentException($"There is no vehicle with license plate {licensePlate} in the park");

            var vehicle = DataRepository.GetVehicle(licensePlate);

            if (exitTime <= vehicle.EnterTime)
                throw new ArgumentException("Invalid exit time");

            var enterTime = vehicle.EnterTime;
            var overtimeHours = (int) Math.Round((exitTime - enterTime).TotalHours) - vehicle.ReservedHours;
            var overtimeRate = overtimeHours > 0 ? overtimeHours * vehicle.OvertimeRate : 0;
            var regularRate = vehicle.RegularRate * vehicle.ReservedHours;

            DataRepository.RemoveVehicle(vehicle.Sector, vehicle.Place);

            return new Ticket(
                vehicle,
                regularRate,
                overtimeRate,
                regularRate + overtimeRate,
                paid,
                paid - (regularRate + overtimeRate)
            );
        }

        public List<SectorStatus> GetStatus()
        {
            var statistic = new List<SectorStatus>(_layout.Sectors);
            for (var sector = 1; sector <= _layout.Sectors; sector++)
            {
                var occupiedPlaces = 0;
                for (var place = 1; place <= _layout.PlacesSec; place++)
                {
                    if (DataRepository.IsPlaceOccupied(sector, place))
                        occupiedPlaces++;
                }
            
                statistic.Add(new SectorStatus(sector, _layout.PlacesSec, occupiedPlaces));
            }

            return statistic;
        }

        public IVehicle FindVehicle(string licensePlate)
        {
            var vehicle = DataRepository.GetVehicle(licensePlate);
            if (vehicle == null)
                throw new ArgumentException($"There is no vehicle with license plate {licensePlate} in the park");

            return vehicle;
        }

        public IList<IVehicle> FindVehiclesByOwner(string owner)
        {
            return DataRepository.FindVehiclesByOwner(owner);            
        }       

        private void Validate(int sector, int placeNumber, IVehicle vehicle, DateTime startTime)
        {
            ValidatePlace(sector, placeNumber);
            ValidateVehicle(vehicle);

            if (startTime == DateTime.MinValue || startTime > DateTime.UtcNow)
                throw new InvalidOperationException();
        }

        private static void ValidateVehicle(IVehicle vehicle)
        {
            if (string.IsNullOrWhiteSpace(vehicle.Owner) ||
                string.IsNullOrWhiteSpace(vehicle.LicensePlate))
                throw new InvalidOperationException();

            if (!Regex.IsMatch(vehicle.LicensePlate, @"^[A-Z]{1,2}\d{4}[A-Z]{2}$"))
                throw new InvalidOperationException();

            if (vehicle.ReservedHours <= 0)
                throw new InvalidOperationException();
        }

        private void ValidatePlace(int sector, int placeNumber)
        {
            if (sector <= 0 || sector > _layout.Sectors)
                throw new ArgumentException($"There is no sector {sector} in the park");
            if (placeNumber <= 0 || placeNumber > _layout.PlacesSec)
                throw new ArgumentException($"There is no place {placeNumber} in sector {sector}");
        }
    }
}