#region usings

using BrazilianCompany.DataAccess;
using Microsoft.VisualStudio.TestTools.UnitTesting;

#endregion

namespace BrazilianCompany.Tests
{
    [TestClass]
    public class VehiclePark_StatusTests
    {
        private VehicleParkTestMock _parkVehicle;

        [TestInitialize]
        public void Initialize()
        {
            _parkVehicle = new VehicleParkTestMock(3, 5, new DataRepository());
        }

        [TestMethod]
        public void NoVehiclesInPark()
        {


        }
    }
}