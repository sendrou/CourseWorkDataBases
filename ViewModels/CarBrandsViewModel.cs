using Cargo.Models;
using static System.Net.Mime.MediaTypeNames;

namespace Cargo.ViewModels
{
    public class CarBrandsViewModel
    {
        public IEnumerable<CarBrand> CarBrands { get; }
        public PageViewModel PageViewModel { get; }
        public FilterCarBrandsViewModel FilterCarBrandsViewModel { get; }

        //public ApplicationUser ApplicationUser { get; }
        public CarBrandsViewModel(IEnumerable<CarBrand> carBrands, PageViewModel viewModel, FilterCarBrandsViewModel filterCarBrandsViewModel)
        {
            CarBrands = carBrands;
            PageViewModel = viewModel;
            FilterCarBrandsViewModel = filterCarBrandsViewModel;
        }

    }
}

