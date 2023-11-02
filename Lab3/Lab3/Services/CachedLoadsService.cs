using Lab02;
using Lab3.Models;
using Microsoft.Extensions.Caching.Memory;

namespace Lab03.Services
{
    public class CachedLoadsService
    {

        private Lab1Context db;
        private IMemoryCache cache;
        private int _rowsNumber;
        public CachedLoadsService(Lab1Context context, IMemoryCache memoryCache)
        {
            db = context;
            cache = memoryCache;
            _rowsNumber = 20;
        }

        public IEnumerable<Load> GetLoads()
        {
            string cacheKey = "GetLoads";
            IEnumerable<Load> loads = null;
            if (!cache.TryGetValue(cacheKey, out loads))
            {

                Console.WriteLine($"Грузы извлечены из базы данных");
                loads = db.Loads.Take(_rowsNumber).ToList();
                if (loads != null)
                {
                    cache.Set(cacheKey, loads,
                        new MemoryCacheEntryOptions().SetAbsoluteExpiration(TimeSpan.FromSeconds(296)));
                }
            }
            else
            {

                Console.WriteLine($"Грузы извлечены из кэша");
            }

            return loads;
        }
        
    }
}