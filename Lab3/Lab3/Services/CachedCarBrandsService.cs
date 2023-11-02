using Lab02;
using Lab3.Models;
using Microsoft.Extensions.Caching.Memory;

namespace Lab3.Services
{
    public class CachedCarBrandsService
    {
        private Lab1Context db;
        private IMemoryCache cache;
        private int _rowsNumber;
        public CachedCarBrandsService(Lab1Context context, IMemoryCache memoryCache)
        {
            db = context;
            cache = memoryCache;
            _rowsNumber = 20;
        }

        public IEnumerable<CarBrand> GetCarBrands()
        {
            string cacheKey = "GetCarBrands";
            IEnumerable<CarBrand> carBrands = null;
            if (!cache.TryGetValue(cacheKey, out carBrands))
            {

                Console.WriteLine($"Марки машин извлечены из базы данных");
                carBrands = db.CarBrands.Take(_rowsNumber).ToList();
                if (carBrands != null)
                {
                    cache.Set(cacheKey, carBrands,
                        new MemoryCacheEntryOptions().SetAbsoluteExpiration(TimeSpan.FromSeconds(296)));
                }
            }
            else
            {

                Console.WriteLine($"Марки машин извлечены из кэша");
            }

            return carBrands;
        }

    }
}
