#region usings

using System;
using BrazilianCompany.DataAccess;
using BrazilianCompany.Model;
using BrazilianCompany.Model.Implementation.Vehicle;
using Microsoft.VisualStudio.TestTools.UnitTesting;

#endregion

namespace BrazilianCompany.Tests
{
    [TestClass]
    public class VehiclePark_ExitVehicleTests
    {
        private VehicleParkTestMock _parkVehicle;

        [TestInitialize]
        public void Initialize()
        {
            _parkVehicle = new VehicleParkTestMock(3, 5, new DataRepository());
        }

        #region negative tests

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Park_NoVehicleInPark_ArgumentException()
        {
            _parkVehicle.ExitVehicle("AA1111AA", DateTime.UtcNow, 0);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Park_ExitTimeEqualEnterTime_ArgumentException()
        {
            var sector = 1;
            var placeNumber = 1;
            var reservedHours = 1;
            var time = DateTime.UtcNow;
            var paid = 10;
            var vehicle = new Vehicle(RateConstants.CAR_REGULAR_RATE, RateConstants.CAR_OVERTIME_RATE, VehicleType.Car,
                "AA1111AA", "DrHouse", reservedHours, time, sector, placeNumber);
            _parkVehicle.Park(vehicle, sector, placeNumber, time);
            _parkVehicle.ExitVehicle(vehicle.LicensePlate, time, paid);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Park_PaidZero_ArgumentException()
        {
            var sector = 1;
            var placeNumber = 1;
            var reservedHours = 1;
            var time = DateTime.UtcNow;
            var paid = 0;
            var vehicle = new Vehicle(RateConstants.CAR_REGULAR_RATE, RateConstants.CAR_OVERTIME_RATE, VehicleType.Car,
                "AA1111AA", "DrHouse", reservedHours, time, sector, placeNumber);
            _parkVehicle.Park(vehicle, sector, placeNumber, time);
            _parkVehicle.ExitVehicle(vehicle.LicensePlate, time, paid);
        }

        #endregion

        #region positive tests

        [TestMethod]
        public void Park_ReservedHoursEqualsActualHours()
        {
            var sector = 1;
            var placeNumber = 1;
            var reservedHours = 1;
            var exitDate = DateTime.UtcNow.AddHours(1);
            var paid = 10;
            var vehicle = new Vehicle(RateConstants.CAR_REGULAR_RATE, RateConstants.CAR_OVERTIME_RATE, VehicleType.Car,
                "AA1111AA", "DrHouse", reservedHours, DateTime.UtcNow, sector, placeNumber);
            _parkVehicle.Park(vehicle, sector, placeNumber, DateTime.UtcNow);
            var ticket = _parkVehicle.ExitVehicle(vehicle.LicensePlate, exitDate, paid);

            Assert.AreEqual(vehicle.RegularRate * reservedHours, ticket.Rate);
            Assert.AreEqual(0, ticket.OvertimeRate);
            Assert.AreEqual(paid - vehicle.RegularRate, ticket.Change);

            var isVehicleInPark = _parkVehicle.Repository.HasVehicle(vehicle.LicensePlate);
            Assert.IsFalse(isVehicleInPark);
        }

        [TestMethod]
        public void Park_ReservedHoursMoreThanActualHours()
        {
            var sector = 1;
            var placeNumber = 1;
            var reservedHours = 2;
            var exitDate = DateTime.UtcNow.AddHours(1);
            var paid = 10;
            var vehicle = new Vehicle(RateConstants.CAR_REGULAR_RATE, RateConstants.CAR_OVERTIME_RATE, VehicleType.Car,
                "AA1111AA", "DrHouse", reservedHours, DateTime.UtcNow, sector, placeNumber);
            _parkVehicle.Park(vehicle, sector, placeNumber, DateTime.UtcNow);
            var ticket = _parkVehicle.ExitVehicle(vehicle.LicensePlate, exitDate, paid);

            Assert.AreEqual(vehicle.RegularRate * reservedHours, ticket.Rate);
            Assert.AreEqual(0, ticket.OvertimeRate);
            Assert.AreEqual(paid - vehicle.RegularRate * reservedHours, ticket.Change);

            var isVehicleInPark = _parkVehicle.Repository.HasVehicle(vehicle.LicensePlate);
            Assert.IsFalse(isVehicleInPark);
        }

        [TestMethod]
        public void Park_ReservedHoursLessThanActualHours()
        {
            var sector = 1;
            var placeNumber = 1;
            var reservedHours = 1;
            var exitDate = DateTime.UtcNow.AddHours(2);
            var paid = 10;
            var vehicle = new Vehicle(RateConstants.CAR_REGULAR_RATE, RateConstants.CAR_OVERTIME_RATE, VehicleType.Car,
                "AA1111AA", "DrHouse", reservedHours, DateTime.UtcNow, sector, placeNumber);
            _parkVehicle.Park(vehicle, sector, placeNumber, DateTime.UtcNow);
            var ticket = _parkVehicle.ExitVehicle(vehicle.LicensePlate, exitDate, paid);

            Assert.AreEqual(vehicle.RegularRate * reservedHours, ticket.Rate);
            Assert.AreEqual(vehicle.OvertimeRate * 1, ticket.OvertimeRate);
            Assert.AreEqual(paid - vehicle.RegularRate * reservedHours - vehicle.OvertimeRate, ticket.Change);

            var isVehicleInPark = _parkVehicle.Repository.HasVehicle(vehicle.LicensePlate);
            Assert.IsFalse(isVehicleInPark);
        }

        [TestMethod]
        public void Park_ValidParamsNoOvertimeCharge0()
        {
            var sector = 1;
            var placeNumber = 1;
            var reservedHours = 1;
            var exitDate = DateTime.UtcNow.AddHours(1);
            var vehicle = new Vehicle(RateConstants.CAR_REGULAR_RATE, RateConstants.CAR_OVERTIME_RATE, VehicleType.Car,
                "AA1111AA", "DrHouse", reservedHours, DateTime.UtcNow, sector, placeNumber);
            var paid = vehicle.RegularRate;

            _parkVehicle.Park(vehicle, sector, placeNumber, DateTime.UtcNow);
            var ticket = _parkVehicle.ExitVehicle(vehicle.LicensePlate, exitDate, paid);

            Assert.AreEqual(vehicle.RegularRate, ticket.Rate);
            Assert.AreEqual(0, ticket.OvertimeRate);
            Assert.AreEqual(0, ticket.Change);

            var isVehicleInPark = _parkVehicle.Repository.HasVehicle(vehicle.LicensePlate);
            Assert.IsFalse(isVehicleInPark);
        }

        #endregion
    }
}