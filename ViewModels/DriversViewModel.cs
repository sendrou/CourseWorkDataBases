using Cargo.Models;
using static System.Net.Mime.MediaTypeNames;

namespace Cargo.ViewModels
{
    public class DriversViewModel
    {
        public IEnumerable<Driver> Drivers{ get; }
        public IEnumerable<CargoTransportation> Cargo  { get; }
        public PageViewModel PageViewModel { get; }
        public FilterDriversViewModel FilterDriversViewModel { get; }

        //public ApplicationUser ApplicationUser { get; }
        public DriversViewModel(IEnumerable<Driver> drivers, IEnumerable<CargoTransportation> cargo, PageViewModel viewModel, FilterDriversViewModel filterDriversViewModel)
        {
            Cargo = cargo;
            Drivers = drivers;
            PageViewModel = viewModel;
            FilterDriversViewModel = filterDriversViewModel;
        }

    }
}
