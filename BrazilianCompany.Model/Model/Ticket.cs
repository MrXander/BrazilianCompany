#region usings

using System.Text;
using BrazilianCompany.Model.Interface;

#endregion

namespace BrazilianCompany.Logic.Implementation
{
    public class Ticket
    {
        private readonly decimal _paid;
        private readonly IVehicle _vehicle;

        public Ticket(IVehicle vehicle, decimal rate, decimal overtimeRate, decimal total, decimal paid, decimal change)
        {
            _vehicle = vehicle;
            Rate = rate;
            OvertimeRate = overtimeRate;
            Total = total;
            _paid = paid;
            Change = change;
        }

        public decimal Change { get; }
        public decimal OvertimeRate { get; }
        public decimal Rate { get; }
        public decimal Total { get; }

        public override string ToString()
        {
            var ticket = new StringBuilder();
            var stars = new string('*', 20);
            ticket.AppendLine(stars)
                .AppendFormat("{0} [{1}], owned by {2}", _vehicle.GetType().Name, _vehicle.LicensePlate, _vehicle.Owner)
                .AppendLine()
                .AppendFormat("at place ({0},{1})", _vehicle.Sector, _vehicle.Place)
                .AppendLine()
                .AppendFormat("Rate: ${0:F2}", Rate)
                .AppendLine()
                .AppendFormat("Overtime rate: ${0:F2}", OvertimeRate)
                .AppendLine()
                .AppendLine(new string('-', 20))
                .AppendFormat("Total: ${0:F2}", Total)
                .AppendLine()
                .AppendFormat("Paid: ${0:F2}", _paid)
                .AppendLine()
                .AppendFormat("Change: ${0:F2}", Change)
                .AppendLine()
                .Append(stars);

            return ticket.ToString();
        }
    }
}