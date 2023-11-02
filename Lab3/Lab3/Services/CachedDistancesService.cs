using Lab02;
using Lab3.Models;
using Microsoft.Extensions.Caching.Memory;
using static System.Net.Mime.MediaTypeNames;

namespace Lab03.Services
{
    public class CachedDistancesService
    {

        private Lab1Context db;
        private IMemoryCache cache;
        private int _rowsNumber;
        public CachedDistancesService(Lab1Context context, IMemoryCache memoryCache)
        {
            db = context;
            cache = memoryCache;
            _rowsNumber = 20;
        }

        public IEnumerable<Distance> GetSettlements()
        {
            string cacheKey = "GetSettlements";
            IEnumerable<Distance> distances = null;
            if (!cache.TryGetValue(cacheKey, out distances))
            {

                Console.WriteLine($"Расстояния извлечены из базы данных");
                distances = db.Distances.Take(_rowsNumber).ToList();
                if (distances != null)
                {
                    cache.Set(cacheKey, distances,
                        new MemoryCacheEntryOptions().SetAbsoluteExpiration(TimeSpan.FromSeconds(296)));
                }
            }
            else
            {

                Console.WriteLine($"Расстояния пункты извлечены из кэша");
            }

            return distances;
        }

    }
}
