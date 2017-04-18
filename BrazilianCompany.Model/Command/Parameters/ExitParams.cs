#region usings

using System;

#endregion

namespace BrazilianCompany.Model.Command.Parameters
{
    public class ExitParams
    {
        public string LicensePlate { get; set; }
        public DateTime Time { get; set; }
        public decimal Paid { get; set; }
    }
}