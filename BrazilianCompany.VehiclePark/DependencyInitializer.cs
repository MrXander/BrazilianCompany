#region usings

using Autofac;
using BrazilianCompany.Logic.Implementation;
using BrazilianCompany.Logic.Interface;
using BrazilianCompany.Model.Interface;

#endregion

namespace BrazilianCompany.VehiclePark
{
    internal class DependencyInitializer
    {
        public static IContainer Initialize()
        {
            var builder = new ContainerBuilder();

            builder.RegisterType<ConsoleInterface>()
                .As<IUserInterface>();

            builder.RegisterType<Logic.Implementation.VehiclePark>()
                .As<IVehiclePark>();

            builder.RegisterType<Engine>()
                .As<IEngine>();

            builder.RegisterType<VehicleFactory>()
                .As<IVehicleFactory>();

            return builder.Build();
        }
    }
}