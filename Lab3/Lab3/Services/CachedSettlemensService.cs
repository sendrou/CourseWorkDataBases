using Lab02;
using Lab3.Models;
using Microsoft.Extensions.Caching.Memory;

namespace Lab03.Services
{
    public class CachedSettlemensService
    {
        private Lab1Context db;
        private IMemoryCache cache;
        private int _rowsNumber;
        public CachedSettlemensService(Lab1Context context, IMemoryCache memoryCache)
        {
            db = context;
            cache = memoryCache;
            _rowsNumber = 20;
        }

        public IEnumerable<Settlement> GetSettlements()
        {
            string cacheKey = "GetSettlements";
            IEnumerable<Settlement> settlements = null;
            if (!cache.TryGetValue(cacheKey, out settlements))
            {

                Console.WriteLine($"Населенные пункты извлечены из базы данных");
                settlements = db.Settlements.Take(_rowsNumber).ToList();
                if (settlements != null)
                {
                    cache.Set(cacheKey, settlements,
                        new MemoryCacheEntryOptions().SetAbsoluteExpiration(TimeSpan.FromSeconds(296)));
                }
            }
            else
            {

                Console.WriteLine($"Населенные пункты извлечены из кэша");
            }

            return settlements;
        }
    }
}
