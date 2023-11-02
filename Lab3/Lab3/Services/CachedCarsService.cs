using Lab02;
using Lab3.Models;
using Microsoft.Extensions.Caching.Memory;

namespace Lab03.Services
{
    public class CachedCarsService
    {
        private Lab1Context db;
        private IMemoryCache cache;
        private int _rowsNumber;
        public CachedCarsService(Lab1Context context, IMemoryCache memoryCache)
        {
            db = context;
            cache = memoryCache;
            _rowsNumber = 20;
        }





        public IEnumerable<Car> GetCars()
        {
            string cacheKey = "GetCars";
            IEnumerable<Car> cars = null;
            if (!cache.TryGetValue(cacheKey, out cars))
            {

                Console.WriteLine($"Машины извлечены из базы данных");
                cars = db.Cars.Take(_rowsNumber).ToList();
                if (cars != null)
                {
                    cache.Set(cacheKey, cars,
                        new MemoryCacheEntryOptions().SetAbsoluteExpiration(TimeSpan.FromSeconds(296)));
                }
            }
            else
            {

                Console.WriteLine($"Машины извлечены из кэша");
            }

            return cars;
        }
    }
}
