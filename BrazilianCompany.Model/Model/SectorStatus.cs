#region usings

using System;

#endregion

namespace BrazilianCompany.Model.Model
{
    public class SectorStatus
    {
        public SectorStatus(int sector, int placesCount, int occupiedPlaces)
        {
            Sector = sector;
            PlacesCount = placesCount;
            OccupiedPlaces = occupiedPlaces;
        }

        public int Sector { get; }
        public int PlacesCount { get; }
        public int OccupiedPlaces { get; }
        public double OccupiedPercent => Math.Round((double) OccupiedPlaces / PlacesCount * 100);

        public override string ToString()
        {
            return
                $"Sector {Sector}: {OccupiedPlaces} / {PlacesCount} ({OccupiedPercent}% full)";
        }
    }
}