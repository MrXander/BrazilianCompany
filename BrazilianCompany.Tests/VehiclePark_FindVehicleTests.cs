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
    public class VehiclePark_FindVehicleTests
    {
        private VehicleParkTestMock _parkVehicle;

        [TestMethod]
        public void VehicleFound()
        {
            var licensePlate = "AA1111AA";
            var owner = "DrHouse";

            var mock = new Mock<IDataRepository>();
            mock.Setup(m => m.FindVehicle(It.IsAny<string>()))
                .Returns((string lp) => new Car(lp, owner, 1, DateTime.UtcNow, 1, 1));

            _parkVehicle = new VehicleParkTestMock(2, 3, mock.Object);

            var result = _parkVehicle.FindVehicle(licensePlate);
            Assert.IsNotNull(result);            
            Assert.AreEqual(licensePlate, result.LicensePlate);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void NoVehicleFound()
        {
            var licensePlate = "AA1111AA";
           
            _parkVehicle = new VehicleParkTestMock(2, 3, new DataRepository());

            _parkVehicle.FindVehicle(licensePlate);          
        }
    }
}