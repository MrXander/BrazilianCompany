#region usings

using BrazilianCompany.DataAccess;
using BrazilianCompany.Logic.Implementation;

#endregion

namespace BrazilianCompany.Tests
{
    internal class VehicleParkTestMock : VehiclePark
    {
        public VehicleParkTestMock(int numberOfSectors, int placesPerSector, IDataRepository dataRepository)
            : base(numberOfSectors, placesPerSector, dataRepository)
        {
        }

        public IDataRepository Repository => DataRepository;
    }
}