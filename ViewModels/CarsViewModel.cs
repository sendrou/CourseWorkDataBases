using Cargo.Models;
using static System.Net.Mime.MediaTypeNames;

namespace Cargo.ViewModels
{
    public class CarsViewModel
    {
        public IEnumerable<Car> Cars{ get; }
        public PageViewModel PageViewModel { get; }
        public FilterCarsViewModel FilterCarsViewModel { get; }

        //public ApplicationUser ApplicationUser { get; }
        public CarsViewModel(IEnumerable<Car> cars, PageViewModel viewModel, FilterCarsViewModel filterCarsViewModel)
        {
            Cars = cars;
            PageViewModel = viewModel;
            FilterCarsViewModel = filterCarsViewModel;
        }

    }
}
