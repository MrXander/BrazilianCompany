#region usings

using System;

#endregion

namespace BrazilianCompany.Model.Command.Parameters
{
    public class ParkParams
    {
        public string Type { get; set; }
        public DateTime Time { get; set; }
        public int Sector { get; set; }
        public int Place { get; set; }
        public string LicensePlate { get; set; }
        public string Owner { get; set; }
        public int Hours { get; set; }
    }
}