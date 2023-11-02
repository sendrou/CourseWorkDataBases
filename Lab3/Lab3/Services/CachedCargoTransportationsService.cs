using Lab02;
using Lab3.Models;
using Microsoft.Extensions.Caching.Memory;

namespace Lab3.Services
{
    public class CachedCargoTransportationsService
    {
        private Lab1Context db;
        private IMemoryCache cache;
        private int _rowsNumber;
        public CachedCargoTransportationsService(Lab1Context context, IMemoryCache memoryCache)
        {
            db = context;
            cache = memoryCache;
            _rowsNumber = 20;
        }
        public IEnumerable<CargoTransportation> GetCargoTransportationsAll()
        {
            IEnumerable<CargoTransportation> cargoTransportations = new List<CargoTransportation>();
            cargoTransportations = db.CargoTransportations.ToList();

            return cargoTransportations;
        }
        public IEnumerable<CargoTransportation> GetCargoTransportations()
        {
            string cacheKey = "GetCargoTransportations";
            IEnumerable<CargoTransportation> cargoTransportations = null;
            if (!cache.TryGetValue(cacheKey, out cargoTransportations))
            {

                Console.WriteLine($"Населенные пункты извлечены из базы данных");
                cargoTransportations = db.CargoTransportations.Take(_rowsNumber).ToList();
                if (cargoTransportations != null)
                {
                    cache.Set(cacheKey, cargoTransportations,
                        new MemoryCacheEntryOptions().SetAbsoluteExpiration(TimeSpan.FromSeconds(296)));
                }
            }
            else
            {

                Console.WriteLine($"Населенные пункты извлечены из кэша");
            }

            return cargoTransportations;
        }
    }
}

