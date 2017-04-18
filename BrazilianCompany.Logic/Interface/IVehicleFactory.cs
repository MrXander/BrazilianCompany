#region usings

using System;
using BrazilianCompany.Model.Interface;

#endregion

namespace BrazilianCompany.Logic.Interface
{
    public interface IVehicleFactory
    {
        IVehicle GetVehicle(string licensePlate, string owner, string type, int reservedHours, DateTime enterTime,
            int sector, int place);
    }
}