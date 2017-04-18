#region usings

using System;
using BrazilianCompany.Logic.Interface;
using BrazilianCompany.Model.Command.Parameters;
using BrazilianCompany.Model.Model;
using Newtonsoft.Json;

#endregion

namespace BrazilianCompany.Logic.Command
{
    internal class FindVehicleByOwnerCommand : ICommand
    {
        private readonly FindByOwnerParams _params;
        private string _state;

        public FindVehicleByOwnerCommand(string args)
        {            
            _params = ParamDeserializer.Deserialize<FindByOwnerParams>(args);
        }

        public void Execute(Context context)
        {
            _state = context.VehiclePark.FindVehiclesByOwner(_params.Owner);
        }

        public object GetState()
        {
            return _state ?? string.Empty;
        }
    }
}