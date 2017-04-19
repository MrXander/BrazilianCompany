#region usings

using System;
using BrazilianCompany.Logic.Interface;
using BrazilianCompany.Model;
using BrazilianCompany.Model.Implementation.Vehicle;
using BrazilianCompany.Model.Interface;

#endregion

namespace BrazilianCompany.Logic.Implementation
{
    public class VehicleFactory : IVehicleFactory
    {
        public IVehicle GetVehicle(string licensePlate, string owner, string type, int reservedHours,
            DateTime enterTime, int sector, int place)
        {
            if (string.IsNullOrWhiteSpace(type))
                throw new ArgumentException("Invalid vehicle type");

            VehicleType vehicleType;
            if (!Enum.TryParse(type.Trim(), true, out vehicleType))
                throw new ArgumentException("Invalid vehicle type");

            switch (vehicleType)
            {
                case VehicleType.Car:
                    return new Vehicle(RateConstants.CAR_REGULAR_RATE, RateConstants.CAR_OVERTIME_RATE, VehicleType.Car,
                        licensePlate, owner, reservedHours, enterTime, sector, place);
                case VehicleType.Motorbike:
                    return new Vehicle(RateConstants.MOTO_REGULAR_RATE, RateConstants.MOTO_OVERTIME_RATE,
                        VehicleType.Motorbike, licensePlate, owner, reservedHours, enterTime, sector, place);
                case VehicleType.Truck:
                    return new Vehicle(RateConstants.TRUCK_REGULAR_RATE, RateConstants.TRUCK_OVERTIME_RATE,
                        VehicleType.Truck, licensePlate, owner, reservedHours, enterTime, sector, place);
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}