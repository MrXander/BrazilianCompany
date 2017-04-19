#region usings

using System;
using System.Collections.Generic;
using BrazilianCompany.Logic.Implementation;
using BrazilianCompany.Model.Model;

#endregion

namespace BrazilianCompany.Model.Interface
{
    public interface IVehiclePark
    {
        /// <summary>
        ///     Try to park a vehicle.
        ///     If the sector does not exist (for example, searching for the fifth sector of a parking lot which only
        ///     has two sectors), the system prints "There is no sector
        ///     <sector>
        ///         in the park".
        ///         If the place does not exist(for example, searching for the tenth place of a parking lot which only
        ///         has two places per sector), the system prints "There is no place
        ///         <place>
        ///             in sector
        ///             <sector>
        ///                 "
        ///                 If there is already a parked vehicle in the place, the system prints "The place (
        ///                 <sector>
        ///                     ,
        ///                     <place>
        ///                         )
        ///                         is occupied".
        ///                         If there is already a vehicle with the provided license plate in the park, the system prints
        ///                         "There
        ///                         is already a vehicle with license plate
        ///                         <license_plate>
        ///                             in the park". The license plate number is
        ///                             unique and this means something tries to enter the park twice without exiting first
        /// </summary>
        /// <param name="vehicle">Vehicle to park</param>
        /// <param name="sector">Sector number</param>
        /// <param name="placeNumber">Place number in sector</param>
        /// <param name="startTime">Time arrived</param>
        /// <returns></returns>
        string Park(IVehicle vehicle, int sector, int placeNumber, DateTime startTime);

        /// <summary>
        ///     Calculates the number of hours the vehicle has stayed in the park. If needed, it
        ///     rounds them up to the nearest hour(for example, 3:40 hours is rounded to 4:00 hours, and 3:10
        ///     is rounded to 3:00 hours). Then it calculates the price for the ticket.After paying the amount,
        ///     the system prints the ticket
        /// </summary>
        /// <param name="licensePlate">License plate</param>
        /// <param name="exitTime">Exit time</param>
        /// <param name="paid">Amount paid</param>
        /// <returns></returns>
        Ticket ExitVehicle(string licensePlate, DateTime exitTime, decimal paid);

        /// <summary>
        ///     Prints the current status of the parking lot
        /// </summary>
        /// <returns></returns>
        List<SectorStatus> GetStatus();

        /// <summary>
        ///     Tries to find a vehicle with the specified license plate number in the parking lot.
        /// </summary>
        /// <param name="licensePlate">License plate</param>
        /// <returns></returns>
        IVehicle FindVehicle(string licensePlate);

        /// <summary>
        ///     Lists all vehicles by the specified owner in the parking lot, ordered by arrival time (in ascending
        ///     order) first, and by license plate number(in ascending order) next.
        /// </summary>
        /// <param name="owner">Owner</param>
        /// <returns></returns>
        IList<IVehicle> FindVehiclesByOwner(string owner);
    }
}