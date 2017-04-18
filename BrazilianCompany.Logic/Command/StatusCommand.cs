#region usings

using System;
using System.Collections.Generic;
using System.Linq;
using BrazilianCompany.Logic.Interface;
using BrazilianCompany.Model.Model;

#endregion

namespace BrazilianCompany.Logic.Command
{
    internal class StatusCommand : ICommand
    {
        private List<SectorStatus> _state;

        public StatusCommand(string args)
        {
            if (string.IsNullOrWhiteSpace(args))
                throw new InvalidOperationException("Invalid Command");

            if (!string.Equals(args.Trim(), "{}", StringComparison.OrdinalIgnoreCase))
                throw new InvalidOperationException("Invalid Command");
        }

        public void Execute(Context context)
        {
            _state = context.VehiclePark.GetStatus();
        }

        public object GetState()
        {
            if (_state == null || !_state.Any()) return string.Empty;

            return string.Join(Environment.NewLine, _state);
        }
    }
}