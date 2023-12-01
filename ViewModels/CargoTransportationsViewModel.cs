using Cargo.Models;
using static System.Net.Mime.MediaTypeNames;

namespace Cargo.ViewModels
{
    public class CargoTransportationsViewModel
    {
        public IEnumerable<CargoTransportation> CargoTransportations{ get; }
        public PageViewModel PageViewModel { get; }

        public FilterCargoTransportationsViewModel FilterCargoTransportationsViewModel { get; }


        public ApplicationUser ApplicationUser { get; }
        public CargoTransportationsViewModel(IEnumerable<CargoTransportation> cargoTransportations, PageViewModel viewModel, FilterCargoTransportationsViewModel filterCargoTransportationsViewModel)
        {
            CargoTransportations = cargoTransportations;
            PageViewModel = viewModel;
            FilterCargoTransportationsViewModel = filterCargoTransportationsViewModel;
        }

    }
}
