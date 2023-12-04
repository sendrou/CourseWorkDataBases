using Cargo.Models;
using static System.Net.Mime.MediaTypeNames;

namespace Cargo.ViewModels
{
    public class CarsViewModel
    {
        public IEnumerable<Car> Cars{ get; }
        public IEnumerable<CargoTransportation> Cargo { get; }
        public PageViewModel PageViewModel { get; }
        public FilterCarsViewModel FilterCarsViewModel { get; }

        //public ApplicationUser ApplicationUser { get; }
        public CarsViewModel(IEnumerable<Car> cars, IEnumerable<CargoTransportation> cargo, PageViewModel viewModel, FilterCarsViewModel filterCarsViewModel)
        {
            Cargo = cargo;
            Cars = cars;
            PageViewModel = viewModel;
            FilterCarsViewModel = filterCarsViewModel;
        }

    }
}
