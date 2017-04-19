#region usings

using System;
using System.Collections.Generic;
using BrazilianCompany.DataAccess;
using BrazilianCompany.Model;
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

        [TestMethod]
        public void HasCarForOwner()
        {
            var licensePlate = "AA1111AA";
            var owner = "DrHouse";

            var mock = new Mock<IDataRepository>();
            mock.Setup(m => m.FindVehiclesByOwner(It.IsAny<string>()))
                .Returns((string ow) => new List<IVehicle>
                {
                    new Vehicle(RateConstants.CAR_REGULAR_RATE, RateConstants.CAR_OVERTIME_RATE, VehicleType.Car,
                        licensePlate, ow, 1, DateTime.UtcNow, 1, 1)
                });

            _parkVehicle = new VehicleParkTestMock(2, 3, mock.Object);

            var result = _parkVehicle.FindVehiclesByOwner(owner);
            Assert.IsNotNull(result);
            Assert.AreEqual(1, result.Count);
            Assert.AreEqual(licensePlate, result[0].LicensePlate);
        }
    }
}