#region usings

using System;
using Autofac;
using BrazilianCompany.DataAccess;
using BrazilianCompany.Logic.Interface;
using BrazilianCompany.Model.Command.Parameters;
using BrazilianCompany.Model.Interface;
using BrazilianCompany.Model.Model;
using Newtonsoft.Json;

#endregion

namespace BrazilianCompany.Logic.Command
{
    internal class SetupParkCommand : ICommand
    {
        private readonly IContainer _container;
        private readonly SetupParkParams _setupParkParams;
        private bool _success;

        public SetupParkCommand(string args, IContainer container)
        {
            _container = container;            
            _setupParkParams = ParamDeserializer.Deserialize<SetupParkParams>(args);
        }

        public void Execute(Context context)
        {
            context.VehiclePark = _container.Resolve<IVehiclePark>(
                new NamedParameter("numberOfSectors", _setupParkParams.Sectors),
                new NamedParameter("placesPerSector", _setupParkParams.PlacesPerSector),
                new TypedParameter(typeof(IDataRepository), new DataRepository()));
            _success = true;
        }

        public object GetState()
        {
            return _success ? "Vehicle park created" : string.Empty;
        }
    }
}