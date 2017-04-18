#region usings

using System;
using BrazilianCompany.Model.Interface;

#endregion

namespace BrazilianCompany.Model.Implementation.Vehicle
{
    public class Motorbike : IVehicle
    {
        private const decimal REGULAR_RATE = 1.35M;
        private const decimal OVERTIME_RATE = 3M;

        public Motorbike(string licensePlate, string owner, int reservedHours, DateTime enterTime, int sector,
            int place)
        {
            LicensePlate = licensePlate;
            Owner = owner;
            ReservedHours = reservedHours;
            EnterTime = enterTime;
            Sector = sector;
            Place = place;
            EnterTime = DateTime.UtcNow;
        }

        public string LicensePlate { get; }
        public string Owner { get; }
        public decimal RegularRate => REGULAR_RATE;
        public decimal OvertimeRate => OVERTIME_RATE;
        public int ReservedHours { get; }
        public DateTime EnterTime { get; }
        public int Sector { get; }
        public int Place { get; }

        public override string ToString()
        {
            return $"{GetType().Name} [{LicensePlate}], owned by {Owner}{Environment.NewLine}Parked at ({Sector},{Place})";
        }
    }
}