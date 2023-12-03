using Cargo.Models;
using static System.Net.Mime.MediaTypeNames;

namespace Cargo.ViewModels
{
    public class TransportationTariffsViewModel
    {
        public IEnumerable<TransportationTariff> TransportationTariffs{ get; }
        public PageViewModel PageViewModel { get; }
        public FilterTransportationTariffsViewModel FilterTransportationTariffsViewModel { get; }

        //public ApplicationUser ApplicationUser { get; }
        public TransportationTariffsViewModel(IEnumerable<TransportationTariff> transportationTariffs, PageViewModel viewModel, FilterTransportationTariffsViewModel filterTransportationTariffsViewModel)
        {
            TransportationTariffs = transportationTariffs;
            PageViewModel = viewModel;
            FilterTransportationTariffsViewModel = filterTransportationTariffsViewModel;
        }

    }
}
