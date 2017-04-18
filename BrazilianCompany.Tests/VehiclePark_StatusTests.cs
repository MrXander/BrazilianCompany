#region usings

using System;
using BrazilianCompany.DataAccess;
using BrazilianCompany.Model.Implementation.Vehicle;
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
            _parkVehicle = new VehicleParkTestMock(2, 3, new DataRepository());
        }

        [TestMethod]
        public void NoVehiclesInPark()
        {
            var statuses = _parkVehicle.GetStatus();
            Assert.AreEqual(2, statuses.Count);
            Assert.AreEqual(0, statuses[0].OccupiedPlaces);
            Assert.AreEqual(0, statuses[1].OccupiedPlaces);
            Assert.AreEqual(0, statuses[0].OccupiedPercent);
            Assert.AreEqual(0, statuses[1].OccupiedPercent);
        }

        [TestMethod]
        public void OneVehicle()
        {
            var sector = 1;
            var placeNumber = 1;
            var vehicle = new Car("AA1111AA", "DrHouse", 1, DateTime.UtcNow, sector, placeNumber);
            _parkVehicle.Park(vehicle, sector, placeNumber, DateTime.UtcNow);

            var statuses = _parkVehicle.GetStatus();
            Assert.AreEqual(2, statuses.Count);
            Assert.AreEqual(1, statuses[0].OccupiedPlaces);
            Assert.AreEqual(0, statuses[1].OccupiedPlaces);
            Assert.AreEqual(33, statuses[0].OccupiedPercent);
            Assert.AreEqual(0, statuses[1].OccupiedPercent);
        }

        [TestMethod]
        public void VehicleLeave()
        {
            var sector = 1;
            var placeNumber = 1;
            var vehicle = new Car("AA1111AA", "DrHouse", 1, DateTime.UtcNow, sector, placeNumber);
            _parkVehicle.Park(vehicle, sector, placeNumber, DateTime.UtcNow);

            var statuses = _parkVehicle.GetStatus();
            Assert.AreEqual(2, statuses.Count);
            Assert.AreEqual(1, statuses[0].OccupiedPlaces);
            Assert.AreEqual(0, statuses[1].OccupiedPlaces);
            Assert.AreEqual(33, statuses[0].OccupiedPercent);
            Assert.AreEqual(0, statuses[1].OccupiedPercent);

            _parkVehicle.ExitVehicle(vehicle.LicensePlate, DateTime.UtcNow.AddHours(1), 10);

            statuses = _parkVehicle.GetStatus();
            Assert.AreEqual(2, statuses.Count);
            Assert.AreEqual(0, statuses[0].OccupiedPlaces);
            Assert.AreEqual(0, statuses[1].OccupiedPlaces);
            Assert.AreEqual(0, statuses[0].OccupiedPercent);
            Assert.AreEqual(0, statuses[1].OccupiedPercent);
        }
    }
}