#region usings

using System;
using System.Text;
using BrazilianCompany.Logic.Implementation;
using BrazilianCompany.Logic.Interface;
using BrazilianCompany.Model.Command.Parameters;
using BrazilianCompany.Model.Model;
using Newtonsoft.Json;

#endregion

namespace BrazilianCompany.Logic.Command
{
    internal class ExitCommand : ICommand
    {
        private readonly ExitParams _exitParams;
        private Ticket _state;

        public ExitCommand(string args)
        {
            _exitParams = ParamDeserializer.Deserialize<ExitParams>(args);
        }

        public void Execute(Context context)
        {
            _state = context.VehiclePark.ExitVehicle(_exitParams.LicensePlate, _exitParams.Time, _exitParams.Paid);
        }

        public object GetState()
        {
            return _state == null ? string.Empty : _state.ToString();
        }
    }
}