using Lab02;
using Lab3.Models;
using Microsoft.Extensions.Caching.Memory;

namespace Lab3.Services
{
    public class CachedTransportationTariffsService
    {
        private Lab1Context db;
        private IMemoryCache cache;
        private int _rowsNumber;
        public CachedTransportationTariffsService(Lab1Context context, IMemoryCache memoryCache)
        {
            db = context;
            cache = memoryCache;
            _rowsNumber = 20;
        }

        public IEnumerable<TransportationTariff> GetTransportationTariffsAll()
        {
            IEnumerable<TransportationTariff> transportationTariffs = new List<TransportationTariff>();
            transportationTariffs = db.TransportationTariffs.ToList();

            return transportationTariffs;
        }

        public IEnumerable<TransportationTariff> GetTransportationTariffs()
        {
            string cacheKey = "GetTransportationTariffs";
            IEnumerable<TransportationTariff> transportationTariffs = null;
            if (!cache.TryGetValue(cacheKey, out transportationTariffs))
            {

                Console.WriteLine($"Тарифы извлечены из базы данных");
                transportationTariffs = db.TransportationTariffs.Take(_rowsNumber).ToList();
                if (transportationTariffs != null)
                {
                    cache.Set(cacheKey, transportationTariffs,
                        new MemoryCacheEntryOptions().SetAbsoluteExpiration(TimeSpan.FromSeconds(296)));
                }
            }
            else
            {

                Console.WriteLine($"Тарифы извлечены из кэша");
            }

            return transportationTariffs;
        }
    }
}
