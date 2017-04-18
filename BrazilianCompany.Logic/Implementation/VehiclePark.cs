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
        private readonly IDataRepository _dataRepository;
        private readonly Layout _layout;

        public VehiclePark(int numberOfSectors, int placesPerSector, IDataRepository dataRepository)
        {
            _dataRepository = dataRepository;
            _layout = new Layout(numberOfSectors, placesPerSector);
        }

        public string Park(IVehicle vehicle, int sector, int placeNumber, DateTime startTime)
        {
            Validate(sector, placeNumber, vehicle, startTime);

            if (_dataRepository.IsPlaceOccupied(sector, placeNumber))
                return $"The place ({sector},{placeNumber}) is occupied";

            if (_dataRepository.HasVehicle(vehicle.LicensePlate))
                return $"There is already a vehicle with license plate {vehicle.LicensePlate} in the park";

            _dataRepository.AddVehicle(sector, placeNumber, vehicle);

            return $"{vehicle.GetType().Name} parked successfully at place ({sector},{placeNumber})";
        }

        public string ExitVehicle(string licensePlate, DateTime exitTime, decimal paid)
        {
            if (!_dataRepository.HasVehicle(licensePlate))
                return $"There is no vehicle with license plate {licensePlate} in the park";

            var vehicle = _dataRepository.GetVehicle(licensePlate);

            var enterTime = vehicle.EnterTime;
            var overtimeHours = (int) Math.Round((exitTime - enterTime).TotalHours) - vehicle.ReservedHours;
            var overtimeRate = overtimeHours > 0 ? overtimeHours * vehicle.OvertimeRate : 0;
            var regularRate = vehicle.RegularRate * vehicle.ReservedHours;

            _dataRepository.RemoveVehicle(vehicle.Sector, vehicle.Place);

            var stars = new string('*', 20);
            var ticket = new StringBuilder();
            ticket.AppendLine(stars)
                .AppendFormat("{0} [{1}], owned by {2}", vehicle.GetType().Name, vehicle.LicensePlate, vehicle.Owner)
                .AppendLine()
                .AppendFormat("at place ({0},{1})", vehicle.Sector, vehicle.Place)
                .AppendLine()
                .AppendFormat("Rate: ${0:F2}", regularRate)
                .AppendLine()
                .AppendFormat("Overtime rate: ${0:F2}", overtimeRate)
                .AppendLine()
                .AppendLine(new string('-', 20))
                .AppendFormat("Total: ${0:F2}", regularRate + overtimeRate)
                .AppendLine()
                .AppendFormat("Paid: ${0:F2}", paid)
                .AppendLine()
                .AppendFormat("Change: ${0:F2}", paid - (regularRate + overtimeRate))
                .AppendLine()
                .Append(stars);

            return ticket.ToString();
        }

        public string GetStatus()
        {
            var statistic = new List<string>(_layout.Sectors);
            for (var sector = 1; sector <= _layout.Sectors; sector++)
            {
                var occupiedPlaces = 0;
                for (var place = 1; place <= _layout.PlacesSec; place++)
                {
                    if (_dataRepository.IsPlaceOccupied(sector, place))
                        occupiedPlaces++;
                }
                var s =
                    $"Sector {sector}: {occupiedPlaces} / {_layout.PlacesSec} ({Math.Round((double) occupiedPlaces / _layout.PlacesSec * 100)}% full)";

                statistic.Add(s);
            }

            return string.Join(Environment.NewLine, statistic);
        }

        public string FindVehicle(string licensePlate)
        {
            var vehicle = _dataRepository.GetVehicle(licensePlate);
            if (vehicle == null)
                return $"There is no vehicle with license plate {licensePlate} in the park";

            return Input(new[] {vehicle});
        }

        public string FindVehiclesByOwner(string owner)
        {
            var vehicles = _dataRepository.FindVehiclesByOwner(owner);
            return vehicles.Any()
                ? string.Join(Environment.NewLine, Input(vehicles))
                : $"No vehicles by {owner}";
        }

        private static string Input(IEnumerable<IVehicle> vehicles)
        {
            return string.Join(Environment.NewLine,
                vehicles.Select(
                    vehicle =>
                        $"{vehicle.GetType().Name} [{vehicle.LicensePlate}], owned by {vehicle.Owner}{Environment.NewLine}Parked at ({vehicle.Sector},{vehicle.Place})"));
        }

        private void Validate(int sector, int placeNumber, IVehicle vehicle, DateTime startTime)
        {
            ValidatePlace(sector, placeNumber);
            ValidateVehicle(vehicle);

            if (startTime == DateTime.MinValue && startTime > DateTime.UtcNow)
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
            if (sector < 0 || sector > _layout.Sectors)
                throw new ArgumentException($"There is no sector {sector} in the park");
            if (placeNumber < 0 || placeNumber > _layout.PlacesSec)
                throw new ArgumentException($"There is no place {placeNumber} in sector {sector}");
        }
    }
}