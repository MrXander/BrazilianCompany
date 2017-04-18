#region usings

using System;
using Autofac;
using BrazilianCompany.Logic.Interface;
using BrazilianCompany.Model.Command.Parameters;
using BrazilianCompany.Model.Model;
using Newtonsoft.Json;

#endregion

namespace BrazilianCompany.Logic.Command
{
    internal class ParkCommand : ICommand
    {
        private readonly ParkParams _parkParams;
        private readonly IVehicleFactory _vehicleFactory;
        private string _state;

        public ParkCommand(string args, IContainer container)
        {
            _parkParams = ParamDeserializer.Deserialize<ParkParams>(args);
            _vehicleFactory = container.Resolve<IVehicleFactory>();
        }

        public void Execute(Context context)
        {
            var vehicle = _vehicleFactory.GetVehicle(_parkParams.LicensePlate,
                _parkParams.Owner,
                _parkParams.Type,
                _parkParams.Hours,
                _parkParams.Time,
                _parkParams.Sector,
                _parkParams.Place);
            _state = context.VehiclePark.Park(vehicle, _parkParams.Sector, _parkParams.Place, _parkParams.Time);
        }

        public object GetState()
        {
            return _state ?? string.Empty;
        }     
    }
}