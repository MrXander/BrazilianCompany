#region usings

using System;
using BrazilianCompany.Model.Interface;

#endregion

namespace BrazilianCompany.Model.Implementation.Vehicle
{
    public class Vehicle : IVehicle
    {
        public Vehicle(decimal regularRate, decimal overtimeRate, VehicleType type, string licensePlate, string owner,
            int reservedHours,
            DateTime enterTime, int sector, int place)
        {
            RegularRate = regularRate;
            OvertimeRate = overtimeRate;
            Type = type;
            LicensePlate = licensePlate;
            Owner = owner;
            ReservedHours = reservedHours;
            EnterTime = enterTime;
            Sector = sector;
            Place = place;
        }

        public VehicleType Type { get; }

        public string LicensePlate { get; }
        public string Owner { get; }
        public decimal RegularRate { get; }
        public decimal OvertimeRate { get; }

        public int ReservedHours { get; }
        public DateTime EnterTime { get; }
        public int Sector { get; }
        public int Place { get; }

        public override string ToString()
        {
            return
                $"{Type} [{LicensePlate}], owned by {Owner}{Environment.NewLine}Parked at ({Sector},{Place})";
        }
    }
}