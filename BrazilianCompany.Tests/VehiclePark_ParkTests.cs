#region usings

using System;
using BrazilianCompany.DataAccess;
using BrazilianCompany.Logic.Implementation;
using BrazilianCompany.Model.Implementation.Vehicle;
using Microsoft.VisualStudio.TestTools.UnitTesting;

#endregion

namespace BrazilianCompany.Tests
{
    [TestClass]
    public class VehiclePark_ParkTests
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
        public void Park_SectorLessThanZero_ArgumentException()
        {
            var sector = -1;
            var place = 1;
            var vehicle = new Car("AA1111AA", "DrHouse", 1, DateTime.UtcNow, sector, place);
            _parkVehicle.Park(vehicle, sector, place, DateTime.UtcNow);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Park_PlaceLessThanZero_ArgumentException()
        {
            var sector = 1;
            var place = -1;
            var vehicle = new Car("AA1111AA", "DrHouse", 1, DateTime.UtcNow, sector, place);
            _parkVehicle.Park(vehicle, sector, place, DateTime.UtcNow);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Park_SectorDoesNotExist_ArgumentException()
        {
            var sector = 4;
            var place = 1;
            var vehicle = new Car("AA1111AA", "DrHouse", 1, DateTime.UtcNow, sector, place);
            _parkVehicle.Park(vehicle, sector, place, DateTime.UtcNow);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Park_PlaceDoesNotExist_ArgumentException()
        {
            var sector = 1;
            var place = 6;
            var vehicle = new Car("AA1111AA", "DrHouse", 1, DateTime.UtcNow, sector, place);
            _parkVehicle.Park(vehicle, sector, place, DateTime.UtcNow);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void Park_ReservedHoursIsNegative_InvalidOperationException()
        {
            var sector = 1;
            var placeNumber = 1;
            var reservedHours = -1;
            var vehicle = new Car("AA1111AA", "DrHouse", reservedHours, DateTime.UtcNow, sector, placeNumber);
            _parkVehicle.Park(vehicle, sector, placeNumber, DateTime.UtcNow);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void Park_ReservedHoursIsZero_InvalidOperationException()
        {
            var sector = 1;
            var placeNumber = 1;
            var reservedHours = 0;
            var vehicle = new Car("AA1111AA", "DrHouse", reservedHours, DateTime.UtcNow, sector, placeNumber);
            _parkVehicle.Park(vehicle, sector, placeNumber, DateTime.UtcNow);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void Park_EnterDateInFuture_InvalidOperationException()
        {
            var sector = 1;
            var placeNumber = 1;
            var reservedHours = 1;
            var enterDate = DateTime.UtcNow.AddDays(5);
            var vehicle = new Car("AA1111AA", "DrHouse", reservedHours, enterDate, sector, placeNumber);
            _parkVehicle.Park(vehicle, sector, placeNumber, enterDate);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void Park_InvalidLicensePlate_InvalidOperationException()
        {
            var sector = 1;
            var placeNumber = 1;
            var reservedHours = 1;
            var enterDate = DateTime.UtcNow.AddDays(5);
            var vehicle = new Car("1111AA", "DrHouse", reservedHours, enterDate, sector, placeNumber);
            _parkVehicle.Park(vehicle, sector, placeNumber, enterDate);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void Park_EmptyLicensePlate_InvalidOperationException()
        {
            var sector = 1;
            var placeNumber = 1;
            var reservedHours = 1;
            var enterDate = DateTime.UtcNow.AddDays(5);
            var vehicle = new Car(string.Empty, "DrHouse", reservedHours, enterDate, sector, placeNumber);
            _parkVehicle.Park(vehicle, sector, placeNumber, enterDate);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void Park_EmptyOwner_InvalidOperationException()
        {
            var sector = 1;
            var placeNumber = 1;
            var reservedHours = 1;
            var enterDate = DateTime.UtcNow.AddDays(5);
            var vehicle = new Car("AA1111AA", string.Empty, reservedHours, enterDate, sector, placeNumber);
            _parkVehicle.Park(vehicle, sector, placeNumber, enterDate);
        }

        #endregion

        #region positive tests

        [TestMethod]
        public void Park_ValidParams_PlaceOccupied()
        {
            var sector = 1;
            var placeNumber = 1;
            var vehicle = new Car("AA1111AA", "DrHouse", 1, DateTime.UtcNow, sector, placeNumber);
            var result = _parkVehicle.Park(vehicle, sector, placeNumber, DateTime.UtcNow);

            Assert.AreEqual($"{vehicle.GetType().Name} parked successfully at place ({sector},{placeNumber})", result);

            var isPlaceOccupied = _parkVehicle.Repository.IsPlaceOccupied(sector, placeNumber);

            Assert.AreEqual(true, isPlaceOccupied);
        }

        [TestMethod]
        public void Park_ValidParamsAndFirstOneLetterInLicensePlate_PlaceOccupied()
        {
            var sector = 1;
            var placeNumber = 1;
            var vehicle = new Car("A1111AA", "DrHouse", 1, DateTime.UtcNow, sector, placeNumber);
            var result = _parkVehicle.Park(vehicle, sector, placeNumber, DateTime.UtcNow);

            Assert.AreEqual($"{vehicle.GetType().Name} parked successfully at place ({sector},{placeNumber})", result);

            var isPlaceOccupied = _parkVehicle.Repository.IsPlaceOccupied(sector, placeNumber);

            Assert.AreEqual(true, isPlaceOccupied);
        }

        [TestMethod]
        public void Park_ParkOnOccupiedPlace_Error()
        {
            var sector = 1;
            var placeNumber = 1;
            var vehicle1 = new Car("AA1111AA", "DrHouse", 1, DateTime.UtcNow, sector, placeNumber);
            var result1 = _parkVehicle.Park(vehicle1, sector, placeNumber, DateTime.UtcNow);

            Assert.AreEqual($"{vehicle1.GetType().Name} parked successfully at place ({sector},{placeNumber})", result1);

            var isPlaceOccupied = _parkVehicle.Repository.IsPlaceOccupied(sector, placeNumber);

            Assert.IsTrue(isPlaceOccupied);

            var vehicle2 = new Car("AA2222AA", "DrRichard", 1, DateTime.UtcNow, sector, placeNumber);
            var result2 = _parkVehicle.Park(vehicle2, sector, placeNumber, DateTime.UtcNow);

            Assert.AreEqual($"The place ({sector},{placeNumber}) is occupied", result2);

            var isVehicle2InPark =_parkVehicle.Repository.HasVehicle("AA2222AA");
            Assert.IsFalse(isVehicle2InPark);
        }

        [TestMethod]
        public void Park_VehicleAlreadyInPark_Error()
        {
            var sector = 1;
            var placeNumber = 1;
            var vehicle1 = new Car("AA1111AA", "DrHouse", 1, DateTime.UtcNow, sector, placeNumber);
            var result1 = _parkVehicle.Park(vehicle1, sector, placeNumber, DateTime.UtcNow);

            Assert.AreEqual($"{vehicle1.GetType().Name} parked successfully at place ({sector},{placeNumber})", result1);

            var isPlaceOccupied = _parkVehicle.Repository.IsPlaceOccupied(sector, placeNumber);

            Assert.IsTrue(isPlaceOccupied);

            var sector2 = 1;
            var placeNumber2 = 2;
            var vehicle2 = new Car("AA1111AA", "DrRichard", 1, DateTime.UtcNow, sector2, placeNumber2);
            var result2 = _parkVehicle.Park(vehicle2, sector2, placeNumber2, DateTime.UtcNow);

            Assert.AreEqual($"There is already a vehicle with license plate {vehicle2.LicensePlate} in the park", result2);            
        }

        #endregion       
    }
}