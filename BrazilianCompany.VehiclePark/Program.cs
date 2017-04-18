#region usings

using Autofac;
using BrazilianCompany.Logic.Implementation;

#endregion

namespace BrazilianCompany.VehiclePark
{
    public static class Program
    {
        private static IContainer Container { get; set; }

        public static void Main()
        {
            Container = DependencyInitializer.Initialize();

            var engine = new Engine(Container);
            engine.Start();
        }
    }
}