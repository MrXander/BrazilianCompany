#region usings

using System;

#endregion

namespace BrazilianCompany.Model.Interface
{
    public interface IVehicle
    {
        string LicensePlate { get; }

        string Owner { get; }

        decimal RegularRate { get; }

        decimal OvertimeRate { get; }

        int ReservedHours { get; }

        DateTime EnterTime { get; }

        int Sector { get; }
        int Place { get; }

        string ToString();
    }
}