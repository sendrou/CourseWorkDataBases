using Cargo.Models;
using static System.Net.Mime.MediaTypeNames;

namespace Cargo.ViewModels
{
    public class DriversViewModel
    {
        public IEnumerable<Driver> Drivers{ get; }
        public PageViewModel PageViewModel { get; }
        public FilterDriversViewModel FilterDriversViewModel { get; }

        //public ApplicationUser ApplicationUser { get; }
        public DriversViewModel(IEnumerable<Driver> drivers, PageViewModel viewModel, FilterDriversViewModel filterDriversViewModel)
        {
            Drivers = drivers;
            PageViewModel = viewModel;
            FilterDriversViewModel = filterDriversViewModel;
        }

    }
}
