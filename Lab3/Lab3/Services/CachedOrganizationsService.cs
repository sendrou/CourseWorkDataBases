using Lab02;
using Lab3.Models;
using Microsoft.Extensions.Caching.Memory;

namespace Lab03.Services
{
    public class CachedOrganizationsService
    {
        private Lab1Context db;
        private IMemoryCache cache;
        private int _rowsNumber;
        public CachedOrganizationsService(Lab1Context context, IMemoryCache memoryCache)
        {
            db = context;
            cache = memoryCache;
            _rowsNumber = 20;
        }
        public IEnumerable<Organization> GetOrganizationsAll()
        {
            IEnumerable<Organization> organizations = new List<Organization>();
            organizations = db.Organizations.ToList();

            return organizations;
        }

        public IEnumerable<Organization> GetOrganizations()
        {
            string cacheKey = "GetOrganizations";
            IEnumerable<Organization> organizations = null;
            if (!cache.TryGetValue(cacheKey, out organizations))
            {

                Console.WriteLine($"Организации извлечены из базы данных");
                organizations = db.Organizations.Take(_rowsNumber).ToList();
                if (organizations != null)
                {
                    cache.Set(cacheKey, organizations,
                        new MemoryCacheEntryOptions().SetAbsoluteExpiration(TimeSpan.FromSeconds(296)));
                }
            }
            else
            {

                Console.WriteLine($"Организации извлечены из кэша");
            }

            return organizations;
        }
    }
}