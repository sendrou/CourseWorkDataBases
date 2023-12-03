using Cargo.Models;
using static System.Net.Mime.MediaTypeNames;

namespace Cargo.ViewModels
{
    public class CarBrandsViewModel
    {
        public IEnumerable<CarBrand> CarBrands{ get; }
        public PageViewModel PageViewModel { get; }


        //public ApplicationUser ApplicationUser { get; }
        public CarBrandsViewModel(IEnumerable<CarBrand> carBrands, PageViewModel viewModel)
        {
            CarBrands = carBrands;
            PageViewModel = viewModel;
        }

    }
}
