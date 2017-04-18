#region usings

using System;

#endregion

namespace BrazilianCompany.Model.Model
{
    public class Layout
    {
        public Layout(int numberOfSectors, int placesPerSector)
        {
            if (numberOfSectors <= 0)
                throw new ArgumentException("The number of sectors must be positive.");
            if (placesPerSector <= 0)
                throw new ArgumentException("The number of places per sector must be positive.");

            Sectors = numberOfSectors;
            PlacesSec = placesPerSector;
        }

        public int PlacesSec { get; }
        public int Sectors { get; }
    }
}