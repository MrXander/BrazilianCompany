#region usings

using System;
using System.Collections.Generic;
using System.Linq;
using BrazilianCompany.Logic.Interface;
using BrazilianCompany.Model.Command.Parameters;
using BrazilianCompany.Model.Interface;
using BrazilianCompany.Model.Model;

#endregion

namespace BrazilianCompany.Logic.Command
{
    internal class FindVehicleByOwnerCommand : ICommand
    {
        private readonly FindByOwnerParams _params;
        private IList<IVehicle> _state;

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
            if (_state == null || !_state.Any()) return $"No vehicles by {_params.Owner}";

            return string.Join(Environment.NewLine, _state);
        }
    }
}