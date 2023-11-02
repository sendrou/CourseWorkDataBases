using Lab02;
using Lab3.Models;
using Microsoft.Extensions.Caching.Memory;

namespace Lab03.Services
{
    public class CachedDriversService
    {
        private Lab1Context db;
        private IMemoryCache cache;
        private int _rowsNumber;
        public CachedDriversService(Lab1Context context, IMemoryCache memoryCache)
        {
            db = context;
            cache = memoryCache;
            _rowsNumber = 20;
        }

        public IEnumerable<Driver> GetDrivers()
        {
            string cacheKey = "GetDrivers";
            IEnumerable<Driver> drivers = null;
            if (!cache.TryGetValue(cacheKey, out drivers))
            {

                Console.WriteLine($"Водители извлечены из базы данных");
                drivers = db.Drivers.Take(_rowsNumber).ToList();
                if (drivers != null)
                {
                    cache.Set(cacheKey, drivers,
                        new MemoryCacheEntryOptions().SetAbsoluteExpiration(TimeSpan.FromSeconds(296)));
                }
            }
            else
            {

                Console.WriteLine($"Водители извлечены из кэша");
            }

            return drivers;
        }
    }
}
