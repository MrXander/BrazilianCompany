#region usings

using System;
using System.Collections.Generic;
using BrazilianCompany.DataAccess;
using BrazilianCompany.Model.Implementation.Vehicle;
using BrazilianCompany.Model.Interface;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

#endregion

namespace BrazilianCompany.Tests
{
    [TestClass]
    public class VehiclePark_FindVehicleByOwnerTests
    {
        private VehicleParkTestMock _parkVehicle;

        [TestInitialize]
        public void Initialize()
        {
            _parkVehicle = new VehicleParkTestMock(2, 3, new DataRepository());
        }

        [TestMethod]
        public void FindByOwner()
        {
            var licensePlate = "AA1111AA";
            var owner = "DrHouse";

            var mock = new Mock<IDataRepository>();
            mock.Setup(m => m.FindVehiclesByOwner(owner))
                .Returns<List<IVehicle>>(vehicle => new List<IVehicle>
                {
                    new Car(licensePlate, owner, 1, DateTime.UtcNow, 1, 1)
                });

            var test = mock.Object.FindVehiclesByOwner(owner);

            _parkVehicle = new VehicleParkTestMock(2, 3, mock.Object);

            var result = _parkVehicle.FindVehiclesByOwner(owner);
            Assert.IsNotNull(result);
            Assert.AreEqual(1, result.Count);
            Assert.AreEqual(licensePlate, result[0].LicensePlate);
        }
    }
}