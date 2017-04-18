#region usings

using BrazilianCompany.Logic.Interface;
using BrazilianCompany.Model.Command.Parameters;
using BrazilianCompany.Model.Interface;
using BrazilianCompany.Model.Model;
using Newtonsoft.Json;

#endregion

namespace BrazilianCompany.Logic.Command
{
    internal class FindVehicleCommand : ICommand
    {
        private readonly FindVehicleParams _params;
        private IVehicle _state;

        public FindVehicleCommand(string args)
        {
            _params = ParamDeserializer.Deserialize<FindVehicleParams>(args);
        }

        public void Execute(Context context)
        {
            _state = context.VehiclePark.FindVehicle(_params.LicensePlate);
        }

        public object GetState()
        {
            return (object) _state ?? string.Empty;
        }
    }
}