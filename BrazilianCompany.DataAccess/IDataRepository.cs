#region usings

using System.Collections.Generic;
using BrazilianCompany.Model.Interface;

#endregion

namespace BrazilianCompany.DataAccess
{
    public interface IDataRepository
    {
        void AddVehicle(int sector, int place, IVehicle vehicle);
        void RemoveVehicle(int sector, int place);
        bool IsPlaceOccupied(int sector, int place);
        IVehicle GetVehicle(string licensePlate);
        bool HasVehicle(string licensePlate);
        IList<IVehicle> FindVehiclesByOwner(string owner);
    }
}