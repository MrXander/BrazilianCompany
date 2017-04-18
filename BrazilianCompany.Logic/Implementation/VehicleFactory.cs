#region usings

using System;
using BrazilianCompany.Logic.Interface;
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

            type = type.Trim().ToLowerInvariant();

            switch (type)
            {
                case "car":
                    return new Car(licensePlate, owner, reservedHours, enterTime, sector, place);
                case "motorbike":
                    return new Motorbike(licensePlate, owner, reservedHours, enterTime, sector, place);
                case "truck":
                    return new Truck(licensePlate, owner, reservedHours, enterTime, sector, place);
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}